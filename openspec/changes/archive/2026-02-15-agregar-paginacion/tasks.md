## 1. Capa de Datos (Data Layer)

- [x] 1.1 Crear clase `ResultadoPaginado<T>` en `TodoApp.Models` para transportar datos y metadatos.
- [x] 1.2 Agregar método `ContarTareas(FiltroTareas filtro)` en `TareaRepository` para obtener el total de registros filtrados.
- [x] 1.3 Agregar método `ObtenerTareasPaginadas(FiltroTareas filtro, OrdenTareas orden, int skip, int take)` en `TareaRepository` usando cláusulas SQL `LIMIT` y `OFFSET`.

## 2. Capa de Servicio (Service Layer)

- [x] 2.1 Modificar `TareaService.ObtenerTareas` para que acepte parámetros de paginación (`paginaActual`, `tamañoPagina`).
- [x] 2.2 Actualizar `TareaService.ObtenerTareas` para que devuelva un `ResultadoPaginado<Tarea>` utilizando los nuevos métodos del repositorio.

## 3. ViewModel (MainViewModel)

- [x] 3.1 Añadir propiedades observables `PaginaActual`, `TamañoPagina` y `TotalRegistros` en `MainViewModel`.
- [x] 3.2 Añadir propiedad calculada `TotalPaginas` y booleanos `EsPrimeraPagina`/`EsUltimaPagina` para el estado de los botones.
- [x] 3.3 Implementar `RelayCommand` para `PaginaSiguiente` y `PaginaAnterior`.
- [x] 3.4 Actualizar el comando `CargarTareas` para que pase la página actual al servicio y actualice el conteo total.
- [x] 3.5 Asegurar que el cambio de filtros u ordenamiento reinicie `PaginaActual` a 1.

## 4. Interfaz de Usuario (View)

- [x] 4.1 Añadir controles de navegación (botones y texto de estado) en `MainWindow.xaml` al pie de la lista de tareas.
- [x] 4.2 Vincular los controles a las propiedades y comandos del `MainViewModel`.
- [x] 4.3 Ajustar los estilos de los controles de paginación para que coincidan con la estética del proyecto.
