using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApp.BackEnd.Domain.Entities.Common;

namespace ToDoApp.BackEnd.Persistence.Configurations.Abstract
{
    public abstract class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            // Define primary key
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).IsRequired().UseIdentityColumn(); // Configure ID to use identity column

            builder.Property(x => x.RowStatus)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.CreatedUser)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);

            builder.Property(x => x.UpdatedUser)
                .IsRequired(false)
                .HasMaxLength(128);
        }
    }
}
