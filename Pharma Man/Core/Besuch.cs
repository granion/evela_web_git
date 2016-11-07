using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma_Man.Core
{
    [Serializable]
    public class Besuch
    {
        private int? id;
        public int? ID { get { return id; } }

        private DateTime datum;
        public DateTime Datum { get { return datum; } }

        private DateTime terminStart;
        public DateTime TerminStart { get { return terminStart; } }

        private DateTime terminEnde;
        public DateTime TerminEnde { get { return terminEnde; } }

        private Produkt[] produkte;
        public Produkt[] Produkte { get { return produkte; } }

        private Arzt arzt;
        public Arzt Arzt { get { return arzt; } }

        #region Für Besuchserfassung

        //Flag für Besuchserfassung
        public bool isErfasst = false;

        #endregion

        #region Für Tagesplanung

        //Flag für Tagesplanung
        public bool isLocked = false;
        public TimeSpan geschätzteDauer;

        #endregion

        public Besuch(DateTime datum,   Arzt arzt=null, int? id=null)
        {
            this.datum = datum;
            this.arzt = arzt;
        }

        public int calculateDuration(DateTime anfang, DateTime ende)
        {
            TimeSpan duration = ende.Subtract(anfang);

            return duration.Minutes;
        }

        public void UpdateProdukte(Produkt[] produkte)
        {
            this.produkte = produkte;
        }

        public void UpdateTermin(DateTime start, DateTime ende)
        {
            this.terminStart = start;
            this.terminEnde = ende;
        }

        public void UpdateID(int id)
        {
            this.id = id;
        }
    }
}
