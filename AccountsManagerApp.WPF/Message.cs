using System.Windows;

namespace AccountsManagerApp.WPF;

public static class Message
{
    private static void ShowMessage(string message, MessageBoxImage image)
    {
        MessageBox.Show(
            caption: Application.Current.Resources["AppTitle"]?.ToString(),
            messageBoxText: message,
            button: MessageBoxButton.OK,
            icon: image
        );
    }
    public static void ErrorMessage(string message)
    {
        ShowMessage(message, MessageBoxImage.Error);
    }
    
    public static void SuccessMessage(string message)
    {
        ShowMessage(message, MessageBoxImage.Information);
    }
}