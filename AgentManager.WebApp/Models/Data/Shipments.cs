using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
{
    public class Shipments
    {
    
        [Display (Name = "Mã lô hàng")]
        public String ShipmentId { get; set; }
        [Required]
        [Display (Name = "Số lượng")]
        public int Quantity { get; set; }
        [Required]
        [Display (Name = "Hạn sản xuất")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display (Name = "Hạn sử dụng")]
        public DateTime EndDate { get; set; }
        [Required]
        [Display (Name = "Mã nguyên vật liệu")]
        public String IngredientId { get; set; }

        public ICollection<InOutRepository> Ingredients { get; set;}
    }
}
