## Context

La aplicación TodoApp actualmente carga todas las tareas de la base de datos `tareas.db` en una única consulta. Aunque esto funciona para listas pequeñas, no es escalable. Este diseño propone una solución de paginación de extremo a extremo (desde la base de datos hasta la interfaz de usuario WPF) utilizando el patrón MVVM y CommunityToolkit.Mvvm.

## Goals / Non-Goals

**Goals:**

- Implementar consultas SQL paginadas (`LIMIT` y `OFFSET`) en el repositorio.
- Actualizar el servicio para soportar parámetros de paginación.
- Introducir controles de navegación en la vista (`MainWindow.xaml`).
- Garantizar que el estado de la paginación se mantenga sincronizado con los filtros y el ordenamiento.

**Non-Goals:**

- Cambiar el motor de base de datos (se mantiene SQLite).
- Implementar "scroll infinito" (se prefiere paginación clásica con botones).
- Paginación del lado del cliente (la paginación será estrictamente del lado del servidor/DB por rendimiento).

## Decisions

### 1. Capa de Datos (TareaRepository)

Se añadirán dos nuevos métodos para soportar la paginación:

- `ContarTareas(completada?)`: Devuelve el total de filas que coinciden con el filtro.
- `ObtenerTareasPaginadas(filtro, orden, skip, take)`: Utiliza cláusulas `LIMIT Y OFFSET X` de SQLite.

### 2. Capa de Servicio (TareaService)

Se modificará `ObtenerTareas` para que devuelva un objeto envoltorio (ResultadoPaginado) que incluya tanto la lista de tareas como el conteo total necesario para calcular las páginas en la UI.

```csharp
public class ResultadoPaginado<T> {
    public List<T> Items { get; set; }
    public int TotalRegistros { get; set; }
}
```

### 3. ViewModel (MainViewModel)

Se añadirán las siguientes propiedades observables:

- `int PaginaActual` (Default: 1)
- `int TamañoPagina` (Default: 10)
- `int TotalRegistros`
- `int TotalPaginas` (Propiedad calculada)

Y comandos:

- `PaginaSiguienteCommand`
- `PaginaAnteriorCommand`

### 4. Interfaz de Usuario (WPF)

Se añadirá un `StackPanel` horizontal al pie del `ItemsControl` (o `ListView`) con:

- Botón "<" (Anterior) vinculado a `PaginaAnteriorCommand`.
- Texto "Página X de Y" vinculado a `PaginaActual` y `TotalPaginas`.
- Botón ">" (Siguiente) vinculado a `PaginaSiguienteCommand`.

## Risks / Trade-offs

- **Drift de Datos**: Si se añade o elimina una tarea mientras el usuario navega, los elementos podrían desplazarse entre páginas. Se asume aceptable para este nivel de aplicación.
- **Complejidad MVVM**: Añadir lógica de paginación aumenta el tamaño del `MainViewModel`. Se mitigará manteniendo la lógica de cálculo lo más simple posible.
- **Dapper Mapping**: Se debe asegurar que las cláusulas `ORDER BY` dinámicas en SQL sean seguras contra inyección o se manejen mediante lógica de selección de strings predefinidos.
