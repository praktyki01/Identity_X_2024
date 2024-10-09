using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Identity_X_2024.Data;
using Identity_X_2024.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Identity_X_2024.Controllers
{
    [Authorize]
    public class TreningController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TreningController(ApplicationDbContext context
             , UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Trening
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var uzytkownik = _context.Uzytkownik.Where(u => u.UzytkownikUserId == user.Id).FirstOrDefault();
            var applicationDbContext = _context.Trening.Include(t => t.Sport).Include(t => t.Uzytkownik).Where(t=>t.UzytkownikId==uzytkownik.UzytkownikId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Trening/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trening = await _context.Trening
                .Include(t => t.Sport)
                .Include(t => t.Uzytkownik)
                .FirstOrDefaultAsync(m => m.TreningId == id);
            if (trening == null)
            {
                return NotFound();
            }

            // odczytanie zalogowanego użytkownika
            var user = await _userManager.GetUserAsync(HttpContext.User);
            //odczytanie zalogowanego użytkownika w tabeli Uzytkownicy
            var uzytkownik = _context.Uzytkownik.
                Where(u => u.UzytkownikUserId == user.Id).FirstOrDefault();
            //sprawdzenie, czy edytujemy rekord zalogowanego użytkownika
            if (uzytkownik != null && trening.UzytkownikId != uzytkownik.UzytkownikId)
            {
                return RedirectToAction("Index");
            }

            return View(trening);
        }

        // GET: Trening/Create
        public IActionResult Create()
        {
            ViewData["SportId"] = new SelectList(_context.Sport, "SportId", "Nazwa");           
            return View();
        }

        // POST: Trening/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TreningId,UzytkownikId,SportId,Dystans,Data,Czas")] Trening trening)
        {
            if (ModelState.IsValid)
            {
                //odczytanie zalogowanego użytkownika
                var user = await _userManager.GetUserAsync(HttpContext.User);
                //odczytanie zalogowanego użytkownika w tabeli Uzytkownicy
                var uzytkownik = _context.Uzytkownik.
                    Where(u => u.UzytkownikUserId == user.Id).FirstOrDefault();
                trening.UzytkownikId = uzytkownik.UzytkownikId;

                _context.Add(trening);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SportId"] = new SelectList(_context.Sport, "SportId", "Nazwa", trening.SportId);
            return View(trening);
        }

        // GET: Trening/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trening = await _context.Trening.FindAsync(id);
            if (trening == null)
            {
                return NotFound();
            }

            //odczytanie zalogowanego użytkownika
            var user = await _userManager.GetUserAsync(HttpContext.User);
            //odczytanie zalogowanego użytkownika w tabeli Uzytkownicy
            var uzytkownik = _context.Uzytkownik.
                Where(u => u.UzytkownikUserId == user.Id).FirstOrDefault();
            //sprawdzenie, czy edytujemy rekord zalogowanego użytkownika
            if (uzytkownik != null && trening.UzytkownikId != uzytkownik.UzytkownikId)
            {
                return RedirectToAction("Index");
            }

            ViewData["SportId"] = new SelectList(_context.Sport, "SportId", "Nazwa", trening.SportId);
            return View(trening);
        }

        // POST: Trening/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TreningId,UzytkownikId,SportId,Dystans,Data,Czas")] Trening trening)
        {
            if (id != trening.TreningId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //odczytanie zalogowanego użytkownika
                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    //odczytanie zalogowanego użytkownika w tabeli Uzytkownicy
                    var uzytkownik = _context.Uzytkownik.
                        Where(u => u.UzytkownikUserId == user.Id).FirstOrDefault();
                    if (uzytkownik != null)
                    {
                        trening.UzytkownikId = uzytkownik.UzytkownikId;
                        _context.Update(trening);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreningExists(trening.TreningId))
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
            ViewData["SportId"] = new SelectList(_context.Sport, "SportId", "Nazwa", trening.SportId);
            return View(trening);
        }

        // GET: Trening/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trening = await _context.Trening
                .Include(t => t.Sport)
                .Include(t => t.Uzytkownik)
                .FirstOrDefaultAsync(m => m.TreningId == id);
            if (trening == null)
            {
                return NotFound();
            }

            //odczytanie zalogowanego użytkownika
            var user = await _userManager.GetUserAsync(HttpContext.User);
            //odczytanie zalogowanego użytkownika w tabeli Uzytkownicy
            var uzytkownik = _context.Uzytkownik.
                Where(u => u.UzytkownikUserId == user.Id).FirstOrDefault();
            //sprawdzenie, czy edytujemy rekord zalogowanego użytkownika
            if (uzytkownik != null && trening.UzytkownikId != uzytkownik.UzytkownikId)
            {
                return RedirectToAction("Index");
            }

            return View(trening);
        }

        // POST: Trening/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trening = await _context.Trening.FindAsync(id);
            if (trening != null)
            {
                //odczytanie zalogowanego użytkownika
                var user = await _userManager.GetUserAsync(HttpContext.User);
                //odczytanie zalogowanego użytkownika w tabeli Uzytkownicy
                var uzytkownik = _context.Uzytkownik.
                    Where(u => u.UzytkownikUserId == user.Id).FirstOrDefault();
                //sprawdzenie, czy edytujemy rekord zalogowanego użytkownika
                if (uzytkownik != null && trening.UzytkownikId != uzytkownik.UzytkownikId)
                {
                    return RedirectToAction("Index");
                }

                _context.Trening.Remove(trening);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TreningExists(int id)
        {
            return _context.Trening.Any(e => e.TreningId == id);
        }        
    }
}
