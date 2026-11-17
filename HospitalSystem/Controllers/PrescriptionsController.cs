using HospitalManagementSystem.Models;
using HospitalSystem.Reposetories;
using HospitalSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.Controllers
{
    // In this database every Prescription row is ONE medicine.
    // The page groups the rows that share the same patient + doctor + date,
    // so /Prescriptions/Details/{id} shows that row and all its "sibling" medicines.
    public class PrescriptionsController(IUnitOfWork unitOfWork) : Controller
    {
        // GET /Prescriptions/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var header = await GetHeaderAsync(id);
            if (header is null) return NotFound();

            var items = await GetItemsAsync(header);

            return View(new PrescriptionDetailsVM { Header = header, Items = items });
        }

        // GET /Prescriptions/AddMedicine/5
        public async Task<IActionResult> AddMedicine(int id)
        {
            var header = await GetHeaderAsync(id);
            if (header is null) return NotFound();

            return View(new AddMedicineVM { PrescriptionId = id, PatientName = header.Patient.name });
        }

        // POST /Prescriptions/AddMedicine/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMedicine(int id, AddMedicineVM vm)
        {
            var header = await GetHeaderAsync(id);
            if (header is null) return NotFound();

            if (!ModelState.IsValid)
            {
                vm.PrescriptionId = id;
                vm.PatientName = header.Patient.name;
                return View(vm);
            }

            // New row = same patient, doctor and date as the prescription being viewed
            await unitOfWork.Prescriptions.AddAsync(new Prescription
            {
                medicine = vm.Medicine,
                dose = vm.Dose,
                duration = vm.Duration,
                date = header.date,
                patient_id = header.patient_id,
                doctor_id = header.doctor_id
            });
            await unitOfWork.CommitAsync();

            return RedirectToAction(nameof(Details), new { id });
        }

        // POST /Prescriptions/RemoveMedicine/5?itemId=7
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveMedicine(int id, int itemId)
        {
            var header = await GetHeaderAsync(id);
            var item = await unitOfWork.Prescriptions.GetOneAsync(p => p.id == itemId);
            if (header is null || item is null) return NotFound();

            unitOfWork.Prescriptions.Remove(item);
            await unitOfWork.CommitAsync();

            // If the removed row was the one in the URL, show one of the remaining rows instead
            if (itemId == id)
            {
                var next = (await GetItemsAsync(header)).FirstOrDefault();
                if (next is null) return RedirectToAction("Index", "Home");
                return RedirectToAction(nameof(Details), new { id = next.id });
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        // ---------- helpers ----------

        private Task<Prescription> GetHeaderAsync(int id) =>
            unitOfWork.Prescriptions.GetOneAsync(
                p => p.id == id,
                includes: [p => p.Patient, p => p.Doctor],
                isTracked: false);

        private async Task<List<Prescription>> GetItemsAsync(Prescription header)
        {
            var day = header.date.Date;
            var nextDay = day.AddDays(1);

            var items = await unitOfWork.Prescriptions.GetListAsync(
                p => p.patient_id == header.patient_id
                  && p.doctor_id == header.doctor_id
                  && p.date >= day && p.date < nextDay,
                isTracked: false);

            return items.OrderBy(p => p.id).ToList();
        }
    }
}
