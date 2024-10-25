using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
        void Save();
    }
}