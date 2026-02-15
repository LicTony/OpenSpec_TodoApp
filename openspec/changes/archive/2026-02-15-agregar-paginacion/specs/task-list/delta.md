## MODIFIED Requirements

### Requirement: Visualización de lista de tareas con límite

La lista principal de tareas ya no mostrará la totalidad de las tareas existentes, sino que estará limitada al conjunto devuelto por la página actual.

#### Scenario: Sincronización con el tamaño de página

- **WHEN** Se define un tamaño de página de 10 tareas.
- **THEN** La lista visual en `MainWindow` nunca debe exceder los 10 elementos simultáneos, independientemente del total de tareas en la base de datos.
