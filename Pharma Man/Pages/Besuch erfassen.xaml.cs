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
using System.ComponentModel;

namespace Pharma_Man.Pages
{

    public partial class Besuch_erfassen : Page
    {

        Core.Besuch besuch;

        //Ärzteliste
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        private List<Core.Arzt> ärzte = Data.Datenbank.Instance.ärzte.Values.ToList();

        public Besuch_erfassen(Core.Besuch besuch)
        {
            InitializeComponent();

            tb_Arzt.IsReadOnly = true;
            tb_Zeit.IsReadOnly = true;


            this.besuch = besuch;
            tb_Zeit.Text = besuch.Datum.ToShortDateString();
            tb_Von.Text = besuch.TerminStart.ToShortTimeString();
            tb_Bis.Text = besuch.TerminEnde.ToShortTimeString();

            if(besuch.Arzt != null) tb_Arzt.Text = besuch.Arzt.Name;

            tb_Thema.Text = besuch.Thema;

            tb_Notizen.Text = besuch.Notiz;



            // Init Ärzteliste
            ärzteListe.ItemsSource = ärzte;

            // Sortierfunktion Ärzteliste
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ärzteListe.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("Priorität", ListSortDirection.Ascending));

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

        private void AddArzt(object sender, RoutedEventArgs e)
        {
            if(ärzteListe.SelectedItem != null)
            {
                var arzt = ärzteListe.SelectedItem as Core.Arzt;
                tb_Arzt.Text = arzt.Name;
                this.besuch.SetArzt(arzt);
            }
            Grid_Modal.Visibility = Visibility.Hidden;

        }

        private void ShowModal(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Visible;
        }

        private void HideModal(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Hidden;
        }

        private void ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                ärzteListe.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            ärzteListe.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }

    }
}


