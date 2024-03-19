namespace UI.Areas.Admin.Models;

public class LoginUser
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; }
}
