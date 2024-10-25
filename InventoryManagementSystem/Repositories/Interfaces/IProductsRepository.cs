using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories.Interfaces
{
    public interface IProductsRepository : IRepository<Products>
    {
        void Update(Products products);
        void Save();
    }
}
