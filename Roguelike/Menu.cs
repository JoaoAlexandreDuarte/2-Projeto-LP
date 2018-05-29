using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    class Menu {
        readonly string[,] developers = new string[,] {
            { "Inês Gonçalves", "a21702076" },
            { "Inês Nunes", "a21702520"},
            { "João Duarte", "a21702097"}};

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
                        Console.Clear();
                        visualization.ShowCredits(developers);
                        Console.ReadKey();
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

            Console.WriteLine();
            visualization.Bye();
            return;
        }
    }
}
