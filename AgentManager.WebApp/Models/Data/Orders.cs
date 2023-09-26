using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
{
    public class Orders
    {
      
        [Display (Name = "Mã hóa đơn")]
        public String OrderId {  get; set; }
        [Required]
        [Display (Name = "Thời gian mua hàng")]
        public DateTime Date { get; set; }
        [Display (Name = "Tổng tiền")]
        public double Cash { get; set; }
        [Required]
        [Display (Name = "Mã nhân viên")]
        public String StaffId { get; set; }
        [Display (Name = "Mã bàn")]
        public String TableId { get; set; }
        [Display (Name = "Mã giảm giá")]
        public String VoucherId { get; set; }

    }
}
