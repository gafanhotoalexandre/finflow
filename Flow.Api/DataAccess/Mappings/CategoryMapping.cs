using Flow.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flow.Api.DataAccess.Mappings;

public class CategoryMapping : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title)
            .IsRequired()
            .HasColumnType("NVARCHAR(80)")
            .HasMaxLength(80);

        builder.Property(c => c.Description)
            .IsRequired(false)
            .HasColumnType("NVARCHAR(255)")
            .HasMaxLength(255);

        builder.Property(c => c.UserId)
            .IsRequired()
            .HasColumnType("VARCHAR(160)") // his email
            .HasMaxLength(160);
    }
}
