using Pharma_Man.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma_Man
{
    class Referent : Benutzer
    {
        public Referent(int id, string vorname, string nachname, Gebiet gebietsID) : base(id, vorname, nachname)
        {
        }
    }
}
