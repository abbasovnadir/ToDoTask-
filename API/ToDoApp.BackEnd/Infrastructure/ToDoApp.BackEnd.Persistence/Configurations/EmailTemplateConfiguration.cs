using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApp.BackEnd.Domain.Entities.ApplicationEntities;
using ToDoApp.BackEnd.Persistence.Configurations.Abstract;

namespace ToDoApp.BackEnd.Persistance.Configurations.ApplicationConfigurations
{
    public sealed class EmailTemplateConfiguration : BaseEntityTypeConfiguration<EmailTemplate>
    {
        public override void Configure(EntityTypeBuilder<EmailTemplate> builder)
        {
            builder.ToTable("EmailTemplates");

            builder.Property(x => x.Type).IsRequired();

            builder.Property(x => x.TypeName).HasMaxLength(128).IsRequired();

            builder.Property(x => x.Template).HasMaxLength(2000).IsRequired();
        }
    }
}
