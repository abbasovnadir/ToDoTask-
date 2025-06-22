using Microsoft.Extensions.DependencyInjection;
using ToDoApp.BackEnd.Infrastructure.CompositionRoot.Services.Interfaces;
using ToDoApp.BackEnd.Persistence.Contexts.EntityFramework;

namespace ToDoApp.BackEnd.Infrastructure.CompositionRoot.Services.Concrete;
public class SeedDataService : ISeedDataService
{
    private readonly IServiceProvider _serviceProvider;

    public SeedDataService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task SeedAsync()
    {
        try
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ToDoAppContext>();
                await ToDoAppContextSeed.SeedAsync(context);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Seeding failed: {ex.Message}");
        }
    }
}