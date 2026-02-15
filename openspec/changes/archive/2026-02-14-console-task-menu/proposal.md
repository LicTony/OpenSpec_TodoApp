# Proposal: Console Task Menu

## Why

La aplicación de consola actualmente solo muestra "Hello, World!" y no tiene funcionalidad real. El usuario necesita una forma interactiva de gestionar tareas pendientes a través de un menú de consola con operaciones CRUD básicas, con validación robusta de entrada.

## What Changes

- The system SHALL replace the boilerplate code with an interactive menu
- The system SHALL implement 4 operations: add, view, delete tasks, and exit
- The system SHALL store tasks in memory during execution
- The system SHALL validate all user inputs (menu options, task indexes)
- The system SHALL display feedback to the user after each operation

## Capabilities

### New Capabilities
- `task-management`: Gestión de tareas con operaciones de agregar, listar y eliminar
- `input-validation`: Validación de entrada del usuario con mensajes de error claros

## Impact

- `Program.cs`: Reescritura completa para implementar el menú, lógica de tareas y validación
