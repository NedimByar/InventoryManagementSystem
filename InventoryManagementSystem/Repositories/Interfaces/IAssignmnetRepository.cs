using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories.Interfaces
{
    public interface IAssignmentRepository : IRepository<Assignment>
    {
        void Update(Assignment assignment);
        void Save();
    }
}
