## Context

La aplicación original era una herramienta de consola para gestionar tareas, utilizando SQLite y Dapper. La lógica de negocio (`Models`, `Data`, `Services`) estaba bien estructurada pero acoplada a una UI de consola. Se ha tomado la decisión estratégica de migrar a WPF para ofrecer una experiencia de usuario superior.

Actualmente, ya se ha realizado la refactorización estructural base:

- Se ha creado el proyecto WPF (`TodoApp`).
- Se han migrado las clases de lógica de negocio.
- Se ha configurado el contenedor de Inyección de Dependencias (Host Builder) en `App.xaml.cs`.
- Se ha creado un `MainViewModel` inicial con comandos básicos.

## Goals / Non-Goals

**Goals:**

- Implementar una interfaz gráfica funcional y atractiva en WPF.
- Utilizar el patrón MVVM (Model-View-ViewModel) para una clara separación de preocupaciones.
- Mantener la integridad de la base de datos y la lógica de negocio existente.
- Proveer feedback visual de las acciones (crear, completar, eliminar).
- Permitir filtrado dinámico de tareas (Todas, Pendientes, Completadas).

**Non-Goals:**

- Reescribir la capa de datos o servicios (se reutiliza lo existente).
- Implementar un sistema de autenticación de usuarios en esta fase.
- Crear una versión móvil (Maui/Xamarin) en este momento.

## Decisions

### Arquitectura MVVM

Se utilizará **CommunityToolkit.Mvvm** por su eficiencia y generadores de código fuente.

- **ViewModel**: `MainViewModel` será el orquestador principal de la vista `MainWindow`.
- **Servicios**: Se inyectarán en el constructor del ViewModel.
- **Binding**: Se usará DataBinding bidireccional x:Bind o Binding estándar de WPF.

### Inyección de Dependencias

Se opta por **Microsoft.Extensions.DependencyInjection** con `Host.CreateDefaultBuilder`.

- Esto alinea la aplicación con los estándares modernos de .NET.
- Permite gestionar el ciclo de vida de los servicios (`Singleton` para Repository, `Transient` para ViewModels).

### Diseño de Interfaz (UI)

Se diseñará una `MainWindow` con una estructura de **Grid**:

- **Header**: Título de la aplicación.
- **Input Area**: TextBox y Button para agregar tareas rápidamente.
- **Filter Bar**: RadioButtons o ComboBox para filtrar por estado.
- **Task List**: Un `ItemsControl` o `ListView` con un `DataTemplate` personalizado para representar cada tarea.
  - Se usarán Triggers o Converters para cambiar el estilo visual de las tareas completadas (e.g., tachado, opacidad reducida).
  - Se incluirán botones de acción (Completar/Eliminar) dentro de cada item de la lista.

## Risks / Trade-offs

- **Curva de aprendizaje XAML**: El diseño en XAML puede ser verborrágico, pero ofrece gran control.
- **Gestión de Errores en UI**: Al pasar de consola a UI, los errores deben manejarse con diálogos o notificaciones (Toasts/Snackbars) en lugar de excepciones no controladas.
- **Rendimiento de Lista**: Si la lista de tareas crece mucho, habrá que considerar virtualización (`VirtualizingStackPanel`), aunque para el alcance actual no es crítico.
