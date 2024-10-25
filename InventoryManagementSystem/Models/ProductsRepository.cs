using InventoryManagementSystem.Utility;
using System.Linq.Expressions;

namespace InventoryManagementSystem.Models
{
    public class InventoryRepository : Repository<Inventory>, IInventoryRepository
    {
        private AppDbContext _appDbContext;
        public InventoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Update(Inventory inventory)
        {
            _appDbContext.Update(inventory);
        }
        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
