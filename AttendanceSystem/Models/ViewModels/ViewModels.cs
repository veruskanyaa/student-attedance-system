using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models.ViewModels
{
    public class CheckInViewModel
    {
        [Required(ErrorMessage = "School ID is required")]
        public string SchoolId { get; set; } = string.Empty;
    }

    public class CheckInResultViewModel
    {
        public string StudentName { get; set; } = string.Empty;
        public string SchoolId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public DateTime CheckInTime { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }

    public class DashboardViewModel
    {
        public int TotalStudents { get; set; }
        public int CheckedInToday { get; set; }
        public int OnTimeToday { get; set; }
        public int LateToday { get; set; }
        public int TotalLogsThisWeek { get; set; }
        public List<RecentActivityItem> RecentActivity { get; set; } = new();
    }

    public class RecentActivityItem
    {
        public string StudentName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string TimeAgo { get; set; } = string.Empty;
        public DateTime CheckInTime { get; set; }
    }

    public class CreateStudentViewModel
    {
        [Required(ErrorMessage = "School ID is required")]
        public string SchoolId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; } = string.Empty;

        public string Grade { get; set; } = string.Empty;
    }

    public class CreateScheduleViewModel
    {
        [Required(ErrorMessage = "Subject name is required")]
        public string SubjectName { get; set; } = string.Empty;

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required(ErrorMessage = "Start time is required")]
        public string StartTime { get; set; } = string.Empty;

        [Required(ErrorMessage = "End time is required")]
        public string EndTime { get; set; } = string.Empty;

        public int GracePeriodMinutes { get; set; } = 15;
    }

    public class HistoryViewModel
    {
        public List<AttendanceLog> Logs { get; set; } = new();
        public int TotalEntries { get; set; }
        public int OnTimeCount { get; set; }
        public int LateCount { get; set; }
        public string? FilterStatus { get; set; }
        public string? FilterDate { get; set; }
    }
}
