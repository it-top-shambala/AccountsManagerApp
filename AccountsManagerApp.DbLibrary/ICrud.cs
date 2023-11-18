using AccountsManagerApp.Models;

namespace AccountsManagerApp.DbLibrary;

public interface ICrud
{
    public IEnumerable<Account> GetAll();
    public Account? GetById(int id);
    public bool Update(Account account);
    public bool Insert(Account account);
    public bool Delete(int id);
}