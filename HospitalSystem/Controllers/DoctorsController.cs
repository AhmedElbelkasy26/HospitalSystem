using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // ضفنا دي عشان الـ Include والـ Async
using HospitalSystem.Data;
using HospitalSystem.Models;

namespace HospitalSystem.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // 1. عرض كل الدكاترة (GET: Doctors/Index)
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // بنجيب كل الدكاترة مع بيانات القسم والتخصص بتاعهم
            var doctors = await _context.Doctors
                .Include(d => d.Department)
                .Include(d => d.Specialization)
                .ToListAsync();

            return View(doctors);
        }

        // ==========================================
        // 2. صفحة إضافة دكتور جديد (GET: Doctors/Create)
        // ==========================================
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "id", "name");
            ViewBag.Specializations = new SelectList(_context.Specializations, "id", "name");
            return View();
        }

        // ==========================================
        // 3. حفظ بيانات الدكتور الجديد (POST: Doctors/Create)
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Doctors.Add(doctor);
                _context.SaveChanges();

                // يفضل بعد الحفظ نرجعه لصفحة العرض (Index) عشان يشوف الدكتور اللي ضافه
                // لو عايزه يفضل في صفحة الإضافة زي ما إنت كاتب، غيرها لـ "Create"
                return RedirectToAction("Index");
            }

            // لو فيه مشكلة في البيانات المدخلة، بنرجع نجهز القوائم ونعرض الفورم تاني
            ViewBag.Departments = new SelectList(_context.Departments, "id", "name", doctor.department_id);
            ViewBag.Specializations = new SelectList(_context.Specializations, "id", "name", doctor.specialization_id);

            return View(doctor);
        }
    }
}