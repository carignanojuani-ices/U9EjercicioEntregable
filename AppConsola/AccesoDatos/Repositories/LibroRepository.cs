using AccesoDatos.Models;
using System.Collections.Generic;
using System.Linq;

namespace AccesoDatos.Repositories
{
    public class LibroRepository : GenericRepository<Libro>
    {
        // Mostrar libros ordenados por año de publicación (más recientes primero).
        public List<Libro> ObtenerLibrosPorMasRecientes()
        {
            return _context.Libro
                           .OrderByDescending(l => l.AnioPublicacion)
                           .ToList();
        }

        // Cantidad total de libros registrados.
        public int ObtenerCantidadLibros()
        {
            return _context.Libro
                           .Count();
        }

        // Cantidad de libros activos.
        public int ObtenerCantidadLibrosActivos()
        {
            return _context.Libro
                           .Count(l => l.Activo);
        }

        // Buscar libro por ID.
        public Libro? ObtenerLibroPorId(int id)
        {
            return _context.Libro
                           .FirstOrDefault(l => l.Id == id);
        }

        // Mostrar libros ordenados por título.
        public List<Libro> ObtenerLibrosOrdenadosPorTitulo()
        {
            return _context.Libro
                           .OrderBy(l => l.Titulo)
                           .ToList();
        }

        // Verificar si existen libros activos.
        public bool ExistenLibrosActivos()
        {
            return _context.Libro
                           .Any(l => l.Activo);
        }
    }
}