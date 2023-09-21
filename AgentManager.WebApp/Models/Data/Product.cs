using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
{
    public class Product
    {
        [Display(Name ="Mã sản phẩm")]
        public int ProductId { get; set; }
        [Display(Name ="Tên sản phẩm")]
        [Required]
        public string? ProductName { get; set; }
        [Display(Name ="Hình ảnh")]
        public string? Image { get; set; }
        [Display(Name ="Đơn giá")]
        [Required]
        public int Price { get; set; }
        [Display(Name ="Trọng lượng")]
        public int ProductWeight { get; set; }
        [Display(Name ="Đơn vị tính")]
        public string ItemUnit { get; set; }
        [Display(Name ="Số lượng hàng tồn kho")]
        public int InventoryQuantity { get; set; }
        [Display(Name = "Mã loại sản phẩm")]
        public int ProductCategoryId { get; set; }
        [Display(Name = "Loại sản phẩm")]
        public ProductCategory? ProductCategory { get; set; }
        [Display(Name = "Danh sách phiếu xuất")]
        public ICollection<DeliveryNoteDetail>? DeliveryNoteDetails { get; set; }
    }
}
