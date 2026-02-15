# enhanced-task-management-ui

## Goal

Mejorar la experiencia de usuario (UX) de gestión de tareas implementando controles completos para prioridades, ordenamiento y edición avanzada, aprovechando las capacidades ya existentes en el backend.

## Why

La aplicación actual tiene limitaciones en la interfaz que ocultan funcionalidades del backend:

1. Las tareas se crean siempre con prioridad "Media" porque falta el control de selección.
2. El servicio soporta ordenamiento, pero la UI no expone esta funcionalidad.
3. La edición de tareas está restringida a solo cambiar el texto mediante un input simple, impidiendo ajustar otros atributos importantes.

## What Changes

### Gestión de Prioridades

- Se agregará un `ComboBox` en el área de creación para seleccionar la prioridad inicial (Baja, Media, Alta).
- Se visualizará la prioridad con indicadores visuales claros (ya existente, pero ahora será dinámica).

### Ordenamiento (Sorting)

- Se agregará un control en la barra superior para ordenar la lista por:
  - Fecha de Creación (Default)
  - Prioridad (Alta -> Baja)
  - Estado (Pendientes primero)

### Edición Avanzada

- Se reemplazará el `Prompt` simple por un nuevo diálogo `TaskDialog` (WPF Window/UserControl).
- Este diálogo permitirá editar simultáneamente:
  - Descripción
  - Prioridad
  - (Potencialmente) Estado

## Capabilities

### New Capabilities

- `task-ui-enhancements`: Mejoras en la interfaz para exponer funcionalidades de prioridad y ordenamiento.

### Modified Capabilities

- `gestion-ui-tareas`: Se actualizarán los requisitos de interfaz para incluir los nuevos controles de creación, ordenamiento y el flujo de edición avanzado.

## Impact

- **Frontend**: `MainWindow.xaml`, `TaskDialog.xaml` (nuevo).
- **ViewModel**: `MainViewModel.cs` (nuevas propiedades y comandos).
- **Services**: `IDialogService.cs` y su implementación (soporte para diálogo complejo).
