using litecart;
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
            LoginPageTests c = new LoginPageTests();

            c.start();

            c.CheckMenuHeadersPresented();

            c.stop();
        }
    }
}
