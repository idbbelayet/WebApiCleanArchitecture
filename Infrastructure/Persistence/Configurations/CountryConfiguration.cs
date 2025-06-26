using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(c => c.CountryId);
            builder.Property(c => c.CountryName).IsRequired().HasMaxLength(100);
            builder.Property(c => c.RegionId).IsRequired();

            // Many-to-One relationship with Region
            builder.HasOne(c => c.Region)
                   .WithMany(r => r.Countries)
                   .HasForeignKey(c => c.RegionId);
        }
    }
}
