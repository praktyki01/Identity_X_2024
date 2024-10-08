using Identity_X_2024.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Identity_X_2024.Controllers
{
    public class HomeController : Controller
    {
        //tworz¹c projekt Typ uwierzytelniania: Pojedyñcze konta
        //Dodaæ klasê Uzytkownik i na samym dodle dodaæ pamiêtaj¹ o koncencji w nazwie 
        //public string UzytkownikUserId { get; set; }
        //public IdentityUser? UzytkownikUser { get; set; }
        //pozosta³e pola musz¹ mieæ pytajnik, czyli nie byæ wymagane
        //klikamy prawym klawiszem myszy na nazwie projektu i dodajemy 
        //nowy element szkieletowy -> To¿samosc
        //dodajemy kontroler UzytkownikControllers z widokami
        //edytujemy plik Register.cshtml.cs
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
