using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;
using InventoryManagementSystem.Repositories.Interfaces;
using InventoryManagementSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace InventoryManagementSystem.Controllers
{

    public class AssignmentController : Controller
    {
        private readonly IAssignmentRepository _AssignmentRepository;  //?singleton, dependency injection
        private readonly IProductsRepository _ProductsRepository;
        private readonly IUserRepository _UsersRepository;
        public readonly IWebHostEnvironment _WebHostEnvironment;

        public AssignmentController(IAssignmentRepository AssignmentRepository, IProductsRepository ProductsRepository, IWebHostEnvironment webHostEnvironment, IUserRepository usersRepository)
        {
            _AssignmentRepository = AssignmentRepository;
            _ProductsRepository = ProductsRepository;
            _WebHostEnvironment = webHostEnvironment;
            _UsersRepository = usersRepository;
        }

        [Authorize(Roles = "IT")]
        public IActionResult Index()
        {
            List<Assignment> objAssignmentList = _AssignmentRepository.GetAll(includeProps: "User,Product").ToList();
            return View(objAssignmentList);
        }

        public IActionResult CreateUpdate(int? id)
        {
            IEnumerable<SelectListItem> ProductsList = _ProductsRepository.GetAll().Select(k => new SelectListItem //selecting items for comboBox/viewbag
            {
                Text = k.ProductName,
                Value = k.Id.ToString()

            });
            ViewBag.ProductsList = ProductsList; 

            IEnumerable<SelectListItem> UsersList = _UsersRepository.GetAll().Select(k => new SelectListItem //selecting items for comboBox/viewbag
            {
                Text = k.FirstName + " " + k.LastName,
                Value = k.Id.ToString()
            });
            ViewBag.UsersList = UsersList;

            if (id==null || id == 0)
            {
                return View();                                  
            }

            else
            {
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

        public IActionResult Delete(int? id) //GET 
        {
            IEnumerable<SelectListItem> ProductsList = _ProductsRepository.GetAll().Select(k => new SelectListItem //selecting items for comboBox/viewbag
            {
                Text = k.ProductName,
                Value = k.Id.ToString()

            });
            ViewBag.ProductsList = ProductsList;

            IEnumerable<SelectListItem> UsersList = _UsersRepository.GetAll().Select(k => new SelectListItem //selecting items for comboBox/viewbag
            {
                Text = k.FirstName + " " + k.LastName,
                Value = k.Id.ToString()
            });
            ViewBag.UsersList = UsersList;

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
