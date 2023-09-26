using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
{
    public class Voucher
    {
        [Required]
        [Display (Name = "Mã giảm giá")]
        public String VoucherId { get; set; }
        [Required]
        [Display(Name = "Số lượng")]
        public int Num {  get; set; }
        [Required]
        [Display(Name = "Ngày bắt đầu")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "Ngày kết thúc")]
        public DateTime EndDate { get; set; }
        [Required]
        [Display(Name = "Loại m")]
        public String State { get; set; }
        [Display(Name = "")]
        public Double Price { get; set; }

        public ICollection<Orders>? Orders { get; set; }
    }
}
