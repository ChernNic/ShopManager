using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ShopManager.Menus
{
    internal class CashierMenu
    {

        string Login;

        public CashierMenu(string login)
        {
            Login = login;
        }

        int SelectedIndex;

        public void Display()
        {
            Console.Clear();
            List<Product> products = FileManager.ReadFromFile<Product>("Products.json");
            int AmoutOFMoney = 0;
            int AmoutOfProduct = 0;

            List<User> users = FileManager.ReadUsersFromFile();

            if (products == null)
            {
                Console.WriteLine("Нет товаров. Обратитесь к менеджеру Склада.");
                Console.ReadKey();
                MainMenu.Run();
            }

            string[] options = new string[products.Count];

            Console.CursorVisible = false;

            string label = Login;

            foreach (User user in users)
            {
                if (user.Login == Login)
                {
                    if (user.Name != null)
                    {
                        label = user.Name;
                        break;
                    }
                }
            }

            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Меню Кссира: {label}                                                                                                              ");
            Console.ResetColor();

            Console.SetCursorPosition(85, 1);
            Console.WriteLine("| <+> - Добавить Товар в чек    |");
            Console.SetCursorPosition(85, 2);
            Console.WriteLine("| <-> - Удаление Товара из чека |");
            Console.SetCursorPosition(85, 3);
            Console.WriteLine("| Esc - Возварт к авторизации   |");


            Console.SetCursorPosition(0, 1);
            Console.WriteLine("ID Наименование               Цена              Кол-во в чеке");

            for (int i = 0; i < options.Length; i++)
            {
                options[i] = products[i].ID + " " + products[i].Name;

                Console.SetCursorPosition(0, i + 2);
                Console.WriteLine(products[i].ID + " " + products[i].Name);
                Console.SetCursorPosition(30, i + 2);
                Console.WriteLine(products[i].Cost);
                Console.ResetColor();
            }

            while (true)
            {
                OptionsMenu.DisplayOption(options, SelectedIndex, 2);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        products[SelectedIndex].Amount = products[SelectedIndex].Amount - AmoutOfProduct;
                        SelectedIndex++;
                        if (SelectedIndex > products.Count - 1)
                        {
                            SelectedIndex = products.Count - 1;
                        }
                        AmoutOfProduct = 0;
                        break;

                    case ConsoleKey.UpArrow:
                        products[SelectedIndex].Amount = products[SelectedIndex].Amount - AmoutOfProduct;
                        if (SelectedIndex > 0)
                        {
                            SelectedIndex--;
                        }
                        AmoutOfProduct = 0;
                        break;

                    case ConsoleKey.Enter:

                        bool isOver = false;
                        while (!isOver)
                        {
                            Console.SetCursorPosition(48, SelectedIndex + 2);
                            Console.WriteLine(AmoutOfProduct + "           ");
                            if(AmoutOfProduct <= 0)
                            {
                                Console.SetCursorPosition(48, SelectedIndex + 2);
                                Console.WriteLine(AmoutOfProduct + "           ");
                            }
                            else
                            {
                                Console.SetCursorPosition(48, SelectedIndex + 2);
                                Console.WriteLine(AmoutOfProduct + "           ");
                            }

                            switch (Console.ReadKey(true).Key)
                            {
                                case ConsoleKey.OemPlus:
                                    AmoutOfProduct++;
                                    if (AmoutOfProduct <= products[SelectedIndex].Amount)
                                    {
                                        Console.SetCursorPosition(48, SelectedIndex + 2);
                                        Console.WriteLine(AmoutOfProduct + "           ");
                                        AmoutOFMoney = AmoutOFMoney + products[SelectedIndex].Cost;
                                        Console.SetCursorPosition(85, 5);
                                        Console.WriteLine($"Сумма: " + AmoutOFMoney + "                   ");
                                    }
                                    else if (AmoutOfProduct == 0)
                                    {
                                        AmoutOfProduct = 0;
                                        Console.SetCursorPosition(85, 5);
                                        Console.WriteLine($"Сумма: " + AmoutOFMoney + "                   ");
                                        Console.WriteLine("              ");
                                    }
                                    break;

                                case ConsoleKey.OemMinus:
                                    AmoutOfProduct--;
                                    if (AmoutOfProduct > 0)
                                    {

                                        AmoutOFMoney = AmoutOFMoney - products[SelectedIndex].Cost;
                                        Console.SetCursorPosition(85, 5);
                                        Console.WriteLine($"Сумма: " + AmoutOFMoney + "                   ");
                                        Console.WriteLine("              ");
                                    }
                                    else if (AmoutOfProduct == 0)
                                    {
                                        AmoutOFMoney = AmoutOFMoney - products[SelectedIndex].Cost;
                                        Console.SetCursorPosition(85, 5);
                                        Console.WriteLine($"Сумма: " + AmoutOFMoney + "                   ");
                                    }
                                    else
                                    {
                                        AmoutOfProduct = 0;
                                        Console.SetCursorPosition(48, SelectedIndex + 2);
                                        Console.WriteLine("                   ");
                                    }
                                    break;

                                case ConsoleKey.Enter:
                                    products[SelectedIndex].Amount = products[SelectedIndex].Amount - AmoutOfProduct;
                                    AmoutOfProduct = 0;
                                    isOver = true;
                                    break;
                            }
                        }
                        break;
                    case ConsoleKey.S:
                        FileManager.SaveToFile<List<Product>>(products, "Products.json");

                        List<Transaction> transactions = FileManager.ReadFromFile<Transaction>("Transactions.json");
                        int id = transactions.Count;
                        foreach (Transaction transaction in transactions)
                        {
                            if (transaction.ID == id)
                            {
                                id = transactions.Count + 1;
                            }                           
                        }

                        transactions.Add(new Transaction(id, "Продажа кассиром: " + label,AmoutOFMoney,DateTime.Now, true));
                        FileManager.SaveToFile<List<Transaction>>(transactions, "Transactions.json");

                        Console.SetCursorPosition(85, 6);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Успех. Продажда осуществилась.");
                        Thread.Sleep(1000);
                        Console.ResetColor();
                        Console.SetCursorPosition(85, 6);
                        Console.Write("                                      ");
                        break;
                    case ConsoleKey.Escape:
                        MainMenu.Run();
                        break;
                }
            }
        }
    }
}
