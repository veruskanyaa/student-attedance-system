using Microsoft.EntityFrameworkCore;
using AttendanceSystem.Models;
using System.Security.Cryptography;
using System.Text;

namespace AttendanceSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<ClassSchedule> ClassSchedules { get; set; }
        public DbSet<AttendanceLog> AttendanceLogs { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.SchoolId)
                .IsUnique();

            modelBuilder.Entity<AdminUser>().HasData(new AdminUser
            {
                Id = 1,
                Username = "admin",
                PasswordHash = HashPassword("admin123"),
                DisplayName = "Administrator"
            });
        }

        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
