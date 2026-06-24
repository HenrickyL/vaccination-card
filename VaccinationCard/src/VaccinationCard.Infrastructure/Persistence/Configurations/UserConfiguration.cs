using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Enums;

namespace VaccinationCard.Infrastructure.Persistence.Configurations;

public  class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        builder.ToTable("Users");

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.Status)
            .HasConversion<int>()
            .IsRequired()
            .HasDefaultValue(AccountStatus.Active);

        builder.Property(u => u.PatientId)
            .IsRequired(false);

    
        builder.Property(u => u.Role)
            .HasConversion<int>()
            .IsRequired()
            .HasDefaultValue(UserRole.Patient);

        builder.Property(u => u.IsActive)
            .IsRequired()
            .HasDefaultValue(true);
    }
}
