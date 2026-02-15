## ADDED Requirements

### Requirement: Detect legacy JSON file

El sistema SHALL detectar automáticamente si existe un archivo `tareas.json` al iniciar la aplicación.

#### Scenario: JSON file exists on first run

- **WHEN** la aplicación se ejecuta por primera vez y existe `tareas.json`
- **THEN** el sistema detecta el archivo y prepara la migración

#### Scenario: No JSON file exists

- **WHEN** la aplicación se ejecuta y no existe `tareas.json`
- **THEN** el sistema inicia con una base de datos SQLite vacía

### Requirement: Migrate JSON data to SQLite

El sistema SHALL migrar automáticamente todas las tareas desde `tareas.json` a la base de datos SQLite.

#### Scenario: Successful migration

- **WHEN** se detecta `tareas.json` con tareas válidas
- **THEN** todas las tareas se importan a SQLite con valores predeterminados para los nuevos campos

#### Scenario: Default values for migrated tasks

- **WHEN** se migran tareas desde JSON
- **THEN** cada tarea recibe:
  - `FechaCreacion`: fecha/hora actual de la migración
  - `Completada`: `false`
  - `FechaCompletada`: `null`
  - `Prioridad`: Media

#### Scenario: Empty JSON file

- **WHEN** `tareas.json` existe pero está vacío o contiene un array vacío
- **THEN** la migración se completa sin errores y la base de datos queda vacía

#### Scenario: Corrupted JSON file

- **WHEN** `tareas.json` existe pero contiene JSON inválido
- **THEN** el sistema registra un error y continúa con una base de datos vacía

### Requirement: Backup legacy file

El sistema SHALL preservar el archivo `tareas.json` original después de la migración.

#### Scenario: Rename JSON after migration

- **WHEN** la migración se completa exitosamente
- **THEN** el archivo `tareas.json` se renombra a `tareas.json.backup` con timestamp

#### Scenario: Preserve original on error

- **WHEN** la migración falla
- **THEN** el archivo `tareas.json` permanece sin cambios

### Requirement: Migration runs only once

El sistema SHALL ejecutar la migración solo una vez, evitando duplicados.

#### Scenario: Skip migration if already done

- **WHEN** la base de datos SQLite ya contiene datos
- **THEN** el sistema omite la migración incluso si existe `tareas.json`

#### Scenario: Migration status tracking

- **WHEN** se completa la migración
- **THEN** el sistema marca que la migración ya fue ejecutada (mediante la existencia de `tareas.db` y ausencia de `tareas.json`)

### Requirement: Migration logging

El sistema SHALL informar al usuario sobre el progreso de la migración.

#### Scenario: Display migration start

- **WHEN** la migración comienza
- **THEN** se muestra un mensaje: "Migrando tareas desde tareas.json..."

#### Scenario: Display migration success

- **WHEN** la migración se completa exitosamente
- **THEN** se muestra: "Migración completada: X tareas importadas"

#### Scenario: Display migration error

- **WHEN** la migración falla
- **THEN** se muestra un mensaje de error descriptivo y se continúa con base de datos vacía
