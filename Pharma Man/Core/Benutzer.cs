using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma_Man
{
    public class Benutzer
    {

        private int id;
        public int ID { get { return id; } }

        private string name;
        public string Name { get { return name; } }

        private string vorname;
        public string Vorname { get { return vorname; } }

        public Benutzer(int id, string vorname, string nachname)
        {
            this.id = id;
            this.name = nachname;
            this.vorname = vorname;
        }
    }
}
