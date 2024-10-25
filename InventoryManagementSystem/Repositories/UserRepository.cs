using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.Interfaces;
using InventoryManagementSystem.Utility;

namespace InventoryManagementSystem.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private AppDbContext _appDbContext;
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Update(ApplicationUser user)
        {
            _appDbContext.Update(user);
        }
        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
