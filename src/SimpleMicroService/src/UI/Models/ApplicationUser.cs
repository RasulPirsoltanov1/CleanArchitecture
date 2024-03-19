using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UI.Models;

public class ApplicationUser : IdentityUser<int>
{
    [MaxLength(100)]
    public string? ApiKey { get; set; }
    [MaxLength(100)]
    public string? SecretKey { get; set; }
}



public class ApplicationRole : IdentityRole<int>
{
    public DateTime? ExpireDate { get; set; }
}
