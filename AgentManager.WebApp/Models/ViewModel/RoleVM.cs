using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.ViewModel
{
    public class RoleVM
    {
        [Display(Name = "Tên nhân viên")]
        public string? RoleName { get; set; }
    }
}
