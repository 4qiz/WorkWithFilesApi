using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace WorkWithFilesApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        

        private byte[] ConvertFileToBytes(string filePath)
        {
            var directory = Directory.GetCurrentDirectory();
            var path = Path.Combine(directory, "Files","Res", "onigiri.png");
            return System.IO.File.ReadAllBytes(path);
        }

        [HttpGet]
        public IActionResult GetImage()
        {
            var path = Path.Combine("Files", "Res", "image.png");
            var image = ConvertFileToBytes(path);
            return File(image, "img/png", "file.png");
        }

        [HttpGet]
        public IActionResult GetFile()
        {
            var path = Path.Combine("Files", "Res", "file.mp3");
            var image = ConvertFileToBytes(path);
            return File(image, "application/octet-stream", "file.mp3");
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            //extension check
            List<string> validExtensions = new List<string>() { ".jpg", ".png" };
            var ext = Path.GetExtension(file.FileName);
            if (!validExtensions.Contains(ext))
            {
                return BadRequest("ext not valid");
            }

            //size
            var size = file.Length;
            if (size > 5 * 1024 * 1024) // 5mb
            {
                return BadRequest("max size 5mb");
            }

            //path to save
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Uploads", file.FileName);

            using FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);

            return Ok();
        }
    }
}
