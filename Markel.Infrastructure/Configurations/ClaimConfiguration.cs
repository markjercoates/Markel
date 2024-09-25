using Markel.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Markel.Infrastructure.Configurations;

public class ClaimConfiguration : IEntityTypeConfiguration<Claim>
{
    public void Configure(EntityTypeBuilder<Claim> builder)
    {
        builder.ToTable("Claims");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UCR).IsRequired().HasMaxLength(20);
        builder.Property(e => e.AssuredName).IsRequired().HasMaxLength(100);
        builder.Ignore(e => e.NumberOfDays);
        
        builder.HasOne(claim => claim.Company)
            .WithMany(company => company.Claims)
            .HasPrincipalKey(company => company.Id)
            .HasForeignKey(claim => claim.CompanyId);
        
        builder.HasOne(claim => claim.ClaimType)
               .WithMany(claimType => claimType.Claims)
               .HasPrincipalKey(claimType => claimType.Id)
               .HasForeignKey(claim => claim.ClaimTypeId);
        
        builder.HasIndex(x => x.UCR).IsUnique();
    }
}