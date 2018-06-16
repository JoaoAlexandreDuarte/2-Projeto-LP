using System;
using System.Collections.Generic;
using System.Linq;

namespace Roguelike {
    public class GameManager {
        public World world;
        public Interface visualization;
        public LevelGenerator levelGen;
        public Player player;
        public FileParser parser;
        public List<string> messages;
        public Command CommandFlag { get; private set; }
        public readonly Dictionary<ConsoleKey, Command> keyBinds =
            new Dictionary<ConsoleKey, Command>();

        public void Update(FileParser p) {
            world = new World();
            visualization = new Interface();
            levelGen = new LevelGenerator();
            player = new Player();
            parser = p;
            messages = new List<string>();
            Tuple<int, int> playerPos;
            Tuple<int, int> exitPos;
            List<Object> currentTile;
            List<IDealsDamage> tileDmg;
            List<IItem> tileItems;
            List<IItem> inventoryItems;
            int level = 1;
            bool quit = false;
            bool action;
            ConsoleKeyInfo option;
            HighScore hS;
            short itemNum;

            if (keyBinds.Count == 0) {
                AddKeys();
            }
            messages.Add("Welcome to the game!");

            do {

                exitPos = levelGen.GenerateLevel(world, player, level, parser);
                playerPos = new Tuple<int, int>(player.X, player.Y);

                do {

                    // Clear our command flags to update next
                    CommandFlag = Command.None;
                    action = false;
                    tileItems = new List<IItem>();
                    tileDmg = new List<IDealsDamage>();

                    currentTile = world.WorldArray[player.X, player.Y].
                        GetInfo().ToList();

                    foreach (IItem obj in currentTile.OfType<IItem>()) {
                        tileItems.Add(obj);
                    }

                    foreach (IDealsDamage obj in
                        currentTile.OfType<IDealsDamage>()) {
                        tileDmg.Add(obj);
                    }

                    foreach (IDealsDamage obj in tileDmg) {
                        if (!(obj as Trap).FallenInto) {
                            obj.OnDetectingPlayer(this);
                        }
                    }

                    inventoryItems = player.Inventory.GetInfo().ToList();

                    if (player.HP <= 0) {
                        break;
                    }

                    visualization.ShowWorld(world, player, level);
                    visualization.ShowStats(world, player);
                    visualization.ShowLegend(world);
                    visualization.ShowMessages(world, messages);
                    visualization.ShowSurrounds
                        (world.GetSurroundingInfo(player));
                    visualization.ShowOptions(new
                        List<ConsoleKey>(keyBinds.Keys));

                    messages.Clear();

                    // Update our input for everything else to use
                    option = Console.ReadKey();
                    if (keyBinds.TryGetValue(option.Key, out var command)) {

                        CommandFlag |= command;

                        switch (CommandFlag) {
                            case Command.Quit:
                                string wantsQuit;
                                do {
                                    visualization.AskQuit();
                                    wantsQuit = Console.ReadLine();
                                } while ((wantsQuit.ToUpper() != "Y") &&
                                (wantsQuit.ToUpper() != "N"));
                                if (wantsQuit.ToUpper() == "Y") {
                                    quit = true;
                                }
                                break;
                            case Command.MoveNorth:
                                if (player.MoveNorth()) {
                                    playerPos =
                                        world.UpdatePlayer(playerPos, player);
                                    action = true;
                                    messages.Add("You moved NORTH");
                                } else {
                                    messages.Add("You tried to move NORTH." +
                                        " But you hit a wall instead");
                                }
                                break;
                            case Command.MoveSouth:
                                if (player.MoveSouth(world.X)) {
                                    playerPos =
                                        world.UpdatePlayer(playerPos, player);
                                    action = true;
                                    messages.Add("You moved SOUTH");
                                } else {
                                    messages.Add("You tried to move SOUTH." +
                                        "  But you hit a wall instead");
                                }
                                break;
                            case Command.MoveWest:
                                if (player.MoveWest()) {
                                    playerPos =
                                        world.UpdatePlayer(playerPos, player);
                                    action = true;
                                    messages.Add("You moved WEST");
                                } else {
                                    messages.Add("You tried to move WEST." +
                                        "  But you hit a wall instead");
                                }
                                break;
                            case Command.MoveEast:
                                if (player.MoveEast(world.Y)) {
                                    playerPos =
                                        world.UpdatePlayer(playerPos, player);
                                    action = true;
                                    messages.Add("You moved EAST");
                                } else {
                                    messages.Add("You tried to move EAST." +
                                        "  But you hit a wall instead");
                                }
                                break;
                            case Command.AttackNPC:
                                break;
                            case Command.PickUpItem:
                                if (tileItems.Count > 0) {
                                    do {
                                        visualization.ShowItems(tileItems,
                                        "Pick Up");
                                        short.TryParse(
                                            Console.ReadLine(), out itemNum);
                                    } while ((itemNum < 0) ||
                                    (itemNum > tileItems.Count));

                                    if (itemNum != tileItems.Count) {
                                        tileItems[itemNum].OnPickUp(this);
                                        action = true;
                                    }
                                } else {
                                    messages.Add("You tried to PICK UP an " +
                                        "item but there are no items " +
                                        "available to be picked up");
                                }
                                break;
                            case Command.UseItem:
                                if (inventoryItems.Count > 0) {
                                    do {
                                        visualization.ShowItems(inventoryItems,
                                        "Use");
                                        short.TryParse(
                                            Console.ReadLine(), out itemNum);
                                    } while ((itemNum < 0) ||
                                    (itemNum > inventoryItems.Count));

                                    if (itemNum != inventoryItems.Count) {
                                        inventoryItems[itemNum].OnUse(this);
                                        action = true;
                                    }
                                } else {
                                    messages.Add("You tried to USE an " +
                                        "item but you currently don't have" +
                                        " any items in the inventory");
                                }
                                break;
                            case Command.DropItem:
                                if (inventoryItems.Count > 0) {
                                    do {
                                        visualization.ShowItems(inventoryItems,
                                        "Drop");
                                        short.TryParse(
                                            Console.ReadLine(), out itemNum);
                                    } while ((itemNum < 0) ||
                                    (itemNum > inventoryItems.Count));

                                    if (itemNum != inventoryItems.Count) {
                                        inventoryItems[itemNum].OnDrop(this);
                                        action = true;
                                    }
                                } else {
                                    messages.Add("You tried to DROP an " +
                                        "item but you currently don't have" +
                                        " any items in the inventory");
                                }
                                break;
                            case Command.Information:
                                visualization.ShowInformation(parser);
                                Console.ReadKey();
                                break;
                        }

                        if (action) {
                            player.LoseHP(1);
                        }
                    } else {
                        string[] keys =
                            keyBinds.Select(k => k.ToString()).ToArray();

                        Console.WriteLine();
                        visualization.WrongOption(option.Key.ToString());
                        Console.ReadKey();
                    }
                } while ((!playerPos.Equals(exitPos)) && (!quit)
                && (player.HP > 0));

                if (!quit) {
                    level++;
                }

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
