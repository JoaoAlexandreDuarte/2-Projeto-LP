using System;

namespace Roguelike {
    /// <summary>
    /// Available keys to play the game
    /// </summary>
    [Flags]
    public enum Command {
        /// <summary>
        /// No key pressed
        /// </summary>
        None = 0,
        /// <summary>
        /// Quit key
        /// </summary>
        Quit = 1,
        /// <summary>
        /// Move North key
        /// </summary>
        MoveNorth = 2,
        /// <summary>
        /// Move South key
        /// </summary>
        MoveSouth = 4,
        /// <summary>
        /// Move West key
        /// </summary>
        MoveWest = 8,
        /// <summary>
        /// Move East key
        /// </summary>
        MoveEast = 16,
        /// <summary>
        /// Attack NPC key
        /// </summary>
        AttackNPC = 32,
        /// <summary>
        /// Pick up item key
        /// </summary>
        PickUpItem = 64,
        /// <summary>
        /// Use item key
        /// </summary>
        UseItem = 128,
        /// <summary>
        /// Drop item key
        /// </summary>
        DropItem = 256,
        /// <summary>
        /// Check information key
        /// </summary>
        Information = 512,
    }
}
