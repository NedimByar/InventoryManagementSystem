using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.Interfaces;
using InventoryManagementSystem.Utility;

namespace InventoryManagementSystem.Repositories
{
    public class UserRoleRepository : Repository<ApplicationUserRole>, IUserRoleRepository
    {
        private AppDbContext _appDbContext;
        public UserRoleRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Update(ApplicationUserRole userRole)
        {
            _appDbContext.Update(userRole);
        }
        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
