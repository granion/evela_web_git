using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pharma_Man.Pages {

    public partial class Besuch_erfassen : Page {

        Core.Besuch besuch;

        public Besuch_erfassen(Core.Besuch besuch) {
            InitializeComponent();

            this.besuch = besuch;
            tb_Zeit.Text = besuch.Datum.ToShortDateString();
        }


        private void BesuchSpeichern(object sender, RoutedEventArgs e)
        {
            // Status setzen
            this.besuch.isErfasst = false;

            // Felder Prüfen


            // Werte aus Feldern in Besuch speichern


            // Tagesplan abrufen
            Core.Tagesplan tagesplan = Data.Datenbank.Instance.GetTagesplan(besuch.Datum);

            // Wenn kein Tagesplan vorhanden, lege neuen an und füge den Besuch hinzu
            if (tagesplan == null) tagesplan = new Core.Tagesplan(besuch.Datum); 

            // Besuch in Tagesplan speichern
            tagesplan.AddBesuch(this.besuch);

            // Tagesplan speichern
            Data.Datenbank.Instance.SaveTagesplan(tagesplan);
        }

        private void BesuchAbschließen(object sender, RoutedEventArgs e)
        {
            // Status setzen
            this.besuch.isErfasst = false;

            // Felder Prüfen


            // Werte aus Feldern in Besuch speichern

            // PDF aus Besuch erzeugen

        }



    }
}
