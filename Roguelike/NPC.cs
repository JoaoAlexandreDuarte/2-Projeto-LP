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

        public bool Hostile { get; set; }

        public double MaxDamage { get; set; }

        public NPC(double hp, double dmg, bool hostile) {
            HP = hp;
            Hostile = hostile;
            MaxDamage = dmg;
        }

        public void OnDetectingPlayer(GameManager gm) {
            double dmg;
            
            dmg = Rnd.NextDouble() * Math.Abs(MaxDamage);
            gm.messages.Add("You were attacked by an NPC and lost "
                + $"{dmg:f1}" + " HP");
            gm.player.LoseHP(dmg);
        }

        public override string ToString() {
            return "NPC (" + (Hostile ? "Hostile" : "Neutral") + ", HP= " + 
                $"{HP:f1}" + ", AP= " + $"{MaxDamage:f1}" + ")";
        }
    }
}
