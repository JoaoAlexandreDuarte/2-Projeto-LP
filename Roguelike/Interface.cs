using System;
using System.Collections.Generic;
using System.Linq;

namespace Roguelike {
    public class Interface {
        private readonly string playerIcon = "\u03A9";
        private readonly string exit = "EXIT!";
        private readonly string empty = ".";
        private readonly string unexplored = "~";
        private readonly string neutral = "\u03fd";
        private readonly string hostile = "\u03ff";
        private readonly string food = "\u03c9";
        private readonly string weapon = "\u03ef";
        private readonly string trap = "\u0394";
        private readonly string map = "\u039e";

        public void ShowMenu() {
            Console.WriteLine("1. New Game\n2. High Scores\n3. Credits\n" +
                "4. Quit");
        }

        public void AskOption() {
            Console.WriteLine("What do you want to do?");
        }

        public void WrongOption(string option, string[] validOptions) {
            Console.Write("The option you chose (" + option + ") is not a " +
                "valid option. \nValid options are: ");
            for (int i = 0; i < validOptions.Length; i++) {
                if (i == validOptions.Length - 1) {
                    Console.WriteLine(validOptions[i]);
                } else {
                    Console.Write(validOptions[i] + " / ");
                }
            }
        }

        public void Bye() {
            Console.WriteLine("Thanks for playing! Until next time!");
        }

        public void ShowCredits(string[,] names) {
            Console.WriteLine("This project was made by:\n");
            for (int i = 0; i < names.GetLength(0); i++) {
                Console.WriteLine("\u25cf " + names[i, 0] + "\n");
                Console.WriteLine("\t\u25cb " + names[i, 1] + "\n\n");
            }
        }

        public void ShowWorld(World world, Player player, int level) {
            List<Object> lst;
            int writeRow, writeCol = 2;

            Console.Clear();
            Console.WriteLine("+++++++++++++++++++++++++++ LP1 RogueLite" +
                " : Level "
                + String.Format("{0:000}", level) +
                " +++++++++++++++++++++++++++");
            for (int row = 0; row < world.X; row++) {
                writeRow = 0;
                for (int column = 0; column < world.Y; column++) {

                    lst = world.WorldArray[row, column].GetInfo().ToList();

                    if (world.WorldArray[row, column].IsVisible) {

                        for (int i = 0; i < lst.Count / 2; i++) {
                            if (world.WorldArray[row, column].IsExit) {
                                WriteAt(exit, writeRow, writeCol);
                                break;
                            } else if (lst[i] == null) {
                                WriteAt(empty, writeRow + i, writeCol);
                            } else if (lst[i] is Player) {
                                WriteAt(playerIcon, writeRow + i, writeCol);
                            }
                        }
                        for (int i = lst.Count / 2; i < lst.Count; i++) {
                            if (world.WorldArray[row, column].IsExit) {
                                WriteAt(exit, writeRow, writeCol + 1);
                                break;
                            } else if (lst[i] == null) {
                                WriteAt(empty, writeRow++, writeCol + 1);
                            } else if (lst[i] is Player) {
                                WriteAt(playerIcon, writeRow++, writeCol + 1);
                            }
                        }
                        writeRow += 1;
                    } else {
                        for (int i = 0; i < lst.Count / 2; i++) {
                            WriteAt("~", writeRow + i, writeCol);
                        }
                        for (int i = lst.Count / 2; i < lst.Count; i++) {
                            WriteAt("~", writeRow++, writeCol + 1);
                        }
                        writeRow += 1;
                    }

                }
                writeCol += 3;
            }
        }

        public void ShowStats(World world, Player player) {
            int writeRow, writeCol = 2;

            writeRow = (world.Y * ((world.TileSize/2) + 1)) + 2;

            WriteAt("Player Stats", writeRow, writeCol++);
            WriteAt("------------", writeRow, writeCol++);
            WriteAt(String.Format("{0,-10}", "HP") + "- " +
                String.Format("{0:0.0}", player.HP), writeRow, writeCol++);
            WriteAt(String.Format("{0,-10}", "Weapon") + "- Rusty Sword",
                writeRow, writeCol++);
            WriteAt(String.Format("{0,-10}", "Inventory") + "- 91% full",
                writeRow, writeCol);
        }

        public void ShowLegend(World world) {
            int writeRow, writeCol = 11;

            writeRow = (world.X * ((world.TileSize / 2) + 1)) + 2;

            WriteAt("Legend", writeRow, writeCol++);
            WriteAt("------", writeRow, writeCol++);
            WriteAt(String.Format("{0,5}", playerIcon) + " - Player",
                writeRow, writeCol++);
            WriteAt(String.Format("{0,5}", exit) + " - Exit",
                writeRow, writeCol++);
            WriteAt(String.Format("{0,5}", empty) + " - Empty",
                writeRow, writeCol++);
            WriteAt(String.Format("{0,5}", unexplored) + " - Unexplored",
                writeRow, writeCol++);
            WriteAt(String.Format("{0,5}", neutral) + " - Neutral NPC",
                writeRow, writeCol++);
            WriteAt(String.Format("{0,5}", hostile) + " - Hostile NPC",
                writeRow, writeCol++);
            WriteAt(String.Format("{0,5}", food) + " - Food",
                writeRow, writeCol++);
            WriteAt(String.Format("{0,5}", weapon) + " - Weapon",
                writeRow, writeCol++);
            WriteAt(String.Format("{0,5}", trap) + " - Trap",
                writeRow, writeCol++);
            WriteAt(String.Format("{0,5}", map) + " - Map",
                writeRow, writeCol++);
        }

        public void ShowCurrentInfo(World world) {
            int writeRow = 0, writeCol;

            writeCol = (world.X * 3) + 2;

            WriteAt("Messages", writeRow, writeCol++);
            WriteAt("--------", writeRow, writeCol++);

            writeCol++;

            WriteAt("What do I see?", writeRow, writeCol++);
            WriteAt("--------------", writeRow, writeCol++);

            writeCol++;

            WriteAt("Options", writeRow, writeCol++);
            WriteAt("-------", writeRow, writeCol++);

            writeCol++;

            WriteAt("> ", writeRow, writeCol);
        }

        private void WriteAt(string s, int x, int y) {
            try {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e) {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }
}
