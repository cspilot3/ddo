using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Miharu.Uploader.Imaging
{
    public class ImageManager
    {
        public static Bitmap GetThumbnail(Bitmap nImage, int nAncho, int nAlto)
        {
            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            return new Bitmap(nImage.GetThumbnailImage(nAncho, nAlto, myCallback, IntPtr.Zero));
        }

        public static short GetFolios(Bitmap Imagen)
        {
            // Crea un objeto mapa de bits a partir de la imagen            
            Guid[] myGuid = Imagen.FrameDimensionsList;
            FrameDimension myFrameDimension = new FrameDimension(myGuid[0]);

            return (short)Imagen.GetFrameCount(myFrameDimension);
        }

        public static Bitmap GetFolio(Bitmap nImagen, int nFolio)
        {
            // Crea un objeto mapa de bits a partir de la imagen            
            Guid[] myGuid = nImagen.FrameDimensionsList;
            FrameDimension myFrameDimension = new FrameDimension(myGuid[0]);
            nImagen.SelectActiveFrame(myFrameDimension, nFolio - 1);
            return new Bitmap(nImagen);
        }

        public static byte[] GetData(string nFileName)
        {
            FileStream fsInput = new FileStream(nFileName, FileMode.Open, FileAccess.Read);
            int Longitud = (int)(fsInput.Length - 1);
            byte[] Data = new byte[Longitud];

            fsInput.Read(Data, 0, Data.Length);
            fsInput.Close();

            return Data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static bool ThumbnailCallback()
        {
            return false;
        }
    }
}
