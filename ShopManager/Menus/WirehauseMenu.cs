using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ShopManager.Menus
{
    class WirehauseMenu
    {
        string Login;

        public WirehauseMenu(string login)
        {
            Login = login;
        }

        int SelectedIndex;

        public void Display()
        {
            Console.Clear();

            List<Product> products = FileManager.ReadFromFile<Product>("Products.json");
            List<User> users = FileManager.ReadUsersFromFile();

            if(products == null)
            {
                Console.WriteLine("Нет товаров. Нажмите любую клавишу чтобы их добавть.");
                Console.ReadKey();
                Create();
            }
            products = FileManager.ReadFromFile<Product>("Products.json");

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
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Меню Менеджера Склада: {label}                                                                                                              ");
            Console.ResetColor();

            Console.SetCursorPosition(89, 1);
            Console.WriteLine("| F1 - Создание Товара        |");
            Console.SetCursorPosition(89, 2);
            Console.WriteLine("| F2 - Изменение Товара       |");
            Console.SetCursorPosition(89, 3);
            Console.WriteLine("| Del - Удаление Товара       |");
            Console.SetCursorPosition(89, 4);
            Console.WriteLine("| Esc - Возварт к авторизации |");

            Console.SetCursorPosition(0, 1);
            Console.WriteLine("ID Наименование         Кол-во");

            for (int i = 0; i < options.Length; i++)
            {
                options[i] = products[i].ID + " " + products[i].Name;

                Console.SetCursorPosition(0, i + 2);
                Console.WriteLine(products[i].ID + " " + products[i].Name);
                Console.SetCursorPosition(24, i + 2);
                Console.WriteLine(products[i].Amount);
                Console.ResetColor();
            }

            while (true)
            {
                OptionsMenu.DisplayOption(options, SelectedIndex, 2);
                switch (Console.ReadKey(true).Key)
                {

                    case ConsoleKey.DownArrow:
                        SelectedIndex++;
                        if (SelectedIndex > products.Count - 1)
                        {
                            SelectedIndex = products.Count - 1;
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (SelectedIndex > 0)
                        {
                            SelectedIndex--;
                        }
                        break;

                    case ConsoleKey.Enter:
                        Console.SetCursorPosition(0, SelectedIndex + 3);
                        Console.ForegroundColor = ConsoleColor.DarkGray;

                        Console.WriteLine($"  Цена за штуку: {products[SelectedIndex].Cost}                      ");

                        Console.ReadKey(true);
                        Console.Clear();
                        Console.ResetColor();
                        Display();
                        break;

                    case ConsoleKey.F1:
                        Create();
                        break;

                    case ConsoleKey.F2:
                        Update(SelectedIndex);
                        break;

                    case ConsoleKey.Delete:
                        if (products[SelectedIndex].ID != 0)
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

            List<Product> products = FileManager.ReadFromFile<Product>("Products.json");
            int SelectedIndex = 0;

            int id = 0;
            string Name = "";
            int coast = -1;
            int amount = -1;

            Console.WriteLine("Меню добовления нового товара.");
            Console.SetCursorPosition(2, 1);
            Console.Write("ID товара: ");
            Console.SetCursorPosition(2, 2);
            Console.Write("Наименование товара: ");
            Console.SetCursorPosition(2, 3);
            Console.Write("Стоимость: ");
            Console.SetCursorPosition(2, 4);
            Console.Write("Кол-во: ");

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
                                                if (product.ID == id)
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
                                Console.SetCursorPosition(22, 2);
                                Console.Write("                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    Console.SetCursorPosition(23, 2);
                                    Name = Console.ReadLine();

                                    if (products != null)
                                    {
                                        foreach (Product product in products)
                                        {
                                            if (product.Name == Name)
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
                                            else if (Name == "")
                                            {
                                                Console.SetCursorPosition(2, 2);
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write("Наименование товара: Ошибка. Товар не может быть с пустым наименованием.");
                                                Thread.Sleep(1000);
                                                Console.SetCursorPosition(2, 2);
                                                Console.ResetColor();
                                                Console.Write("Наименование товара:                                                                         ");
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

                        if (id == -1 || coast == -1 || Name == "" || amount == -1)
                        {
                            Console.SetCursorPosition(2, 5);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Ошибка. Поля не заполнены.");
                            Thread.Sleep(1000);
                            Console.ResetColor();
                            Console.SetCursorPosition(2, 5);
                            Console.Write("                                                    ");
                        }
                        else
                        {
                            if(products == null)
                            {
                                products = new List<Product>();
                            }

                            products.Add(new Product(id, Name, coast, amount));
                            FileManager.SaveToFile(products, "Products.json");

                            Console.SetCursorPosition(2, 5);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Успех. Товар создан.");
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
            List<Product> products = FileManager.ReadFromFile<Product>("Products.json");
            products.RemoveAt(Index);
            FileManager.SaveToFile(products, "Products.json");
        }
    }
}

