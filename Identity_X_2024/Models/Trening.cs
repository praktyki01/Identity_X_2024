using System.ComponentModel.DataAnnotations;

namespace Identity_X_2024.Models
{
    public class Trening
    {
        public int TreningId { get; set; }

        [Display(Name = "Uzytkownik")]
        public int? UzytkownikId { get; set; }
        public Uzytkownik? Uzytkownik { get; set; } = null!;

        [Display(Name = "Sport")]
        public int? SportId { get; set; }
        public Sport? Sport { get; set; } = null!;

        public float Dystans { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Czas { get; set; }
    }
}
