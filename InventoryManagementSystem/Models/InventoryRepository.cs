using InventoryManagementSystem.Utility;
using System.Linq.Expressions;

namespace InventoryManagementSystem.Models
{
    public class ProductsRepository : Repository<Products>, IProductsRepository
    {
        private AppDbContext _appDbContext;
        public ProductsRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Update(Products products)
        {
            _appDbContext.Update(products);
        }
        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
