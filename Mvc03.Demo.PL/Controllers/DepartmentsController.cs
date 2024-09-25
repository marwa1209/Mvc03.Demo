using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.Demo.DAL.Models;
using Mvc03.Demo.BLL.Interfaces;

namespace Mvc03.Demo.PL.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentsController(IDepartmentRepository DepartmentRepository)
        {
            _departmentRepository = DepartmentRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departments = await _departmentRepository.GetAllAsync();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department model)
        {
            if (ModelState.IsValid)
            {
                var Count =await _departmentRepository.AddAsync(model);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? Id ,string viewName = "Details")
        {
            if (Id is null) return BadRequest();
            var department = await _departmentRepository.GetAsync(Id.Value);
            if (department is null) return BadRequest();
            return View(viewName,department);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return BadRequest("Department ID cannot be null");
            var department = await _departmentRepository.GetAsync(id.Value);
            if (department is null) return NotFound("Department not found");
            return View("Update", department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] int? id, Department model)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                var Count =await _departmentRepository.UpdateAsync(model);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var department = await _departmentRepository.GetAsync(id);
                if (department == null) return NotFound();

                var Count = await _departmentRepository.DeleteAsync(department);
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
