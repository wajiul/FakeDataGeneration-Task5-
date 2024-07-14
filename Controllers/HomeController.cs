using FakeDataGeneration.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task_5_FakeDataGeneration_.Models;


namespace FakeDataGeneration_Task5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var dg = new DataGeneratorService();
            var data = dg.GetFakeData(1234, "en_IND");
            return Ok(data);
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
