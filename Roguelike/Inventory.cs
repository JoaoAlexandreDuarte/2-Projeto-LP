using System;
using System.Collections.Generic;

namespace Roguelike {
    public class Inventory : List<IItem>, IHasWeight {
        public double Weight {
            get {
                double weight = 0;

                foreach (IItem item in this) {
                    weight += item.Weight;
                }

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
