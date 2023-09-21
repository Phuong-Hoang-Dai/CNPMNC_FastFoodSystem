using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
{
    public class Department
    {
        [Display(Name ="Mã phòng ban")]
        public int DepartmentId { get; set; }
        [Display(Name ="Tên phòng ban")]
        [Required]
        public string? DepartmentName { get; set; }
        [Display(Name ="Danh sách nhân viên")]
        public ICollection<Staff>? Staffs { get; set; }
    }
}
