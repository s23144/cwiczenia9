using Microsoft.EntityFrameworkCore;
using System;

namespace cwiczenia8.Models
{
    public class MainDbContext : DbContext

    { 
        protected MainDbContext()
        {
        }
        public MainDbContext( DbContextOptions options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription_Medicament> Prescriptions_Medicament { get; set; }
        public DbSet<Account> Accounts { get; set; }


       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(acc =>
            {
                acc.HasKey(e => e.IdUser);
                acc.Property(e => e.IdUser).UseIdentityColumn();
                acc.Property(e=>e.Login).IsRequired().HasMaxLength(20);
                acc.HasIndex(e => e.Login).IsUnique();
                acc.Property(e=>e.Password).IsRequired();
                acc.Property(e=>e.Salt).IsRequired();
                acc.Property(e=>e.RefreshToken).IsRequired();
                acc.Property(e => e.RefreshTokenExp);

            });

            modelBuilder.Entity<Patient>(p =>
            {
            p.HasKey(e => e.IdPatient);
            p.Property(e => e.FirsName).IsRequired().HasMaxLength(100);
            p.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            p.Property(e => e.BirthDate).IsRequired();

                p.HasData(
                    new Patient { IdPatient = 1, FirsName = "Jan", LastName = "Kowalski", BirthDate = DateTime.Parse("2000-01-01") },
                    new Patient
                    {
                        IdPatient = 2,
                        FirsName = "Adam",
                        LastName = "Nowak",
                        BirthDate = DateTime.Parse("2001-01-01")
                    }



                );
            });

            modelBuilder.Entity<Doctor>(d =>
            {
                d.HasKey(e => e.IdDoctor);
                d.Property(e => e.FirsName).IsRequired().HasMaxLength(100);
                d.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                d.Property(e=> e.Email).IsRequired();


                d.HasData(
                    new Doctor { IdDoctor = 1, FirsName ="Jan",LastName ="Kowal",Email ="jan@kowal.pl"},
                    new Doctor { IdDoctor=2, FirsName ="Adam", LastName="Zbik", Email ="adam@zbik.pl"}
                    );
            });

            modelBuilder.Entity<Prescription>(p =>
            {
                p.HasKey(e => e.IdPrescription);
                p.Property(e=> e.Date).IsRequired();
                p.Property(e=> e.DueDate).IsRequired();

                p.HasOne(e => e.PatientNavigation).WithMany(e => e.Prescription).HasForeignKey(e => e.IdPatient);
                p.HasOne(e => e.DoctorNavigation).WithMany(e => e.Prescription).HasForeignKey(e => e.IdDoctor);

                p.HasData(
                    new Prescription { IdPrescription = 1, Date = DateTime.Parse("2022-01-01"), DueDate= DateTime.Parse("2022-03-01"),IdDoctor=1,IdPatient =1},
                    new Prescription
                    {
                        IdPrescription = 2,
                        Date = DateTime.Parse("2022-01-01"),
                        DueDate = DateTime.Parse("2023-03-01"),
                        IdDoctor = 2,
                        IdPatient = 2
                    }
                    );
            });

            modelBuilder.Entity<Medicament>(m =>
            {
                m.HasKey(e => e.IdMedicament);
                m.Property(e=>e.Name).IsRequired().HasMaxLength(100);
                m.Property(e => e.Description).IsRequired().HasMaxLength(100);
                m.Property(e => e.Type).IsRequired().HasMaxLength(100);

                m.HasData(
                    new Medicament { IdMedicament = 1 , Name = "Apap", Description ="Na różne bóle", Type="Przeciwbólowy" },
                    new Medicament { IdMedicament = 2 , Name ="Kaszlox",Description ="Syrop na kaszel",Type="Syropik"}

                    );

            });

            modelBuilder.Entity<Prescription_Medicament>(pm =>
            {
                pm.HasKey(e => new { e.IdMedicament, e.IdPrescription });
                
                pm.HasOne(e => e.MedicamentNavigation).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdMedicament);
                pm.HasOne(e => e.PrescriptionNavigation).WithMany(e => e.PrescriptionsMedicaments).HasForeignKey(e => e.IdPrescription);

                pm.Property(e => e.Dose);
                pm.Property(e => e.Details).IsRequired().HasMaxLength(100);

                pm.HasData(
                
                    new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1, Dose = 2, Details = "Rano i wieczorem" },
                    new Prescription_Medicament { IdMedicament = 2, IdPrescription = 2, Dose = 3, Details = "3 razy dziennie" } 
                );
            });




        }

       
    }
}
