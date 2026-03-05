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
    class FileTransferTCPServer
    {
        #region Declaraciones

        private TcpListener Listener;

        private Thread ListenThread;

        private int _Port;

        private FileTransferClient _FileTransferClient;

        #endregion

        #region Constructores

        public FileTransferTCPServer(int nPort, FileTransferClient nFileTransferClient)
        {
            _Port = nPort;
            _FileTransferClient = nFileTransferClient;

            this.Listener = new TcpListener(IPAddress.Any, nPort);
            this.ListenThread = new Thread(new ThreadStart(ListenForClients));
            this.ListenThread.Start();
        }

        #endregion

        #region Metodos

        private void ListenForClients()
        {
            this.Listener.Start();

            while (true)
            {
                //blocks until a client has connected to the server
                TcpClient client = this.Listener.AcceptTcpClient();

                //create a thread to handle communication 
                //with connected client
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }

        private void HandleClientComm(object nClient)
        {
            TcpClient Client = (TcpClient)nClient;
            NetworkStream ClientStream = Client.GetStream();
            byte[] DataMessage = null;

            while (true)
            {
                byte[] PartialMessage = new byte[4096];
                int BytesRead = 0;

                try
                {
                    // Leer el mensaje                    
                    BytesRead = ClientStream.Read(PartialMessage, 0, 4096);
                }
                catch
                {
                    // Error en la transmisión
                    break;
                }

                if (BytesRead == 0)// El cliente se ha desconectado del servidor
                    break;

                // Concatenar el mensaje
                if (DataMessage == null)
                {
                    DataMessage = new byte[BytesRead];
                    Program.ArrayCopy(ref DataMessage, 0, PartialMessage, 0, BytesRead);
                }
                else
                {
                    byte[] TempArray = new byte[DataMessage.Length + BytesRead];

                    DataMessage.CopyTo(TempArray, 0);
                    Program.ArrayCopy(ref TempArray, DataMessage.Length, PartialMessage, 0, PartialMessage.Length);
                    DataMessage = TempArray;
                }

                if (BytesRead < 4096 || !ClientStream.DataAvailable)// El cliente se ha desconectado del servidor
                    break;
            }

            // Cerrar la conexión
            Client.Close();

            // Procesar el mensaje
            if (DataMessage != null && DataMessage.Length > 0)
            {
                try
                {
                    FileTransferMessageTCP Message = ExtractMessage(DataMessage);

                    ProcessMessage(Message);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    // TODO: Reportar error
                }
            }
        }

        private FileTransferMessageTCP ExtractMessage(byte[] nDataMessage)
        {
            XmlSerializer fileSerializer = new XmlSerializer(typeof(FileTransferMessageTCP));
            MemoryStream stream = new MemoryStream(nDataMessage);

            // Serialize object
            return (FileTransferMessageTCP)fileSerializer.Deserialize(stream);
        }

        private void ProcessMessage(FileTransferMessageTCP nMessage)
        {
            switch (nMessage.Command)
            {
                case FileTransferMessageTCP.Commands.SEND_FILE:
                    ProcessSEND_FILE(new FileTransferMessageTCP.SEND_FILEParameter(nMessage.Parameters));
                    break;

                case FileTransferMessageTCP.Commands.READY:
                    ProcessREADY(new FileTransferMessageTCP.READYParameter(nMessage.Parameters));
                    break;

                case FileTransferMessageTCP.Commands.OKSend:
                    ProcessOKSend(new FileTransferMessageTCP.OKSendParameter(nMessage.Parameters));
                    break;

                case FileTransferMessageTCP.Commands.OKReceive:
                    ProcessOKReceive(new FileTransferMessageTCP.OKReceiveParameter(nMessage.Parameters));
                    break;
            }
        }

        #endregion

        #region Comandos

        private void ProcessSEND_FILE(FileTransferMessageTCP.SEND_FILEParameter nParameters)
        {
            // Crear contenedor para la transmision de partes
            FileStream Archivo = new FileStream(_FileTransferClient.DownloadPath + nParameters.Identifier + ".part", FileMode.Create);
            Archivo.Write(new byte[nParameters.FileSize], 0, (int)nParameters.FileSize);
            Archivo.Close();

            // Crear la definición
            FileTransferDefinition Definition = new FileTransferDefinition();

            Definition.Identifier = nParameters.Identifier;
            Definition.Type = FileTransferDefinition.DefinitionType.CLIENT;
            Definition.FileName = nParameters.FileName;
            Definition.FileSize = nParameters.FileSize;
            Definition.PackageSize = nParameters.PackageSize;
            Definition.FilePackages = nParameters.FilePackages;
            Definition.Parts = new bool[Definition.FilePackages];
            Definition.CreationDate = DateTime.Now;
            Definition.ServerIPAddress = nParameters.ServerIPAddress;
            Definition.TCPPort = nParameters.TCPPort;
            Definition.UDPPort = nParameters.UDPPort;

            // Almacenar la definición
            FileTransferDefinition.Serialize(Definition, _FileTransferClient.DownloadPath + nParameters.Identifier + ".definition");
            
            // Enviar mensaje READY
            this._FileTransferClient.SendReadyMessage(Definition, true, null);
        }

        private void ProcessREADY(FileTransferMessageTCP.READYParameter nParameters)
        {
            // Validar que exista el archivo
            if (!File.Exists(this._FileTransferClient.RepositoryPath + nParameters.Identifier + ".definition"))
            {
                //TODO: Reportar no existencia del archivo, intentar bloquear archivos para evitar borrado
            }
            else
            {
                FileTransferDefinition Definition = FileTransferDefinition.Deserialize(this._FileTransferClient.RepositoryPath + nParameters.Identifier + ".definition");

                this._FileTransferClient.SendFileParts(Definition, nParameters.All, nParameters.Parts);
            }
        }

        private void ProcessOKSend(FileTransferMessageTCP.OKSendParameter nParameters)
        {
            if (File.Exists(this._FileTransferClient.DownloadPath + nParameters.Identifier + ".definition"))
            {
                FileTransferDefinition Definition = FileTransferDefinition.Deserialize(this._FileTransferClient.DownloadPath + nParameters.Identifier + ".definition");

                if (!Definition.IsCompleted())
                    this._FileTransferClient.SendReadyMessage(Definition, false, Definition.getMissingParts(this._FileTransferClient.MaxSendingPackages));
            }
        }

        private void ProcessOKReceive(FileTransferMessageTCP.OKReceiveParameter nParameters)
        {
            if (File.Exists(this._FileTransferClient.RepositoryPath + nParameters.Identifier + ".definition"))
            {
                FileTransferDefinition Definition = FileTransferDefinition.Deserialize(this._FileTransferClient.RepositoryPath + nParameters.Identifier + ".definition");

                switch (Definition.Type)
                {
                    case FileTransferDefinition.DefinitionType.CLIENT:
                        break;

                    case FileTransferDefinition.DefinitionType.PUSH:
                        File.Delete(this._FileTransferClient.RepositoryPath + nParameters.Identifier + ".definition");
                        File.Delete(this._FileTransferClient.RepositoryPath + nParameters.Identifier + ".source");

                        break;

                    case FileTransferDefinition.DefinitionType.SERVER:

                        break;
                }
            }
        }

        #endregion
    }
}