using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        void Update(ApplicationUser user);
        void Save();
    }
}
