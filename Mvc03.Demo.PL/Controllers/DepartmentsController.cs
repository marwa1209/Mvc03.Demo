using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
           var departments= _departmentRepository.GetAll();
            return View(departments);
        }
    }
}
