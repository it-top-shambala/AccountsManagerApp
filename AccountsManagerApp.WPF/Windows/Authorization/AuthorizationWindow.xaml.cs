using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AccountsManagerApp.AccountsManager;
using AccountsManagerApp.Models;
using AccountsManagerApp.WPF.Windows.Administrator;
using AccountsManagerApp.WPF.Windows.User;

namespace AccountsManagerApp.WPF.Windows.Authorization;

public partial class AuthorizationWindow : Window
{
    private string _password;
    private readonly AccountManager _accountManager;
    
    public AuthorizationWindow()
    {
        _accountManager = (AccountManager)Application.Current.Resources["Context"];
        
        InitializeComponent();
    }

    private void WindowClosing(object? sender, CancelEventArgs args)
    {
        var result = MessageBox.Show(
            caption: Application.Current.Resources["AppTitle"]?.ToString(),
            messageBoxText: "Вы действительно хотите закрыть приложение?",
            button: MessageBoxButton.YesNo,
            icon: MessageBoxImage.Stop);
        
        if (result == MessageBoxResult.No)
        {
            args.Cancel = true;
            this.Closing -= WindowClosing;
        }
    }

    private void Border_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        this.DragMove();
    }

    private void Close(object sender, RoutedEventArgs e)
    {
        this.Closing += WindowClosing;
        this.Close();
    }

    private void LogIn(object sender, RoutedEventArgs e)
    {
        var login = Input_Login.Text;
        var password = _password;

        var accounts = _accountManager.GetAll();
        var account = accounts.SingleOrDefault(a => a.Login == login && a.Password == password);

        switch (account?.Role)
        {
            case AccountRole.User:
                var userWindow = new UserWindow();
                userWindow.Show();
                this.Close();
                break;
            case AccountRole.Admin:
                var administratorWindow = new AdministratorWindow();
                administratorWindow.Show();
                this.Close();
                break;
            case AccountRole.Unknown:
            case null:
                MessageBox.Show(
                    caption: Application.Current.Resources["AppTitle"]?.ToString(),
                    messageBoxText: "Вы ввели неверные данные",
                    button: MessageBoxButton.OK,
                    icon: MessageBoxImage.Error
                );
                break;
        }
    }

    private void Clear(object sender, RoutedEventArgs e)
    {
        Input_Login.Clear();
        Input_Password.Clear();
        _password = "";
    }

    private void Input_Password_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (Input_Password.Text.Length == 0) return;
        
        var length = Input_Password.Text.Length;
        _password += Input_Password.Text[(length - 1)..];
        var builder = "";
        for (int i = 0; i < length; i++)
        {
            builder += '+';
        }

        Input_Password.Text = builder;
        Input_Password.CaretIndex = length;
        _password = _password.Trim('+');
    }
}