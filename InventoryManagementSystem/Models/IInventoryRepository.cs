namespace InventoryManagementSystem.Models
{
    public interface IInventoryRepository : IRepository<Inventory>
    {
        void Update(Inventory inventory);
        void Save();
    }
}
