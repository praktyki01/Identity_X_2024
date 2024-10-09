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
    public class WagaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public WagaController(ApplicationDbContext context
            , UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Waga
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var uzytkownik = _context.Uzytkownik.Where(u => u.UzytkownikUserId == user.Id).FirstOrDefault();
            var applicationDbContext = _context.Waga.
                Include(w => w.Uzytkownik).Where(u=>u.UzytkownikId==uzytkownik.UzytkownikId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Waga/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var waga = await _context.Waga
                .Include(w => w.Uzytkownik)
                .FirstOrDefaultAsync(m => m.WagaId == id);
            if (waga == null)
            {
                return NotFound();
            }

            //odczytanie zalogowanego użytkownika
            var user = await _userManager.GetUserAsync(HttpContext.User);
            //odczytanie zalogowanego użytkownika w tabeli Uzytkownicy
            var uzytkownik = _context.Uzytkownik.
                Where(u => u.UzytkownikUserId == user.Id).FirstOrDefault();
            //sprawdzenie, czy edytujemy rekord zalogowanego użytkownika
            if (uzytkownik != null && waga.UzytkownikId != uzytkownik.UzytkownikId)
            {
                return RedirectToAction("Index");
            }

            return View(waga);
        }

        // GET: Waga/Create
        public IActionResult Create()
        {
            ViewData["UzytkownikId"] = new SelectList(_context.Uzytkownik, "UzytkownikId", "UzytkownikId");
            return View();
        }

        // POST: Waga/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WagaId,Data,Wartosc,UzytkownikId")] Waga waga)
        {
            if (ModelState.IsValid)
            {
                //odczytanie zalogowanego użytkownika
                var user = await _userManager.GetUserAsync(HttpContext.User);
                //odczytanie zalogowanego użytkownika w tabeli Uzytkownicy
                var uzytkownik=_context.Uzytkownik.
                    Where(u=>u.UzytkownikUserId==user.Id).FirstOrDefault();
                if (uzytkownik != null)
                {
                    waga.UzytkownikId = uzytkownik.UzytkownikId;
                    _context.Add(waga);
                    await _context.SaveChangesAsync();
                }
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["UzytkownikId"] = new SelectList(_context.Uzytkownik, "UzytkownikId", "UzytkownikId", waga.UzytkownikId);
            return View(waga);
        }

        // GET: Waga/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
                                 
            var waga = await _context.Waga.FindAsync(id);
            if (waga == null)
            {
                return NotFound();
            }

            //odczytanie zalogowanego użytkownika
            var user = await _userManager.GetUserAsync(HttpContext.User);
            //odczytanie zalogowanego użytkownika w tabeli Uzytkownicy
            var uzytkownik = _context.Uzytkownik.
                Where(u => u.UzytkownikUserId == user.Id).FirstOrDefault();
            //sprawdzenie, czy edytujemy rekord zalogowanego użytkownika
            if (uzytkownik!=null && waga.UzytkownikId!=uzytkownik.UzytkownikId)
            {
                return RedirectToAction("Index");
            }
            ViewData["UzytkownikId"] = new SelectList(_context.Uzytkownik, "UzytkownikId", "UzytkownikId", waga.UzytkownikId);
            return View(waga);
        }

        // POST: Waga/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WagaId,Data,Wartosc,UzytkownikId")] Waga waga)
        {
            if (id != waga.WagaId)
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
                    if(uzytkownik!=null)
                    {
                        waga.UzytkownikId = uzytkownik.UzytkownikId;
                        _context.Update(waga);
                    }
                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WagaExists(waga.WagaId))
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
            ViewData["UzytkownikId"] = new SelectList(_context.Uzytkownik, "UzytkownikId", "UzytkownikId", waga.UzytkownikId);
            return View(waga);
        }

        // GET: Waga/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waga = await _context.Waga
                .Include(w => w.Uzytkownik)
                .FirstOrDefaultAsync(m => m.WagaId == id);
            if (waga == null)
            {
                return NotFound();
            }

            //odczytanie zalogowanego użytkownika
            var user = await _userManager.GetUserAsync(HttpContext.User);
            //odczytanie zalogowanego użytkownika w tabeli Uzytkownicy
            var uzytkownik = _context.Uzytkownik.
                Where(u => u.UzytkownikUserId == user.Id).FirstOrDefault();
            //sprawdzenie, czy edytujemy rekord zalogowanego użytkownika
            if (uzytkownik != null && waga.UzytkownikId != uzytkownik.UzytkownikId)
            {
                return RedirectToAction("Index");
            }

            return View(waga);
        }

        // POST: Waga/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var waga = await _context.Waga.FindAsync(id);
            if (waga != null)
            {
                //odczytanie zalogowanego użytkownika
                var user = await _userManager.GetUserAsync(HttpContext.User);
                //odczytanie zalogowanego użytkownika w tabeli Uzytkownicy
                var uzytkownik = _context.Uzytkownik.
                    Where(u => u.UzytkownikUserId == user.Id).FirstOrDefault();
                //sprawdzenie, czy edytujemy rekord zalogowanego użytkownika
                if (uzytkownik != null && waga.UzytkownikId != uzytkownik.UzytkownikId)
                {
                    return RedirectToAction("Index");
                }

                _context.Waga.Remove(waga);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WagaExists(int id)
        {
            return _context.Waga.Any(e => e.WagaId == id);
        }
    }
}
