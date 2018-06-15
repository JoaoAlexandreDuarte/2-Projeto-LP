using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Roguelike {
    public class FileParser {
        public List<Trap> listOfTraps;

        public void ReadFromFiles() {
            string jsonString = ReadFile("./Objects/traps.json");
            listOfTraps = JsonConvert.DeserializeObject<List<Trap>>(jsonString);
        }

        public string ReadFile(string filepath) {
            using (var file = File.Open(filepath, FileMode.Open,
                FileAccess.Read, FileShare.Read))
            using (var reader = new StreamReader(file)) {
                return reader.ReadToEnd();
            }
        }

        public void WriteFile(string filepath, string text) {
            using (var file = File.Open(filepath, FileMode.Truncate,
                FileAccess.Read, FileShare.Read))
            using (var writer = new StreamWriter(file)) {
                writer.Write(text);
            }
        }
    }
}
