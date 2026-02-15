using System.Windows;

namespace TodoApp.Services;

public class DialogService : IDialogService
{
    public bool Confirm(string message, string title)
    {
        var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
        return result == MessageBoxResult.Yes;
    }

    public void Alert(string message, string title)
    {
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public string? Prompt(string message, string title, string defaultValue = "")
    {
        var inputDialog = new InputDialog(message, title, defaultValue);
        if (inputDialog.ShowDialog() == true)
        {
            return inputDialog.ResponseText;
        }
        return null;
    }
    
    public (bool Confirmed, string Descripcion, Models.Prioridad Prioridad, bool Completada) ShowEditTaskDialog(string descripcion, Models.Prioridad prioridad, bool completada)
    {
        var dialog = new TaskDialog(descripcion, prioridad, completada);
        if (dialog.ShowDialog() == true)
        {
            return (true, dialog.Descripcion, dialog.Prioridad, dialog.Completada);
        }
        return (false, string.Empty, Models.Prioridad.Media, false);
    }
}
