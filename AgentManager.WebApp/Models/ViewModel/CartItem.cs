using Humanizer;
using System.Net.NetworkInformation;

namespace FastFoodSystem.WebApp.Models.ViewModel
{
    public class CartItem
    {
        public string FFSProductId { get; set; } // ID sản phẩm
        public string tenSanPham { get; set; } // Tên sản phẩm
        public string anh { get; set; } // Đường dẫn ảnh
        public decimal gia { get; set; } // Giá sản phẩm
        public int Quantity { get; set; } // Số lượng sản phẩm
        public decimal total
        {
            get
            {
                if (gia != null && Quantity != null) return gia * Quantity;
                else return 0;
            }
        }
    }
}
