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

using GMap.NET;
using GMap.NET.MapProviders;

namespace Pharma_Man.Pages
{
    /// <summary>
    /// Interaktionslogik für Tagesplan.xaml
    /// </summary>
    public partial class Tagesplan : Page
    {
        public Tagesplan(DateTime date)
        {
            InitializeComponent();

            lbl_pageCaption.Content = "Tagesplan";


            lbl_date.Content = "für den " + date.ToShortDateString() + ".";

            // Ärzteliste
            /*
            ListBoxItem itm = new ListBoxItem();
            itm.Content = "some text";
            docList.Items.Add(itm);
            */


            List<BesuchItem> items = new List<BesuchItem>();
            items.Add(new BesuchItem( new Core.Besuch( Data.Datenbank.Instance.GetBesuchID(), Data.Datenbank.Instance.GetArzt(1) )));
            items.Add(new BesuchItem( new Core.Besuch( Data.Datenbank.Instance.GetBesuchID(), Data.Datenbank.Instance.GetArzt(2) )));
            items.Add(new BesuchItem( new Core.Besuch( Data.Datenbank.Instance.GetBesuchID(), Data.Datenbank.Instance.GetArzt(3) )));

            docList.ItemsSource = items;

            //Load Map
            // config map
            MainMap.MapProvider = GMapProviders.BingHybridMap;
            MainMap.Zoom = 15;
            MainMap.Position = new PointLatLng(49.351491, 9.143293);

        }
    }

    //Wrapper um Items sperren ('locken') zu können
    public class BesuchItem {
        public bool locked = false;

        private Core.Besuch besuch;
        public Core.Besuch Besuch { get { return besuch; } }

        public BesuchItem(Core.Besuch besuch) {
            this.besuch = besuch;
        }
    }
}
