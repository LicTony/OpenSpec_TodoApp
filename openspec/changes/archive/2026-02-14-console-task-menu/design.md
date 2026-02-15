# Design: Console Task Menu

## Context

La aplicación es una consola .NET 10 minimalista. Actualmente solo contiene código boilerplate. Necesitamos implementar un menú interactivo con gestión de tareas en memoria.

## Goals / Non-Goals

**Goals:**
- Implementar menú interactivo de consola
- Operaciones CRUD básicas para tareas
- Validación de entrada robusta
- Código limpio y mantenible

**Non-Goals:**
- Persistencia de datos (archivos, base de datos)
- Autenticación de usuarios
- Interfaz gráfica
- Tareas con fechas, prioridades o categorías

## Decisions

### Decision 1: Estructura del código

**Enfoque:** Todo en `Program.cs` usando top-level statements y funciones locales.

**Rationale:** 
- La aplicación es pequeña (~80 líneas)
- No justifica crear clases separadas
- Top-level statements mantienen el código limpio
- Funciones locales para cada operación mejoran legibilidad

### Decision 2: Almacenamiento de tareas

**Enfoque:** Usar `List<string>` para almacenar tareas en memoria.

**Rationale:**
- Simple y suficiente para el scope actual
- Fácil de modificar cuando agreguemos persistencia
- Los strings son suficientes para descripciones de tareas

### Decision 3: Manejo de entrada

**Enfoque:** Usar `Console.ReadLine()` con validación inmediata y mensajes de error claros.

**Rationale:**
- Validación en el punto de entrada evita errores en cascada
- Mensajes en español para consistencia con el menú existente
- Loop hasta entrada válida para opciones de menú

### Decision 4: Flujo del programa

**Enfoque:** Loop infinito con switch expression, salir con `return`.

**Rationale:**
- Patrón común en aplicaciones de consola
- Switch expression es más limpio que switch statement
- `return` termina el programa limpiamente

## Code Structure

```
Program.cs
├── List<string> tareas (estado)
├── Main loop
│   ├── MostrarMenú()
│   ├── LeerOpción() con validación
│   └── Switch → Agregar | Ver | Eliminar | Salir
├── AgregarTarea()
├── VerTareas()
└── EliminarTarea()
```
