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

        public bool isPDFcreated = false; //wird benutzt um die PDF im anderen Fenster anzuzeigen 

        //PDF Daten
        string datum;
        string von;
        string bis;
        string thema;
        string sonderbesuch;
        string notiz;

        //PDF-File Name (für Prototypen n statischer name)
        //Sollte immer im bin/Debug Folder vom Projekt abgelegt werden
        const string filename = "pharmaMan_beleg.pdf";
        const string filename_signed = "pharmaMan_beleg_sig.pdf";

        public Besuch_erfassen(Core.Besuch besuch)
        {
            InitializeComponent();

            this.besuch = besuch;
            tb_Zeit.Text = besuch.Datum.ToShortDateString();
        }

        //Zum Switchen nach dem Button Besuch Abschließen zur PDF view ansicht (mit unterschrift etc.)
        public void convertVisibility()
        {
            lbl_Zeit.Visibility = Visibility.Hidden;
            tb_Zeit.Visibility = Visibility.Hidden;
            tb_Von.Visibility = Visibility.Hidden;
            tb_Bis.Visibility = Visibility.Hidden;

            lbl_Thema.Visibility = Visibility.Hidden;
            tb_Thema.Visibility = Visibility.Hidden;

            lbl_SonderbesuchNotiz.Visibility = Visibility.Hidden;
            tb_SonderbesuchNotiz.Visibility = Visibility.Hidden;

            lbl_Notizen.Visibility = Visibility.Hidden;
            rtb_Notizen.Visibility = Visibility.Hidden;

            btn_Speichern.Visibility = Visibility.Hidden;

            lbl_Ärztemuster.Visibility = Visibility.Hidden;
            lb_Ärztemuster.Visibility = Visibility.Hidden;


            pdf_web.Visibility = Visibility.Visible;
            ink.Visibility = Visibility.Visible;
            btn_BesuchAbschließen.Visibility = Visibility.Visible;

        }

        private void btn_BesuchSpeichern(object sender, RoutedEventArgs e)
        {
            // Status setzen
            this.besuch.isErfasst = false;

            // Felder Prüfen


            // Werte aus Feldern in Besuch speichern


            // Tagesplan abrufen
            Core.Tagesplan tagesplan = Data.Datenbank.Instance.GetTagesplan(besuch.Datum);

            // Wenn kein Tagesplan vorhanden, lege neuen an und füge den Besuch hinzu
            if (tagesplan == null) tagesplan = new Core.Tagesplan(besuch.Datum);

            // Besuch in Tagesplan speichern
            tagesplan.AddBesuch(this.besuch);

            // Tagesplan speichern
            Data.Datenbank.Instance.SaveTagesplan(tagesplan);

            //PDF Daten ziehen
            datum = tb_Zeit.Text;
            von = tb_Von.Text;
            bis = tb_Bis.Text;
            thema = tb_Thema.Text;
            sonderbesuch = tb_SonderbesuchNotiz.Text;

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
                pdf_web.Source = new Uri(@"C:\Users\Vladi\Source\Repos\evela_web_git4\Pharma Man\bin\Debug\pharmaMan_beleg.pdf");
            }
        }

        private void BesuchAbschließen(object sender, RoutedEventArgs e)
        {
            // Status setzen
            this.besuch.isErfasst = false;

            // Felder Prüfen


            // Werte aus Feldern in Besuch speichern

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



        }
    }
}


