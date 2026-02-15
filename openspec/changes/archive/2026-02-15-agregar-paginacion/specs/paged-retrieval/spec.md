## ADDED Requirements

### Requirement: Recuperación de tareas por páginas

El sistema debe permitir solicitar un subconjunto específico de tareas indicando el número de página y el tamaño de la misma, integrándose con los filtros y ordenamientos existentes.

#### Scenario: Carga de la primera página

- **WHEN** El usuario abre la aplicación o selecciona una nueva combinación de filtros/orden.
- **THEN** El sistema devuelve los primeros N elementos (donde N es el tamaño de página) y el conteo total de registros que coinciden con el criterio.

#### Scenario: Navegación a página específica

- **WHEN** El sistema solicita la página X con un tamaño Y.
- **THEN** El sistema devuelve los registros saltando los primeros (X-1)*Y elementos y tomando los siguientes Y elementos.

#### Scenario: Cambio de filtros reinicia paginación

- **WHEN** El usuario cambia el filtro (ej: de "Todas" a "Pendientes").
- **THEN** El sistema debe volver a la página 1 para los nuevos resultados filtrados.
