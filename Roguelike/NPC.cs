using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    public class NPC : IDealsDamage, IHasHP {
        private Random Rnd = new Random(Guid.NewGuid().GetHashCode());

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

            dmg = Rnd.NextDouble() * MaxDamage;
            gm.messages.Add("You were attacked by an NPC and lost "
                + $"{dmg:f1}" + " HP");
            gm.player.LoseHP(dmg);
        }

        public override string ToString() {
            return "NPC (" + (Hostile ? "Hostile" : "Neutral") + ", HP= " +
                $"{HP:f1}" + ", AP= " + $"{MaxDamage:f1}" + ")";
        }

        public void TakeDamage(GameManager gm, double damage) {
            HP -= damage;

            if (!Hostile) {
                Hostile = true;
                gm.messages.Add("You attacked a neutral NPC for " +
                    $"{damage:f1}" + " damage and he became hostile");
            } else {
                gm.messages.Add("You attacked a hostile NPC for " +
                    $"{damage:f1}" + " damage");
            }

            if (HP <= 0) {
                int itemsDropped = 0, tempNum;
                double probabilityOfDropping = 0.8;
                IItem droppedItem, randomItem;

                gm.messages.Add("The NPC you attacked died");
                gm.world.WorldArray[gm.player.X, gm.player.Y].Remove(this);

                // Maximum number of items dropped by npc on kill
                for (int i = 0; i < 3; i++) {
                    if (Rnd.NextDouble() <= probabilityOfDropping) {
                        probabilityOfDropping -= 0.2;
                        if (Rnd.NextDouble() < 0.5) {
                            tempNum = Rnd.Next(gm.parser.listOfFoods.Count);
                            randomItem = gm.parser.listOfFoods[tempNum];

                            droppedItem = new Food((randomItem as Food).Name,
                                (randomItem as Food).HPIncrease,
                                (randomItem as Food).Weight);
                        } else {
                            tempNum = Rnd.Next(gm.parser.listOfWeapons.Count);
                            randomItem = gm.parser.listOfWeapons[tempNum];

                            droppedItem = new Weapon(
                                (randomItem as Weapon).Name,
                                (randomItem as Weapon).AttackPower,
                                (randomItem as Weapon).Weight,
                                (randomItem as Weapon).Durability);
                        }

                        if (!gm.world.WorldArray[gm.player.X, gm.player.Y].
                            AddTo(droppedItem)) {
                            break;
                        }

                        itemsDropped++;
                    }
                }
                gm.messages.Add("The NPC you killed dropped " + itemsDropped
                    + " item/s");
            }
        }
    }
}
