using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories.Interfaces
{

    public interface IRoleRepository : IRepository<ApplicationRole>
    {
        void Update(ApplicationRole role);
        void Save();
    }
}
