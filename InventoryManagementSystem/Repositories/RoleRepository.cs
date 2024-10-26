using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.Interfaces;
using InventoryManagementSystem.Utility;

namespace InventoryManagementSystem.Repositories
{
    public class RoleRepository : Repository<ApplicationRole>, IRoleRepository
    {
        private AppDbContext _appDbContext;
        public RoleRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Update(ApplicationRole role)
        {
            _appDbContext.Update(role);
        }
        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
