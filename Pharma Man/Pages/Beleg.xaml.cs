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

using System.Collections.ObjectModel;

using System.Device.Location;

using System.IO;

using System.ComponentModel;
using System.Net;
using Microsoft.Maps.MapControl.WPF;

namespace Pharma_Man.Pages
{
    public partial class Beleg : Page
    {
        Core.Besuch besuch;

        public Beleg(Core.Besuch besuch)
        {
            InitializeComponent();

            // Der Besuch von der "besuch erfassen" Seite, beinhaltet aklle notwendigen Daten für die PDF
            this.besuch = besuch;

            // HIER SOLL DIE PDF >OHNE< UNTERSCHRIFT ERZEUGT WERDEN <<<<<<<<<<<

            // ANZEIGEN DER ERSTELLTEN PDF 
            //pdf_web.Source = new Uri(@"PFAD DER ERSTELLTEN PDF >> OHNE UNTERSCHRIFT <<");


        }

        private void BelegBestätigen(object sender, RoutedEventArgs e)
        {
            // WENN UNTERSCHRIFT VORHANDEN
            if (true) {
                // HIER WIR DIE PDF >>MIT<< UNTERSCHRIFT ERZEUGT! <<<<<<<<<<<<

                // Speicherort der Belege
                string savePath = AppDomain.CurrentDomain.BaseDirectory + @"Belege/";

                // BelegID wird immer um 1 inkrementiert
                int belegID = Data.Datenbank.Instance.GetBelegID();

                // Das soll der Name der PDF sein
                string pdfFileName = savePath + belegID.ToString() + ".pdf";


                // HIER SOLL DIE PDF MIT UNTERSCHRIFT AM ORT "pdfFileName" gespeichert werden! <<<<<<<<<<<<








                // Setze Status auf erfasst
                this.besuch.isErfasst = true;

                // Speichere Besuch/Tagesplan
                var tagesplan = Data.Datenbank.Instance.GetTagesplan(this.besuch.Datum);
                tagesplan.UpdateBesuch(this.besuch.ID, this.besuch);
                Data.Datenbank.Instance.SaveTagesplan(tagesplan);
            }
        }
    }
}
