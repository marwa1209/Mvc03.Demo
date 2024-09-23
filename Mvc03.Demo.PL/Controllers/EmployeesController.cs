using Microsoft.AspNetCore.Mvc;
using Mvc.Demo.DAL.Models;
using Mvc03.Demo.BLL.Interfaces;
using Mvc03.Demo.BLL.Repositories;

namespace Mvc03.Demo.PL.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        public EmployeesController(IEmployeeRepository  EmployeeRepository , IDepartmentRepository DepartmentRepository)
        {
            _employeeRepository = EmployeeRepository;
            _departmentRepository=DepartmentRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();
            /// View's Dictionary:Transfer From Action To View [One Way]
            /// 1.ViewData:Property Inherited from Controller Class,Dictionary
            /// ViewData["Data01"]="Hello World From Data01 ViewData";//Required Casting
            /// 2.ViewBag:Property Inherited from Controller Class,dynamic
            /// ViewBag.Data02="Hello World From ViewBag"//Not Required Casting
            /// 3.TempData:Property Inherited from Controller Class,Dictionary
            /// TempData["Data01"]="Hello World From Data01 TempData"; //transfer data from request to another request
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var departments = _departmentRepository.GetAll();
            ViewData["departments"] = departments;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee model)
        {
            if (ModelState.IsValid)
            {
                var Count = _employeeRepository.Add(model);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int? Id, string viewName = "Details")
        {
            if (Id is null) return BadRequest();
            var employee = _employeeRepository.Get(Id.Value);
            if (employee is null) return BadRequest();
            return View(viewName, employee);
        }
        [HttpGet]
        public IActionResult Update(int? Id)
        {
            var departments = _departmentRepository.GetAll();
            ViewData["departments"] = departments;
            return Details(Id, "Update");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute] int? id, Employee model)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                var Count = _employeeRepository.Update(model);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var employee = _employeeRepository.Get(id);
                if (employee == null) return NotFound();

                var Count = _employeeRepository.Delete(employee);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return BadRequest();
        }
    }
}
