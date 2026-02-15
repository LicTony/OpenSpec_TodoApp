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
    private Prioridad _nuevaTareaPrioridad = Prioridad.Media;

    [ObservableProperty]
    private FiltroTareas _filtroActual = FiltroTareas.Todas;

    [ObservableProperty]
    private OrdenTareas _ordenActual = OrdenTareas.FechaCreacion;

    [ObservableProperty]
    private ObservableCollection<Tarea> _tareas;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalPaginas))]
    [NotifyCanExecuteChangedFor(nameof(PaginaSiguienteCommand))]
    [NotifyCanExecuteChangedFor(nameof(PaginaAnteriorCommand))]
    private int _paginaActual = 1;

    [ObservableProperty]
    private int _tamañoPagina = 10;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalPaginas))]
    [NotifyCanExecuteChangedFor(nameof(PaginaSiguienteCommand))]
    private int _totalRegistros;

    public int TotalPaginas => (int)Math.Ceiling((double)TotalRegistros / TamañoPagina);

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
        var resultado = _tareaService.ObtenerTareas(FiltroActual, OrdenActual, PaginaActual, TamañoPagina);
        Tareas = new ObservableCollection<Tarea>(resultado.Items);
        TotalRegistros = resultado.TotalRegistros;
    }

    [RelayCommand(CanExecute = nameof(PuedeIrAPaginaAnterior))]
    private void PaginaAnterior()
    {
        if (PaginaActual > 1)
        {
            PaginaActual--;
            CargarTareas();
        }
    }

    private bool PuedeIrAPaginaAnterior() => PaginaActual > 1;

    [RelayCommand(CanExecute = nameof(PuedeIrAPaginaSiguiente))]
    private void PaginaSiguiente()
    {
        if (PaginaActual < TotalPaginas)
        {
            PaginaActual++;
            CargarTareas();
        }
    }

    private bool PuedeIrAPaginaSiguiente() => PaginaActual < TotalPaginas;

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
            _tareaService.CrearTarea(NuevaTareaDescripcion, NuevaTareaPrioridad);
            NuevaTareaDescripcion = string.Empty;
            NuevaTareaPrioridad = Prioridad.Media;
            // Al agregar una nueva tarea, usualmente queremos verla, 
            // pero si estamos en otra página podría no aparecer.
            // Por simplicidad, recargamos la página actual.
            CargarTareas();
        }
        catch (Exception ex)
        {
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

        var result = _dialogService.ShowEditTaskDialog(
            tarea.Descripcion,
            tarea.Prioridad,
            tarea.Completada);

        if (result.Confirmed)
        {
            try
            {
                _tareaService.ActualizarTarea(tarea.Id, result.Descripcion, result.Prioridad, result.Completada);
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
        PaginaActual = 1;
        CargarTareas();
    }

    partial void OnOrdenActualChanged(OrdenTareas value)
    {
        PaginaActual = 1;
        CargarTareas();
    }
}
