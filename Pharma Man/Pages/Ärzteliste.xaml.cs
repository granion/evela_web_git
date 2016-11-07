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

using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using Pharma_Man.controls;
using System.Diagnostics;
using Pharma_Man.Core;
using Pharma_Man.Pages;
using System.Windows.Ink;

namespace Pharma_Man.Pages
{
    public partial class Ärzteliste : Page
    {

        //Ärzteliste
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;

        private ObservableCollection<Core.Arzt> ärzte = new ObservableCollection<Core.Arzt>();

        private Core.Arzt selectedArzt;

        public Ärzteliste()
        {
            InitializeComponent();

            UpdateÄrzteliste();

            // Init Ärzteliste
            ärzteListe.ItemsSource = ärzte;

            // Sortierfunktion Ärzteliste
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ärzteListe.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("Priorität", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("Ort", ListSortDirection.Ascending));

        }

        private void UpdateÄrzteliste()
        {
            ärzte.Clear();
            if (Data.Datenbank.Instance.ärzte.Values.Count > 0)
            {
                foreach (var item in Data.Datenbank.Instance.ärzte.Values)
                {
                    ärzte.Add(item);
                }
                ärzteListe.UpdateLayout();
            }
        }

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

        private void ArztAusgewählt(object sender, RoutedEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            selectedArzt = (Core.Arzt)item.DataContext;

            tb_Prio.Text = selectedArzt.Priorität.ToString();
            lbl_Arzt_Name.Content = selectedArzt.Name;
          
            Grid_Modal.Visibility = Visibility.Visible;

        }

        private void ChangePrio(object sender, RoutedEventArgs e)
        {
            try
            {
                Data.Datenbank.Instance.UpdateArztPrio(selectedArzt.ID, Convert.ToInt32(tb_Prio.Text));
                Grid_Modal.Visibility = Visibility.Hidden;
                UpdateÄrzteliste();
            }
            catch
            {
                MessageBox.Show("Überprüfen Sie die Eingabe. Geben Sie bitte die Dauer in Minuten an.", "Fehlerhafte Eingabe", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
