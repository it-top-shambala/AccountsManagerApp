using AccountsManagerApp.DbLibrary;
using AccountsManagerApp.Models;

namespace AccountsManagerApp.AccountsManager;

public class AccountManager : ICrud
{
    private readonly ICrud _context;

    public AccountManager(ICrud context)
    {
        _context = context;
    }

    public IEnumerable<Account> GetAll()
    {
        return _context.GetAll();
    }

    public Account? GetById(int id)
    {
        return _context.GetById(id);
    }

    public bool Update(Account account)
    {
        return _context.Update(account);
    }

    public bool Insert(Account account)
    {
        return _context.Insert(account);
    }

    public bool Delete(int id)
    {
        return _context.Delete(id);
    }
}