using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManager
{
    internal class Product
    {
        public int ID;
        public string Name;
        public int Cost;
        public int Amount;

        public Product(int id, string name, int cost, int amount)
        {
            ID = id;
            Name = name;
            Cost = cost;
            Amount = amount;
        }
    }
}
