using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalSystem.Models
{
    public class Prescription
    {
        [Key]
        public int id { get; set; }

        public string medicine { get; set; }
        public DateTime date { get; set; }
        public string duration { get; set; }
        public string dose { get; set; }

        // Foreign Keys (Exact names)
        public int doctor_id { get; set; }
        public int patient_id { get; set; }
        

        // Navigation Properties
        [ForeignKey("doctor_id")]
        public Doctor Doctor { get; set; }

        [ForeignKey("patient_id")]
        public Patient Patient { get; set; }

     
    }
}