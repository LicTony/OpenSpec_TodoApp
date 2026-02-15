using Dapper;
using Microsoft.Data.Sqlite;
using TodoApp.Models;

namespace TodoApp.Data;

/// <summary>
/// Repositorio para operaciones CRUD de tareas usando Dapper
/// </summary>
public class TareaRepository
{
    private const string ConnectionString = "Data Source=tareas.db";
    
    /// <summary>
    /// Agrega una nueva tarea a la base de datos
    /// </summary>
    public int Agregar(Tarea tarea)
    {
        using var connection = new SqliteConnection(ConnectionString);
        
        var sql = @"
            INSERT INTO Tareas (Descripcion, FechaCreacion, Completada, FechaCompletada, Prioridad)
            VALUES (@Descripcion, @FechaCreacion, @Completada, @FechaCompletada, @Prioridad);
            SELECT last_insert_rowid();";
        
        var parametros = new
        {
            tarea.Descripcion,
            FechaCreacion = tarea.FechaCreacion.ToString("O"),
            Completada = tarea.Completada ? 1 : 0,
            FechaCompletada = tarea.FechaCompletada?.ToString("O"),
            Prioridad = (int)tarea.Prioridad
        };
        
        return connection.ExecuteScalar<int>(sql, parametros);
    }
    
    /// <summary>
    /// Obtiene todas las tareas
    /// </summary>
    public List<Tarea> ObtenerTodas()
    {
        using var connection = new SqliteConnection(ConnectionString);
        
        var sql = "SELECT * FROM Tareas";
        var tareas = connection.Query<TareaDto>(sql).ToList();
        
        return tareas.Select(MapearDtoATarea).ToList();
    }
    
    /// <summary>
    /// Obtiene una tarea por su ID
    /// </summary>
    public Tarea? ObtenerPorId(int id)
    {
        using var connection = new SqliteConnection(ConnectionString);
        
        var sql = "SELECT * FROM Tareas WHERE Id = @Id";
        var tareaDto = connection.QueryFirstOrDefault<TareaDto>(sql, new { Id = id });
        
        return tareaDto != null ? MapearDtoATarea(tareaDto) : null;
    }
    
    /// <summary>
    /// Actualiza una tarea existente
    /// </summary>
    public void Actualizar(Tarea tarea)
    {
        using var connection = new SqliteConnection(ConnectionString);
        
        var sql = @"
            UPDATE Tareas 
            SET Descripcion = @Descripcion,
                FechaCreacion = @FechaCreacion,
                Completada = @Completada,
                FechaCompletada = @FechaCompletada,
                Prioridad = @Prioridad
            WHERE Id = @Id";
        
        var parametros = new
        {
            tarea.Id,
            tarea.Descripcion,
            FechaCreacion = tarea.FechaCreacion.ToString("O"),
            Completada = tarea.Completada ? 1 : 0,
            FechaCompletada = tarea.FechaCompletada?.ToString("O"),
            Prioridad = (int)tarea.Prioridad
        };
        
        connection.Execute(sql, parametros);
    }
    
    /// <summary>
    /// Elimina una tarea por su ID
    /// </summary>
    public void Eliminar(int id)
    {
        using var connection = new SqliteConnection(ConnectionString);
        
        var sql = "DELETE FROM Tareas WHERE Id = @Id";
        connection.Execute(sql, new { Id = id });
    }
    
    /// <summary>
    /// Obtiene tareas filtradas por estado de completado
    /// </summary>
    public List<Tarea> ObtenerPorEstado(bool completada)
    {
        using var connection = new SqliteConnection(ConnectionString);
        
        var sql = "SELECT * FROM Tareas WHERE Completada = @Completada";
        var tareas = connection.Query<TareaDto>(sql, new { Completada = completada ? 1 : 0 }).ToList();
        
        return tareas.Select(MapearDtoATarea).ToList();
    }

    /// <summary>
    /// Cuenta el total de tareas según el filtro aplicado
    /// </summary>
    public int ContarTareas(FiltroTareas filtro)
    {
        using var connection = new SqliteConnection(ConnectionString);
        
        var sql = "SELECT COUNT(*) FROM Tareas";
        
        if (filtro == FiltroTareas.Pendientes)
            sql += " WHERE Completada = 0";
        else if (filtro == FiltroTareas.Completadas)
            sql += " WHERE Completada = 1";
            
        return connection.ExecuteScalar<int>(sql);
    }

    /// <summary>
    /// Obtiene una página de tareas con filtros y ordenamiento
    /// </summary>
    public List<Tarea> ObtenerTareasPaginadas(FiltroTareas filtro, OrdenTareas orden, int skip, int take)
    {
        using var connection = new SqliteConnection(ConnectionString);
        
        var sql = "SELECT * FROM Tareas";
        
        // Filtro
        if (filtro == FiltroTareas.Pendientes)
            sql += " WHERE Completada = 0";
        else if (filtro == FiltroTareas.Completadas)
            sql += " WHERE Completada = 1";

        // Ordenamiento
        var orderBy = orden switch
        {
            OrdenTareas.Prioridad => "Prioridad DESC, FechaCreacion DESC",
            OrdenTareas.Estado => "Completada ASC, FechaCreacion DESC",
            _ => "FechaCreacion DESC"
        };
        
        sql += $" ORDER BY {orderBy}";
        
        // Paginación
        sql += " LIMIT @Take OFFSET @Skip";

        var tareas = connection.Query<TareaDto>(sql, new { Skip = skip, Take = take }).ToList();
        
        return tareas.Select(MapearDtoATarea).ToList();
    }
    
    /// <summary>
    /// Marca una tarea como completada con timestamp
    /// </summary>
    public void MarcarComoCompletada(int id)
    {
        using var connection = new SqliteConnection(ConnectionString);
        
        var sql = @"
            UPDATE Tareas 
            SET Completada = 1,
                FechaCompletada = @FechaCompletada
            WHERE Id = @Id";
        
        connection.Execute(sql, new 
        { 
            Id = id, 
            FechaCompletada = DateTime.Now.ToString("O") 
        });
    }
    
    /// <summary>
    /// Marca una tarea como pendiente, limpiando la fecha de completado
    /// </summary>
    public void MarcarComoPendiente(int id)
    {
        using var connection = new SqliteConnection(ConnectionString);
        
        var sql = @"
            UPDATE Tareas 
            SET Completada = 0,
                FechaCompletada = NULL
            WHERE Id = @Id";
        
        connection.Execute(sql, new { Id = id });
    }
    
    /// <summary>
    /// Mapea un DTO de base de datos a un objeto Tarea
    /// </summary>
    private Tarea MapearDtoATarea(TareaDto dto)
    {
        return new Tarea
        {
            Id = dto.Id,
            Descripcion = dto.Descripcion,
            FechaCreacion = DateTime.Parse(dto.FechaCreacion),
            Completada = dto.Completada == 1,
            FechaCompletada = string.IsNullOrEmpty(dto.FechaCompletada) 
                ? null 
                : DateTime.Parse(dto.FechaCompletada),
            Prioridad = (Prioridad)dto.Prioridad
        };
    }
    
    /// <summary>
    /// DTO para mapeo de Dapper (SQLite almacena fechas como TEXT y bool como INTEGER)
    /// </summary>
    private class TareaDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string FechaCreacion { get; set; } = string.Empty;
        public int Completada { get; set; }
        public string? FechaCompletada { get; set; }
        public int Prioridad { get; set; }
    }
}
