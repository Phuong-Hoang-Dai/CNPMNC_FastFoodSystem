using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
{
    public class ProductCategories
    {
        [Display(Name = "Mã loại sản phẩm")]
        public String ProductCategoryId { get; set; }
        [Required]
        [Display(Name = "Tên loại sản phẩm")]
        public string? Name { get; set; }
        [Display(Name = "Danh sách sản phẩm")]
       // public ICollection<Product>? Products { get; set; }
    }
}
