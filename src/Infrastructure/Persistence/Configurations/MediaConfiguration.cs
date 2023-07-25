using MvDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MvDb.Infrastructure.Persistence.Configurations;

public class MediaConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.Property(m => m.Title)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(m => m.Description)
            .HasMaxLength(1000);
    }
}
