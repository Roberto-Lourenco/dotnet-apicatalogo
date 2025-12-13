using APICatalogo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICatalogo.Data.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(p => p.Price)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(p => p.ImgUrl)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.AvailableQuantity)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder.Property(p => p.UpdatedAt)
            .HasColumnType("timestamp with time zone");

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(p => p.Name);
    }
}
