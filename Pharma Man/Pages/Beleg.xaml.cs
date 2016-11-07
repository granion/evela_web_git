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
    public partial class Beleg : Page
    {
        Core.Besuch besuch;

        string tmpPdfPath = AppDomain.CurrentDomain.BaseDirectory + @"Belege/" + "unsigned.pdf";

        public Beleg(Core.Besuch besuch)
        {
            InitializeComponent();

            // Der Besuch von der "besuch erfassen" Seite, beinhaltet aklle notwendigen Daten für die PDF
            this.besuch = besuch;

            // HIER SOLL DIE PDF >OHNE< UNTERSCHRIFT ERZEUGT WERDEN <<<<<<<<<<<
            PdfDocument document = new PdfDocument();

            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            PDFCreator.DrawLogo(gfx, 1);
            PDFCreator.DrawZeit(page, gfx, besuch.TerminStart, besuch.TerminEnde);
            PDFCreator.DrawThema(page, gfx, besuch.Thema);
            PDFCreator.DrawArzt(page, gfx, besuch.Arzt);


            if (File.Exists(tmpPdfPath)) File.Delete(tmpPdfPath);

            // SPEICHERN DER PDF
            // GEGEBENENFALLS EIN BESSERER FILENAME?
            document.Save(tmpPdfPath);
            document.Close();
            page.Close();
            gfx.Dispose();

            // ANZEIGEN DER ERSTELLTEN PDF 
            pdf_web.Source = new Uri(tmpPdfPath);
            


        }

        private void BelegBestätigen(object sender, RoutedEventArgs e)
        {
            // CHECKEN, OB UNTERSCHRIEBEN WURDE
            Rect bounds = VisualTreeHelper.GetDescendantBounds(ink);
            double dpi = 96d;
            if (ink.Strokes.Count == 0)
            {
                MessageBox.Show("Das Unterschriftenfeld ist leer", "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            else {
                // HIER WIR DIE PDF >>MIT<< UNTERSCHRIFT ERZEUGT! <<<<<<<<<<<<

                // Speicherort der Belege
                string savePath = AppDomain.CurrentDomain.BaseDirectory + @"Belege/";

                // BelegID wird immer um 1 inkrementiert
                int belegID = Data.Datenbank.Instance.GetBelegID();

                // Das soll der Name der PDF sein
                string pdfFileName = savePath + belegID.ToString() + ".pdf";

                // Untetschriftsfeld färben
                ink.Background = new SolidColorBrush(Colors.White);
                
                foreach (Stroke s in ink.Strokes)
                {
                    s.DrawingAttributes.Color = Colors.Black;
                }

                ink.UpdateLayout();


                RenderTargetBitmap rtb = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, dpi, dpi, System.Windows.Media.PixelFormats.Default);
                DrawingVisual dv = new DrawingVisual();
                using (DrawingContext dc = dv.RenderOpen())
                {
                    VisualBrush vb = new VisualBrush(ink);
                    dc.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
                }
                rtb.Render(dv);

                BitmapEncoder pngEncoder = new PngBitmapEncoder();
                pngEncoder.Frames.Add(BitmapFrame.Create(rtb));
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {

                    pngEncoder.Save(ms);
                    System.IO.File.WriteAllBytes("signatur.png", ms.ToArray());

                    PdfDocument document = new PdfDocument();

                    PdfPage page = document.AddPage();

                    XGraphics gfx = XGraphics.FromPdfPage(page);

                    PDFCreator.DrawLogo(gfx, 1);
                    PDFCreator.DrawZeit(page, gfx, besuch.TerminStart, besuch.TerminEnde);
                    PDFCreator.DrawThema(page, gfx, besuch.Thema);
                    PDFCreator.DrawArzt(page, gfx, besuch.Arzt);
                    PDFCreator.DrawSignature(gfx, 2);

                    // HIER SOLL DIE PDF MIT UNTERSCHRIFT AM ORT "pdfFileName" gespeichert werden! 
                    if (File.Exists(pdfFileName)) File.Delete(pdfFileName);
                    document.Save(pdfFileName);

                    // Setze Status auf erfasst
                    this.besuch.isErfasst = true;

                    // Speichere Besuch/Tagesplan
                    var tagesplan = Data.Datenbank.Instance.GetTagesplan(this.besuch.Datum);
                    tagesplan.UpdateBesuch(this.besuch.ID, this.besuch);
                    Data.Datenbank.Instance.SaveTagesplan(tagesplan);

                    pdf_web.Navigate(new Uri("about:blank"));


                    //Gebe Ressourcen frei
                    page.Close();
                    document.Close();
                    document.Dispose();
                    gfx.Dispose();

                    


                    // Untetschriftsfeld färben
                    ink.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F2171717"));

                    foreach (Stroke s in ink.Strokes)
                    {
                        s.DrawingAttributes.Color = Colors.White;
                    }

                    ink.UpdateLayout();

                    MessageBox.Show("Die PDF wurde erfolgreich gespeichert", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);

                    // PDF ÖFFNET SICH NACH BUTTON "ABSCHLIEßEN"
                    Process.Start(pdfFileName);
                }

            }
        }
    }
}
