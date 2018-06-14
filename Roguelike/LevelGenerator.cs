using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    public class LevelGenerator {
        private Random rnd = new Random();

        public Tuple<int, int> GenerateLevel(World world, Player player,
            int level) {

            int rowPlayer, rowExit;
            int tempRow, tempCol;
            Map map;

            if (level > 1) {
                CleanLevel(world);
            }

            rowExit = rnd.Next(world.X);

            world.WorldArray[rowExit, world.Y - 1].IsExit = true;

            do {
                tempRow = rnd.Next(world.X);
                tempCol = rnd.Next(world.Y);
            } while ((tempRow == rowExit) && (tempCol == world.Y - 1));

            map = new Map(tempRow, tempCol);
            world.WorldArray[tempRow, tempCol].AddTo(map);

            rowPlayer = rnd.Next(world.X);

            world.WorldArray[rowPlayer, 0].AddTo(player);
            world.WorldArray[rowPlayer, 0].IsVisible = true;
            player.X = rowPlayer;
            player.Y = 0;

            world.UpdateExploredPlaces(player);

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
