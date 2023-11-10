using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FastFoodSystem.WebApp.Models.Data
{
    public class FFSOrder
    {
      
        [Display (Name = "Mã hóa đơn")]
        public int FFSOrderId {  get; set; }

        [Required]
        [Display (Name = "Thời gian mua hàng")]
        public DateTime Date { get; set; }

        [Display (Name = "Tổng tiền")]
        public double Cash { get; set; }

        [Required]
        [Display (Name = "Mã nhân viên")]
        public string StaffId { get; set; }
        public Staff Staff { get; set; }

        [Display (Name = "Mã bàn")]
        public string TableId { get; set; }

        [Display (Name = "Mã giảm giá")]
        public string FFSVoucherId { get; set; }
        public FFSVoucher? FFSVoucher { get; set; }
        [JsonIgnore]
        public ICollection<FFSProductOrder>? FFSProductOrders { get; set; }


	}
}
