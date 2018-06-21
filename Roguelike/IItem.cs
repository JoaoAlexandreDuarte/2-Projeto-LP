namespace Roguelike {
    public interface IItem : IHasWeight {

        void OnPickUp(GameManager gm);

        void OnUse(GameManager gm);

        void OnDrop(GameManager gm);
    }
}
