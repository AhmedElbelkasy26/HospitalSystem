using Ecommerce533.Reposetories;
using HospitalSystem.Models;

namespace HospitalSystem.Reposetories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Specialization> Specializations { get; }
        IRepository<Department> Departments { get; }
        IRepository<Doctor> Doctors { get; }
        IRepository<Patient> Patients { get; }
        IRepository<Appointment> Appointments { get; }
        Task<int> CommitAsync();
    }
}
