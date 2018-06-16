using System;

namespace Roguelike {
    public class Food : IItem {
        public string Name { get; }

        public double HPIncrease { get; set; }

        public double Weight { get; }

        public Food(string name, double hpInc, double weight) {
            Name = name;
            HPIncrease = hpInc;
            Weight = weight;
        }

        public void OnDrop(GameManager gm) {
            if (gm.world.WorldArray[gm.player.X, gm.player.Y].AddTo(this)) {
                gm.messages.Add("You droped a food (" + Name + ") on the " +
                    "floor");
                gm.player.Inventory.Remove(this);
            } else {
                gm.messages.Add("You tried to drop a food (" + Name + ") " +
                    "but the floor was full");
            }
        }

        public void OnPickUp(GameManager gm) {
            double weight = (gm.player.SelectedWeapon == null) ? 0 :
                gm.player.SelectedWeapon.Weight;

            if (weight + gm.player.Inventory.Weight <=
                gm.player.maxWeight) {
                gm.messages.Add("You picked up a food (" + Name + ") and " +
                    "put it in the inventory");
                gm.player.Inventory.Add(this);
                gm.world.WorldArray[gm.player.X, gm.player.Y].RemoveHere(this);
            } else {
                gm.messages.Add("You tried to pick up a food (" + Name + ") " +
                    "but you weren't strong enough to carry it");
            }
        }

        public void OnUse(GameManager gm) {
            if (gm.player.HP == 100) {
                gm.messages.Add("You tried to eat a food (" + Name + ") " +
                    "but you are already full health");
            } else {
                gm.player.GainHP(HPIncrease);
                gm.player.Inventory.Remove(this);
                gm.messages.Add("You ate food (" + Name + ") " +
                    "and feel better now");
            }
        }

        public override string ToString() {
            return "Food (" + Name + ")";
        }
    }
}
