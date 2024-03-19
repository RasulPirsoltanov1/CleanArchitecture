using UI.Models;

namespace UI.Areas.Admin.Models;

public class RoleDetailsVM
{
    public ApplicationRole? Role { get; set; }
    public List<ApplicationUser>? Members { get; set; }
    public List<ApplicationUser>? NonMembers { get; set; }
}


public class RoleEditDto
{
    public string RoleName { get; set; } = null!; 
    public string Email { get; set; } = null!; 
}