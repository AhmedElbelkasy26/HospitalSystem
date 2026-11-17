using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace HospitalSystem.Models
{
    public class Department
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }

        // Navigation Property: One Department has Many Doctors
        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}