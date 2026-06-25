using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Infrastructure.Persistence.Configurations;

public class VaccineConfiguration : BaseEntityConfiguration<Vaccine>
{
    public override void Configure(EntityTypeBuilder<Vaccine> builder)
    {
        base.Configure(builder);
        builder.ToTable("Vaccines");

        builder.Property(u => u.Name)
           .IsRequired()
           .HasMaxLength(63);

        builder.Property(u => u.Description)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.Code)
           .IsRequired()
           .HasMaxLength(15);

        builder.HasIndex(u => u.Code)
            .IsUnique();

    }
}
