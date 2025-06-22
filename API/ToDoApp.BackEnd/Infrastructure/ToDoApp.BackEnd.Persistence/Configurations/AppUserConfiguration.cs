using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApp.BackEnd.Domain.Entities;

namespace ToDoApp.BackEnd.Persistence.Configurations;
public sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("AspNetUsers");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(256)
            .HasColumnType("nvarchar(256)");

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(256)
            .HasColumnType("nvarchar(256)");

        builder.Property(u => u.PaswordSalt)
            .IsRequired()
            .HasColumnType("varbinary(max)");

        builder.Property(u => u.PaswordHash)
            .IsRequired()
            .HasColumnType("varbinary(max)");

        builder.Property(u => u.CreatedAt)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(u => u.IsActive)
            .IsRequired()
            .HasColumnType("bit");

        builder.Property(u => u.MailConfirm)
            .IsRequired()
            .HasColumnType("bit");

        builder.Property(u => u.MailConfirmValue)
            .HasMaxLength(50)
            .HasColumnType("nvarchar(50)");

        builder.Property(u => u.MailConfirmDate)
            .HasColumnType("datetime");
    }
}
