using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PDFTestSolution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //neues Dokument
            PdfDocument doc = new PdfDocument();

            //leere Page
            PdfPage page = doc.AddPage();

            //XGraphis zum zeichnen
            XGraphics gfx = XGraphics.FromPdfPage(page);

            //Font erstellen
            XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);

            //Text zeichnen
            gfx.DrawString("Das ist ein offizieller PharaMan Test.", font, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height),
                XStringFormats.Center);

            //File speichern
            const string filename = "pdf_test1.pdf";
            doc.Save(filename);

            //Viewer starten
            Process.Start(filename);
        }
    }
}
