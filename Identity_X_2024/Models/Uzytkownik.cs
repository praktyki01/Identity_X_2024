using Microsoft.AspNetCore.Identity;

namespace Identity_X_2024.Models
{
    public class Uzytkownik
    {
        public int UzytkownikId { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int Wzrost { get; set; }
        public string Plec { get; set; }
        public string UzytkownikUserId { get; set; }
        public IdentityUser? UzytkownikUser { get; set; }

    }
}
