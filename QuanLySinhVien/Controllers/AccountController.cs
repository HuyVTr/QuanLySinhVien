using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Models;

namespace QuanLySinhVien.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        // GET: Login
        public IActionResult Login(string MaSV)
        {
            // Kiểm tra xem _context có null không
            if (_context == null)
            {
                return View("Error");  // Hoặc trả về 500 nếu _context là null
            }

            // Sử dụng đúng tên DbSet: SinhVien
            var student = _context.SinhVien.FirstOrDefault(s => s.MaSV == MaSV);

            if (student != null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["ErrorMessage"] = "Mã sinh viên không tồn tại.";
            return View();
        }

    }
}
