using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
{
    public class VoucherProduct
    {
        [Required]
        [Display (Name = "Mã sản phẩm")]
        public String ProductId {  get; set; }
        [Required]
        [Display (Name = "Phần trăm")]
        public int Persent { get; set; }

        public ICollection<Voucher> Vouchers { get; set; }
    }
}
