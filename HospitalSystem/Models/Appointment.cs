using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagementSystem.Models
{
    public class Appointment
    {
        [Key]
        public int id { get; set; }

        public TimeSpan time { get; set; }
        public DateTime date { get; set; }
        public string note { get; set; }

        // Foreign Keys (Exact names)
        public int doctor_id { get; set; }
        public int patient_id { get; set; }

        // Navigation Properties
        [ForeignKey("doctor_id")]
        public Doctor Doctor { get; set; }

        [ForeignKey("patient_id")]
        public Patient Patient { get; set; }

        // Navigation Property: One Appointment can have Many Prescriptions
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}