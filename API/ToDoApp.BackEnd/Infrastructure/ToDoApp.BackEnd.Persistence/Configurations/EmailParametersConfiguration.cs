using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApp.BackEnd.Domain.Entities.ApplicationEntities;
using ToDoApp.BackEnd.Persistence.Configurations.Abstract;

namespace ToDoApp.BackEnd.Persistance.Configurations.ApplicationConfigurations;
public sealed class EmailParametersConfiguration : BaseEntityTypeConfiguration<EmailParameter>
{
    public override void Configure(EntityTypeBuilder<EmailParameter> builder)
    {
        builder.ToTable("EmailParameters");

        builder.Property(x => x.Email).HasMaxLength(128).IsRequired();

        builder.Property(x => x.Password).HasMaxLength(128).IsRequired();

        builder.Property(x => x.SMTP).HasMaxLength(128).IsRequired();

        builder.Property(x => x.Port).IsRequired();

        builder.Property(x => x.SSL).IsRequired();

        builder.Property(x => x.EmailParameterType).IsRequired();
    }
}
