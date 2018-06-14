using System;
using System.Collections.Generic;
using System.Linq;

// https://gist.github.com/phxvyper/ca78a715486c0f5077bb29e6bab279d1
// //interface IEntity {
//    Vector2i Position { get; }

//    void Update();
//}

namespace Roguelike {
    public class GameManager {
        public Command CommandFlag { get; private set; }
        public readonly Dictionary<ConsoleKey, Command> keyBinds =
            new Dictionary<ConsoleKey, Command>();

        public void Update() {
            World world = new World();
            Interface visualization = new Interface();
            LevelGenerator levelGen = new LevelGenerator();
            Player player = new Player();
            Tuple<int, int> playerPos;
            Tuple<int, int> exitPos;
            int level = 1;
            bool quit = false;
            bool action;
            ConsoleKeyInfo option;

            if (keyBinds.Count == 0) {
                AddKeys();
            }

            do {

                exitPos = levelGen.GenerateLevel(world, player, level);
                playerPos = new Tuple<int, int>(player.X, player.Y);

                do {

                    // Clear our command flags to update next
                    CommandFlag = Command.None;
                    action = false;

                    visualization.ShowWorld(world, player, level);
                    visualization.ShowStats(world, player);
                    visualization.ShowLegend(world);
                    visualization.ShowCurrentInfo(world);

                    // Update our input for everything else to use
                    option = Console.ReadKey();
                    if (keyBinds.TryGetValue(option.Key, out var command)) {

                        CommandFlag |= command;

                        switch (CommandFlag) {
                            case Command.Quit:
                                quit = true;
                                break;
                            case Command.MoveNorth:
                                if (player.MoveNorth()) {
                                    playerPos =
                                        world.UpdatePlayer(playerPos, player);
                                    action = true;
                                }
                                break;
                            case Command.MoveSouth:
                                if (player.MoveSouth(world.X)) {
                                    playerPos =
                                        world.UpdatePlayer(playerPos, player);
                                    action = true;
                                }
                                break;
                            case Command.MoveWest:
                                if (player.MoveWest()) {
                                    playerPos =
                                        world.UpdatePlayer(playerPos, player);
                                    action = true;
                                }
                                break;
                            case Command.MoveEast:
                                if (player.MoveEast(world.Y)) {
                                    playerPos =
                                        world.UpdatePlayer(playerPos, player);
                                    action = true;
                                }
                                break;
                            case Command.AttackNPC:
                                break;
                            case Command.PickUpItem:
                                break;
                            case Command.UseItem:
                                break;
                            case Command.DropItem:
                                break;
                            case Command.Information:
                                break;
                        }

                        if (action) {
                            player.LoseHP(1);
                        }
                    } else {

                        string[] keys =
                            keyBinds.Select(c => c.ToString()).ToArray();

                        Console.WriteLine();
                        visualization.WrongOption(option.Key.ToString(), keys);
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
            } else {
                //todo
                Console.WriteLine("Left on level " + level);
            }


            Console.ReadKey();
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
