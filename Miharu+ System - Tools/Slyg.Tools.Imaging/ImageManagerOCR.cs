using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Slyg.Tools.Imaging.OCR.Models;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Drawing;
using System.IO;

namespace Slyg.Tools.Imaging.OCR.Models
{
    public class ImageSize
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class ImageTextExtractionOCR
    {
        public ImageSize ImageSize { get; set; }
        public List<DetectedWordBounds> Words { get; set; }

        
        public ImageTextExtractionOCR()
        {
            Words = new List<DetectedWordBounds>();
        }
    }

    public class ImageBytesData
    {
        public byte[] ImageBytes { get; set; }
    }

    public class DetectedWordBounds
    {
        public string Text { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double FontSize { get; set; }
    }

}

namespace Slyg.Tools.Imaging.OCR
{

    public class ImageManagerOCR
    {

        public static void SavePDF(List<string> nInputFileNames, List<ImageTextExtractionOCR> nListWordsExtractionOCR, string nOutputFileName)
        {

            if (nInputFileNames == null || nListWordsExtractionOCR == null || nInputFileNames.Count != nListWordsExtractionOCR.Count)
            {
                throw new ArgumentException("Las listas de rutas de imágenes y OCR deben ser válidas y tener el mismo tamaño.");
            }

            // Crear un nuevo documento PDF
            var pdfDocument = CreatePdfDocument();

            for (int i = 0; i < nInputFileNames.Count; i++)
            {
                string imagePath = nInputFileNames[i];
                ImageTextExtractionOCR ocrData = nListWordsExtractionOCR[i];

                using (Bitmap image = new Bitmap(imagePath))
                {
                    double scaleWidth, scaleHeight;
                    double dpi = image.HorizontalResolution; // O image.VerticalResolution

                    var imageSize = new XSize(ConvertToPoints(ocrData.ImageSize.Width, dpi),
                                               ConvertToPoints(ocrData.ImageSize.Height, dpi));
                    
                    var pageDocument = CreatePagePdfDoc(imageSize);
                    pdfDocument.AddPage(pageDocument);
                    using (var gfx = XGraphics.FromPdfPage(pdfDocument.Pages[i]))
                    {
                        scaleWidth = CalculateScale(imageSize.Width, image.Width);
                        scaleHeight = CalculateScale(imageSize.Height, image.Height);

                        ProcessText(ocrData, gfx, scaleWidth, scaleHeight);
                        AddImageToPdf(gfx, image, imageSize);
                    }
                    
                }

            }

            // Guardar el documento PDF
            pdfDocument.Save(nOutputFileName);
            pdfDocument.Close();
        }

        /// <summary>
        /// Procesa y dibuja palabras en un documento PDF.
        /// </summary>
        /// <param name="pageProcessor">El procesador de páginas Tesseract.</param>
        /// <param name="gfx">El contexto gráfico para el PDF.</param>
        /// <param name="scaleWidth">Factor de escala para el ancho.</param>
        /// <param name="scaleHeight">Factor de escala para la altura.</param>
        public static void ProcessText(ImageTextExtractionOCR ocrData, XGraphics gfx, double scaleWidth, double scaleHeight)
        {
            var countdata = 0;
            // Dibujar las palabras en la página usando los datos OCR
            foreach (var word in ocrData.Words)
            {
                if (!string.IsNullOrEmpty(word.Text) && !word.Text.Contains("|"))
                {
                    if(countdata == 0)
                    {
                        DrawTextOnPdf(gfx, "PalabraPrueba", new XPoint(0, 200), word.Height);
                        DrawTextOnPdf(gfx, "PalabraPrueba", new XPoint(0, 1000), word.Height);
                        countdata++;
                    }

                    double x1 = word.X * scaleWidth;
                    double y1 = (word.Y * scaleHeight) + (word.Height * scaleHeight);

                    DrawTextOnPdf(gfx, word.Text, new XPoint(x1, y1), word.Height);
                }
            }
        }

        /// <summary>
        /// Agrega una imagen desde un archivo a un documento PDF en el contexto gráfico especificado.
        /// </summary>
        /// <param name="gfx">El contexto gráfico donde se agregará la imagen al PDF.</param>
        /// <param name="imagePath">La ruta del archivo de imagen a agregar al PDF.</param>
        /// <param name="imageSize">El tamaño de la imagen a agregar en el formato XSize (ancho y alto).</param>
        public static void AddImageToPdf(XGraphics gfx, Bitmap image, XSize imageSize)
        {
            var adjustedImage = ConvertBitmapToXImage(image);                      // Convertir el bitmap ajustado a un objeto XImage.                
            gfx.DrawImage(adjustedImage, 0, 0, imageSize.Width, imageSize.Height);  // Dibujar la imagen en el PDF.
        }

        /// <summary>
        /// Convierte un objeto Bitmap a un objeto XImage utilizando el formato PNG.
        /// </summary>
        /// <param name="bitmap">El Bitmap que se desea convertir.</param>
        /// <returns>Un objeto XImage generado a partir del Bitmap.</returns>
        public static XImage ConvertBitmapToXImage(Bitmap bitmap)
        {
            try
            {
                return XImage.FromGdiPlusImage(bitmap);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al convertir Bitmap a XImage. Clase:PDFServices. Metodo:ConvertBitmapToXImage.  Detalles: " + ex.Message, ex);
            }
        }

        public static PdfPage CreatePagePdfDoc(XSize imageSize)
        {
            var page = new PdfSharp.Pdf.PdfPage();
            page.Width = imageSize.Width;
            page.Height = imageSize.Height;
            return page;
        }


        /// <summary>
        /// Crea un nuevo documento PDF.
        /// </summary>
        /// <returns>Un objeto PdfDocument que se utilizará como documento PDF de destino.</returns>
        public static PdfDocument CreatePdfDocument()
        {
            try
            {
                return new PdfDocument();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear un nuevo documento PDF. Clase:PDFServices. Metodo:CreatePdfDocument." + ex.Message, ex);
            }
        }

        /// <summary>
        /// Convierte un valor de longitud desde una unidad de medida específica a puntos (72 puntos por pulgada) en un contexto de resolución dado.
        /// </summary>
        /// <param name="value">El valor de longitud que se desea convertir.</param>
        /// <param name="dpi">La resolución en puntos por pulgada (DPI) en la que se realiza la conversión.</param>
        /// <returns>El valor de longitud convertido a puntos en el contexto de resolución especificado.</returns>
        public static double ConvertToPoints(double value, double dpi)
        {
            try
            {
                return value * 72.0 / dpi;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al convertir unidades a puntos. Clase:PDFServices. Metodo:ConvertToPoints." + ex.Message, ex);
            }
        }

        private static double CalculateScale(double value1, double value2)
        {
            try
            {
                if (value2 == 0)
                {
                    throw new Exception("Error al calcular la escala: el divisor no puede ser cero. Clase:PDFServices. Metodo:CalculateScale.", null);
                }
                return value1 / value2;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular la escala. Clase:PDFServices. Metodo:CalculateScale." + ex.Message, ex);
            }
        }

        /// <summary>
        /// Calcula el tamaño de fuente en puntos para que la altura de la fuente coincida con el valor deseado en píxeles.
        /// </summary>
        /// <param name="targetHeightInPixels">La altura de la fuente deseada en píxeles.</param>
        /// <returns>El tamaño de fuente en puntos que produce la altura de fuente deseada.</returns>
        private static double CalculateFontSize(double targetHeightInPixels)
        {
            const double tolerance = 1;
            const string fontFamilyName = "Arial";
            double realSizeInPoints = 12;

            var fontTest = new XFont(fontFamilyName, realSizeInPoints);
            double fontHeightInPixels = fontTest.GetHeight();

            while (Math.Abs(fontHeightInPixels - targetHeightInPixels) > tolerance)
            {
                if (fontHeightInPixels < targetHeightInPixels)
                {
                    realSizeInPoints += 1.0d;
                }
                else
                {
                    realSizeInPoints -= 1.0d;
                }

                fontTest = new XFont(fontFamilyName, realSizeInPoints);
                fontHeightInPixels = fontTest.GetHeight();
            }

            return realSizeInPoints;
        }

        /// <summary>
        /// Dibuja el texto en un documento PDF en la ubicación especificada con el tamaño de fuente indicado.
        /// </summary>
        /// <param name="gfx">El contexto gráfico para el PDF.</param>
        /// <param name="text">El texto a dibujar.</param>
        /// <param name="position">La posición (X, Y) en la que se dibujará el texto.</param>
        /// <param name="fontSize">Tamaño de fuente en puntos.</param>
        public static void DrawTextOnPdf(XGraphics gfx, string text, XPoint position, double fontSize)
        {
            try
            {
                var font = new XFont("Arial", fontSize);              // Agrega la palabra con el tamaño de fuente calculado
                XBrush brush = XBrushes.Transparent;                    // Establecer el color del texto como transparente
                gfx.DrawString(text, font, brush, position);    // Agregar la palabra y sus coordenadas al PDF
                //gfx.DrawString(word, font, XBrushes.Black, new XPoint(x1, y1));  // Agregar la palabra y sus coordenadas al PDF
            }
            catch (Exception ex)
            {
                throw new Exception("Error al dibujar texto en el PDF. Clase:PDFServices. Metodo:DrawTextOnPdf.  Detalles: " + ex.Message, ex);
            }
        }


    }
}


