using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma_Man.Core
{
    public enum Status
    {
        Draft,
        Finished
    }

    [Serializable]
    public class Tagesplan
    {

        private List<Besuch> besuche = new List<Besuch>();
        public List<Besuch> Besuche { get { return besuche; } }

        private DateTime date;
        public DateTime Date { get { return date; } }

        public Status Status = Status.Draft;

        public Tagesplan(DateTime date) {
            this.date = date;
        }

        public void UpdateBesuche(List<Besuch> besuche)
        {
            this.besuche = besuche;
            
            for(int i=0; i < besuche.Count; i++)
            {
                this.besuche[i].UpdateID(i);
            }
        }

        public void AddBesuch(Core.Besuch besuch)
        {
            besuch.UpdateID(besuche.Count);
            besuche.Add(besuch);
        }

        public void UpdateBesuch(int i,Core.Besuch besuch)
        {
            //TODO
            if (i > 0 && i < this.besuche.Count)
            {
                if (this.besuche[i].ID == besuch.ID)
                {
                    this.besuche[i] = besuch;
                }
            }
        }

    }
}
