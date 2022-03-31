using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class RollerDbContext : DbContext
{
    public RollerDbContext(DbContextOptions<RollerDbContext> options)
        : base(options)
    {
    }

    public DbSet<RollHistory> RollHistories { get; set; }
    public DbSet<DiceValue> DiceValues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RollerDbContext).Assembly);
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<Base>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreateAt = DateTime.Now;
                    entry.Entity.CreatedBy = Guid.Empty;
                    break;
                case EntityState.Modified:
                    entry.Entity.ChangedAt = DateTime.Now;
                    entry.Entity.ChangedBy = Guid.Empty;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}