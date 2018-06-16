namespace Roguelike {
    public class Map : IItem {

        public void OnPickUp(GameManager gm) {
            gm.messages.Add("You revealed the entire level");
            gm.world.RevealLevel(this, gm.player);
        }

        public override string ToString() {
            return "Map";
        }
    }
}
