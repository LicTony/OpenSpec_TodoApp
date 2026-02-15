namespace TodoApp.Services;

public interface IDialogService
{
    bool Confirm(string message, string title);
    void Alert(string message, string title);
    string? Prompt(string message, string title, string defaultValue = "");
    (bool Confirmed, string Descripcion, Models.Prioridad Prioridad, bool Completada) ShowEditTaskDialog(string descripcion, Models.Prioridad prioridad, bool completada);
}
