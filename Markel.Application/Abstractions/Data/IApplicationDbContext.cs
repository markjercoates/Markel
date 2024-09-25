using Markel.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Markel.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Company> Companies { get; }
    DbSet<Claim> Claims { get;  }
    DbSet<ClaimType> ClaimTypes { get;  }   
}