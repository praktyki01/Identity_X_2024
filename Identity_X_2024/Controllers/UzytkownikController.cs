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
    //dodanie poniższego anatation 
    //wymaga zalogowania się aby korzystać z poniższych metod
    [Authorize]
    public class UzytkownikController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UzytkownikController(ApplicationDbContext context
            ,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Uzytkownik
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Uzytkownik.Include(u => u.UzytkownikUser);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> IndexUzytkownik()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var applicationDbContext = _context.Uzytkownik.
                Include(u => u.UzytkownikUser).Where(u=>u.UzytkownikUserId==user.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Uzytkownik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uzytkownik = await _context.Uzytkownik
                .Include(u => u.UzytkownikUser)
                .FirstOrDefaultAsync(m => m.UzytkownikId == id);
            if (uzytkownik == null)
            {
                return NotFound();
            }

            return View(uzytkownik);
        }
        [Authorize(Roles = "Admin")]
        // GET: Uzytkownik/Create
        public IActionResult Create()
        {
            ViewData["UzytkownikUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Uzytkownik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("UzytkownikId,Imie,Nazwisko,Wzrost,Plec,UzytkownikUserId")] Uzytkownik uzytkownik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uzytkownik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UzytkownikUserId"] = new SelectList(_context.Users, "Id", "Id", uzytkownik.UzytkownikUserId);
            return View(uzytkownik);
        }

        // GET: Uzytkownik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }            
            var uzytkownik = await _context.Uzytkownik.FindAsync(id);
            if (uzytkownik == null )
            {
                return NotFound();
            }

            //zablokowanie możlwiwości edycji innego rekordu niż zalogowany użytkownik
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user.Id!=uzytkownik.UzytkownikUserId)
            {
                return RedirectToAction("IndexUzytkownik");
            }
            //
            ViewData["UzytkownikUserId"] = new SelectList(_context.Users, "Id", "Id", uzytkownik.UzytkownikUserId);
            return View(uzytkownik);
        }

        // POST: Uzytkownik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UzytkownikId,Imie,Nazwisko,Wzrost,Plec,UzytkownikUserId")] Uzytkownik uzytkownik)
        {
            if (id != uzytkownik.UzytkownikId)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    //zablokowanie możlwiwości edycji innego rekordu niż zalogowany użytkownik
                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    if (user.Id != uzytkownik.UzytkownikUserId)
                    {
                        return RedirectToAction("IndexUzytkownik");
                    }
                    //
                    _context.Update(uzytkownik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UzytkownikExists(uzytkownik.UzytkownikId))
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
            ViewData["UzytkownikUserId"] = new SelectList(_context.Users, "Id", "Id", uzytkownik.UzytkownikUserId);
            return View(uzytkownik);
        }
        //ograniczenie do roli Admin
        [Authorize(Roles ="Admin")]
        // GET: Uzytkownik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uzytkownik = await _context.Uzytkownik
                .Include(u => u.UzytkownikUser)
                .FirstOrDefaultAsync(m => m.UzytkownikId == id);
            if (uzytkownik == null)
            {
                return NotFound();
            }

            return View(uzytkownik);
        }
        [Authorize(Roles = "Admin")]
        // POST: Uzytkownik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uzytkownik = await _context.Uzytkownik.FindAsync(id);
            if (uzytkownik != null)
            {
                _context.Uzytkownik.Remove(uzytkownik);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UzytkownikExists(int id)
        {
            return _context.Uzytkownik.Any(e => e.UzytkownikId == id);
        }
    }
}
