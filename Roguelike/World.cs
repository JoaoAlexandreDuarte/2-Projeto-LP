using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    public class World {
        public int X { get; } = 8;
        public int Y { get; } = 8;
        public int TileSize { get; } = 10;
        public Tile[,] WorldArray { get; }

        public World() {
            WorldArray = new Tile[X, Y];
            for (int row = 0; row < X; row++) {
                for (int column = 0; column < Y; column++) {
                    WorldArray[row, column] = new Tile(TileSize);
                }
            }
        }

        public Tuple<int, int> UpdatePlayer(Tuple<int, int> playerPos,
            Player player) {


            WorldArray[playerPos.Item1, playerPos.Item2].Remove(player);
            WorldArray[player.X, player.Y].AddTo(player);
            WorldArray[playerPos.Item1, playerPos.Item2].FillEmpty();

            UpdateExploredPlaces(player);

            return new Tuple<int, int>(player.X, player.Y);
        }

        public void UpdateExploredPlaces(Player player) {
            // If the player isn't next to a wall in the North it sets the
            // Tile visible
            if (player.X - 1 >= 0) {
                WorldArray[player.X - 1, player.Y].IsVisible = true;
            }
            // If the player isn't next to a wall in the South it sets the
            // Tile visible
            if (player.X + 1 < this.X) {
                WorldArray[player.X + 1, player.Y].IsVisible = true;
            }// If the player isn't next to a wall in the North it sets the
            // Tile visible
            if (player.Y - 1 >= 0) {
                WorldArray[player.X, player.Y - 1].IsVisible = true;
            }// If the player isn't next to a wall in the North it sets the
            // Tile visible
            if (player.Y + 1 < this.Y) {
                WorldArray[player.X, player.Y + 1].IsVisible = true;
            }
        }
    }
}
