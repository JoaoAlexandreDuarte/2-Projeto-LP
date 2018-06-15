namespace Roguelike {
    public interface IDealsDamage {
        double MaxDamage { get; set; }

        void OnDetectingPlayer(Player player);
    }
}
