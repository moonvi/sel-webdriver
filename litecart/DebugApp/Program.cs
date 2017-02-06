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
            AdminPageTests c = new AdminPageTests();

            c.start();

            c.CheckGeoZonesAlphabeticallySorted();

            c.stop();

        }
    }
}
