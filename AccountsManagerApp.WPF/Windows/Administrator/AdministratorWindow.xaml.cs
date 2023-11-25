using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AccountsManagerApp.AccountsManager;
using AccountsManagerApp.Models;
using Microsoft.Win32;

namespace AccountsManagerApp.WPF.Windows.Administrator;

public partial class AdministratorWindow : Window
{
    private byte[] _image;
    private readonly AccountManager _accountManager;
    public ObservableCollection<Account> Accounts { get; set; }
    public AdministratorWindow()
    {
        _image = null;
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
        Check_IsDelete.IsChecked = item.IsDelete;
        Image_UserPic.Source = LoadImage(item.Image);
    }

    private void DeleteAccount(object sender, ExecutedRoutedEventArgs e)
    {
        var id = (AccountList.SelectedItem as Account)?.Id;
        if (id is null)
        {
            Message.ErrorMessage("Аккаунт не найден");
            return;
        }
        var result = _accountManager.Delete((int)id);

        if (!result)
        {
            Message.ErrorMessage("Аккаунт не удалён");
        }
        else
        {
            Message.SuccessMessage("Аккаунт успешно удалён");
        }
    }

    private void SaveAccount(object sender, ExecutedRoutedEventArgs e)
    {
        var selectedAccount = AccountList.SelectedItem as Account;
        if (selectedAccount is null)
        {
            Message.ErrorMessage("Аккаунт не обновлён");
            return;
        }

        var name = Input_Name.Text.Split(' ');
        var account = new Account()
        {
            Id = selectedAccount.Id,
            LastName = name[0],
            FirstName = name[1],
            Patronymic = name[2],
            Login = Input_Login.Text,
            Password = Input_Password.Text,
            Email = Input_Email.Text,
            IsDelete = (bool)Check_IsDelete.IsChecked,
            Role = selectedAccount.Role,
            Image = _image
        };
        
        var result = _accountManager.Update(account);

        if (!result)
        {
            Message.ErrorMessage("Аккаунт не обновлён");
        }
        else
        {
            Message.SuccessMessage("Аккаунт успешно обновлён");
        }
    }

    private void Clear(object sender, ExecutedRoutedEventArgs e)
    {
        Input_Login.Clear();
        Input_Password.Clear();
        Input_Name.Clear();
        Input_Email.Clear();
        Check_IsDelete.IsChecked = false;
        _image = null;
        Image_UserPic.Source = null;

        AccountList.SelectedItem = null;
    }

    private void OpenPicture(object sender, ExecutedRoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "Images (.jpg)|*.jpg|All|*.*"
        };
        openFileDialog.ShowDialog();
        _image = File.ReadAllBytes(openFileDialog.FileName);
        Image_UserPic.Source = LoadImage(_image);
    }
    
    private static BitmapImage LoadImage(byte[] imageData)
    {
        if (imageData == null || imageData.Length == 0) return null;
        var image = new BitmapImage();
        using (var mem = new MemoryStream(imageData))
        {
            mem.Position = 0;
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = null;
            image.StreamSource = mem;
            image.EndInit();
        }
        image.Freeze();
        return image;
    }
}