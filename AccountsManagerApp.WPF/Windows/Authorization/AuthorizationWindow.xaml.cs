﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using AccountsManagerApp.WPF.Windows.Administrator;

namespace AccountsManagerApp.WPF.Windows.Authorization;

public partial class AuthorizationWindow : Window
{
    public AuthorizationWindow()
    {
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
        var administratorWindow = new AdministratorWindow();
        administratorWindow.Show();
        this.Close();
    }

    private void Clear(object sender, RoutedEventArgs e)
    {
        Input_Login.Clear();
        Input_Password.Clear();
    }
    
    
}