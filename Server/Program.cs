using API.Extensions;

try
{
    WebApplication
        .CreateBuilder(args)
        .ConfigureBuilder()
        .Build()
        .ConfigureApplication()
        .Run();
}
catch (Exception ex)
{
    // log exceptions
}
finally
{
    // log something
}

