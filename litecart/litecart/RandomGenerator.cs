using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litecart
{
    public class RandomGenerator
    {
        private static DateTime timeStartPoint = new DateTime(2017, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long GetTimeBasedRndNum()
        {
            TimeSpan diff = DateTime.UtcNow - timeStartPoint;
            return diff.Ticks;
        }
    }
}
