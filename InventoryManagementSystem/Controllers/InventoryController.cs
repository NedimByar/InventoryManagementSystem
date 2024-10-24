using InventoryManagementSystem.Models;
using InventoryManagementSystem.Utility;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class InventoryController : Controller
    {
        private readonly AppDbContext _appDbContext;  //singleton, dependency injection
        public InventoryController(AppDbContext context)
        {
            _appDbContext = context;
        }

        public IActionResult Index()
        {
            List<Inventory> objInventoryList = _appDbContext.Inventories.ToList();
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
                    _appDbContext.Inventories.Add(inventory);
                    _appDbContext.SaveChanges();
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
            Inventory? InventoryDb = _appDbContext.Inventories.Find(id);
            if(InventoryDb==null) 
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
                _appDbContext.Inventories.Update(inventory);
                _appDbContext.SaveChanges();
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
            Inventory? InventoryDb = _appDbContext.Inventories.Find(id);
            if (InventoryDb == null)
            {
                return NotFound();
            }
            return View(InventoryDb);
        }

        [HttpPost, ActionName("Delete")] //POST
        public IActionResult DeletePOST(int? id)
        {
            Inventory? inventory = _appDbContext.Inventories.Find(id);
            if (inventory == null)
            {
                return NotFound();
            }
            _appDbContext.Inventories.Remove(inventory);
            _appDbContext.SaveChanges();
            TempData["Succeed"] = "The item has been deleted successfully.";
            return RedirectToAction("Index", "Inventory");
        }
    }
}
