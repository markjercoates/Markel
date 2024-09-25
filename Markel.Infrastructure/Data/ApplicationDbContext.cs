using Markel.Application.Abstractions.Data;
using Markel.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Markel.Infrastructure.Data;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Company> Companies { get; private set; }
    
    public DbSet<Claim> Claims { get; private set; }
    
    public DbSet<ClaimType> ClaimTypes { get; private set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}