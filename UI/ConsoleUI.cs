using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.UI;

/// <summary>
/// Interfaz de usuario de consola para la aplicación de tareas
/// </summary>
public class ConsoleUI
{
    private readonly TareaService _service;
    private FiltroTareas _filtroActual = FiltroTareas.Todas;
    private OrdenTareas _ordenActual = OrdenTareas.FechaCreacion;
    
    /// <summary>
    /// Constructor con inyección de dependencias
    /// </summary>
    public ConsoleUI(TareaService service)
    {
        _service = service;
    }
    
    /// <summary>
    /// Ejecuta el loop principal de la aplicación
    /// </summary>
    public void Ejecutar()
    {
        LimpiarConsola();
        
        while (true)
        {
            MostrarMenuPrincipal();
            string? opcion = Console.ReadLine();
            
            switch (opcion)
            {
                case "1":
                    AgregarTarea();
                    break;
                case "2":
                    VerTareas();
                    break;
                case "3":
                    CambiarEstadoTarea();
                    break;
                case "4":
                    EliminarTarea();
                    break;
                case "5":
                    Console.WriteLine("\n¡Hasta luego!");
                    return;
                default:
                    Console.WriteLine("\nOpción no válida. Seleccione 1-5");
                    EsperarTecla();
                    break;
            }
        }
    }
    
    /// <summary>
    /// Muestra el menú principal
    /// </summary>
    private void MostrarMenuPrincipal()
    {
        Console.WriteLine("\n╔════════════════════════════════════╗");
        Console.WriteLine("║     GESTOR DE TAREAS - SQLite      ║");
        Console.WriteLine("╚════════════════════════════════════╝");
        Console.WriteLine("\n1. Agregar tarea");
        Console.WriteLine("2. Ver tareas");
        Console.WriteLine("3. Marcar tarea como completada/pendiente");
        Console.WriteLine("4. Eliminar tarea");
        Console.WriteLine("5. Salir");
        Console.Write("\nSeleccione una opción: ");
    }
    
    /// <summary>
    /// Formulario para agregar una nueva tarea
    /// </summary>
    private void AgregarTarea()
    {
        LimpiarConsola();
        Console.WriteLine("═══ AGREGAR NUEVA TAREA ═══\n");
        
        Console.Write("Descripción: ");
        string? descripcion = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(descripcion))
        {
            Console.WriteLine("\n❌ La tarea no puede estar vacía");
            EsperarTecla();
            return;
        }
        
        Console.WriteLine("\nPrioridad:");
        Console.WriteLine("1. Baja");
        Console.WriteLine("2. Media (predeterminada)");
        Console.WriteLine("3. Alta");
        Console.Write("\nSeleccione prioridad (1-3): ");
        
        string? prioridadInput = Console.ReadLine();
        Prioridad prioridad = prioridadInput switch
        {
            "1" => Prioridad.Baja,
            "3" => Prioridad.Alta,
            _ => Prioridad.Media
        };
        
        try
        {
            int id = _service.CrearTarea(descripcion, prioridad);
            Console.WriteLine($"\n✓ Tarea agregada exitosamente (ID: {id})");
            Console.WriteLine($"  Prioridad: {ObtenerNombrePrioridad(prioridad)}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"\n❌ Error: {ex.Message}");
        }
        
        EsperarTecla();
    }
    
    /// <summary>
    /// Muestra la lista de tareas con opciones de filtrado y ordenamiento
    /// </summary>
    private void VerTareas()
    {
        LimpiarConsola();
        Console.WriteLine("═══ LISTA DE TAREAS ═══\n");
        
        // Opciones de filtro
        Console.WriteLine("Filtro actual: " + ObtenerNombreFiltro(_filtroActual));
        Console.WriteLine("Orden actual: " + ObtenerNombreOrden(_ordenActual));
        Console.WriteLine("\n[F] Cambiar filtro | [O] Cambiar orden | [Enter] Ver con configuración actual");
        Console.Write("\nOpción: ");
        
        string? opcion = Console.ReadLine()?.ToUpper();
        
        if (opcion == "F")
        {
            MostrarOpcionesFiltro();
            return;
        }
        else if (opcion == "O")
        {
            MostrarOpcionesOrdenamiento();
            return;
        }
        
        // Obtener y mostrar tareas
        var tareas = _service.ObtenerTareas(_filtroActual, _ordenActual);
        
        LimpiarConsola();
        Console.WriteLine("═══ LISTA DE TAREAS ═══\n");
        Console.WriteLine($"Filtro: {ObtenerNombreFiltro(_filtroActual)} | Orden: {ObtenerNombreOrden(_ordenActual)}\n");
        
        if (tareas.Count == 0)
        {
            Console.WriteLine("No hay tareas para mostrar");
        }
        else
        {
            MostrarListaTareas(tareas);
        }
        
        EsperarTecla();
    }
    
    /// <summary>
    /// Muestra opciones de filtrado
    /// </summary>
    private void MostrarOpcionesFiltro()
    {
        LimpiarConsola();
        Console.WriteLine("═══ FILTRO DE TAREAS ═══\n");
        Console.WriteLine("1. Todas");
        Console.WriteLine("2. Pendientes");
        Console.WriteLine("3. Completadas");
        Console.Write("\nSeleccione filtro (1-3): ");
        
        string? opcion = Console.ReadLine();
        
        _filtroActual = opcion switch
        {
            "2" => FiltroTareas.Pendientes,
            "3" => FiltroTareas.Completadas,
            _ => FiltroTareas.Todas
        };
        
        Console.WriteLine($"\n✓ Filtro cambiado a: {ObtenerNombreFiltro(_filtroActual)}");
        EsperarTecla();
        VerTareas();
    }
    
    /// <summary>
    /// Muestra opciones de ordenamiento
    /// </summary>
    private void MostrarOpcionesOrdenamiento()
    {
        LimpiarConsola();
        Console.WriteLine("═══ ORDENAMIENTO DE TAREAS ═══\n");
        Console.WriteLine("1. Fecha de creación (más reciente primero)");
        Console.WriteLine("2. Prioridad (alta a baja)");
        Console.WriteLine("3. Estado (pendientes primero)");
        Console.Write("\nSeleccione orden (1-3): ");
        
        string? opcion = Console.ReadLine();
        
        _ordenActual = opcion switch
        {
            "2" => OrdenTareas.Prioridad,
            "3" => OrdenTareas.Estado,
            _ => OrdenTareas.FechaCreacion
        };
        
        Console.WriteLine($"\n✓ Orden cambiado a: {ObtenerNombreOrden(_ordenActual)}");
        EsperarTecla();
        VerTareas();
    }
    
    /// <summary>
    /// Muestra la lista de tareas con formato y metadata
    /// </summary>
    private void MostrarListaTareas(List<Tarea> tareas)
    {
        for (int i = 0; i < tareas.Count; i++)
        {
            var tarea = tareas[i];
            
            // Símbolo de completado
            string simbolo = tarea.Completada ? "✓" : " ";
            
            // Color según prioridad
            ConsoleColor colorOriginal = Console.ForegroundColor;
            Console.ForegroundColor = ObtenerColorPrioridad(tarea.Prioridad);
            
            // Línea principal
            Console.Write($"[{simbolo}] {tarea.Id}. ");
            Console.ForegroundColor = colorOriginal;
            
            if (tarea.Completada)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            
            Console.WriteLine(tarea.Descripcion);
            
            // Metadata
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"    Prioridad: {ObtenerNombrePrioridad(tarea.Prioridad)} | ");
            Console.Write($"Creada: {FormatearFecha(tarea.FechaCreacion)}");
            
            if (tarea.Completada && tarea.FechaCompletada.HasValue)
            {
                Console.Write($" | Completada: {FormatearFecha(tarea.FechaCompletada.Value)}");
            }
            
            Console.WriteLine();
            Console.ForegroundColor = colorOriginal;
            Console.WriteLine();
        }
    }
    
    /// <summary>
    /// Cambia el estado de una tarea (completada/pendiente)
    /// </summary>
    private void CambiarEstadoTarea()
    {
        LimpiarConsola();
        Console.WriteLine("═══ CAMBIAR ESTADO DE TAREA ═══\n");
        
        var tareas = _service.ObtenerTareas(FiltroTareas.Todas, OrdenTareas.FechaCreacion);
        
        if (tareas.Count == 0)
        {
            Console.WriteLine("No hay tareas disponibles");
            EsperarTecla();
            return;
        }
        
        MostrarListaTareas(tareas);
        
        Console.Write("Ingrese el ID de la tarea: ");
        string? entrada = Console.ReadLine();
        
        if (!int.TryParse(entrada, out int id))
        {
            Console.WriteLine("\n❌ ID inválido");
            EsperarTecla();
            return;
        }
        
        try
        {
            _service.CambiarEstadoTarea(id);
            Console.WriteLine("\n✓ Estado de la tarea actualizado");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"\n❌ Error: {ex.Message}");
        }
        
        EsperarTecla();
    }
    
    /// <summary>
    /// Elimina una tarea con confirmación
    /// </summary>
    private void EliminarTarea()
    {
        LimpiarConsola();
        Console.WriteLine("═══ ELIMINAR TAREA ═══\n");
        
        var tareas = _service.ObtenerTareas(FiltroTareas.Todas, OrdenTareas.FechaCreacion);
        
        if (tareas.Count == 0)
        {
            Console.WriteLine("No hay tareas para eliminar");
            EsperarTecla();
            return;
        }
        
        MostrarListaTareas(tareas);
        
        Console.Write("Ingrese el ID de la tarea a eliminar: ");
        string? entrada = Console.ReadLine();
        
        if (!int.TryParse(entrada, out int id))
        {
            Console.WriteLine("\n❌ ID inválido");
            EsperarTecla();
            return;
        }
        
        var tarea = tareas.FirstOrDefault(t => t.Id == id);
        if (tarea == null)
        {
            Console.WriteLine("\n❌ Tarea no encontrada");
            EsperarTecla();
            return;
        }
        
        Console.Write($"\n¿Está seguro de eliminar la tarea \"{tarea.Descripcion}\"? (s/n): ");
        string? confirmacion = Console.ReadLine()?.ToLower();
        
        if (confirmacion == "s")
        {
            try
            {
                _service.EliminarTarea(id);
                Console.WriteLine("\n✓ Tarea eliminada exitosamente");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\n❌ Error: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("\n❌ Eliminación cancelada");
        }
        
        EsperarTecla();
    }
    
    // ═══ MÉTODOS AUXILIARES ═══
    
    private void LimpiarConsola()
    {
        Console.Clear();
        Console.Write("\x1b[3J");
        Console.SetCursorPosition(0, 0);
    }
    
    private void EsperarTecla()
    {
        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey(true);
        LimpiarConsola();
    }
    
    private string FormatearFecha(DateTime fecha)
    {
        return fecha.ToString("dd/MM/yyyy HH:mm");
    }
    
    private string ObtenerNombrePrioridad(Prioridad prioridad)
    {
        return prioridad switch
        {
            Prioridad.Baja => "Baja",
            Prioridad.Alta => "Alta",
            _ => "Media"
        };
    }
    
    private ConsoleColor ObtenerColorPrioridad(Prioridad prioridad)
    {
        return prioridad switch
        {
            Prioridad.Alta => ConsoleColor.Red,
            Prioridad.Media => ConsoleColor.Yellow,
            Prioridad.Baja => ConsoleColor.Green,
            _ => ConsoleColor.White
        };
    }
    
    private string ObtenerNombreFiltro(FiltroTareas filtro)
    {
        return filtro switch
        {
            FiltroTareas.Pendientes => "Pendientes",
            FiltroTareas.Completadas => "Completadas",
            _ => "Todas"
        };
    }
    
    private string ObtenerNombreOrden(OrdenTareas orden)
    {
        return orden switch
        {
            OrdenTareas.Prioridad => "Prioridad",
            OrdenTareas.Estado => "Estado",
            _ => "Fecha de creación"
        };
    }
}
