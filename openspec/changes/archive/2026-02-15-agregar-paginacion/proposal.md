# Proposal: agregar-paginacion

## Why

Actualmente, la aplicación carga todas las tareas de la base de datos a la vez. A medida que la lista de tareas crece, esto puede causar problemas de rendimiento, tiempos de carga lentos y una interfaz saturada que dificulta la navegación del usuario. La paginación permitirá manejar grandes volúmenes de datos de manera eficiente, mejorando tanto la velocidad de respuesta como la usabilidad.

## What Changes

- Se implementará el soporte para consultas paginadas en la capa de datos (`TareaRepository`) y servicios (`TareaService`).
- El `MainViewModel` incluirá lógica para controlar la página actual, el tamaño de la página y el conteo total de elementos/páginas.
- Se actualizará la interfaz de usuario (`MainWindow.xaml`) para incluir controles de navegación (Anterior, Siguiente, Indicador de página).
- Se modificará la carga de tareas para que sea reactiva a los cambios de página.

## Capabilities

### New Capabilities

- `paged-retrieval`: Capacidad de recuperar un subconjunto específico de tareas (página) basándose en un índice y tamaño de página.
- `pagination-navigation`: Sistema de navegación en la interfaz de usuario para cambiar entre diferentes bloques de datos.

### Modified Capabilities

- `task-list`: La visualización de la lista de tareas ahora estará limitada por el tamaño de página configurado.

## Impact

- **TodoApp.Data.TareaRepository**: Nuevos métodos para contar tareas y obtener tareas paginadas.
- **TodoApp.Services.TareaService**: Actualización para manejar parámetros de paginación en `ObtenerTareas`.
- **TodoApp.ViewModels.MainViewModel**: Nuevas propiedades observables (`PaginaActual`, `TotalItems`, `EsPrimeraPagina`, etc.) y comandos de navegación.
- **TodoApp.MainWindow.xaml**: Adición de la barra de herramientas de paginación al pie de la lista.
