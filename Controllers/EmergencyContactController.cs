using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCarePatientPortal.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCarePatientPortal.Controllers
{
    [Authorize]
    public class EmergencyContactController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmergencyContactController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: EmergencyContact list for the logged-in patient
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var patient = await _context.Patients
                .Include(p => p.EmergencyContacts)
                .FirstOrDefaultAsync(p => p.UserId == user.Id);

            return View(patient.EmergencyContacts.ToList());
        }

        // GET: Create EmergencyContact
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create EmergencyContact
        [HttpPost]
        [ValidateAntiForgeryToken] // ✅ Added CSRF protection
        public async Task<IActionResult> Create(EmergencyContact contact)
        {
            var user = await _userManager.GetUserAsync(User);
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == user.Id);

            if (ModelState.IsValid && patient != null)
            {
                contact.PatientId = patient.PatientId;
                _context.EmergencyContacts.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: Edit EmergencyContact
        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _context.EmergencyContacts.FindAsync(id);
            if (contact == null) return NotFound();
            return View(contact);
        }

        // POST: Edit EmergencyContact
        [HttpPost]
        [ValidateAntiForgeryToken] // ✅ Added CSRF protection
        public async Task<IActionResult> Edit(EmergencyContact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Update(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Delete EmergencyContact
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _context.EmergencyContacts.FindAsync(id);
            if (contact == null) return NotFound();

            _context.EmergencyContacts.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

