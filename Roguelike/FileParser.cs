using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Roguelike {
    public class FileParser {
        public List<Trap> listOfTraps;
        public List<HighScore> listHighScores;
        public List<Weapon> listOfWeapons;
        public List<Food> listOfFoods;

        public void ReadFromFiles() {
            string jsonTraps, jsonScores, jsonWeapons, jsonFoods;

            //Will read from the json files the corresponding file
            // and then convert them to .NET types
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

            // If the highscore list is null, it'll create a new one
            if (listHighScores == null) {
                listHighScores = new List<HighScore>();
            }
        }

        // Will add a highscore to the list and sort between the already
        // existing ones
        public void UpdateHighScores(HighScore hS) {

            listHighScores.Add(hS);

            listHighScores.Sort((y, x) => x.Score.CompareTo(y.Score));

            // If the list is bigger than 10, it'll remove the scores
            // after the 10th
            if (listHighScores.Count > 10) {
                listHighScores.RemoveAt(10);
            }

            // Will update the file
            WriteToFile();
        }

        private void WriteToFile() {
            string jsonStr = JsonConvert.SerializeObject(listHighScores);
            WriteFile("../../Data/highscores.json", jsonStr);
        }

        private string ReadFile(string filepath) {
            using (var file = File.Open(filepath, FileMode.Open,
                FileAccess.Read, FileShare.Read))
            using (var reader = new StreamReader(file)) {
                return reader.ReadToEnd();
            }
        }

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
