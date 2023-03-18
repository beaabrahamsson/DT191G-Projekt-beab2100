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
    public class ResumeModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResumeModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ResumeModels
        public async Task<IActionResult> Index()
        {

            if (_context.CV != null)
            {
                var resume = await _context.CV.ToListAsync();
                ViewBag.CV = resume;
                return View(resume);
            }
            else
            {
                return Problem("Entity set 'ApplicationDbContext.Projects'  is null.");
            }

            return View();
        }

        // GET: ResumeModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CV == null)
            {
                return NotFound();
            }

            var resumeModel = await _context.CV
                .FirstOrDefaultAsync(m => m.ID == id);
            if (resumeModel == null)
            {
                return NotFound();
            }

            return View(resumeModel);
        }

        // GET: ResumeModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResumeModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,JobTitle,Company,Description,YearStart,YearEnd,IsOngoing")] ResumeModel resumeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resumeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resumeModel);
        }

        // GET: ResumeModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CV == null)
            {
                return NotFound();
            }

            var resumeModel = await _context.CV.FindAsync(id);
            if (resumeModel == null)
            {
                return NotFound();
            }
            return View(resumeModel);
        }

        // POST: ResumeModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,JobTitle,Company,Description,YearStart,YearEnd,IsOngoing")] ResumeModel resumeModel)
        {
            if (id != resumeModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resumeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResumeModelExists(resumeModel.ID))
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
            return View(resumeModel);
        }

        // GET: ResumeModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CV == null)
            {
                return NotFound();
            }

            var resumeModel = await _context.CV
                .FirstOrDefaultAsync(m => m.ID == id);
            if (resumeModel == null)
            {
                return NotFound();
            }

            return View(resumeModel);
        }

        // POST: ResumeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CV == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CV'  is null.");
            }
            var resumeModel = await _context.CV.FindAsync(id);
            if (resumeModel != null)
            {
                _context.CV.Remove(resumeModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResumeModelExists(int id)
        {
          return (_context.CV?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
