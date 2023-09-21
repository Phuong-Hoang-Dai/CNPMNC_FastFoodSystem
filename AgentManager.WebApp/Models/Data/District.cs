using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
{
    public class District
    {
        [Display(Name ="Mã quận")]
        public int DistrictID { get; set; }
        [Display(Name ="Tên quận")]
        [Required]
        public string? DistrictName { get; set; }
        [Display(Name ="Danh sách đại lý")]
        public ICollection<Agent>? Agents { get; set; }
    }
}
