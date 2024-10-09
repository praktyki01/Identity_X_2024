using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Identity_X_2024.Data;
using Identity_X_2024.Models;

namespace Identity_X_2024.Controllers
{
    public class SportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sport
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sport.ToListAsync());
        }

        // GET: Sport/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sport = await _context.Sport
                .FirstOrDefaultAsync(m => m.SportId == id);
            if (sport == null)
            {
                return NotFound();
            }

            return View(sport);
        }

        // GET: Sport/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sport/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SportId,Nazwa,ImgUrl")] Sport sport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sport);
        }

        // GET: Sport/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sport = await _context.Sport.FindAsync(id);
            if (sport == null)
            {
                return NotFound();
            }
            return View(sport);
        }

        // POST: Sport/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SportId,Nazwa,ImgUrl")] Sport sport)
        {
            if (id != sport.SportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SportExists(sport.SportId))
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
            return View(sport);
        }

        // GET: Sport/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sport = await _context.Sport
                .FirstOrDefaultAsync(m => m.SportId == id);
            if (sport == null)
            {
                return NotFound();
            }

            return View(sport);
        }

        // POST: Sport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sport = await _context.Sport.FindAsync(id);
            if (sport != null)
            {
                _context.Sport.Remove(sport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SportExists(int id)
        {
            return _context.Sport.Any(e => e.SportId == id);
        }
    }
}
