using System;
using System.Text;

namespace Roguelike {
    class Program {
        static void Main(string[] args) {
            // Utiliza o UTF-8 na consola
            Console.OutputEncoding = Encoding.UTF8;
            // Construtor vazio
            Menu menu = new Menu();

            menu.Options();
        }
    }
}
