using System;
using System.Collections.Generic;
using System.Linq;

namespace Roguelike {
    public class Interface {
        // Will assign a unicode to each string
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

        // Renders the menu
        public void ShowMenu() {
            Console.WriteLine("1. New Game\n2. High Scores\n3. Credits\n" +
                "4. Quit");
        }

        // Will clear the screen
        public void ClearScreen() {
            Console.Clear();
        }

        // Renders the highscore list
        public void ShowHighScores(FileParser parser) {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(String.Format("{0,7}", "THE"));
            Console.Write(String.Format("{0,4}", parser.listHighScores.Count));
            Console.Write(String.Format("{0,7}", "BEST"));
            Console.Write(String.Format("{0,12}", "PLAYERS\n\n"));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(String.Format("{0,7}", "RANK"));
            Console.Write(String.Format("{0,11}", "SCORE"));
            Console.Write(String.Format("{0,12}", "NAME\n\n"));
            // For that will run through the highscore list
            for (int i = 0; i < parser.listHighScores.Count; i++) {
                string str;

                // If it's an even number, the color will be red
                if ((i + 2) % 2 == 0) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    // If it's an odd number, the color will be blue
                } else {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }

                // An if that will attribute each place on the list
                if (i == 0) {
                    str = "1ST";
                } else if (i == 1) {
                    str = "2ND";
                } else if (i == 2) {
                    str = "3RD";
                // After the 3rd, it'll add one more number, followed by "TH"
                } else {
                    str = "" + (i + 1) + "TH";
                }
                Console.Write(String.Format("{0,7}", str));
                Console.Write(String.Format("{0,11}",
                    parser.listHighScores[i].Score));
                Console.Write(String.Format("{0,12}",
                    parser.listHighScores[i].Name.ToUpper() + "\n\n"));
            }
            // Will reset the colors
            Console.ResetColor();
        }

        // Will render the question about what the player wants to do
        public void AskOption() {
            Console.WriteLine("What do you want to do?");
        }

        // Renders the error when the player chooses an unavailable option
        public void WrongOption(string option) {
            Console.Write("The option you chose (" + option + ") is not a " +
                "valid option.\n");
        }

        // Renders the goodbye message
        public void Bye() {
            Console.WriteLine("\nThanks for playing! Until next time!");
        }

        // Renders this question when the player wants to leave, asking
        // "Y/N"
        public void AskQuit() {
            Console.Clear();
            Console.WriteLine("\nDo you really want to quit? (Y/N)");
            Console.Write("\n> ");
        }

        // Will show the credits
        public void ShowCredits(string[,] names) {
            Console.WriteLine("This project was made by:\n");
            for (int i = 0; i < names.GetLength(0); i++) {
                Console.WriteLine("\u25cf " + names[i, 0] + "\n");
                Console.WriteLine("\t\u25cb " + names[i, 1] + "\n\n");
            }
        }

        //A death method that if the player dies, it'll the corresponding 
        // message in the color red
        public void ShowDeath(int level) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\n\n\nYou died on level " + level + "!");
            // Resets the color
            Console.ResetColor();
        }

        // A Success method that if the player places above 10th, it will
        // render the corresponding message
        public void Success(int level) {
            Console.Clear();
            Console.WriteLine("Congratulations! You reached level " + level +
                " and are part of the 10 highest high scores!");
            // Will then ask the player's name
            Console.WriteLine("\nWhat's your name?\n");
        }

        // A failure method that will show if the player finishes playing but
        // does not place in the top 10
        public void Failure(int level) {
            Console.Clear();
            Console.WriteLine("Too bad! You reached level " + level + " but" +
                " are not eligible to take part in the 10 highest scores.");
            Console.WriteLine("\nBetter luck next time!");
        }

        // Will render the world
        public void ShowWorld(World world, Player player, int level) {
            List<Object> lst;
            int writeRow, writeCol = 2;

            Console.Clear();
            // Will show the player the current level
            Console.WriteLine("+++++++++++++++++++++++++++ LP1 RogueLite" +
                " : Level "
                + String.Format("{0:000}", level) +
                " +++++++++++++++++++++++++++");

            // A for that will run through the world
            for (int row = 0; row < world.X; row++) {
                writeRow = 0;
                for (int column = 0; column < world.Y; column++) {

                    lst = world.WorldArray[row, column].GetInfo().ToList();

                    if (world.WorldArray[row, column].IsVisible) {

                        // A for that will assign colors to the respective
                        // objects
                        for (int i = 0; i < lst.Count / 2; i++) {
                            if (world.WorldArray[row, column].IsExit) {
                                Console.ForegroundColor = ConsoleColor.White;
                                WriteAt(exit, writeRow, writeCol);
                            } else if (lst[i] == null) {
                                WriteAt(empty, writeRow + i, writeCol);
                             // If it's a player, it'll assign the color yellow
                            } else if (lst[i] is Player) {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                WriteAt(playerIcon, writeRow + i, writeCol);
                            // If it's a map, it'll attribute the color
                            // dark yellow
                            } else if (lst[i] is Map) {
                                Console.ForegroundColor =
                                    ConsoleColor.DarkYellow;
                                WriteAt(map, writeRow + i, writeCol);
                            // If it's a trap, it'll have two options
                            } else if (lst[i] is Trap) {
                                // If it's an active trap, it'll be dark red
                                if (!(lst[i] as Trap).FallenInto) {
                                    Console.ForegroundColor =
                                        ConsoleColor.DarkRed;
                                // If it's not active, it'll be dark cyan
                                } else {
                                    Console.ForegroundColor =
                                        ConsoleColor.DarkCyan;
                                }
                                WriteAt(trap, writeRow + i, writeCol);
                            // If it's food, it'll be the color green
                            } else if (lst[i] is Food) {
                                Console.ForegroundColor = ConsoleColor.Green;
                                WriteAt(food, writeRow + i, writeCol);
                            // If it's a weapon, it'll be the color magenta
                            } else if (lst[i] is Weapon) {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                WriteAt(weapon, writeRow + i, writeCol);
                            // If it's a NPC, there will be two options
                            } else if (lst[i] is NPC) {
                                // If it's hostile, it'll be the color red
                                if ((lst[i] as NPC).Hostile) {
                                    Console.ForegroundColor =
                                        ConsoleColor.Red;
                                    WriteAt(hostile, writeRow + i, writeCol);
                                // If it's not hostile, it'll be the color cyan
                                } else {
                                    Console.ForegroundColor =
                                        ConsoleColor.Cyan;
                                    WriteAt(neutral, writeRow + i, writeCol);
                                }
                            }
                            // Will reset all colors after each turn
                            Console.ResetColor();
                        }
                        
                        for (int i = lst.Count / 2; i < lst.Count; i++) {
                            if (world.WorldArray[row, column].IsExit) {
                                Console.ForegroundColor = ConsoleColor.White;
                                WriteAt(exit, writeRow, writeCol + 1);
                            } else if (lst[i] == null) {
                                WriteAt(empty, writeRow++, writeCol + 1);
                            } else if (lst[i] is Map) {
                                Console.ForegroundColor =
                                    ConsoleColor.DarkYellow;
                                WriteAt(map, writeRow++, writeCol + 1);
                            } else if (lst[i] is Trap) {
                                if (!(lst[i] as Trap).FallenInto) {
                                    Console.ForegroundColor =
                                        ConsoleColor.DarkRed;
                                } else {
                                    Console.ForegroundColor =
                                        ConsoleColor.DarkCyan;
                                }
                                WriteAt(trap, writeRow++, writeCol + 1);
                            } else if (lst[i] is Food) {
                                Console.ForegroundColor = ConsoleColor.Green;
                                WriteAt(food, writeRow++, writeCol + 1);
                            } else if (lst[i] is Weapon) {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                WriteAt(weapon, writeRow++, writeCol + 1);
                            } else if (lst[i] is NPC) {
                                if ((lst[i] as NPC).Hostile) {
                                    Console.ForegroundColor =
                                        ConsoleColor.Red;
                                    WriteAt(hostile, writeRow++, writeCol + 1);
                                } else {
                                    Console.ForegroundColor =
                                        ConsoleColor.Cyan;
                                    WriteAt(neutral, writeRow++, writeCol + 1);
                                }
                            }
                            Console.ResetColor();
                        }
                        writeRow += 1;
                    } else {
                        // When there's nothing there and the player hasn't
                        // been near that spot, it'll place a "~" on the color
                        // dark gray
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        for (int i = 0; i < lst.Count / 2; i++) {
                            WriteAt("~", writeRow + i, writeCol);
                        }
                        for (int i = lst.Count / 2; i < lst.Count; i++) {
                            WriteAt("~", writeRow++, writeCol + 1);
                        }
                        writeRow += 1;
                        Console.ResetColor();
                    }

                }
                writeCol += 3;
            }
        }

        // Will render the stats
        public void ShowStats(World world, Player player) {
            int writeRow, writeCol = 2;
            string weaponName;

            writeRow = (world.Y * ((world.TileSize / 2) + 1)) + 2;

            WriteAt("Player Stats", writeRow, writeCol++);
            WriteAt("------------", writeRow, writeCol++);
            // If the player's HP is 50 or above, it'll show the current HP
            // in the color green
            if (player.HP >= 50) {
                Console.ForegroundColor = ConsoleColor.Green;
                // If it's 20 or above, it'll show the current HP in the color
                // yellow
            } else if (player.HP > 20) {
                Console.ForegroundColor = ConsoleColor.Yellow;
            // If it's below 20, it'll show the current HP in the color red
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            WriteAt(String.Format("{0,-10}", "HP") + "- " +
                String.Format("{0:0.0}", player.HP), writeRow, writeCol++);
            Console.ResetColor();
            // Will see if the player has a weapon
            weaponName = (player.SelectedWeapon == null) ? "None" :
                player.SelectedWeapon.Name;
            // Will show the weapon's name
            WriteAt(String.Format("{0,-10}", "Weapon") + "- " + weaponName,
                writeRow, writeCol++);
            // Will show the percentage of the weight the player currently
            // has on their inventory
            WriteAt(String.Format("{0,-10}", "Inventory") + "- " +
                $"{player.Weight / player.maxWeight:p1}" + " full",
                writeRow, writeCol);
        }

        public void ShowLegend(World world) {
            int writeRow, writeCol = 11;

            writeRow = (world.X * ((world.TileSize / 2) + 1)) + 2;

            // Shows the legend
            WriteAt("Legend", writeRow, writeCol++);
            WriteAt("------", writeRow, writeCol++);
            // The player will be in the color yellow
            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteAt(String.Format("{0,5}", playerIcon) + " - Player",
                writeRow, writeCol++);
            // The exit will be in the color white
            Console.ForegroundColor = ConsoleColor.White;
            WriteAt(String.Format("{0,5}", exit) + " - Exit",
                writeRow, writeCol++);
            // Resets the colors
            Console.ResetColor();
            WriteAt(String.Format("{0,5}", empty) + " - Empty",
                writeRow, writeCol++);
            // If it's unexplorec, it'll be in the color dark gray
            Console.ForegroundColor = ConsoleColor.DarkGray;
            WriteAt(String.Format("{0,5}", unexplored) + " - Unexplored",
                writeRow, writeCol++);
            // The neutral NPCs will be in the color cyan
            Console.ForegroundColor = ConsoleColor.Cyan;
            WriteAt(String.Format("{0,5}", neutral) + " - Neutral NPC",
                writeRow, writeCol++);
            // Hostile NPCs will be in the color red
            Console.ForegroundColor = ConsoleColor.Red;
            WriteAt(String.Format("{0,5}", hostile) + " - Hostile NPC",
                writeRow, writeCol++);
            // Food will be in the color green
            Console.ForegroundColor = ConsoleColor.Green;
            WriteAt(String.Format("{0,5}", food) + " - Food",
                writeRow, writeCol++);
            // Weapons will be in the color magenta
            Console.ForegroundColor = ConsoleColor.Magenta;
            WriteAt(String.Format("{0,5}", weapon) + " - Weapon",
                writeRow, writeCol++);
            // Inactive traps will be in the color dark cyan
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            WriteAt(String.Format("{0,5}", trap) + " - Inactive Trap",
                writeRow, writeCol++);
            // Active ones will be in the color dark red
            Console.ForegroundColor = ConsoleColor.DarkRed;
            WriteAt(String.Format("{0,5}", trap) + " - Active Trap",
                writeRow, writeCol++);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            WriteAt(String.Format("{0,5}", map) + " - Map",
                writeRow, writeCol++);
            // Resets the colors
            Console.ResetColor();
        }

        public void ShowMessages(World world, List<string> lst) {
            int writeRow = 0, writeCol;

            writeCol = (world.X * 3) + 2;

            // Will show the corresponding messages
            WriteAt("Messages", writeRow, writeCol++);
            Console.WriteLine("\n--------");
            // If the player hasn't done anything, it'll point that out
            if (lst.Count == 0) {
                Console.WriteLine("* You did nothing");
            } else {
                foreach (string str in lst) {
                    Console.WriteLine("* " + str);
                }
            }
            Console.WriteLine("\n");
        }

        // Renders the coordinates, and what is surrounding said coordinates
        public void ShowSurrounds(Tuple<List<Object>,
            List<Object>, List<Object>, List<Object>, List<Object>> surr) {
            Console.WriteLine("What do I see?");
            Console.WriteLine("--------------");
            Console.WriteLine("* NORTH : " +
                TransformSurroundingInfo(surr.Item1));
            Console.WriteLine("* SOUTH : " +
                TransformSurroundingInfo(surr.Item2));
            Console.WriteLine("* WEST  : " +
                TransformSurroundingInfo(surr.Item3));
            Console.WriteLine("* EAST  : " +
                TransformSurroundingInfo(surr.Item4));
            Console.WriteLine("* HERE  : " +
                TransformSurroundingInfo(surr.Item5));
        }

        public void ShowOptions(List<ConsoleKey> options) {

            // Will render the available options
            Console.WriteLine("\n\nOptions");
            Console.WriteLine("-------");
            Console.WriteLine("(" + options[1] + ") Move NORTH  (" +
                options[3] + ") Move WEST    (" + options[2] + ") Move SOUTH" +
                " (" + options[4] + ") Move EAST");
            Console.WriteLine("(" + options[5] + ") Attack NPC  (" +
                options[6] + ") Pick up item (" + options[7] + ") Use item  " +
                " (" + options[8] + ") Drop item");
            Console.WriteLine("(" + options[9] + ") Information (" + options[0]
                + ") Quit game");

            Console.Write("\n> ");

        }

        public void ShowItems(List<IItem> lst, string option) {

            // Will clear the console, creating a new "screen" allowing the
            // player to choose what item to pick up
            Console.Clear();
            Console.WriteLine("Select item to " + option);
            Console.Write("---------------");
            for (int i = 0; i < option.Length; i++) {
                Console.Write("-");
            }
            Console.WriteLine("\n");
            for (int i = 0; i < lst.Count; i++) {
                Console.WriteLine("" + i + ". " + lst[i].ToString());
            }
            Console.WriteLine("" + lst.Count + ". Go Back");
            Console.Write("\n> ");
        }

        // Renders the information about the food
        public void ShowInformation(FileParser parser) {
            Console.Clear();
            Console.WriteLine(String.Format("{0,-20}", "Food") +
                String.Format("{0,-15}", "HPIncrease") + "Weight");
            Console.WriteLine("-----------------------------------------");
            foreach (Food food in parser.listOfFoods) {
                Console.Write(food.Name);
                for (int i = 0; i < 22 - food.Name.Length; i++) {
                    Console.Write(" ");
                }
                Console.Write(String.Format("{0,8:00}",
                    "+" + food.HPIncrease));
                Console.WriteLine(String.Format("{0,11:0.0}", food.Weight));
            }

            // Renders the information about the weapon
            Console.WriteLine("\n");
            Console.WriteLine(String.Format("{0,-19}", "Weapon") +
                String.Format("{0,-16}", "AttackPower") +
                String.Format("{0,-10}", "Weight") + "Durability");
            Console.WriteLine("-----------------------------------------" +
                "--------------");
            foreach (Weapon weapon in parser.listOfWeapons) {
                Console.Write(weapon.Name);
                for (int i = 0; i < 22 - weapon.Name.Length; i++) {
                    Console.Write(" ");
                }
                Console.Write(String.Format("{0,8:0.0}",
                    weapon.AttackPower));
                Console.Write(String.Format("{0,11:0.0}", weapon.Weight));
                Console.WriteLine(String.Format("{0,14:0.00}", weapon.Durability));
            }

            // Renders the information about the trap
            Console.WriteLine("\n");
            Console.WriteLine(String.Format("{0,-21}", "Trap") + "MaxDamage");
            Console.WriteLine("------------------------------");
            foreach (Trap trp in parser.listOfTraps) {
                Console.Write(trp.Name);
                for (int i = 0; i < 22 - trp.Name.Length; i++) {
                    Console.Write(" ");
                }
                Console.WriteLine(String.Format("{0,8:0}", trp.MaxDamage));
            }
        }

        // A method that will allow the player to know what is around them
        private string TransformSurroundingInfo(List<Object> lst) {
            string returnString = "";

            if (lst == null) {
                returnString = "Wall";
            } else if (lst[0] != null) {
                foreach (Object obj in lst) {
                    if ((obj != null) && !(obj is Player)) {
                        if (obj.ToString() == "0") {
                            returnString = "Exit";
                        } else {
                            if (returnString == "") {
                                returnString += obj.ToString();
                            } else {
                                returnString += ", " + obj.ToString();
                            }
                        }
                    }
                }
                if (returnString == "") {
                    returnString = "Empty";
                }
            } else {
                returnString = "Empty";
            }

            return returnString;
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
