using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    class NPC : IDealsDamage, IHasHP {
        private Random Rnd = new Random();

        public const float maxNumber = 100;

        public double HP { get; set; }

        public string Name { get; set; }

        public bool Hostile { get; set; }

        public double MaxDamage { get; set; }

        public NPC(string name, double dmg, bool hostile) {
            Name = name;
            Hostile = hostile;
            MaxDamage = dmg;
            HP = maxNumber;
        }

        public void OnDetectingPlayer(GameManager gm) {
            gm.player.LoseHP(Rnd.NextDouble() * MaxDamage);
        }

        public override string ToString() {
            return "NPC (" + (Hostile ? "Hostile" : "Neutral") + ", HP";
        }
    }
}
