using System.Windows.Controls;

namespace AccountsManagerApp.WPF.Components.InputComponent;

public partial class InputComponent : UserControl
{
    public string Title { get; set; }
    public InputComponent()
    {
        InitializeComponent();

        this.DataContext = this;
    }
}