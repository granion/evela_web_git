using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma_Man.Core {
    public class Gebiet {

        private int id;
        public int ID { get { return id; } }

        private string name;
        public string Name { get { return name; } }

        Dictionary<int, Arzt> ärzte = new Dictionary<int, Arzt>();

        public Gebiet(int id,string name) {
            this.id = id;
            this.name = name;
        }

        public void AddArzt(Arzt arzt) {
            ärzte.Add(arzt.ID , arzt);
        }



    }
}
