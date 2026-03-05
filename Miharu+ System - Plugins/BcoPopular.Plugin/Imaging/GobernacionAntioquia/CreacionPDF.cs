using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BcoPopular.Plugin.Imaging.GobernacionAntioquia
{
    public class CreacionPDF
    {
        public static void Save(Slyg.Tools.Imaging.FreeImageAPI.FreeImageBitmap nInputImage, string nOutputFileName, string nSuffixFormat, Slyg.Tools.Imaging.ImageManager.EnumFormat nFormat, Slyg.Tools.Imaging.ImageManager.EnumCompression nCompression, bool nSinglePage, string nTempPath)
        {
#if DEBUG
            try
            {
#endif
                // ReSharper disable AssignNullToNotNullAttribute
                if (!Directory.Exists(Path.GetDirectoryName(nOutputFileName)))
                    Directory.CreateDirectory(Path.GetDirectoryName(nOutputFileName));
                // ReSharper restore AssignNullToNotNullAttribute

                if (!Directory.Exists(nTempPath))
                    Directory.CreateDirectory(nTempPath);


                switch (nFormat)
                {
                    case Slyg.Tools.Imaging.ImageManager.EnumFormat.Tiff:
                        //if (nSinglePage)
                        //    SaveToTiff(nInputImage, nOutputFileName, nSuffixFormat, 1, nCompression);
                        //else
                        //    SaveToTiff(nInputImage, nOutputFileName, nCompression);

                        break;

                    case Slyg.Tools.Imaging.ImageManager.EnumFormat.Pdf:
                        //if (nSinglePage)
                        //    //SaveToPdf(nInputImage, nOutputFileName, nSuffixFormat, 1, nTempPath);
                        //else
                        //    //SaveToPdf(nInputImage, nOutputFileName, nTempPath);

                        break;

                    case Slyg.Tools.Imaging.ImageManager.EnumFormat.Bmp:
                    case Slyg.Tools.Imaging.ImageManager.EnumFormat.Gif:
                    case Slyg.Tools.Imaging.ImageManager.EnumFormat.Jpeg:
                    case Slyg.Tools.Imaging.ImageManager.EnumFormat.Png:
                        //SaveToImage(nInputImage, nOutputFileName, nSuffixFormat, 0, nFormat);

                        break;

                    default:
                        throw new Exception("Formato de salida no válido: " + Enum.GetName(typeof(Slyg.Tools.Imaging.ImageManager.EnumFormat), nFormat));
                }
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("Save - " + ex.Message);
            }
#endif
        }

    }
}
