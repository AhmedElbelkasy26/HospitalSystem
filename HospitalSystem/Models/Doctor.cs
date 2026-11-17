using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagementSystem.Models
{
    public class Doctor
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }

        // Foreign Keys (Exact names)
        public int department_id { get; set; }
        public int specialization_id { get; set; }

        // Navigation Properties (Foreign Key References)
        [ForeignKey("department_id")]
        public Department Department { get; set; }

        [ForeignKey("specialization_id")]
        public Specialization Specialization { get; set; }

        // Navigation Properties (Collections)
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}