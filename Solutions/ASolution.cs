namespace AdventOfCode.Solutions {

    abstract class ASolution {

        public int day;
        public string title;
        public string input;

        protected ASolution(int day, string title) {
            this.day = day;
            this.title = title;
        }

        protected abstract string PartOne();
        protected abstract string PartTwo();

        public int GetDay() => this.day;
        public string GetTitle() => this.title;
        protected string GetInput() => this.input;

    }

    enum Part { All, One, Two }
}
