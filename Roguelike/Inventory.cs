using System;
using System.Collections.Generic;

namespace Roguelike {
    public class Inventory : List<IItem>, IHasWeight {
        // Starts the weight at 0
        public double Weight {
            get {
                double weight = 0;

                // A foreach that will add the item's weight to the current one
                foreach (IItem item in this) {
                    weight += item.Weight;
                }

                // Will return the updated weight
                return weight;
            }
        }

        public Inventory() : base() {
        }

        public IEnumerable<IItem> GetInfo() {

            foreach (IItem obj in this) {
                yield return obj;
            }
        }
    }
}
