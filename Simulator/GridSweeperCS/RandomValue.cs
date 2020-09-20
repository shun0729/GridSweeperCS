using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSweeperCS
{
    public static class RandomValue
    {
        static UInt32 x = 123456789;
        static UInt32 y = 362436069;
        static UInt32 z = 521288629;
        //static UInt32 w = 88675123;
        static UInt32 w = (UInt32)Environment.TickCount;

        public static uint GetRandomValue(int minNumber, int maxNumber)
        {
            UInt32 t = x ^ (x << 11);
            x = y; y = z; z = w;
            w = (w ^ (w >> 19)) ^ (t ^ (t >> 8));

            return (UInt32)(minNumber + (w % maxNumber));
        }
    }
}
