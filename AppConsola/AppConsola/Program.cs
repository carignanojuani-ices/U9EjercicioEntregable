using AccesoDatos.Models;
using AccesoDatos.Repositories;

IGenericRepository<Autor> autorRepository = new GenericRepository<Autor>();
IGenericRepository<Categoria> categoriaRepository = new GenericRepository<Categoria>();
IGenericRepository<Libro> libroRepository = new GenericRepository<Libro>();

bool continuar = true;

while (continuar)
{
    Console.WriteLine("================================");
    Console.WriteLine(" SISTEMA DE BIBLIOTECA ");
    Console.WriteLine("================================");
    Console.WriteLine("1. Alta Autor");
    Console.WriteLine("2. Alta Categoría");
    Console.WriteLine("3. Alta Libro");
    Console.WriteLine("4. Ver Autores");
    Console.WriteLine("5. Ver Categorías");
    Console.WriteLine("6. Ver Libros");
    Console.WriteLine("7. Modificar Libro");
    Console.WriteLine("8. Eliminar Libro");
    Console.WriteLine("9. Modificar Autor");
    Console.WriteLine("0. Salir");
    Console.WriteLine();

    Console.Write("Seleccione una opción: ");
    string opcion = Console.ReadLine();

    Console.Clear();

    switch (opcion)
    {
        case "1":
            AltaAutor();
            break;

        case "2":
            AltaCategoria();
            break;

        case "3":
            AltaLibro();
            break;

        case "4":
            MostrarAutores();
            break;

        case "5":
            MostrarCategorias();
            break;

        case "6":
            MostrarLibros();
            break;

        case "7":
            ModificarLibro();
            break;

        case "8":
            EliminarLibro();
            break;

        case "9":
            ModificarAutor();
            break;

        case "0":
            continuar = false;
            Console.WriteLine("Aplicación finalizada.");
            break;

        default:
            Console.WriteLine("Opción inválida.");
            PresioneParaContinuar();
            break;
    }
}

void AltaAutor()
{
    Console.Write("Nombre del autor: ");

    Autor autor = new Autor
    {
        Nombre = Console.ReadLine()
    };

    autorRepository.Agregar(autor);

    Console.WriteLine("Autor registrado correctamente.");

    PresioneParaContinuar();
}

void AltaCategoria()
{
    Console.Write("Nombre de la categoría: ");

    Categoria categoria = new Categoria
    {
        Nombre = Console.ReadLine()
    };

    categoriaRepository.Agregar(categoria);

    Console.WriteLine("Categoría registrada correctamente.");

    PresioneParaContinuar();
}

void AltaLibro()
{
    Console.Write("Título: ");
    string titulo = Console.ReadLine();

    Console.Write("Año publicación: ");
    int anio = int.Parse(Console.ReadLine());

    Console.WriteLine();
    Console.WriteLine("Autores disponibles:");

    foreach (var autor in autorRepository.ObtenerTodos())
    {
        Console.WriteLine(
            $"ID: {autor.Id} - {autor.Nombre}");
    }

    Console.Write("Seleccione el ID del autor: ");
    int autorId = int.Parse(Console.ReadLine());

    Console.WriteLine();
    Console.WriteLine("Categorías disponibles:");

    foreach (var categoria in categoriaRepository.ObtenerTodos())
    {
        Console.WriteLine(
            $"ID: {categoria.Id} - {categoria.Nombre}");
    }

    Console.Write("Seleccione el ID de la categoría: ");
    int categoriaId = int.Parse(Console.ReadLine());

    Libro libro = new Libro
    {
        Titulo = titulo,
        AnioPublicacion = anio,
        AutorId = autorId,
        CategoriaId = categoriaId,
        Activo = true
    };

    libroRepository.Agregar(libro);

    Console.WriteLine("Libro registrado correctamente.");

    PresioneParaContinuar();
}

void MostrarAutores()
{
    Console.WriteLine("===== AUTORES =====");

    var autores = autorRepository.ObtenerTodos();

    foreach (var autor in autores)
    {
        Console.WriteLine(
            $"ID: {autor.Id} | Nombre: {autor.Nombre}");
    }

    PresioneParaContinuar();
}

void MostrarCategorias()
{
    Console.WriteLine("===== CATEGORÍAS =====");

    var categorias = categoriaRepository.ObtenerTodos();

    foreach (var categoria in categorias)
    {
        Console.WriteLine(
            $"ID: {categoria.Id} | Nombre: {categoria.Nombre}");
    }

    PresioneParaContinuar();
}

void MostrarLibros()
{
    Console.WriteLine("===== LISTADO DE LIBROS =====");

    var libros = libroRepository.ObtenerTodosCon("Autor");

    if (!libros.Any())
    {
        Console.WriteLine("No existen libros registrados.");
    }
    else
    {
        foreach (var libro in libros)//.Where(l => l.Activo))
        {
            Console.WriteLine(
                $"ID: {libro.Id} | " +
                $"Título: {libro.Titulo} | " +
                $"Año: {libro.AnioPublicacion} | "); //+
                //$"Autor: {libro.Autor.Nombre}");
        }
    }

    Console.WriteLine("=============================");

    PresioneParaContinuar();
}

void ModificarAutor()
{
    MostrarAutores();

    Console.Write("Ingrese el ID del autor: ");
    int id = int.Parse(Console.ReadLine());

    var autor = autorRepository.ObtenerPorId(id);

    if (autor != null)
    {
        Console.Write("Nuevo nombre: ");
        autor.Nombre = Console.ReadLine();

        autorRepository.Modificar(autor);

        Console.WriteLine("Autor modificado correctamente.");
    }
    else
    {
        Console.WriteLine("Autor no encontrado.");
    }

    PresioneParaContinuar();
}

void ModificarLibro()
{
    MostrarLibros();

    Console.Write("Ingrese el ID del libro: ");
    int id = int.Parse(Console.ReadLine());

    var libro = libroRepository.ObtenerPorId(id);

    if (libro != null)
    {
        Console.Write("Nuevo título: ");
        libro.Titulo = Console.ReadLine();

        libroRepository.Modificar(libro);

        Console.WriteLine("Libro modificado correctamente.");
    }
    else
    {
        Console.WriteLine("Libro no encontrado.");
    }

    PresioneParaContinuar();
}

void EliminarLibro()
{
    MostrarLibros();

    Console.Write("Ingrese el ID del libro: ");
    string idLibroABorrar = Console.ReadLine();

    if (idLibroABorrar == null || idLibroABorrar == "") 
    {
        Console.WriteLine("No fue ingresado ningun ID de libro");
        PresioneParaContinuar();
    } 
    else
    {
        int id = int.Parse(idLibroABorrar);

        var libro = libroRepository.ObtenerPorId(id);

        if (libro != null)
        {
            libro.Activo = false;

            libroRepository.Modificar(libro);

            Console.WriteLine("Libro eliminado lógicamente.");
        }
        else
        {
            Console.WriteLine("Libro no encontrado.");
        }
    }

    PresioneParaContinuar();
}

void PresioneParaContinuar()
{
    Console.WriteLine();
    Console.WriteLine("Presione una tecla para continuar...");
    Console.ReadKey();
    Console.Clear();
}