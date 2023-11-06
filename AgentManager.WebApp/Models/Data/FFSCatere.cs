using System.ComponentModel.DataAnnotations;

namespace FastFoodSystem.WebApp.Models.Data
{
    public class FFSCatere
    {
        
        [Display (Name = "Mã nhà cung cấp")]
        [Required]  
        public string FFSCatereId { get; set; }
        [Required]
        [Display (Name = "Tên nhà cung cấp")]
        public string? Name { get; set; }
        [Required]
        [Display (Name = "Địa chỉ")]
        public string? Address { get; set; }
        [Required]
        [Display (Name = "Mã hợp đồng")]
        public string? ContractId { get; set; }
		[Required]
		[Display(Name = "Số điện thoại")]
		public string? PhoneNumber { get; set; }
		[Required]
		[Display(Name = "Địa chỉ Email")]
		public string EmailAddress { get; set; }
		public ICollection<FFSIngredient>? FFSIngredients { get; set;}
    }
}
