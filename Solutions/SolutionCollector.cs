using System; 
using System.Collections; 
using System.Collections.Generic; 
using System.Linq; 
using System.Reflection; 

namespace AdventOfCode.Solutions {
    class SolutionCollector : IEnumerable<ASolution> {

        IEnumerable<ASolution> Solutions;

        public SolutionCollector(int year) => Solutions = LoadSolutions(year);

        public ASolution GetSolution(int day) {
            return Solutions.Single(s => s.Day == day);
        }

        public IEnumerator<ASolution> GetEnumerator() {
            return Solutions.GetEnumerator(); 
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator(); 
        }

        IEnumerable<ASolution> LoadSolutions(int year) {
            foreach(Type type in Assembly.GetExecutingAssembly().GetTypes().Where(type => type.Namespace == $"AdventOfCode.Solutions.Year{year}")) {
                yield return (ASolution) Activator.CreateInstance(type);
            }
        }
    }
}
