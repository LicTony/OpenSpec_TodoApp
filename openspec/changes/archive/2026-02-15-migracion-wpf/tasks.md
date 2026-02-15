## 1. Diseño de la Interfaz (XAML)

- [x] 1.1 Definir la estructura de cuadrícula (Grid) en `MainWindow.xaml` (Header, Input, Filters, Items).
- [x] 1.2 Implementar el panel superior con un `TextBox` para la descripción de la tarea y un `Button` para agregar.
- [x] 1.3 Implementar la barra de filtros usando `RadioButtons` o un `ComboBox` vinculado a `FiltroActual`.
- [x] 1.4 Crear el `ListView` o `ItemsControl` para mostrar la colección `Tareas`.

## 2. Implementación de Data Templates y Estilos

- [x] 2.1 Crear un `DataTemplate` para los elementos de la lista de tareas.
- [x] 2.2 Agregar un `CheckBox` vinculado a la propiedad `Completada` (usando el comando `CambiarEstadoTareaCommand`).
- [x] 2.3 Implementar disparadores visuales (Triggers) o Coverters para tachar el texto o reducir la opacidad en tareas completadas.
- [x] 2.4 Aplicar colores o iconos distintivos según el nivel de `Prioridad`.

## 3. Lógica del ViewModel y Vinculación

- [x] 3.1 Crear el `MainViewModel` con la lógica de carga y comandos (Ya implementado).
- [x] 3.2 Configurar la Inyección de Dependencias en `App.xaml.cs` (Ya implementado).
- [x] 3.3 Asegurar que el `TextBox` de nueva tarea soporte el comando Agregar al presionar la tecla `Enter`.
- [x] 3.4 Implementar un diálogo de confirmación o un manejo básico de errores para la eliminación de tareas.

## 4. Pruebas y Pulido Final

- [x] 4.1 Verificar la persistencia: los cambios en la UI deben reflejarse inmediatamente en `tareas.db`.
- [x] 4.2 Probar el filtrado dinámico: al cambiar el filtro, la lista debe actualizarse sin refrescar la ventana.
- [x] 4.3 Ajustar márgenes, paddings y tipografía para un acabado "premium".

## 5. Feedback y Mejoras (Iteración 2)

- [x] 5.1 Configurar propiedades de MainWindow (Título "Todo App", Maximizado).
- [x] 5.2 Implementar validación con alerta al intentar agregar tarea vacía.
- [x] 5.3 Implementar funcionalidad de editar descripción de tarea (Servicio + Command + Dialog + UI).
