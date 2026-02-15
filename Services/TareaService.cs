using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Services;

/// <summary>
/// Servicio de lógica de negocio para tareas
/// </summary>
public class TareaService
{
    private readonly TareaRepository _repository;
    
    /// <summary>
    /// Constructor con inyección de dependencias
    /// </summary>
    public TareaService(TareaRepository repository)
    {
        _repository = repository;
    }
    
    /// <summary>
    /// Crea una nueva tarea con validación
    /// </summary>
    public int CrearTarea(string descripcion, Prioridad prioridad = Prioridad.Media)
    {
        // Validación
        if (string.IsNullOrWhiteSpace(descripcion))
        {
            throw new ArgumentException("La descripción no puede estar vacía", nameof(descripcion));
        }
        
        if (descripcion.Length > 500)
        {
            throw new ArgumentException("La descripción no puede exceder 500 caracteres", nameof(descripcion));
        }
        
        var tarea = new Tarea
        {
            Descripcion = descripcion.Trim(),
            FechaCreacion = DateTime.Now,
            Completada = false,
            FechaCompletada = null,
            Prioridad = prioridad
        };
        
        return _repository.Agregar(tarea);
    }
    
    /// <summary>
    /// Obtiene tareas con filtrado y ordenamiento
    /// </summary>
    public List<Tarea> ObtenerTareas(FiltroTareas filtro = FiltroTareas.Todas, OrdenTareas orden = OrdenTareas.FechaCreacion)
    {
        List<Tarea> tareas;
        
        // Aplicar filtro
        switch (filtro)
        {
            case FiltroTareas.Pendientes:
                tareas = _repository.ObtenerPorEstado(false);
                break;
            case FiltroTareas.Completadas:
                tareas = _repository.ObtenerPorEstado(true);
                break;
            case FiltroTareas.Todas:
            default:
                tareas = _repository.ObtenerTodas();
                break;
        }
        
        // Aplicar ordenamiento
        return orden switch
        {
            OrdenTareas.Prioridad => tareas.OrderByDescending(t => t.Prioridad).ToList(),
            OrdenTareas.Estado => tareas.OrderBy(t => t.Completada).ToList(),
            OrdenTareas.FechaCreacion or _ => tareas.OrderByDescending(t => t.FechaCreacion).ToList()
        };
    }
    
    /// <summary>
    /// Cambia el estado de una tarea (toggle completada/pendiente)
    /// </summary>
    public void CambiarEstadoTarea(int id)
    {
        var tarea = _repository.ObtenerPorId(id);
        
        if (tarea == null)
        {
            throw new InvalidOperationException($"No se encontró la tarea con ID {id}");
        }
        
        if (tarea.Completada)
        {
            _repository.MarcarComoPendiente(id);
        }
        else
        {
            _repository.MarcarComoCompletada(id);
        }
    }
    
    /// <summary>
    /// Elimina una tarea con validación de existencia
    /// </summary>
    public void EliminarTarea(int id)
    {
        var tarea = _repository.ObtenerPorId(id);
        
        if (tarea == null)
        {
            throw new InvalidOperationException($"No se encontró la tarea con ID {id}");
        }
        
        _repository.Eliminar(id);
    }
}

/// <summary>
/// Opciones de filtrado de tareas
/// </summary>
public enum FiltroTareas
{
    Todas,
    Pendientes,
    Completadas
}

/// <summary>
/// Opciones de ordenamiento de tareas
/// </summary>
public enum OrdenTareas
{
    FechaCreacion,
    Prioridad,
    Estado
}
