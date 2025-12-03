namespace Abyat.Domains.Models;

public class TbRefreshToken : BaseTable
{
    public string Token { get; set; }

    public int UserId { get; set; }

    public DateTime Expires { get; set; }
}
