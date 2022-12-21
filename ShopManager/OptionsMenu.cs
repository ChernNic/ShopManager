using System;
using System.Collections.Generic;

namespace ShopManager
{
    static class OptionsMenu
    {
        public static void DisplayOption(string[] options , int selectedIndex, int offset = 0)
        {
            Console.ResetColor();
            if (selectedIndex == 0)
            {
                Console.SetCursorPosition(0, selectedIndex + offset);
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(options[selectedIndex]);

                Console.SetCursorPosition(0, selectedIndex + offset + 1);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                try
                {
                    Console.WriteLine(options[selectedIndex + 1]);
                }
                catch (Exception)
                {
                    
                }
            }
            else
            {
                Console.SetCursorPosition(0, selectedIndex + offset - 1);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(options[selectedIndex - 1]);

                Console.SetCursorPosition(0, selectedIndex + offset);
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                try
                {
                    Console.WriteLine(options[selectedIndex]);
                }
                catch (Exception)
                {

                }

                Console.SetCursorPosition(0, selectedIndex + offset + 1);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;   
                try
                {
                    Console.WriteLine(options[selectedIndex + 1]);
                }
                catch (Exception)
                {
                    Console.ResetColor();
                }
            }
        }
    }
}
