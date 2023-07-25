using MvDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MvDb.Infrastructure.Persistence.Configurations;

public class AgeRatingConfiguration : IEntityTypeConfiguration<AgeRating>
{
    public void Configure(EntityTypeBuilder<AgeRating> builder)
    {
        builder.Property(a => a.Name)
            .HasMaxLength(20)
            .IsRequired();
    }
}
