# Task Management Specification

## ADDED Requirements

### Requirement: Display Main Menu

The system SHALL display an interactive menu with four options when the application starts.

#### Scenario: Application starts

- **WHEN** the application is launched
- **THEN** the system displays a menu with options:
  1. Agregar tarea
  2. Ver tareas
  3. Eliminar tarea
  4. Salir
- **AND** the system prompts the user to select an option

---

### Requirement: Add Task

The system SHALL allow users to add a new task to the list.

#### Scenario: User adds a task successfully

- **WHEN** the user selects option "1" (Agregar tarea)
- **THEN** the system prompts for the task description
- **AND** when the user enters a non-empty description
- **THEN** the task is added to the list
- **AND** the system displays a confirmation message
- **AND** the menu is displayed again

#### Scenario: User tries to add an empty task

- **WHEN** the user enters an empty or whitespace-only description
- **THEN** the system displays an error message "La tarea no puede estar vacía"
- **AND** prompts again for the task description

---

### Requirement: View Tasks

The system SHALL display all tasks in the list.

#### Scenario: User views tasks with items in list

- **WHEN** the user selects option "2" (Ver tareas)
- **THEN** the system displays all tasks with their index numbers (1-based)
- **AND** the menu is displayed again

#### Scenario: User views tasks with empty list

- **WHEN** the user selects option "2" (Ver tareas)
- **AND** the task list is empty
- **THEN** the system displays "No hay tareas pendientes"
- **AND** the menu is displayed again

---

### Requirement: Delete Task

The system SHALL allow users to delete a task by its index number.

#### Scenario: User deletes a task successfully

- **WHEN** the user selects option "3" (Eliminar tarea)
- **THEN** the system displays the current task list
- **AND** prompts for the task number to delete
- **AND** when the user enters a valid index
- **THEN** the task is removed from the list
- **AND** the system displays a confirmation message
- **AND** the menu is displayed again

#### Scenario: User enters invalid task number

- **WHEN** the user enters a non-numeric value
- **THEN** the system displays an error message "Ingrese un número válido"

#### Scenario: User enters out-of-range task number

- **WHEN** the user enters a number outside the valid range
- **THEN** the system displays an error message "Número de tarea fuera de rango"

---

### Requirement: Exit Application

The system SHALL allow users to exit the application.

#### Scenario: User exits the application

- **WHEN** the user selects option "4" (Salir)
- **THEN** the system displays a goodbye message
- **AND** the application terminates

---

### Requirement: Validate Menu Input

The system SHALL validate all menu selections.

#### Scenario: User enters invalid menu option

- **WHEN** the user enters a value that is not 1, 2, 3, or 4
- **THEN** the system displays an error message "Opción no válida. Seleccione 1-4"
- **AND** the menu is displayed again
