using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using Pharma_Man.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma_Man.controls
{
    public class PDFCreator
    {

        public static void DrawLogo(XGraphics gfx, int number)
        {

            XImage image = XImage.FromFile("C:/Users/Vladi/Source/Repos/evela_web_git4/Pharma Man/logo placeholder.png");

            double width = 100;
            double height = 100;

            gfx.DrawImage(image, 50, 50, width, height);
            gfx.DrawString("Beleg", new XFont("Arial", 80, XFontStyle.Regular), XBrushes.Black, 200, 125);

        }


        public static void DrawZeit(PdfPage page, XGraphics gfx, string tag, string von, string bis)
        {

            XRect rect = new XRect(new XPoint(), gfx.PageSize);
            rect.Inflate(100, -15);

            //Überschriften Font
            XFont font = new XFont("Arial", 24, XFontStyle.Bold);

            //Inhalt Font
            XFont font2 = new XFont("Arial", 16, XFontStyle.Bold);


            gfx.DrawString("Zeiterfassung", font, XBrushes.Red, 50, 200);
            gfx.DrawString(tag, font2, XBrushes.Black, 50, 230);
            gfx.DrawString("von " + von, font2, XBrushes.Black, 200, 230);
            gfx.DrawString(" bis " + bis, font2, XBrushes.Black, 350, 230);
        }

        public static void DrawThema(PdfPage page, XGraphics gfx, string inhalt)
        {
            XRect rect = new XRect(new XPoint(), gfx.PageSize);
            rect.Inflate(100, -15);

            //Überschriften Font
            XFont font = new XFont("Arial", 24, XFontStyle.Bold);

            //Inhalt Font
            XFont font2 = new XFont("Arial", 16, XFontStyle.Bold);

            //Max. 90char String zur Themenbeschreibung
            if (inhalt.Length > 90)
            {
                throw new ArgumentOutOfRangeException("Die Themenbeschreibung überschreitet 90 Zeichen.");
            }
            else
            {
                gfx.DrawString("Thema", font, XBrushes.Red, 50, 270);

                int zeile1 = 44;
                int zeile2 = inhalt.Length - zeile1;

                if (inhalt.Length <= zeile1)
                {
                    gfx.DrawString(inhalt.Substring(0, inhalt.Length), font2, XBrushes.Black, 50, 300);
                }

                if (inhalt.Length > 45 && inhalt.Length < 90)
                {
                    gfx.DrawString(inhalt.Substring(0, zeile1), font2, XBrushes.Black, 50, 300);
                    gfx.DrawString(inhalt.Substring(45, zeile2 - 1), font2, XBrushes.Black, 50, 330);
                }
            }

        }

        public static void DrawArzt(PdfPage page, XGraphics gfx, Arzt arzt)
        {
            XRect rect = new XRect(new XPoint(), gfx.PageSize);
            rect.Inflate(100, -15);

            //Überschriften Font
            XFont font = new XFont("Arial", 24, XFontStyle.Bold);

            //Inhalt Font
            XFont font2 = new XFont("Arial", 16, XFontStyle.Bold);

            gfx.DrawString("Besuchter Arzt", font, XBrushes.Red, 50, 370);
            gfx.DrawString(arzt.Name, font2, XBrushes.Black, 50, 400);
        }

        //Dummy Implementierung für ÄRztemuster
        public static void DrawMuster(PdfPage page, XGraphics gfx, string[] muster)
        {
            XRect rect = new XRect(new XPoint(), gfx.PageSize);
            rect.Inflate(100, -15);

            //Überschriften Font
            XFont font = new XFont("Arial", 24, XFontStyle.Bold);

            //Inhalt Font
            XFont font2 = new XFont("Arial", 16, XFontStyle.Bold);

            //dummy array später ersetzten
            muster[0] = "Cannabis Öl";
            muster[1] = "Marihuanna Medical extra kush";
            muster[2] = "Magische Bohnen";

            gfx.DrawString("Ärztemuster", font, XBrushes.Red, 50, 440);
            int abstand = 30;
            for (int i = 0; i < muster.Length; i++)
            {
                gfx.DrawString("5x " + muster[i], font2, XBrushes.Black, 50, 440 + abstand);
                abstand = abstand + 30;

            }
        }

        public static void DrawSignature(XGraphics gfx, int number)
        {

            XImage image = XImage.FromFile(@"C:\Users\Vladi\Source\Repos\evela_web_git3\Pharma Man\bin\Debug\signatur.png");

            double width = 250;
            double height = 100;

            gfx.DrawImage(image, 50, 550, width, height);

        }

    }


}

