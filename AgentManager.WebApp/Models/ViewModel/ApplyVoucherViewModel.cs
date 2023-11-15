using System.ComponentModel.DataAnnotations;

namespace FastFoodSystem.WebApp.Models.ViewModel
{
    public class ApplyVoucherViewModel
    {
        [Required(ErrorMessage = "Mã khuyến mãi không được bỏ trống.")]
        public string PromoCode { get; set; }
    }
}
