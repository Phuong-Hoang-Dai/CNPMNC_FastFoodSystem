using System.ComponentModel.DataAnnotations;

namespace FastFoodSystem.WebApp.Models.ViewModel
{
    public class AddDeliveryNoteDetail
    {
        [Display(Name = "Mã phiếu xuất")]
        public int DeliveryNoteId { get; set; }
        [Display(Name = "Mã nhân viên")]
        public int ProductId { get; set; }
        [Display(Name = "Số lượng")]
        [Required]
        public int Quantity { get; set; }
    }
}
