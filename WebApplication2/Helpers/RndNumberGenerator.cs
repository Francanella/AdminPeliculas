using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Helpers
{
    public class RndNumberGenerator
    {
        Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000ffff);

        public int GenerateInt(int minValue = 0, int maxValue = 100)
        {
            var value = rnd.Next(minValue, maxValue + 1);

            return value;
        }

        public double GenerateDouble(int minValue = 0, int maxValue = 100)
        {
            var value = rnd.NextDouble() * (maxValue - minValue) + minValue;

            return value;
        }
    }

}