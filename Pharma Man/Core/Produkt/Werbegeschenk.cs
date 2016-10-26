using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma_Man
{
    class Werbegeschenk : Produkt
    {
        public Werbegeschenk(int id, double preis, string beschreibung, int kategorie) : base(id, preis, beschreibung, kategorie)
        {
        }
    }
}
