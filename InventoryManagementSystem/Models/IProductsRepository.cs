namespace InventoryManagementSystem.Models
{
    public interface IProductsRepository : IRepository<Products>
    {
        void Update(Products products);
        void Save();
    }
}
