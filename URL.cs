using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgurParser
{
    class URL
    {
        public static string Generate() 
        {
            Random r = new Random();

            string URL = "";
            char[] lettersAndDigits = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

            for (int i = 0; i < 7; i++)
                URL += lettersAndDigits[r.Next(0, 61)];

            return URL;
        }
    }
}
