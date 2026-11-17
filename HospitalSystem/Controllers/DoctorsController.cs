using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HospitalSystem.Data; // غير الـ Namespace لو مختلف
using HospitalSystem.Models;

namespace HospitalSystem.Controllers // غير الـ Namespace حسب مشروعك
{
    public class DoctorsController : Controller
    {
        // التعديل هنا
        private readonly ApplicationDbContext _context;

        public DoctorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "id", "name");
            ViewBag.Specializations = new SelectList(_context.Specializations, "id", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Doctors.Add(doctor);
                _context.SaveChanges();
                return RedirectToAction("Create"); // رجعه لنفس الصفحة بعد الحفظ
            }

            ViewBag.Departments = new SelectList(_context.Departments, "id", "name", doctor.department_id);
            ViewBag.Specializations = new SelectList(_context.Specializations, "id", "name", doctor.specialization_id);

            return View(doctor);
        }
    }
}