using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApp.BackEnd.Domain.Entities;

namespace ToDoApp.BackEnd.Persistence.Configurations;
public sealed class AppUserTokenConfiguration : IEntityTypeConfiguration<AppUserToken>
{
    public void Configure(EntityTypeBuilder<AppUserToken> builder)
    {
        builder.ToTable("AppUserTokens");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).IsRequired().UseIdentityColumn();

        builder.Property(x => x.RefreshToken)
          .IsRequired()
          .HasMaxLength(2048)
            .HasColumnType("varchar(2048)");

        builder.Property(x=>x.TokenExpiredDate)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");

        builder.HasOne(x => x.AppUser)
            .WithOne(x => x.AppUserToken)
            .HasForeignKey<AppUserToken>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
