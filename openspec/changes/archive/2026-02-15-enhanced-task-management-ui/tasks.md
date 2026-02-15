# enhanced-task-management-ui Tasks

## 1. Infraestructura UI (Fase 1)

- [x] 1.1 Exponer Enums como recursos XAML (Prioridad, OrdenTareas) para uso en ComboBoxes.
- [x] 1.2 Crear `TaskDialog.xaml` con controles para Descripción, Prioridad y Estado.
- [x] 1.3 Implementar lógica code-behind en `TaskDialog.xaml.cs` para manejar OK/Cancel y retorno de datos.

## 2. ViewModel y Lógica (Fase 2)

- [x] 2.1 Actualizar contrato `IDialogService` con método `ShowTaskDialog`.
- [x] 2.2 Implementar método `ShowTaskDialog` en la clase concreta del servicio.
- [x] 2.3 Actualizar `MainViewModel` para soportar `NuevaTareaPrioridad` y `OrdenActual`.
- [x] 2.4 Modificar comandos `AgregarTarea` y `CargarTareas` para usar las nuevas propiedades.
- [x] 2.5 Modificar comando `EditarTarea` para utilizar el nuevo `TaskDialog`.

## 3. Integración UI Principal (Fase 3)

- [x] 3.1 Agregar selector de Prioridad en el área de creación de `MainWindow.xaml`.
- [x] 3.2 Agregar controles de Ordenamiento en el header de `MainWindow.xaml`.
- [x] 3.3 Validar funcionamiento completo (Crear con prioridad, Ordenar, Editar avanzado).
