using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Dsw2026Ej15.Data;

public class MedicalDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Speciality> Specialities { get; set; }
    public MedicalDbContext(DbContextOptions<MedicalDbContext   > options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Doctor>(e =>
        {
            e.ToTable("Doctors");
            e.Property(d => d.Name).HasMaxLength(500).IsRequired();
            e.Property(d => d.LicenseNumber).HasMaxLength(50).IsRequired();
            e.HasIndex(d => d.LicenseNumber).IsUnique();
        });

        modelBuilder.Entity<Speciality>(e =>
        {
            e.ToTable("Specialities");
            e.Property(s => s.Name).HasMaxLength(500).IsRequired();
            e.Property(s => s.Description).HasMaxLength(300).IsRequired();
        });
    }
}
