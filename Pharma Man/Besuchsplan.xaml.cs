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

namespace Pharma_Man
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
            // ... Get reference.
            var calendar = sender as Calendar;

            // ... See if a date is selected.
            if (calendar.SelectedDate.HasValue)
            {
                // ... Display SelectedDate in Title.
                DateTime date = calendar.SelectedDate.Value;

                Tagesplan tp = new Tagesplan(date);
                this.NavigationService.Navigate(tp);
            }

            
        }
    }
}
