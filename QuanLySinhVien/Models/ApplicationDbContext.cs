using Microsoft.EntityFrameworkCore;

namespace QuanLySinhVien.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Student> SinhVien { get; set; }
        public DbSet<Grade> Grade { get; set; }
    }
}
