using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgurParser.Parser
{
    internal struct StateOfParser
    {
        public bool IsBusy { get; set; }
        public int NumberOfTry { get; set; }
    }
}
