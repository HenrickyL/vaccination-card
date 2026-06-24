using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Infrastructure.Persistence.Configurations;

public class PatientConfiguration : BaseEntityConfiguration<Patient>
{
    public override void Configure(EntityTypeBuilder<Patient> builder)
    {
        base.Configure(builder);

        builder.ToTable("Patients");


        builder.Property(p => p.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.IdentificationNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(p => p.IdentificationNumber)
            .IsUnique();
    }
}