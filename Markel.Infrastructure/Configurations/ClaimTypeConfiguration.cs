using Markel.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Markel.Infrastructure.Configurations;

public class ClaimTypeConfiguration : IEntityTypeConfiguration<ClaimType>
{
    public void Configure(EntityTypeBuilder<ClaimType> builder)
    {
        builder.ToTable("ClaimTypes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
    }
}