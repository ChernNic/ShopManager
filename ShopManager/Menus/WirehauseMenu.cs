using System;
using System.Collections.Generic;
using System.Text;

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

            List<Product
                
                
                
                
                
                
                
                
                
                
                
                
                > products = FileManager.ReadFromFile<User>("Products.json");
            string[] options = new string[products.Count];

            Console.CursorVisible = false;

            string label;
            foreach (User Product in products)
            {
                if (Product.Login == Login)
                {
                    if (Product.Name != null)
                    {
                        label = Product.Name;
                    }
                    else
                    {
                        label = Login;
                    }
                }
            }

            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Меню Администратора: {Login}                                                                                                              ");
            Console.ResetColor();

            Console.SetCursorPosition(89, 1);
            Console.WriteLine("| F1 - Создание пользователя  |");
            Console.SetCursorPosition(89, 2);
            Console.WriteLine("| F2 - Изменение пользователя |");
            Console.SetCursorPosition(89, 3);
            Console.WriteLine("| Del - Удаление польователя  |");
            Console.SetCursorPosition(89, 4);
            Console.WriteLine("| Esc - Возварт к авторизации |");

            Console.SetCursorPosition(0, 1);
            Console.WriteLine("ID Наименованияе                      Роль");

            for (int i = 0; i < options.Length; i++)
            {
                string role = "";
                options[i] = products[i].ID + "  " + products[i].Login;

                Console.SetCursorPosition(0, i + 2);
                Console.WriteLine(products[i].ID + "  " + products[i].Login);
                switch (products[i].Role)
                {
                    case (int)Roles.Admin:
                        role = "Администратор";
                        break;
                    case (int)Roles.HR:
                        role = "Mенеджер персонала";
                        break;
                    case (int)Roles.Manger:
                        role = "Бугалтер";
                        break;
                    case (int)Roles.WarehouseManager:
                        role = "Cклад-менеджер";
                        break;
                    case (int)Roles.Cashier:
                        role = "Кассир";
                        break;
                }
                Console.SetCursorPosition(30, i + 2);
                Console.WriteLine(role);
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

                        Console.WriteLine($"  Пароль пользователя: {products[SelectedIndex].Password}                      ");

                        Console.ReadKey(true);
                        Console.Clear();
                        Console.ResetColor();
                        Display();
                        break;

                    case ConsoleKey.F1:
                        Create();
                        break;

                    case ConsoleKey.F2:
                        ; Update(SelectedIndex);
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

                        break;
                }
            }
        }

        public void Create()
        {
            Console.Clear();

            Console.CursorVisible = false;

            List<User> users = FileManager.ReadUsersFromFile();
            int SelectedIndex = 0;

            int id = -1;
            string login = "";
            string password = "";
            int role = -1;

            Console.WriteLine("Меню добовления нового пользователя");
            Console.SetCursorPosition(2, 1);
            Console.Write("ID пользователя: ");
            Console.SetCursorPosition(2, 2);
            Console.Write("Логин пользователя: ");
            Console.SetCursorPosition(2, 3);
            Console.Write("Пароль пользователя: ");
            Console.SetCursorPosition(2, 4);
            Console.Write("Роль пользователя: ");

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
                                Console.SetCursorPosition(19, 1);
                                Console.Write("                 ");

                                bool isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {
                                        Console.SetCursorPosition(19, 1);
                                        id = Convert.ToInt32(Console.ReadLine());

                                        foreach (User user in users)
                                        {
                                            if (user.ID == id)
                                            {
                                                Console.SetCursorPosition(2, 1);
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write("ID пользователя: Ошибка. Пользователь с таким ID уже существует.");
                                                Thread.Sleep(1000);
                                                Console.SetCursorPosition(2, 1);
                                                Console.ResetColor();
                                                Console.Write("ID пользователя:                                                ");
                                                Console.SetCursorPosition(19, 1);
                                                isCorrect = false;
                                                break;
                                            }
                                            else if (id < 0)
                                            {
                                                Console.SetCursorPosition(2, 1);
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write("ID пользователя: Ошибка. ID не может быть меньше нуля.");
                                                Thread.Sleep(1000);
                                                Console.SetCursorPosition(2, 1);
                                                Console.ResetColor();
                                                Console.Write("ID пользователя:                                                ");
                                                isCorrect = false;
                                                break;
                                            }
                                            else
                                            {
                                                isCorrect = true;
                                            }
                                        }

                                    }
                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(2, 1);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("ID пользователя: Ошибка. Неправельный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(2, 1);
                                        Console.Write("ID пользователя:                                                ");
                                    }
                                }

                                Console.SetCursorPosition(19, 1);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(id);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(19, 1);
                                Console.Write(id);

                                SelectedIndex = 1;
                                break;

                            case 1:
                                Console.SetCursorPosition(22, 2);
                                Console.Write("                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    Console.SetCursorPosition(22, 2);
                                    login = Console.ReadLine();

                                    foreach (User user in users)
                                    {
                                        if (user.Login == login)
                                        {
                                            Console.SetCursorPosition(2, 2);
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write("Логин пользователя: Ошибка. Пользователь с таким логином уже существует.");
                                            Thread.Sleep(1000);
                                            Console.SetCursorPosition(2, 2);
                                            Console.ResetColor();
                                            Console.Write("Логин пользователя:                                                                         ");
                                            Console.SetCursorPosition(22, 2);
                                            isCorrect = false;
                                            break;
                                        }
                                        else if (login == "")
                                        {
                                            Console.SetCursorPosition(2, 2);
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write("Логин пользователя: Ошибка. Пользователь не может быть с пустым логином.");
                                            Thread.Sleep(1000);
                                            Console.SetCursorPosition(2, 2);
                                            Console.ResetColor();
                                            Console.Write("Логин пользователя:                                                                         ");
                                            Console.SetCursorPosition(22, 2);
                                            isCorrect = false;
                                            break;
                                        }
                                        else
                                        {
                                            isCorrect = true;
                                        }
                                    }
                                }

                                Console.SetCursorPosition(22, 2);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(login);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(22, 2);
                                Console.Write(login);

                                SelectedIndex = 2;
                                break;


                            case 2:
                                Console.SetCursorPosition(23, 3);
                                Console.Write("                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    Console.SetCursorPosition(23, 3);
                                    password = Console.ReadLine();


                                    if (password == "")
                                    {
                                        Console.SetCursorPosition(2, 3);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("Пароль пользователя: Ошибка. Пользователь не может быть с пустым паролем.");
                                        Thread.Sleep(1000);
                                        Console.SetCursorPosition(2, 3);
                                        Console.ResetColor();
                                        Console.Write("Пароль пользователя:                                                                         ");
                                        Console.SetCursorPosition(23, 3);
                                        isCorrect = false;
                                        break;
                                    }
                                    else
                                    {
                                        isCorrect = true;
                                    }
                                }

                                Console.SetCursorPosition(23, 3);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(password);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(23, 3);
                                Console.Write(password);

                                SelectedIndex = 3;
                                break;

                            case 3:

                                Console.SetCursorPosition(21, 4);
                                Console.Write("                 ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {
                                        Console.SetCursorPosition(21, 4);
                                        role = Convert.ToInt32(Console.ReadLine());

                                        foreach (User user in users)
                                        {
                                            if (role < 0 || role > 4)
                                            {
                                                Console.SetCursorPosition(2, 4);
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write("Роль пользователя: Ошибка. Такой роли не существует.");
                                                Thread.Sleep(1000);
                                                Console.SetCursorPosition(2, 4);
                                                Console.ResetColor();
                                                Console.Write("Роль пользователя:                                                ");
                                                isCorrect = false;
                                                break;
                                            }
                                            else
                                            {
                                                isCorrect = true;
                                            }
                                        }

                                    }
                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(2, 4);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("Роль пользователя: Ошибка. Неправельный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(2, 4);
                                        Console.Write("Роль пользователя:                                                ");
                                    }
                                }

                                Console.SetCursorPosition(21, 4);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(role);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(21, 4);
                                Console.Write(role);
                                break;
                        }
                        Console.CursorVisible = false;
                        break;

                    case ConsoleKey.S:

                        if (id == -1 || role == -1 || login == "" || password == "")
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
                            users.Add(new User(id, role, login, password));
                            FileManager.SaveToFile(users, "Users.json");

                            Console.SetCursorPosition(2, 5);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Успех. Ползователь создан.");
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
            Console.CursorVisible = true;

            Console.Clear();

            List<User> users = FileManager.ReadUsersFromFile();
            List<User> FoundUsers = null;

            Console.WriteLine("Меню поиска. ");
            Console.Write("Поиск: ");
            string search = Console.ReadLine();
        }

        public void Update(int Index)
        {
            Console.Clear();

            Console.CursorVisible = false;

            List<User> users = FileManager.ReadUsersFromFile();
            int SelectedIndex = 0;

            int id = users[Index].ID;
            string login = users[Index].Login;
            string password = users[Index].Password;
            int role = users[Index].Role;

            Console.WriteLine("Меню изменения пользователя");
            Console.SetCursorPosition(2, 1);
            Console.Write($"ID пользователя: {id}");
            Console.SetCursorPosition(2, 2);
            Console.Write($"Логин пользователя: {login}");
            Console.SetCursorPosition(2, 3);
            Console.Write($"Пароль пользователя: {password}");
            Console.SetCursorPosition(2, 4);
            Console.Write($"Роль пользователя: {role}");

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
                                Console.SetCursorPosition(19, 1);
                                Console.Write("                 ");

                                bool isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {
                                        Console.SetCursorPosition(19, 1);
                                        id = Convert.ToInt32(Console.ReadLine());

                                        foreach (User user in users)
                                        {
                                            if (user.ID == id && users[Index].ID != id)
                                            {
                                                Console.SetCursorPosition(2, 1);
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write("ID пользователя: Ошибка. Пользователь с таким ID уже существует.");
                                                Thread.Sleep(1000);
                                                Console.SetCursorPosition(2, 1);
                                                Console.ResetColor();
                                                Console.Write($"ID пользователя:                                                ");
                                                Console.SetCursorPosition(19, 1);
                                                isCorrect = false;
                                                break;
                                            }
                                            else if (id < 0)
                                            {
                                                Console.SetCursorPosition(2, 1);
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write("ID пользователя: Ошибка. ID не может быть меньше нуля.");
                                                Thread.Sleep(1000);
                                                Console.SetCursorPosition(2, 1);
                                                Console.ResetColor();
                                                Console.Write("ID пользователя:                                                  ");
                                                isCorrect = false;
                                                break;
                                            }
                                            else
                                            {
                                                isCorrect = true;
                                            }
                                        }

                                    }
                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(2, 1);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("ID пользователя: Ошибка. Неправельный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(2, 1);
                                        Console.Write("ID пользователя:                                                ");
                                    }
                                }

                                Console.SetCursorPosition(19, 1);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(id);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(19, 1);
                                Console.Write(id);

                                SelectedIndex = 1;
                                break;

                            case 1:
                                Console.SetCursorPosition(22, 2);
                                Console.Write("                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    Console.SetCursorPosition(22, 2);
                                    login = Console.ReadLine();

                                    foreach (User user in users)
                                    {
                                        if (user.Login == login && users[Index].Login != login)
                                        {
                                            Console.SetCursorPosition(2, 2);
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write("Логин пользователя: Ошибка. Пользователь с таким логином уже существует.");
                                            Thread.Sleep(1000);
                                            Console.SetCursorPosition(2, 2);
                                            Console.ResetColor();
                                            Console.Write("Логин пользователя:                                                                         ");
                                            Console.SetCursorPosition(22, 2);
                                            isCorrect = false;
                                            break;
                                        }
                                        else if (login == "")
                                        {
                                            Console.SetCursorPosition(2, 2);
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write("Логин пользователя: Ошибка. Пользователь не может быть с пустым логином.");
                                            Thread.Sleep(1000);
                                            Console.SetCursorPosition(2, 2);
                                            Console.ResetColor();
                                            Console.Write("Логин пользователя:                                                                         ");
                                            Console.SetCursorPosition(22, 2);
                                            isCorrect = false;
                                            break;
                                        }
                                        else
                                        {
                                            isCorrect = true;
                                        }
                                    }
                                }

                                Console.SetCursorPosition(22, 2);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(login);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(22, 2);
                                Console.Write(login);

                                SelectedIndex = 2;
                                break;


                            case 2:
                                Console.SetCursorPosition(23, 3);
                                Console.Write("                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    Console.SetCursorPosition(23, 3);
                                    password = Console.ReadLine();


                                    if (password == "")
                                    {
                                        Console.SetCursorPosition(2, 3);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("Пароль пользователя: Ошибка. Пользователь не может быть с пустым паролем.");
                                        Thread.Sleep(1000);
                                        Console.SetCursorPosition(2, 3);
                                        Console.ResetColor();
                                        Console.Write("Пароль пользователя:                                                                         ");
                                        Console.SetCursorPosition(23, 3);
                                        isCorrect = false;
                                        break;
                                    }
                                    else
                                    {
                                        isCorrect = true;
                                    }
                                }

                                Console.SetCursorPosition(23, 3);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(password);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(23, 3);
                                Console.Write(password);

                                SelectedIndex = 3;
                                break;

                            case 3:

                                Console.SetCursorPosition(21, 4);
                                Console.Write("                 ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {
                                        Console.SetCursorPosition(21, 4);
                                        role = Convert.ToInt32(Console.ReadLine());

                                        foreach (User user in users)
                                        {
                                            if (role < 0 || role > 5)
                                            {
                                                Console.SetCursorPosition(2, 4);
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write("Роль пользователя: Ошибка. Такой роли не существует.");
                                                Thread.Sleep(1000);
                                                Console.SetCursorPosition(2, 4);
                                                Console.ResetColor();
                                                Console.Write("Роль пользователя:                                                ");
                                                isCorrect = false;
                                                break;
                                            }
                                            else
                                            {
                                                isCorrect = true;
                                            }
                                        }

                                    }
                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(2, 4);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("Роль пользователя: Ошибка. Неправельный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(2, 4);
                                        Console.Write("Роль пользователя:                                                ");
                                    }
                                }

                                Console.SetCursorPosition(21, 4);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(role);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(21, 4);
                                Console.Write(role);
                                break;
                        }
                        Console.CursorVisible = false;
                        break;

                    case ConsoleKey.S:

                        users[Index] = new User(id, role, login, password);
                        FileManager.SaveToFile(users, "Users.json");

                        Console.SetCursorPosition(2, 5);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Успех. Ползователь изменен.");
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
            List<User> users = FileManager.ReadUsersFromFile();
            users.RemoveAt(Index);
            FileManager.SaveToFile(users, "Users.json");
        }
    }
}
}
