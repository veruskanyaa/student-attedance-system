using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.Data;
using AttendanceSystem.Models;
using AttendanceSystem.Models.ViewModels;
using System.Text;

namespace AttendanceSystem.Controllers
{
    [Authorize]
    public class HistoryController : Controller
    {
        private readonly AppDbContext _db;

        public HistoryController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(string? status, string? date)
        {
            var query = _db.AttendanceLogs
                .Include(a => a.Student)
                .Include(a => a.Schedule)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status) && status != "All")
            {
                if (Enum.TryParse<AttendanceStatus>(status, out var statusEnum))
                {
                    query = query.Where(a => a.Status == statusEnum);
                }
            }

            if (!string.IsNullOrWhiteSpace(date) && DateOnly.TryParse(date, out var filterDate))
            {
                query = query.Where(a => a.Date == filterDate);
            }

            var logs = await query
                .OrderByDescending(a => a.CheckInTime)
                .ToListAsync();

            var model = new HistoryViewModel
            {
                Logs = logs,
                TotalEntries = logs.Count,
                OnTimeCount = logs.Count(l => l.Status == AttendanceStatus.OnTime),
                LateCount = logs.Count(l => l.Status == AttendanceStatus.Late),
                FilterStatus = status,
                FilterDate = date
            };

            return View(model);
        }

        public async Task<IActionResult> Export()
        {
            var logs = await _db.AttendanceLogs
                .Include(a => a.Student)
                .Include(a => a.Schedule)
                .OrderByDescending(a => a.CheckInTime)
                .ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine("Student Name,School ID,Action,Subject,Date,Time,Status,Notes");

            foreach (var log in logs)
            {
                sb.AppendLine($"\"{log.Student?.FullName}\",\"{log.Student?.SchoolId}\",\"Check In\",\"{log.Schedule?.SubjectName ?? "General"}\",\"{log.Date}\",\"{log.CheckInTime:hh:mm tt}\",\"{log.Status}\",\"{log.Notes}\"");
            }

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", $"attendance_logs_{DateTime.Now:yyyyMMdd}.csv");
        }
    }
}
