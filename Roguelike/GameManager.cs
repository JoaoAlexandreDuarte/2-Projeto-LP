using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    class GameManager {
        public void Update() {
            World world = new World();
            Interface visualization = new Interface();
            LevelGenerator levelGen = new LevelGenerator();
            int oldLevel = 1, currentLevel = 1;

            do {
                if (oldLevel == currentLevel) {

                }

                visualization.ShowWorld(world);
                visualization.ShowStats(world);

            } while (currentLevel < 3);

            Console.ReadKey();
        }
    }
}
