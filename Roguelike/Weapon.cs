using System;

namespace Roguelike {
    public class Weapon : IItem {
        public string Name { get; }

        public double AttackPower { get; set; }

        public double Durability { get; set; }

        public double Weight { get; }

        public Weapon(string name, double atk, double weight, double dura) {
            Name = name;
            AttackPower = atk;
            Weight = weight;
            Durability = dura;
        }

        public void OnDrop(GameManager gm) {
            if (gm.world.WorldArray[gm.player.X, gm.player.Y].AddTo(this)) {
                gm.messages.Add("You droped a weapon (" + Name + ") on the " +
                    "floor");
                gm.player.Inventory.Remove(this);
            } else {
                gm.messages.Add("You tried to drop a weapon (" + Name + ") " +
                    "but the floor was full");
            }
        }

        public void OnPickUp(GameManager gm) {

            if (gm.player.Weight <= gm.player.maxWeight) {
                gm.messages.Add("You picked up a weapon (" + Name + ") and " +
                    "put it in the inventory");
                gm.player.Inventory.Add(this);
                gm.world.WorldArray[gm.player.X, gm.player.Y].RemoveHere(this);
            } else {
                gm.messages.Add("You tried to pick up a weapon (" + Name + ") "
                    + "but you weren't strong enough to carry it");
            }
        }

        public void OnUse(GameManager gm) {
            Weapon temp;

            if (gm.player.SelectedWeapon != null) {
                temp = gm.player.SelectedWeapon;
                gm.messages.Add("You put the previous weapon (" + temp.Name +
                ") in the inventory");
                gm.player.Inventory.Add(temp);
            }

            gm.player.SelectedWeapon = this;
            gm.player.Inventory.Remove(this);
            gm.messages.Add("You equipped a new weapon (" + Name + ")");
        }

        public override string ToString() {
            return "Weapon (" + Name + ")";
        }
    }
}
