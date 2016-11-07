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

namespace Pharma_Man.Pages
{
    public partial class Beleg : Page
    {
        Core.Besuch besuch;

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

            // SPEICHERN DER PDF
            // GEGEBENENFALLS EIN BESSERER FILENAME?
            document.Save(AppDomain.CurrentDomain.BaseDirectory + @"Belege/" + "unsigned.pdf");

            // ANZEIGEN DER ERSTELLTEN PDF 
            pdf_web.Source = new Uri(@AppDomain.CurrentDomain.BaseDirectory + @"Belege/" + "unsigned.pdf");


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

                    // HIER SOLL DIE PDF MIT UNTERSCHRIFT AM ORT "pdfFileName" gespeichert werden! <<<<<<<<<<<<
                    document.Save(pdfFileName);

                    MessageBox.Show("Die PDF wurde erfolgreich gespeichert", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);

                    // PDF ÖFFNET SICH NACH BUTTON "ABSCHLIEßEN"
                    Process.Start(pdfFileName);
                }

               
                // Setze Status auf erfasst
                this.besuch.isErfasst = true;

                // Speichere Besuch/Tagesplan
                var tagesplan = Data.Datenbank.Instance.GetTagesplan(this.besuch.Datum);
                tagesplan.UpdateBesuch(this.besuch.ID, this.besuch);
                Data.Datenbank.Instance.SaveTagesplan(tagesplan);
            }
        }
    }
}
