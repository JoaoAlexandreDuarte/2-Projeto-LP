using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    public class Player {
        public float HP { get; set; }
        public Object SelectedWeapon { get; set; }
        public Object Inventory { get; set; }
        public float MaxWeigth { get; set; }

        public Player(float hp, float maxWeight) {
            HP = hp;
            MaxWeigth = maxWeight;
        }

        public float LoseHP(float damage) {
            HP -= damage;

            return HP;
        }
    }
}
