using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartCarePatientPortal.Models;

namespace SmartCarePatientPortal.Controllers
{
    public class LabTestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LabTestController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: LabTest
        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;
            ViewBag.UserRole = user?.Role.ToString();
            var labTests = _context.LabTests.ToList();
            return View(labTests);
        }

        // GET: LabTest/MyLabTests (Patient: view their own lab tests)
        public IActionResult MyLabTests()
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user == null || user.Role.ToString() != "Patient")
                return RedirectToAction("AccessDenied", "Account");

            var patientId = _context.Patients.FirstOrDefault(p => p.UserId == user.Id)?.PatientId;
            if (patientId == null)
                return NotFound();

            var labTests = _context.LabTests
                .Where(l => l.PatientId == patientId)
                .OrderByDescending(l => l.OrderedDate)
                .ToList();

            return View(labTests);
        }

        // POST: LabTest/Accept
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Accept(int id)
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user == null || user.Role.ToString() != "Patient")
                return RedirectToAction("AccessDenied", "Account");

            var labTest = _context.LabTests.Find(id);
            if (labTest != null && labTest.Status == LabTestStatus.Pending)
            {
                labTest.Status = LabTestStatus.InProgress;
                labTest.AcceptedByPatient = DateTime.Now;
                _context.Update(labTest);
                _context.SaveChanges();
            }
            return RedirectToAction("MyLabTests");
        }

        // GET: LabTest/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var labTest = _context.LabTests.FirstOrDefault(m => m.LabTestId == id);
            if (labTest == null)
                return NotFound();

            return View(labTest);
        }

        // GET: LabTest/Create (Doctor Only, manual check)
        public IActionResult Create()
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user == null || user.Role.ToString() != "Doctor")
                return RedirectToAction("AccessDenied", "Account");

            ViewBag.PatientId = new SelectList(_context.Patients, "PatientId", "PatientNumber");
            ViewBag.DoctorId = new SelectList(_context.Doctors, "DoctorId", "DoctorNumber");
            return View();
        }

        // POST: LabTest/Create (Doctor Only, manual check)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LabTest labTest)
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user == null || user.Role.ToString() != "Doctor")
                return RedirectToAction("AccessDenied", "Account");

            if (ModelState.IsValid)
            {
                labTest.Status = LabTestStatus.Pending; // Always Pending when created
                labTest.OrderedDate = DateTime.Now;
                _context.LabTests.Add(labTest);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate dropdowns if validation fails
            ViewBag.PatientId = new SelectList(_context.Patients, "PatientId", "PatientNumber", labTest.PatientId);
            ViewBag.DoctorId = new SelectList(_context.Doctors, "DoctorId", "DoctorNumber", labTest.DoctorId);

            return View(labTest);
        }

        // GET: LabTest/Complete/5 (Admin Only)
        public IActionResult Complete(int? id)
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user == null || user.Role.ToString() != "Admin")
                return RedirectToAction("AccessDenied", "Account");

            if (id == null)
                return NotFound();

            var labTest = _context.LabTests.Find(id);
            if (labTest == null || labTest.Status != LabTestStatus.InProgress)
                return NotFound();

            return View(labTest);
        }

        // POST: LabTest/Complete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Complete(int id, IFormFile resultFile)
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user == null || user.Role.ToString() != "Admin")
                return RedirectToAction("AccessDenied", "Account");

            var labTest = _context.LabTests.Find(id);
            if (labTest != null && labTest.Status == LabTestStatus.InProgress)
            {
                if (resultFile != null && resultFile.Length > 0)
                {
                    // Save file to /wwwroot/LabResults/
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "LabResults");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var fileName = $"LabResult_{labTest.LabTestId}_{DateTime.Now:yyyyMMddHHmmss}{Path.GetExtension(resultFile.FileName)}";
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        resultFile.CopyTo(stream);
                    }

                    labTest.ResultFilePath = $"/LabResults/{fileName}";
                }

                labTest.Status = LabTestStatus.Completed;
                labTest.CompletedByAdmin = DateTime.Now;
                _context.Update(labTest);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: LabTest/Edit/5 (Doctor/Admin only, manual check)
        public IActionResult Edit(int? id)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var role = user?.Role.ToString();
            if (user == null || (role != "Doctor" && role != "Admin"))
                return RedirectToAction("AccessDenied", "Account");

            if (id == null)
                return NotFound();

            var labTest = _context.LabTests.Find(id);
            if (labTest == null)
                return NotFound();

            return View(labTest);
        }

        // POST: LabTest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, LabTest labTest)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var role = user?.Role.ToString();
            if (user == null || (role != "Doctor" && role != "Admin"))
                return RedirectToAction("AccessDenied", "Account");

            if (id != labTest.LabTestId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(labTest);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(labTest);
        }

        // GET: LabTest/Delete/5 (Doctor/Admin only, manual check)
        public IActionResult Delete(int? id)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var role = user?.Role.ToString();
            if (user == null || (role != "Doctor" && role != "Admin"))
                return RedirectToAction("AccessDenied", "Account");

            if (id == null)
                return NotFound();

            var labTest = _context.LabTests.FirstOrDefault(m => m.LabTestId == id);
            if (labTest == null)
                return NotFound();

            return View(labTest);
        }

        // POST: LabTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var role = user?.Role.ToString();
            if (user == null || (role != "Doctor" && role != "Admin"))
                return RedirectToAction("AccessDenied", "Account");

            var labTest = _context.LabTests.Find(id);
            if (labTest != null)
            {
                _context.LabTests.Remove(labTest);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
