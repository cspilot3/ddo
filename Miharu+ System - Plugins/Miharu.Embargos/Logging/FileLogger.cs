using System;
using System.IO;

namespace Miharu.CentralizacionEmbargos.Logging
{
    public class FileLogger
    {
        #region ' Declaraciones '

        private static StreamWriter mswSw = null;
        private static int Instancias;

        #endregion

        #region ' Constructor '

        public FileLogger()
        {
            if (Instancias == 0)
                AbrirFichero();

            Instancias++;
        }

        #endregion


        #region ' Metodos '

        protected void AbrirFichero()
        {
            String NombreFichero = GetNombreFichero();

            if (!(new FileInfo(NombreFichero).Exists))
            {
                if (mswSw != null)
                {
                    mswSw.Close();
                    mswSw.Dispose();
                }
                mswSw = new StreamWriter(NombreFichero, true);
            }
            else
            {
                if (mswSw == null)
                {
                    mswSw = new StreamWriter(NombreFichero, true);
                }
            }
        }

        public void AddErrorEntry(string Mensaje)
        {
            Escribir("ERROR", Mensaje);
        }

        public void AddWarningEntry(string Mensaje)
        {
            Escribir("WARNING", Mensaje);
        }

        public void AddInformationEntry(string Mensaje)
        {
            Escribir("INFORMATION", Mensaje);
        }

        protected void Escribir(string Tipo, string Mensaje)
        {
            lock (this)
            {
                AbrirFichero();
                mswSw.WriteLine("--------------------------------------------------------------");
                mswSw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                mswSw.WriteLine("Mensaje: " + Mensaje);
                mswSw.WriteLine("--------------------------------------------------------------");
                mswSw.WriteLine("");
                mswSw.Flush();
            }
        }
        public void Dispose()
        {
            lock (this)
            {
                if (--Instancias == 0)
                {
                    mswSw.Close();
                    mswSw.Dispose();
                }
            }
        }

        public void WriteErrorLog(string nMessage)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(Embargos.Program.AppDataPath, true))
                {
                    sw.WriteLine("--------------------------------------------------------------");
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    sw.WriteLine("Mensaje: " + nMessage);
                    sw.WriteLine("--------------------------------------------------------------");
                    sw.WriteLine("");
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ' Funciones '

        protected string GetNombreFichero()
        {
            return Embargos.Program.AppDataPath;
        }

        #endregion

    }
}
