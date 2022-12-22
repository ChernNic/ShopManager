using ShopManager.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManager
{
    static class Authorization
    {
        public static bool Check(string login, string password)
        {
            bool isAuthorized = false;

            List<User> users = FileManager.ReadUsersFromFile();

            foreach(User user in users)
            {
                if (user.Login != login || user.Password != password)
                {
                    isAuthorized = false;
                }
                else
                {
                    switch (user.Role)
                    {
                        case 0:
                            new AdminMenu(login).Display();
                            break;
                        case 1:
                            new HRMenu(login).Display();
                            break;
                        case 2:
                            new ManagerMenu(login).Display();
                            break;
                        case 3:
                            new WirehauseMenu(login).Display();
                            break;
                        case 4:
                            new CashierMenu(login).Display();
                            break;
                    }
                    isAuthorized = true;
                }     
            }
            return isAuthorized;
        }
    }
}
