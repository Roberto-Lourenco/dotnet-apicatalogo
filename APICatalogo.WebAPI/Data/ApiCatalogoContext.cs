using APICatalogo.Entities;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Context;

internal sealed class ApiCatalogoContext : DbContext
{
    public ApiCatalogoContext(DbContextOptions<ApiCatalogoContext> options): base(options)
    {
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("core");
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiCatalogoContext).Assembly);
    }
}
