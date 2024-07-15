using FakeDataGeneration.Models;
using FakeDataGeneration.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task_5_FakeDataGeneration_.Models;


namespace FakeDataGeneration_Task5.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataGeneratorService _dataGenerator;
        private readonly ErrorGeneratorService _errorGenerator;
        public HomeController(DataGeneratorService dataGenerator, ErrorGeneratorService errorGenerator)
        {
            
            _dataGenerator = dataGenerator;
            _errorGenerator = errorGenerator;
        }

        private List<string> UserModelToList(UserModel user)
        {
            return new List<string>() { user.Name, user.Email, user.Address, user.Phone};
        }
        private UserModel ListToUserModel(List<string> userData)
        {
            return new UserModel
            {
                Name = userData[0],
                Email = userData[1],
                Address = userData[2],
                Phone = userData[3]
            };
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetFakeUsers(string locale, int errorCount, int seed, int record = 20)
        {
            if (errorCount < 0)
            {
                return BadRequest("Errors must be greater than or equal to 0");
            }


            var data = _dataGenerator.GetFakeData(seed, locale, record);

            var errorData = new List<UserModel>();

            foreach (var user in data)
            {
                var userData = UserModelToList(user);
                var erroredUserData = _errorGenerator.AddErrors(userData, errorCount);
                var erroredUserModel = ListToUserModel(erroredUserData);
                erroredUserModel.Id = user.Id;
                errorData.Add(erroredUserModel);
            }

            return Ok(errorData);
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
