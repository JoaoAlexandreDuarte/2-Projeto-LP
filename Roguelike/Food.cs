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

        // Method to drop the food
        public void OnDrop(GameManager gm) {
            // If available, it will remove the food from the list and display
            // the corresponding message
            if (gm.world.WorldArray[gm.player.X, gm.player.Y].AddTo(this)) {
                gm.messages.Add("You droped a food (" + Name + ") on the " +
                    "floor");
                gm.player.Inventory.Remove(this);
                // If not, it'll display the error message
            } else {
                gm.messages.Add("You tried to drop a food (" + Name + ") " +
                    "but the floor was full");
            }
        }

        // Method that picks up the food
        public void OnPickUp(GameManager gm) {

            // If the food's weight doesn't surpass the available weight limit,
            // it'll add it to the inventory
            if (gm.player.Weight <= gm.player.maxWeight) {
                gm.messages.Add("You picked up a food (" + Name + ") and " +
                    "put it in the inventory");
                gm.player.Inventory.Add(this);
                gm.world.WorldArray[gm.player.X, gm.player.Y].RemoveHere(this);
                // If it does surpass, it'll display the error message
            } else {
                gm.messages.Add("You tried to pick up a food (" + Name + ") " +
                    "but you weren't strong enough to carry it");
            }
        }

        // Method that will utilize the food
        public void OnUse(GameManager gm) {
            // If the player's health is 100, then they won't be able to "eat"
            // the food
            if (gm.player.HP == 100) {
                gm.messages.Add("You tried to eat a food (" + Name + ") " +
                    "but you are already full health");
                // If it's below 100, the player will be able to "eat" it and
                // regain health
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
