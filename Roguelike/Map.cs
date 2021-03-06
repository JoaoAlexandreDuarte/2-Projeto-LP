﻿namespace Roguelike {
    public class Map : IItem {

        // This item doesn't have weight
        public double Weight { get; }

        public void OnPickUp(GameManager gm) {
            gm.messages.Add("You revealed the entire level!");
            gm.world.RevealLevel(this, gm.player);
        }

        public void OnUse(GameManager gm) {
            // This item isn't used
        }

        public void OnDrop(GameManager gm) {
            // This item isn't dropped
        }

        public override string ToString() {
            return "Map";
        }
    }
}
