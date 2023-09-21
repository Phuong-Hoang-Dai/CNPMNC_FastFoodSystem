using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AgentManager.WebApp.Models.Data
{
    public class AgentCategory
    {
        [Display(Name = "Nã loại")]
        public int AgentCategoryId { get; set; }
        [Display(Name = "Nợ tối đa")]
        [Required]
        public int MaxDebt { get; set; }
        [Display(Name = "Danh sách đại lý")]
        public ICollection<Agent>? Agents { get; set; }
    }
}
