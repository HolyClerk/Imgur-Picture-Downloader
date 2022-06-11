using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgurParser.Link
{
    class LinkRandomizer
    {
        public string GetRandomizedLink(int length)
        {
            var URL = "";

            var random = new Random();
            char[] lettersAndDigits = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

            for (int i = 0; i < length; i++)
            {
                URL += lettersAndDigits[random.Next(0, 61)];
            }

            return "https://i.imgur.com/" + URL + ".png";
        }
    }
}
