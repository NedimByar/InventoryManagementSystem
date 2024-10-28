using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleRepository _RoleRepository;  //singleton, dependency injection
        public readonly IWebHostEnvironment _WebHostEnvironment;

        public RoleController(IRoleRepository context, IWebHostEnvironment webHostEnvironment)
        {
            _RoleRepository = context;
            _WebHostEnvironment = webHostEnvironment;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            List<ApplicationRole> roleList = _RoleRepository.GetAll().ToList();
            return View(roleList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ApplicationRole role)
        {
            if (ModelState.IsValid)  // Validating the input to ensure doesn't empty or invalid inputs before saving to the database.

            {
                _RoleRepository.Add(role); //
                _RoleRepository.Save();
                TempData["Succeed"] = "The item has been created successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Update(string? id) // GET
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            ApplicationRole? role = _RoleRepository.Get(u => u.Id == id); //(System.Linq.Expressions.Expression<Func<T, bool>> filter)
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        [HttpPost, ActionName("Update")]
        public IActionResult UpdateRole(ApplicationRole role) //POST
        {
            if (ModelState.IsValid)
            {
                var existingRole = _RoleRepository.Get(u => u.Id == role.Id);
                if (existingRole == null)
                {
                    return NotFound();
                }
                existingRole.Name = role.Name;
                existingRole.NormalizedName = role.NormalizedName;
                existingRole.ConcurrencyStamp = Guid.NewGuid().ToString();

                _RoleRepository.Update(existingRole);
                _RoleRepository.Save();
                TempData["Succeed"] = "The item has been updated successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(string? id) //GET 
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            ApplicationRole? role = _RoleRepository.Get(u => u.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        [HttpPost, ActionName("Delete")] //POST
        public IActionResult DeletePOST(string? id)
        {
            ApplicationRole? role = _RoleRepository.Get(u => u.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            _RoleRepository.Delete(role);
            _RoleRepository.Save();
            TempData["Succeed"] = "The item has been deleted successfully.";
            return RedirectToAction("Index", "Role");
        }
    }
}
