using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Roguelike {
    /// <summary>
    /// File that writes and reads from files
    /// </summary>
    public class FileParser {
        /// <summary>
        /// List of available traps to be used by the game
        /// </summary>
        public List<Trap> listOfTraps;
        /// <summary>
        /// List of the high scores for this game
        /// </summary>
        public List<HighScore> listHighScores;
        /// <summary>
        /// List of the available weapons to be used by the game
        /// </summary>
        public List<Weapon> listOfWeapons;
        /// <summary>
        /// List of the available foods to be used by the game
        /// </summary>
        public List<Food> listOfFoods;

        /// <summary>
        /// Method that gets the necessary info from the data files
        /// </summary>
        public void ReadFromFiles() {
            string jsonTraps, jsonScores, jsonWeapons, jsonFoods;

            // Each data is saved in a corresponding data holder
            jsonTraps = ReadFile("../../Data/traps.json");
            listOfTraps = JsonConvert.DeserializeObject<List<Trap>>(jsonTraps);

            jsonScores = ReadFile("../../Data/highscores.json");
            listHighScores =
                JsonConvert.DeserializeObject<List<HighScore>>(jsonScores);

            jsonWeapons = ReadFile("../../Data/weapons.json");
            listOfWeapons = 
                JsonConvert.DeserializeObject<List<Weapon>>(jsonWeapons);

            jsonFoods = ReadFile("../../Data/foods.json");
            listOfFoods =
                JsonConvert.DeserializeObject<List<Food>>(jsonFoods);

            if (listHighScores == null) {
                listHighScores = new List<HighScore>();
            }
        }

        /// <summary>
        /// Method that updates the list of high scores
        /// </summary>
        /// <param name="hS">The high score to be added to the list</param>
        public void UpdateHighScores(HighScore hS) {

            // Adds the high score to the list
            listHighScores.Add(hS);

            // Sorts the list by descending order
            listHighScores.Sort((y, x) => x.Score.CompareTo(y.Score));

            // If the list has more than 10 elements removes the last one
            if (listHighScores.Count > 10) {
                listHighScores.RemoveAt(10);
            }


            // Writes the new updated data to the file
            string jsonStr = JsonConvert.SerializeObject(listHighScores);
            WriteFile("../../Data/highscores.json", jsonStr);
        }

        /// <summary>
        /// Method that reads from a given file
        /// </summary>
        /// <param name="filepath">The path to the given file</param>
        /// <returns>A string with all the data from the file</returns>
        private string ReadFile(string filepath) {
            using (var file = File.Open(filepath, FileMode.Open,
                FileAccess.Read, FileShare.Read))
            using (var reader = new StreamReader(file)) {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Method that writes a given text to a specified file
        /// </summary>
        /// <param name="filepath">The path to the given file</param>
        /// <param name="text">The text to write</param>
        private void WriteFile(string filepath, string text) {
            if (!File.Exists(filepath)) {
                File.Create(filepath);
            }
            using (var file = File.Open(filepath, FileMode.Truncate,
                FileAccess.Write, FileShare.Read))
            using (var writer = new StreamWriter(file)) {
                writer.WriteLine(text);
            }
        }
    }
}
