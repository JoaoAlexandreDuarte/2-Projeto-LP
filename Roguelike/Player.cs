using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    public class Player {
        private const float maxHP = 100;
        public float MaxWeight { get; } = 100;
        public float HP { get; set; }
        public Object SelectedWeapon { get; set; }
        public Object Inventory { get; set; }
        public float Weight { get; set; }

        public Player() {
            HP = maxHP;
        }

        public float LoseHP(float damage) {
            HP -= damage;

            return HP;
        }
    }
}
