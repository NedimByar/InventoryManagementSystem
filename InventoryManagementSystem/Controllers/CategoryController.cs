using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.Interfaces;
using InventoryManagementSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _CategoryRepository;  //singleton, dependency injection
        public CategoryController(ICategoryRepository context)
        {
            _CategoryRepository = context;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            List<Category> objInventoryList = _CategoryRepository.GetAll().ToList();
             
            return View(objInventoryList);
        }

        public IActionResult Create()
        {
            return View();
       
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)  // Validating the input to ensure doesn't empty or invalid inputs before saving to the database.
            {
                _CategoryRepository.Add(category);
                _CategoryRepository.Save();
                TempData["Succeed"] = "The item has been created successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Update(int? id) // GET
        {
            if(id== null || id==0)
            {
                return NotFound();
            }
            Category? category = _CategoryRepository.Get(u=>u.Id==id); //(System.Linq.Expressions.Expression<Func<T, bool>> filter)
            if (category == null) 
            {
                return NotFound();
            }
            return View(category);  
        }
         
        [HttpPost]
        public IActionResult Update(Category category) //POST
        {
            if (ModelState.IsValid)

            {
                _CategoryRepository.Update(category);
                _CategoryRepository.Save();
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
            Category? category = _CategoryRepository.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")] //POST
        public IActionResult DeletePOST(int? id)
        {
            Category? category = _CategoryRepository.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _CategoryRepository.Delete(category);
            _CategoryRepository.Save();
            TempData["Succeed"] = "The item has been deleted successfully.";
            return RedirectToAction("Index", "Category");
        }
    }
}
