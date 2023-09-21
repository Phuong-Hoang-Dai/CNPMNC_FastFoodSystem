using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
{
    public class ProductCategory
    {
        [Display(Name = "Mã loại sản phẩm")]
        public int ProductCategoryId { get; set; }
        [Required]
        [Display(Name = "Tên loại sản phẩm")]
        public string? ProductCategoryName { get; set; }
        [Display(Name = "Danh sách sản phẩm")]
        public ICollection<Product>? Products { get; set; }
    }
}
