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
    /// Obtiene tareas con filtrado, ordenamiento y paginación
    /// </summary>
    public ResultadoPaginado<Tarea> ObtenerTareas(
        FiltroTareas filtro = FiltroTareas.Todas, 
        OrdenTareas orden = OrdenTareas.FechaCreacion,
        int paginaActual = 1,
        int tamañoPagina = 10)
    {
        // Validaciones básicas de paginación
        if (paginaActual < 1) paginaActual = 1;
        if (tamañoPagina < 1) tamañoPagina = 10;

        int skip = (paginaActual - 1) * tamañoPagina;
        
        // Obtener el total de registros para el filtro
        int totalRegistros = _repository.ContarTareas(filtro);
        
        // Obtener la página de tareas
        var items = _repository.ObtenerTareasPaginadas(filtro, orden, skip, tamañoPagina);
        
        return new ResultadoPaginado<Tarea>
        {
            Items = items,
            TotalRegistros = totalRegistros
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
    /// Actualiza la descripción de una tarea existente
    /// </summary>
    public void ActualizarDescripcion(int id, string nuevaDescripcion)
    {
        if (string.IsNullOrWhiteSpace(nuevaDescripcion))
        {
            throw new ArgumentException("La descripción no puede estar vacía", nameof(nuevaDescripcion));
        }
        
        var tarea = _repository.ObtenerPorId(id);
        
        if (tarea == null)
        {
            throw new InvalidOperationException($"No se encontró la tarea con ID {id}");
        }
        
        tarea.Descripcion = nuevaDescripcion.Trim();
        _repository.Actualizar(tarea);
    }

    /// <summary>
    /// Actualiza todos los datos de una tarea
    /// </summary>
    public void ActualizarTarea(int id, string descripcion, Prioridad prioridad, bool completada)
    {
        if (string.IsNullOrWhiteSpace(descripcion))
        {
            throw new ArgumentException("La descripción no puede estar vacía", nameof(descripcion));
        }

        var tarea = _repository.ObtenerPorId(id);
        
        if (tarea == null)
        {
            throw new InvalidOperationException($"No se encontró la tarea con ID {id}");
        }

        tarea.Descripcion = descripcion.Trim();
        tarea.Prioridad = prioridad;
        
        if (tarea.Completada != completada)
        {
             if (completada) 
             {
                 tarea.Completada = true;
                 tarea.FechaCompletada = DateTime.Now;
             }
             else
             {
                 tarea.Completada = false;
                 tarea.FechaCompletada = null;
             }
        }

        _repository.Actualizar(tarea);
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
