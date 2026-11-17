using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.ViewModels
{
    public class AddMedicineVM
    {
        public int PrescriptionId { get; set; }
        public string PatientName { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Medicine { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Dose { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Duration { get; set; } = string.Empty;
    }
}
