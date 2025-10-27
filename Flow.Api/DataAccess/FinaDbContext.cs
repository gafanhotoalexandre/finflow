using Flow.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Flow.Api.DataAccess;

public class FinaDbContext(DbContextOptions<FinaDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinaDbContext).Assembly);
    }
}
