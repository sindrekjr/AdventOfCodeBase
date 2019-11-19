using AdventOfCode.Solutions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace AdventOfCode {

    class Program {

        public static readonly Config config = JsonSerializer.Deserialize<Config>(File.ReadAllText("config.json").ToLower());
        static IEnumerable<ASolution> solutions = GetAllSolutions();

        static void Main(string[] args) {
            if(string.IsNullOrWhiteSpace(config.cookie)) return;

            if(config.day == 0) {
                foreach(ASolution s in solutions) {
                    Console.WriteLine(s.Solve()); 
                }
            } else {
                Console.WriteLine(GetSolution(config.day).Solve());
            }
        }

        private static ASolution GetSolution(int day) {
            return solutions.Single(x => x.Day == day);
        }
        private static IEnumerable<ASolution> GetAllSolutions() {
            foreach(Type type in Assembly.GetExecutingAssembly().GetTypes().Where(type => type.Namespace == $"AdventOfCode.Solutions.Year{config.year}")) {
                yield return (ASolution) Activator.CreateInstance(type);
            }
        }
    }

    internal class Config {
        private string _c; 
        private int _y;
        private int _d;
        private int _p; 

        public string cookie { 
            get => _c;
            set {
                if(Regex.IsMatch(value, "^session=[a-z0-9]+$")) {
                    _c = value; 
                } else {
                    _c = null;  
                }
            }
        }
        public int year { 
            get => _y;
            set {
                if(value < 2015 || value > DateTime.Now.Year) {
                    _y = DateTime.Now.Year; 
                    Console.WriteLine("Invalid year supplied; set to current year {} by default.", _y);
                } else {
                    _y = value; 
                }
            } 
        }
        public int day { 
            get => _d;
            set {
                if(value < 0 || value > 25) {
                    _d = (DateTime.Now.Month == 12) ? DateTime.Now.Day : 0; 
                    Console.WriteLine("Invalid day supplied; set to {} by default", _d);
                } else {
                    _d = value; 
                }
            }
        }
        public int part { 
            get => _p;
            set {
                if(value < 0 || value > 2) {
                    _p = 0; 
                    Console.WriteLine("Invalid part value supplied; set to {} by default.", _p); 
                } else {
                    _p = value; 
                }
            }
        }
    }
}
