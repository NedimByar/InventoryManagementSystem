using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories.Interfaces
{
    public interface IInventoryRepository : IRepository<Inventory>
    {
        void Update(Inventory inventory);
        void Save();
    }
}