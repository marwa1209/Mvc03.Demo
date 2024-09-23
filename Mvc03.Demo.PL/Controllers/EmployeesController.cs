using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mvc.Demo.DAL.Models;
using Mvc03.Demo.BLL.Interfaces;
using Mvc03.Demo.BLL.Repositories;
using Mvc03.Demo.PL.ViewModels.Employees;

namespace Mvc03.Demo.PL.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper mapper;

        public EmployeesController(IEmployeeRepository EmployeeRepository, IDepartmentRepository DepartmentRepository, IMapper mapper)

        {
            _employeeRepository = EmployeeRepository;
            _departmentRepository = DepartmentRepository;
            this.mapper = mapper;
        }
        public IActionResult Index(string searchString)
        {
            var employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(searchString))
            {
                employees = _employeeRepository.GetAll();
            }
            else
            {
                employees = _employeeRepository.GetByName(searchString);
            }
            var Results = mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            /// View's Dictionary:Transfer From Action To View [One Way]
            /// 1.ViewData:Property Inherited from Controller Class,Dictionary
            /// ViewData["Data01"]="Hello World From Data01 ViewData";//Required Casting
            /// 2.ViewBag:Property Inherited from Controller Class,dynamic
            /// ViewBag.Data02="Hello World From ViewBag"//Not Required Casting
            /// 3.TempData:Property Inherited from Controller Class,Dictionary
            /// TempData["Data01"]="Hello World From Data01 TempData"; //transfer data from request to another request
            return View(Results);
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
        public IActionResult Create(EmployeeViewModel model)
        {
            //cast employeeViewModel to Employee
            //Mapping
            //1.Manual Mapping
            //Employee employee = new Employee()
            //{
            //    Id = model.Id,
            //    Name = model.Name,
            //    Address = model.Address,
            //    Salary = model.Salary,
            //    Age = model.Age,
            //    HiringDate = model.HiringDate,
            //    WorkFor = model.WorkFor,
            //    WorkForId = model.WorkForId,
            //    Email = model.Email,
            //    PhoneNumber = model.PhoneNumber,
            //    IsActive = model.IsActive,
            //};
            //2.Auto Mapping
            var employee = mapper.Map<Employee>(model);
            if (ModelState.IsValid)
            {
                var Count = _employeeRepository.Add(employee);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();

            var model = _employeeRepository.Get(id.Value);
            if (model is null) return NotFound();

            // Map Employee to EmployeeViewModel
            //var employeeViewModel = new EmployeeViewModel()
            //{
            //    Id = model.Id,
            //    Name = model.Name,
            //    Address = model.Address,
            //    Salary = model.Salary,
            //    Age = model.Age,
            //    HiringDate = model.HiringDate,
            //    WorkFor = model.WorkFor,
            //    WorkForId = model.WorkForId,
            //    Email = model.Email,
            //    PhoneNumber = model.PhoneNumber,
            //    IsActive = model.IsActive
            //};
            var employeeViewModel = mapper.Map<EmployeeViewModel>(model);
            return View(viewName, employeeViewModel);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id is null) return BadRequest();

            var model = _employeeRepository.Get(id.Value);
            if (model is null) return NotFound();

            //var employeeViewModel = new EmployeeViewModel()
            //{
            //    Id = model.Id,
            //    Name = model.Name,
            //    Address = model.Address,
            //    Salary = model.Salary,
            //    Age = model.Age,
            //    HiringDate = model.HiringDate,
            //    WorkFor = model.WorkFor,
            //    WorkForId = model.WorkForId,
            //    Email = model.Email,
            //    PhoneNumber = model.PhoneNumber,
            //    IsActive = model.IsActive
            //};
            var employeeViewModel = mapper.Map<EmployeeViewModel>(model);
            var departments = _departmentRepository.GetAll();
            ViewData["departments"] = departments;

            return View("Update", employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute] int? id, EmployeeViewModel model)
        {
            if (id != model.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                // Manual mapping from EmployeeViewModel to Employee
                //var employee = new Employee()
                //{
                //    Id = model.Id,
                //    Name = model.Name,
                //    Address = model.Address,
                //    Salary = model.Salary,
                //    Age = model.Age,
                //    HiringDate = model.HiringDate,
                //    WorkFor = model.WorkFor,
                //    WorkForId = model.WorkForId,
                //    Email = model.Email,
                //    PhoneNumber = model.PhoneNumber,
                //    IsActive = model.IsActive
                //};
                var employee = mapper.Map<Employee>(model);
                var count = _employeeRepository.Update(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            var departments = _departmentRepository.GetAll();
            ViewData["departments"] = departments;

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
