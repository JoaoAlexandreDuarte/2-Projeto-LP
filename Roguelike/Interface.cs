using System;

namespace Roguelike {
    class Interface {
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
    }
}
