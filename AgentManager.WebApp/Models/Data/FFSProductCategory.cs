using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FastFoodSystem.WebApp.Models.Data
{
    public class FFSProductCategory
    {

        [Display(Name = "Mã loại sản phẩm")]
        [Required]
        public string FFSProductCategoryId { get; set; }

        [Display(Name = "Tên loại sản phẩm")]
        [Required]
		public string? Name { get; set; }

        [Display(Name = "Danh sách sản phẩm")]
        [JsonIgnore]
        public ICollection<FFSProduct>? FFSProducts { get; set; }
    }
}
