using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.HasKey(r => r.RegionId);
            builder.Property(r => r.RegionName).IsRequired().HasMaxLength(100);
            builder.Property(r => r.IsActive).IsRequired();
            builder.Property(r => r.UserId).IsRequired();
            builder.Property(r => r.DataCollectionDate).IsRequired();

            // One-to-Many relationship with Country
            builder.HasMany(r => r.Countries)
                   .WithOne(c => c.Region)
                   .HasForeignKey(c => c.RegionId)
                   .OnDelete(DeleteBehavior.Cascade); // Adjust as needed
        }
    }
}
