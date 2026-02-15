## Why

La aplicación actual, aunque funcional en su lógica de negocio, carece de una interfaz gráfica de usuario moderna. Al ser una aplicación de consola, limita la experiencia del usuario y no aprovecha las capacidades visuales y de interacción que ofrece el ecosistema Windows. Migrar a WPF (Windows Presentation Foundation) permitirá ofrecer una experiencia más rica, intuitiva y profesional, facilitando la gestión de tareas visualmente.

## What Changes

Se transformará la aplicación de consola en una aplicación de escritorio WPF completa.
- Se eliminará la capa de presentación basada en consola (`ConsoleUI`).
- Se creará una nueva capa de presentación en WPF (`TodoApp` project).
- Se implementará el patrón MVVM (Model-View-ViewModel) para desacoplar la lógica de la vista.
- Se integrará un contenedor de Inyección de Dependencias (`Microsoft.Extensions.DependencyInjection`) para gestionar los servicios y ViewModels.
- Se mantendrá y reutilizará la lógica de negocio existente (`Models`, `Data`, `Services`) que ya ha sido migrada a una biblioteca de clases o integrada en el nuevo proyecto.

## Capabilities

### New Capabilities
<!-- Capabilities being introduced. Replace <name> with kebab-case identifier (e.g., user-auth, data-export, api-rate-limiting). Each creates specs/<name>/spec.md -->
- `gestion-ui-tareas`: Interfaz gráfica completa para listar, filtrar, crear, completar y eliminar tareas, con soporte visual para prioridades y estados.

### Modified Capabilities
<!-- Existing capabilities whose REQUIREMENTS are changing (not just implementation).
     Only list here if spec-level behavior changes. Each needs a delta spec file.
     Use existing spec names from openspec/specs/. Leave empty if no requirement changes. -->

## Impact

- **Código Estructural**: Se reemplaza `Program.cs` de consola por `App.xaml` y `App.xaml.cs` con Host Builder.
- **Dependencias**: Se añaden paquetes NuGet de WPF y MVVM Toolkit (`CommunityToolkit.Mvvm`, `Microsoft.Extensions.DependencyInjection`, `Microsoft.Extensions.Hosting`).
- **Archivos**: Eliminación de archivos específicos de consola y reestructuración de carpetas.
- **Base de Datos**: La estructura de la base de datos SQLite se mantiene intacta, asegurando compatibilidad de datos.
