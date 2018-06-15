using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    public class Menu {
        Interface visualization;
        GameManager game;
        FileParser parser;
        readonly string[,] developers = new string[,] {
            { "Inês Gonçalves", "a21702076" },
            { "Inês Nunes", "a21702520"},
            { "João Duarte", "a21702097"}};

        public void Options() {
            bool end = false, isError = false;
            short option;
            visualization = new Interface();
            game = new GameManager();
            parser = new FileParser();

            parser.ReadFromFiles();

            do {
                if (!isError) {
                    visualization.ClearScreen();
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
                        Console.Clear();
                        game.Update(parser);
                        break;
                    case 2:
                        visualization.ClearScreen();
                        visualization.ShowHighScores(parser);
                        Console.ReadKey();
                        break;
                    case 3:
                        visualization.ClearScreen();
                        visualization.ShowCredits(developers);
                        Console.ReadKey();
                        break;
                    case 4:
                        end = true;
                        break;
                    default:
                        visualization.ClearScreen();
                        isError = true;
                        visualization.WrongOption(option.ToString(),
                            new string[] { "1", "2", "3", "4" });
                        break;
                }

            } while (end == false);
            
            visualization.Bye();
            return;
        }
    }
}
