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

namespace Pharma_Man.Pages
{
    /// <summary>
    /// Interaktionslogik für Besuchsplan.xaml
    /// </summary>
    public partial class Besuchsplan : Page
    {
        public Besuchsplan()
        {
            InitializeComponent();


        }

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            var calendar = sender as Calendar;
            if (calendar.SelectedDate.HasValue)
            {
                DateTime date = calendar.SelectedDate.Value;

                // Rufe Tagesplan von Datenbank
                Core.Tagesplan tagesplan = Data.Datenbank.Instance.GetTagesplan(date);

                // Tagesplan empfangen?
                if (tagesplan == null) tagesplan = new Core.Tagesplan(date);

                Tagesplanung tp = new Tagesplanung(tagesplan);
                this.NavigationService.Navigate(tp);
            }
            */
            
        }

        private void calendar_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (!(e.OriginalSource is FrameworkElement &&
            (e.OriginalSource as FrameworkElement).DataContext is DateTime))
            {
                base.OnPreviewMouseLeftButtonDown(e);
                return;
            }


            DateTime date = (DateTime)(e.OriginalSource as FrameworkElement).DataContext;

            // Rufe Tagesplan von Datenbank
            Core.Tagesplan tagesplan = Data.Datenbank.Instance.GetTagesplan(date);

            // Tagesplan empfangen?
            if (tagesplan == null) tagesplan = new Core.Tagesplan(date);

            Tagesplanung tp = new Tagesplanung(tagesplan);
            this.NavigationService.Navigate(tp);

        }

        
    }
}
