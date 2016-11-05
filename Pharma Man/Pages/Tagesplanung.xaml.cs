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


        public Tagesplanung(Core.Tagesplan tagesplan)
        {
                       
            InitializeComponent();

            lbl_pageCaption.Content = "Tagesplan";
            lbl_date.Content = "für den " + tagesplan.Date.ToShortDateString() + ".";

            this.tagesplan = tagesplan;

            // Lade Besuche, falls vorhanden
            if (this.tagesplan.Besuche.Count>0)
            {
                //var oc = new ObservableCollection<Core.Besuch>();
                tagesplan.Besuche.ForEach(x => items.Add(x));
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


        }

        #region Button-Events


        private void MoveItemUp(object sender, RoutedEventArgs e)
        {
            var selectedIndex = this.docList.SelectedIndex;

            if (selectedIndex > 0)
            {
                var itemToMoveUp = this.items[selectedIndex];

                for (int i = 1; selectedIndex - i >= 0; i++)
                {
                    if (!items[selectedIndex -i].isLocked)
                    {
                        var itemToSwap = this.items[selectedIndex - i];

                        this.items.Remove(itemToMoveUp);
                        this.items.Remove(itemToSwap);
                     
                        this.items.Insert(selectedIndex-i, itemToMoveUp);
                        this.items.Insert(selectedIndex, itemToSwap);

                        this.docList.SelectedIndex = selectedIndex-i;
                        break;
                    }
                }
            }
            docList.UpdateLayout();
        }

        private void MoveItemDown(object sender, RoutedEventArgs e)
        {
            var selectedIndex = this.docList.SelectedIndex;
            var itemToMoveDown = this.items[selectedIndex];

            if (selectedIndex + 1 < this.items.Count)
            {
                for(int i = 1; selectedIndex + i < this.items.Count; i++)
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
            docList.UpdateLayout();
        }


        private void LockListItem(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Core.Besuch item = button.DataContext as Core.Besuch;
            item.isLocked = !item.isLocked;

        }

        private void GeschätzteDauerÄndern(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Core.Besuch item = button.DataContext as Core.Besuch;

            //Rückgabewert übertragen
            item.geschätzteDauer = new TimeSpan(0,30,0);
        }

        private void AddBesuch(object sender, RoutedEventArgs e)
        {
           // items.Add(new Core.Besuch(Data.Datenbank.Instance.GetArzt(4)));

            foreach (var item in ärzteListe.SelectedItems)
            {
                var arzt = item as Core.Arzt;
                items.Add(new Core.Besuch(arzt));
            }


            docList.UpdateLayout();
            Grid_Modal.Visibility = Visibility.Hidden;
        }

        private void RemoveBesuch(object sender, RoutedEventArgs e)
        {
            items.Remove((Core.Besuch)docList.SelectedItem);
            docList.UpdateLayout();
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



        #endregion


    }
}
