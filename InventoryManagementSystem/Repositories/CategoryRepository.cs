using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.Interfaces;
using InventoryManagementSystem.Utility;
using System.Linq.Expressions;

namespace InventoryManagementSystem.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private AppDbContext _appDbContext;
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Update(Category category)
        {
            _appDbContext.Update(category);
        }
        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
