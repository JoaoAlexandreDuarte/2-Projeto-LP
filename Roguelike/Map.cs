namespace Roguelike {
    public class Map : IItem {
        public int[] Position { get; set; }

        public Map(int x, int y) {
            Position = new int[] { x, y };
        }

        public void OnPickUp(GameManager gm) {
            gm.world.RevealLevel(this);
        }

        public override string ToString() {
            return "Map";
        }
    }
}
