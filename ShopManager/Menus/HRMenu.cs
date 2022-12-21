using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ShopManager.Menus
{
    public class HRMenu : ICrud
    {
        string Login;

        public HRMenu(string login)
        {
            Login = login;
        }

        int SelectedIndex;

        public void Display()
        {
            Console.Clear();

            List<User> users = FileManager.ReadUsersFromFile();
            string[] options = new string[users.Count];

            Console.CursorVisible = false;

            string label;
            foreach (User user in users)
            {
                if (user.Login == Login)
                {
                    if (user.Name != null)
                    {
                        label = user.Name;
                    }
                    else
                    {
                        label = Login;
                    }
                }
            }

            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Меню Менеджера персонала: {Login}                                                                                                              ");
            Console.ResetColor();

            Console.SetCursorPosition(89, 1);
            Console.WriteLine("| F1 - Привязка сотрудника    |");
            Console.SetCursorPosition(89, 2);
            Console.WriteLine("| F2 - Изменение сотрудника   |");
            Console.SetCursorPosition(89, 3);
            Console.WriteLine("| Del - Удаление сотрудника   |");
            Console.SetCursorPosition(89, 4);
            Console.WriteLine("| Esc - Возварт к авторизации |");

            Console.SetCursorPosition(0, 1);
            Console.WriteLine("Логин                              ФИО           ");

            for (int i = 0; i < options.Length; i++)
            {
                options[i] = users[i].Login;

                Console.SetCursorPosition(0, i + 2);
                Console.WriteLine(users[i].Login);

                Console.SetCursorPosition(30, i + 2);
                Console.WriteLine(users[i].Surname + " " + users[i].Name + " " + users[i].Patronymic );

                Console.ResetColor();
            }

            while (true)
            {
                OptionsMenu.DisplayOption(options, SelectedIndex, 2);
                switch (Console.ReadKey(true).Key)
                {

                    case ConsoleKey.DownArrow:
                        SelectedIndex++;
                        if (SelectedIndex > users.Count - 1)
                        {
                            SelectedIndex = users.Count - 1;
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

                        if(users[SelectedIndex].DateOfBirth == null)
                        {
                            Console.WriteLine($"  Дата рождения: не назначена");
                        }
                        else
                        {
                            Console.WriteLine($"  Дата рождения: {users[SelectedIndex].DateOfBirth}");
                        }

                        Console.WriteLine($"  Зарплата: {users[SelectedIndex].Salary}");

                        if (users[SelectedIndex].PassportInfo == 0)
                        {
                            Console.WriteLine($"  Серия и номер паспорта: не назначена");
                        }
                        else
                        {
                            Console.WriteLine($"  Серия и номер паспорта: {users[SelectedIndex].PassportInfo}");
                        }


                        if (users[SelectedIndex].Job == null)
                        {
                            Console.WriteLine($"  Должность: не назначена");
                        }
                        else
                        {
                            Console.WriteLine($"  Должность: {users[SelectedIndex].Job}");
                        }

                        Console.ReadKey(true);
                        Console.Clear();
                        Console.ResetColor();
                        Display();
                        break;

                    case ConsoleKey.F1:
                        Update(SelectedIndex);
                        break;

                    case ConsoleKey.F2:
                        Update(SelectedIndex);
                        break;

                    case ConsoleKey.Delete:
                        Delete(SelectedIndex);
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
           
        }

        public void Read()
        {
            
        }

        public void Update(int Index)
        {
            Console.Clear();

            Console.CursorVisible = false;

            List<User> users = FileManager.ReadUsersFromFile();
            int SelectedIndex = 0;

            string name = users[Index].Name;
            string surname = users[Index].Surname;
            string patronymic = users[Index].Patronymic;
            string dateOfBirth = users[Index].DateOfBirth;
            string job = users[Index].Patronymic;
            long passportInfo = users[Index].PassportInfo;
            int salary = users[Index].Salary;

            Console.WriteLine("Меню привязки сотрудника");

            Console.SetCursorPosition(0, 1);
            if (users[SelectedIndex].Name == null)
            {
                Console.WriteLine($"  Имя: не назначено");
            }
            else
            {
                Console.WriteLine($"  Имя: {users[SelectedIndex].Name}");
            }

            Console.SetCursorPosition(0, 2);
            if (users[SelectedIndex].Surname == null)
            {
                Console.WriteLine($"  Фамилия: не назначена");
            }
            else
            {
                Console.WriteLine($"  Фамилия: {users[SelectedIndex].Surname}");
            }

            Console.SetCursorPosition(0, 3);
            if (users[SelectedIndex].Patronymic == null)
            {
                Console.WriteLine($"  Очество: не назначено");
            }
            else
            {
                Console.WriteLine($"  Очество: {users[SelectedIndex].Patronymic}");
            }

            Console.SetCursorPosition(0, 4);
            if (users[SelectedIndex].DateOfBirth == null)
            {
                Console.WriteLine($"  Дата рождения [DD-MM-YYYY]: не назначена");
            }
            else
            {
                Console.WriteLine($"  Дата рождения [DD-MM-YYYY]: {users[SelectedIndex].DateOfBirth}");
            }

            Console.SetCursorPosition(0, 5);
            Console.WriteLine($"  Зарплата: {users[SelectedIndex].Salary}");

            Console.SetCursorPosition(0, 6);
            if (users[SelectedIndex].PassportInfo == 0)
            {
                Console.WriteLine($"  Серия и номер паспорта: не назначена");
            }
            else
            {
                Console.WriteLine($"  Серия и номер паспорта: {users[SelectedIndex].PassportInfo}");
            }

            Console.SetCursorPosition(0, 7);
            if (users[SelectedIndex].Job == null)
            {
                Console.WriteLine($"  Должность: не назначена");
            }
            else
            {
                Console.WriteLine($"  Должность: {users[SelectedIndex].Job}");
            }

            while (true)
            {
                Arrows.DisplayArrow(SelectedIndex, 1);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        SelectedIndex++;
                        if (SelectedIndex > 6)
                        {
                            SelectedIndex = 6;
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
                                Console.SetCursorPosition(7, 1);
                                Console.Write("                 ");

                                bool isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {
                                        Console.SetCursorPosition(7, 1);
                                        name = Console.ReadLine();


                                        if (name == "")
                                        {
                                            name = users[Index].Name;
                                        }

                                        isCorrect = true;
                                    }
                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(0, 1);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write(" Имя: Ошибка. Неправельный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(0, 1);
                                        Console.Write(" Имя:                                                     ");
                                    }
                                }

                                Console.SetCursorPosition(7, 1);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(name);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(7, 1);
                                Console.Write(name);
                                SelectedIndex = 1;

                                break;

                            case 1:
                                Console.SetCursorPosition(11, 2);
                                Console.Write("                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {
                                        Console.SetCursorPosition(11, 2);
                                        surname = Console.ReadLine();


                                        if (surname == "")
                                        {
                                            surname = users[Index].Surname;
                                        }

                                        isCorrect = true;
                                    }
                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(0, 2);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("  Фамилия: Ошибка. Неправельный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(0, 2);
                                        Console.Write("  Фамилия:                                                     ");
                                    }
                                }

                                Console.SetCursorPosition(11, 2);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(surname);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(11, 2);
                                Console.Write(surname);

                                SelectedIndex = 2;
                                break;

                            case 2:
                                Console.SetCursorPosition(12, 3);
                                Console.Write("                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {
                                        Console.SetCursorPosition(12, 3);
                                        patronymic = Console.ReadLine();


                                        if (patronymic == "")
                                        {
                                            patronymic = users[Index].Patronymic;
                                        }

                                        isCorrect = true;
                                    }
                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(0, 3);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("  Отчество: Ошибка. Неправельный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(0, 3);
                                        Console.Write("  Отчество:                                                     ");
                                    }
                                }

                                Console.SetCursorPosition(12, 3);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(patronymic);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(12, 3);
                                Console.Write(patronymic);

                                SelectedIndex = 3;
                                break;

                            case 3:
                                Console.SetCursorPosition(30, 4);
                                Console.Write("                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {
                                        Console.SetCursorPosition(30, 4);
                                        dateOfBirth = Console.ReadLine();

                                        if(dateOfBirth == "")
                                        {
                                            dateOfBirth = users[Index].DateOfBirth;
                                            break;
                                        }

                                        DateTime dateTimeObj;
                                        isCorrect = DateTime.TryParse(dateOfBirth, out dateTimeObj);

                                        if (isCorrect == false)
                                        {
                                            Console.SetCursorPosition(0, 4);
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write("  Дата рождения [DD-MM-YYYY]:  Ошибка. Неправельный ввод.");
                                            Thread.Sleep(1000);
                                            Console.ResetColor();
                                            Console.SetCursorPosition(0, 4);
                                            Console.Write("  Дата рождения [DD-MM-YYYY]:                                                       ");
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(0, 4);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("  Дата рождения [DD-MM-YYYY]:  Ошибка. Неправельный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(0, 4);
                                        Console.Write("  Дата рождения [DD-MM-YYYY]:                                                       ");
                                    }
                                }

                                Console.SetCursorPosition(30, 4);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(dateOfBirth);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(30, 4);
                                Console.Write(dateOfBirth);

                                SelectedIndex = 4;
                                break;

                            case 4:
                                Console.SetCursorPosition(12, 5);
                                Console.Write("                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {
                                        Console.SetCursorPosition(12, 5);
                                        salary = Convert.ToInt32(Console.ReadLine());
                                        isCorrect = true;
                                    }
                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(0, 5);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("  Зарплата: Ошибка. Неправельный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(0, 5);
                                        Console.Write("  Зарплата:                                                     ");
                                    }
                                }

                                Console.SetCursorPosition(12, 5);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(salary);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(12, 5);
                                Console.Write(salary);

                                SelectedIndex = 5;
                                break;

                            case 5:
                                Console.SetCursorPosition(26, 6);
                                Console.Write("************                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {
                                        Console.SetCursorPosition(26, 6);
                                        passportInfo = Convert.ToInt64(Console.ReadLine());
                                        isCorrect = true;

                                        if (Convert.ToString(passportInfo).Length != 12)
                                        {
                                            Console.SetCursorPosition(0, 6);
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write("  Серия и номер паспорта: Ошибка. Вы должны напечатать 12 цифр.");
                                            Thread.Sleep(1000);
                                            Console.ResetColor();
                                            Console.SetCursorPosition(0, 6);
                                            Console.Write("  Серия и номер паспорта: ************                                            ");
                                            isCorrect = false;
                                        }

                                    }
                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(0, 6);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("  Серия и номер паспорта: Ошибка. Неправельный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(0, 6);
                                        Console.Write("  Серия и номер паспорта: ************                                                    ");
                                    }
                                }

                                Console.SetCursorPosition(26, 6);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(passportInfo);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(26, 6);
                                Console.Write(passportInfo);

                                SelectedIndex = 6;
                                break;

                            case 6:
                                Console.SetCursorPosition(13, 7);
                                Console.Write("                                                       ");

                                isCorrect = false;

                                while (!isCorrect)
                                {
                                    try
                                    {
                                        Console.SetCursorPosition(13, 7);
                                        job = Console.ReadLine();


                                        if (job == "")
                                        {
                                            job = users[Index].Job;
                                        }

                                        isCorrect = true;

                                    }
                                    catch (Exception)
                                    {
                                        Console.SetCursorPosition(0, 7);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("  Должность: Ошибка. Неправельный ввод.");
                                        Thread.Sleep(1000);
                                        Console.ResetColor();
                                        Console.SetCursorPosition(0, 7);
                                        Console.Write("  Должность:                                                     ");
                                    }
                                }

                                Console.SetCursorPosition(13, 7);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(job);
                                Thread.Sleep(1000);
                                Console.ResetColor();
                                Console.SetCursorPosition(13, 7);
                                Console.Write(job);

                                SelectedIndex = 6;
                                break;
                        }
                        Console.CursorVisible = false;
                        break;

                    case ConsoleKey.S:

                        users[Index] = new User((int)users[Index].ID, users[Index].Role, users[Index].Login, users[Index].Password, name , surname, patronymic, dateOfBirth, job, passportInfo, salary);
                        FileManager.SaveToFile(users, "Users.json");

                        Console.SetCursorPosition(2, 8);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Успех. Сотрудник изменен.");
                        Console.ResetColor();
                        Console.ReadLine();
                        Update(Index);
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
            users[Index] = new User((int)users[Index].ID, users[Index].Role, users[Index].Login, users[Index].Password, null, null, null, null, null, 0, 0);
            FileManager.SaveToFile(users, "Users.json");
            Display();
        }
    }
}
