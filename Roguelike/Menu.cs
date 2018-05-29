using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    class Menu {

        public void Options() {
            bool end = false, isError = false;
            short option;
            Interface visualization = new Interface();
            GameManager game = new GameManager();

            do {
                if (!isError) {
                    Console.Clear();
                } else {
                    Console.WriteLine();
                }

                isError = false;
                visualization.ShowMenu();
                Console.WriteLine("\n");
                visualization.AskOption();
                short.TryParse(Console.ReadLine(), out option);

                switch (option) {
                    case 1:
                        // todo play game
                        break;
                    case 2:
                        // todo high scores
                        break;
                    case 3:
                        // todo credits
                        break;
                    case 4:
                        end = true;
                        break;
                    default:
                        Console.Clear();
                        isError = true;
                        visualization.WrongOption(option.ToString(),
                            new string[] { "1", "2", "3", "4" });
                        break;
                }

            } while (end == false);

            return;
        }
    }
}
