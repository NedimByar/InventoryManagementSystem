using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;
using InventoryManagementSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace InventoryManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _UserRepository;  //?singleton, dependency injection
        public readonly IWebHostEnvironment _WebHostEnvironment;

        public UserController(IUserRepository context, IWebHostEnvironment webHostEnvironment)
        {
            _UserRepository = context;
            _WebHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<ApplicationUser> userList = _UserRepository.GetAll().ToList();
            return View(userList);
        }

        public IActionResult Create()
        {
            return View();
            // The View() method is used to return the associated view for this action.
            // The view will be populated with data via Dependency Injection, using the model specified in the view file.
        }

        [HttpPost]
        public IActionResult Create(ApplicationUser user)
        {
            if (ModelState.IsValid)  // Validating the input to ensure doesn't empty or invalid inputs before saving to the database.

            {
                _UserRepository.Add(user); //
                _UserRepository.Save();
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
            ApplicationUser? user = _UserRepository.Get(u => u.Id == id); //(System.Linq.Expressions.Expression<Func<T, bool>> filter)
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Update")]
        public IActionResult UpdateUser(ApplicationUser user) //POST
        {
            if (ModelState.IsValid)
            {
                var existingUser = _UserRepository.Get(u => u.Id == user.Id);
                if (existingUser == null)
                {
                    return NotFound();
                }
                existingUser.UserName = user.UserName;
                existingUser.Email = user.Email;
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Department = user.Department; 

                // Generate a new ConcurrencyStamp
                existingUser.ConcurrencyStamp = Guid.NewGuid().ToString();

                _UserRepository.Update(existingUser);
                _UserRepository.Save();
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
            ApplicationUser? user = _UserRepository.Get(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")] //POST
        public IActionResult DeletePOST(string? id)
        {
            ApplicationUser? user = _UserRepository.Get(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            _UserRepository.Delete(user);
            _UserRepository.Save();
            TempData["Succeed"] = "The item has been deleted successfully.";
            return RedirectToAction("Index", "User");
        }
    }
}
