using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FastFoodSystem.WebApp.Models.Data
{
    public class Staff : IdentityUser
    {
   
        [Display(Name = "Tên nhân viên")]
        public string? StaffName { get; set; }
        [Display(Name = "Giới tính")]
        public string? Gender { get; set; }
        [Display(Name = "Ngày sinh")]
        public DateTime DoB { get; set; }
        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }
        [Display(Name = "Mã phòng ban")]
        public int DepartmentId { get; set; }
        [Display(Name = "Mã chức vụ")]
        public int PositionId { get; set; }
        [Display(Name = "Chức vụ")]
        public Position? Position { get; set; }
        public ICollection<FFSOrder>? Orders { get; set; }
        public ICollection<FFSDeliveryRecievedNote>? FFSDeliveryRecievedNotes { get; set; }
        
    }
}
