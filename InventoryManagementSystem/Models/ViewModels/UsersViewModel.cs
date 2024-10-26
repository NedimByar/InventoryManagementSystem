using InventoryManagementSystem.Models.FilterModels;

namespace InventoryManagementSystem.Models.ViewModels
{
    public class UsersViewModel
    {
        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public ProductFilterModel Filter { get; set; } = new ProductFilterModel();
    }
}
