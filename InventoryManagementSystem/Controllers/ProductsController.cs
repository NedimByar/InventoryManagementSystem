using InventoryManagementSystem.Models;
using InventoryManagementSystem.Models.FilterModels;
using InventoryManagementSystem.Models.ViewModels;
using InventoryManagementSystem.Repositories;
using InventoryManagementSystem.Repositories.Interfaces;
using InventoryManagementSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Drawing2D;

namespace InventoryManagementSystem.Controllers
{


    public class ProductsController : Controller
    {
        private readonly IProductsRepository _ProductsRepository;  //singleton, dependency injection
        private readonly ICategoryRepository _CategoryRepository;
        public readonly IWebHostEnvironment _WebHostEnvironment;

        public ProductsController(IProductsRepository context, ICategoryRepository CategoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _ProductsRepository = context;
            _CategoryRepository = CategoryRepository;
            _WebHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Index(ProductFilterModel filterModel)
        {
            filterModel ??= new ProductFilterModel();

            var productsQuery = _ProductsRepository.GetAll(includeProps: "Category").AsQueryable();

            if (!string.IsNullOrEmpty(filterModel.ProductName))
            {
                productsQuery = productsQuery.Where(p => p.ProductName.Contains(filterModel.ProductName));
            }

            if (!string.IsNullOrEmpty(filterModel.SerialNumber))
            {
                productsQuery = productsQuery.Where(p => p.SerialNumber == Convert.ToInt32(filterModel.SerialNumber));
            }

            var viewModel = new ProductsViewModel
            {
                Filter = filterModel,
                Products = productsQuery.ToList() // Ensure this is a List
            };

            return View(viewModel);
        }

        [Authorize(Roles = UserRole.Role_Admin)]
        public IActionResult CreateUpdate(int? id)
        {
            IEnumerable<SelectListItem> InventoryList = _CategoryRepository.GetAll().Select(k => new SelectListItem //selecting items for comboBox/viewbag
            {
                Text = k.Name,
                Value = k.Id.ToString()

            });
            ViewBag.InventoryList = InventoryList;

            if (id==null || id == 0)
            {
                // CREATE NEW                  
                return View(); 
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

        [Authorize(Roles = UserRole.Role_Admin)]
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

        [Authorize(Roles = UserRole.Role_Admin)]
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

        [Authorize(Roles = UserRole.Role_Admin)]
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
