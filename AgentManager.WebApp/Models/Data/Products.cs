using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
{
    public class Products
    {
        [Display(Name = "Mã sản phẩm")]
        public String ProductId { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        public string Desc { get; set; }

        [Display(Name = "Đơn giá")]
        [Required]
        public int Price { get; set; }

        [Display(Name = "Loại sản phẩm")]
        public String ProductCategoryId { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Image {  get; set; }

        //public ICollection<ProductCategories>? ProductCategories { get; set; }
    }
}
