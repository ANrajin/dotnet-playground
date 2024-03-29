﻿using FilesHandling.Test.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FilesHandling.Test.Controllers
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
            return View();
        }

        public IActionResult FilePond()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Store(FileHandlerModel model)
        {
            if (ModelState.IsValid)
                Console.WriteLine("hello");
            return RedirectToAction("FilePond");
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