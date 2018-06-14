using System;

namespace Roguelike {
    [Flags]
    public enum Command {
        None = 0,
        Quit = 1,
        MoveNorth = 2,
        MoveSouth = 4,
        MoveWest = 8,
        MoveEast = 16,
        AttackNPC = 32,
        PickUpItem = 64,
        UseItem = 128,
        DropItem = 256,
        Information = 512,
    }
}
