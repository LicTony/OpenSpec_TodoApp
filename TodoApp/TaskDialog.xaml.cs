using System.Windows;
using TodoApp.Models;

namespace TodoApp;

public partial class TaskDialog : Window
{
    public string Descripcion { get; private set; } = string.Empty;
    public Prioridad Prioridad { get; private set; }
    public bool Completada { get; private set; }

    public TaskDialog(string descripcion, Prioridad prioridad, bool completada)
    {
        InitializeComponent();
        
        txtDescripcion.Text = descripcion;
        cmbPrioridad.SelectedItem = prioridad;
        chkCompletada.IsChecked = completada;

        txtDescripcion.Focus();
        txtDescripcion.SelectAll();
    }

    private void btnOk_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
        {
            MessageBox.Show("La descripción no puede estar vacía.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        Descripcion = txtDescripcion.Text;
        if (cmbPrioridad.SelectedItem is Prioridad p)
        {
            Prioridad = p;
        }
        Completada = chkCompletada.IsChecked ?? false;

        DialogResult = true;
    }

    private void btnCancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}
