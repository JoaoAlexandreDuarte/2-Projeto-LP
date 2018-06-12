using System;

namespace Roguelike {
    public class GameManager {
        public void Update() {
            World world = new World();
            Interface visualization = new Interface();
            LevelGenerator levelGen = new LevelGenerator();
            Player player = new Player();
            int oldLevel = 1, currentLevel = 1;

            levelGen.GenerateLevel(world, player, currentLevel);

            visualization.ShowWorld(world, player, currentLevel);
            visualization.ShowStats(world, player);
            visualization.ShowLegend(world);
            visualization.ShowCurrentInfo(world);

            Console.ReadKey();
        }
    }
}
