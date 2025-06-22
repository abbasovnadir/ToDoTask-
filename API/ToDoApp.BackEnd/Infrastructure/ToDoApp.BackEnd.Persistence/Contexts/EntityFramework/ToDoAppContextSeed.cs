using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using ToDoApp.BackEnd.Domain.Entities;

namespace ToDoApp.BackEnd.Persistence.Contexts.EntityFramework;

public class ToDoAppContextSeed
{
    public static async Task SeedAsync(ToDoAppContext context)
    {
        // Rolleri ekle
        await SeedRolesAsync(context);

        // Super Admin kullanıcıyı ekle
        await SeedSuperAdminAsync(context);
    }

    private static async Task SeedRolesAsync(ToDoAppContext context)
    {
        if (!context.AppRoles.Any())
        {
            var roles = new List<AppRole>
            {
        new AppRole { Id = Guid.NewGuid(), Name = "Super Admin", IsActive = true, CreatedAt = DateTime.UtcNow },
        new AppRole { Id = Guid.NewGuid(), Name = "Admin", IsActive = true, CreatedAt = DateTime.UtcNow },
        new AppRole { Id = Guid.NewGuid(), Name = "User", IsActive = true, CreatedAt = DateTime.UtcNow }
    };

            await context.AppRoles.AddRangeAsync(roles);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedSuperAdminAsync(ToDoAppContext context)
    {
        var email = "superadmin@example.com";
        if (!context.AppUsers.Any(u => u.Email == email))
        {
            // Super Admin kullanıcısı oluştur
            var password = "SuperAdmin123!";
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var superAdminUser = new AppUser
            {
                Id = Guid.NewGuid(),
                Name = "Super Admin",
                Email = email,
                PaswordSalt = passwordSalt,
                PaswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                MailConfirm = true,
                MailConfirmValue = "some_value",
                MailConfirmDate = DateTime.UtcNow
            };

            await context.AppUsers.AddAsync(superAdminUser);
            await context.SaveChangesAsync();

            // Super Admin rolünü atama
            var superAdminRole = await context.AppRoles.FirstOrDefaultAsync(r => r.Name == "Super Admin");
            if (superAdminRole != null)
            {
                var userRole = new AppUserRoles
                {
                    UserId = superAdminUser.Id,
                    RoleId = superAdminRole.Id
                };
                await context.AppUserRoles.AddAsync(userRole);
                await context.SaveChangesAsync();
            }
        }
    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
