using Ecommerce533.Reposetories;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;

using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Reposetories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(IRepository<Specialization> specializations, IRepository<Department> departments,
            IRepository<Doctor> doctors, IRepository<Patient> patients,
            IRepository<Appointment> appointments , ApplicationDbContext dbContext)
        {
            Specializations = specializations;
            Departments = departments;
            Doctors = doctors;
            Patients = patients;
            Appointments = appointments;
            _context = dbContext;
        }

        public IRepository<Specialization> Specializations { get; }

        public IRepository<Department> Departments { get; }

        public IRepository<Doctor> Doctors { get; }

        public IRepository<Patient> Patients { get; }

        public IRepository<Appointment> Appointments { get; }

        public async Task<int> CommitAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
