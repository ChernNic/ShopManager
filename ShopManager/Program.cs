using ShopManager.Menus;
using System;
using System.Collections.Generic;

namespace ShopManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<User> list = FileManager.ReadUsersFromFile();

            new AdminMenu().Display();
        }
    }
}
