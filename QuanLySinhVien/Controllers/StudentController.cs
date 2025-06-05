using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Models;

public class StudentController : Controller
{
    private readonly ApplicationDbContext _context;

    public StudentController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Student
    public IActionResult Index()
    {
        var students = _context.SinhVien.ToList();
        return View(students);
    }

    // GET: Student/Create
    public IActionResult Create()
    {
        ViewBag.NganhList = new SelectList(_context.Grade.ToList(), "MaNganh", "TenNganh");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student student, IFormFile HinhFile)
    {
        //if (ModelState.IsValid)
        {
            if (HinhFile != null && HinhFile.Length > 0)
            {
                var fileName = Path.GetFileName(HinhFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await HinhFile.CopyToAsync(stream);
                }

                student.Hinh = "/images/" + fileName;
            }

            _context.SinhVien.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.NganhList = new SelectList(_context.Grade.ToList(), "MaNganh", "TenNganh");
        return View(student);
    }

    public IActionResult Edit(string id)
    {
        var student = _context.SinhVien.FirstOrDefault(s => s.MaSV == id);
        if (student == null) return NotFound();

        ViewBag.NganhList = new SelectList(_context.Grade.ToList(), "MaNganh", "TenNganh", student.MaNganh);
        return View(student);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, Student sinhVien, IFormFile upload)
    {
        if (id != sinhVien.MaSV)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Nếu có ảnh mới thì cập nhật
                if (upload != null && upload.Length > 0)
                {
                    var fileName = Path.GetFileName(upload.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await upload.CopyToAsync(stream);
                    }

                    sinhVien.Hinh = "/images/" + fileName; // Cập nhật đường dẫn ảnh
                }

                _context.Update(sinhVien);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SinhVienExists(sinhVien.MaSV))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        return View(sinhVien);
    }


    private bool SinhVienExists(string id)
    {
        return _context.SinhVien.Any(e => e.MaSV == id);
    }


    // GET: SinhVien/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var sinhVien = await _context.SinhVien
            .FirstOrDefaultAsync(m => m.MaSV == id);

        if (sinhVien == null)
        {
            return NotFound();
        }

        return View(sinhVien);
    }

    // POST: SinhVien/DeleteConfirmed/5
    [HttpPost, ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Student student)
    {
        var sinhVien = await _context.SinhVien.FindAsync(student.MaSV);
        if (sinhVien != null)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", Path.GetFileName(sinhVien.Hinh ?? ""));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _context.SinhVien.Remove(sinhVien);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
    public IActionResult Detail(string id)
    {
        var student = _context.SinhVien.FirstOrDefault(s => s.MaSV == id);
        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

}
