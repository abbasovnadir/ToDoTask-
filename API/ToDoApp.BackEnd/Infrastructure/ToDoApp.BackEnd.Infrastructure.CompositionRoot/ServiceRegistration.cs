using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.BackEnd.Application.Interfaces.Services;
using ToDoApp.BackEnd.Application.Interfaces.Services.ApplicationServices;
using ToDoApp.BackEnd.Application.Interfaces.Services.AuthServices;
using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
using ToDoApp.BackEnd.Infrastructure.CompositionRoot.Services.Concrete;
using ToDoApp.BackEnd.Infrastructure.CompositionRoot.Services.Interfaces;
using ToDoApp.BackEnd.Infrastructure.Services.ApplicationServices;
using ToDoApp.BackEnd.Infrastructure.Services.AuthServices;
using ToDoApp.BackEnd.Persistance.Repositories.Concrete.UnitOfWork;
using ToDoApp.BackEnd.Persistence.Contexts.EntityFramework;

namespace ToDoApp.BackEnd.Infrastructure.CompositionRoot
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ToDoAppContext>(x =>
            {
                x.UseSqlServer(configuration.GetConnectionString("Local"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddSingleton<IHashingService, HashingHelper>();
            services.AddSingleton<IEmailServices, EmailServices>();

            // Add the seeding service
            services.AddScoped<ISeedDataService, SeedDataService>();
        }
    }

}
