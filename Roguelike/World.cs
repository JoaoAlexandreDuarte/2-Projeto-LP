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

        public void RevealLevel(Map map) {
            for (int row = 0; row < X; row++) {
                for (int column = 0; column < Y; column++) {
                    WorldArray[row, column].IsVisible = true;
                }
            }

            WorldArray[map.Position[0], map.Position[1]].Remove(map);
            WorldArray[map.Position[0], map.Position[1]].FillEmpty();
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
            }
            // If the player isn't next to a wall in the West it sets the
            // Tile visible
            if (player.Y - 1 >= 0) {
                WorldArray[player.X, player.Y - 1].IsVisible = true;
            }
            // If the player isn't next to a wall in the East it sets the
            // Tile visible
            if (player.Y + 1 < this.Y) {
                WorldArray[player.X, player.Y + 1].IsVisible = true;
            }
        }

        public Tuple<List<Object>, List<Object>, List<Object>, List<Object>,
            List<Object>> GetSurroundingInfo(Player player) {

            List<Object> str1, str2, str3, str4, str5;

            // Checks for the North Position if available
            str1 = CheckSurrouding(player.X - 1, player.Y);
            // Checks for the South Position if available
            str2 = CheckSurrouding(player.X + 1, player.Y);
            // Checks for the West Position if available
            str3 = CheckSurrouding(player.X, player.Y - 1);
            // Checks for the East Position if available
            str4 = CheckSurrouding(player.X, player.Y + 1);
            // Checks for the Player Position
            str5 = CheckSurrouding(player.X, player.Y);

            return new Tuple<List<Object>, List<Object>, List<Object>,
                List<Object>, List<Object>>
                (str1, str2, str3, str4, str5);
        }

        private List<Object> CheckSurrouding(int x, int y) {
            List<Object> lst;

            if ((x < 0) || (x > this.X - 1) || (y < 0) || (y > this.Y - 1)) {
                lst = null;
            } else if (WorldArray[x, y].IsExit) {
                lst = new List<Object> {
                    0
                };
            } else {
                lst = WorldArray[x, y].GetInfo().ToList();
            }

            return lst;
        }
    }
}
