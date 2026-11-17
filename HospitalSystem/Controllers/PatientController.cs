using HospitalManagementSystem.Models;
using HospitalSystem.Reposetories;

using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.Controllers
{
    public class PatientController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        // حقن الـ UnitOfWork
        public PatientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(string searchString, int page = 1)
        {
            int pageSize = 5;


            var allPatients = await _unitOfWork.Patients.GetListAsync();
            var patientsQuery = allPatients.AsQueryable();


            if (!string.IsNullOrEmpty(searchString))
            {
                patientsQuery = patientsQuery.Where(p => p.name.Contains(searchString));
            }


            int totalCount = patientsQuery.Count();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);


            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages > 0 ? totalPages : 1;
            ViewData["CurrentFilter"] = searchString;


            var patients = patientsQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return View(patients);
        }






        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                // إدراج المريض الجديد باستخدام الـ Repository والـ UnitOfWork
                await _unitOfWork.Patients.AddAsync(patient);
                await _unitOfWork.CommitAsync(); // لحفظ التغييرات في قاعدة البيانات

                return RedirectToAction(nameof(Index));
            }

            return View(patient);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var patient = await _unitOfWork.Patients.GetOneAsync(p => p.id == id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Patient patient)
        {
            if (id != patient.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Patients.Update(patient);
                await _unitOfWork.CommitAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(patient);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _unitOfWork.Patients.GetOneAsync(p => p.id == id);
            if (patient == null)
            {
                return NotFound();
            }
            _unitOfWork.Patients.Remove(patient);
            await _unitOfWork.CommitAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}