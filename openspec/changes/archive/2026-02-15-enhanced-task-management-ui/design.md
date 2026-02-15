# enhanced-task-management-ui Design

## Context

La aplicación "TodoApp" en WPF utiliza un patrón MVVM. Actualmente, `MainViewModel` gestiona la lista de tareas y operaciones básicas. El modelo `Tarea` y el servicio `TareaService` soportan prioridades y ordenamiento, pero la UI en `MainWindow.xaml` no expone estos controles.

## Goals

1. **Exponer Prioridades**: Permitir al usuario seleccionar la prioridad al crear una tarea.
2. **Habilitar Ordenamiento**: Proveer controles para reordenar la lista (Fecha, Prioridad, Estado).
3. **Mejorar Edición**: Implementar un diálogo dedicado (`TaskDialog`) para editar múltiples atributos de una tarea.
4. **Mantener MVVM**: Utilizar `IDialogService` para abstraer la creación de ventanas del ViewModel.

## Non-Goals

- Persistencia compleja o cambios en la base de datos (SQLite ya soporta los campos necesarios).
- Autenticación de usuarios.
- Temas o estilos avanzados más allá de lo necesario para los nuevos controles.

## Decisions

### 1. UI de Creación y Ordenamiento

- Se añadirá un `ComboBox` para la prioridad al lado del campo de texto de nueva tarea.
- Se añadirá un panel de control con `ComboBox` para el criterio de ordenamiento (`Fecha`, `Prioridad`, `Estado`).
- El `MainViewModel` tendrá propiedades `NuevaTareaPrioridad` y `OrdenActual` que dispararán la recarga de la lista (`CargarTareas`) al cambiar.

### 2. TaskDialog (Edición)

- Se creará una nueva ventana `TaskDialog.xaml`.
- Contendrá:
  - TextBox para **Descripción**.
  - ComboBox (o RadioButtons) para **Prioridad**.
  - CheckBox para **Estado** (Completada/Pendiente).
- Botones **Guardar** y **Cancelar**.

### 3. IDialogService

- Se expandirá la interfaz `IDialogService` con un nuevo método:

    ```csharp
    (bool Confirmado, string Descripcion, Prioridad Prioridad, bool Completada) ShowEditTaskDialog(Tarea tareaExistente);
    // O alternativamente retornar un objeto modelo/DTO
    ```

    *Decisión simplificada*: Retornar una tupla o clase resultado para evitar pasar referencias directas.

## Risks / Trade-offs

- **Complejidad del Diálogo**: Manejar el estado de la ventana modal y asegurar que retorna los datos correctamente al ViewModel.
- **Refresco de UI**: Al cambiar el orden o editar una tarea, la lista (`ObservableCollection`) debe actualizarse correctamente para reflejar los cambios inmediatos sin requerir una recarga completa de base de datos si es posible, aunque `CargarTareas` recarga todo, lo cual es seguro pero podría ser menos eficiente. Dado el tamaño de la lista (todo apps suelen ser pequeñas), recargar es aceptable.
