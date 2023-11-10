
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FastFoodSystem.WebApp.Models.Data
{
    public class FFSProductOrder
    {
  
        [Display (Name = "Mã sản phẩm")]
        [Required]
        public string FFSProductId { get; set; }
        public FFSProduct FFSProduct { get; set; }

        [Required]
        [Display (Name = "Mã hóa đơn")]
        public int FFSOrderId { get; set; }
        public FFSOrder FFSOrder { get; set; }

        [Required]
        [Display (Name = "Số lượng")]
        public int Quantity { get; set; }
       
    }
}
