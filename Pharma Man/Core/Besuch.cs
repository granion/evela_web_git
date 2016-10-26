using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma_Man.Core
{
    public class Besuch
    {

        private int id;
        public int ID { get { return id; } }

        private DateTime terminStart;
        public DateTime TerminStart { get { return terminStart; } }

        private DateTime terminEnde;
        public DateTime TerminEnde { get { return terminEnde; } }

        private Produkt[] produkte;
        public Produkt[] Produkte { get { return produkte; } }

        public Besuch(int id, DateTime start, DateTime ende, Produkt[] produkte)
        {
            this.id = id;
            this.terminStart = start;
            this.terminEnde = ende;
            this.produkte = produkte;
        }

        public int calculateDuration(DateTime anfang, DateTime ende)
        {
            TimeSpan duration = ende.Subtract(anfang);

            return duration.Minutes;
        }
    }
}
