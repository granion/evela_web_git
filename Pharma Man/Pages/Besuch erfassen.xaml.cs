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

namespace Pharma_Man.Pages {

    public partial class Besuch_erfassen : Page {
        public Besuch_erfassen(Core.Besuch besuch) {
            InitializeComponent();


            tb_Zeit.Text = besuch.Datum.ToShortDateString();
        }






    }
}
