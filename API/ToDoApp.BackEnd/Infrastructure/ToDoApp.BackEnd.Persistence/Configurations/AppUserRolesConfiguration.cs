using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApp.BackEnd.Domain.Entities;

namespace ToDoApp.BackEnd.Persistence.Configurations;
public sealed class AppUserRolesConfiguration : IEntityTypeConfiguration<AppUserRoles>
{
    public void Configure(EntityTypeBuilder<AppUserRoles> builder)
    {
        builder.ToTable("AspNetUserRoles");

        builder.HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.Property(ur => ur.UserId)
       .IsRequired()
       .HasColumnType("uniqueidentifier");

        // RoleId property'si
        builder.Property(ur => ur.RoleId)
            .IsRequired()
            .HasColumnType("uniqueidentifier");

        builder.HasOne(x => x.AppUser)
            .WithMany(x => x.AppUserRoles)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.AppRole)
            .WithMany(x => x.AppUserRoles)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
