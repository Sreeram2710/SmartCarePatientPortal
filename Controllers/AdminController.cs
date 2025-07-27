using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCarePatientPortal.Models;
using SmartCarePatientPortal.Models.ViewModels;

namespace SmartCarePatientPortal.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user?.Role != UserRole.Admin)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var model = new DashboardViewModel
            {
                UserName = user.FullName,
                UserRole = user.Role,
                TotalPatients = await _context.Patients.CountAsync(),
                TotalDoctors = await _context.Doctors.CountAsync(),
                TotalAppointments = await _context.Appointments.CountAsync(),
                TodayAppointments = await _context.Appointments
                    .CountAsync(a => a.AppointmentDate.Date == DateTime.Today),
                RecentAppointments = await _context.Appointments
                    .Include(a => a.Patient).ThenInclude(p => p.User)
                    .Include(a => a.Doctor).ThenInclude(d => d.User)
                    .OrderByDescending(a => a.CreatedDate)
                    .Take(5)
                    .ToListAsync()
            };

            return View(model);
        }

        public async Task<IActionResult> ManageUsers()
        {
            var users = await _context.Users
                .Include(u => u.Patient)
                .Include(u => u.Doctor)
                .ToListAsync();

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var model = new UpdateUserViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.ToString()
            };

            return View("UpdateUser", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View("UpdateUser", model);

            if (string.IsNullOrEmpty(model.Id))
                return NotFound();

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();

            var names = model.FullName?.Split(' ', 2);
            if (names != null && names.Length > 0) user.FirstName = names[0];
            if (names != null && names.Length > 1) user.LastName = names[1];

            user.Email = model.Email;

            if (!Enum.TryParse<UserRole>(model.Role, out var parsedRole))
            {
                ModelState.AddModelError("Role", "Invalid role selected.");
                return View("UpdateUser", model);
            }

            user.Role = parsedRole;

            await _userManager.UpdateAsync(user);
            return RedirectToAction("ManageUsers");
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var model = new UpdateUserViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.ToString()
            };

            return View("UpdateUser", model); // ✅ FIXED: use UpdateUser view
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UpdateUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View("UpdateUser", model); // ✅ FIXED: use UpdateUser view

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();

            var names = model.FullName?.Split(' ', 2);
            if (names != null && names.Length > 0) user.FirstName = names[0];
            if (names != null && names.Length > 1) user.LastName = names[1];

            user.Email = model.Email;

            if (!Enum.TryParse<UserRole>(model.Role, out var parsedRole))
            {
                ModelState.AddModelError("Role", "Invalid role selected.");
                return View("UpdateUser", model);
            }

            user.Role = parsedRole;

            await _userManager.UpdateAsync(user);
            return RedirectToAction("ManageUsers");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            await _userManager.DeleteAsync(user);
            return RedirectToAction("ManageUsers");
        }

        public async Task<IActionResult> Reports()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Patient).ThenInclude(p => p.User)
                .Include(a => a.Doctor).ThenInclude(d => d.User)
                .ToListAsync();

            return View(appointments);
        }
    }
}
