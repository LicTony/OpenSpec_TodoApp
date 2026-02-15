using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoApp.Data;
using TodoApp.Models;
using TodoApp.Services;
using TodoApp.ViewModels;

namespace TodoApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    // Host genérico que gestiona la DI, configuración y logging
    private IHost? _host;

    public App()
    {
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Construir el Host
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Capa de Datos
                services.AddSingleton<TareaRepository>(); // Singleton porque la conexión se abre/cierra per method
                services.AddSingleton<DatabaseInitializer>();

                // Capa de Servicios
                services.AddTransient<TareaService>();
                services.AddSingleton<IDialogService, DialogService>();

                // Capa de UI (ViewModels y Ventanas)
                services.AddTransient<MainViewModel>();
                services.AddTransient<MainWindow>();
            })
            .Build();

        await _host.StartAsync();

        // Inicializar BD
        var dbInit = _host.Services.GetRequiredService<DatabaseInitializer>();
        dbInit.Inicializar();

        // Mostrar Ventana Principal
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        using (_host)
        {
            if (_host != null)
            {
                await _host.StopAsync();
            }
        }
        base.OnExit(e);
    }
}
