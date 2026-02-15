// TodoApp - Console Task Manager
using TodoApp.Data;
using TodoApp.Services;
using TodoApp.UI;

Console.Title = "Gestor de Tareas - SQLite";

Subir a git
//Todo migrar la aplicacion a WPF

// 1. Inicialización de Base de Datos y Migración
// Esto creará el esquema si no existe y migrará datos desde JSON si es necesario
var dbInitializer = new DatabaseInitializer();
dbInitializer.Inicializar();

// 2. Configuración de Dependencias (Manual Dependency Injection)
var repository = new TareaRepository();
var service = new TareaService(repository);
var ui = new ConsoleUI(service);

// 3. Ejecución de la Interfaz de Usuario
// El control se transfiere a la clase ConsoleUI que maneja el loop principal
ui.Ejecutar();
