using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
{
    public class ProductOrder
    {
  
        [Display (Name = "Mã sản phẩm")]
        public String ProductId { get; set; }

        [Display (Name = "Mã hóa đơn")]
        public String OrderId { get; set; }

        [Required]
        [Display (Name = "Số lượng")]
        public int Quantity { get; set; }

        public ICollection<Orders>? Orders { get; set; }
    }
}
