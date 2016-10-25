using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma_Man{
    public class Ärztemuster : Produkt
    {
        public Ärztemuster(int id, double preis, string beschreibung, int kategorie) : base(id, preis, beschreibung, kategorie)
        {
        }
    }
}
