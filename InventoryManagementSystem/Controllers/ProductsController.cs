using InventoryManagementSystem.Models;
using InventoryManagementSystem.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagementSystem.Controllers
{

    public class ProductsController : Controller
    {
        private readonly IProductsRepository _ProductsRepository;  //?singleton, dependency injection
        private readonly IInventoryRepository _InventoryRepository;
        public readonly IWebHostEnvironment _WebHostEnvironment;

        public ProductsController(IProductsRepository context, IInventoryRepository InventoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _ProductsRepository = context;
            _InventoryRepository = InventoryRepository;
            _WebHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            //List<Products> objProductsList = _ProductsRepository.GetAll().ToList();
            List<Products> objProductsList = _ProductsRepository.GetAll(includeProps:"Inventory").ToList();
            return View(objProductsList);
        }

        //public IActionResult Create() // OLD CREATE
        //{
        //    IEnumerable<SelectListItem> InventoryList = _InventoryRepository.GetAll().Select(k => new SelectListItem //selecting items for comboBox/viewbag
        //    {
        //        Text = k.Name,
        //        Value = k.Id.ToString()

        //    });
        //    ViewBag.InventoryList = InventoryList;
        //    return View();   // The View() method is used to return the associated view for this action.
        //                     // The view will be populated with data via Dependency Injection, using the model specified in the view file.
        //}

        public IActionResult CreateUpdate(int? id)
        {
            IEnumerable<SelectListItem> InventoryList = _InventoryRepository.GetAll().Select(k => new SelectListItem //selecting items for comboBox/viewbag
            {
                Text = k.Name,
                Value = k.Id.ToString()

            });
            ViewBag.InventoryList = InventoryList;

            if (id==null || id == 0)
            {
                // CREATE NEW                  
                return View();  // The View() method is used to return the associated view for this action.
                                // The view will be populated with data via Dependency Injection, using the model specified in the view file.
            }

            else
            {
                //UPDATE NEW
                Products? ProductsDb = _ProductsRepository.Get(u => u.Id == id); //(System.Linq.Expressions.Expression<Func<T, bool>> filter)
                if (ProductsDb == null)
                {
                    return NotFound();
                }
                return View(ProductsDb);
            }


        }

        [HttpPost]
        public IActionResult CreateUpdate(Products products, IFormFile? file)
        {
            if (ModelState.IsValid)  // Validating the input to ensure doesn't empty or invalid inputs before saving to the database.

            {
                string wwwRootPath = _WebHostEnvironment.WebRootPath;
                string InventoryPath = Path.Combine(wwwRootPath, @"img");


                if (file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(InventoryPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    products.ImageURL = @"\img\" + file.FileName;
                }
                


                if (products.Id ==0)
                {
                    _ProductsRepository.Add(products);
                    TempData["Succeed"] = "The item has been created successfully.";
                }
                else
                {
                    _ProductsRepository.Update(products);
                    TempData["Succeed"] = "The item has been updated successfully.";
                }

                _ProductsRepository.Save();
                return RedirectToAction("Index");
                }
            return View();
        }


        /* // OLD UPDATE
        public IActionResult Update(int? id) // GET
        {
            if(id== null || id==0)
            {
                return NotFound();
            }
            Products? ProductsDb = _ProductsRepository.Get(u=>u.Id==id); //(System.Linq.Expressions.Expression<Func<T, bool>> filter)
            if (ProductsDb == null) 
            {
                return NotFound();
            }
            return View(ProductsDb);  
        }
        */

        /* OLD UPDATE/HttpPOST
        [HttpPost]  
        public IActionResult Update(Products products) //POST
        {
            if (ModelState.IsValid)

            {
                _ProductsRepository.Update(products); //
                _ProductsRepository.Save();
                TempData["Succeed"] = "The item has been updated successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
        */

        public IActionResult Delete(int? id) //GET 
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Products? ProductsDb = _ProductsRepository.Get(u => u.Id == id);
            if (ProductsDb == null)
            {
                return NotFound();
            }
            return View(ProductsDb);
        }

        [HttpPost, ActionName("Delete")] //POST
        public IActionResult DeletePOST(int? id)
        {
            Products? products = _ProductsRepository.Get(u => u.Id == id);
            if (products == null)
            {
                return NotFound();
            }
            _ProductsRepository.Delete(products);
            _ProductsRepository.Save();
            TempData["Succeed"] = "The item has been deleted successfully.";
            return RedirectToAction("Index", "Products");
        }
    }
}
