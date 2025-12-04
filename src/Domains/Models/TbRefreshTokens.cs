namespace Abyat.Domains.Models;

public class TbRefreshToken : BaseTable
{
    public string Token { get; set; }

    public Guid UserId { get; set; }

    public DateTime Expires { get; set; }
}
