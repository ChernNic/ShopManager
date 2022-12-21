using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopManager
{
    internal class User
    {
        public int ID;
        public string Login;
        public string Password;
        public string Name;
        public string Surname;
        public string Patronymic;
        public string DateOfBirth;
        public string Job;
        public long PassportInfo;
        public int Salary;
        public int Role;

        public User(int id, int role, string login, string password, string name = null, string surname = null, string patronymic = null, string dateOfBirth = null, string job = null, long passportInfo = 0, int salary = 0)
        {
            ID = id;
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            DateOfBirth = dateOfBirth;
            Job = job;
            PassportInfo = passportInfo;
            Salary = salary;
            Role = role;
        }
    }
}
