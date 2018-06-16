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

        public void OnDetectingPlayer(GameManager gm) {
            double dmg;

            FallenInto = true;
            dmg = Rnd.NextDouble() * Math.Abs(MaxDamage);
            gm.messages.Add("You fell in a TRAP (" + Name + ") and lost "
                + $"{dmg:f1}" + " HP");
            gm.player.LoseHP(dmg);
        }

        public override string ToString() {
            return "Trap (" + Name + ")";
        }
    }
}
