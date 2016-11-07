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

    public partial class Besuch_erfassen : Page
    {

        Core.Besuch besuch;

        public Besuch_erfassen(Core.Besuch besuch)
        {
            InitializeComponent();

            this.besuch = besuch;
            tb_Zeit.Text = besuch.Datum.ToShortDateString();
            tb_Von.Text = besuch.TerminStart.ToShortTimeString();
            tb_Bis.Text = besuch.TerminEnde.ToShortTimeString();

            if(besuch.Arzt != null) tb_Arzt.Text = besuch.Arzt.Name;

            tb_Thema.Text = besuch.Thema;

            tb_Notizen.Text = besuch.Notiz;
        }

        private void BesuchSpeichern()
        {
            // Status setzen
            this.besuch.isErfasst = false;

            // Breche ab, falls eines der Felder fehlerhaft ist
            if (!CheckFelder()) return;

            // Speichere Daten in Besuch
            ParseData();

            // Tagesplan abrufen
            Core.Tagesplan tagesplan = Data.Datenbank.Instance.GetTagesplan(besuch.Datum);

            // Wenn kein Tagesplan vorhanden, lege neuen an und füge den Besuch hinzu
            if (tagesplan == null)
            {
                tagesplan = new Core.Tagesplan(besuch.Datum);

                // Besuch in Tagesplan speichern
                tagesplan.AddBesuch(this.besuch);
            }
            else
            {
                tagesplan.UpdateBesuch(this.besuch.ID, this.besuch);
            }

            // Tagesplan speichern
            Data.Datenbank.Instance.SaveTagesplan(tagesplan);

        }

        // Prüfe Felder
        private bool CheckFelder()
        {
            try
            {
                // Zeit
                // Von
                var von = DateTime.Parse(tb_Von.Text);
                // Bis
                var bis = DateTime.Parse(tb_Bis.Text);
            }
            catch
            {
                MessageBox.Show("Bitte Eingaben prüfen!", "Fehlerhafte Eingabe.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        // Speichere Daten im Besuch
        private void ParseData()
        {
            this.besuch.UpdateTermin(DateTime.Parse(tb_Von.Text), DateTime.Parse(tb_Bis.Text));
            
            this.besuch.Thema = tb_Thema.Text;
            this.besuch.Notiz = tb_Notizen.Text;

            // Haben keine Produkte :/
            this.besuch.UpdateProdukte(null);
        }

        private void btn_BesuchSpeichern(object sender, RoutedEventArgs e)
        {
            // Speichere Besuch
            BesuchSpeichern();



            // VLADI, bitte in die "Beleg"-Seite auslagern!
            #region old
            /*

            //PDF Daten ziehen
            datum = tb_Zeit.Text;
            von = tb_Von.Text;
            bis = tb_Bis.Text;
            thema = tb_Thema.Text;
            //sonderbesuch = tb_SonderbesuchNotiz.Text;

            //erste PDF (ohne Unterschrift)
            PdfDocument document = new PdfDocument();

            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            PDFCreator.DrawLogo(gfx, 1);
            PDFCreator.DrawZeit(page, gfx, datum, von, bis);
            PDFCreator.DrawThema(page, gfx, thema);

            //Wieso funktoniert new Arzt hier nicht mehr?
            // PDFCreator.DrawArzt(page, gfx, new Arzt(1, "Dr. Klaus Testdoktor", new Adresse(97882, "Straße Test", 3, "Testhausen", 0.1, 0.1), 1); //Arzt-Testobjekt für die PDF
            PDFCreator.DrawMuster(page, gfx, new string[3]);

            document.Save(filename);
            isPDFcreated = true;

            if (isPDFcreated)
            {
                convertVisibility();
                pdf_web.Source = new Uri(@"pharmaMan_beleg.pdf");
            }

            */
            #endregion
        }

        private void BesuchAbschließen(object sender, RoutedEventArgs e)
        {
            // Speichere Besuch
            BesuchSpeichern();

            Pages.Beleg beleg = new Beleg(this.besuch);

            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(beleg);


            // VLADI, bitte in die "Beleg"-Seite auslagern!
            #region old
            /*

            // PDF aus Besuch erzeugen
            Rect bounds = VisualTreeHelper.GetDescendantBounds(ink);
            double dpi = 96d;
            //Checken Ob unterschrieben wurde
            if (ink.Strokes.Count == 0)
            {
                MessageBox.Show("Das Unterschriftenfeld ist leer", "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //wenn ja, dann:
            else
            {
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

                    //neue PDF
                    PdfDocument document = new PdfDocument();

                    PdfPage page = document.AddPage();

                    XGraphics gfx = XGraphics.FromPdfPage(page);

                    PDFCreator.DrawLogo(gfx, 1);
                    PDFCreator.DrawZeit(page, gfx, datum, von, bis);
                    PDFCreator.DrawThema(page, gfx, thema);
                    //PDFCreator.DrawArzt(page, gfx, new Arzt(1, "Dr. Klaus Testdoktor", new Adresse(9788, "Straße Test", 3, "Testhausen"), 1)); //Arzt-Testobjekt für die PDF
                    PDFCreator.DrawMuster(page, gfx, new string[3]);
                    PDFCreator.DrawSignature(gfx, 2);
                    document.Save(filename_signed);

                    MessageBox.Show("Die PDF wurde erfolgreich gespeichert", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
                    Process.Start(filename_signed);
                }
            }

    */
            #endregion

        }
    }
}


