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
            bool quit = false;
            string option;
            string[] options = new string[] { "W", "A", "S", "D", "F", "E",
                "U", "V", "I", "Q"};

            do {

                coord = levelGen.GenerateLevel(world, player, level);
                playerPos = new int[] { coord.Item1, coord.Item2 };
                exitPos = new int[] { coord.Item3, coord.Item4 };

                do {

                    visualization.ShowWorld(world, player, level);
                    visualization.ShowStats(world, player);
                    visualization.ShowLegend(world);
                    visualization.ShowCurrentInfo(world);

                    option = Console.ReadLine();

                    if (options.Contains(option.ToUpper())) {
                        //todo
                        Console.WriteLine("works");
                    } else {
                        visualization.WrongOption(option, options);
                    }

                    Console.ReadKey();
                } while ((!playerPos.SequenceEqual(exitPos)) || quit);

                level++;

            } while ((player.HP > 0) || quit);

            if (!quit) {

                visualization.ShowWorld(world, player, level);
                visualization.ShowStats(world, player);
                visualization.ShowLegend(world);
            } else {

            }


            Console.ReadKey();
        }
    }
}
