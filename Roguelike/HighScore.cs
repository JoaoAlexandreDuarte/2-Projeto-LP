namespace Roguelike {
    public class HighScore {
        // Creates a propriety of a string Name
        public string Name { get; set; }

        // Creates a propriety of a string Score
        public int Score { get; set; }

        public HighScore(string name, int score) {
            Name = name;
            Score = score;
        }
    }
}
