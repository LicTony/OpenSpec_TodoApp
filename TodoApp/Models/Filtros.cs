namespace TodoApp.Models;

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
