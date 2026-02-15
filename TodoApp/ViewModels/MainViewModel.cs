using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.ViewModels;

public sealed partial class MainViewModel : ObservableObject
{
    private readonly TareaService _tareaService;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    private string _nuevaTareaDescripcion = string.Empty;

    [ObservableProperty]
    private FiltroTareas _filtroActual = FiltroTareas.Todas;

    [ObservableProperty]
    private ObservableCollection<Tarea> _tareas;

    public MainViewModel(TareaService tareaService, IDialogService dialogService)
    {
        _tareaService = tareaService;
        _dialogService = dialogService;
        _tareas = new ObservableCollection<Tarea>();
        CargarTareas();
    }

    [RelayCommand]
    private void CargarTareas()
    {
        var tareas = _tareaService.ObtenerTareas(FiltroActual);
        Tareas = new ObservableCollection<Tarea>(tareas);
    }

    [RelayCommand]
    private void AgregarTarea()
    {
        if (string.IsNullOrWhiteSpace(NuevaTareaDescripcion))
        {
            _dialogService.Alert("Debe ingresar una descripción para la tarea.", "Aviso");
            return;
        }

        try
        {
            _tareaService.CrearTarea(NuevaTareaDescripcion);
            NuevaTareaDescripcion = string.Empty;
            CargarTareas();
        }
        catch (Exception ex)
        {
            // TODO: Manejar error, quizás con un servicio de diálogos
            System.Diagnostics.Debug.WriteLine($"Error al crear tarea: {ex.Message}");
            _dialogService.Alert($"No se pudo crear la tarea: {ex.Message}", "Error");
        }
    }

    [RelayCommand]
    private void EliminarTarea(Tarea? tarea)
    {
        if (tarea == null) return;

        if (_dialogService.Confirm("¿Está seguro de que desea eliminar esta tarea?", "Confirmar Eliminación"))
        {
            _tareaService.EliminarTarea(tarea.Id);
            CargarTareas();
        }
    }

    [RelayCommand]
    private void EditarTarea(Tarea? tarea)
    {
        if (tarea == null) return;

        var nuevaDescripcion = _dialogService.Prompt(
            "Ingrese la nueva descripción:",
            "Editar Tarea",
            tarea.Descripcion);

        if (!string.IsNullOrWhiteSpace(nuevaDescripcion) && nuevaDescripcion != tarea.Descripcion)
        {
            try
            {
                _tareaService.ActualizarDescripcion(tarea.Id, nuevaDescripcion);
                CargarTareas();
            }
            catch (Exception ex)
            {
                _dialogService.Alert($"Error al actualizar la tarea: {ex.Message}", "Error");
            }
        }
    }

    [RelayCommand]
    private void CambiarEstadoTarea(Tarea? tarea)
    {
        if (tarea == null) return;

        _tareaService.CambiarEstadoTarea(tarea.Id);
        CargarTareas();
    }

    partial void OnFiltroActualChanged(FiltroTareas value)
    {
        CargarTareas();
    }
}
