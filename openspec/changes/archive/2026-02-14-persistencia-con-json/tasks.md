## 1. Setup

- [x] 1.1 Agregar `using System.Text.Json;` al inicio de [`Program.cs`](Program.cs)
- [x] 1.2 Definir constante para el nombre del archivo: `const string ArchivoTareas = "tareas.json";`

## 2. Implementar Carga de Tareas

- [x] 2.1 Crear función `CargarTareas()` que lee el archivo JSON
- [x] 2.2 Manejar caso donde el archivo no existe (retornar lista vacía)
- [x] 2.3 Manejar caso donde el JSON está corrupto (retornar lista vacía con try-catch)
- [x] 2.4 Inicializar la lista `tareas` llamando a `CargarTareas()` al inicio

## 3. Implementar Guardado de Tareas

- [x] 3.1 Crear función `GuardarTareas()` que escribe la lista al archivo JSON
- [x] 3.2 Llamar `GuardarTareas()` al final de `AgregarTarea()`
- [x] 3.3 Llamar `GuardarTareas()` al final de `EliminarTarea()`

## 4. Agregar Confirmación de Eliminación

- [x] 4.1 Modificar `EliminarTarea()` para pedir confirmación antes de eliminar
- [x] 4.2 Preguntar "¿Estás seguro de eliminar la tarea X? (s/n): "
- [x] 4.3 Solo eliminar si el usuario responde 's' o 'S'
- [x] 4.4 Mostrar mensaje de cancelación si el usuario responde 'n' o 'N'

## 5. Limpieza

- [x] 5.1 Eliminar los comentarios TODO del código que ya no son necesarios
- [x] 5.2 Verificar que la aplicación compila sin errores

## 6. Verificación

- [x] 6.1 Ejecutar la aplicación y verificar que carga tareas existentes
- [x] 6.2 Agregar una tarea y verificar que persiste al reiniciar
- [x] 6.3 Eliminar una tarea y verificar que el cambio persiste al reiniciar
- [x] 6.4 Verificar que funciona correctamente cuando no existe el archivo JSON
