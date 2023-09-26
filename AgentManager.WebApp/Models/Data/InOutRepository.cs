using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
{
    public class InOutRepository
    {
        
        [Display (Name = "Mã hóa đơn nhập xuất kho")]
        public String InOutRepositoryId { get; set; }
        [Required]
        [Display (Name = "Trạng thái")]
        public String State { get; set; }
        [Required]
        [Display (Name = "Mã nhân viên")]
        public String StaffId  { get; set; }
        [Required]
        [Display (Name = "Mã lô hàng")]
        public String ShipmentId { get; set; }

        
    }
}
