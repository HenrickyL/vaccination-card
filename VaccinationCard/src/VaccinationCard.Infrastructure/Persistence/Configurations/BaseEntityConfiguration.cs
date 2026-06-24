using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaccinationCard.Domain.Common;

public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");//postgreSQL

        builder.Property(e => e.UpdatedAt)
            .IsRequired(false);

        builder.Property(e => e.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.DeletedAt)
            .IsRequired(false);

        // Query active entities
        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}