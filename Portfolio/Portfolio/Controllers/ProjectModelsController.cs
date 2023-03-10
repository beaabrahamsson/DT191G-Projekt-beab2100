using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System.Drawing;
using Microsoft.AspNetCore.Routing.Constraints;
using System.IO;
using System.Drawing.Drawing2D;

namespace Portfolio.Controllers
{
    public class ProjectModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProjectModelsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostEnvironment;
        }

        // GET: ProjectModels
        public async Task<IActionResult> Index()
        {
            return _context.Projects != null ?
                        View(await _context.Projects.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Projects'  is null.");
        }

        // GET: ProjectModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var projectModel = await _context.Projects
                .FirstOrDefaultAsync(m => m.ID == id);
            if (projectModel == null)
            {
                return NotFound();
            }

            return View(projectModel);
        }

        // GET: ProjectModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProjectModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,Link,GitHub,Image")] ProjectModel projectModel)
        {
            if (ModelState.IsValid)
            {
                //Save image to wwwroot/image
                string wwwRootPath = _hostingEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(projectModel.Image.FileName);
                string extension = Path.GetExtension(projectModel.Image.FileName);
                projectModel.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await projectModel.Image.CopyToAsync(fileStream);
                }

                //Insert record
                _context.Add(projectModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } 
            return View(projectModel);
        }

        // GET: ProjectModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var projectModel = await _context.Projects.FindAsync(id);
            if (projectModel == null)
            {
                return NotFound();
            }
            return View(projectModel);
        }

        // POST: ProjectModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile Image, [Bind("ID,Title,Description,Link,GitHub,ImageName,Image")] ProjectModel projectModel)
        {
            

            if (id != projectModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var im = await _context.Projects.AsNoTracking().SingleOrDefaultAsync(i => i.ID == id);

                if (im.ImageName != null) {
                    //delete image from wwwroot/image
                    string filePath = Path.Combine(_hostingEnvironment.WebRootPath + "/image/", im.ImageName);
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);
                }
               
                   
                //Save file to wwwroot/file
                string wwwRootPath = _hostingEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(projectModel.Image.FileName);
                string extension = Path.GetExtension(projectModel.Image.FileName);
                projectModel.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await projectModel.Image.CopyToAsync(fileStream);
                }
                _context.Add(projectModel);
                

                _context.Update(projectModel);
                await _context.SaveChangesAsync();             
                return RedirectToAction(nameof(Index));

            }
            return View(projectModel);
        }

        // GET: ProjectModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var projectModel = await _context.Projects
                .FirstOrDefaultAsync(m => m.ID == id);
            if (projectModel == null)
            {
                return NotFound();
            }

            return View(projectModel);
        }

        // POST: ProjectModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Projects'  is null.");
            }
            var projectModel = await _context.Projects.FindAsync(id);

            //delete file from wwwroot/file
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "image", projectModel.ImageName);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
            //delete the record
            _context.Projects.Remove(projectModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectModelExists(int id)
        {
            return (_context.Projects?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        [HttpPost, ActionName("Download")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Download(int id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Projects'  is null.");
            }
            var projectModel = await _context.Projects.FindAsync(id);

            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "image", "IMG235110849.webp");
            return File(System.IO.File.ReadAllBytes(filePath), "image/*", System.IO.Path.GetFileName(filePath));
        }
    }
}
