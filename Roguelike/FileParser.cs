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

        public void UpdateHighScores(HighScore hS) {

            listHighScores.Add(hS);

            listHighScores.Sort((y, x) => x.Score.CompareTo(y.Score));

            if (listHighScores.Count > 10) {
                listHighScores.RemoveAt(10);
            }

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
