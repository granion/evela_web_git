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
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Besuchsplan bp;
        BesuchsplanErfassen bpe;

        public MainWindow()
        {
            InitializeComponent();

            bp = new Besuchsplan();
            bpe = new BesuchsplanErfassen();

            frame.NavigationService.Navigate(bp);
        }

        private void btn_next_Click(object sender, RoutedEventArgs e)
        {
            if (frame.NavigationService.CanGoForward)
            {
                frame.NavigationService.GoForward();
            }
        }

        private void btn_prev_Click(object sender, RoutedEventArgs e)
        {
            if (frame.NavigationService.CanGoBack)
            {
                frame.NavigationService.GoBack();
            }
        }

        private void btn_Besuchsplan_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(bp);
        }

        private void btn_BesuchErfassen_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(bpe);
        }
    }
}
