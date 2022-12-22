using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ShopManager.Menus
{
    static class MainMenu
    {
        static public void Run()
        {
            Console.Clear();
            int SelectedIndex = 0;
            string Login = "";
            string Password = "";
            List<string> PasswordChars = new List<string>();


            Console.WriteLine("Авторизация");
            Console.WriteLine("  Логин: ");
            Console.WriteLine("  Пароль: ");
            Console.WriteLine("  Войти");

            while (true)
            {
                Console.CursorVisible = false;
                Arrows.DisplayArrow(SelectedIndex, 1);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        SelectedIndex++;
                        if (SelectedIndex > 2)
                        {
                            SelectedIndex = 2;
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (SelectedIndex > 0)
                        {
                            SelectedIndex--;
                        }
                        break;

                    case ConsoleKey.Enter:
                        Console.CursorVisible = true;
                        switch (SelectedIndex)
                        {
                            case 0:
                                Console.SetCursorPosition(10, 1);
                                Console.WriteLine("                            ");
                                Console.SetCursorPosition(10, 1);
                                Login = Console.ReadLine();
                                break;
                            case 1:
                                PasswordChars = new List<string>();
                                Console.SetCursorPosition(11, 2);
                                Console.WriteLine("                            ");
                                Console.SetCursorPosition(11, 2);
                                while (true)
                                {
                                    ConsoleKeyInfo key = Console.ReadKey(true);
                                    if (key.Key == ConsoleKey.Enter) break;

                                    if (key.Key == ConsoleKey.Backspace)
                                    {
                                        if (PasswordChars.Count > 0)
                                        {
                                            PasswordChars.RemoveAt(PasswordChars.Count - 1);
                                            Console.Write("\b \b");
                                        }
                                    }
                                    else if (key.KeyChar != '\u0000')
                                    {
                                        PasswordChars.Add(key.KeyChar.ToString());
                                        Console.Write("*");
                                    }
                                }
                                Password = string.Join("", PasswordChars);
                                break;
                            case 2:
                                if(Password != "" || Login != "")
                                {
                                    bool isCheck = Authorization.Check(Login, Password);
                                    if (!isCheck)
                                    {
                                        Console.SetCursorPosition(2, 4);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("Войти. Данные неверны.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(2, 4);
                                        Console.Write("Войти");
                                    }
                                }
                                else
                                {
                                    Console.SetCursorPosition(2, 4);
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("Войти");
                                    Thread.Sleep(1000);
                                    Console.ResetColor();
                                    Console.SetCursorPosition(2, 4);
                                    Console.Write("Войти");
                                }
                                break;
                        }
                        break;
                }
            }
        }
    }
}
