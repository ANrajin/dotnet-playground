using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace FilesHandling.Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilepondController : Controller
    {
        private IWebHostEnvironment _webHostEnvironment;

        public FilepondController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<ActionResult> Process(IFormFile file, CancellationToken cancellationToken)
        {
            try
            {
                var guid = Guid.NewGuid().ToString();
                var originalName = file.FileName.Split('.').LastOrDefault();
                var newimage = string.Format("{0}.{1}", guid, originalName);

                using var newMemoryStream = new MemoryStream();
                await file.CopyToAsync(newMemoryStream, cancellationToken);

                if (file.Length > 0)
                {
                    // full path to file in temp location
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "public");

                    using (var stream = new FileStream(Path.Combine(filePath, newimage), FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                return Ok(newimage);
            }
            catch (Exception e)
            {
                return BadRequest($"Process Error: {e.Message}"); // Oops!
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Revert()
        {
            using StreamReader reader = new(Request.Body, Encoding.UTF8);
            string guid = await reader.ReadToEndAsync();

            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "public");
            var filePath = Path.Combine(folderPath, guid.ToString());

            FileInfo file = new FileInfo(filePath);

            if (file.Exists)//check file exsit or not  
            {
                file.Delete();
            }

            return Ok();
        }
    }
}
