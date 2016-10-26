using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma_Man.Core { 
    public class Besuchsplan{

        private Arzt[] ärzte;
        public Arzt[] Ärzte { get { return ärzte; } }

        public Besuchsplan(Arzt[] ärzte)
        {
            this.ärzte = ärzte;
        }
    }
}
