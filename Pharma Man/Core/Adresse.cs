using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma_Man.Core
{
    public class Adresse
    {
        private int plz;
        public int PLZ{get { return plz; }}

        private string straße;
        public string Straße{ get { return straße; }}

        private int hausNr;
        public int HausNr{get { return hausNr; }}

        private string ort;
        public string Ort{get { return ort; }}

        private string zusatz;
        public string Zusatz{get { return zusatz; }}


        public Adresse(int plz, string straße, int hausNr, string ort, string zusatz=null)
        {
            this.plz = plz;
            this.straße = straße;
            this.hausNr = hausNr;
            this.ort = ort;
            this.zusatz = zusatz;
        }
    }
}
