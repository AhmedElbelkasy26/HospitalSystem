using System;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Models; // تأكد من اسم الـ Namespace بتاعك

namespace HospitalManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor بياخد الخيارات بتاعة الاتصال بقاعدة البيانات
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // 1. تعريف الجداول (DbSets)
        public DbSet<Department> Departments { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }

        // 2. إعداد العلاقات (Fluent API)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================================
            // Doctor Relationships
            // ==========================================

            // Doctor -> Department (Many-to-One)
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Department)
                .WithMany(dep => dep.Doctors)
                .HasForeignKey(d => d.department_id)
                .OnDelete(DeleteBehavior.Restrict); // منع مسح القسم لو فيه دكاترة

            // Doctor -> Specialization (Many-to-One)
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Specialization)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.specialization_id)
                .OnDelete(DeleteBehavior.Restrict); // منع مسح التخصص لو فيه دكاترة


            // ==========================================
            // Appointment Relationships
            // ==========================================

            // Appointment -> Doctor (Many-to-One)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.doctor_id)
                .OnDelete(DeleteBehavior.Restrict);

            // Appointment -> Patient (Many-to-One)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.patient_id)
                .OnDelete(DeleteBehavior.Cascade); // لو المريض اتمسح، مواعيده تتمسح


            // ==========================================
            // Prescription Relationships
            // ==========================================

            // Prescription -> Doctor (Many-to-One)
            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Doctor)
                .WithMany(d => d.Prescriptions)
                .HasForeignKey(p => p.doctor_id)
                .OnDelete(DeleteBehavior.Restrict);

            // Prescription -> Patient (Many-to-One)
            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Patient)
                .WithMany(p => p.Prescriptions)
                .HasForeignKey(p => p.patient_id)
                .OnDelete(DeleteBehavior.Cascade);

            // Prescription -> Appointment (Many-to-One)
            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Appointment)
                .WithMany(a => a.Prescriptions)
                .HasForeignKey(p => p.appointment_id)
                .OnDelete(DeleteBehavior.SetNull); // لو الموعد اتمسح، الروشتة تفضل بس من غير موعد
        }
    }
}