using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;
using InventoryManagementSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagementSystem.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly IUserRoleRepository _UserRoleRepository;  //singleton, dependency injection
        private readonly IUserRepository _UsersRepository;
        private readonly IRoleRepository _RolesRepository;
        public readonly IWebHostEnvironment _WebHostEnvironment;

        public UserRoleController(IUserRoleRepository context, IWebHostEnvironment webHostEnvironment, IRoleRepository RolesRepository, IUserRepository UsersRepository)
        {
            _UserRoleRepository = context;
            _WebHostEnvironment = webHostEnvironment;
            _RolesRepository = RolesRepository;
            _UsersRepository = UsersRepository;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            List<ApplicationUserRole> userList = _UserRoleRepository.GetAll("User,Role").ToList();
            return View(userList);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> RolesList = _RolesRepository.GetAll().Select(k => new SelectListItem //selecting items for comboBox/viewbag
            {
                Text = k.Name,
                Value = k.Id.ToString()
            });
            ViewBag.RolesList = RolesList;

            IEnumerable<SelectListItem> UsersList = _UsersRepository.GetAll().Select(k => new SelectListItem //selecting items for comboBox/viewbag
            {
                Text = k.FirstName + " " + k.LastName,
                Value = k.Id.ToString()
            });
            ViewBag.UsersList = UsersList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(ApplicationUserRole userRole)
        {
            if (string.IsNullOrEmpty(userRole.UserId) || string.IsNullOrEmpty(userRole.RoleId))
            {
                return NotFound();
            }
            _UserRoleRepository.Add(userRole); //
            _UserRoleRepository.Save();
            TempData["Succeed"] = "The item has been created successfully.";
            return RedirectToAction("Index");
            
        }



        public IActionResult Update(string userId, string roleId) // GET
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleId))
            {
                return NotFound();
            }
            IEnumerable<SelectListItem> RolesList = _RolesRepository.GetAll().Select(k => new SelectListItem //selecting items for comboBox/viewbag
            {
                Text = k.Name,
                Value = k.Id.ToString()
            });
            ViewBag.RolesList = RolesList;
            IEnumerable<SelectListItem> UsersList = _UsersRepository.GetAll().Select(k => new SelectListItem //selecting items for comboBox/viewbag
            {
                Text = k.FirstName + " " + k.LastName,
                Value = k.Id.ToString()
            });
            ViewBag.UsersList = UsersList;

            ApplicationUserRole? userRole = _UserRoleRepository.Get(u => u.UserId == userId && u.RoleId == roleId); //(System.Linq.Expressions.Expression<Func<T, bool>> filter)
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
                var existingUserRole = _UserRoleRepository.Get(u => u.UserId == userRole.UserId && u.RoleId == userRole.RoleId);
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

        public IActionResult Delete(string userId, string roleId) //GET 
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleId))
            {
                return NotFound();
            }
            ApplicationUserRole? userRole = _UserRoleRepository.Get(u => u.UserId == userId && u.RoleId == roleId);
            if (userRole == null)
            {
                return NotFound();
            }
            IEnumerable<SelectListItem> RolesList = _RolesRepository.GetAll().Select(k => new SelectListItem //selecting items for comboBox/viewbag
            {
                Text = k.Name,
                Value = k.Id.ToString()
            });
            ViewBag.RolesList = RolesList;
            IEnumerable<SelectListItem> UsersList = _UsersRepository.GetAll().Select(k => new SelectListItem //selecting items for comboBox/viewbag
            {
                Text = k.FirstName + " " + k.LastName,
                Value = k.Id.ToString()
            });
            ViewBag.UsersList = UsersList;
            return View(userRole);
        }

        [HttpPost, ActionName("Delete")] //POST
        public IActionResult DeletePOST(ApplicationUserRole userRole)
        {
            ApplicationUserRole? existingUserRole = _UserRoleRepository.Get(u => u.UserId == userRole.UserId && u.RoleId == userRole.RoleId);
            if (userRole == null)
            {
                return NotFound();
            }
            _UserRoleRepository.Delete(existingUserRole);
            _UserRoleRepository.Save();
            TempData["Succeed"] = "The item has been deleted successfully.";
            return RedirectToAction("Index", "UserRole");
        }
    }
}
