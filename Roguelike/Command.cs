using System;

namespace Roguelike {
    [Flags]
    public enum Command {
        None = 0,
        Quit = 1,
        MoveUp = 2,
        MoveDown = 4,
        MoveLeft = 8,
        MoveRight = 16,
        AttackNPC = 32,
        PickUpItem = 64,
        UseItem = 128,
        DropItem = 256,
        Information = 512
    }
}
