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
                            Console.WriteLine(12);
                            break;
                        case 1:
                            Console.WriteLine(0);
                            break;
                    }
                    isAuthorized = true;
                }     
            }
            return isAuthorized;
        }
    }
}
