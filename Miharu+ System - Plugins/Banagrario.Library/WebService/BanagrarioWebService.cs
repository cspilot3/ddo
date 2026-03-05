using System;
using System.Collections.Generic;
using Banagrario.Library;
using Banagrario.Library.BanagrarioServiceReference;
using System.Drawing;
using System.IO;

namespace Banagrario.Library.WebService
{
    public class BanagrarioWebService
    {
        #region Declaraciones

        private string _WebServiceURL;
        //private string _ClientIPAddress;

        //private string ClientPrivateKey;
        //private string ClientPublicKey;
        //private string ServerPublicKey;
        private BanagrarioService ServicioWebBanagrario;
        //private string Token;
        //private string Login;
        //private string Password;

        #endregion

        #region Propiedades

        public string WebServiceURL
        {
            get { return _WebServiceURL; }
        }

        //public string ClientIPAddress
        //{
        //    get { return _ClientIPAddress; }
        //}

        #endregion

        #region Metodos

        public BanagrarioWebService(string nWebServiceURL)//, string nClientIPAddress)
        {
            try
            {
                //Crypto.RSA.CrearKeys(out ClientPrivateKey, out ClientPublicKey);

                _WebServiceURL = nWebServiceURL;
                //_ClientIPAddress = nClientIPAddress;
                ServicioWebBanagrario = getBanagrarioServiceReference(_WebServiceURL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        //private void CreateSecureChannel()
        //{
        //    var Respuesta = ServicioWebBanagrario.CreateSecureChannel(ClientPublicKey);

        //    if (Respuesta.Result)
        //    {
        //        ServerPublicKey = Respuesta.ServerPublicKey;
        //        Token = Crypto.RSA.Decrypt(Respuesta.Token, ClientPrivateKey);
        //    }
        //    else
        //    {
        //        throw new Exception("No se pudo crear un canal seguro. " + Respuesta.Message);
        //    }
        //}

        #endregion

        #region Funciones

        private BanagrarioServiceReference.BanagrarioService getBanagrarioServiceReference(string nWebServiceURL)
        {
            //var binding = new System.ServiceModel.BasicHttpBinding();
            //var baseAddress = new System.ServiceModel.EndpointAddress(nWebServiceURL);
            //BanagrarioServiceReference.BanagrarioService ws;

            //binding.Name = "binding1";
            //binding.MaxReceivedMessageSize *= 20;
            //binding.CloseTimeout = new TimeSpan(1, 0, 0);
            //binding.SendTimeout = new TimeSpan(1,0,0);

            BanagrarioServiceReference.BanagrarioService ws = new BanagrarioServiceReference.BanagrarioService();
            ws.Url = nWebServiceURL;
            return ws;
        }

        public string[] getPermisos(string nUser, out int nUserID)
        {
            var Respuesta = ServicioWebBanagrario.getPermisos(nUser);

            if (Respuesta.Result)
                if (Respuesta.Permisos != null)
                {
                    nUserID = Respuesta.UserID;
                    return Respuesta.Permisos;
                }
                else
                {
                    throw new Exception("El usuario no cuenta con permisos para ingresar al módulo");
                }
            else
                throw new Exception(Respuesta.Message);
        }

        public TypeOffice[] getOfficeList()
        {
            var Respuesta = ServicioWebBanagrario.getOfficeList();

            if (Respuesta.Result)
                return Respuesta.Offices;
            else
                throw new Exception(Respuesta.Message);
        }

        public TypeMovement[] getMovementList()
        {
            var Respuesta = ServicioWebBanagrario.getMovementList();

            if (Respuesta.Result)
                return Respuesta.Movements;
            else
                throw new Exception(Respuesta.Message);
        }

        public TypeError[] getErrorList(int nOffice)
        {
            var Respuesta = ServicioWebBanagrario.getErrorList(nOffice);

            if (Respuesta.Result)
                return Respuesta.Errors;
            else
                throw new Exception(Respuesta.Message);
        }

        public short getFolios(Guid nToken)
        {
            var Respuesta = ServicioWebBanagrario.getFolios(nToken);

            if (Respuesta.Result)
                return Respuesta.Folios;
            else
                throw new Exception(Respuesta.Message);
        }

        public Bitmap getFolio(Guid nToken, short nFolio)
        {
            var Respuesta = ServicioWebBanagrario.getFolio(nToken, nFolio);

            if (Respuesta.Result)
                return new Bitmap(new MemoryStream(Respuesta.Data));
            else
                throw new Exception(Respuesta.Message);
        }

        public Guid NewImage(Guid nToken, byte[] nData, int nUsuario, bool nUltimo)
        {
            var Respuesta = ServicioWebBanagrario.NewImage(nToken, nData, nUsuario, nUltimo);

            if (Respuesta.Result)
                return Respuesta.Token;
            else
                throw new Exception(Respuesta.Message);
        }

        public short AddFolio(Guid nToken, byte[] nData, bool nUltimo)
        {
            var Respuesta = ServicioWebBanagrario.AddFolio(nToken, nData, nUltimo);

            if (Respuesta.Result)
                return Respuesta.Folio;
            else
                throw new Exception(Respuesta.Message);
        }


        public string CentroProcesamiento()
        {
            var Respuesta = ServicioWebBanagrario.CentroProcesamiento();

            if (Respuesta.Result)
                return Respuesta.CentroProcesamiento;
            else
                throw new Exception(Respuesta.Message);
        }

        public string SedeProcesamiento()
        {
            var Respuesta = ServicioWebBanagrario.SedeProcesamiento();

            if (Respuesta.Result)
                return Respuesta.SedeProcesamiento;
            else
                throw new Exception(Respuesta.Message);
        }



        #endregion
    }
}