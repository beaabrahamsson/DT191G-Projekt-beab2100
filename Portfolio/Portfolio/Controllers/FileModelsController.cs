using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Portfolio.Data;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class FileModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public FileModelsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostEnvironment;
        }

        // GET: FileModels
        public async Task<IActionResult> Index()
        {
              return _context.Files != null ? 
                          View(await _context.Files.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Files'  is null.");
        }

        // GET: FileModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Files == null)
            {
                return NotFound();
            }

            var fileModel = await _context.Files
                .FirstOrDefaultAsync(m => m.ID == id);
            if (fileModel == null)
            {
                return NotFound();
            }

            return View(fileModel);
        }

        // GET: FileModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FileModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,File")] FileModel fileModel)

        {
            if (ModelState.IsValid)
            {

                //Save image to wwwroot/image
                string wwwRootPath = _hostingEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(fileModel.File.FileName);
                string extension = Path.GetExtension(fileModel.File.FileName);
                fileModel.FileName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await fileModel.File.CopyToAsync(fileStream);
                }
                //Insert record
                _context.Add(fileModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fileModel);
        }

        // GET: FileModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Files == null)
            {
                return NotFound();
            }

            var fileModel = await _context.Files.FindAsync(id);
            if (fileModel == null)
            {
                return NotFound();
            }
            return View(fileModel);
        }

        // POST: FileModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,FileName")] FileModel fileModel)
        {
            if (id != fileModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fileModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileModelExists(fileModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fileModel);
        }

        // GET: FileModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Files == null)
            {
                return NotFound();
            }

            var fileModel = await _context.Files
                .FirstOrDefaultAsync(m => m.ID == id);
            if (fileModel == null)
            {
                return NotFound();
            }

            return View(fileModel);
        }

        // POST: FileModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Files == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Files'  is null.");
            }
            var fileModel = await _context.Files.FindAsync(id);

            //delete image from wwwroot/image
            var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "image", fileModel.FileName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
            //delete the record
            _context.Files.Remove(fileModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            if (fileModel != null)
            {
                _context.Files.Remove(fileModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileModelExists(int id)
        {
          return (_context.Files?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
