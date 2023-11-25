namespace AccountsManagerApp.Models;

public enum AccountRole
{
    Unknown, User, Admin
}

public class Account
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Patronymic { get; set; }
    public string Email { get; set; }
    public bool IsDelete { get; set; }
    public AccountRole Role { get; set; }
    public byte[]? Image { get; set; }
}