using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class EducationModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EducationModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EducationModels
        public async Task<IActionResult> Index()
        {
            if (_context.Education != null)
            {
                var education = await _context.Education.ToListAsync();
                ViewBag.Edu = education;
                return View(education);
            }
            else
            {
                return Problem("Entity set 'ApplicationDbContext.Projects'  is null.");
            }

            return View();
        }

        // GET: EducationModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Education == null)
            {
                return NotFound();
            }

            var educationModel = await _context.Education
                .FirstOrDefaultAsync(m => m.ID == id);
            if (educationModel == null)
            {
                return NotFound();
            }

            return View(educationModel);
        }

        // GET: EducationModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EducationModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,School,Credits,Description,YearStart,YearEnd")] EducationModel educationModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(educationModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(educationModel);
        }

        // GET: EducationModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Education == null)
            {
                return NotFound();
            }

            var educationModel = await _context.Education.FindAsync(id);
            if (educationModel == null)
            {
                return NotFound();
            }
            return View(educationModel);
        }

        // POST: EducationModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,School,Credits,Description,YearStart,YearEnd")] EducationModel educationModel)
        {
            if (id != educationModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(educationModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationModelExists(educationModel.ID))
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
            return View(educationModel);
        }

        // GET: EducationModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Education == null)
            {
                return NotFound();
            }

            var educationModel = await _context.Education
                .FirstOrDefaultAsync(m => m.ID == id);
            if (educationModel == null)
            {
                return NotFound();
            }

            return View(educationModel);
        }

        // POST: EducationModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Education == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Education'  is null.");
            }
            var educationModel = await _context.Education.FindAsync(id);
            if (educationModel != null)
            {
                _context.Education.Remove(educationModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationModelExists(int id)
        {
          return (_context.Education?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
