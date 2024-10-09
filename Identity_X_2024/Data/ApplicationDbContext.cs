using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Identity_X_2024.Models;

namespace Identity_X_2024.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Identity_X_2024.Models.Uzytkownik> Uzytkownik { get; set; } = default!;
        public DbSet<Identity_X_2024.Models.Sport> Sport { get; set; } = default!;
        public DbSet<Identity_X_2024.Models.Trening> Trening { get; set; } = default!;
        public DbSet<Identity_X_2024.Models.Waga> Waga { get; set; } = default!;
    }
}
