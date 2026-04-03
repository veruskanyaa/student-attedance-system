using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceSystem.Models
{
    public enum AttendanceStatus
    {
        OnTime,
        Late
    }

    public class AttendanceLog
    {
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student? Student { get; set; }

        public int? ScheduleId { get; set; }

        [ForeignKey("ScheduleId")]
        public ClassSchedule? Schedule { get; set; }

        [Required]
        public DateTime CheckInTime { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public AttendanceStatus Status { get; set; }

        [StringLength(200)]
        public string? Notes { get; set; }
    }
}
