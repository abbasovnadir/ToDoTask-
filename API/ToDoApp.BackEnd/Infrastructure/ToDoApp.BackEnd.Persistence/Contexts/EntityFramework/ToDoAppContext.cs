using Microsoft.EntityFrameworkCore;
using ToDoApp.BackEnd.Domain.Entities;
using ToDoApp.BackEnd.Domain.Entities.ApplicationEntities;
using ToDoApp.BackEnd.Domain.Entities.UserLoginEntities;
using ToDoApp.BackEnd.Persistance.Configurations.ApplicationConfigurations;
using ToDoApp.BackEnd.Persistance.Configurations.UserLoginConfigurations;
using ToDoApp.BackEnd.Persistence.Configurations;

namespace ToDoApp.BackEnd.Persistence.Contexts.EntityFramework;
public sealed class ToDoAppContext : DbContext
{
    public ToDoAppContext(DbContextOptions<ToDoAppContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
        modelBuilder.ApplyConfiguration(new AppUserConfiguration());
        modelBuilder.ApplyConfiguration(new AppUserRolesConfiguration());
        modelBuilder.ApplyConfiguration(new AppUserTokenConfiguration());

        modelBuilder.ApplyConfiguration(new EmailParametersConfiguration());
        modelBuilder.ApplyConfiguration(new EmailTemplateConfiguration());
        modelBuilder.ApplyConfiguration(new UserEmailTemporaryValueConfiguration());

        modelBuilder.ApplyConfiguration(new TodoItemConfiguration());
    }
    public DbSet<AppRole> AppRoles { get; set; } = null!;
    public DbSet<AppUser> AppUsers { get; set; } = null!;
    public DbSet<AppUserRoles> AppUserRoles { get; set; } = null!;
    public DbSet<AppUserToken> AppUserTokens { get; set; } = null!;
    public DbSet<EmailParameter> EmailParameters { get; set; } = null!;
    public DbSet<EmailTemplate> EmailTemplates { get; set; } = null!;
    public DbSet<TodoItem> TodoItems { get; set; } = null!;
    public DbSet<UserEmailTemporaryValue> UserEmailTemporaryValues { get; set; } = null!;
}

