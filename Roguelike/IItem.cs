namespace Roguelike {
    public interface IItem {
        int[] Position { get; set; }

        void OnPickUp(GameManager gm);
    }
}
