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
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using Pharma_Man.controls;
using System.Diagnostics;
using Pharma_Man.Core;
using Pharma_Man.Pages;

namespace Pharma_Man.Pages
{

    public partial class Besuch_erfassen : Page
    {

        Core.Besuch besuch;

        public Besuch_erfassen(Core.Besuch besuch)
        {
            InitializeComponent();

            this.besuch = besuch;
            tb_Zeit.Text = besuch.Datum.ToShortDateString();
            tb_Von.Text = besuch.TerminStart.ToShortTimeString();
            tb_Bis.Text = besuch.TerminEnde.ToShortTimeString();

            if(besuch.Arzt != null) tb_Arzt.Text = besuch.Arzt.Name;

            tb_Thema.Text = besuch.Thema;

            tb_Notizen.Text = besuch.Notiz;
        }

        private bool BesuchSpeichern()
        {
            // Status setzen
            this.besuch.isErfasst = false;

            // Breche ab, falls eines der Felder fehlerhaft ist
            if (!CheckFelder()) return false;

            // Speichere Daten in Besuch
            ParseData();

            // Tagesplan abrufen
            Core.Tagesplan tagesplan = Data.Datenbank.Instance.GetTagesplan(besuch.Datum);

            // Wenn kein Tagesplan vorhanden, lege neuen an und füge den Besuch hinzu
            if (tagesplan == null)
            {
                tagesplan = new Core.Tagesplan(besuch.Datum);

                // Besuch in Tagesplan speichern
                tagesplan.AddBesuch(this.besuch);
            }
            else
            {
                tagesplan.UpdateBesuch(this.besuch.ID, this.besuch);
            }

            // Tagesplan speichern
            Data.Datenbank.Instance.SaveTagesplan(tagesplan);

            return true;

        }

        // Prüfe Felder
        private bool CheckFelder()
        {
            try
            {
                // Zeit
                // Von
                var von = DateTime.Parse(tb_Von.Text);
                // Bis
                var bis = DateTime.Parse(tb_Bis.Text);

                foreach(Core.Arzt arzt in Data.Datenbank.Instance.ärzte.Values)
                {
                    if (tb_Arzt.Text == arzt.Name) return true;
                }
                throw new Exception();
            }
            catch
            {
                MessageBox.Show("Bitte Eingaben prüfen!", "Fehlerhafte Eingabe.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        // Speichere Daten im Besuch
        private void ParseData()
        {
            this.besuch.UpdateTermin(DateTime.Parse(tb_Von.Text), DateTime.Parse(tb_Bis.Text));
            
            this.besuch.Thema = tb_Thema.Text;
            this.besuch.Notiz = tb_Notizen.Text;

            if(this.besuch.Arzt == null)
            {
                foreach (Core.Arzt arzt in Data.Datenbank.Instance.ärzte.Values)
                {
                    if (tb_Arzt.Text == arzt.Name) this.besuch.SetArzt(arzt);
                }
            }

            // Haben keine Produkte :/
            this.besuch.UpdateProdukte(null);
        }

        private void btn_BesuchSpeichern(object sender, RoutedEventArgs e)
        {
            // Speichere Besuch
            BesuchSpeichern();
        }

        private void BesuchAbschließen(object sender, RoutedEventArgs e)
        {
            // Speichere Besuch
            if (BesuchSpeichern())
            {
                //Zeige Beleg an, falls erfolgreich
                Pages.Beleg beleg = new Beleg(this.besuch);

                NavigationService nav = NavigationService.GetNavigationService(this);
                nav.Navigate(beleg);
            }

        }
    }
}


