using InventoryManagementSystem.Models.FilterModels;

namespace InventoryManagementSystem.Models.ViewModels
{
    public class ProductsViewModel
    {
        public List<Products> Products { get; set; }
        public ProductFilterModel Filter { get; set; }
    }
}
