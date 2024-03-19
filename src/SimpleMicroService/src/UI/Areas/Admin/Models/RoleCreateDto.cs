namespace UI.Areas.Admin.Models;
public class RoleCreateDto
{
    public string Name { get; set; } = null!;
    public DateTime? ExpireDate { get; set; }
}