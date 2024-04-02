using Microsoft.EntityFrameworkCore;
using Server.Entities;
using Server.Models;
using System.Text.Json;

namespace Server.Infrastructure.Persistence;

public class AppDbContextInitialiser
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public AppDbContextInitialiser(AppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;

    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsNpgsql())
            {
                var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                    await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
           // Log errors ..
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            if(!_context.Products.Any())
                await TrySeedProducts();

        }
        catch (Exception ex)
        {
            // Log errors ..
            throw;
        }
    }

    public async Task TrySeedProducts()
    {
        var wwwrootPath = _environment.WebRootPath;
        var fullPath = Path.Combine(wwwrootPath, "dataset/dataset.json");
        var jsonContent = File.ReadAllText(fullPath);

        var products = JsonSerializer.Deserialize<List<ProductDto>>(jsonContent);

        foreach(var prod in products)
        {
            var product = new Product()
            {
                Id = prod.Id,
                Name = prod.Name,
                Image = prod.Image,
                Price = prod.OnlyPrice,
                Type = prod.Type,
                IsSmart = prod.IsProductSmart,
                ReleaseDate = prod.ReleaseDate,
                NextMaintenanceDate = prod.NextMaintenanceDate
            };

            await _context.Products.AddAsync(product);
        }

        await _context.SaveChangesAsync();
    }
}
