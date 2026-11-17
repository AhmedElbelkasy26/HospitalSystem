

namespace HospitalSystem.Models
{
    public class DashboardViewModel
    {
        public int PatientsToday { get; set; }
        public int AppointmentsToday { get; set; }
        public int FreeBeds { get; set; } = 17;
        public int UnpaidInvoices { get; set; } = 9;
        public List<Appointment> TodaysAppointments { get; set; } = new();



        
    }
}