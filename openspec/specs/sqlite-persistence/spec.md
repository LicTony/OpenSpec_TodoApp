## ADDED Requirements

### Requirement: Database initialization

El sistema SHALL crear automáticamente la base de datos SQLite y su esquema si no existe al iniciar la aplicación.

#### Scenario: First run creates database

- **WHEN** la aplicación se ejecuta por primera vez
- **THEN** se crea el archivo `tareas.db` con el esquema completo de tablas

#### Scenario: Existing database is preserved

- **WHEN** la aplicación se ejecuta y `tareas.db` ya existe
- **THEN** el sistema usa la base de datos existente sin modificarla

### Requirement: Task table schema

La base de datos SHALL contener una tabla `Tareas` con las siguientes columnas:

- `Id` (INTEGER PRIMARY KEY AUTOINCREMENT)
- `Descripcion` (TEXT NOT NULL)
- `FechaCreacion` (TEXT NOT NULL, formato ISO 8601)
- `Completada` (INTEGER NOT NULL, 0 o 1)
- `FechaCompletada` (TEXT NULL, formato ISO 8601)
- `Prioridad` (INTEGER NOT NULL, 0=Baja, 1=Media, 2=Alta)

#### Scenario: Schema validation

- **WHEN** se inicializa la base de datos
- **THEN** la tabla `Tareas` contiene todas las columnas especificadas con los tipos y restricciones correctos

### Requirement: CRUD operations with Dapper

El sistema SHALL implementar operaciones CRUD (Create, Read, Update, Delete) usando Dapper como micro-ORM.

#### Scenario: Create task

- **WHEN** se agrega una nueva tarea
- **THEN** Dapper ejecuta un INSERT y retorna el ID generado automáticamente

#### Scenario: Read all tasks

- **WHEN** se solicitan todas las tareas
- **THEN** Dapper ejecuta un SELECT y mapea los resultados a objetos `Tarea`

#### Scenario: Update task

- **WHEN** se modifica una tarea existente
- **THEN** Dapper ejecuta un UPDATE con los nuevos valores

#### Scenario: Delete task

- **WHEN** se elimina una tarea
- **THEN** Dapper ejecuta un DELETE y la tarea desaparece de la base de datos

### Requirement: Connection management

El sistema SHALL gestionar correctamente las conexiones a SQLite usando el patrón `using` para garantizar la liberación de recursos.

#### Scenario: Connection is disposed after use

- **WHEN** se completa una operación de base de datos
- **THEN** la conexión se cierra y libera automáticamente

#### Scenario: Multiple operations use separate connections

- **WHEN** se ejecutan múltiples operaciones consecutivas
- **THEN** cada operación abre y cierra su propia conexión

### Requirement: Transaction support

El sistema SHALL soportar transacciones para operaciones que requieren atomicidad.

#### Scenario: Rollback on error

- **WHEN** ocurre un error durante una operación transaccional
- **THEN** todos los cambios se revierten (rollback)

#### Scenario: Commit on success

- **WHEN** una operación transaccional se completa exitosamente
- **THEN** todos los cambios se confirman (commit) en la base de datos
