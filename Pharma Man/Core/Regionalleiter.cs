using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma_Man.Core
{
    class Regionalleiter : Benutzer
    {

        private int[] bereiche;
        public int[] Bereiche { get { return bereiche; } }

        public Regionalleiter(int id, string vorname, string nachname) : base(id, vorname, nachname)
        {
        }

        //möglich?
        public Regionalleiter(int id, string vorname, string nachname, int[] bereiche) : base(id, vorname, nachname)
        {
            this.bereiche = bereiche;
        }
    }
}
