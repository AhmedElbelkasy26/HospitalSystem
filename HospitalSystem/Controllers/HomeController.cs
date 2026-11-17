using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using HospitalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;

            var model = new DashboardViewModel
            {
               
                PatientsToday = await _context.Appointments
                    .Where(a => a.date.Date == today).CountAsync(),

                AppointmentsToday = await _context.Appointments
                    .Where(a => a.date.Date == today).CountAsync(),

                TodaysAppointments = await _context.Appointments
                    .Include(a => a.Patient)
                    .Include(a => a.Doctor)
                    .ThenInclude(d => d.Department)
                    .Where(a => a.date.Date == today)
                    .OrderBy(a => a.time)
                    .ToListAsync()
            };

            return View(model);
        }
    }
}