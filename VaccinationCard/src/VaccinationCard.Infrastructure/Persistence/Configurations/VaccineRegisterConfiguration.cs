using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Infrastructure.Persistence.Configurations;

public class VaccineRegisterConfiguration : BaseEntityConfiguration<VaccineRegister>
{
    public override void Configure(EntityTypeBuilder<VaccineRegister> builder)
    {
        base.Configure(builder);

        builder.ToTable("VaccineRegistrations");

        // property
        builder.Property(r => r.ApplicationDate)
            .IsRequired()
            .HasColumnType("timestamp without time zone");

        builder.Property(r => r.Dose)
            .IsRequired();


        builder.Property(r => r.Lot)
                .HasMaxLength(50)
                .HasColumnType("varchar(50)");

        builder.Property(r => r.Observations)
            .HasMaxLength(500)
            .HasColumnType("varchar(500)");

        builder.Property(r => r.RegisteredAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp with time zone");
        // --- relations
        builder.HasOne(r => r.Patient)
            .WithMany(p => p.Registrations)
            .HasForeignKey(r => r.PatientId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(r => r.Vaccine)
            .WithMany(v => v.Registrations)
            .HasForeignKey(r => r.VaccineId)
            .OnDelete(DeleteBehavior.Restrict);

        // Composite indexes
        builder.HasIndex(r => new { r.PatientId, r.VaccineId, r.Dose })
            .IsUnique()
            .HasDatabaseName("Registrations_PatientVaccineDose_Unique");

        //// performance
        builder.HasIndex(r => r.PatientId);
        builder.HasIndex(r => r.VaccineId);
        builder.HasIndex(r => r.ApplicationDate);
        builder.HasIndex(r => r.Dose);
    }
}
