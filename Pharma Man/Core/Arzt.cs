using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace Pharma_Man.Core
{
    [Serializable]
    public class Arzt
    {
        private int id;
        public int ID{get { return id; }}

        private string name;
        public string Name{get { return name; }}

        private Adresse adresse;
        public Adresse Adresse { get { return adresse; } }

        private int priorität;
        public int Priorität { get { return priorität; } }

        public Arzt(int id, string name, Adresse adresse, int prio)
        {
            this.id = id;
            this.name = name;
            this.adresse = adresse;
            this.priorität = prio;
        }


    }
}
