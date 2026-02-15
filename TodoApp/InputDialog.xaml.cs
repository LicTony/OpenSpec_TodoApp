using System.Windows;

namespace TodoApp;

public partial class InputDialog : Window
{
    public string ResponseText { get; private set; } = string.Empty;

    public InputDialog(string message, string title, string defaultValue = "")
    {
        InitializeComponent();
        Title = title;
        lblMessage.Text = message;
        txtInput.Text = defaultValue;
        txtInput.SelectAll();
        txtInput.Focus();
    }

    private void btnOk_Click(object sender, RoutedEventArgs e)
    {
        ResponseText = txtInput.Text;
        DialogResult = true;
    }

    private void btnCancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}
