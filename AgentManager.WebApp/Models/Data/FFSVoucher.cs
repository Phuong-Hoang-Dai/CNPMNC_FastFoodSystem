using System.ComponentModel.DataAnnotations;

namespace FastFoodSystem.WebApp.Models.Data
{
    public class FFSVoucher
    {
        [Required]
        [Display (Name = "Mã giảm giá")]
        public string FFSVoucherId { get; set; }
        [Required]
        [Display(Name = "Giới hạn mức")]
        public int Num {  get; set; }
        [Required]
        [Display(Name = "Ngày bắt đầu")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "Ngày kết thúc")]
        public DateTime EndDate { get; set; }
        [Required]
        [Display(Name = "Đơn vị")]
        public string State { get; set; }
        [Display(Name = "Giảm")]
        public double Price { get; set; }

        public ICollection<FFSOrder>? FFSOrders { get; set; }
    }
}
