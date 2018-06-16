namespace Roguelike {
    public interface IItem {
        void OnPickUp(GameManager gm);

        void OnUse(GameManager gm);

        void OnDrop(GameManager gm);
    }
}
