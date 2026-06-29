using Microsoft.EntityFrameworkCore;
using Dsw2026Ej15.Domain.Entities;


namespace Dsw2026Ej15.Data;
public class MedicalContext : DbContext
{
    public MedicalContext(DbContextOptions<MedicalContext> options) : base(options)
    {
    }

    // Mapeamos las entidades a tablas de la base de datos
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Speciality> Specialities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuramos la tabla Specialities
        modelBuilder.Entity<Speciality>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
        });

        // Configuramos la tabla Doctors
        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
            entity.Property(d => d.LicenseNumber).IsRequired().HasMaxLength(50);
            entity.Property(d => d.IsActive).HasDefaultValue(true);

            // Relación: Un médico tiene una especialidad (1 a muchos)
            entity.HasOne(d => d.Speciality)
                  .WithMany()
                  .HasForeignKey("SpecialityId") // EF creará esta columna como clave foránea en las sombras
                  .IsRequired();
        });
    }
}
