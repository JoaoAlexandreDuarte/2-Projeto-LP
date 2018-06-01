﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    class GameManager {
        public void Update() {
            World world = new World();
            Interface visualization = new Interface();
            LevelGenerator levelGen = new LevelGenerator();


            visualization.ShowWorld(world);
            visualization.ShowStats(world);
            Console.ReadKey();
        }
    }
}
