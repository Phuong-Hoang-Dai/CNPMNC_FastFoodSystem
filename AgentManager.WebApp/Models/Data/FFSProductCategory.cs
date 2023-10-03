using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
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
        public ICollection<FFSProduct>? FFSProducts { get; set; }
    }
}
