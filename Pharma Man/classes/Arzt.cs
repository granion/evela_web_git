using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma_Man
{
    public class Arzt
    {
        private int id;
        public int ID{get { return id; }}

        private string name;
        public string Name{get { return name; }}

        private Adresse adresse;
        public Adresse Adresse { get { return adresse; } }


        public Arzt(int id, string name, Adresse adresse)
        {
            this.id = id;
            this.name = name;
            this.adresse = adresse;
        }


    }
}
