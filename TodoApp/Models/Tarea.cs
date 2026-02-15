namespace TodoApp.Models;

/// <summary>
/// Representa una tarea en el sistema
/// </summary>
public class Tarea
{
    /// <summary>
    /// Identificador único de la tarea
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Descripción de la tarea
    /// </summary>
    public string Descripcion { get; set; } = string.Empty;
    
    /// <summary>
    /// Fecha y hora de creación de la tarea
    /// </summary>
    public DateTime FechaCreacion { get; set; }
    
    /// <summary>
    /// Indica si la tarea está completada
    /// </summary>
    public bool Completada { get; set; }
    
    /// <summary>
    /// Fecha y hora de completado (null si está pendiente)
    /// </summary>
    public DateTime? FechaCompletada { get; set; }
    
    /// <summary>
    /// Nivel de prioridad de la tarea
    /// </summary>
    public Prioridad Prioridad { get; set; }
}
