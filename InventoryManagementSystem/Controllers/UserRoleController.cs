using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly IUserRoleRepository _UserRoleRepository;  //singleton, dependency injection
        public readonly IWebHostEnvironment _WebHostEnvironment;

        public UserRoleController(IUserRoleRepository context, IWebHostEnvironment webHostEnvironment)
        {
            _UserRoleRepository = context;
            _WebHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<ApplicationUserRole> userList = _UserRoleRepository.GetAll().ToList();
            return View(userList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ApplicationUserRole userRole)
        {
            if (ModelState.IsValid)  // Validating the input to ensure doesn't empty or invalid inputs before saving to the database.

            {
                _UserRoleRepository.Add(userRole); //
                _UserRoleRepository.Save();
                TempData["Succeed"] = "The item has been created successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }



        public IActionResult Update(int? id) // GET
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            ApplicationUserRole? userRole = _UserRoleRepository.Get(u => u.Id == id); //(System.Linq.Expressions.Expression<Func<T, bool>> filter)
            if (userRole == null)
            {
                return NotFound();
            }
            return View(userRole);
        }

        [HttpPost, ActionName("Update")]
        public IActionResult UpdateUserRole(ApplicationUserRole userRole) //POST
        {
            if (ModelState.IsValid)
            {
                var existingUserRole = _UserRoleRepository.Get(u => u.Id == userRole.Id);
                if (existingUserRole == null)
                {
                    return NotFound();
                }
                existingUserRole.UserId = userRole.UserId;
                existingUserRole.RoleId = userRole.RoleId;

                _UserRoleRepository.Update(existingUserRole);
                _UserRoleRepository.Save();
                TempData["Succeed"] = "The item has been updated successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id) //GET 
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            ApplicationUserRole? userRole = _UserRoleRepository.Get(u => u.Id == id);
            if (userRole == null)
            {
                return NotFound();
            }
            return View(userRole);
        }

        [HttpPost, ActionName("Delete")] //POST
        public IActionResult DeletePOST(int? id)
        {
            ApplicationUserRole? userRole = _UserRoleRepository.Get(u => u.Id == id);
            if (userRole == null)
            {
                return NotFound();
            }
            _UserRoleRepository.Delete(userRole);
            _UserRoleRepository.Save();
            TempData["Succeed"] = "The item has been deleted successfully.";
            return RedirectToAction("Index", "UserRole");
        }
    }
}
