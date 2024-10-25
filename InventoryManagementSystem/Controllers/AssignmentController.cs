using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;
using InventoryManagementSystem.Repositories.Interfaces;
using InventoryManagementSystem.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace InventoryManagementSystem.Controllers
{

    public class AssignmentController : Controller
    {
        private readonly IAssignmentRepository _AssignmentRepository;  //?singleton, dependency injection
        private readonly IProductsRepository _ProductsRepository;
        public readonly IWebHostEnvironment _WebHostEnvironment;

        public AssignmentController(IAssignmentRepository AssignmentRepository, IProductsRepository ProductsRepository, IWebHostEnvironment webHostEnvironment)
        {
            _AssignmentRepository = AssignmentRepository;
            _ProductsRepository = ProductsRepository;
            _WebHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Assignment> objAssignmentList = _AssignmentRepository.GetAll(includeProps: "Product").ToList();
            return View(objAssignmentList);
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
            IEnumerable<SelectListItem> ProductsList = _ProductsRepository.GetAll().Select(k => new SelectListItem //selecting items for comboBox/viewbag
            {
                Text = k.ProductName,
                Value = k.Id.ToString()

            });
            ViewBag.ProductsList = ProductsList;

            if (id==null || id == 0)
            {
                // CREATE NEW                  
                return View();  // The View() method is used to return the associated view for this action.
                                // The view will be populated with data via Dependency Injection, using the model specified in the view file.
            }

            else
            {
                //UPDATE NEW
                Assignment? AssignmentDb = _AssignmentRepository.Get(u => u.Id == id); //(System.Linq.Expressions.Expression<Func<T, bool>> filter)
                if (AssignmentDb == null)
                {
                    return NotFound();
                }
                return View(AssignmentDb);
            }


        }

        [HttpPost]
        public IActionResult CreateUpdate(Assignment assignment)
        {
            if (ModelState.IsValid)  // Validating the input to ensure doesn't empty or invalid inputs before saving to the database.
 
            {
                if (assignment.Id ==0)
                {
                    _AssignmentRepository.Add(assignment);
                    TempData["Succeed"] = "The new Assignment is successfully created.";
                }
                else
                {
                    _AssignmentRepository.Update(assignment);
                    TempData["Succeed"] = "The new Assignment is successfully updated.";
                }

                _AssignmentRepository.Save();
                return RedirectToAction("Index", "Assignment");
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
            IEnumerable<SelectListItem> ProductsList = _ProductsRepository.GetAll().Select(k => new SelectListItem //selecting items for comboBox/viewbag
            {
                Text = k.ProductName,
                Value = k.Id.ToString()

            });
            ViewBag.ProductsList = ProductsList;

            if (id == null || id == 0)
            {
                return NotFound();
            }
            Assignment? AssignmentDb = _AssignmentRepository.Get(u => u.Id == id);
            if (AssignmentDb == null)
            {
                return NotFound();
            }
            return View(AssignmentDb);
        }

        [HttpPost, ActionName("Delete")] //POST
        public IActionResult DeletePOST(int? id)
        {
            Assignment? Assignment = _AssignmentRepository.Get(u => u.Id == id);
            if (Assignment == null)
            {
                return NotFound();
            }
            _AssignmentRepository.Delete(Assignment);
            _AssignmentRepository.Save();
            TempData["Succeed"] = "The item has been deleted successfully.";
            return RedirectToAction("Index", "Assignment");
        }
    }
}
