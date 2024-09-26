using Markel.Application.Abstractions.Data;
using Markel.Application.Abstractions.Repositories;
using Markel.Application.Abstractions.Time;
using Markel.Infrastructure.Data;
using Markel.Infrastructure.Repositories;
using Markel.Infrastructure.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;

namespace Markel.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                        IConfiguration configuration)
    {
      
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        
        string connectionString = configuration.GetConnectionString("Database") ??
                                  throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));
        services.AddScoped<IClaimRepository, ClaimRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IClaimTypeRepository, ClaimTypeRepository>();
        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        
        services.AddHttpContextAccessor();

        return services;
    }
}