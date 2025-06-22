using ToDoApp.BackEnd.Domain.Entities.UserLoginEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoApp.BackEnd.Persistance.Configurations.UserLoginConfigurations
{
    internal class UserEmailTemporaryValueConfiguration : IEntityTypeConfiguration<UserEmailTemporaryValue>
    {
        public void Configure(EntityTypeBuilder<UserEmailTemporaryValue> builder)
        {
            builder.ToTable("UserEmailTemporaryValues");

            builder.HasKey(u => u.Id);

            // Id property'si
            builder.Property(u => u.Id)
                .IsRequired()
                .HasColumnType("uniqueidentifier")
                .ValueGeneratedOnAdd();

            builder.Property(u=>u.ConfirmValue)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.ConfirmValueType)
                .IsRequired()
                .HasColumnType("INT");

            builder.Property(x => x.ValueExpiredDate)
              .IsRequired()
              .HasDefaultValueSql("GETDATE()");

            builder.Property(u => u.IsConfirmed)
                .IsRequired()
                .HasColumnType("BIT");

            builder.Property(x => x.CreatedAt)
              .IsRequired()
              .HasDefaultValueSql("GETDATE()");


            builder.HasOne(d => d.AppUser)
             .WithOne(u => u.UserEmailTemporaryValue)
             .HasForeignKey<UserEmailTemporaryValue>(d => d.UserId)
             .OnDelete(DeleteBehavior.Restrict); // Set delete behavior to restrict
        }
    }
}
