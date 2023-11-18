using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using AccountsManagerApp.AccountsManager;
using AccountsManagerApp.Models;

namespace AccountsManagerApp.WPF.Windows.Administrator;

public partial class AdministratorWindow : Window
{
    private readonly AccountManager _accountManager;
    public ObservableCollection<Account> Accounts { get; set; }
    public AdministratorWindow()
    {
        _accountManager = (AccountManager)Application.Current.Resources["Context"];
        Accounts = new ObservableCollection<Account>(_accountManager.GetAll());
        
        InitializeComponent();
    }

    private void AccountList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (AccountList.SelectedItem is not Account item) return;
        
        Input_Login.Text = item.Login;
        Input_Password.Text = item.Password;
        Input_Name.Text = $"{item.LastName} {item.FirstName} {item.Patronymic}";
        Input_Email.Text = item.Email;
    }
}