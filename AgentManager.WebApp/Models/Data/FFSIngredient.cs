using System.ComponentModel.DataAnnotations;

namespace FastFoodSystem.WebApp.Models.Data
{
    public class FFSIngredient
    {
        
        [Display (Name = "Mã nguyên vật liệu")]
        public string? FFSIngredientId {  get; set; }
        [Required]
        [Display (Name = "Tên nguyên vật liệu")]
		public string Name { get; set; }
        public int Quantity { get; set; }
        [Required]
		public string FFSCatereId { get; set; }

		public FFSCatere? FFSCatere { get; set; }
		public ICollection<FFSShipment>? FFSShipments { get; set; }
    }
}
