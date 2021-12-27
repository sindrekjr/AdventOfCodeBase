using System.Text.Json;
using System.Text.Json.Serialization;
using AdventOfCode.Services.Models;

namespace AdventOfCode.Services;

public static class ConfigurationService
{
    static Config Config = GetConfig();

    public static string GetCookie() => Config.Cookie;

    public static int GetYear() => Config.Year;

    public static int[] GetDays() => Config.Days;

    private static Config GetConfig(string path = "config.json")
    {
        var options = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };
        Config config;
        if (File.Exists(path))
        {
            config = JsonSerializer.Deserialize<Config>(File.ReadAllText(path), options);
            config.setDefaults();
        }
        else
        {
            config = new Config();
            config.setDefaults();
            File.WriteAllText(path, JsonSerializer.Serialize<Config>(config, options));
        }

        return config;
    }
}
