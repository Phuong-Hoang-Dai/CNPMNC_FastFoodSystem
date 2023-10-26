using System.ComponentModel.DataAnnotations;

namespace FastFoodSystem.WebApp.Models.ViewModel
{
    public class EditRoleVM
    {
        [Display(Name = "Tên quyền")]
        [Required]
        public string? RoleName { get; set; }
        [Display(Name = "Mã quyền")]
        public string? RoleId { get; set; }
    }
}
