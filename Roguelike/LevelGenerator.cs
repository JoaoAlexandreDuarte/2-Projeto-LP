using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    public class LevelGenerator {
        private Random rnd = new Random();

        public Tuple<int, int> GenerateLevel(World world, Player player,
            int level, FileParser parser) {

            int rowPlayer, rowExit;
            int tempRow, tempCol;
            double maxNum;
            Trap rndTrap;
            Food rndFood;
            Weapon rndWeapon;
            int tempNum;
            double hp, attackPower;
            bool state;
            Map map;

            if (level > 1) {
                CleanLevel(world);
            }

            // Exit
            rowExit = rnd.Next(world.X);

            world.WorldArray[rowExit, world.Y - 1].IsExit = true;

            // Player
            rowPlayer = rnd.Next(world.X);
            world.WorldArray[rowPlayer, 0].AddTo(player);
            world.WorldArray[rowPlayer, 0].IsVisible = true;
            player.X = rowPlayer;
            player.Y = 0;

            world.UpdateExploredPlaces(player);

            // Map
            do {
                tempRow = rnd.Next(world.X);
                tempCol = rnd.Next(world.Y);
            } while ((tempRow == rowExit) && (tempCol == world.Y - 1));

            map = new Map();
            world.WorldArray[tempRow, tempCol].AddTo(map);

            // Traps
            maxNum = Logistic(level, world.X * world.Y + world.TileSize,
                ((world.X * world.Y + world.TileSize) / 2) -
                (world.TileSize / 2), 0.07);

            for (int i = 0; i < maxNum; i++) {
                do {
                    tempRow = rnd.Next(world.X);
                    tempCol = rnd.Next(world.Y);
                } while (((tempRow == rowExit) && (tempCol == world.Y - 1)) ||
                ((tempRow == rowPlayer) && (tempCol == 0)));

                tempNum = rnd.Next(parser.listOfTraps.Count);

                rndTrap = parser.listOfTraps[tempNum];
                Trap finalTrap = new Trap(rndTrap.Name, rndTrap.MaxDamage);
                if (!world.WorldArray[tempRow, tempCol].AddTo(finalTrap)) {
                    i--;
                }
            }

            // Food
            maxNum = Logistic(level, world.X * world.Y,
                ((world.X * world.Y + world.TileSize) / 2) -
                (world.TileSize / 2), -0.05);

            for (int i = 0; i < maxNum; i++) {
                do {
                    tempRow = rnd.Next(world.X);
                    tempCol = rnd.Next(world.Y);
                } while ((tempRow == rowExit) && (tempCol == world.Y - 1));

                tempNum = rnd.Next(parser.listOfFoods.Count);

                rndFood = parser.listOfFoods[tempNum];
                Food finalFood = new Food(rndFood.Name, rndFood.HPIncrease,
                    rndFood.Weight);
                if (!world.WorldArray[tempRow, tempCol].AddTo(finalFood)) {
                    i--;
                }
            }

            // Weapon
            maxNum = Logistic(level, world.X * world.Y,
                ((world.X * world.Y + world.TileSize) / 2) -
                (world.TileSize / 2), -0.05);

            for (int i = 0; i < maxNum; i++) {
                do {
                    tempRow = rnd.Next(world.X);
                    tempCol = rnd.Next(world.Y);
                } while ((tempRow == rowExit) && (tempCol == world.Y - 1));

                tempNum = rnd.Next(parser.listOfWeapons.Count);

                rndWeapon = parser.listOfWeapons[tempNum];
                Weapon finalWeapon = new Weapon(rndWeapon.Name,
                    rndWeapon.AttackPower, rndWeapon.Weight,
                    rndWeapon.Durability);
                if (!world.WorldArray[tempRow, tempCol].AddTo(rndWeapon)) {
                    i--;
                }
            }

            // NPC
            maxNum = Logistic(level, world.X * world.Y + world.TileSize,
                ((world.X * world.Y + world.TileSize) / 2) -
                (world.TileSize / 2), 0.07);

            for (int i = 0; i < maxNum; i++) {
                do {
                    tempRow = rnd.Next(world.X);
                    tempCol = rnd.Next(world.Y);
                } while ((tempRow == rowExit) && (tempCol == world.Y - 1) ||
                ((tempRow == rowPlayer) && (tempCol == 0)));

                hp = Logistic(level, world.X * world.Y + world.TileSize,
                ((world.X * world.Y + world.TileSize) / 2) -
                (world.TileSize / 2), 0.07);

                attackPower = Logistic(level, world.X * world.Y +
                    world.TileSize, ((world.X * world.Y + world.TileSize) / 2)
                    - (world.TileSize / 2), 0.07);

                state = rnd.NextDouble() < 0.5 ? true : false;

                NPC npc = new NPC(hp, attackPower, state);
                if (!world.WorldArray[tempRow, tempCol].AddTo(npc)) {
                    i--;
                }
            }

            return new Tuple<int, int>(rowExit, world.Y - 1);
        }

        private void CleanLevel(World world) {
            for (int row = 0; row < world.X; row++) {
                for (int column = 0; column < world.Y; column++) {
                    world.WorldArray[row, column] = new Tile(world.TileSize);
                }
            }
        }

        /// <summary>
        /// Logistic function
        /// </summary>
        /// <param name="x">Input variable x</param>
        /// <param name="L">The curve's maximum value</param>
        /// <param name="x0">The x-value of the sigmoid's midpoint</param>
        /// <param name="k">The steepness of the curve</param>
        /// <returns>The y output variable</returns>
        private double Logistic(double x, double L, double x0, double k) {
            if (x == 1) {
                x = 1.001;
            }
            return L / (1 + Math.Exp(-k * (x - x0)));
        }
    }
}
