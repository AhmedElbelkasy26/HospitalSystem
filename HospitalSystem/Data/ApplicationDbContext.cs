using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for Hospital System Entities
        public DbSet<Department> Departments { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // منع الـ Cascade Delete على جدول الروشتات لتجنب تضارب المسارات
            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Doctor)
                .WithMany()
                .HasForeignKey(p => p.doctor_id)
                .OnDelete(DeleteBehavior.NoAction); // <--- الحل هنا

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Appointment)
                .WithMany()
                .HasForeignKey(p => p.appointment_id)
                .OnDelete(DeleteBehavior.NoAction); // <--- وهنا برضه لو لزم الأمر
        }


    }
}
