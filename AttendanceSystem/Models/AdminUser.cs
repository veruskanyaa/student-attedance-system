using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models
{
    public class AdminUser
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [StringLength(100)]
        public string DisplayName { get; set; } = string.Empty;
    }
}
