using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Miharu.Data.Bridge.DataRemoteService;

namespace Miharu.Data.Bridge
{
    /// <summary>
    /// Descripción breve de DataBridgeService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class DataBridgeService : System.Web.Services.WebService
    {


        [WebMethod]
        public string DataBridge(string request)
        {
            var webServiceDataRemote = new DataRemoteService.DataRemoteService();
            string response = webServiceDataRemote.dataBridge(request);
            return response;
        }

        [WebMethod]
        public string ImageCount(string request)
        {
            var webServiceDataRemote = new DataRemoteService.DataRemoteService();
            string response = webServiceDataRemote.ImageCount(request);
            return response;
        }

        [WebMethod]
        public string GetFolio(string request)
        {
            var webServiceDataRemote = new DataRemoteService.DataRemoteService();
            string response = webServiceDataRemote.GetFolio(request);
            return response;
        }

        [WebMethod]
        public string SendDataPostRequest(string request)
        {
            var webServiceDataRemote = new DataRemoteService.DataRemoteService();
            string response = webServiceDataRemote.SendDataPostRequest(request);
            return response;
        }

        [WebMethod]
        public string GetFoliosCargueItem(string request)
        {
            var webServiceDataRemote = new DataRemoteService.DataRemoteService();
            string response = webServiceDataRemote.GetFoliosCargueItem(request);
            return response;
        }

        [WebMethod]
        public string GetFolioCargueItem(string request)
        {
            var webServiceDataRemote = new DataRemoteService.DataRemoteService();
            string response = webServiceDataRemote.GetFolioCargueItem(request);
            return response;
        }

        [WebMethod]
        public string GetFoliosFile(string request)
        {
            var webServiceDataRemote = new DataRemoteService.DataRemoteService();
            string response = webServiceDataRemote.GetFoliosFile(request);
            return response;
        }

        [WebMethod]
        public string GetFolioFile(string request)
        {
            var webServiceDataRemote = new DataRemoteService.DataRemoteService();
            string response = webServiceDataRemote.GetFolioFile(request);
            return response;
        }
        
        [WebMethod]
        public string CreateImageFolioManager(string request)
        {
            var webServiceDataRemote = new DataRemoteService.DataRemoteService();
            string response = webServiceDataRemote.CreateImageFolioManager(request);
            return response;
        }
    }
}
