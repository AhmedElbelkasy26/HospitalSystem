
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HospitalManagementSystem.Models;
using HospitalSystem.Reposetories;

namespace HospitalSystem.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ==========================================
        // 1. INDEX - Display All Appointments
        // ==========================================

        public async Task<IActionResult> Index()
        {
            var appointments = await _unitOfWork.Appointments.GetListAsync(
                includes: new System.Linq.Expressions.Expression<Func<Appointment, object>>[]
                {
                    a => a.Doctor,
                    a => a.Patient,
                    a => a.Doctor.Department
                },
                isTracked: false
            );

            var result = appointments
                .OrderBy(a => a.date)
                .ThenBy(a => a.time);

            return View(result);
        }

        // ==========================================
        // 2. DETAILS - Display Appointment Details
        // ==========================================

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var appointment = await _unitOfWork.Appointments.GetOneAsync(
                filter: a => a.id == id,
                includes: new System.Linq.Expressions.Expression<Func<Appointment, object>>[]
                {
                    a => a.Doctor,
                    a => a.Patient,
                    a => a.Doctor.Department
                },
                isTracked: false
            );

            if (appointment == null)
                return NotFound();

            return View(appointment);
        }

        // ==========================================
        // 3. CREATE - GET
        // Display Create Appointment Form
        // ==========================================

        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();

            return View();
        }

        // ==========================================
        // 4. CREATE - POST
        // Save New Appointment
        // ==========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Appointment appointment, int departmentId)
        {
            ModelState.Remove("Doctor");
            ModelState.Remove("Patient");
            ModelState.Remove("Prescriptions");

            await ValidateReferencesAsync(appointment, departmentId);

            if (ModelState.IsValid)
            {
                await _unitOfWork.Appointments.AddAsync(appointment);

                int result = await _unitOfWork.CommitAsync();

                if (result > 0)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "Failed to save appointment.");
            }

            await LoadDropdownsAsync(
                departmentId,
                appointment.doctor_id,
                appointment.patient_id
            );

            return View(appointment);
        }

        // ==========================================
        // 5. EDIT - GET
        // Display Existing Appointment Data
        // ==========================================

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var appointment = await _unitOfWork.Appointments.GetOneAsync(
                filter: a => a.id == id,
                includes: new System.Linq.Expressions.Expression<Func<Appointment, object>>[]
                {
                    a => a.Doctor,
                    a => a.Patient
                }
            );

            if (appointment == null)
                return NotFound();

            await LoadDropdownsAsync(
                appointment.Doctor?.department_id,
                appointment.doctor_id,
                appointment.patient_id
            );

            return View(appointment);
        }

        // ==========================================
        // 6. EDIT - POST
        // Update Existing Appointment
        // ==========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, Appointment appointment, int departmentId)
        {
            if (id != appointment.id)
                return NotFound();

            ModelState.Remove("Doctor");
            ModelState.Remove("Patient");
            ModelState.Remove("Prescriptions");

            await ValidateReferencesAsync(appointment, departmentId);

            if (ModelState.IsValid)
            {
                _unitOfWork.Appointments.Update(appointment);

                int result = await _unitOfWork.CommitAsync();

                if (result > 0)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "Failed to update appointment.");
            }

            await LoadDropdownsAsync(
                departmentId,
                appointment.doctor_id,
                appointment.patient_id
            );

            return View(appointment);
        }

        // ==========================================
        // 7. DELETE - GET
        // Display Delete Confirmation
        // ==========================================

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var appointment = await _unitOfWork.Appointments.GetOneAsync(
                filter: a => a.id == id,
                includes: new System.Linq.Expressions.Expression<Func<Appointment, object>>[]
                {
                    a => a.Doctor,
                    a => a.Patient,
                    a => a.Doctor.Department
                },
                isTracked: false
            );

            if (appointment == null)
                return NotFound();

            return View(appointment);
        }

        // ==========================================
        // 8. DELETE - POST
        // Remove Appointment
        // ==========================================

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _unitOfWork.Appointments.GetOneAsync(
                filter: a => a.id == id
            );

            if (appointment == null)
                return NotFound();

            _unitOfWork.Appointments.Remove(appointment);

            int result = await _unitOfWork.CommitAsync();

            if (result > 0)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Failed to delete appointment.");

            return RedirectToAction(nameof(Delete), new { id });
        }

        // ==========================================
        // HELPER METHODS
        // ==========================================

        // Load Dropdown Lists for Create and Edit

        private async Task LoadDropdownsAsync(
            int? selectedDepartmentId = null,
            int? selectedDoctorId = null,
            int? selectedPatientId = null)
        {
            var departments = await _unitOfWork.Departments.GetListAsync(
                isTracked: false
            );

            var doctors = await _unitOfWork.Doctors.GetListAsync(
                isTracked: false
            );

            var patients = await _unitOfWork.Patients.GetListAsync(
                isTracked: false
            );

            ViewBag.Departments = new SelectList(
                departments,
                "id",
                "name",
                selectedDepartmentId
            );

            ViewBag.Doctors = new SelectList(
                doctors,
                "id",
                "name",
                selectedDoctorId
            );

            ViewBag.Patients = new SelectList(
                patients,
                "id",
                "name",
                selectedPatientId
            );
        }

        // Validate Department, Doctor and Patient

        private async Task ValidateReferencesAsync(
            Appointment appointment, int departmentId)
        {
            var department = await _unitOfWork.Departments.GetOneAsync(
                d => d.id == departmentId,
                isTracked: false
            );

            var doctor = await _unitOfWork.Doctors.GetOneAsync(
                d => d.id == appointment.doctor_id,
                isTracked: false
            );

            var patient = await _unitOfWork.Patients.GetOneAsync(
                p => p.id == appointment.patient_id,
                isTracked: false
            );

            if (department == null)
            {
                ModelState.AddModelError(
                    "departmentId", "Please select a valid department."
                );
            }

            if (doctor == null)
            {
                ModelState.AddModelError(
                    "doctor_id", "Please select a valid doctor."
                );
            }
            else if (doctor.department_id != departmentId)
            {
                ModelState.AddModelError(
                    "doctor_id",
                    "The selected doctor does not belong to this department."
                );
            }

            if (patient == null)
            {
                ModelState.AddModelError(
                    "patient_id", "Please select a valid patient."
                );
            }
        }
    }
}
