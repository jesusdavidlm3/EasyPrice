using ServerSideAPI.Interfaces;
using ServerSideAPI.Repositories;
using ServerSideAPI.Services;

public static class ApiRest
{
    private static WebApplication app;
    private static DbService _dbService;
    
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder();

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        if (args.Contains("-desktop"))
        {
            _dbService = new DbService(true);
        }
        else
        {
            _dbService = new DbService();
        }
        
        builder.Services.AddSingleton(_dbService);
        builder.Services.AddSingleton<IProductsRepo, ProductsRepo>();
        builder.Services.AddSingleton(new SettingsRepo());
        
        app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseAuthentication();

        app.MapControllers();

        if (args.Contains("--start-server"))
        {
            start();
        }
    }

    public static void start()
    {
        app.Run();
    }

    public static void stop()
    {
        app.StopAsync();
    }
}