using Markel.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Markel.Infrastructure.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Address1).HasMaxLength(100);
        builder.Property(e => e.Address2).HasMaxLength(100);
        builder.Property(e => e.Address3).HasMaxLength(100);
        builder.Property(e => e.PostCode).HasMaxLength(20);
        builder.Property(e => e.Country).HasMaxLength(50);
    }
}