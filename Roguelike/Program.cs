using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Roguelike {
    public class Program {

        // https://www.c-sharpcorner.com/code/448/code-to-auto-maximize-console-application-according-to-screen-width-in-c-sharp.aspx
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static readonly IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        static void Main(string[] args) {
            // Utiliza o UTF-8 na consola
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetWindowSize(Console.LargestWindowWidth,
                Console.LargestWindowHeight);
            ShowWindow(ThisConsole, 3);
            // Construtor vazio
            Menu menu = new Menu();

            menu.Options();
        }
    }
}
