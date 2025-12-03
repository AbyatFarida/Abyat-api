namespace Abyat.Bl.Settings;

// Serilog config is usually bound directly via UseSerilog(), 
// but if you want to capture parts of it, you can also model it:
public class SerilogSettings
{
    public Dictionary<string, string>? MinimumLevel { get; set; }

    public List<string>? Using { get; set; }

    public List<object>? WriteTo { get; set; }

    public List<string>? Enrich { get; set; }
}
