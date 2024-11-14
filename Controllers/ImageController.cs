using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using TextEditor.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TextEditor.Data;

namespace TextEditor.Controllers
{
    [Authorize(Roles = "User")]
    public class ImageController : Controller
    {
        private readonly string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ImageController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User); // Get the current logged-in user ID
            var userImages = await _context.Images
                .Where(i => i.UserId == userId)
                .ToListAsync();

            return View(userImages);
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            if (file != null)
            {
                // Get the current user's ID
                var userId = _userManager.GetUserId(User);

                // Create unique file path
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(rootPath, fileName);

                // Save the file to the server
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }

                // Save the file metadata to the database
                var image = new Image
                {
                    FileName = fileName,
                    FilePath = filePath,
                    UserId = userId,
                    UploadDate = DateTime.Now
                };

                _context.Images.Add(image);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> DownloadImage(int id)
        {
            var image = await _context.Images.FindAsync(id);

            if (image != null)
            {
                var memory = new MemoryStream();
                using (var fs = new FileStream(image.FilePath, FileMode.Open))
                {
                    await fs.CopyToAsync(memory);
                }
                memory.Position = 0;

                return File(memory, "application/octet-stream", image.FileName);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _context.Images.FindAsync(id);

            if (image != null)
            {
                if (System.IO.File.Exists(image.FilePath))
                {
                    System.IO.File.Delete(image.FilePath);
                }

                _context.Images.Remove(image);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
