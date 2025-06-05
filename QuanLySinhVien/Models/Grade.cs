using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySinhVien.Models
{
    [Table("NganhHoc")]
    public class Grade
    {
        [Key]
        public string MaNganh { get; set; }
        public string TenNganh { get; set; }
    }
}
