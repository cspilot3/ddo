using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Gios;
using System.Drawing;
using System.Drawing.Drawing2D;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Windows.Media.Imaging;
using iTextSharp;
using System.Text;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.SqlClient;
using System.Data;
using GdPicture12;
using System.Linq;



namespace Slyg.Tools.Imaging
{
    /// <summary>
    /// Creado por Soluciones Lógicas Yayan Gaia
    /// URL: http:www.slyg.com.co
    /// Descripción:
    /// Clase de manipulación de imágenes
    /// </summary>
    public class ImageManager
    {
        #region Enumeraciones

        /// <summary>
        /// Formatos de imágenes soportados
        /// </summary>
        public enum EnumFormat : byte
        {
            Bmp = 0,
            Gif = 1,
            Jpeg = 2,
            Pdf = 3,
            Png = 4,
            Tiff = 5
        }

        /// <summary>
        /// Tipos de compresión soportados para imágenes TIFF
        /// </summary>
        public enum EnumCompression : byte
        {
            None = 0,
            Ccitt3 = 1,
            Ccitt4 = 2,
            Jpeg = 3,
            Lzw = 4,
        }

        #endregion

        #region Save

        /// <summary>
        /// Guarda un bitmap en disco con el formato y la compresión definidos
        /// </summary>
        /// <param name="nInputImage">Bitmap a guardar</param>
        /// <param name="nOutputFileName">Nombre del fichero de salida</param>
        /// <param name="nSuffixFormat"></param>
        /// <param name="nFormat">Formato que tendrá la imagen de salida</param>
        /// <param name="nCompression">Tipo de compresión usado en la imagen de salida</param>
        /// <param name="nSinglePage">Define si la salidad será un único archivo con varios fólios o un archivo por folio</param>
        /// <param name="nTempPath">Carpeta usada para las operaciones de escritura</param>
        public static void Save(FreeImageAPI.FreeImageBitmap nInputImage, string nOutputFileName, string nSuffixFormat, EnumFormat nFormat, EnumCompression nCompression, bool nSinglePage, string nTempPath)
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
                    case EnumFormat.Tiff:
                        if (nSinglePage)
                            SaveToTiff(nInputImage, nOutputFileName, nSuffixFormat, 1, nCompression);
                        else
                            SaveToTiff(nInputImage, nOutputFileName, nCompression);

                        break;

                    case EnumFormat.Pdf:
                        if (nSinglePage)
                            SaveToPdf(nInputImage, nOutputFileName, nSuffixFormat, 1, nTempPath);
                        else
                            SaveToPdf(nInputImage, nOutputFileName, nTempPath);

                        break;

                    case EnumFormat.Bmp:
                    case EnumFormat.Gif:
                    case EnumFormat.Jpeg:
                    case EnumFormat.Png:
                        SaveToJpeg(nInputImage, nOutputFileName, nCompression);
                        //SaveToImage(nInputImage, nOutputFileName, nSuffixFormat, 0, nFormat);
                        //SaveToImage(nInputImage, nOutputFileName + ".jpg", "-0000", 1, EnumFormat.Jpeg);

                        break;

                    default:
                        throw new Exception("Formato de salida no válido: " + Enum.GetName(typeof(EnumFormat), nFormat));
                }
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("Save - " + ex.Message);
            }
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputImages"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nSuffixFormat"></param>
        /// <param name="nFormat"></param>
        /// <param name="nCompression"></param>
        /// <param name="nSinglePage"></param>
        /// <param name="nTempPath"></param>
        public static void Save(List<FreeImageAPI.FreeImageBitmap> nInputImages, string nOutputFileName, string nSuffixFormat, EnumFormat nFormat, EnumCompression nCompression, bool nSinglePage, string nTempPath)
        {
            // ReSharper disable AssignNullToNotNullAttribute
            if (!Directory.Exists(Path.GetDirectoryName(nOutputFileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(nOutputFileName));
            // ReSharper restore AssignNullToNotNullAttribute

            if (!Directory.Exists(nTempPath))
                Directory.CreateDirectory(nTempPath);

            switch (nFormat)
            {
                case EnumFormat.Tiff:
                    if (nSinglePage)
                        SaveToTiff(nInputImages, nOutputFileName, nSuffixFormat, nCompression);
                    else
                        SaveToTiff(nInputImages, nOutputFileName, nCompression);

                    break;

                case EnumFormat.Pdf:
                    if (nSinglePage)
                        SaveToPdf(nInputImages, nOutputFileName, nSuffixFormat, nTempPath);
                    else
                        SaveToPdf(nInputImages, nOutputFileName, nTempPath);

                    break;

                case EnumFormat.Bmp:
                case EnumFormat.Gif:
                case EnumFormat.Jpeg:
                case EnumFormat.Png:
                    SaveToImage(nInputImages, nOutputFileName, nSuffixFormat, nFormat);

                    break;

                default:
                    throw new Exception("Formato de salida no válido: " + Enum.GetName(typeof(EnumFormat), nFormat));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputFileName"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nSuffixFormat"></param>
        /// <param name="nFormat"></param>
        /// <param name="nCompression"></param>
        /// <param name="nSinglePage"></param>
        /// <param name="nTempPath"></param>
        public static void Save(string nInputFileName, string nOutputFileName, string nSuffixFormat, EnumFormat nFormat, EnumCompression nCompression, bool nSinglePage, string nTempPath)
        {
            // ReSharper disable AssignNullToNotNullAttribute
            if (!Directory.Exists(Path.GetDirectoryName(nOutputFileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(nOutputFileName));
            // ReSharper restore AssignNullToNotNullAttribute

            if (!Directory.Exists(nTempPath))
                Directory.CreateDirectory(nTempPath);

            switch (nFormat)
            {
                case EnumFormat.Tiff:
                    if (nSinglePage)
                        SaveToTiff(nInputFileName, nOutputFileName, nSuffixFormat, 0, nCompression);
                    else
                        SaveToTiff(nInputFileName, nOutputFileName, nCompression, nTempPath);

                    break;

                case EnumFormat.Pdf:
                    if (nSinglePage)
                        SaveToPdf(nInputFileName, nOutputFileName, nSuffixFormat, 0, nTempPath);
                    else
                        SaveToPdf(nInputFileName, nOutputFileName, nTempPath);

                    break;

                case EnumFormat.Bmp:
                case EnumFormat.Gif:
                case EnumFormat.Jpeg:
                case EnumFormat.Png:
                    SaveToImage(nInputFileName, nOutputFileName, nSuffixFormat, 0, nFormat);


                    break;

                default:
                    throw new Exception("Formato de salida no válido: " + Enum.GetName(typeof(EnumFormat), nFormat));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputFileNames"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nSuffixFormat"></param>
        /// <param name="nFormat"></param>
        /// <param name="nCompression"></param>
        /// <param name="nSinglePage"></param>
        /// <param name="nTempPath"></param>
        /// <param name="nIsInputSingle"></param>
        public static void Save(List<string> nInputFileNames, string nOutputFileName, string nSuffixFormat, EnumFormat nFormat, EnumCompression nCompression, bool nSinglePage, string nTempPath, bool nIsInputSingle)
        {
            // ReSharper disable AssignNullToNotNullAttribute
            if (!Directory.Exists(Path.GetDirectoryName(nOutputFileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(nOutputFileName));
            // ReSharper restore AssignNullToNotNullAttribute

            if (!Directory.Exists(nTempPath))
                Directory.CreateDirectory(nTempPath);

            switch (nFormat)
            {
                case EnumFormat.Tiff:
                    if (nSinglePage)
                        SaveToTiff(nInputFileNames, nOutputFileName, nSuffixFormat, nCompression);
                    else
                        SaveToTiff(nInputFileNames, nOutputFileName, nCompression, nTempPath, nIsInputSingle);

                    break;

                case EnumFormat.Pdf:
                    if (nSinglePage)
                        SaveToPdf(nInputFileNames, nOutputFileName, nSuffixFormat, nTempPath);
                    else
                        SaveToPdf(nInputFileNames, nOutputFileName, nTempPath);
                    //SaveToPdfA(nInputFileNames, nOutputFileName, nTempPath);
                    break;

                case EnumFormat.Bmp:
                case EnumFormat.Gif:
                case EnumFormat.Jpeg:
                case EnumFormat.Png:
                    SaveToImage(nInputFileNames, nOutputFileName, nSuffixFormat, nFormat);
                    break;

                default:
                    throw new Exception("Formato de salida no válido: " + Enum.GetName(typeof(EnumFormat), nFormat));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputFileNames"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nSuffixFormat"></param>
        /// <param name="nFormat"></param>
        /// <param name="nCompression"></param>
        /// <param name="nSinglePage"></param>
        /// <param name="nTempPath"></param>
        /// <param name="nIsInputSingle"></param>
        public static void Save(List<string> nInputFileNames, string nOutputFileName, string nSuffixFormat, EnumFormat nFormat, EnumCompression nCompression, bool nSinglePage, string nTempPath, bool nIsInputSingle, bool nIsPDF)
        {
            // ReSharper disable AssignNullToNotNullAttribute
            if (!Directory.Exists(Path.GetDirectoryName(nOutputFileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(nOutputFileName));
            // ReSharper restore AssignNullToNotNullAttribute

            if (!Directory.Exists(nTempPath))
                Directory.CreateDirectory(nTempPath);

            switch (nFormat)
            {
                case EnumFormat.Tiff:
                    if (nSinglePage)
                        SaveToTiff(nInputFileNames, nOutputFileName, nSuffixFormat, nCompression);
                    else
                        SaveToTiff(nInputFileNames, nOutputFileName, nCompression, nTempPath, nIsInputSingle);

                    break;

                case EnumFormat.Pdf:
                    if (nSinglePage)
                        SaveToPdf(nInputFileNames, nOutputFileName, nSuffixFormat, nTempPath);
                    else
                        CreatePdf(nInputFileNames, nOutputFileName);
                    break;

                case EnumFormat.Bmp:
                case EnumFormat.Gif:
                case EnumFormat.Jpeg:
                    SaveToTiff(nInputFileNames, nOutputFileName, nCompression, nTempPath, nIsInputSingle);
                    break;
                case EnumFormat.Png:
                    SaveToImage(nInputFileNames, nOutputFileName, nSuffixFormat, nFormat);

                    break;

                default:
                    throw new Exception("Formato de salida no válido: " + Enum.GetName(typeof(EnumFormat), nFormat));
            }
        }

        /// <summary>
        /// Convierte el archivo PDF a PDF/A 
        /// </summary>
        /// <param name="nInputFileNames"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nSuffixFormat"></param>
        /// <param name="nFormat"></param>
        /// <param name="nCompression"></param>
        /// <param name="nSinglePage"></param>
        /// <param name="nTempPath"></param>
        /// <param name="nIsInputSingle"></param>

        // Creación Pdf Baja Calidad
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputFileNames"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nSuffixFormat"></param>
        /// <param name="nFormat"></param>
        /// <param name="nCalidad"></param>
        /// <param name="nSinglePage"></param>
        /// <param name="nTempPath"></param>
        /// <param name="nIsInputSingle"></param>
        public static void Save(List<string> nInputFileNames, string nOutputFileName, string nSuffixFormat, EnumFormat nFormat, bool nCalidad, bool nSinglePage, string nTempPath, bool nIsInputSingle)
        {
            // ReSharper disable AssignNullToNotNullAttribute
            if (!Directory.Exists(Path.GetDirectoryName(nOutputFileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(nOutputFileName));
            // ReSharper restore AssignNullToNotNullAttribute

            if (!Directory.Exists(nTempPath))
                Directory.CreateDirectory(nTempPath);

            if (nSinglePage)
                SaveToPdf(nInputFileNames, nOutputFileName, nSuffixFormat, nTempPath);
            else
                SaveToPdf(nInputFileNames, nOutputFileName, nTempPath);
        }

        #endregion

        #region Split

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputFileNames"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nOutputFileNames"></param>
        /// <param name="nSuffixFormat"></param>
        /// <param name="nCompression"></param>
        /// <param name="nTempPath"></param>
        /// <param name="nDelay"></param>
        /// <returns></returns>
        public static void Split(List<string> nInputFileNames, string nOutputFileName, ref List<string> nOutputFileNames, string nSuffixFormat, EnumCompression nCompression, string nTempPath, short nDelay)
        {
#if DEBUG
            try
            {
#endif
                var tempFileName = Path.GetFullPath(nOutputFileName).TrimEnd('\\') + "\\" + Path.GetFileNameWithoutExtension(nOutputFileName);
                var folios = 0;

                // ReSharper disable AssignNullToNotNullAttribute
                if (!Directory.Exists(Path.GetDirectoryName(nOutputFileName)))
                    Directory.CreateDirectory(Path.GetDirectoryName(nOutputFileName));
                // ReSharper restore AssignNullToNotNullAttribute

                if (!Directory.Exists(nTempPath))
                    Directory.CreateDirectory(nTempPath);

                foreach (var fileName in nInputFileNames)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    var extension = Path.GetExtension(fileName).ToLower();

                    int newFolios;
                    switch (extension)
                    {
                        case ".tif":
                            newFolios = SaveToTiff(fileName, nOutputFileName, nSuffixFormat, folios + 1, nCompression);

                            for (var folio = 1; folio <= newFolios; folio++)
                            {
                                nOutputFileNames.Add(tempFileName + (folios + folio).ToString(nSuffixFormat) + ".tif");
                            }

                            folios += newFolios;

                            break;

                        case ".pdf":
                            newFolios = SaveToPdf(fileName, nOutputFileName, nSuffixFormat, folios + 1, nTempPath);

                            for (var folio = 1; folio <= newFolios; folio++)
                            {
                                nOutputFileNames.Add(tempFileName + (folios + folio).ToString(nSuffixFormat) + ".pdf");
                            }

                            folios += newFolios;

                            break;

                        default:
                            newFolios = SaveToImage(fileName, nOutputFileName, nSuffixFormat, folios + 1, GetEnumFormat(fileName));

                            for (var folio = 1; folio <= newFolios; folio++)
                            {
                                nOutputFileNames.Add(tempFileName + (folios + folio).ToString(nSuffixFormat) + extension);
                            }

                            folios += newFolios;

                            break;
                    }

                    System.Threading.Thread.Sleep(nDelay);
                }
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("Split - " + ex.Message);
            }
#endif
        }

        #endregion

        #region SaveToImage

        /// <summary>
        /// Almacena un FreeImageAPI.FreeImageBitmap como imagen en disco
        /// </summary>
        /// <param name="nInputImage">FreeImageAPI.FreeImageBitmap a almacenar</param>
        /// <param name="nOutputFileName">Nombre de la imagen almacenada</param>
        /// <param name="nSuffixFormat">Formato del consecutivo del nombre de la imagen almacenada si el FreeImageAPI.FreeImageBitmap representa una imagen de múltiples folios</param>
        /// <param name="nSuffixStartIndex">Inicio del consecutivo del sufijo del nombre de la imagen, 0 para omitirlo</param>
        /// <param name="nFormat">Formato de salida de la imagen</param>
        /// <returns>Imágenes generadas</returns>
        private static int SaveToImage(FreeImageAPI.FreeImageBitmap nInputImage, string nOutputFileName, string nSuffixFormat, int nSuffixStartIndex, EnumFormat nFormat)
        {
#if DEBUG
            try
            {
#endif
                var folios = GetFolios(nInputImage);

                for (var folio = 1; folio <= folios; folio++)
                {
                    using (var image = GetFolioBitmap(nInputImage, folio))
                    {
                        string newFileName;
                        var fileName = nOutputFileName.Replace(Path.GetExtension(nOutputFileName), "");
                        var extension = GetExtension(nFormat);

                        if (nSuffixStartIndex > 0)
                            newFileName = fileName + (nSuffixStartIndex + folio - 1).ToString(nSuffixFormat) + extension;
                        else if (folios > 1)
                            newFileName = fileName + folio.ToString(nSuffixFormat) + extension;
                        else
                            newFileName = fileName + extension;

#if DEBUG
                        try
                        {
#endif
                            if (File.Exists(newFileName))
                                File.Delete(newFileName);
#if DEBUG
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("No se pudo borrar el archivo " + newFileName + ". " + ex.Message);
                        }
#endif
#if DEBUG
                        try
                        {
#endif
                            image.Save(newFileName, GetImageFormat(nFormat));

#if DEBUG
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error al guardar la imagen " + newFileName + " con formato " + Enum.GetName(typeof(EnumFormat), nFormat) + ". " + ex.Message);
                        }
#endif
                    }
                }

                return folios;
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("SaveToImage: " + ex.Message);
            }
#endif
        }

        private static int SaveToImage(FreeImageAPI.FreeImageBitmap nInputImage, string nOutputFileName, string nSuffixFormat, int nSuffixStartIndex, EnumFormat nFormat, FreeImageAPI.FREE_IMAGE_SAVE_FLAGS nFlags)
        {
#if DEBUG
            try
            {
#endif
                var folios = GetFolios(nInputImage);

                for (var folio = 1; folio <= folios; folio++)
                {
                    using (var image = GetFolioBitmap(nInputImage, folio))
                    {
                        string newFileName;
                        var fileName = nOutputFileName.Replace(Path.GetExtension(nOutputFileName), "");
                        var extension = GetExtension(nFormat);

                        if (nSuffixStartIndex > 0)
                            newFileName = fileName + (nSuffixStartIndex + folio - 1).ToString(nSuffixFormat) + extension;
                        else if (folios > 1)
                            newFileName = fileName + folio.ToString(nSuffixFormat) + extension;
                        else
                            newFileName = fileName + extension;

#if DEBUG
                        try
                        {
#endif
                            if (File.Exists(newFileName))
                                File.Delete(newFileName);
#if DEBUG
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("No se pudo borrar el archivo " + newFileName + ". " + ex.Message);
                        }
#endif
#if DEBUG
                        try
                        {
#endif
                            image.Save(newFileName, GetImageFormat(nFormat), nFlags);

#if DEBUG
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error al guardar la imagen " + newFileName + " con formato " + Enum.GetName(typeof(EnumFormat), nFormat) + ". " + ex.Message);
                        }
#endif
                    }
                }

                return folios;
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("SaveToImage: " + ex.Message);
            }
#endif
        }


        /// <summary>
        /// Almacena una lista de FreeImageAPI.FreeImageBitmaps como imagen en disco
        /// </summary>
        /// <param name="nInputImages">Lista de FreeImageAPI.FreeImageBitmaps a almacenar</param>
        /// <param name="nOutputFileName">Nombre de la imagen almacenada</param>
        /// <param name="nSuffixFormat">Formato del consecutivo del nombre de la imagen almacenada</param>
        /// <param name="nFormat">Formato de salida de la imagen</param>
        private static int SaveToImage(IEnumerable<FreeImageAPI.FreeImageBitmap> nInputImages, string nOutputFileName, string nSuffixFormat, EnumFormat nFormat)
        {
            var folios = 0;

            foreach (var imagen in nInputImages)
            {
                folios += SaveToImage(imagen, nOutputFileName, nSuffixFormat, folios + 1, nFormat);
            }

            return folios;
        }

        /// <summary>
        /// Convierte un archivo de imagen a otro formato
        /// </summary>
        /// <param name="nInputFileName">Archivos a convertir</param>
        /// <param name="nOutputFileName">Nombre de la imagen almacenada</param>
        /// <param name="nSuffixFormat">Formato del consecutivo del nombre de la imagen almacenada si el FreeImageAPI.FreeImageBitmap representa una imagen de múltiples folios</param>
        /// <param name="nSuffixStartIndex">Inicio del consecutivo del sufijo del nombre de la imagen, 0 para omitirlo</param>
        /// <param name="nFormat">Formato de salida de la imagen</param>
        /// <returns>Imágenes generadas</returns>
        public static int SaveToImage(string nInputFileName, string nOutputFileName, string nSuffixFormat, int nSuffixStartIndex, EnumFormat nFormat)
        {
            var folios = 0;

            // ReSharper disable once PossibleNullReferenceException
            switch (Path.GetExtension(nInputFileName).ToLower())
            {
                case ".pdf":
                    var newFolios = GetFolios(nInputFileName);

                    for (var folio = 1; folio <= newFolios; folio++)
                    {
                        using (var imagen = GetFolioBitmap(nInputFileName, folio))
                        {
                            if (nSuffixStartIndex > 0)
                                folios += SaveToImage(imagen, nOutputFileName, nSuffixFormat, nSuffixStartIndex + folios, nFormat, FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYGOOD);
                            else if (newFolios > 1)
                                folios += SaveToImage(imagen, nOutputFileName, nSuffixFormat, folio, nFormat, FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYGOOD);
                            else
                                folios += SaveToImage(imagen, nOutputFileName, "", 0, nFormat, FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYGOOD);
                        }
                    }

                    break;

                default:
                    using (var imagen = new FreeImageAPI.FreeImageBitmap(nInputFileName))
                    {
                        folios += SaveToImage(imagen, nOutputFileName, nSuffixFormat, nSuffixStartIndex, nFormat);
                    }

                    break;
            }

            return folios;
        }

        /// <summary>
        /// Convierte una lista de archivos de imagen a otro formato
        /// </summary>
        /// <param name="nInputFileNames">Lista de archivos a convertir</param>
        /// <param name="nOutputFileName">Nombre de la imagen almacenada</param>
        /// <param name="nSuffixFormat">Formato del consecutivo del nombre de la imagen almacenada si el FreeImageAPI.FreeImageBitmap representa una imagen de múltiples folios</param>        
        /// <param name="nFormat">Formato de salida de la imagen</param>
        /// <returns>Imágenes generadas</returns>
        private static int SaveToImage(IEnumerable<string> nInputFileNames, string nOutputFileName, string nSuffixFormat, EnumFormat nFormat, FreeImageAPI.FREE_IMAGE_SAVE_FLAGS nFlags = FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.DEFAULT)
        {
            var folios = 0;

            foreach (var fileName in nInputFileNames)
            {
                // ReSharper disable once PossibleNullReferenceException
                switch (Path.GetExtension(fileName).ToLower())
                {
                    case ".pdf":
                        var newFolios = GetFolios(fileName);

                        for (var folio = 1; folio <= newFolios; folio++)
                        {
                            using (var imagen = GetFolioBitmap(fileName, folio))
                            {
                                folios += SaveToImage(imagen, nOutputFileName, nSuffixFormat, folios + folio, nFormat, nFlags);
                            }
                        }

                        break;

                    default:
                        using (var imagen = new FreeImageAPI.FreeImageBitmap(fileName))
                        {
                            folios += SaveToImage(imagen, nOutputFileName, nSuffixFormat, folios + 1, nFormat, nFlags);
                        }

                        break;
                }
            }

            return folios;
        }

        #endregion

        #region SaveToTIFF

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputImage"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nCompression"></param>
        /// <returns></returns>
        private static void SaveToTiff(FreeImageAPI.FreeImageBitmap nInputImage, string nOutputFileName, EnumCompression nCompression)
        {
#if DEBUG
            try
            {
#endif
                var compresion = GetFreeImageCompression(nCompression);
                var fiBitmap = FreeImageAPI.FreeImage.CreateFromBitmap((System.Drawing.Bitmap)nInputImage);
                FreeImageAPI.FreeImage.Save(FreeImageAPI.FREE_IMAGE_FORMAT.FIF_TIFF, fiBitmap, nOutputFileName, compresion);
                FreeImageAPI.FreeImage.Unload(fiBitmap);
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("SaveToTIFF 1 - " + ex.Message);
            }
#endif
        }

        private static void SaveToJpeg(FreeImageAPI.FreeImageBitmap nInputImage, string nOutputFileName, EnumCompression nCompression)
        {
#if DEBUG
            try
            {
#endif
                var compresion = GetFreeImageCompression(nCompression);
                var fiBitmap = FreeImageAPI.FreeImage.CreateFromBitmap((System.Drawing.Bitmap)nInputImage);
                FreeImageAPI.FreeImage.Save(FreeImageAPI.FREE_IMAGE_FORMAT.FIF_JPEG, fiBitmap, nOutputFileName, compresion);
                FreeImageAPI.FreeImage.Unload(fiBitmap);
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("SaveToTIFF 1 - " + ex.Message);
            }
#endif
        }

        private static void SaveToJpeg(IList<string> nInputFileNames, string nOutputFileName, EnumCompression nCompression, string nTempPath)
        {

            try
            {

                using (System.Drawing.Image imageFile = System.Drawing.Image.FromFile(nTempPath))
                {
                    FrameDimension frameDimensions = new FrameDimension(
                    imageFile.FrameDimensionsList[Convert.ToInt16(nInputFileNames.Count)]);

                    int frameNum = imageFile.GetFrameCount(frameDimensions);

                    // Gets the number of pages from the tiff image (if multipage) 

                    string[] jpegPaths = new string[frameNum];

                    for (int frame = 0; frame < frameNum; frame++)
                    {
                        // Selects one frame at a time and save as jpeg. 
                        imageFile.SelectActiveFrame(frameDimensions, frame);
                        using (Bitmap bmp = new Bitmap(imageFile))
                        {
                            jpegPaths[frame] = String.Format(nOutputFileName,
                                Path.GetDirectoryName(nTempPath),
                                Path.GetFileNameWithoutExtension(nTempPath),
                                frame);
                            bmp.Save(jpegPaths[frame], ImageFormat.Jpeg);
                        }




                        //return jpegPaths;
                    }

                }


            }
            catch (Exception ex)
            {
                throw new Exception("SaveToTIFF 1 - " + ex.Message);
            }

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputImage"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nSuffixFormat"></param>
        /// <param name="nSuffixStartIndex"></param>
        /// <param name="nCompression"></param>
        /// <returns></returns>
        private static int SaveToTiff(FreeImageAPI.FreeImageBitmap nInputImage, string nOutputFileName, string nSuffixFormat, int nSuffixStartIndex, EnumCompression nCompression)
        {
#if DEBUG
            try
            {
#endif
                var folios = GetFolios(nInputImage);
                // ReSharper disable once PossibleNullReferenceException
                var fileName = Path.GetDirectoryName(nOutputFileName).TrimEnd('\\') + "\\" + Path.GetFileNameWithoutExtension(nOutputFileName);

                for (var folio = 1; folio <= folios; folio++)
                {
                    using (var image = GetFolioBitmap(nInputImage, folio))
                    {
                        string newFileName;
                        if (nSuffixStartIndex > 0)
                            newFileName = fileName + (nSuffixStartIndex + folio - 1).ToString(nSuffixFormat) + ".tif";
                        else if (folios > 1)
                            newFileName = fileName + folio.ToString(nSuffixFormat) + ".tif";
                        else
                            newFileName = fileName + ".tif";

                        SaveToTiff(image, newFileName, nCompression);
                    }
                }

                return folios;
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("SaveToTIFF 2 - " + ex.Message);
            }
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nImageList"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nCompression"></param>
        /// <returns></returns>
        private static void SaveToTiff(IEnumerable<FreeImageAPI.FreeImageBitmap> nImageList, string nOutputFileName, EnumCompression nCompression)
        {
#if DEBUG
            try
            {
#endif
                var compresion = GetFreeImageCompression(nCompression);
                var newMultiBitmap = FreeImageAPI.FreeImage.OpenMultiBitmap(FreeImageAPI.FREE_IMAGE_FORMAT.FIF_TIFF, nOutputFileName, true, false, true, FreeImageAPI.FREE_IMAGE_LOAD_FLAGS.DEFAULT);
                var fiBitmapList = new List<FreeImageAPI.FIBITMAP>();

                foreach (var imagen in nImageList)
                {
                    var fiBitmap = FreeImageAPI.FreeImage.CreateFromBitmap((System.Drawing.Bitmap)imagen);
                    FreeImageAPI.FreeImage.AppendPage(newMultiBitmap, fiBitmap);
                    fiBitmapList.Add(fiBitmap);
                }

                FreeImageAPI.FreeImage.CloseMultiBitmap(newMultiBitmap, compresion);

                foreach (var fiBitmap in fiBitmapList)
                {
                    FreeImageAPI.FreeImage.Unload(fiBitmap);
                }

                //return GetFolios(nOutputFileName);
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("SaveToTIFF 3 - " + ex.Message);
            }
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputImages"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nSuffixFormat"></param>
        /// <param name="nCompression"></param>
        /// <returns></returns>
        private static void SaveToTiff(IEnumerable<FreeImageAPI.FreeImageBitmap> nInputImages, string nOutputFileName, string nSuffixFormat, EnumCompression nCompression)
        {
            var folios = 0;

            foreach (FreeImageAPI.FreeImageBitmap imagen in nInputImages)
            {
                folios += SaveToTiff(imagen, nOutputFileName, nSuffixFormat, folios + 1, nCompression);
            }

            //return Folios;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputFileName"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nCompression"></param>
        /// <param name="nTempPath"></param>
        /// <returns></returns>
        private static void SaveToTiff(string nInputFileName, string nOutputFileName, EnumCompression nCompression, string nTempPath)
        {
#if DEBUG
            try
            {
#endif
                var tempFileName = nTempPath.TrimEnd('\\') + "\\" + Guid.NewGuid();
                var folios = SaveToImage(nInputFileName, tempFileName + ".png", "-0000", 1, EnumFormat.Png);

                var filenames = new List<string>();

                for (var folio = 1; folio <= folios; folio++)
                {
                    filenames.Add(tempFileName + folio.ToString("-0000") + ".png");
                }

                SaveToTiff(filenames, nOutputFileName, nCompression, nTempPath, true);

                for (var folio = 1; folio <= folios; folio++)
                {
                    File.Delete(tempFileName + folio.ToString("-0000") + ".png");
                }

                //return Folios;
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("SaveToTIFF 5 - " + ex.Message);
            }
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputFileName"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nSuffixFormat"></param>
        /// <param name="nSuffixStartIndex"></param>
        /// <param name="nCompression"></param>
        /// <returns></returns>
        private static int SaveToTiff(string nInputFileName, string nOutputFileName, string nSuffixFormat, int nSuffixStartIndex, EnumCompression nCompression)
        {
            var folios = 0;
            // ReSharper disable once PossibleNullReferenceException
            var extension = Path.GetExtension(nInputFileName).ToLower();

            switch (extension)
            {
                case ".tif":
                    using (var imagen = new FreeImageAPI.FreeImageBitmap(nInputFileName))
                    {
                        folios = SaveToTiff(imagen, nOutputFileName, nSuffixFormat, nSuffixStartIndex, nCompression);
                    }

                    break;

                case ".pdf":
                    var newFolios = GetFoliosPdf(nInputFileName);

                    for (var folio = 1; folio <= newFolios; folio++)
                    {
                        using (var imagen = GetFolioBitmapFromPdf(nInputFileName, folio))
                        {
                            if (nSuffixStartIndex > 0)
                                folios += SaveToTiff(imagen, nOutputFileName, nSuffixFormat, nSuffixStartIndex + folio - 1, nCompression);
                            else if (newFolios > 1)
                                folios += SaveToTiff(imagen, nOutputFileName, nSuffixFormat, folio, nCompression);
                            else
                                folios += SaveToTiff(imagen, nOutputFileName, "", 0, nCompression);
                        }
                    }
                    break;

                default:
                    folios = 1;

                    using (var imagen = new FreeImageAPI.FreeImageBitmap(nInputFileName))
                    {
                        SaveToTiff(imagen, nOutputFileName, nSuffixFormat, nSuffixStartIndex, nCompression);
                    }

                    break;
            }

            return folios;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputFileNames"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nCompression"></param>
        /// <param name="nTempPath"></param>
        /// <param name="nIsInputSingle"></param>
        /// <returns></returns>
        private static void SaveToTiff(IList<string> nInputFileNames, string nOutputFileName, EnumCompression nCompression, string nTempPath, bool nIsInputSingle)
        {
#if DEBUG
            try
            {
#endif
                var tempFileName = nTempPath.TrimEnd('\\') + "\\" + Guid.NewGuid();
                var folios = 0;

                if (nIsInputSingle)
                {
                    folios = nInputFileNames.Count;
                }
                else
                {
                    foreach (var fileName in nInputFileNames)
                    {
                        folios += SaveToImage(fileName, tempFileName + ".png", "-0000", folios + 1, EnumFormat.Png);
                    }
                }

                var compresion = GetFreeImageCompression(nCompression);
                var newImagen = FreeImageAPI.FreeImage.OpenMultiBitmap(FreeImageAPI.FREE_IMAGE_FORMAT.FIF_TIFF, nOutputFileName, true, false, true, FreeImageAPI.FREE_IMAGE_LOAD_FLAGS.TIFF_CMYK);
                var fBitmapList = new List<FreeImageAPI.FreeImageBitmap>();
                var fiBitmapList = new List<FreeImageAPI.FIBITMAP>();

                for (var folio = 1; folio <= folios; folio++)
                {

                    var imageName = nIsInputSingle ? nInputFileNames[folio - 1] : tempFileName + folio.ToString("-0000") + ".png";
                    var fBitmap = new FreeImageAPI.FreeImageBitmap(imageName, FreeImageAPI.FREE_IMAGE_LOAD_FLAGS.DEFAULT);
                    var fiBitmap = FreeImageAPI.FreeImage.CreateFromBitmap((System.Drawing.Bitmap)fBitmap);
                    FreeImageAPI.FreeImage.AppendPage(newImagen, fiBitmap);
                    fBitmap.Dispose();
                    FreeImageAPI.FreeImage.Unload(fiBitmap);


                }

                FreeImageAPI.FreeImage.CloseMultiBitmap(newImagen, compresion);

                //foreach (var fBitmap in fBitmapList)
                //{
                //    fBitmap.Dispose();
                //}

                //foreach (var fiBitmap in fiBitmapList)
                //{
                //     FreeImageAPI.FreeImage.Unload(fiBitmap);
                //}

                for (var folio = 1; folio <= folios; folio++)
                {
                    //File.Delete(nIsInputSingle ? nInputFileNames[folio - 1] : tempFileName + folio.ToString("-0000") + ".png");
                    File.Delete(tempFileName + folio.ToString("-0000") + ".png");
                }

                //return Folios;
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("SaveToTIFF 7 - " + ex.Message);
            }
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputFileNames"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nSuffixFormat"></param>
        /// <param name="nCompression"></param>        
        /// <returns></returns>
        private static void SaveToTiff(IEnumerable<string> nInputFileNames, string nOutputFileName, string nSuffixFormat, EnumCompression nCompression)
        {
            var folios = 0;

            foreach (var fileName in nInputFileNames)
            {
                folios += SaveToTiff(fileName, nOutputFileName, nSuffixFormat, folios + 1, nCompression);
            }

            //return Folios;
        }

        #endregion

        #region SaveToPDF

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputImage"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nTempPath"></param>
        /// <returns></returns>
        private static void SaveToPdf(FreeImageAPI.FreeImageBitmap nInputImage, string nOutputFileName, string nTempPath)
        {
            var tempFileName = nTempPath.TrimEnd('\\') + "\\" + Guid.NewGuid();

            var folios = SaveToImage(nInputImage, tempFileName + ".jpg", "-0000", 1, EnumFormat.Jpeg);

            CreatePdf(folios, tempFileName, nOutputFileName);

            //return Folios;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputImage"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nSuffixFormat"></param>
        /// <param name="nSuffixStartIndex"></param>
        /// <param name="nTempPath"></param>
        /// <returns></returns>
        private static void SaveToPdf(FreeImageAPI.FreeImageBitmap nInputImage, string nOutputFileName, string nSuffixFormat, int nSuffixStartIndex, string nTempPath)
        {
            var tempFileName = nTempPath.TrimEnd('\\') + "\\" + Guid.NewGuid();

            var folios = SaveToImage(nInputImage, tempFileName + ".jpg", "-0000", nSuffixStartIndex, EnumFormat.Jpeg);

            CreatePdf(folios, tempFileName, nOutputFileName, nSuffixFormat);

            //return Folios;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputImages"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nTempPath"></param>
        /// <returns></returns>
        private static void SaveToPdf(IEnumerable<FreeImageAPI.FreeImageBitmap> nInputImages, string nOutputFileName, string nTempPath)
        {
            var tempFileName = nTempPath.TrimEnd('\\') + "\\" + Guid.NewGuid();

            var folios = SaveToImage(nInputImages, tempFileName + ".jpg", "-0000", EnumFormat.Jpeg);

            CreatePdf(folios, tempFileName, nOutputFileName);

            //return Folios;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputImages"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nSuffixFormat"></param>
        /// <param name="nTempPath"></param>
        /// <returns></returns>
        private static void SaveToPdf(IEnumerable<FreeImageAPI.FreeImageBitmap> nInputImages, string nOutputFileName, string nSuffixFormat, string nTempPath)
        {
            var tempFileName = nTempPath.TrimEnd('\\') + "\\" + Guid.NewGuid();

            var folios = SaveToImage(nInputImages, tempFileName + ".jpg", "-0000", EnumFormat.Jpeg);

            CreatePdf(folios, tempFileName, nOutputFileName, nSuffixFormat);

            //return Folios;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputFileName"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nTempPath"></param>
        /// <returns></returns>
        private static void SaveToPdf(string nInputFileName, string nOutputFileName, string nTempPath)
        {
            //var tempFileName = nTempPath.TrimEnd('\\') + "\\" + Guid.NewGuid();
            //var folios = SaveToImage(nInputFileName, tempFileName + ".jpg", "-0000", 1, EnumFormat.Jpeg);
            //CreatePdf(folios, tempFileName, nOutputFileName);
            CreatePdf(nInputFileName, nOutputFileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputFileName"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nSuffixFormat"></param>
        /// <param name="nSuffixStartIndex"></param>
        /// <param name="nTempPath"></param>
        /// <returns></returns>
        private static int SaveToPdf(string nInputFileName, string nOutputFileName, string nSuffixFormat, int nSuffixStartIndex, string nTempPath)
        {
            string tempFileName = nTempPath.TrimEnd('\\') + "\\" + Guid.NewGuid();
            int folios = 0;

            folios += SaveToImage(nInputFileName, tempFileName + ".jpg", "-0000", nSuffixStartIndex, EnumFormat.Jpeg);

            CreatePdf(folios, tempFileName, nOutputFileName, nSuffixFormat);

            return folios;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputFileNames"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nTempPath"></param>
        /// <returns></returns>
        private static void SaveToPdf(List<string> nInputFileNames, string nOutputFileName, string nTempPath)
        {
            //var tempFileName = nTempPath.TrimEnd('\\') + "\\" + Guid.NewGuid();
            //var folios = SaveToImage(nInputFileNames, tempFileName + ".jpg", "-0000", EnumFormat.Jpeg, FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYNORMAL);
            //CreatePdf(folios, tempFileName, nOutputFileName);
            CreatePdf(nInputFileNames, nOutputFileName);
        }


        private static Dictionary<string, string> formSettings;
        private Thread threadOcr;

        private readonly GdPicturePDF _nativePdf = new GdPicturePDF();
        string inputpath;

        private void OcrPagesDone(GdPictureStatus status)
        {
            if (status == GdPictureStatus.OK)
            {
                string outputFilePath = inputpath.Substring(0, inputpath.Length - 4) + "_ocr.pdf";

                //if (chkIncSave.Checked)
                //    status = _nativePdf.SaveToFileInc(outputFilePath);
                //else
                status = _nativePdf.SaveToFile(outputFilePath, true);

            }

            _nativePdf.CloseDocument();

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputFileNames"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nTempPath"></param>
        /// <returns></returns>

        public static void SaveToPdfA(string nInputPdfFileName, string nOutputFileName, string nPathTemporal)
        {
            LicenseManager oLicenseManager = new LicenseManager();
            oLicenseManager.RegisterKEY("0496356687192237158421272");
            string txtDictsPath = "\\Dlls\\GdPicture.NET 12\\Redist\\OCR";

            //string txtDictsPath = nPathTemporal.Replace(nPathTemporal, "\\Dlls\\GdPicture.NET 12Dlls\\GdPicture.NET 12\\Redist\\OCR");

            //string txtDictsPath = "C:\\GdPicture.NET 12\\Redist\\OCR\\";
            //"D:\\GdPicture.NET 12\\Redist\\OCR\\";--> ejecutar cuando se publica y copiar la carpeta GdPicture.NET 12 en Disco Local D

            //oLicenseManager.GetRedistPath() + "OCR\\";---->ejecutar cuando se prueba en codigo

            //nPathTemporal.Replace(nPathTemporal,  "\\Dlls\\GdPicture.NET 12Dlls\\GdPicture.NET 12\\Redist\\OCR"); PUBLICACION A CERTIFICACION Y PRODUCCION
            //nPathTemporal.Replace(nPathTemporal, "D:\\Proyectos\\Miharu\\Miharu+ System - Plugins\\Miharu+ System - Tools\\Slyg.Tools.Imaging\\Dlls\\GdPicture.NET 12Dlls\\GdPicture.NET 12\\Redist\\OCR");



            GdPicturePDF oGdPicturePDF = new GdPicturePDF();
            GdPictureStatus status = oGdPicturePDF.LoadFromFile(nInputPdfFileName, false);
            if (status == GdPictureStatus.OK)
            {
                try
                {
                    int pageCount = oGdPicturePDF.GetPageCount();

                    for (int i = 1; i <= pageCount; i++)
                    {

                        oGdPicturePDF.SelectPage(i);
                        if (status == GdPictureStatus.OK)
                        {
                            oGdPicturePDF.OcrPage("eng", txtDictsPath, "", 300);
                        }
                    }
                    oGdPicturePDF.EnableCompression(true);
                    status = oGdPicturePDF.SaveToFile(nOutputFileName, true);

                    if (status != GdPictureStatus.OK)
                    {
                        CrearLog(nOutputFileName, "Error en generando OCR:" + status, nPathTemporal);
                    }

                    oGdPicturePDF.CloseDocument();
                }
                catch (Exception e)
                {

                    throw new Exception("Error:" + e.Message.ToString());
                }

            }

            oGdPicturePDF.Dispose();

        }

        /// <summary>
        /// Crea Log Para PDFA con OCR
        /// </summary>
        /// <param name="nInputFileNames"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nSuffixFormat"></param>
        /// <param name="nTempPath"></param>

        public static void CrearLog(string nOutputFileName, string mensaje, string nPathTemporal)
        {
            if (!Directory.Exists(nPathTemporal))
                Directory.CreateDirectory(nPathTemporal);

            string filepath = nPathTemporal + "LogOCR" + "_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            string logMessage = mensaje;

            if (!File.Exists(filepath))
            {
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(logMessage);
                }
            }
            else
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(logMessage);
                }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nInputFileNames"></param>
        /// <param name="nOutputFileName"></param>
        /// <param name="nSuffixFormat"></param>
        /// <param name="nTempPath"></param>
        /// <returns></returns>
        private static void SaveToPdf(List<string> nInputFileNames, string nOutputFileName, string nSuffixFormat, string nTempPath)
        {
            //var tempFileName = nTempPath.TrimEnd('\\') + "\\" + Guid.NewGuid();
            //var folios = SaveToImage(nInputFileNames, tempFileName + ".jpg", "-0000", EnumFormat.Jpeg);
            //CreatePdf(folios, tempFileName, nOutputFileName, nSuffixFormat);
            CreatePdf(nInputFileNames, nOutputFileName, nSuffixFormat);
        }

        /// <summary>
        /// Crea un archivo PDF multipágina a partir de las imágenes temporales creadas por otros métodos
        /// </summary>
        /// <param name="nFolios">Número de imágenes temporales</param>
        /// <param name="nTempFileName">Nombre base de las imágenes temporales</param>
        /// <param name="nOutputFileName">Nombre del PDF a crear</param>
        public static void CreatePdf(int nFolios, string nTempFileName, string nOutputFileName)
        {
#if DEBUG
            try
            {
#endif
                var project = new Gios.PDF.SplitMerge.PdfProject();

                for (var folio = 1; folio <= nFolios; folio++)
                {
                    // Por el contrario, debe ser un archivo de Imagen
                    var newElement = (Gios.PDF.SplitMerge.PdfProjectElement)Activator.CreateInstance(typeof(Gios.PDF.SplitMerge.PdfProjectImage), new object[] { nTempFileName + folio.ToString("-0000") + ".jpg" });
                    newElement.Analyze();
                    project.Elements.Add(newElement);
                }

                project.Target = nOutputFileName;

                using (var s = new FileStream(project.TempTarget, FileMode.Create, FileAccess.Write))
                {
                    var merger = new Gios.PDF.SplitMerge.PdfMerger(s);

                    foreach (Gios.PDF.SplitMerge.PdfProjectElement newElement in project.Elements)
                    {
                        if (merger.CancelPending) return;
                        if (newElement.Enabled) newElement.AddToMerger(merger);
                    }

                    merger.Finish();
                }

                File.Copy(project.TempTarget, project.Target, true);
                File.Delete(project.TempTarget);

                for (var folio = 1; folio <= nFolios; folio++)
                {
                    File.Delete(nTempFileName + folio.ToString("-0000") + ".jpg");
                }
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("CreatePDF 1 - " + ex.Message);
            }
#endif
        }

        /// <summary>
        /// Crea un archivo PDF por cada imágen a partir de las imágenes temporales creadas por otros métodos
        /// </summary>
        /// <param name="nFolios">Número de imágenes temporales</param>
        /// <param name="nTempFileName">Nombre base de las imágenes temporales</param>
        /// <param name="nOutputFileName">Nombre del PDF a crear</param>
        /// <param name="nSuffixFormat">Formato del consecutivo del nombre de la imagen almacenada</param>
        private static void CreatePdf(int nFolios, string nTempFileName, string nOutputFileName, string nSuffixFormat)
        {
#if DEBUG
            try
            {
#endif
                for (var folio = 1; folio <= nFolios; folio++)
                {
                    var project = new Gios.PDF.SplitMerge.PdfProject();

                    var newElement = (Gios.PDF.SplitMerge.PdfProjectElement)Activator.CreateInstance(typeof(Gios.PDF.SplitMerge.PdfProjectImage), new object[] { nTempFileName + folio.ToString("-0000") + ".jpg" });
                    newElement.Analyze();
                    project.Elements.Add(newElement);

                    project.Target = nOutputFileName.TrimEnd(".pdf".ToCharArray()) + folio.ToString(nSuffixFormat) + ".pdf";

                    using (var s = new FileStream(project.TempTarget, FileMode.Create, FileAccess.Write))
                    {
                        var merger = new Gios.PDF.SplitMerge.PdfMerger(s);

                        foreach (var element in project.Elements)
                        {
                            if (merger.CancelPending) return;
                            if (element.Enabled) element.AddToMerger(merger);
                        }

                        merger.Finish();
                    }

                    File.Copy(project.TempTarget, project.Target, true);
                    File.Delete(project.TempTarget);
                }

                for (var folio = 1; folio <= nFolios; folio++)
                {
                    File.Delete(nTempFileName + folio.ToString("-0000") + ".jpg");
                }
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("CreatePDF 2 - " + ex.Message);
            }
#endif
        }

        private static void CreatePdf(string nInputFileName, string nOutputFileName)
        {
#if DEBUG
            try
            {
#endif
                
                PdfSharp.Pdf.PdfDocument docPdf = new PdfSharp.Pdf.PdfDocument();
                PdfSharp.Pdf.PdfPage page = new PdfSharp.Pdf.PdfPage();
                //using (System.Drawing.Image img = System.Drawing.Image.FromFile(nInputFileName.ToString()))
                using (FileStream fs = new FileStream(nInputFileName.ToString(), FileMode.Open, FileAccess.ReadWrite)) 
                {
                    using (System.Drawing.Image img = System.Drawing.Image.FromStream(fs,true))
                    {
                    page.Width = img.Width;
                    page.Height = img.Height;
                    docPdf.Pages.Add(page);
                    XGraphics xgr = XGraphics.FromPdfPage(docPdf.Pages[0]);
                    xgr.DrawImage(img, 0, 0);
                     }
                }
                docPdf.Save(nOutputFileName);
                docPdf.Close();
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("CreatePDF 3 - " + ex.Message);
            }
#endif
        }

        private static string Create_PDF(List<string> nInputFileNames, string nOutputFileName)
        {
            iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 0, 0, 0, 0);

            iTextSharp.text.pdf.PdfWriter pdfw = iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(nOutputFileName, System.IO.FileMode.Create));
            //pdfw.PDFXConformance = PdfWriter.PDFA1B;
            document.Open();
            foreach (var item in nInputFileNames)
            {
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(item);



                int total = bmp.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page);
                iTextSharp.text.pdf.PdfContentByte cb = pdfw.DirectContent;
                for (int k = 0; k < total; ++k)
                {
                    bmp.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, k);
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(bmp, System.Drawing.Imaging.ImageFormat.Bmp);

                    // Scale the image to fit in the page
                    float percentage = 0.0f;
                    percentage = 620 / img.Width;
                    img.ScalePercent(percentage * 100);
                    //img.ScalePercent(22f / img.DpiX * 100);
                    img.SetAbsolutePosition(0, 0);
                    cb.AddImage(img);
                    document.NewPage();
                }

            }
            pdfw.CreateXmpMetadata();
            document.Close();
            return nOutputFileName;

        }

        private static string CreatePDFA1B(List<string> nInputFileNames, string nOutputFileName)
        {
            iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 0, 0, 0, 0);

            iTextSharp.text.pdf.PdfWriter pdfw = iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(nOutputFileName, System.IO.FileMode.Create));
            pdfw.PDFXConformance = PdfWriter.PDFA1B;
            document.Open();
            foreach (var item in nInputFileNames)
            {
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(item);



                int total = bmp.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page);
                iTextSharp.text.pdf.PdfContentByte cb = pdfw.DirectContent;
                for (int k = 0; k < total; ++k)
                {
                    bmp.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, k);
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(bmp, System.Drawing.Imaging.ImageFormat.Bmp);

                    // Scale the image to fit in the page
                    float percentage = 0.0f;
                    percentage = 620 / img.Width;
                    img.ScalePercent(percentage * 100);
                    //img.ScalePercent(22f / img.DpiX * 100);
                    img.SetAbsolutePosition(0, 0);
                    cb.AddImage(img);
                    document.NewPage();
                }

            }
            pdfw.CreateXmpMetadata();
            document.Close();
            return nOutputFileName;

        }

        private static string CreatePdfA(List<string> nInputFileNames, string nOutputFileName)
        {
            try
            {
                PdfSharp.Pdf.PdfDocument docPdf = new PdfSharp.Pdf.PdfDocument();
                for (int i = 0; i < nInputFileNames.Count; i++)
                {
                    PdfSharp.Pdf.PdfPage page = new PdfSharp.Pdf.PdfPage();
                    using (System.Drawing.Image img = System.Drawing.Image.FromFile(nInputFileNames[i].ToString()))
                    {
                        page.Width = img.Width;
                        page.Height = img.Height;
                        docPdf.Pages.Add(page);
                        XGraphics xgr = XGraphics.FromPdfPage(docPdf.Pages[i]);
                        xgr.DrawImage(img, 0, 0);
                    }
                }
                docPdf.Save(nOutputFileName);
                docPdf.Close();
                return nOutputFileName;
            }
            catch (Exception ex)
            {
                throw new Exception("CreatePDFA - " + ex.Message);
            }
        }

        private static void CreatePdf(List<string> nInputFileNames, string nOutputFileName)
        {
            try
            {
                PdfSharp.Pdf.PdfDocument docPdf = new PdfSharp.Pdf.PdfDocument();
                for (int i = 0; i < nInputFileNames.Count; i++)
                {
                    PdfSharp.Pdf.PdfPage page = new PdfSharp.Pdf.PdfPage();
                    using (System.Drawing.Image img = System.Drawing.Image.FromFile(nInputFileNames[i].ToString()))
                    {
                        page.Width = img.Width;
                        page.Height = img.Height;
                        docPdf.Pages.Add(page);
                        XGraphics xgr = XGraphics.FromPdfPage(docPdf.Pages[i]);
                        xgr.DrawImage(img, 0, 0);
                    }

                }
                docPdf.Save(nOutputFileName);
                docPdf.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("CreatePDF 4 - " + ex.Message);
            }

        }

        private static void CreatePdf(List<string> nInputFileNames, string nOutputFileName, string nSuffixFormat)
        {
            try
            {
                for (int i = 0; i < nInputFileNames.Count; i++)
                {
                    PdfSharp.Pdf.PdfDocument docPdf = new PdfSharp.Pdf.PdfDocument();
                    PdfSharp.Pdf.PdfPage page = new PdfSharp.Pdf.PdfPage();
                    String output = nOutputFileName.TrimEnd(".pdf".ToCharArray()) + (i + 1).ToString(nSuffixFormat) + ".pdf";
                    using (System.Drawing.Image img = System.Drawing.Image.FromFile(nInputFileNames[i].ToString()))
                    {
                        page.Width = img.Width;
                        page.Height = img.Height;
                        docPdf.Pages.Add(page);
                        XGraphics xgr = XGraphics.FromPdfPage(docPdf.Pages[0]);
                        xgr.DrawImage(img, 0, 0);
                        docPdf.Save(output);
                        docPdf.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("CreatePDF 5 - " + ex.Message);
            }
        }

        #endregion

        #region Folios

        public static int GetFolios(FreeImageAPI.FreeImageBitmap imagen)
        {
            return imagen.FrameCount;
        }

        public static int GetFolios(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            if (extension != null) extension = extension.ToLower();
            switch (extension)
            {
                case ".tif":
                    return GetFoliosTiff(fileName);

                case ".pdf":
                    return GetFoliosPdf(fileName);

                default:
                    return 1;
            }
        }

        private static int GetFoliosPdf(string nFileName)
        {
#if DEBUG
            try
            {
#endif
                //return (short)getNumberOfPdfPages(nFileName); 

                iTextSharp.text.pdf.PdfReader PdfReader = new iTextSharp.text.pdf.PdfReader(nFileName);

                return PdfReader.NumberOfPages;

                //return PDFLibrary.iTextSharpUtil.GetPDFPageCount(nFileName);

#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("GetFoliosPDF - " + ex.Message);
            }
#endif
        }

        private static int GetFoliosTiff(string nFileName)
        {
#if DEBUG
            try
            {
#endif
                using (var imagen = new FreeImageAPI.FreeImageBitmap(nFileName))
                {
                    return GetFolios(imagen);
                }
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("GetFoliosTIFF - " + ex.Message);
            }
#endif
        }

        #endregion

        #region Folio

        /// <summary>
        ///         
        /// </summary>
        /// <param name="nImagen"></param>
        /// <param name="nFolio"></param>
        /// <returns></returns>
        public static FreeImageAPI.FreeImageBitmap GetFolioBitmap(FreeImageAPI.FreeImageBitmap nImagen, int nFolio)
        {
#if DEBUG
            try
            {
#endif
                if (nFolio > 1 || nImagen.FrameCount > 1)
                {
                    // Crea un objeto mapa de bits a partir de la imagen
                    nImagen.SelectActiveFrame(nFolio - 1);
                    return new FreeImageAPI.FreeImageBitmap(nImagen);
                }
                return new FreeImageAPI.FreeImageBitmap(nImagen);
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("GetFolio 1 - " + ex.Message);
            }
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nFileName"></param>
        /// <param name="nFolio"></param>
        /// <returns></returns>
        public static FreeImageAPI.FreeImageBitmap GetFolioBitmap(string nFileName, int nFolio)
        {
#if DEBUG
            try
            {
#endif
                var extension = Path.GetExtension(nFileName);
                if (extension != null) extension = extension.ToLower();

                switch (extension)
                {
                    case ".pdf":

                        return GetFolioBitmapFromPdf(nFileName, nFolio);

                    default:
                        using (var imagen = new FreeImageAPI.FreeImageBitmap(nFileName))
                        {
                            return GetFolioBitmap(imagen, nFolio);
                        }
                }
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("GetFolio 2 - " + ex.Message);
            }
#endif
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="nFileName"></param>
        /// <param name="nFolio"></param>
        /// <param name="nFormat"></param>
        /// <param name="nCompression"></param>
        /// <returns></returns>
        public static byte[] GetFolioData(string nFileName, int nFolio, EnumFormat nFormat, EnumCompression nCompression)
        {
            switch (nFormat)
            {
                case EnumFormat.Tiff:
                    return GetFolioDataTiff(nFileName, nFolio, nCompression);

                case EnumFormat.Pdf:
                    return GetFolioDataPdf(nFileName, nFolio);

                default:
                    return GetFolioDataImage(nFileName, nFolio, nFormat);
            }
        }

        public static byte[] GetFolioDataPdfOnly(string nFileName, int nFolio, EnumFormat nFormat, EnumCompression nCompression, FreeImageAPI.FREE_IMAGE_SAVE_FLAGS nFlags)
        {
            return GetFolioDataPdf(nFileName, nFolio, nFormat, nCompression, nFlags);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nImagen"></param>
        /// <param name="nFolio"></param>
        /// <param name="nResolucion"></param>
        /// <param name="nFormat"></param>
        /// <param name="nCompression"></param>
        /// <returns></returns>
        public static byte[] GetFolioData(FreeImageAPI.FreeImageBitmap nImagen, int nFolio, float nResolucion, EnumFormat nFormat, EnumCompression nCompression)
        {
            using (FreeImageAPI.FreeImageBitmap folio = GetFolioBitmap(nImagen, nFolio))
            {
                switch (nFormat)
                {
                    case EnumFormat.Tiff:
                        return GetFolioDataTiff(folio, nCompression);

                    case EnumFormat.Pdf:
                        return GetFolioDataPdf(folio);

                    default:
                        return GetFolioDataImage(folio, nFormat);
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="nFileName"></param>
        /// <param name="nFolio"></param>
        /// <param name="nFormat"></param>
        /// <returns></returns>
        private static byte[] GetFolioDataImage(string nFileName, int nFolio, EnumFormat nFormat)
        {
#if DEBUG
            try
            {
#endif
                using (var imagen = GetFolioBitmap(nFileName, nFolio))
                {
                    return GetFolioDataImage(imagen, nFormat);
                }
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("GetFolioImage 1 - " + ex.Message);
            }
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nImagen"></param>        
        /// <param name="nFormat"></param>
        /// <returns></returns>
        private static byte[] GetFolioDataImage(FreeImageAPI.FreeImageBitmap nImagen, EnumFormat nFormat)
        {
#if DEBUG
            try
            {
#endif
                //using (var newImage = nResolucion == 1.0 ? nImagen : new FreeImageAPI.FreeImageBitmap(new System.Drawing.Bitmap((System.Drawing.Bitmap)nImagen, (int)(nImagen.Width * nResolucion), (int)(nImagen.Height * nResolucion))))
                using (var newImage = nImagen)
                {
                    using (var myMemoryStream = new MemoryStream())
                    {
                        newImage.Save(myMemoryStream, GetImageFormat(nFormat));
                        return myMemoryStream.GetBuffer();
                    }
                }
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("GetFolioImage 2 - " + ex.Message);
            }
#endif
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="nFileName"></param>
        /// <param name="nFolio"></param>
        /// <param name="nCompression"></param>
        /// <returns></returns>
        private static byte[] GetFolioDataTiff(string nFileName, int nFolio, EnumCompression nCompression)
        {
#if DEBUG
            try
            {
#endif
                var compresion = GetFreeImageCompression(nCompression);

                using (var fBitmap = new FreeImageAPI.FreeImageBitmap(nFileName))
                {
                    fBitmap.SelectActiveFrame(nFolio - 1);

                    var fiBitmap = FreeImageAPI.FreeImage.CreateFromBitmap((System.Drawing.Bitmap)fBitmap);
                    var dataStream = new MemoryStream();
                    FreeImageAPI.FreeImage.SaveToStream(fiBitmap, dataStream, FreeImageAPI.FREE_IMAGE_FORMAT.FIF_TIFF, compresion);
                    FreeImageAPI.FreeImage.Unload(fiBitmap);
                    return dataStream.GetBuffer();
                }
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("GetFolioTIFF 1 - " + ex.Message);
            }
#endif
        }

        /// <summary>
        /// 
        /// </summary>        
        /// <param name="nImage"></param>
        /// <param name="nCompression"></param>
        /// <returns></returns>
        private static byte[] GetFolioDataTiff(FreeImageAPI.FreeImageBitmap nImage, EnumCompression nCompression)
        {
#if DEBUG
            try
            {
#endif
                var compresion = GetFreeImageCompression(nCompression);

                var fiBitmap = FreeImageAPI.FreeImage.CreateFromBitmap((System.Drawing.Bitmap)nImage);
                var dataStream = new MemoryStream();
                FreeImageAPI.FreeImage.SaveToStream(fiBitmap, dataStream, FreeImageAPI.FREE_IMAGE_FORMAT.FIF_TIFF, compresion);
                FreeImageAPI.FreeImage.Unload(fiBitmap);
                return dataStream.GetBuffer();
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("GetFolioTIFF 2 - " + ex.Message);
            }
#endif
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="nFileName"></param>
        /// <param name="nFolio"></param>        
        /// <returns></returns>
        private static byte[] GetFolioDataPdf(string nFileName, int nFolio)
        {
            using (var newImage = GetFolioBitmap(nFileName, nFolio))
            {
                var tempFileNameJpg = Path.GetTempFileName();
                var tempFileNamePdf = Path.GetTempFileName();

                File.Delete(tempFileNameJpg);
                File.Delete(tempFileNamePdf);

                tempFileNameJpg = Path.ChangeExtension(tempFileNameJpg, ".jpg");
                tempFileNamePdf = Path.ChangeExtension(tempFileNamePdf, ".pdf");

                newImage.Save(tempFileNameJpg, FreeImageAPI.FREE_IMAGE_FORMAT.FIF_JPEG, FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYGOOD);
                newImage.Dispose();

                CreatePdf(tempFileNameJpg, tempFileNamePdf);

                File.Delete(tempFileNameJpg);

                using (var stream = new FileStream(tempFileNamePdf, FileMode.Open, FileAccess.Read))
                {
                    var data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);
                    stream.Close();

                    File.Delete(tempFileNamePdf);

                    return data;
                }
            }
        }

        private static byte[] GetFolioDataPdf(string nFileName, int nFolio, EnumFormat format, EnumCompression Compresion, FreeImageAPI.FREE_IMAGE_SAVE_FLAGS flags)
        {
            using (var newImage = GetFolioBitmap(nFileName, nFolio))
            {

                double resizeratio = 1;


                if (newImage.Size.Height > newImage.Size.Width)
                {
                    resizeratio = 1600 / (double)newImage.Size.Height;
                }
                else
                {
                    resizeratio = 1600 / (double)newImage.Size.Width;
                }


                var nSize = new System.Drawing.Size((int)(newImage.Size.Width * resizeratio), (int)(newImage.Size.Height * resizeratio));
                newImage.Rescale(nSize, FreeImageAPI.FREE_IMAGE_FILTER.FILTER_BICUBIC);
                newImage.SetResolution(200, 200);


                var compresion = GetFreeImageCompression(Compresion);

                using (var fBitmap = new FreeImageAPI.FreeImageBitmap(newImage))
                {
                    fBitmap.SelectActiveFrame(0);

                    var fiBitmap = FreeImageAPI.FreeImage.CreateFromBitmap((System.Drawing.Bitmap)fBitmap);
                    var dataStream = new MemoryStream();
                    if (format == EnumFormat.Tiff)
                    {
                        FreeImageAPI.FreeImage.SaveToStream(fiBitmap, dataStream, GetImageFormat(format), compresion);
                    }
                    else
                    {
                        FreeImageAPI.FreeImage.SaveToStream(fiBitmap, dataStream, GetImageFormat(format), flags);//compresion);  
                    }

                    FreeImageAPI.FreeImage.Unload(fiBitmap);
                    return dataStream.GetBuffer();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nImagen"></param>
        /// <returns></returns>
        private static byte[] GetFolioDataPdf(FreeImageAPI.FreeImageBitmap nImagen)
        {
            var tempFileNameJpg = Path.GetTempFileName();
            var tempFileNamePdf = Path.GetTempFileName();

            //se agregan los formatos correspondientes
            tempFileNameJpg = Path.ChangeExtension(tempFileNameJpg, ".jpg");
            tempFileNamePdf = Path.ChangeExtension(tempFileNamePdf, ".pdf");

            nImagen.Save(tempFileNameJpg, FreeImageAPI.FREE_IMAGE_FORMAT.FIF_JPEG);

            CreatePdf(tempFileNameJpg, tempFileNamePdf);

            File.Delete(tempFileNameJpg);

            using (var stream = new FileStream(tempFileNamePdf, FileMode.Open, FileAccess.Read))
            {
                var data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                stream.Close();

                File.Delete(tempFileNamePdf);

                return data;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nFileName"></param>
        /// <param name="nFolio"></param>
        /// <returns></returns>
        private static FreeImageAPI.FreeImageBitmap GetFolioBitmapFromPdf(string nFileName, int nFolio)
        {
            return new FreeImageAPI.FreeImageBitmap(PDFLibrary.PDFConvert.GetPageFromPDF(nFileName, nFolio));
        }

        #endregion

        #region Data

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nFileName"></param>
        /// <returns></returns>
        public static byte[] GetData(string nFileName)
        {
#if DEBUG
            try
            {
#endif
                using (var fsInput = new FileStream(nFileName, FileMode.Open, FileAccess.Read))
                {
                    var longitud = (int)(fsInput.Length);
                    var data = new byte[longitud];

                    fsInput.Read(data, 0, data.Length);
                    fsInput.Close();

                    return data;
                }
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("GetData- " + ex.Message);
            }
#endif
        }

        #endregion

        #region Thumbnail

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nImage"></param>
        /// <param name="nFolio"></param>
        /// <param name="nAncho"></param>
        /// <param name="nAlto"></param>
        /// <returns></returns>
        public static FreeImageAPI.FreeImageBitmap GetThumbnailBitmap(FreeImageAPI.FreeImageBitmap nImage, int nFolio, int nAncho, int nAlto)
        {
            using (var folio = GetFolioBitmap(nImage, nFolio))
            {
#if DEBUG
                try
                {
#endif
                    System.Drawing.Image.GetThumbnailImageAbort myCallback = ThumbnailCallback;
                    return new FreeImageAPI.FreeImageBitmap(folio.GetThumbnailImage(nAncho, nAlto, myCallback, IntPtr.Zero));
#if DEBUG
                }
                catch (Exception ex)
                {
                    throw new Exception("GetThumbnail 1 - " + ex.Message);
                }
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nImage"></param>
        /// <param name="nFolioFinal"></param>
        /// <param name="nAncho"></param>
        /// <param name="nAlto"></param>
        /// <param name="nFolioInicial"></param>
        /// <returns></returns>
        public static List<byte[]> GetThumbnailData(FreeImageAPI.FreeImageBitmap nImage, int nFolioInicial, int nFolioFinal, int nAncho, int nAlto)
        {
#if DEBUG
            try
            {
#endif
                var imagenes = new List<byte[]>();

                for (var folio = nFolioInicial; folio <= nFolioFinal; folio++)
                {
                    using (FreeImageAPI.FreeImageBitmap folioImagen = GetFolioBitmap(nImage, folio))
                    {
                        using (FreeImageAPI.FreeImageBitmap imagenThumbnail = GetThumbnailBitmap(folioImagen, nFolioInicial, nAncho, nAlto))
                        {
                            imagenes.Add(GetFolioDataImage(imagenThumbnail, EnumFormat.Gif));
                        }
                    }
                }

                return imagenes;
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("GetThumbnailImage - " + ex.Message);
            }
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nFileName"></param>
        /// <param name="nFolioInicial"></param>
        /// <param name="nFolioFinal"></param>
        /// <param name="nAncho"></param>
        /// <param name="nAlto"></param>
        /// <returns></returns>
        public static List<byte[]> GetThumbnailData(string nFileName, int nFolioInicial, int nFolioFinal, int nAncho, int nAlto)
        {
            // ReSharper disable once PossibleNullReferenceException
            var extension = Path.GetExtension(nFileName).ToLower();

            switch (extension)
            {
                case ".tif":
                    return GetThumbnailDataFromTiff(nFileName, nFolioInicial, nFolioFinal, nAncho, nAlto);

                case ".pdf":
                    return GetThumbnailDataFromPdf(nFileName, 1, 1, nAncho, nAlto);

                default:
                    return GetThumbnailDataFromImage(nFileName, nFolioInicial, nAncho, nAlto);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="nFileName"></param>
        /// <param name="nFolio"></param>
        /// <param name="nAncho"></param>
        /// <param name="nAlto"></param>
        /// <returns></returns>
        private static List<byte[]> GetThumbnailDataFromImage(string nFileName, int nFolio, int nAncho, int nAlto)
        {
#if DEBUG
            try
            {
#endif
                using (var imagen = new FreeImageAPI.FreeImageBitmap(nFileName))
                {
                    var imagenes = new List<byte[]>();

                    using (var imagenThumbnail = GetThumbnailBitmap(imagen, nFolio, nAncho, nAlto))
                    {
                        imagenes.Add(GetFolioDataImage(imagenThumbnail, EnumFormat.Gif));
                    }

                    return imagenes;
                }
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("GetThumbnailImage - " + ex.Message);
            }
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nFileName"></param>
        /// <param name="nFolioInicial"></param>
        /// <param name="nFolioFinal"></param>
        /// <param name="nAncho"></param>
        /// <param name="nAlto"></param>
        /// <returns></returns>
        private static List<byte[]> GetThumbnailDataFromTiff(string nFileName, int nFolioInicial, int nFolioFinal, int nAncho, int nAlto)
        {
#if DEBUG
            try
            {
#endif
                using (var fBitmap = new System.Drawing.Bitmap(nFileName))
                {
                    var lista = new List<byte[]>();

                    for (int i = nFolioInicial; i <= nFolioFinal; i++)
                    {
                        fBitmap.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, i - 1);

                        var callback = new IntPtr();

                        //var a = new System.Drawing.Bitmap(nFileName);
                        //var b = a.GetThumbnailImage(nAncho, nAlto, ThumbnailCallback, Callback);

                        var fBitmapT = fBitmap.GetThumbnailImage(nAncho, nAlto, ThumbnailCallback, callback);
                        var fiBitmapT = FreeImageAPI.FreeImage.CreateFromBitmap((System.Drawing.Bitmap)fBitmapT);

                        var dataStream = new MemoryStream();
                        FreeImageAPI.FreeImage.SaveToStream(fiBitmapT, dataStream, FreeImageAPI.FREE_IMAGE_FORMAT.FIF_GIF, FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.DEFAULT);

                        lista.Add(dataStream.GetBuffer());
                    }

                    return lista;
                }
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("GetThumbnailTIFF - " + ex.Message);
            }
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nFileName"></param>
        /// <param name="nFolioInicial"></param>
        /// <param name="nFolioFinal"></param>
        /// <param name="nAncho"></param>
        /// <param name="nAlto"></param>
        /// <returns></returns>
        private static List<byte[]> GetThumbnailDataFromPdf(string nFileName, int nFolioInicial, int nFolioFinal, int nAncho, int nAlto)
        {
#if DEBUG
            try
            {
#endif
                var imagenes = new List<byte[]>();

                for (var folio = nFolioInicial; folio <= nFolioFinal; folio++)
                {
                    using (var imagen = new FreeImageAPI.FreeImageBitmap(PDFLibrary.PDFConvert.GetPageFromPDF(nFileName, folio)))
                    {
                        using (var imagenThumbnail = GetThumbnailBitmap(imagen, nFolioInicial, nAncho, nAlto))
                        {
                            imagenes.Add(GetFolioDataImage(imagenThumbnail, EnumFormat.Gif));
                        }
                    }
                }

                return imagenes;
#if DEBUG
            }
            catch (Exception ex)
            {
                throw new Exception("GetThumbnailPDF - " + ex.Message);
            }
#endif
        }

        #endregion

        #region Auxiliares

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nFormat"></param>
        /// <returns></returns>
        public static string GetExtension(EnumFormat nFormat)
        {
            switch (nFormat)
            {
                case EnumFormat.Bmp: return ".bmp";
                case EnumFormat.Gif: return ".gif";
                case EnumFormat.Jpeg: return ".jpg";
                case EnumFormat.Pdf: return ".pdf";
                case EnumFormat.Png: return ".png";
                case EnumFormat.Tiff: return ".tif";
                default: throw new Exception("Formato no válido: " + Enum.GetName(typeof(EnumFormat), nFormat));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nFileName"></param>
        /// <returns></returns>
        public static EnumFormat GetEnumFormat(string nFileName)
        {
            // ReSharper disable once PossibleNullReferenceException
            var extension = Path.GetExtension(nFileName).ToLower();

            switch (extension)
            {
                case ".bmp": return EnumFormat.Bmp;
                case ".gif": return EnumFormat.Gif;
                case ".jpg": return EnumFormat.Jpeg;
                case ".pdf": return EnumFormat.Pdf;
                case ".png": return EnumFormat.Png;
                case ".tif": return EnumFormat.Tiff;
                default: throw new Exception("Extensión no válida: " + extension);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nFormat"></param>
        /// <returns></returns>
        public static FreeImageAPI.FREE_IMAGE_FORMAT GetImageFormat(EnumFormat nFormat)
        {
            switch (nFormat)
            {
                case EnumFormat.Bmp: return FreeImageAPI.FREE_IMAGE_FORMAT.FIF_BMP;
                case EnumFormat.Gif: return FreeImageAPI.FREE_IMAGE_FORMAT.FIF_GIF;
                case EnumFormat.Jpeg: return FreeImageAPI.FREE_IMAGE_FORMAT.FIF_JPEG;
                case EnumFormat.Png: return FreeImageAPI.FREE_IMAGE_FORMAT.FIF_PNG;
                case EnumFormat.Tiff: return FreeImageAPI.FREE_IMAGE_FORMAT.FIF_TIFF;
                default: throw new Exception("Formato no válido: " + Enum.GetName(typeof(EnumFormat), nFormat));
            }
        }

        /// <summary>
        /// Valida si el archivo es un formato de imagen válido
        /// </summary>
        /// <param name="nFileName">Nombre del archivo a validar</param>
        /// <returns></returns>
        public static bool IsValidFile(string nFileName)
        {
            try
            {
                // ReSharper disable once PossibleNullReferenceException
                switch (Path.GetExtension(nFileName).ToLower())
                {
                    case ".bmp":
                    case ".gif":
                    case ".jpg":
                    case ".png":
                    case ".tif":
                        // ReSharper disable once UnusedVariable
                        using (var imagen = new FreeImageAPI.FreeImageBitmap(nFileName))
                        {
                        }
                        break;

                    case ".pdf":
                        GetFoliosPdf(nFileName);
                        break;

                    default:
                        return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida si la data corresponde a un formato de imagen válido
        /// </summary>
        /// <param name="nFileData">Data del archivo a validar</param>
        /// <param name="nFormat">Extensión del archivo a validar (.bmp;.gif;.jpg;.png;.tif;.pdf)</param>
        /// <param name="nTempPath">Directorio temporal donde se creará el archivo para la validación</param>
        /// <returns></returns>
        public static bool IsValidFile(byte[] nFileData, EnumFormat nFormat, string nTempPath)
        {
            switch (nFormat)
            {
                case EnumFormat.Bmp:
                case EnumFormat.Gif:
                case EnumFormat.Jpeg:
                case EnumFormat.Png:
                case EnumFormat.Tiff:
                    try
                    {
                        using (var stream = new MemoryStream(nFileData))
                        {
                            // ReSharper disable once UnusedVariable
                            using (var imagen = new FreeImageAPI.FreeImageBitmap(stream))
                            {
                            }
                        }
                    }
                    catch
                    {
                        return false;
                    }

                    break;

                case EnumFormat.Pdf:
                    var tempFileName = nTempPath.TrimEnd('\\') + Guid.NewGuid() + ".pdf";

                    try
                    {
                        using (var stream = new FileStream(tempFileName, FileMode.Create))
                        {
                            stream.Write(nFileData, 0, (int)stream.Length);
                            stream.Close();
                            GetFoliosPdf(tempFileName);
                        }
                    }
                    catch
                    {
                        return false;
                    }
                    finally
                    {
                        if (File.Exists(tempFileName))
                            File.Delete(tempFileName);
                    }

                    break;

                default:
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nFileName"></param>
        /// <returns></returns>
        public static FreeImageAPI.FreeImageBitmap GetImage(string nFileName)
        {
            return new FreeImageAPI.FreeImageBitmap(nFileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nData"></param>
        /// <returns></returns>
        public static FreeImageAPI.FreeImageBitmap GetImage(byte[] nData)
        {
            return new FreeImageAPI.FreeImageBitmap(new MemoryStream(nData));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static bool ThumbnailCallback()
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nCompression"></param>
        /// <returns></returns>
        private static FreeImageAPI.FREE_IMAGE_SAVE_FLAGS GetFreeImageCompression(EnumCompression nCompression)
        {
            switch (nCompression)
            {
                case EnumCompression.Ccitt3:
                    return FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.TIFF_CCITTFAX3;

                case EnumCompression.Ccitt4:
                    return FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.TIFF_CCITTFAX4;

                case EnumCompression.Lzw:
                    return FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.TIFF_LZW;

                case EnumCompression.Jpeg:
                    return FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.TIFF_JPEG;

                default:
                    return FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.TIFF_NONE;
            }
        }

        private static int getNumberOfPdfPages(string fileName)
        {



            using (StreamReader sr = new StreamReader(File.OpenRead(fileName)))
            {
                Regex regex = new Regex(@"/Type\s*/Page[^s]");
                MatchCollection matches = regex.Matches(sr.ReadToEnd());

                return matches.Count;
            }
        }


        public static Bitmap ResizeImage(Bitmap image, Size size, bool preserveAspectRatio = true)
        {
            int newWidth = 0;
            int newHeight = 0;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = Convert.ToSingle(size.Width) / Convert.ToSingle(originalWidth);
                float percentHeight = Convert.ToSingle(size.Height) / Convert.ToSingle(originalHeight);
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = Convert.ToInt32(originalWidth * percent);
                newHeight = Convert.ToInt32(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            Bitmap newImage = new Bitmap(newWidth, newHeight);
            newImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBilinear;
                graphicsHandle.CompositingQuality = CompositingQuality.HighQuality;
                graphicsHandle.CompositingMode = CompositingMode.SourceCopy;
                graphicsHandle.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphicsHandle.SmoothingMode = SmoothingMode.HighQuality;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);



            }
            return newImage;
        }


        public static Bitmap ComprimirImagen(System.Drawing.Image inputImage, ImageFormat imageFormat, long compression)
        {
            try
            {
                EncoderParameters eps = new EncoderParameters(1);

                eps.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, compression);
                string mimetype = GetMimeType(imageFormat);
                ImageCodecInfo ici = GetEncoderInfo(mimetype);
                var ms = new System.IO.MemoryStream();
                ms.Seek(0, SeekOrigin.Begin);
                ms.Position = 0;
                ms.SetLength(0);
                inputImage.Save(ms, ici, eps);
                inputImage.Dispose();
                return new Bitmap(ms);
            }
            catch (Exception ex)
            {

                throw new Exception(" Error Comprimir Imagen. " + ex.Message);
            }
        }

        public static void ComprimirImagenUMV(string inputFile, string ouputfile, ImageFormat imageFormat, long compression)
        {
            try
            {


                System.Drawing.Image image = System.Drawing.Image.FromFile(inputFile);

                EncoderParameters eps = new EncoderParameters(1);
                eps.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, compression);

                string mimetype = GetMimeType(imageFormat);

                ImageCodecInfo ici = GetEncoderInfoumv(mimetype);

                image.Save(ouputfile, ici, eps);
                eps = null;
                mimetype = null;
                ici = null;
                image.Dispose();
            }
            catch (Exception ex)
            {

                throw new Exception(" Error Comprimir Imagen. " + ex.Message);
            }

        }

        static ImageCodecInfo GetEncoderInfoumv(string mimeType)
        {

            System.Drawing.Imaging.ImageCodecInfo[] encoders;

            encoders = ImageCodecInfo.GetImageEncoders();



            ImageCodecInfo encoder = (from enc in encoders

                                      where enc.MimeType == mimeType

                                      select enc).First();

            return encoder;



        }

        static string GetMimeType(ImageFormat formato)
        {
            if (formato == ImageFormat.Bmp)
                return "image/bmp";
            else if (formato == ImageFormat.Jpeg)
                return "image/jpeg";
            else if (formato == ImageFormat.Gif)
                return "image/gif";
            else if (formato == ImageFormat.Tiff)
                return "image/tiff";
            else if (formato == ImageFormat.Png)
                return "image/png";
            else
                return "image/jpeg";

        }

        static string GetMimeType(string ext)
        {
            switch (ext.ToLower())
            {
                case ".bmp":
                case ".dib":
                case ".rle":
                    return "image/bmp";

                case ".jpg":
                case ".jpeg":
                case ".jpe":
                case ".fif":
                    return "image/jpeg";

                case "gif":
                    return "image/gif";
                case ".tif":
                case ".tiff":
                    return "image/tiff";
                case "png":
                    return "image/png";
                default:
                    return "image/jpeg";
            }
        }

        static ImageCodecInfo GetEncoderInfo(string mimeType)
        {

            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();

            foreach (var encoder in encoders)
            {
                if (encoder.MimeType == mimeType)
                    return encoder;
            }

            return null;

        }

        #endregion
    }
}