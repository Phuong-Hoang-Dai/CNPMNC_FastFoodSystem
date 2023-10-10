using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace FastFoodSystem.WebApp.Models.ViewModel
{
    public class AddRoleVM
    {
        [DisplayName("Danh sách các role")]
        public string[] roles { get; set; }
        public SelectList allRoles { get; set; }
    }
}
