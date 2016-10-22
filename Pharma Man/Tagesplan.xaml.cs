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
    /// Interaktionslogik für Tagesplan.xaml
    /// </summary>
    public partial class Tagesplan : Page
    {
        public Tagesplan(DateTime date)
        {
            InitializeComponent();
            lbl_date.Content = "für den " + date.ToShortDateString() + ".";


            ListBoxItem itm = new ListBoxItem();
            itm.Content = "some text";

            docList.Items.Add(itm);

        }
    }
}
