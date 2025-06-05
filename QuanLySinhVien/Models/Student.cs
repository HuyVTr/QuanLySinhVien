using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien.Models
{
    public class Student
    {
        [Key]
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Hinh { get; set; }     // Tên file ảnh: "nam1.jpg"
        public string MaNganh { get; set; }
    }
}
