using AccountsManagerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountsManagerApp.DbLibrary;

public class AccountContext : DbContext, ICrud
{
    public DbSet<Account> Accounts { get; set; }
    
    private readonly string _connectionString;
    
    public AccountContext(string connectionString)
    {
        _connectionString = connectionString;

        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }

    public IEnumerable<Account> GetAll()
    {
        return Accounts;
    }

    public Account? GetById(int id)
    {
        return Accounts.SingleOrDefault(a => a.Id == id);
    }

    public bool Update(Account account)
    {
        var _account = GetById(account.Id);
        if (_account is null) return false;

        _account = account;
        SaveChanges();
        return true;
    }

    public bool Insert(Account account)
    {
        Accounts.Add(account);
        SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var account = GetById(id);
        if (account is null) return false;
        
        account.IsDelete = true;
        SaveChanges();
        return true;
    }
}