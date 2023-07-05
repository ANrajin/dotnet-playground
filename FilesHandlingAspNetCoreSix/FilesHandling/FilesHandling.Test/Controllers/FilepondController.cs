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
        public async Task<ActionResult> Process(IFormFile files, CancellationToken cancellationToken)
        {
            var folder = Guid.NewGuid().ToString();
            // full path to file in temp location
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, $"tmp/{folder}");
            Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, filePath));

            try
            {
                var fileName = files.FileName;

                using var newMemoryStream = new MemoryStream();
                await files.CopyToAsync(newMemoryStream, cancellationToken);

                if (files.Length > 0)
                {
                    using (var stream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                    {
                        await files.CopyToAsync(stream);
                    }
                }

                return Ok(folder);
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

            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "tmp");
            var filePath = Path.Combine(folderPath, guid.ToString());

            FileInfo file = new FileInfo(filePath);

            if (Directory.Exists(filePath))//check file exsit or not  
            {
                Directory.Delete(filePath, true);
            }

            return Ok();
        }
    }
}
