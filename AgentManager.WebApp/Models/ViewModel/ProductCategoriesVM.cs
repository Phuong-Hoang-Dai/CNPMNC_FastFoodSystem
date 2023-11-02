
using FastFoodSystem.WebApp.Models.Data;

namespace FastFoodSystem.WebApp.Models.ViewModel
{
    public class ProductCategoryViewModel
    {
        public string SelectedCategoryId { get; set; } //ID cate selected
        public List<FFSProductCategory> Categories { get; set; }
        public List<FFSProduct> Products { get; set; }
    }
}
