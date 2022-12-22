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
            if (!File.Exists(Environment.SystemDirectory + path))
            {
                FileStream fileStream = File.Create(Environment.SystemDirectory + "/" + path);
                fileStream.Dispose();
            }

            string json = JsonConvert.SerializeObject(list);
            File.WriteAllText(Environment.SystemDirectory + "/" + path, json);
        }

        static public List<User> ReadUsersFromFile()
        {
            List<User> users;
            if (!File.Exists(Environment.SystemDirectory + "/" + "Users.json"))
            {
                FileStream fileStream = File.Create(Environment.SystemDirectory + "/" + "Users.json");
                fileStream.Dispose();

                users = new List<User>();
                users.Add(new User(0, 0, "Admin", "Admin"));
                SaveToFile(users, Environment.SystemDirectory + "/" + "Users.json");
            }
            else
            {
                string usersInfo = File.ReadAllText(Environment.SystemDirectory + "/" + "Users.json");
                users = JsonConvert.DeserializeObject<List<User>>(usersInfo);
            }
            return users;
        }

        static public List<T> ReadFromFile<T>(string path)
        {
            List<T> result;
            if (!File.Exists(Environment.SystemDirectory + "/" + path))
            {
                FileStream fileStream = File.Create(Environment.SystemDirectory + "/" + path);
                fileStream.Dispose();
            }

            string resultInfo = File.ReadAllText(Environment.SystemDirectory + "/" + path);
            result = JsonConvert.DeserializeObject<List<T>>(resultInfo);

            return result;
        }
    }
}
