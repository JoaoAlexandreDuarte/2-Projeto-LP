using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    class Player {
        public float HP { get; set; }
        public Object SelectedWeapon { get; set; }
        public Object Inventory { get; set; }
        public float MaxWeigth { get; set; }
    }
}
