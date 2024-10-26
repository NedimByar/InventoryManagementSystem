using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories.Interfaces
{
    public interface IUserRoleRepository : IRepository<ApplicationUserRole>
    {
        void Update(ApplicationUserRole userRole);
        void Save();
    }
}
