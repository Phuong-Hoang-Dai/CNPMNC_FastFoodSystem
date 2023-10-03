using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
{
    public class FFSShipment
    {
    
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
		[Display(Name = "Mã nguyên vật liệu")]
		public string FFSIngredientId { get; set; }
		public FFSIngredient? FFSIngredient { get; set; }

		[Required]
		[Display(Name = "Mã nguyên vật liệu")]
		public string FFSDeliveryRecievedNoteId { get; set; }
		public FFSDeliveryRecievedNote? FFSDeliveryRecievedNote { get; set; }
	}
}
