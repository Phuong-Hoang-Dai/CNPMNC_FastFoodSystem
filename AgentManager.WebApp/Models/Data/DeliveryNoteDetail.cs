using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
{
    public class DeliveryNoteDetail
    {
        [Required]
        [Display(Name ="Số lượng")]
        public int Quantity { get; set; }
        [Display(Name ="Thành tiền")]
        public int Price { get; set; }
        [Display(Name ="Mã sản phẩm")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        [Display(Name ="Mã phiếu")]
        public int DeliveryNoteId { get; set; }
        public DeliveryNote? DeliveryNote { get; set; }
    }
}
