using Microsoft.Data.Sqlite;
using System.Text.Json;
using System.IO;
using TodoApp.Models;

namespace TodoApp.Data;

/// <summary>
/// Inicializa la base de datos SQLite y maneja la migración desde JSON
/// </summary>
public class DatabaseInitializer
{
    private const string ConnectionString = "Data Source=tareas.db";
    private const string ArchivoJson = "tareas.json";
    
    /// <summary>
    /// Inicializa la base de datos, crea el esquema si no existe y ejecuta migración si es necesario
    /// </summary>
    public void Inicializar()
    {
        CrearEsquema();
        MigrarDesdeJsonSiEsNecesario();
    }
    
    /// <summary>
    /// Crea el esquema de la base de datos si no existe
    /// </summary>
    private void CrearEsquema()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        
        var createTableCommand = connection.CreateCommand();
        createTableCommand.CommandText = @"
            CREATE TABLE IF NOT EXISTS Tareas (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Descripcion TEXT NOT NULL,
                FechaCreacion TEXT NOT NULL,
                Completada INTEGER NOT NULL DEFAULT 0,
                FechaCompletada TEXT NULL,
                Prioridad INTEGER NOT NULL DEFAULT 1
            )";
        
        createTableCommand.ExecuteNonQuery();
    }
    
    /// <summary>
    /// Detecta si existe tareas.json y migra los datos a SQLite
    /// </summary>
    private void MigrarDesdeJsonSiEsNecesario()
    {
        // Si no existe el archivo JSON, no hay nada que migrar
        if (!File.Exists(ArchivoJson))
        {
            return;
        }
        
        // Verificar si la base de datos ya tiene datos (migración ya ejecutada)
        if (TieneDatos())
        {
            return;
        }
        
        try
        {
            Console.WriteLine("Migrando tareas desde tareas.json...");
            
            // Leer y deserializar JSON
            string json = File.ReadAllText(ArchivoJson);
            var descripciones = JsonSerializer.Deserialize<List<string>>(json);
            
            if (descripciones == null || descripciones.Count == 0)
            {
                Console.WriteLine("Migración completada: 0 tareas importadas (archivo vacío)");
                RenombrarArchivoJson();
                return;
            }
            
            // Convertir strings a objetos Tarea con valores default
            var tareas = descripciones.Select(desc => new Tarea
            {
                Descripcion = desc,
                FechaCreacion = DateTime.Now,
                Completada = false,
                FechaCompletada = null,
                Prioridad = Prioridad.Media
            }).ToList();
            
            // Insertar tareas en SQLite
            InsertarTareasMigradas(tareas);
            
            Console.WriteLine($"Migración completada: {tareas.Count} tareas importadas");
            
            // Renombrar archivo JSON como backup
            RenombrarArchivoJson();
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error al leer tareas.json (JSON corrupto): {ex.Message}");
            Console.WriteLine("Continuando con base de datos vacía");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error durante la migración: {ex.Message}");
            Console.WriteLine("Continuando con base de datos vacía");
        }
    }
    
    /// <summary>
    /// Verifica si la base de datos ya contiene datos
    /// </summary>
    private bool TieneDatos()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        
        var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM Tareas";
        
        var count = Convert.ToInt32(command.ExecuteScalar());
        return count > 0;
    }
    
    /// <summary>
    /// Inserta las tareas migradas en la base de datos
    /// </summary>
    private void InsertarTareasMigradas(List<Tarea> tareas)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        
        using var transaction = connection.BeginTransaction();
        
        try
        {
            foreach (var tarea in tareas)
            {
                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Tareas (Descripcion, FechaCreacion, Completada, FechaCompletada, Prioridad)
                    VALUES (@Descripcion, @FechaCreacion, @Completada, @FechaCompletada, @Prioridad)";
                
                command.Parameters.AddWithValue("@Descripcion", tarea.Descripcion);
                command.Parameters.AddWithValue("@FechaCreacion", tarea.FechaCreacion.ToString("O"));
                command.Parameters.AddWithValue("@Completada", tarea.Completada ? 1 : 0);
                command.Parameters.AddWithValue("@FechaCompletada", (object?)tarea.FechaCompletada?.ToString("O") ?? DBNull.Value);
                command.Parameters.AddWithValue("@Prioridad", (int)tarea.Prioridad);
                
                command.ExecuteNonQuery();
            }
            
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
    
    /// <summary>
    /// Renombra el archivo JSON a backup con timestamp
    /// </summary>
    private void RenombrarArchivoJson()
    {
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string nuevoNombre = $"{ArchivoJson}.backup.{timestamp}";
        File.Move(ArchivoJson, nuevoNombre);
    }
}
