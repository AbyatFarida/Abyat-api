using Microsoft.AspNetCore.Identity;
using static Abyat.Core.Enums.Status.Status;

namespace Abyat.Da.Identity;

public class AppUser : IdentityUser<Guid>
{
    public AppUser() : base() { }

    public AppUser(string userName) : base(userName) { }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public enCurrentState CurrentState { get; set; }

}