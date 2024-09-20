using Microsoft.AspNetCore.Mvc;
using Mvc.Demo.DAL.Models;
using Mvc03.Demo.BLL.Interfaces;

namespace Mvc03.Demo.PL.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentsController(IDepartmentRepository DepartmentRepository)
        {
            _departmentRepository = DepartmentRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department model)
        {
            if (ModelState.IsValid)
            {
                var Count = _departmentRepository.Add(model);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int? Id)
        {
            if (Id is null) return BadRequest();
            var department = _departmentRepository.GetDepartment(Id.Value);
            if (department is null) return BadRequest();
            return View(department);
        }
        [HttpGet]
        public IActionResult Update(int? Id)
        {
            if (Id is null) return BadRequest();
            var department = _departmentRepository.GetDepartment(Id.Value);
            if (department is null) return BadRequest();
            return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute] int? id, Department model)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                var Count = _departmentRepository.Update(model);
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
                var department = _departmentRepository.GetDepartment(id);
                if (department == null) return NotFound();

                var Count = _departmentRepository.Delete(department);
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
