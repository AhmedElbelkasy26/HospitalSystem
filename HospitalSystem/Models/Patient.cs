using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.Models
{
    public class Patient
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }
        public int age { get; set; }
        public string phone { get; set; }
        public string insurance { get; set; }

        // Navigation Properties
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}