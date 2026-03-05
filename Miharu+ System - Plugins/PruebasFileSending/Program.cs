using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
//using Banagrario.Library.WebService;
using System.DirectoryServices;
using System.Text;

namespace Exportador_Acciones_Valores
{
    static class Program
    {
        public static string CnnCore { get { return ConfigurationManager.AppSettings["CnnCore"]; } }

        public static string CnnImaging { get { return ConfigurationManager.AppSettings["CnnImaging"]; } }

        public static string CnnStorage { get { return ConfigurationManager.AppSettings["CnnStorage"]; } }

        public static string CnnBanagrario { get { return ConfigurationManager.AppSettings["CnnBanagrario"]; } }

        public static string CnnTools { get { return ConfigurationManager.AppSettings["CnnTools"]; } }

        public static string AppPath
        {
            get { return System.Windows.Forms.Application.StartupPath.TrimEnd('\\') + "\\"; }
        }

        public static string TempPath = @"temp\";

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //try
            //{
            //    BanagrarioWebService a = new BanagrarioWebService("http://10.64.6.63/Banagrario.WebServiceLDAP/BanagrarioService.asmx");


            //    a.SincronizarLDAP("mrodriguez", "Stefan*73");
            //  //  a.SincronizarLDAP("leydy.fandino", "**Ginebra..18");

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            Application.Run(new FormCanjeTransfer());
            //Application.Run(new FormFileSending());
           //Application.Run(new FormFileProvider());



          //  //var nUser = "Sandra.Cruz";
          //  //var nPassword = "Colombia2014*";
          //  var nUser = "mycastillo";
          //  var nPassword = "Tuplipan*83666";

          ////  DirectoryEntry entry = new DirectoryEntry("LDAP://latam.brinksgbl.com/DC=latam, DC=brinksgbl, DC=com", nUser, nPassword);

          //  DirectoryEntry entry = new DirectoryEntry("LDAP://192.168.81.141/DC=bancoagrario,DC=gov,DC=co", nUser, nPassword);
       
          //  DirectorySearcher searcher = new DirectorySearcher(entry);

          //  searcher.Filter = "samaccountname=" + nUser;
          //  searcher.PropertiesToLoad.Add("memberof");
          //  searcher.PropertiesToLoad.Add("name");
          //  searcher.PropertiesToLoad.Add("mail");
          //  searcher.PropertiesToLoad.Add("streetaddress");
          //  searcher.PropertiesToLoad.Add("telephonenumber");

          //  entry.Dispose();
          //  searcher.Dispose();

          //  try
          //  {
          //      SearchResultCollection results = searcher.FindAll();

          //      MessageBox.Show(results.ToString());

          //      var Grupos = new StringBuilder();

          //      foreach (SearchResult resultados in results)
          //      {
          //          ResultPropertyCollection colProperties = resultados.Properties;

          //          // Grupos
          //          foreach (var value in colProperties["memberof"])
          //          {
          //              string group = value.ToString();
          //              string[] groupParts = group.Split(',');
          //              foreach (string part in groupParts)
          //              {
          //                  if (part.StartsWith("CN="))
          //                      Grupos.AppendLine(part.Replace("CN=", ""));
          //              }
          //          }

          //          if (colProperties["name"].Count > 0)
          //              MessageBox.Show(colProperties["name"][0].ToString());

          //          if (colProperties["mail"].Count > 0)
          //              MessageBox.Show(colProperties["mail"][0].ToString());

          //          if (colProperties["streetaddress"].Count > 0)
          //              MessageBox.Show(colProperties["streetaddress"][0].ToString());

          //          if (colProperties["telephonenumber"].Count > 0)
          //              MessageBox.Show(colProperties["telephonenumber"][0].ToString());
          //      }

          //      MessageBox.Show(Grupos.ToString());
          //  }
          //  catch (System.Runtime.InteropServices.COMException ex)
          //  {
          //      //switch (ex.ErrorCode)
          //      //{
          //      //    case -2147023570:// Usuario o contraseña inválido
          //      //        break;

          //      //    default:
          //            //  throw new Exception("Error LDAP: " + ex.Message, ex);
          //      MessageBox.Show(ex.Message);
          //    //  }
          //  }
          //  catch (Exception ex)
          //  {
          //      MessageBox.Show(ex.Message);
          //  }
        }
    }
}

