namespace Roguelike {
    public class Map : IItem {

        public void OnPickUp(GameManager gm) {
            gm.world.RevealLevel(this, gm.player);
        }

        public override string ToString() {
            return "Map";
        }
    }
}
