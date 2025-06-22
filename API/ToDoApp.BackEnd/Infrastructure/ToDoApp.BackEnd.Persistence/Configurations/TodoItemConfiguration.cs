using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApp.BackEnd.Domain.Entities;
using ToDoApp.BackEnd.Persistence.Configurations.Abstract;

namespace ToDoApp.BackEnd.Persistence.Configurations;
public sealed class TodoItemConfiguration : BaseEntityTypeConfiguration<TodoItem>
{
    public override void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.ToTable("TodoItems");

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnType("nvarchar(200)");

        builder.Property(t => t.Description)
            .HasMaxLength(1000)
            .HasColumnType("nvarchar(1000)");

        builder.Property(x => x.DueDate)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(t => t.Status)
            .HasConversion<int>()
            .HasColumnType("int")
            .IsRequired();

        builder.HasOne(x=>x.AppUser)
            .WithMany(x => x.TodoItems)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
