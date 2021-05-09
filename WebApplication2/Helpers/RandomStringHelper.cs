using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Helpers
{
    public class RandomStringHelper
    {
        public string Generate(int length)
        {
            var result = "";
            var rnd = new RndNumberGenerator();
            var PosibleChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!#$%&()_-[]*@+¿?¡";

            for (int i = 0; i < length; i++)
            {
                var rndPosition = rnd.GenerateInt(0, PosibleChars.Count() - 1);
                result += PosibleChars[rndPosition];
            }

            return result;
        }

        public string GenerateCode()
        {
            var result = "";
            var rnd = new RndNumberGenerator();

            //Posible Values
            var Posiblealpha = "abcdefghijklmnopqrstuvwxyz";
            var posibleNumbers = "0123456789";
            var posibleSimbols = "!#$%&()_-[]*@+¿?¡";

            //Code Generation
            result += rnd.GenerateInt(0, 100) < 50 ? (Posiblealpha[rnd.GenerateInt(0, Posiblealpha.Count() - 1)]).ToString().ToUpper() : Posiblealpha[rnd.GenerateInt(0, Posiblealpha.Count() - 1)].ToString();
            result += rnd.GenerateInt(0, 100) < 50 ? (Posiblealpha[rnd.GenerateInt(0, Posiblealpha.Count() - 1)]).ToString().ToUpper() : Posiblealpha[rnd.GenerateInt(0, Posiblealpha.Count() - 1)].ToString();
            result += posibleNumbers[rnd.GenerateInt(0, posibleNumbers.Count() - 1)];
            result += posibleNumbers[rnd.GenerateInt(0, posibleNumbers.Count() - 1)];
            result += posibleSimbols[rnd.GenerateInt(0, posibleSimbols.Count() - 1)];
            result += posibleSimbols[rnd.GenerateInt(0, posibleSimbols.Count() - 1)];

            //Code Randomization
            List<char> lista = new List<char>();
            lista.AddRange(result);

            lista = lista.OrderBy(a => Guid.NewGuid()).ToList();
            var join = string.Join("", lista);

            return join;
        }


    }
}