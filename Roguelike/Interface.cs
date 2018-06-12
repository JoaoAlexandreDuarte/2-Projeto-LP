using System;
using System.Collections.Generic;
using System.Linq;

namespace Roguelike {
    public class Interface {

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

        public void ShowWorld(World world) {
            List<Object> list;
            int origRow, origCol = 2;

            Console.WriteLine("+++++++++++++++++++++++++++ LP1 Rogue : Level" +
                " 009 +++++++++++++++++++++++++++");
            for (int row = 0; row < world.X; row++) {
                origRow = 0;
                for (int column = 0; column < world.Y; column++) {
                    if (world.WorldArray[row, column].IsVisible) {
                        list = world.WorldArray[row, column].GetInfo().ToList();

                        for (int i = 0; i < list.Count / 2; i++) {
                            if (list[i] == null) {
                                WriteAt(".", origRow + i, origCol);
                            } else {
                                WriteAt("P", origRow + i, origCol);
                            }
                        }
                        for (int i = 5; i < list.Count; i++) {
                            if (list[i] == null) {
                                WriteAt(".", origRow++, origCol + 1);
                            } else {
                                WriteAt("P", origRow++, origCol + 1);
                            }
                        }
                        origRow += 1;
                    } else {
                        WriteAt("~~~~~ ", origRow, origCol);
                        WriteAt("~~~~~ ", origRow, origCol + 1);
                        origRow += 6;
                    }

                }
                origCol += 3;
            }
        }

        public void ShowStats(World world) {
            int writeRow, writeCol = 2;

            writeRow = world.X * 6 + 2;

            WriteAt("Player Stats", writeRow, writeCol++);
            WriteAt("------------", writeRow, writeCol++);
            WriteAt("HP", writeRow, writeCol);
            WriteAt("- " + "34.4", 61, writeCol++);
            WriteAt("Weapon", writeRow, writeCol);
            WriteAt("- " + "Rusty Sword", 61, writeCol++);
            WriteAt("Inventory", writeRow, writeCol);
            WriteAt("- " + "91% full", 61, writeCol++);
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
