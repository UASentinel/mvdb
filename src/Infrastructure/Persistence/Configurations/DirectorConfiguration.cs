using MvDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MvDb.Infrastructure.Persistence.Configurations;

public class DirectorConfiguration : IEntityTypeConfiguration<Director>
{
    public void Configure(EntityTypeBuilder<Director> builder)
    {
        builder.Property(d => d.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(d => d.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(d => d.Biography)
            .HasMaxLength(1000);
    }
}
