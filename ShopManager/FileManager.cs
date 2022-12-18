using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace ShopManager
{
    static class FileManager
    {
        static public void SaveToFile<T>(T list, string path)
        {
            if (!File.Exists(path))
            {
                FileStream fileStream = File.Create(path);
                fileStream.Dispose();
            }

            string json = JsonConvert.SerializeObject(list);
            File.WriteAllText(path, json);
        }

        static public List<User> ReadUsersFromFile()
        {
            List<User> users;
            if (!File.Exists("Users.json"))
            {
                FileStream fileStream = File.Create("Users.json");
                fileStream.Dispose();

                users = new List<User>();
                users.Add(new User(0, 0, "Admin", "Admin"));
                SaveToFile(users, "Users.json");
            }
            else
            {
                string usersInfo = File.ReadAllText("Users.json");
                users = JsonConvert.DeserializeObject<List<User>>(usersInfo);
            }
            return users;
        }
    }
}
