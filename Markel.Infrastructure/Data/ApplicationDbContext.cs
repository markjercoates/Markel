using Markel.Application.Abstractions.Data;
using Markel.Application.Abstractions.Time;
using Markel.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Markel.Infrastructure.Data;
public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    private readonly IDateTimeProvider _dateTimeProvider;
    public DbSet<Company> Companies { get; private set; }
    
    public DbSet<Claim> Claims { get; private set; }
    
    public DbSet<ClaimType> ClaimTypes { get; private set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, 
                    IDateTimeProvider dateTimeProvider) :base(options)
    {
        _dateTimeProvider = dateTimeProvider;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}