using System; 
using System.Collections.Generic; 
using System.IO; 
using System.Linq; 
using System.Text.Json; 
using System.Text.Json.Serialization; 
using System.Text.RegularExpressions; 

namespace AdventOfCode {
    class Config {
        string _c; 
        int _y;
        double[] _p;

        public string Cookie { 
            get => _c;
            set {
                if(Regex.IsMatch(value, "^session=[a-z0-9]+$")) {
                    _c = value; 
                } else {
                    _c = "";
                }
            }
        }
        public int Year { 
            get => _y;
            set {
                if(value < 2015 || value > DateTime.Now.Year) {
                    _y = DateTime.Now.Year; 
                    Console.WriteLine($"Invalid year supplied; set to current year {_y} by default.");
                } else {
                    _y = value; 
                }
            } 
        }
        [JsonPropertyName("days")]
        public double[] Puzzles { 
            get => _p;
            set {
                IList<double> intermediate = new List<double>(); 
                bool allDaysCovered = false, all1 = false, all2 = false;
                for(int i = 0; i < value.Length; i++) {
                    double day = value[i]; 
                    if(day == 0 || (all1 && all2)) {
                        allDaysCovered = true; 
                    } else if(day == 0.1) {
                        all1 = true; 
                    } else if(day == 0.2) {
                        all2 = true; 
                    } else {
                        intermediate.Add(day);
                    }
                }

                _p = intermediate.ToArray<double>(); 
                
                double[] allDays = (from n in Enumerable.Range(1,25) select (double) n).ToArray<double>();
                if(allDaysCovered) {
                    _p = _p.Union(allDays).ToArray<double>(); 
                } else if(all1) {
                    _p = _p.Union(from n in allDays select n + 0.1).ToArray<double>(); 
                } else if(all2) {
                    _p = _p.Union(from n in allDays select n + 0.2).ToArray<double>(); 
                }

                Array.Sort(_p);
            }
        }
        
        public static Config Get(string path) {
            JsonSerializerOptions options = new JsonSerializerOptions(){
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            Config config; 
            if(File.Exists(path)) {
                config = JsonSerializer.Deserialize<Config>(File.ReadAllText(path), options);
            } else {
                config = new Config();
                config.Cookie = ""; 
                config.Year = DateTime.Now.Year;
                config.Puzzles = (DateTime.Now.Month == 12) ? new double[]{DateTime.Now.Day} : new double[]{0}; 
                
                File.WriteAllText(path, JsonSerializer.Serialize<Config>(config, options));
            }
            return config; 
        }

        public bool Verify() => !string.IsNullOrWhiteSpace(Cookie);
    }
}
