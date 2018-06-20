namespace Roguelike {
    public interface IDealsDamage {
        // Creates a propriety of a double MaxDamage
        double MaxDamage { get; set; }

        // Calls the method OnDetectingPlayer
        void OnDetectingPlayer(GameManager gm);
    }
}
