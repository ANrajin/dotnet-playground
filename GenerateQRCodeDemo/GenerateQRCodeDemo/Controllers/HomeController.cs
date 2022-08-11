using GenerateQRCodeDemo.Models;
using IronBarCode;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing;

namespace GenerateQRCodeDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult CreateQRCode()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateQRCode(GenerateQRCodeModel model)
        {
            try
            {
                GeneratedBarcode generatedBarcode = QRCodeWriter.CreateQrCode(model.QRCodeText, 200);
                generatedBarcode.AddBarcodeValueTextBelowBarcode();

                generatedBarcode.SetMargins(10);
                generatedBarcode.ChangeBarCodeColor(Color.BlueViolet);

                string path = Path.Combine(_webHostEnvironment.WebRootPath, "GeneratedQRCode");

                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "GeneratedQRCode/qrcode.png");
                generatedBarcode.SaveAsPng(filePath);

                string fileName = Path.GetFileName(filePath);
                string imageUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/GeneratedQRCode/{fileName}";
                ViewBag.QrCodeUri = imageUrl;
            }
            catch(Exception)
            {
                throw;
            }

            return View();
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