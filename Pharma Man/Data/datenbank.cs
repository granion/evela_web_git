using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pharma_Man.Core;

namespace Pharma_Man.Data {
    public class Datenbank {

        Dictionary<int, Gebiet> gebiete = new Dictionary<int, Gebiet>();

        Dictionary<int, Arzt> ärzte = new Dictionary<int, Arzt>();

        Dictionary<int, Besuch> besuche = new Dictionary<int, Besuch>();

        public int GetBesuchID() { return besuche.Count; }


        private static Datenbank instance;
        public static Datenbank Instance
        {
            get
            {
                if (instance == null) {
                    instance = new Datenbank();
                }
                return instance;
            }
        }

        private Datenbank() {


            InitGebiete();
            InitÄrzte();


            foreach(Arzt arzt in ärzte.Values) {
                gebiete[0].AddArzt(arzt);
            }

        }

        private void InitGebiete() {
            gebiete.Add(gebiete.Count, new Gebiet(gebiete.Count,"Mosbach"));
        }

        private void InitÄrzte() {
            ärzte.Add(ärzte.Count, new Arzt(ärzte.Count, "Dr. Test", new Adresse(74211, "Im Lützelfeld", 5, "Leingarten"), 1));
            ärzte.Add(ärzte.Count, new Arzt(ärzte.Count, "Dr. Hans", new Adresse(74211, "Platenenweg", 6, "Leingarten"), 1));
            ärzte.Add(ärzte.Count, new Arzt(ärzte.Count, "Dr. Bauch", new Adresse(74211, "Schwaigerner Straße", 7, "Leingarten"), 1));
            ärzte.Add(ärzte.Count, new Arzt(ärzte.Count, "Dr. Brett", new Adresse(74211, "Am Freibad", 8, "Leingarten"), 1));
            ärzte.Add(ärzte.Count, new Arzt(ärzte.Count, "Dr. Scharf", new Adresse(74211, "Karlsruher Straße", 9, "Leingarten"), 1));
            ärzte.Add(ärzte.Count, new Arzt(ärzte.Count, "Dr. Wizofskie", new Adresse(74211, "Augelbaum Straße", 4, "Leingarten"), 1));
        }


        public Arzt GetArzt(int id) {
            return ärzte[id];
        }

        public Gebiet GetGebiet(int id) {
            return gebiete[id];
        }

        public Gebiet GetGebiet(string name) {
            foreach (Gebiet gebiet in gebiete.Values) {
                if (gebiet.Name.ToUpper() == name.ToUpper()) {
                    return gebiet;
                }
            }
            return null;
        }

    }
}
