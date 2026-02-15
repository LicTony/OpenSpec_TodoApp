## ADDED Requirements

### Requirement: Navegación de interfaz de usuario para paginación

La interfaz debe proporcionar controles intuitivos para que el usuario pueda navegar entre las páginas de resultados.

#### Scenario: Controles de navegación habilitados/deshabilitados

- **WHEN** El usuario está en la página 1.
- **THEN** El botón "Anterior" debe estar deshabilitado.
- **WHEN** El usuario está en la última página.
- **THEN** El botón "Siguiente" debe estar deshabilitado.

#### Scenario: Indicador de estado de paginación

- **WHEN** Se cargan los datos.
- **THEN** La interfaz debe mostrar el número de página actual y el total de páginas (ej: "Página 1 de 5").

#### Scenario: Actualización de lista al navegar

- **WHEN** El usuario hace clic en "Siguiente".
- **THEN** El sistema incrementa la página actual y recarga automáticamente los datos correspondientes.
