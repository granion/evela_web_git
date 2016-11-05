using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Pharma_Man.Pages
{
    /// <summary>
    /// Interaktionslogik für BesuchsplanErfassen.xaml
    /// </summary>
    public partial class BesuchsplanErfassen : Page
    {
        private ObservableCollection<Core.Besuch> items = new ObservableCollection<Core.Besuch>();

        DateTime selectedDate;


        public BesuchsplanErfassen()
        {
            InitializeComponent();

            besuchList.ItemsSource = items;
        }


        private void HideModal(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Hidden;
        }

        // Datum geklickt
        private void calendar_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!(e.OriginalSource is FrameworkElement &&
            (e.OriginalSource as FrameworkElement).DataContext is DateTime))
            {
                base.OnPreviewMouseLeftButtonDown(e);
                return;
            }


            selectedDate = (DateTime)(e.OriginalSource as FrameworkElement).DataContext;

            Grid_Modal_TopRow_lbl_Date.Content = "für den" + selectedDate.ToShortDateString();

            // Rufe Tagesplan von ausgewähltem Datum von der Datenbank ab
            Core.Tagesplan tagesplan = Data.Datenbank.Instance.GetTagesplan(selectedDate);

            if(tagesplan != null)
            {
                items.Clear();
                tagesplan.Besuche.ForEach(x => items.Add(x));
            }

          

            // Öffne Tagesplan Modal

            Grid_Modal.Visibility = Visibility.Visible;
            besuchList.Focus();

        }

        private void BesuchAusgewählt(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = sender as ListBoxItem;
            Core.Besuch besuch = (Core.Besuch)item.DataContext;


            


            Besuch_erfassen berf = new Besuch_erfassen(besuch);
            this.NavigationService.Navigate(berf);
        }



        private void NeuerBesuch(object sender, RoutedEventArgs e)
        {
            Core.Besuch besuch = new Core.Besuch(selectedDate);
            Besuch_erfassen berf = new Besuch_erfassen(besuch);
            this.NavigationService.Navigate(berf);
        }

    }
}
