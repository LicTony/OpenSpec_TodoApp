namespace TodoApp.Models;

/// <summary>
/// Clase genérica para transportar resultados de una consulta paginada
/// </summary>
/// <typeparam name="T">Tipo de los elementos en la página</typeparam>
public class ResultadoPaginado<T>
{
    /// <summary>
    /// Lista de elementos de la página actual
    /// </summary>
    public List<T> Items { get; set; } = new();

    /// <summary>
    /// Total de registros que coinciden con el filtro (sin paginar)
    /// </summary>
    public int TotalRegistros { get; set; }
}
