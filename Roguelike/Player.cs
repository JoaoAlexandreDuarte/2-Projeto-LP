using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    class Player {
        public float HP { get; set; } = 100f;
        public Object SelectedWeapon { get; set; } = null;
        public Object Inventory { get; set; } = null;
        public float MaxWeigth { get; set; } = 200;
    }
}
