using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma_Man.data
{
    public class ÄrzteData
    {
        private static List<Arzt> list_Arzt = new List<Arzt>();
        public static List<Arzt> Ärzte { get { return list_Arzt; } }

        private static ÄrzteData instance;
        public static ÄrzteData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ÄrzteData();
                }
                return instance;
            }
        }

        private ÄrzteData() {
            list_Arzt.Add(new Arzt(list_Arzt.Count + 1, "Dr. Test", new Adresse(74211, "Im Lützelfeld", 5, "Leingarten")));
            list_Arzt.Add(new Arzt(list_Arzt.Count + 1, "Dr. Hans", new Adresse(74211, "Platenenweg", 6, "Leingarten")));
            list_Arzt.Add(new Arzt(list_Arzt.Count + 1, "Dr. Bauch", new Adresse(74211, "Schwaigerner Straße", 7, "Leingarten")));
            list_Arzt.Add(new Arzt(list_Arzt.Count + 1, "Dr. Brett", new Adresse(74211, "Am Freibad", 8, "Leingarten")));
            list_Arzt.Add(new Arzt(list_Arzt.Count + 1, "Dr. Scharf", new Adresse(74211, "Karlsruher Straße", 9, "Leingarten")));
            list_Arzt.Add(new Arzt(list_Arzt.Count + 1, "Dr. Wizofskie", new Adresse(74211, "Augelbaum Straße", 4, "Leingarten")));
        }

    }
}
