namespace Abyat.Bl.Settings;

/// <summary>
/// Root settings class to hold all configuration.
/// </summary>
public class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; set; } = new ConnectionStrings();

    public StripeSettings Stripe { get; set; } = new StripeSettings();

    public ApplicationSettings ApplicationSettings { get; set; } = new ApplicationSettings();

    public EmailSettings Email { get; set; } = new EmailSettings();

    public JwtSettings Jwt { get; set; } = new JwtSettings();

    public string AppUrl { get; set; } = string.Empty;

    public SerilogSettings Serilog { get; set; } = new SerilogSettings();
}
