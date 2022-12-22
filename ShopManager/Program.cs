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

            new CashierMenu(list[0].Login).Display();

            new WirehauseMenu(list[0].Login).Display();



            new ManagerMenu(list[0].Login).Display();

            new HRMenu(list[0].Login).Display();
        }
    }
}
