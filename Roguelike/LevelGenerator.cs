using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    public class LevelGenerator {
        Random rnd = new Random();

        public Tuple<int, int> GenerateLevel(World world, Player player,
            int level) {

            if (level > 1) {
                CleanLevel(world);
            }

            int rowPlayer, rowExit;

            rowPlayer = rnd.Next(world.X);

            world.WorldArray[rowPlayer, 0].AddTo(player);
            world.WorldArray[rowPlayer, 0].IsVisible = true;
            player.X = rowPlayer;
            player.Y = 0;

            world.UpdateExploredPlaces(player);

            rowExit = rnd.Next(world.X);

            world.WorldArray[rowExit, 7].AddTo(0);
            world.WorldArray[rowExit, 7].IsExit = true;

            return new Tuple<int, int>(rowExit, 7);
        }

        private void CleanLevel(World world) {
            for (int row = 0; row < world.X; row++) {
                for (int column = 0; column < world.Y; column++) {
                    world.WorldArray[row, column] = new Tile(world.TileSize);
                }
            }
        }
    }
}
