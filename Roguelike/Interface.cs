using System;
using System.Collections.Generic;

namespace Roguelike {
    class Interface {

        protected static void WriteAt(string s, int x, int y) {
            try {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e) {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

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
            //IEnumerable<Object> list;
            int origCol, origRow;

            Console.WriteLine("+++++++++++++++++++++++++++ LP1 Rogue : Level" +
                " 009 +++++++++++++++++++++++++++");
            origCol = 2;
            for (int row = 0; row < world.X; row++) {
                origRow = 0;
                for (int column = 0; column < world.Y; column++) {
                    WriteAt("~~~~~ ", origRow, origCol);
                    WriteAt("~~~~~ ", origRow, origCol+1);
                    origRow += 6;
                    //list = world.WorldArray[row, column].GetInfo<Object>();

                    //for (int i = 0; i < world.TileSize/2; i++) {

                    //}

                }
                origCol += 3;
            }
        }
    }
}
