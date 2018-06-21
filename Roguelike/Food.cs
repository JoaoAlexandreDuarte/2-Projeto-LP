namespace Roguelike {
    /// <summary>
    /// Class that creates and keeps an item of type food
    /// </summary>
    public class Food : IItem {
        /// <summary>
        /// The name of the food
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// The amount of hp increased by eating a food
        /// </summary>
        public double HPIncrease { get; set; }
        /// <summary>
        /// The weight of the food
        /// </summary>
        public double Weight { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Food"/> class.
        /// </summary>
        /// <param name="name">Name of the food</param>
        /// <param name="hpInc">Amout of hp increased by the food</param>
        /// <param name="weight">Weight of the food</param>
        public Food(string name, double hpInc, double weight) {
            Name = name;
            HPIncrease = hpInc;
            Weight = weight;
        }

        /// <summary>
        /// Method that drops the food on the floor
        /// </summary>
        /// <param name="gm">The instance of game manager that allows
        /// the food to be dropped</param>
        public void OnDrop(GameManager gm) {
            // If there's enough space in the tile drops the food
            if (gm.world.WorldArray[gm.player.X, gm.player.Y].AddTo(this)) {
                gm.messages.Add("You droped a food (" + Name + ") on the " +
                    "floor");
                gm.player.Inventory.Remove(this);
                //If not shows an error message
            } else {
                gm.messages.Add("You tried to drop a food (" + Name + ") " +
                    "but the floor was full");
            }
        }

        /// <summary>
        /// Method that picks up a food from the floor
        /// </summary>
        /// <param name="gm">The instance of game manager that allows
        /// the food to be picked up</param>
        public void OnPickUp(GameManager gm) {

            // If the current weight of the player + the weight of the food
            // is lower than the max weight, it adds the food to the inventory
            if (gm.player.Weight + Weight <= gm.player.maxWeight) {
                gm.messages.Add("You picked up a food (" + Name + ") and " +
                    "put it in the inventory");
                gm.player.Inventory.Add(this);
                gm.world.WorldArray[gm.player.X, gm.player.Y].RemoveHere(this);
                // If not shows an error message
            } else {
                gm.messages.Add("You tried to pick up a food (" + Name + ") " +
                    "but you weren't strong enough to carry it");
            }
        }

        /// <summary>
        /// Method that uses a food from the inventory
        /// </summary>
        /// <param name="gm">The instance of game manager that allows the food
        /// to be used</param>
        public void OnUse(GameManager gm) {
            // If the hp of the player is already maxed it shows an error 
            // message
            if (gm.player.HP == 100) {
                gm.messages.Add("You tried to eat a food (" + Name + ") " +
                    "but you are already full health");
                // If not consumes the food
            } else {
                gm.player.GainHP(HPIncrease);
                gm.player.Inventory.Remove(this);
                gm.messages.Add("You ate food (" + Name + ") " +
                    "and feel better now");
            }
        }

        /// <summary>
        /// Method that overrides the default ToString method
        /// </summary>
        /// <returns>The new modified string</returns>
        public override string ToString() {
            return "Food (" + Name + ")";
        }
    }
}
