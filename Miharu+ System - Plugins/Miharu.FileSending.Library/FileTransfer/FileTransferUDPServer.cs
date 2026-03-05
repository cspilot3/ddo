using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Xml.Serialization;
using System.IO;

namespace Slyg.Tools.Net.FileTransfer
{
    class FileTransferUDPServer
    {
        #region Declaraciones

        private Thread ListenThread;

        private int _Port;

        private FileTransferClient _FileTransferClient;

        #endregion

        #region Constructores

        public FileTransferUDPServer(int nPort, FileTransferClient nFileTransferClient)
        {
            _Port = nPort;
            _FileTransferClient = nFileTransferClient;

            this.ListenThread = new Thread(new ThreadStart(HandleClientComm));
            this.ListenThread.Start();
        }

        #endregion

        #region Metodos

        private void HandleClientComm()
        {
            UdpClient Client = new UdpClient(_Port, AddressFamily.InterNetwork);
            IPEndPoint Sender = new IPEndPoint(IPAddress.Any, _Port);

            Client.DontFragment = false;

            while (true)
            {
                //if (Client.Available > 0)
                //{
                try
                {
                    byte[] DataMessage = Client.Receive(ref Sender);
                    FileTransferMessageUDP Mensaje = ExtractMessage(DataMessage);

                    Procesar(Mensaje);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                //}
                //else
                //{
                //    Thread.Sleep(1000);
                //}
            }
        }

        private FileTransferMessageUDP ExtractMessage(byte[] nDataMessage)
        {
            XmlSerializer fileSerializer = new XmlSerializer(typeof(FileTransferMessageUDP));
            MemoryStream stream = new MemoryStream(nDataMessage);

            // Serialize object
            return (FileTransferMessageUDP)fileSerializer.Deserialize(stream);
        }

        private void Procesar(FileTransferMessageUDP nMensaje)
        {
            if (File.Exists(this._FileTransferClient.DownloadPath + nMensaje.Identificador + ".definition"))
            {
                FileTransferDefinition Definition = FileTransferDefinition.Deserialize(this._FileTransferClient.DownloadPath + nMensaje.Identificador + ".definition");

                if(!Definition.Parts[nMensaje.PartIndex])
                {
                    if (File.Exists(this._FileTransferClient.DownloadPath + nMensaje.Identificador + ".part"))
                    {
                        FileStream Archivo = new FileStream(this._FileTransferClient.DownloadPath + nMensaje.Identificador + ".part", FileMode.Open, FileAccess.Write);
                        //byte[] Buffer = new byte[Archivo.Length];
                        //Archivo.Read(Buffer, 0, (int)Archivo.Length);
                        //Archivo.Close();

                        //Archivo = new FileStream(this._FileTransferClient.DownloadPath + nMensaje.Identificador + ".part", FileMode.Open);
                        BinaryWriter bw = new BinaryWriter(Archivo);

                        int Inicio = Definition.PackageSize * nMensaje.PartIndex;
                        //int Fin = Inicio + nMensaje.Data.Length;

                        //for (int i = Inicio; i < Fin; i++)
                        //{
                        //    Buffer[i] = nMensaje.Data[i - Inicio];
                        //}

                        //Archivo.Write(Buffer, 0, Buffer.Length);

                        bw.Seek(Inicio, SeekOrigin.Begin);
                        bw.Write(nMensaje.Data);

                        bw.Close();
                        Archivo.Close();

                        Definition.Parts[nMensaje.PartIndex] = true;

                        // Si el archivo esta completo se finaliza la transacción
                        if (Definition.IsCompleted())
                        {
                            //File.Delete(this._FileTransferClient.DownloadPath + nMensaje.Identificador + ".definition");
                            File.Move(this._FileTransferClient.DownloadPath + nMensaje.Identificador + ".part", this._FileTransferClient.DownloadPath + Definition.FileName);

                            this._FileTransferClient.SendOKReceiveMessage(Definition);
                        }
                        else
                        {
                            FileTransferDefinition.Serialize(Definition, this._FileTransferClient.DownloadPath + nMensaje.Identificador + ".definition");
                        }
                    }
                }
            }
        }

        #endregion
    }
}