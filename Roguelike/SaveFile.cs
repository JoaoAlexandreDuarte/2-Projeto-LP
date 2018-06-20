namespace Roguelike {
    public class SaveFile {
        public Player Player { get; set; }
        public World World { get; set; }
        public int Level { get; set; }
        public int[] ExitPos { get; set; }

        public SaveFile(Player player, World world, int level, int[] exit) {
            Player = player;
            World = world;
            Level = level;
            ExitPos = exit;
        }
    }
}
