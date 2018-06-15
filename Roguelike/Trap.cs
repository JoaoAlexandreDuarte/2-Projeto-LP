using System;

namespace Roguelike {
    public class Trap : IDealsDamage {
        private Random Rnd = new Random();

        public string Name { get; set; }

        public bool FallenInto { get; set; }

        public double MaxDamage { get; set; }

        public Trap(string name, double dmg) {
            Name = name;
            FallenInto = false;
            MaxDamage = dmg;
        }

        public void OnDetectingPlayer(Player player) {
            FallenInto = true;
            player.LoseHP(Rnd.NextDouble() * Math.Abs(MaxDamage));
        }

        public override string ToString() {
            return "Trap (" + Name + ")";
        }
    }
}
