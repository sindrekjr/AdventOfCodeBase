using System; 
using System.Collections; 
using System.Collections.Generic; 
using System.Linq; 
using System.Reflection; 

namespace AdventOfCode.Solutions {
    class SolutionCollector : IEnumerable<ASolution> {

        IEnumerable<ASolution> Solutions;

        public SolutionCollector(int year, int[] days) => Solutions = LoadSolutions(year, days);

        public ASolution GetSolution(int day) {
            try {
                return Solutions.Single(s => s.Day == day);
            } catch(InvalidOperationException) {
                return null; 
            }
        }

        public IEnumerator<ASolution> GetEnumerator() {
            return Solutions.GetEnumerator(); 
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator(); 
        }

        IEnumerable<ASolution> LoadSolutions(int year, int[] days) {
            if(days.Sum() == 0) {
                IEnumerable<Type> solutions = Assembly
                    .GetExecutingAssembly()
                    .GetTypes()
                    .Where(type => type.Namespace == $"AdventOfCode.Solutions.Year{year}");
                foreach(Type solution in solutions) {
                    yield return (ASolution) Activator.CreateInstance(solution);
                }
            } else {
                foreach(int day in days) {
                    Type solution = Type.GetType($"AdventOfCode.Solutions.Year{year}.Day{day.ToString("D2")}");
                    if(solution != null) {
                        yield return (ASolution) Activator.CreateInstance(solution); 
                    }
                }
            }
        }
    }
}
