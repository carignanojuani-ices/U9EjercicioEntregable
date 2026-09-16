using Microsoft.EntityFrameworkCore;
using AccesoDatos.Models;

namespace AccesoDatos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Autor> Autor { get; set; }
        public DbSet<Libro> Libro { get; set; }
        public DbSet<Categoria> Categoria { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\databases\\BaseDatosEjercicios.db");
        }
    }
}
