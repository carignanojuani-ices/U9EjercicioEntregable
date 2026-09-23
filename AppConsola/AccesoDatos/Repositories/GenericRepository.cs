using AccesoDatos.Data;
using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        // METODOS GENERICOS.
        protected readonly ApplicationDbContext _context;

        public GenericRepository()
        {
            _context = new ApplicationDbContext();
        }

        public void Agregar(T entidad)
        {
            _context.Set<T>().Add(entidad);
            _context.SaveChanges();
        }

        public List<T> ObtenerTodos()
        {
            return _context.Set<T>()
                           .AsNoTracking()
                           .ToList();
        }

        public List<T> ObtenerTodosCon(string propiedadRelacionada)
        {
            return _context.Set<T>()
                           .Include(propiedadRelacionada)
                           .AsNoTracking()
                           .ToList();
        }

        public T ObtenerPorId(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Modificar(T entidad)
        {
            _context.Set<T>().Update(entidad);
            _context.SaveChanges();
        }

        public void Eliminar(object id)
        {
            var entidad = _context.Set<T>().Find(id);

            if (entidad != null)
            {
                _context.Set<T>().Remove(entidad);
                _context.SaveChanges();
            }
        }

    }
}