using Microsoft.AspNetCore.Identity;

namespace Abyat.Da.Identity;

public class AppRole : IdentityRole<Guid>
{
    public AppRole() : base() { }

    public AppRole(string roleName) : base(roleName) { }

}