using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ShopManager.Menus
{
    public class AdminMenu : ICrud
    {
        public void Create()
        {
            List<User> users = FileManager.ReadUsersFromFile();

            Console.WriteLine("Меню добовления нового пользователя");
            Console.Write("ID пользователя: ");
            Console.Write("Логин пользователя: ");
            while (true)
            {
                try
                {
                    Console.SetCursorPosition(17, 1);
                    int id = Convert.ToInt32(Console.ReadLine());
                    foreach (User user in users)
                    {
                        if (user.ID == id)
                        {
                            Console.SetCursorPosition(0, 1);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("ID пользователя: Ошибка. Пользователь с таким ID уже существует.");
                            Thread.Sleep(1000);
                            Console.SetCursorPosition(0, 1);
                            Console.ResetColor();
                            Console.Write("ID пользователя:                                                ");
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                    Console.SetCursorPosition(0, 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("ID пользователя: Ошибка. Неправильный ввод.                                       ");
                    Thread.Sleep(1000);
                    Console.SetCursorPosition(0, 1);
                    Console.ResetColor();
                    Console.Write("ID пользователя:                                                                  ");
                }
            }

            Console.SetCursorPosition(20, 2);
            string login = Console.ReadLine();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}


