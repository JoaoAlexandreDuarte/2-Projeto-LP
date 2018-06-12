using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    public class LevelGenerator {
        Random rnd = new Random();

        public void GenerateLevel(World world, Player player, int level) {

            int row = rnd.Next(world.X);

            world.WorldArray[row, 0].AddTo(player);
            world.WorldArray[row, 0].SetVisible(true);
        }
    }
}
