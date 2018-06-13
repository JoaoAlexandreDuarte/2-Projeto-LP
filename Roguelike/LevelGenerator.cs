using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    public class LevelGenerator {
        Random rnd = new Random();

        public Tuple<int, int, int, int> GenerateLevel(World world,
            Player player, int level) {
            int rowPlayer, rowExit;

            rowPlayer = rnd.Next(world.X);

            world.WorldArray[rowPlayer, 0].AddTo(player);
            world.WorldArray[rowPlayer, 0].IsVisible = true;

            rowExit = rnd.Next(world.X);

            world.WorldArray[rowExit, 7].AddTo(0);
            world.WorldArray[rowExit, 7].IsExit = true;

            return new Tuple<int, int, int, int>(rowPlayer, 0, rowExit, 7);
        }
    }
}
