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
    public partial class Tagesplanung : Page
    {
        private ObservableCollection<Core.Besuch> items = new ObservableCollection<Core.Besuch>();
        private Core.Tagesplan tagesplan;

        //Ärzteliste
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        private List<Core.Arzt> ärzte = Data.Datenbank.Instance.ärzte.Values.ToList();

        //Map
        double gesamtDistanz = 0;

        //Time
        int selectedId = 0;

        public Tagesplanung(Core.Tagesplan tagesplan)
        {

            InitializeComponent();

            lbl_pageCaption.Content = "Tagesplan";
            lbl_date.Content = "für den " + tagesplan.Date.ToShortDateString() + ".";

            this.tagesplan = tagesplan;

            // Lade Besuche, falls vorhanden
            if (this.tagesplan.Besuche.Count > 0)
            {
                tagesplan.Besuche.ForEach(x => items.Add(x));
                GesamtEntfernungAusrechnen();
            }

            // Übertrage Besuche in docList (ListBox)
            docList.ItemsSource = items;

            // Init Ärzteliste
            ärzteListe.ItemsSource = ärzte;

            // Sortierfunktion Ärzteliste
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ärzteListe.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("Priorität", ListSortDirection.Ascending));

            GesamtEntfernungAusrechnen();
            GeschätzteDauerBerechnen();
        }

        private void UpdateIDs()
        {
            for(int i = 0; i < items.Count; i++)
            {
                items[i].UpdateID(i);
            }

            docList.UpdateLayout();
        }

        #region Button-Events


        private void MoveItemUp(object sender, MouseButtonEventArgs e)
        {
            var selectedIndex = this.docList.SelectedIndex;

            if (selectedIndex > 0 && selectedIndex<items.Count)
            {
                var itemToMoveUp = this.items[selectedIndex];

                for (int i = 1; selectedIndex - i >= 0; i++)
                {
                    if (!items[selectedIndex - i].isLocked)
                    {
                        var itemToSwap = this.items[selectedIndex - i];

                        this.items.Remove(itemToMoveUp);
                        this.items.Remove(itemToSwap);

                        this.items.Insert(selectedIndex - i, itemToMoveUp);
                        this.items.Insert(selectedIndex, itemToSwap);

                        this.docList.SelectedIndex = selectedIndex - i;
                        break;
                    }
                }
            }
            UpdateIDs();
            docList.UpdateLayout();
            GesamtEntfernungAusrechnen();
        }

        private void MoveItemDown(object sender, MouseButtonEventArgs e)
        {
            var selectedIndex = this.docList.SelectedIndex;
            if (selectedIndex >= 0)
            {
                var itemToMoveDown = this.items[selectedIndex];

                if (selectedIndex + 1 < this.items.Count)
                {
                    for (int i = 1; selectedIndex + i < this.items.Count; i++)
                    {
                        if (!items[selectedIndex + i].isLocked)
                        {
                            var itemToSwap = this.items[selectedIndex + i];

                            this.items.Remove(itemToMoveDown);
                            this.items.Remove(itemToSwap);

                            this.items.Insert(selectedIndex, itemToSwap);
                            this.items.Insert(selectedIndex + i, itemToMoveDown);

                            this.docList.SelectedIndex = selectedIndex + i;
                            break;
                        }
                    }
                }
                UpdateIDs();
                docList.UpdateLayout();
                GesamtEntfernungAusrechnen();
            }
        }

        private void LockListItem(object sender, MouseButtonEventArgs e)
        {
            UserControls.ImageButton button = sender as UserControls.ImageButton;
            Core.Besuch item = button.DataContext as Core.Besuch;
            item.isLocked = !item.isLocked;

        }

        private void AddBesuch(object sender, RoutedEventArgs e)
        {
            foreach (var item in ärzteListe.SelectedItems)
            {
                var arzt = item as Core.Arzt;
                items.Add(new Core.Besuch(this.tagesplan.Date, arzt));
            }

            UpdateIDs();
            docList.UpdateLayout();
            Grid_Modal.Visibility = Visibility.Hidden;
            GesamtEntfernungAusrechnen();
        }

        private void TagesplanSpeichern(object sender, RoutedEventArgs e)
        {
            this.tagesplan.UpdateBesuche(items.ToList());
            this.tagesplan.Status = Core.Status.Draft;
            Data.Datenbank.Instance.SaveTagesplan(this.tagesplan);
        }

        private void TagesplanAbschließen(object sender, RoutedEventArgs e)
        {
            this.tagesplan.UpdateBesuche(items.ToList());
            this.tagesplan.Status = Core.Status.Finished;
            Data.Datenbank.Instance.SaveTagesplan(this.tagesplan);
        }

        #endregion

        #region Ärzteliste Methods

        private void ShowModal(object sender, MouseButtonEventArgs e)
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


        #endregion

        private void RouteOptimieren(object sender, RoutedEventArgs e)
        {
            // Besuche temporär speichern
            var tmpList = items;

            // Sind mehr als 2 Besuche vorhanden?
            if (tmpList.Count > 2)
            {
                // Äußere Schleife
                for (int i = 0; i + 1 < tmpList.Count; i++)
                {
                    int? id = null;
                    double? minDistance = null;

                    // Innere Schleife - Suche kürzesten weg zu einem Besuch
                    for (int j = i + 1; j + 1 < tmpList.Count; j++)
                    {
                        // Überspringe falls nächster Besuch im Plan gesperrt ist
                        if (items[j].isLocked) break;

                        // Berechne Distanz zwischen Besuch(i) und Besuch(j)
                        GeoCoordinate ort1 = new GeoCoordinate(items[i].Arzt.Adresse.Latitude, items[i].Arzt.Adresse.Longitude);
                        GeoCoordinate ort2 = new GeoCoordinate(items[j].Arzt.Adresse.Latitude, items[j].Arzt.Adresse.Longitude);
                        double distance = ort1.GetDistanceTo(ort2);

                        // Speichere Distanz zum ersten folge Besuch
                        if (minDistance == null)
                        {
                            minDistance = distance;
                            continue;
                        }

                        // Speichere Distanz, falls Distanz < mindest Distanz
                        if (distance < minDistance)
                        {
                            id = j;
                            minDistance = distance;
                        }
                    }

                    // Wenn id != null, tausche Besuche
                    if (id != null)
                    {
                        // Speichere Besuche
                        var currentBesuch = items[i + 1];
                        var shorterBesuch = this.items[id.Value];

                        // Lösche Besuche aus Liste
                        this.items.Remove(currentBesuch);
                        this.items.Remove(shorterBesuch);

                        // Füge Besuche an getauschte Positionen wieder ein
                        this.items.Insert(i + 1, shorterBesuch);
                        this.items.Insert(id.Value, currentBesuch);
                    }
                }

                double value = 0;
                for (int i = 0; i + 1 < tmpList.Count; i++)
                {
                    GeoCoordinate ort1 = new GeoCoordinate(tmpList[i].Arzt.Adresse.Latitude, tmpList[i].Arzt.Adresse.Longitude);
                    GeoCoordinate ort2 = new GeoCoordinate(tmpList[i + 1].Arzt.Adresse.Latitude, tmpList[i + 1].Arzt.Adresse.Longitude);

                    value += ort1.GetDistanceTo(ort2);
                }

                if (value < gesamtDistanz)
                {
                    items = tmpList;
                    gesamtDistanz = value;
                }
                UpdateIDs();
                docList.UpdateLayout();
                GesamtEntfernungAusrechnen();
            }
        }

        private void GesamtEntfernungAusrechnen()
        {
            double value = 0;
            for (int i = 0; i + 1 < items.Count; i++)
            {
                GeoCoordinate ort1 = new GeoCoordinate(items[i].Arzt.Adresse.Latitude, items[i].Arzt.Adresse.Longitude);
                GeoCoordinate ort2 = new GeoCoordinate(items[i + 1].Arzt.Adresse.Latitude, items[i + 1].Arzt.Adresse.Longitude);

                value += ort1.GetDistanceTo(ort2);
            }
            gesamtDistanz = value;
            lbl_map_distance.Content = "Gesamtstrecke: " + (Convert.ToInt32(value)).ToString() + "m.";
            SetPins();
        }

        private void SetPins()
        {
            BingMap.Children.Clear();

            foreach (Core.Besuch besuch in items)
            {
                Pushpin pin = new Pushpin();
                pin.Tag = besuch.Arzt.Name;
                pin.Location = new Location(besuch.Arzt.Adresse.Latitude, besuch.Arzt.Adresse.Longitude);
                BingMap.Children.Add(pin);
            }
            CenterMap();
            DrawLine();
        }

        private void DrawLine()
        {
            if (items.Count > 1)
            {
                MapPolyline polyline = new MapPolyline();
                polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
                polyline.StrokeThickness = 5;
                polyline.Opacity = 0.7;
                polyline.Locations = new LocationCollection();

                foreach (var besuch in items)
                {
                    polyline.Locations.Add(new Location(besuch.Arzt.Adresse.Latitude, besuch.Arzt.Adresse.Longitude));
                }

                BingMap.Children.Add(polyline);
            }
        }

        private void CenterMap()
        {
            if (items.Count > 2)
            {
                double minLat = items[0].Arzt.Adresse.Latitude;
                double minLng = items[0].Arzt.Adresse.Longitude;

                double maxLat = items[0].Arzt.Adresse.Latitude;
                double maxLng = items[0].Arzt.Adresse.Longitude;

                foreach (Core.Besuch besuch in items)
                {
                    if (besuch.Arzt.Adresse.Latitude < minLat) minLat = besuch.Arzt.Adresse.Latitude;
                    else if (besuch.Arzt.Adresse.Latitude > maxLat) maxLat = besuch.Arzt.Adresse.Latitude;

                    if (besuch.Arzt.Adresse.Longitude < minLng) minLng = besuch.Arzt.Adresse.Longitude;
                    else if (besuch.Arzt.Adresse.Longitude > maxLng) maxLng = besuch.Arzt.Adresse.Longitude;
                }

                double centerLat = (minLat + maxLat) / 2;
                double centerLng = (minLng + maxLng) / 2;

                BingMap.Center = new Location(centerLat, centerLng);
            }

        }

        private void RemoveBesuch(object sender, MouseButtonEventArgs e)
        {
            UserControls.ImageButton button = sender as UserControls.ImageButton;
            Core.Besuch item = button.DataContext as Core.Besuch;

            items.Remove(item);

            UpdateIDs();
            

            GesamtEntfernungAusrechnen();
            GeschätzteDauerBerechnen();

            docList.UpdateLayout();
        }

        #region TimeModal

        private void HideTimeModal(object sender, RoutedEventArgs e)
        {
            Grid_Time_Modal.Visibility = Visibility.Hidden;
        }

        private void OpenTimeModal(object sender, MouseButtonEventArgs e)
        {
            UserControls.ImageButton button = sender as UserControls.ImageButton;
            Core.Besuch item = button.DataContext as Core.Besuch;

            selectedId = item.ID;
            tb_Time.Text = item.geschätzteDauer.TotalMinutes.ToString();

            Grid_Time_Modal.Visibility = Visibility.Visible;
            tb_Time.Focus();
        }

        private void ChangeTime(object sender, RoutedEventArgs e)
        {
            try
            {
                items[selectedId].geschätzteDauer = new TimeSpan(0, Convert.ToInt32(tb_Time.Text), 0);
                GeschätzteDauerBerechnen();
                Grid_Time_Modal.Visibility = Visibility.Hidden;
            }
            catch
            {
                MessageBox.Show("Überprüfen Sie die Eingabe. Geben Sie bitte die Dauer in Minuten an.", "Fehlerhafte Eingabe", MessageBoxButton.OK, MessageBoxImage.Warning);
            }           
        }

        private void GeschätzteDauerBerechnen()
        {
            TimeSpan dauer = new TimeSpan();

            foreach(Core.Besuch besuch in items)
            {
                dauer += besuch.geschätzteDauer;
            }


            if(dauer.Hours>=8) MessageBox.Show("Die geschätzte Besuchsdauer überschreitet 8 Studen!", "Zeitsoll erfüllt.", MessageBoxButton.OK, MessageBoxImage.Warning);

            lbl_geschätzteDauer.Content = "Geschätzte Dauer: " + dauer.Hours + "Std. " + dauer.Minutes + "Min.";
        }



        #endregion
    }
}
