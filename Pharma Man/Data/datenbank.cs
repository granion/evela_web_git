using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pharma_Man.Core;
using System.IO;

namespace Pharma_Man.Data {
    public class Datenbank {

        public Dictionary<int, Gebiet> gebiete = new Dictionary<int, Gebiet>();

        public Dictionary<int, Arzt> ärzte = new Dictionary<int, Arzt>();

        public Dictionary<int, Besuch> besuche = new Dictionary<int, Besuch>();

        public Dictionary<DateTime, Core.Tagesplan> tagespläne = new Dictionary<DateTime, Tagesplan>();

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
            LoadTagespläne();


            foreach(Arzt arzt in ärzte.Values) {
                gebiete[0].AddArzt(arzt);
            }

        }

        private void InitGebiete() {
            gebiete.Add(gebiete.Count, new Gebiet(gebiete.Count,"Mosbach"));
        }

        private void InitÄrzte()
        {
            ärzte.Add(ärzte.Count, new Arzt(ärzte.Count, "Dr. Test", new Adresse(74211, "Im Lützelfeld", 5, "Leingarten", 49.145467, 9.219008), 1));
            ärzte.Add(ärzte.Count, new Arzt(ärzte.Count, "Dr. Hans", new Adresse(74211, "Platenenweg", 6, "Leingarten", 49.136434, 9.212970), 2));
            ärzte.Add(ärzte.Count, new Arzt(ärzte.Count, "Dr. Bauch", new Adresse(74211, "Schwaigerner Straße", 7, "Leingarten", 49.138280, 9.224167), 3));
            ärzte.Add(ärzte.Count, new Arzt(ärzte.Count, "Dr. Brett", new Adresse(74211, "Am Freibad", 8, "Leingarten", 49.145189, 9.234029), 4));
            ärzte.Add(ärzte.Count, new Arzt(ärzte.Count, "Dr. Scharf", new Adresse(74211, "Karlsruher Straße", 9, "Leingarten", 49.151243, 9.211938), 2));
            ärzte.Add(ärzte.Count, new Arzt(ärzte.Count, "Dr. Wizofskie", new Adresse(74211, "Augelbaum Straße", 4, "Leingarten", 49.149298, 9.241282), 1));
        }

        private void LoadTagespläne()
        {
            if (Directory.Exists("./Tagespläne"))
            {
                foreach (var file in Directory.GetFiles("./Tagespläne", "*.bin"))
                {
                    var tagesplan = Binary_Parser.LoadTagesplan(file);
                    if (tagesplan != null) tagespläne.Add(tagesplan.Date, tagesplan);
                }
            }
            else Directory.CreateDirectory("./Tagespläne");
        }

        public int GetBelegID()
        {
            if (Directory.Exists("./Belege"))
            {
                var number =  Directory.GetFiles("./Belege", "*.pdf").Count();
                return number;
            }
            else Directory.CreateDirectory("./Belege");
            return 0;
        }

        public void SaveTagesplan(Core.Tagesplan tagesplan)
        {
            if (tagespläne.Keys.Contains(tagesplan.Date)) tagespläne[tagesplan.Date] = tagesplan;
            else tagespläne.Add(tagesplan.Date, tagesplan);           
            Core.Binary_Parser.SaveTagesplan(tagesplan);
        }

        public Core.Tagesplan GetTagesplan(DateTime date)
        {
            if (tagespläne.Keys.Contains(date)) return tagespläne[date];
            else return null;
        }


        public void UpdateArztPrio(int arztID,int prio) {
            ärzte[arztID].UpdatePriorität(prio);
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
