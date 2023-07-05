using Mapster;
using MapsterExp.DTOs;
using MapsterExp.Entities;
using MapsterExp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MapsterExp.Controllers
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
            Student student = new Student()
            {
                Id = 1,
                FirstName = "Hello",
                LastName = "World",
                Email = "hello.world@gmail.com",
                Addresses = new List<Address>() { new Address()
                    {
                        Id = 1,
                        City = "Dhaka",
                        Region = "Mirpur",
                        Country = "Bangladesh"
                    },
                    new Address(){
                        Id = 2,
                        City = "Sylhet",
                        Region = "Habiganj",
                        Country = "Bangladesh"
                    }}
            };

            var data = student.Adapt<StudentDto>();

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