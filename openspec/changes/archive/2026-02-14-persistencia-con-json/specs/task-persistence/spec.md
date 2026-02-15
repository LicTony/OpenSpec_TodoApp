# task-persistence Delta Specification

## ADDED Requirements

### Requirement: Load Tasks on Startup

The system SHALL load tasks from the JSON file when the application starts.

#### Scenario: Load tasks from existing file

- **WHEN** the application starts
- **AND** the file `tareas.json` exists with valid JSON content
- **THEN** the system loads all tasks into the task list
- **AND** the tasks are available for viewing and modification

#### Scenario: Start with empty list when file does not exist

- **WHEN** the application starts
- **AND** the file `tareas.json` does not exist
- **THEN** the system starts with an empty task list
- **AND** no error is displayed to the user

#### Scenario: Handle corrupted JSON file

- **WHEN** the application starts
- **AND** the file `tareas.json` exists but contains invalid JSON
- **THEN** the system starts with an empty task list
- **AND** no error is displayed to the user

---

### Requirement: Save Tasks on Modification

The system SHALL save tasks to the JSON file after each modification operation.

#### Scenario: Save tasks after adding

- **WHEN** a user adds a new task
- **THEN** the system saves the updated task list to `tareas.json`
- **AND** the file contains the new task

#### Scenario: Save tasks after deleting

- **WHEN** a user deletes a task
- **THEN** the system saves the updated task list to `tareas.json`
- **AND** the file no longer contains the deleted task

---

### Requirement: JSON File Format

The system SHALL store tasks in JSON format as an array of strings.

#### Scenario: File format is valid JSON array

- **WHEN** tasks are saved to `tareas.json`
- **THEN** the file contains a valid JSON array
- **AND** each task is a JSON string element in the array

#### Scenario: Empty list produces empty array

- **WHEN** the task list is empty
- **AND** tasks are saved to `tareas.json`
- **THEN** the file contains an empty JSON array `[]`
