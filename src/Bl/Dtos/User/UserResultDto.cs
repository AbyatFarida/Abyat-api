namespace Abyat.Bl.Dtos.User;

public class UserResultDto
{
    public bool Success { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    public string? Email { get; set; }
    public int UserId { get; set; }
    public bool Requires2FA { get; set; }
}
