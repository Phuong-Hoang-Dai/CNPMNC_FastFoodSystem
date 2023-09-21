using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
{
    public class DeliveryNote
    {
        [Display(Name ="Mã đại lý")]
        public int DeliveryNoteId { get; set; }
        [Display(Name ="Ngày tạo phiếu")]
        public DateTime CreatedDate { get; set; }
        [Display(Name ="Tổng tiền")]
        public int TotalPrice { get; set; }
        [Display(Name ="Tiền trả")]
        public int Payment { get; set; }
        [Display(Name ="Danh sách mua")]
        public ICollection<DeliveryNoteDetail>? DeliveryNoteDetails { get; set; }
        [Display(Name ="Mã đại lý")]
        [Required]
        public int AgentId { get; set; }
        public Agent? Agent { get; set; }
        [Display(Name ="Mã nhân viên")]
        [Required]
        public string? StaffId { get; set; }
        public Staff? Staff { get; set; }
    }
}
