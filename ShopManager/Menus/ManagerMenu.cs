using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ShopManager.Menus
{
    internal class ManagerMenu
    {
        string Login;

        public ManagerMenu(string login)
        {
            Login = login;
        }

        int SelectedIndex;

        public void Display()
        {
            Console.Clear();

            List<Transaction> transactions = FileManager.ReadFromFile<Transaction>("Transactions.json");
            List<User> users = FileManager.ReadUsersFromFile();

            if (transactions == null)
            {
                Console.WriteLine("Нет товаров. Нажмите любую клавишу чтобы их добавть.");
                Console.ReadKey();
                Create();
            }
            transactions = FileManager.ReadFromFile<Transaction>("Transactions.json");

            string[] options = new string[transactions.Count];

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
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Меню Менеджера: {label}                                                                                                              ");
            Console.ResetColor();

            Console.SetCursorPosition(89, 1);
            Console.WriteLine("| F1 - Создание Транзакции    |");
            Console.SetCursorPosition(89, 2);
            Console.WriteLine("| F2 - Изменение Транзакции   |");
            Console.SetCursorPosition(89, 3);
            Console.WriteLine("| Del - Удаление Транзакции   |");
            Console.SetCursorPosition(89, 4);
            Console.WriteLine("| Esc - Возварт к авторизации |");

            Console.SetCursorPosition(0, 1);
            Console.WriteLine("ID Наименование         Сумма             Время записи               Прибавка");

            for (int i = 0; i < options.Length; i++)
            {
                options[i] = transactions[i].ID + "  " + transactions[i].Name;

                Console.SetCursorPosition(0, i + 2);
                Console.WriteLine(transactions[i].ID + "  " + transactions[i].Name);
                Console.SetCursorPosition(24, i + 2);
                Console.WriteLine(transactions[i].Amount_Of_Money);
                Console.SetCursorPosition(40, i + 2);
                Console.WriteLine(transactions[i].Date);
                if (transactions[i].Increase)
                {
                    Console.SetCursorPosition(69, i + 2);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("+");
                    Console.ResetColor();
                }
                else
                {
                    Console.SetCursorPosition(69, i + 2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("-");
                    Console.ResetColor();
                }
            }

            while (true)
            {
                OptionsMenu.DisplayOption(options, SelectedIndex, 2);
                switch (Console.ReadKey(true).Key)
                {

                    case ConsoleKey.DownArrow:
                        SelectedIndex++;
                        if (SelectedIndex > transactions.Count - 1)
                        {
                            SelectedIndex = transactions.Count - 1;
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (SelectedIndex > 0)
                        {
                            SelectedIndex--;
                        }
                        break;

                    case ConsoleKey.F1:
                        Create();
                        break;

                    case ConsoleKey.F2:
                        Update(SelectedIndex);
                        break;

                    case ConsoleKey.Delete:
                        if (transactions[SelectedIndex].ID != 0)
                        {
                            Delete(SelectedIndex);
                            Display();
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        Read();
                        break;

                    case ConsoleKey.Escape:
                        MainMenu.Run();
                        break;
                }
            }
        }

        public void Create()
        {
            Console.Clear();

            Console.CursorVisible = false;

            List<Transaction> transactions = FileManager.ReadFromFile<Transaction>("Transactions.json");
            int SelectedIndex = 0;

            int id = -1;
            string name = "";
            int amount_of_money = -1;
            DateTime date = DateTime.Now;
            bool increase = false;

            Console.WriteLine("Меню добовления новой транзакции");

            Console.SetCursorPosition(2, 1);
            Console.Write("ID транзакции: ");
            Console.SetCursorPosition(2, 2);
            Console.Write("Наименование: ");
            Console.SetCursorPosition(2, 3);
            Console.Write("Сумма: ");
            Console.SetCursorPosition(2, 4);
            Console.Write("Прибавка: ");
            //Console.SetCursorPosition(2, 5);
            //Console.Write("Время[DD.MM.YYYY HH:mm]: ");

            while (true)
            {
                Arrows.DisplayArrow(SelectedIndex, 1);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        SelectedIndex++;
                        if (SelectedIndex > 4)
                        {
                            SelectedIndex = 4;
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
                                Console.SetCursorPosition(17, 1);
                                Console.Write("                 ");

                                bool isCorrect = false;

                                while (!isCorrect)
                                {

                                    try
                                    {
                                        Console.SetCursorPosition(17, 1);
                                        id = Convert.ToInt32(Console.ReadLine());

                                        if (transactions != null)
                                        {
                                            foreach (Transaction transaction in transactions)
                                            {
                                                if (transaction.ID == id)
                                                {
                                                    Console.SetCursorPosition(2, 1);
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.Write("ID транзакции: Транзакция с таким ID уже существует.");
                                                    Thread.Sleep(1000);
                                                    Console.SetCursorPosition(2, 1);
                                                    Console.ResetColor();
                                                    Console.Write("ID транзакции:                                            ");
                                                    Console.SetCursorPosition(17, 1);
                                                    isCorrect = false;
                                                    break;
                                                }
                                                else if (id < 0)
                                                {
                                                    Console.SetCursorPosition(2, 1);
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.Write("ID транзакции: ID не может быть меньше нуля.");
                                                    Thread.Sleep(1000);
                                                    Console.SetCursorPosition(2, 1);
                                                    Console.ResetColor();
                                                    Console.Write("ID транзакции:                                    ");
                                                    isCorrect = false;
                                                    break;
                                                }
                                                else
                                                {
                                                    isCorrect = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            isCorrect = true;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(2, 1);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("ID транзакции: Неправильный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(2, 1);
                                        Console.Write("ID транзакции:                                                     ");
                                    }
                                }

                                Console.SetCursorPosition(17, 1);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(id);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(17, 1);
                                Console.Write(id);

                                SelectedIndex = 1;
                                break;

                            case 1:
                                Console.SetCursorPosition(17, 2);
                                Console.Write("                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    Console.SetCursorPosition(17, 2);
                                    name = Console.ReadLine();
                                    if (transactions != null)
                                    {
                                        foreach (Transaction transaction in transactions)
                                        {
                                            if (transaction.Name == name)
                                            {
                                                Console.SetCursorPosition(2, 2);
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write("Наименование: Транзакция с таким наименованием уже существует.");
                                                Thread.Sleep(1000);
                                                Console.SetCursorPosition(2, 2);
                                                Console.ResetColor();
                                                Console.Write("Наименование:                                                    ");
                                                Console.SetCursorPosition(17, 2);
                                                isCorrect = false;
                                                break;
                                            }
                                            else if (name == "")
                                            {
                                                Console.SetCursorPosition(2, 2);
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write("Наименование: Транзакция не может быть с пустым наименованием.");
                                                Thread.Sleep(1000);
                                                Console.SetCursorPosition(2, 2);
                                                Console.ResetColor();
                                                Console.Write("Наименование:                                                   ");
                                                Console.SetCursorPosition(17, 2);
                                                isCorrect = false;
                                                break;
                                            }
                                            else
                                            {
                                                isCorrect = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        isCorrect = true;
                                    }
                                }

                                Console.SetCursorPosition(17, 2);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(name);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(17, 2);
                                Console.Write(name);

                                SelectedIndex = 2;
                                break;


                            case 2:
                                Console.SetCursorPosition(17, 3);
                                Console.Write("                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {

                                        Console.SetCursorPosition(17, 3);
                                        amount_of_money = Convert.ToInt32(Console.ReadLine());

                                        if (amount_of_money == 0)
                                        {
                                            Console.SetCursorPosition(2, 3);
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write("Cумма: Сумма не может быть равна нулю");
                                            Thread.Sleep(1000);
                                            Console.SetCursorPosition(2, 3);
                                            Console.ResetColor();
                                            Console.Write("Сумма:                                 ");
                                            Console.SetCursorPosition(17, 3);
                                            isCorrect = false;
                                            break;
                                        }

                                        else
                                        {
                                            isCorrect = true;
                                        }
                                    }

                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(2, 3);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("Сумма: Неправильный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(2, 3);
                                        Console.Write("Сумма:                                      ");
                                    }

                                }

                                Console.SetCursorPosition(17, 3);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(amount_of_money);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(17, 3);
                                Console.Write(amount_of_money);

                                SelectedIndex = 3;
                                break;

                            case 3:
                                Console.SetCursorPosition(17, 4);
                                Console.Write("");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    switch (Console.ReadKey(true).Key)
                                    {
                                        case ConsoleKey.OemMinus:
                                            Console.SetCursorPosition(17, 4);
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.Write("-");
                                            Thread.Sleep(1000);
                                            Console.ResetColor();
                                            Console.SetCursorPosition(17, 4);
                                            Console.Write("-");

                                            increase = false;
                                            isCorrect = true;
                                            break;

                                        case ConsoleKey.OemPlus:
                                            Console.SetCursorPosition(17, 4);
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.Write("+");
                                            Thread.Sleep(1000);
                                            Console.ResetColor();
                                            Console.SetCursorPosition(17, 4);
                                            Console.Write("+");

                                            increase = true;
                                            isCorrect = true;
                                            break;
                                    }
                                    if (!isCorrect)
                                    {
                                        Console.SetCursorPosition(2, 4);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("Прибавка: Ошибка. Неверные данные");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(2, 4);
                                        Console.Write("Прибавка:                                           ");
                                    }
                                    isCorrect = true;
                                }
                                SelectedIndex = 3;
                                break;

                            ////case 4:
                            ////    Console.SetCursorPosition(17, 5);
                            ////    Console.Write("                                                       ");

                            ////    isCorrect = false;

                            ////    while (!isCorrect)
                            ////    {
                            ////        Console.SetCursorPosition(17, 5);

                            ////        while (!DateTime.TryParse(Console.ReadLine(), out date))
                            ////        {
                            ////            Console.SetCursorPosition(2, 5);
                            ////            Console.ForegroundColor = ConsoleColor.Red;
                            ////            Console.Write("Время[DD.MM.YYYY HH:mm]:  Ошибка. ");
                            ////            Thread.Sleep(1000);
                            ////            Console.SetCursorPosition(2, 5);
                            ////            Console.ResetColor();
                            ////            Console.Write("Время[DD.MM.YYYY HH:mm]:                                                     ");
                            ////        }

                            ////    }

                            ////    Console.SetCursorPosition(17, 5);
                            ////    Console.ForegroundColor = ConsoleColor.Green;
                            ////    Console.Write(date);
                            ////    Thread.Sleep(1000);
                            ////    Console.ResetColor();
                            ////    Console.SetCursorPosition(17, 5);
                            ////    Console.Write(date);

                            ////    SelectedIndex = 4;
                            ////    break;
                        }
                        break;

                    case ConsoleKey.S:

                        if (id == -1 || name == "" || amount_of_money == -1)
                        {
                            Console.SetCursorPosition(2, 6);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Ошибка. Поля не заполнены.");
                            Thread.Sleep(1000);
                            Console.ResetColor();
                            Console.SetCursorPosition(2, 6);
                            Console.Write("                                                    ");
                        }
                        else
                        {
                            if (transactions == null)
                            {
                                transactions = new List<Transaction>();
                            }

                            transactions.Add(new Transaction(id, name, amount_of_money, date ,increase));
                            FileManager.SaveToFile(transactions, "Transactions.json");

                            Console.SetCursorPosition(2, 6);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Успех. Транзация успешно создана.");
                            Console.ResetColor();
                            Console.ReadLine();
                            Create();
                        }
                        break;
                    case ConsoleKey.Escape:
                        Display();
                        break;
                }
            }
        }

        public void Read()
        {

        }

        public void Update(int Index)
        {
            Console.Clear();

            Console.CursorVisible = false;

            List<Product> products = FileManager.ReadFromFile<Product>("Products.json");
            int SelectedIndex = 0;

            int id = products[Index].ID;
            string Name = products[Index].Name;
            int coast = products[Index].Cost;
            int amount = products[Index].Amount;

            Console.WriteLine("Меню изменения товара.");
            Console.SetCursorPosition(2, 1);
            Console.Write("ID товара: " + id);
            Console.SetCursorPosition(2, 2);
            Console.Write("Наименование товара: " + Name);
            Console.SetCursorPosition(2, 3);
            Console.Write("Стоимость: " + coast);
            Console.SetCursorPosition(2, 4);
            Console.Write("Кол-во: " + amount);

            while (true)
            {
                Arrows.DisplayArrow(SelectedIndex, 1);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        SelectedIndex++;
                        if (SelectedIndex > 3)
                        {
                            SelectedIndex = 3;
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
                                Console.SetCursorPosition(12, 1);
                                Console.Write("                 ");

                                bool isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {
                                        Console.SetCursorPosition(13, 1);
                                        id = Convert.ToInt32(Console.ReadLine());


                                        if (products != null)
                                        {
                                            foreach (Product product in products)
                                            {
                                                if (product.ID == id && product.ID != products[Index].ID)
                                                {
                                                    Console.SetCursorPosition(2, 1);
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.Write("ID товара: Ошибка. Товар с таким ID уже существует.");
                                                    Thread.Sleep(1000);
                                                    Console.SetCursorPosition(2, 1);
                                                    Console.ResetColor();
                                                    Console.Write("ID товара:                                                ");
                                                    isCorrect = false;
                                                    break;
                                                }
                                                else if (id < 0)
                                                {
                                                    Console.SetCursorPosition(2, 1);
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.Write("ID товара: Ошибка. ID не может быть меньше нуля.");
                                                    Thread.Sleep(1000);
                                                    Console.SetCursorPosition(2, 1);
                                                    Console.ResetColor();
                                                    Console.Write("ID товара:                                                ");
                                                    isCorrect = false;
                                                    break;
                                                }
                                                else
                                                {
                                                    isCorrect = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            isCorrect = true;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(2, 1);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("ID товара: Ошибка. Неправельный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(2, 1);
                                        Console.Write("ID товара:                                                ");
                                    }
                                }

                                Console.SetCursorPosition(13, 1);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(id);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(13, 1);
                                Console.Write(id);

                                SelectedIndex = 1;
                                break;

                            case 1:
                                Console.SetCursorPosition(23, 2);
                                Console.Write("                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    Console.SetCursorPosition(23, 2);
                                    Name = Console.ReadLine();

                                    if (Name == "")
                                    {
                                        Name = products[Index].Name;
                                    }

                                    if (products != null)
                                    {
                                        foreach (Product product in products)
                                        {
                                            if (product.Name == Name && product.Name != products[Index].Name)
                                            {
                                                Console.SetCursorPosition(2, 2);
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write("Наименование товара: Ошибка. Товар с таким наименованием уже существует.");
                                                Thread.Sleep(1000);
                                                Console.SetCursorPosition(2, 2);
                                                Console.ResetColor();
                                                Console.Write("Наименование товара:                                                                         ");
                                                Console.SetCursorPosition(23, 2);
                                                isCorrect = false;
                                                break;
                                            }
                                            else
                                            {
                                                isCorrect = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        isCorrect = true;
                                    }
                                }

                                Console.SetCursorPosition(23, 2);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(Name);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(23, 2);
                                Console.Write(Name);

                                SelectedIndex = 2;
                                break;


                            case 2:
                                Console.SetCursorPosition(13, 3);
                                Console.Write("                 ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {
                                        Console.SetCursorPosition(13, 3);
                                        coast = Convert.ToInt32(Console.ReadLine());
                                        isCorrect = true;
                                    }
                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(2, 3);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("Стоимость: Ошибка. Неправельный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(2, 3);
                                        Console.Write("Стоимость:                                                ");
                                    }
                                }

                                Console.SetCursorPosition(13, 3);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(coast);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(13, 3);
                                Console.Write(coast);

                                SelectedIndex = 3;
                                break;

                            case 3:
                                Console.SetCursorPosition(10, 4);
                                Console.Write("                 ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {
                                        Console.SetCursorPosition(10, 4);
                                        amount = Convert.ToInt32(Console.ReadLine());
                                        isCorrect = true;
                                    }
                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(2, 4);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("Кол-во: Ошибка. Неправельный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(2, 4);
                                        Console.Write("Кол-во:                                                ");
                                    }
                                }

                                Console.SetCursorPosition(10, 4);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(amount);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(10, 4);
                                Console.Write(amount);

                                SelectedIndex = 3;
                                break;
                        }
                        Console.CursorVisible = false;
                        break;

                    case ConsoleKey.S:

                        products[Index] = new Product(id, Name, coast, amount);
                        FileManager.SaveToFile(products, "Products.json");

                        Console.SetCursorPosition(2, 5);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Успех. Товар изменен.");
                        Console.ResetColor();
                        Console.ReadLine();
                        Create();
                        break;

                    case ConsoleKey.Escape:
                        Display();
                        break;
                }
            }
        }

        public void Delete(int Index)
        {
            List<Transaction> transactions = FileManager.ReadFromFile<Transaction>("Transactions.json");
            transactions.RemoveAt(Index);
            FileManager.SaveToFile(transactions, "Transactions.json");
        }
    }
}
