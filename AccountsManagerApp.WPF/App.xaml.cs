using System.IO;
using System.Windows;
using AccountsManagerApp.AccountsManager;
using AccountsManagerApp.DbLibrary;
using Microsoft.Extensions.Configuration;

namespace AccountsManagerApp.WPF
{
    public partial class App : Application
    {
        public App()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");
            
            Resources.Add("Context", new AccountManager(new AccountContext(connectionString)));
        }
    }
}