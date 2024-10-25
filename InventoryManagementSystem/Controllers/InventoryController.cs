using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.Interfaces;
using InventoryManagementSystem.Utility;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventoryRepository _InventoryRepository;  //?singleton, dependency injection
        public InventoryController(IInventoryRepository context)
        {
            _InventoryRepository = context;
        }

        public IActionResult Index()
        {
            List<Inventory> objInventoryList = _InventoryRepository.GetAll().ToList();
             
            return View(objInventoryList);
        }

        public IActionResult Create()
        {
            return View();
            // The View() method is used to return the associated view for this action.
            // The view will be populated with data via Dependency Injection, using the model specified in the view file.
        }

        [HttpPost]
        public IActionResult Create(Inventory inventory)
        {
            if (ModelState.IsValid)  // Validating the input to ensure doesn't empty or invalid inputs before saving to the database.

            {
                    _InventoryRepository.Add(inventory); //
                    _InventoryRepository.Save();
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
            Inventory? InventoryDb = _InventoryRepository.Get(u=>u.Id==id); //(System.Linq.Expressions.Expression<Func<T, bool>> filter)
            if (InventoryDb==null) 
            {
                return NotFound();
            }
            return View(InventoryDb);  
        }
         
        [HttpPost]
        public IActionResult Update(Inventory inventory) //POST
        {
            if (ModelState.IsValid)

            {
                _InventoryRepository.Update(inventory);
                _InventoryRepository.Save();
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
            Inventory? InventoryDb = _InventoryRepository.Get(u => u.Id == id);
            if (InventoryDb == null)
            {
                return NotFound();
            }
            return View(InventoryDb);
        }

        [HttpPost, ActionName("Delete")] //POST
        public IActionResult DeletePOST(int? id)
        {
            Inventory? inventory = _InventoryRepository.Get(u => u.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }
            _InventoryRepository.Delete(inventory);
            _InventoryRepository.Save();
            TempData["Succeed"] = "The item has been deleted successfully.";
            return RedirectToAction("Index", "Inventory");
        }
    }
}
