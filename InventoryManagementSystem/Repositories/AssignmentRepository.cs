using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.Interfaces;
using InventoryManagementSystem.Utility;
using System.Linq.Expressions;

namespace InventoryManagementSystem.Repositories
{
    public class AssignmentRepository : Repository<Assignment>, IAssignmentRepository
    {
        private AppDbContext _appDbContext;
        public AssignmentRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Update(Assignment assignment)
        {
            _appDbContext.Update(assignment);
        }
        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
