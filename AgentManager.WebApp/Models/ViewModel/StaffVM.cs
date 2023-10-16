using FastFoodSystem.WebApp.Models.Data;
using System.ComponentModel.DataAnnotations;

namespace FastFoodSystem.WebApp.Models.ViewModel
{
    public class StaffVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType (DataType.Text)]
        [Display(Name = "Tên nhân viên")]
        public string? StaffName { get; set; }
        [Display(Name = "Giới tính")]
        [Required]
        [DataType (DataType.Text)]
        public string? Gender { get; set; }
        [Display(Name = "Ngày sinh")]
        [Required]
        public DateTime DoB { get; set; }
        [Display(Name = "Địa chỉ")]
        [Required]
        public string? Address { get; set; }
       
        [Display(Name = "Mã chức vụ")]
        public int PositionId { get; set; }
        [Display(Name = "Chức vụ")]
        public Position? Position { get; set; }
    }
}
