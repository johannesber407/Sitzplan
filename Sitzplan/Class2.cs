using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitzplan
{
    public class TBlockierteKombination
    {
        public struct BlockierteKombination
        {
            public BlockierteKombination(string s)
            {
                Blockiert = s;

            }
            public string Blockiert { get; set; }
        }
    }
}
