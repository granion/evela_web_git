using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma_Man
{
    class Bestellung
    {

        private int bestellID;
        public int BestellID { get { return bestellID; } }

        private Produkt[] produkte;
        public Produkt[] Produkte { get { return produkte; } }

        private DateTime bestelldatum;
        public DateTime Bestelldatum { get { return bestelldatum; } }

        public Bestellung(int id, Produkt[] produkte, DateTime bestelldatum)
        {
            this.bestellID = id;
            this.produkte = produkte;
            this.bestelldatum = bestelldatum;
        }
        
    }
}
