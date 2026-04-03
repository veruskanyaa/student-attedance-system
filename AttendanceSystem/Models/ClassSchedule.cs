using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models
{
    public class ClassSchedule
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string SubjectName { get; set; } = string.Empty;

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// Minutes after StartTime that a student can still be considered "On Time"
        /// </summary>
        public int GracePeriodMinutes { get; set; } = 15;

        public ICollection<AttendanceLog> AttendanceLogs { get; set; } = new List<AttendanceLog>();
    }
}
