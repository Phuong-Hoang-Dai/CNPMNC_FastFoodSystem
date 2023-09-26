using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
{
    public class Catere
    {
        
        [Display (Name = "Mã nhà cung cấp")]
        public String CatereId { get; set; }
        [Required]
        [Display (Name = "Tên nhà cung cấp")]
        public String Name { get; set; }
        [Required]
        [Display (Name = "Địa chỉ")]
        public String Address { get; set; }
        [Required]
        [Display (Name = "Mã hợp đồng")]
        public String ContractId { get; set; }
        public ICollection<Ingredients> Ingredients { get; set;}
    }
}
