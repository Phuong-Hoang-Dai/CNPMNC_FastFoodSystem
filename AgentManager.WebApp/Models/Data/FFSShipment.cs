using System.ComponentModel.DataAnnotations;

namespace FastFoodSystem.WebApp.Models.Data
{
    public class FFSShipment
    {
    
        [Required]
        [Display (Name = "Số lượng")]
        public int Quantity { get; set; }


        [Display (Name = "Hạn sản xuất")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display (Name = "Hạn sử dụng")]
        public DateTime EndDate { get; set; }

		[Required]
		[Display(Name = "Nguyên vật liệu")]
		public string FFSIngredientId { get; set; }
		public FFSIngredient? FFSIngredient { get; set; }

		[Required]
		[Display(Name = "Mã phiếu nhập/xuất")]
		public string FFSDeliveryRecievedNoteId { get; set; }
		public FFSDeliveryRecievedNote? FFSDeliveryRecievedNote { get; set; }
	}
}
