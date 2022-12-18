using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManager
{
    internal class Transaction
    {
        public int ID;
        public string Name;
        public int Amount_Of_Money;
        public DateTime Date;
        public bool Increase;

        public Transaction(int id, string name, int amount_of_money, DateTime date, bool increase)
        {
            ID = id;
            Name = name;
            Amount_Of_Money = amount_of_money;
            Date = date;
            Increase = increase;
        }
    }
}
