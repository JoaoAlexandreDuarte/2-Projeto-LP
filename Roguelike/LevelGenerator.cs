using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    public class LevelGenerator {
        Random rnd = new Random();

        public void GenerateLevel(World world) {
            Player player = new Player();

            int row = rnd.Next(world.X);

            world.WorldArray[row, 0].Add(player);
        }
    }
}
