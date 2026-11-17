using HospitalManagementSystem.Models;

namespace HospitalSystem.ViewModels
{
    public class PrescriptionDetailsVM
    {
        // The prescription in the URL (gives patient, doctor and date for the cards)
        public Prescription Header { get; set; } = null!;

        // Every medicine for the same patient + doctor + date
        public List<Prescription> Items { get; set; } = [];
    }
}
