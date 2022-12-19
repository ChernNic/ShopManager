using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManager
{
    internal interface ICrud
    {
        void Create();
        void Read();
        void Update(int Index);
        void Delete(int Index);
    }
}
