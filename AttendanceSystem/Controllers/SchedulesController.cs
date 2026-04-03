using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.Data;
using AttendanceSystem.Models;
using AttendanceSystem.Models.ViewModels;

namespace AttendanceSystem.Controllers
{
    [Authorize]
    public class SchedulesController : Controller
    {
        private readonly AppDbContext _db;

        public SchedulesController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var schedules = await _db.ClassSchedules
                .ToListAsync();

            // Sort client-side because SQLite doesn't support TimeSpan in ORDER BY
            schedules = schedules
                .OrderBy(s => s.DayOfWeek)
                .ThenBy(s => s.StartTime)
                .ToList();

            return View(schedules);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateScheduleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fill in all required fields.";
                return RedirectToAction("Index");
            }

            if (!TimeSpan.TryParse(model.StartTime, out var startTime) ||
                !TimeSpan.TryParse(model.EndTime, out var endTime))
            {
                TempData["Error"] = "Invalid time format.";
                return RedirectToAction("Index");
            }

            var schedule = new ClassSchedule
            {
                SubjectName = model.SubjectName.Trim(),
                DayOfWeek = model.DayOfWeek,
                StartTime = startTime,
                EndTime = endTime,
                GracePeriodMinutes = model.GracePeriodMinutes
            };

            _db.ClassSchedules.Add(schedule);
            await _db.SaveChangesAsync();

            TempData["Success"] = $"Schedule '{schedule.SubjectName}' added successfully.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var schedule = await _db.ClassSchedules.FindAsync(id);
            if (schedule != null)
            {
                _db.ClassSchedules.Remove(schedule);
                await _db.SaveChangesAsync();
                TempData["Success"] = $"Schedule '{schedule.SubjectName}' deleted.";
            }
            return RedirectToAction("Index");
        }
    }
}
