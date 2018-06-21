using System;
using System.Collections.Generic;
using System.Linq;

namespace Roguelike {
    /// <summary>
    /// Class that manages the game flow
    /// </summary>
    public class GameManager {
        /// <summary>
        /// World instance that keeps all the data
        /// </summary>
        public World world;
        /// <summary>
        /// Interface instance that displays the game to the player
        /// </summary>
        public Interface visualization;
        /// <summary>
        /// Level Generator instance that randomly generates the level
        /// </summary>
        public LevelGenerator levelGen;
        /// <summary>
        ///  Insteance of the player
        /// </summary>
        public Player player;
        /// <summary>
        /// Instance of the file parser
        /// </summary>
        public FileParser parser;
        /// <summary>
        /// List of messages to display to the player
        /// </summary>
        public List<string> messages;
        /// <summary>
        /// The command that user presses
        /// </summary>
        public Command CommandFlag { get; private set; }
        /// <summary>
        /// A dictionary of the available keys to press
        /// </summary>
        public readonly Dictionary<ConsoleKey, Command> keyBinds =
            new Dictionary<ConsoleKey, Command>();

        /// <summary>
        /// Main method that updates the gameloop
        /// </summary>
        /// <param name="p">The instance of the file parser with the needed
        /// information</param>
        public void Update(FileParser p) {
            // Initializes the needed variables and sets parser to the parser
            world = new World();
            visualization = new Interface();
            levelGen = new LevelGenerator();
            player = new Player();
            parser = p;
            messages = new List<string>();
            // Player position
            Tuple<int, int> playerPos;
            // Exit position
            Tuple<int, int> exitPos;
            // List of the objects in the current tile
            List<Object> currentTile;
            // List of the harmful objects in the current tile
            List<IDealsDamage> tileDmg;
            // List of the NPC's in the current tile
            List<NPC> tileNPC;
            // List of items in the current tile
            List<IItem> tileItems;
            // List of inventory items
            List<IItem> inventoryItems;
            // Current level
            int level = 1;
            // If the player wants to quit
            bool quit = false;
            // If the turn 
            bool action;
            // The option the user chooses
            ConsoleKeyInfo option;
            // The high score to be saved if that's the case
            HighScore hS;
            // Input of the player regarding which item/ npc to interact with
            short itemNum, npcNum;

            // If the dictionary has no keys, it adds them
            if (keyBinds.Count == 0) {
                AddKeys();
            }
            // Welcome message
            messages.Add("Welcome to the game!");

            // First do..while manages each level
            do {

                // Saves the player position and the exit position to compare
                // at the end of the turn
                exitPos = levelGen.GenerateLevel(world, player, level, parser);
                playerPos = new Tuple<int, int>(player.X, player.Y);

                // Second do...while manages each turn
                do {

                    // Clear our command flags to update to this turn
                    CommandFlag = Command.None;
                    // Sets the lose hp this turn to false until player does
                    // something
                    action = false;
                    // Initializes the lists
                    tileItems = new List<IItem>();
                    tileDmg = new List<IDealsDamage>();
                    tileNPC = new List<NPC>();

                    // Gets the information about the current tile where the 
                    // player is
                    currentTile = world.WorldArray[player.X, player.Y].
                        GetInfo().ToList();

                    // Checks the items that are present in the tile
                    foreach (IItem obj in currentTile.OfType<IItem>()) {
                        tileItems.Add(obj);
                    }

                    // Checks the harmful things to the player
                    foreach (IDealsDamage obj in
                        currentTile.OfType<IDealsDamage>()) {
                        tileDmg.Add(obj);
                    }

                    // Each harmful thing will damage the player if it's the 
                    // case
                    foreach (IDealsDamage obj in tileDmg) {
                        if (obj is Trap) {
                            // If it's a trap that hasn't been activated 
                            // damages the player
                            if (!(obj as Trap).FallenInto) {
                                obj.OnDetectingPlayer(this);
                            }
                        }
                        if (obj is NPC) {
                            // Adds the npc to the list of npcs in the tile
                            tileNPC.Add(obj as NPC);
                            // If it's an hostile npc it attacks the player
                            if (((obj as NPC).Hostile)) {
                                obj.OnDetectingPlayer(this);
                            }
                        }
                    }

                    // Gets information about the player's inventory
                    inventoryItems = player.Inventory.GetInfo().ToList();

                    // After the player being damaged if the HP drops to 0
                    // the game ends
                    if (player.HP <= 0) {
                        break;
                    }

                    // Shows screen to the player
                    visualization.ShowWorld(world, player, level);
                    visualization.ShowStats(world, player);
                    visualization.ShowLegend(world);
                    visualization.ShowMessages(world, messages);
                    visualization.ShowSurrounds
                        (world.GetSurroundingInfo(player));
                    visualization.ShowOptions(new
                        List<ConsoleKey>(keyBinds.Keys));

                    // Clears the messages from the precious turn
                    messages.Clear();

                    // Update our input for everything else to use
                    option = Console.ReadKey();
                    // If it's a valid input
                    if (keyBinds.TryGetValue(option.Key, out var command)) {

                        // Adds the key the player pressed to the command
                        // this way makes it able to press many keys in a turn
                        // if we want to improve this game in the future
                        CommandFlag |= command;

                        // According to the command it does the corresponding
                        // action
                        switch (CommandFlag) {
                            // In case the Quit key was pressed
                            case Command.Quit:
                                string wantsQuit;
                                // Asks for confirmation until it's a valid
                                // input
                                do {
                                    visualization.AskQuit();
                                    wantsQuit = Console.ReadLine();
                                } while ((wantsQuit.ToUpper() != "Y") &&
                                (wantsQuit.ToUpper() != "N"));
                                // If the user confirmed it proceeds to end the
                                // game
                                if (wantsQuit.ToUpper() == "Y") {
                                    quit = true;
                                }
                                break;
                            // In case the Move North key was pressed
                            case Command.MoveNorth:
                                // If the player can move it moves him
                                // and updates it's position
                                if (player.MoveNorth()) {
                                    playerPos =
                                        world.UpdatePlayer(playerPos, player);
                                    action = true;
                                    messages.Add("You moved NORTH");

                                    // If not shows an error message
                                } else {
                                    messages.Add("You tried to move NORTH." +
                                        " But you hit a wall instead");
                                }
                                break;
                            // In case the Move South key was pressed
                            case Command.MoveSouth:
                                // If the player can move it moves him
                                // and updates it's position
                                if (player.MoveSouth(world.X)) {
                                    playerPos =
                                        world.UpdatePlayer(playerPos, player);
                                    action = true;
                                    messages.Add("You moved SOUTH");
                                    // If not shows an error message
                                } else {
                                    messages.Add("You tried to move SOUTH." +
                                        "  But you hit a wall instead");
                                }
                                break;
                            // In case the Move West key was pressed
                            case Command.MoveWest:
                                // If the player can move it moves him
                                // and updates it's position
                                if (player.MoveWest()) {
                                    playerPos =
                                        world.UpdatePlayer(playerPos, player);
                                    action = true;
                                    messages.Add("You moved WEST");
                                    // If not shows an error message
                                } else {
                                    messages.Add("You tried to move WEST." +
                                        "  But you hit a wall instead");
                                }
                                break;
                            // In case the Move West key was pressed
                            case Command.MoveEast:
                                // If the player can move it moves him
                                // and updates it's position
                                if (player.MoveEast(world.Y)) {
                                    playerPos =
                                        world.UpdatePlayer(playerPos, player);
                                    action = true;
                                    messages.Add("You moved EAST");
                                    // If not shows an error message
                                } else {
                                    messages.Add("You tried to move EAST." +
                                        "  But you hit a wall instead");
                                }
                                break;
                            // In case the Attack NPC key was pressed
                            case Command.AttackNPC:
                                // If there are more than 0 NPC's in the 
                                // current tile 
                                if ((tileNPC.Count > 0)) {
                                    // And if the player has a wepon equipped
                                    if (player.SelectedWeapon != null) {
                                        // Asks the player which NPC he wants 
                                        // to attack until it's a valid input
                                        do {
                                            visualization.ShowNPCsToAttack(
                                                tileNPC);
                                            short.TryParse(
                                                Console.ReadLine(),
                                                out npcNum);
                                        } while ((npcNum < 0) ||
                                    (npcNum > tileItems.Count));

                                        // In case it's an NPC number, attacks
                                        // that npc, otherwise goes back to
                                        // the choice screen without moving a 
                                        // turn
                                        if (npcNum != tileItems.Count) {
                                            player.AttackNPC(this,
                                                tileNPC[npcNum]);
                                            action = true;
                                        }
                                        // If there isn't any weapon equipped
                                        // shows an error message
                                    } else {
                                        messages.Add("You tried to attack an" +
                                            " NPC but you don't have a " +
                                            "weapon equipped");
                                    }
                                    // If there isn't any NPC in the tile 
                                    // shows an error message
                                } else {
                                    messages.Add("You tried to attack an NPC" +
                                        " but there are no NPC in the " +
                                        "current tile");
                                }
                                break;
                            // In case the Pick up item key was pressed
                            case Command.PickUpItem:
                                // If there are more than 0 items in the tile
                                // it asks which one the player wants to pick
                                // until it's a valid input
                                if (tileItems.Count > 0) {
                                    do {
                                        visualization.ShowItems(tileItems,
                                        "Pick Up");
                                        short.TryParse(
                                            Console.ReadLine(), out itemNum);
                                    } while ((itemNum < 0) ||
                                    (itemNum > tileItems.Count));

                                    // If it's an intem number it picks the
                                    // the item, if not goes back to the screen
                                    // without wastig a turn
                                    if (itemNum != tileItems.Count) {
                                        tileItems[itemNum].OnPickUp(this);
                                        action = true;
                                    }
                                    // If there are no items to pick it shows
                                    // an error message
                                } else {
                                    messages.Add("You tried to PICK UP an " +
                                        "item but there are no items " +
                                        "available to be picked up");
                                }
                                break;
                            // In case the Use item key was pressed
                            case Command.UseItem:
                                // If there are more than 0 items in the
                                // inventory, it asks which one the user wants
                                // to use until it's a valid input
                                if (inventoryItems.Count > 0) {
                                    do {
                                        visualization.ShowItems(inventoryItems,
                                        "Use");
                                        short.TryParse(
                                            Console.ReadLine(), out itemNum);
                                    } while ((itemNum < 0) ||
                                    (itemNum > inventoryItems.Count));

                                    // If it's an item number uses the item,
                                    // otherwise it goes back to the screen
                                    // without wasting a turn
                                    if (itemNum != inventoryItems.Count) {
                                        inventoryItems[itemNum].OnUse(this);
                                        action = true;
                                    }
                                    // If there are no items in the inventory
                                    // it shows an error message
                                } else {
                                    messages.Add("You tried to USE an " +
                                        "item but you currently don't have" +
                                        " any items in the inventory");
                                }
                                break;
                            // In case the Drop item key was pressed
                            case Command.DropItem:
                                // If there are more than 0 items in the
                                // inventory, it asks which one the user wants
                                // to use until it's a valid input
                                if (inventoryItems.Count > 0) {
                                    do {
                                        visualization.ShowItems(inventoryItems,
                                        "Drop");
                                        short.TryParse(
                                            Console.ReadLine(), out itemNum);
                                    } while ((itemNum < 0) ||
                                    (itemNum > inventoryItems.Count));

                                    // If it's an item number uses the item,
                                    // otherwise it goes back to the screen
                                    // without wasting a turn
                                    if (itemNum != inventoryItems.Count) {
                                        inventoryItems[itemNum].OnDrop(this);
                                        action = true;
                                    }
                                    // If there are no items in the inventory
                                    // it shows an error message
                                } else {
                                    messages.Add("You tried to DROP an " +
                                        "item but you currently don't have" +
                                        " any items in the inventory");
                                }
                                break;
                            // In case the Drop item key was pressed
                            case Command.Information:
                                // Adds a message to remind the user what it 
                                // did and shows the information screen
                                // without wasting a turn
                                messages.Add("You sought more info in the " +
                                    "elder scrolls");
                                visualization.ShowInformation(parser);
                                Console.ReadKey();
                                break;
                        }
                        // If the player advanced a turn loses 1 hp
                        if (action) {
                            player.LoseHP(1);
                        }
                        // If it's not a valid key input it shows an error
                        // message
                    } else {
                        Console.WriteLine();
                        visualization.WrongOption(option.Key.ToString());
                        Console.ReadKey();
                    }
                    // The level stays the same while the Player doesn't reach
                    // the exit, doesn't want to quit and has a positive HP
                } while ((!playerPos.Equals(exitPos)) && (!quit)
                && (player.HP > 0));

                // If the player doesn't want to quit the level increments by 1
                if (!quit) {
                    level++;
                }

                // The game is running while the player has a positive HP, and
                // doesn't want to quit
            } while ((player.HP > 0) && (!quit));

            if (!quit) {
                visualization.ShowWorld(world, player, level);
                visualization.ShowStats(world, player);
                visualization.ShowLegend(world);

                visualization.ShowDeath(level);
            }

            if (CheckHighScore(level)) {
                visualization.Success(level);
                string name = Console.ReadLine();
                if (name.Length > 3) {
                    name = name.Substring(0, 3);
                }
                hS = new HighScore(name, level);
                parser.UpdateHighScores(hS);
            } else {
                visualization.Failure(level);
                Console.ReadKey();
            }
        }

        private bool CheckHighScore(int level) {
            bool isHighScore = false;

            if ((parser.listHighScores == null)) {
                isHighScore = true;
            } else if (parser.listHighScores.Count < 10) {
                isHighScore = true;
            } else {
                foreach (HighScore hS in parser.listHighScores) {
                    if (level > hS.Score) {
                        isHighScore = true;
                        break;
                    }
                }
            }

            return isHighScore;
        }

        private void AddKeys() {
            keyBinds.Add(ConsoleKey.Q, Command.Quit);
            keyBinds.Add(ConsoleKey.W, Command.MoveNorth);
            keyBinds.Add(ConsoleKey.S, Command.MoveSouth);
            keyBinds.Add(ConsoleKey.A, Command.MoveWest);
            keyBinds.Add(ConsoleKey.D, Command.MoveEast);
            keyBinds.Add(ConsoleKey.F, Command.AttackNPC);
            keyBinds.Add(ConsoleKey.E, Command.PickUpItem);
            keyBinds.Add(ConsoleKey.U, Command.UseItem);
            keyBinds.Add(ConsoleKey.V, Command.DropItem);
            keyBinds.Add(ConsoleKey.I, Command.Information);
        }
    }
}
