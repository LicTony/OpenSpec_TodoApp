# TodoApp

Una aplicación WPF moderna para gestión de tareas con soporte completo de prioridades, filtrado y ordenamiento.

## 📋 Características

- ✨ **Gestión completa de tareas**: Crear, editar, completar y eliminar tareas
- 🎯 **Prioridades**: Asigna prioridades (Baja, Media, Alta) al crear o editar tareas
- 🔽 **Ordenamiento avanzado**: Ordena tareas por Fecha de Creación, Prioridad o Estado
- 🔍 **Filtrado**: Visualiza Todas las tareas, solo Pendientes o solo Completadas
- ✏️ **Edición avanzada**: Diálogo dedicado para editar descripción, prioridad y estado
- 💾 **Persistencia SQLite**: Base de datos local con Dapper
- 🎨 **Interfaz WPF moderna**: Diseño limpio con indicadores visuales de prioridad

## 🏗️ Arquitectura

El proyecto utiliza el patrón **MVVM (Model-View-ViewModel)** con inyección de dependencias:

```
TodoApp/
├── Models/           # Entidades de dominio (Tarea, Prioridad)
├── Data/             # Acceso a datos con Dapper + SQLite
├── Services/         # Lógica de negocio (TareaService, DialogService)
├── ViewModels/       # ViewModels con CommunityToolkit.Mvvm
├── Views/            # Vistas XAML (MainWindow, TaskDialog)
└── App.xaml.cs       # Configuración DI y arranque
```

## 🚀 Cómo Ejecutar

### Requisitos

- .NET 10.0 SDK
- Windows (WPF)

### Pasos

1. Clonar el repositorio o navegar al directorio del proyecto.

2. Restaurar dependencias:

   ```bash
   dotnet restore
   ```

3. Ejecutar la aplicación:

   ```bash
   dotnet run --project TodoApp
   ```

   O desde Visual Studio: abrir `TodoApp.sln` y presionar F5.

## 🛠️ Tecnologías

- **Framework**: .NET 10.0 / WPF
- **Base de datos**: SQLite con Dapper
- **MVVM**: CommunityToolkit.Mvvm
- **Inyección de dependencias**: Microsoft.Extensions.DependencyInjection

## 📦 Dependencias

- `CommunityToolkit.Mvvm` (8.4.0)
- `Dapper` (2.1.66)
- `Microsoft.Data.Sqlite` (10.0.3)
- `Microsoft.Extensions.DependencyInjection` (10.0.3)
- `Microsoft.Extensions.Hosting` (10.0.3)

## 📝 Uso

### Crear una tarea

1. Escribe la descripción en el campo de texto
2. Selecciona la prioridad (Baja/Media/Alta)
3. Presiona "Add Task" o Enter

### Editar una tarea

1. Haz clic en el botón ✎ (editar) de la tarea
2. Modifica descripción, prioridad y/o estado en el diálogo
3. Confirma con "Guardar"

### Ordenar y Filtrar

- Usa el control **Order** para ordenar por Fecha, Prioridad o Estado
- Usa el control **Filter** para mostrar Todas/Pendientes/Completadas

## 🗂️ Especificaciones

Este proyecto sigue el sistema **OpenSpec** para documentación y gestión de cambios. Las especificaciones se encuentran en:

- `openspec/specs/` - Especificaciones principales
- `openspec/changes/archive/` - Historial de cambios implementados

## 📄 Licencia

Este proyecto es de código abierto con propósitos educativos.
