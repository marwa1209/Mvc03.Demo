using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc03.Demo.PL.Services;
using Mvc03.Demo.PL.ViewModels;
using System.Diagnostics;
using System.Text;

namespace Mvc03.Demo.PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedService scoped01;
        private readonly IScopedService scoped02;
        private readonly ITransientService transient01;
        private readonly ITransientService transient02;
        private readonly ISingeltonService singeltont01;
        private readonly ISingeltonService singeltont02;

        public HomeController
            (
            ILogger<HomeController> logger,
            IScopedService scoped01,
            IScopedService scoped02,
            ITransientService transient01,
            ITransientService transient02,
            ISingeltonService Singeltont01,
            ISingeltonService Singeltont02

            )
        {
            _logger = logger;
            this.scoped01 = scoped01;
            this.scoped02 = scoped02;
            this.transient01 = transient01;
            this.transient02 = transient02;
            singeltont01 = Singeltont01;
            singeltont02 = Singeltont02;
        }
        public string TestLifeTime()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Scoped01 = {scoped01.GetGuid()}\n");
            stringBuilder.Append($"scoped02 = {scoped02.GetGuid()}\n\n");
            stringBuilder.Append($"scoped02 = {transient01.GetGuid()}\n");
            stringBuilder.Append($"scoped02 = {transient02.GetGuid()}\n\n");
            stringBuilder.Append($"scoped02 = {singeltont01.GetGuid()}\n");
            stringBuilder.Append($"scoped02 = {singeltont02.GetGuid()}\n\n");
            return stringBuilder.ToString();    
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
