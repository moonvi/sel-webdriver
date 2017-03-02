using Litecart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MainPageTests c = new MainPageTests();

            c.start();

            c.CheckAddRemoveProductsFromBasket();

            c.stop();

        }
    }
}
