using System.ComponentModel.DataAnnotations;

namespace Identity_X_2024.Models
{
    public class Waga
    {
        public int WagaId { get; set; }
        public DateTime Data { get; set; }
        public float Wartosc { get; set; }

        [Display(Name = "Uzytkownik")]
        public int? UzytkownikId { get; set; }
        public Uzytkownik? Uzytkownik { get; set; } = null!;
    }
}
