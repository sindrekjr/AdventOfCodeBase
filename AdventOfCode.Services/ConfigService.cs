using AdventOfCode.Services.Models;

namespace AdventOfCode.Services
{
    public static class ConfigService
    {
        static Config Config = Config.Get("config.json");

        public static string GetCookie() => Config.Cookie;

        public static int GetYear() => Config.Year;

        public static int[] GetDays() => Config.Days;
    }
}
