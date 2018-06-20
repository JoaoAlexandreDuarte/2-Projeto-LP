namespace Roguelike {
    public interface IItem : IHasWeight {

        // Calls the method OnPickUp
        void OnPickUp(GameManager gm);

        // Calls the method OnUse
        void OnUse(GameManager gm);

        // Calls the method OnDrop
        void OnDrop(GameManager gm);
    }
}
