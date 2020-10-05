using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace AdventOfCode
{

    class Config
    {

        string _c;
        int _y;
        int[] _d;

        public string Cookie
        {
            get => _c;
            set
            {
                if(Regex.IsMatch(value, "^session=[a-z0-9]+$")) _c = value;
            }
        }
        public int Year
        {
            get => _y;
            set
            {
                if(value >= 2015 && value <= DateTime.Now.Year) _y = value;
            }
        }
        public int[] Days
        {
            get => _d;
            set
            {
                bool allDaysCovered = false;
                _d = value.Where(v =>
                {
                    if(v == 0) allDaysCovered = true;
                    return v > 0 && v < 26;
                }).ToArray();

                if(allDaysCovered)
                {
                    _d = new int[] { 0 };
                }
                else
                {
                    Array.Sort(_d);
                }
            }
        }

        void setDefaults()
        {
            if(Cookie == default(string)) Cookie = "";
            if(Year == default(int)) Year = DateTime.Now.Year;
            if(Days == default(int[])) Days = (DateTime.Now.Month == 12) ? new int[] { DateTime.Now.Day } : new int[] { 0 };
        }

        public static Config Get(string path)
        {
            var options = new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            Config config;
            if(File.Exists(path))
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
}
