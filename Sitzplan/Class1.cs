using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitzplan
{
    public class TErgebnis
    {
        public struct Ergebnis
        {
            public Ergebnis(string P)
            {
                Paar = P;
                
            }
            public string Paar { get; set; }
           
        }
    }
}
