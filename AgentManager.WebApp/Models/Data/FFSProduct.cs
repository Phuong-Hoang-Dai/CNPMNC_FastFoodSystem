using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
{
    public class FFSProduct
    {
       
        [Display(Name = "Mã sản phẩm")]
        [Required]
        public string? FFSProductId { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required]
        public string? Name { get; set; }
            
        [Display(Name = "Mô tả")]
        public string? Desc { get; set; }

        [Display(Name = "Đơn giá")]
        [Required]
        public int Price { get; set; }

        [Display(Name = "Loại sản phẩm")]
        public string? FFSProductCategoryId { get; set; }
		public FFSProductCategory? FFSProductCategory { get; set; }

		[Display(Name = "Hình ảnh")]
        public string? Image {  get; set; }

        public ICollection<FFSProductOrder>? FFSProductOrders { get; set; }
    }
}
