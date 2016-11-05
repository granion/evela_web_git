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
        }

    }
}
