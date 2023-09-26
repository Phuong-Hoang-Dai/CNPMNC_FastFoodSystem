using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
{
    public class Ingredients
    {
        
        [Display (Name = "Mã nguyên vật liệu")]
        public String IngredientId {  get; set; }
        [Required]
        [Display (Name = "Tên nguyên vật liệu")]
        public String Name { get;}
        public ICollection<Shipments> Shipments { get; set; }
    }
}
