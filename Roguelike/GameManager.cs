using System;
using System.Linq;

namespace Roguelike {
    public class GameManager {
        public void Update() {
            World world = new World();
            Interface visualization = new Interface();
            LevelGenerator levelGen = new LevelGenerator();
            Player player = new Player();
            int[] playerPos;
            int[] exitPos;
            Tuple<int, int, int, int> coord;
            int level = 1;

            do {

                coord = levelGen.GenerateLevel(world, player, level);
                playerPos = new int[]{ coord.Item1, coord.Item2};
                exitPos = new int[] { coord.Item3, coord.Item4 };

                do {
                    Console.Clear();
                    visualization.ShowWorld(world, player, level);
                    visualization.ShowStats(world, player);
                    visualization.ShowLegend(world);
                    visualization.ShowCurrentInfo(world);

                    Console.ReadLine();
                } while (!playerPos.SequenceEqual(exitPos));

                level++;

            } while (player.HP > 0);

            visualization.ShowWorld(world, player, level);
            visualization.ShowStats(world, player);
            visualization.ShowLegend(world);


            Console.ReadKey();
        }
    }
}
