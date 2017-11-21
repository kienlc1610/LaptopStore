using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/UploadImage")]
    public class UploadImageController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
            long size = file.Length;

            // full path to file in temp location
            var filePath = Path.Combine(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()), "git\\LaptopStoreProject\\LaptopStore\\WebCore\\wwwroot\\img\\products", file.FileName);

            if (size > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { file = file, size, filePath });
        }
    }
}