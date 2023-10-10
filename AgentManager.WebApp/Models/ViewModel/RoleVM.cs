using System.ComponentModel.DataAnnotations;

namespace FastFoodSystem.WebApp.Models.ViewModel
{
    public class RoleVM
    {
        [Display(Name = "Tên nhân viên")]
        public string? RoleName { get; set; }
    }
}
