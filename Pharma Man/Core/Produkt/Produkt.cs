using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma_Man {
    [Serializable]
    public class Produkt {

        private int id;
        public int ID { get { return id; } }

        private double preis;
        public double Preis { get { return preis; } }

        private string beschreibung;
        public string Beschreibung { get { return beschreibung; } }

        private int kategorie;
        public int Kategorie { get { return kategorie; } }

        public Produkt(int id, double preis, string beschreibung, int kategorie)
        {
            this.id = id;
            this.preis = preis;
            this.beschreibung = beschreibung;
            this.kategorie = kategorie;
        }


    }
}
