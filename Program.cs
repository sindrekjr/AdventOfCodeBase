using AdventOfCode.Solutions;
using System;

namespace AdventOfCode {

    class Program {

        public static Config Config = Config.Get("config.json"); 
        static SolutionCollector Solutions = new SolutionCollector(Config.Year); 

        static void Main(string[] args) {
            if(!Config.Verify()) {
                Console.WriteLine("Config invalid.");
                return; 
            }

            for(int i = 0; i < Config.Puzzles.Length; i++) {
                TryOutput((int) Config.Puzzles[i], (int) (Config.Puzzles[i] * 10 % 10));
            }
        }

        static void TryOutput(int day, int part) {
            try {
                Console.WriteLine(Solutions.GetSolution(day).Solve(part)); 
            } catch(InvalidOperationException) {
                Console.WriteLine($"No solution for Day {day} exists."); 
            }
        }
    }
}
