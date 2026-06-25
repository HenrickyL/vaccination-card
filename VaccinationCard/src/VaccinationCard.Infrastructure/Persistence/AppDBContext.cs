using Microsoft.EntityFrameworkCore;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Vaccine> Vaccines => Set<Vaccine>();
    public DbSet<VaccineRegister> VaccineRegistrations => Set<VaccineRegister>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}