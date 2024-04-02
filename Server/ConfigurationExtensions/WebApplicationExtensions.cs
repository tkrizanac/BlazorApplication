using Microsoft.EntityFrameworkCore;
using Server.Infrastructure.Interfaces;
using Server.Infrastructure.Persistence;

namespace API.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHealthChecks().AddDbContextCheck<AppDbContext>();

        var configuration = builder.Configuration;
        var conn = $"Host={configuration["DB:HOST"]};Port={configuration["DB:PORT"]};Username={configuration["DB:USERNAME"]};Password={configuration["DB:PASSWORD"]};Database={configuration["DB:NAME"]};pooling=true;";

        builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(conn, builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        builder.Services.AddScoped<IAppDbContext, AppDbContext>();
        builder.Services.AddScoped<AppDbContextInitialiser>();

        // Allow all because this is only local development to avoid fetch errors
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
        });

        return builder;
    }

    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        InitialiseDatabase().Wait();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
             SeedDatabase().Wait();
        }
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapHealthChecks("/health");
        app.MapControllers();
        app.UseCors("AllowAll");

        return app;

        async Task InitialiseDatabase()
        {
            using (var scope = app.Services.CreateScope())
            {
                var initialiser = scope.ServiceProvider.GetRequiredService<AppDbContextInitialiser>();
                await initialiser.InitialiseAsync();
            }
        }

        async Task SeedDatabase()
        {
            using (var scope = app.Services.CreateScope())
            {
                var initialiser = scope.ServiceProvider.GetRequiredService<AppDbContextInitialiser>();
                await initialiser.SeedAsync();
            }
        }
    }
}
