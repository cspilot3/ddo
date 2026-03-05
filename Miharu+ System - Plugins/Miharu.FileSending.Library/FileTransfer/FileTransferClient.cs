using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Slyg.Tools.Net.FileTransfer
{
    public class FileTransferClient
    {
        #region Delegados

        public delegate void FileSendedDelegate(object sender, Guid identifier);

        #endregion

        #region Declaraciones
        
        public const int DEFAULT_PACKAGE_SIZE = 2048;

        private int _PackageSize;

        private int _TCPPort;
        private int _UDPPort;

        private FileTransferTCPServer TCPServer;
        private FileTransferUDPServer UDPServer;

        private string _ClientIPAddress;

        #endregion

        #region Constructores

        public FileTransferClient(int nTCPPort, int nUDPPort)
            : this(nTCPPort, nUDPPort, Program.AppPath + "Repository", Program.AppPath + "DownloadPath")
        {
        }

        public FileTransferClient(int nTCPPort, int nUDPPort, string nRepositoryPath, string nDownloadPath)
            : this(nTCPPort, nUDPPort, DEFAULT_PACKAGE_SIZE, nRepositoryPath, nDownloadPath)
        {
        }

        public FileTransferClient(int nTCPPort, int nUDPPort, int nPackageSize, string nRepositoryPath, string nDownloadPath)
        {
            this.RepositoryPath = nRepositoryPath.TrimEnd('\\') + "\\";
            this.DownloadPath = nDownloadPath.TrimEnd('\\') + "\\";
            this.PackageSize = nPackageSize;
            this._TCPPort = nTCPPort;
            this._UDPPort = nUDPPort;
            this._ClientIPAddress = Program.getClientIPAddress();
            this.MaxSendingPackages = 50;

            // Crear los directorios si no existen
            if (!Directory.Exists(this.RepositoryPath))
                Directory.CreateDirectory(this.RepositoryPath);

            if (!Directory.Exists(this.DownloadPath))
                Directory.CreateDirectory(this.DownloadPath);

            //TODO: Reiniciar transmisiones pendientes

            // Iniciar servidor
            TCPServer = new FileTransferTCPServer(nTCPPort, this);
            UDPServer = new FileTransferUDPServer(nUDPPort, this);
        }

        #endregion

        #region Propiedades

        public string RepositoryPath { get; set; }

        public string DownloadPath { get; set; }

        public int PackageSize
        {
            get
            {
                return _PackageSize;
            }
            set
            {
                if (value > 8000 || value < 1)
                    throw new Exception("El tamaño del paquete debe ser un valor entre 1 y 8000");
                else
                    _PackageSize = value;
            }
        }

        public int TCPPort { get { return this._TCPPort; } }

        public int UDPPort { get { return this._UDPPort; } }

        public string ClientIPAddress { get { return this._ClientIPAddress; } }

        public int MaxSendingPackages { get; set; }

        #endregion

        #region Metodos

        private void SendTCPMessage(FileTransferMessageTCP nMessage, string nServerIPAddress, int nPort)
        {
            XmlSerializer Serializador = new XmlSerializer(typeof(FileTransferMessageTCP));
            MemoryStream stream = new MemoryStream();

            // Serializar el mensaje
            Serializador.Serialize(stream, nMessage);

            // Convertir mensaje a arreglo de bytes
            stream.Position = 0;
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, Convert.ToInt32(stream.Length));

            TcpClient client = new TcpClient();

            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(nServerIPAddress), nPort);

            client.Connect(serverEndPoint);

            NetworkStream clientStream = client.GetStream();

            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
        }

        private void SendUDPMessage(FileTransferMessageUDP nMessage, string nServerIPAddress, int nPort)
        {
            XmlSerializer Serializador = new XmlSerializer(typeof(FileTransferMessageUDP));
            MemoryStream stream = new MemoryStream();

            // Serializar el mensaje
            Serializador.Serialize(stream, nMessage);

            // Convertir mensaje a arreglo de bytes
            stream.Position = 0;
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, Convert.ToInt32(stream.Length));

            // Enviar mensaje             
            UdpClient sender = new UdpClient();
            IPEndPoint endPoint;

            IPAddress remoteIPAddress = IPAddress.Parse(nServerIPAddress);
            endPoint = new IPEndPoint(remoteIPAddress, nPort);

            sender.Send(buffer, buffer.Length, endPoint);
            stream.Close();
        }

        #endregion

        #region Mensajes TCP

        internal void SendFileMessage(FileTransferDefinition nDefinition)
        {
            // Crear mensaje            
            FileTransferMessageTCP.SEND_FILEParameter Parameters = new FileTransferMessageTCP.SEND_FILEParameter();

            Parameters.Identifier = nDefinition.Identifier;
            Parameters.FileName = nDefinition.FileName;
            Parameters.FileSize = nDefinition.FileSize;
            Parameters.PackageSize = nDefinition.PackageSize;
            Parameters.FilePackages = nDefinition.FilePackages;
            Parameters.ServerIPAddress = this._ClientIPAddress;
            Parameters.TCPPort = this._TCPPort;
            Parameters.UDPPort = this._UDPPort;

            FileTransferMessageTCP Message = new FileTransferMessageTCP();
            Message.Command = FileTransferMessageTCP.Commands.SEND_FILE;
            Message.Parameters = Parameters.getParameters();

            // Enviar el mensaje
            SendTCPMessage(Message, nDefinition.ServerIPAddress, nDefinition.TCPPort);
        }

        internal void SendReadyMessage(FileTransferDefinition nDefinition, bool nAll, int[] nParts)
        {
            // Crear mensaje            
            FileTransferMessageTCP.READYParameter Parameters = new FileTransferMessageTCP.READYParameter();

            Parameters.Identifier = nDefinition.Identifier;
            Parameters.All = nAll;
            Parameters.Parts = nParts;

            FileTransferMessageTCP Message = new FileTransferMessageTCP();
            Message.Command = FileTransferMessageTCP.Commands.READY;
            Message.Parameters = Parameters.getParameters();

            // Enviar el mensaje
            SendTCPMessage(Message, nDefinition.ServerIPAddress, nDefinition.TCPPort);
        }

        internal void SendOKSendMessage(FileTransferDefinition nDefinition)
        {
            // Crear mensaje            
            FileTransferMessageTCP.OKSendParameter Parameters = new FileTransferMessageTCP.OKSendParameter();

            Parameters.Identifier = nDefinition.Identifier;

            FileTransferMessageTCP Message = new FileTransferMessageTCP();
            Message.Command = FileTransferMessageTCP.Commands.OKSend;
            Message.Parameters = Parameters.getParameters();

            // Enviar el mensaje
            SendTCPMessage(Message, nDefinition.ServerIPAddress, nDefinition.TCPPort);
        }

        internal void SendOKReceiveMessage(FileTransferDefinition nDefinition)
        {
            // Crear mensaje            
            FileTransferMessageTCP.OKReceiveParameter Parameters = new FileTransferMessageTCP.OKReceiveParameter();

            Parameters.Identifier = nDefinition.Identifier;

            FileTransferMessageTCP Message = new FileTransferMessageTCP();
            Message.Command = FileTransferMessageTCP.Commands.OKReceive;
            Message.Parameters = Parameters.getParameters();

            // Enviar el mensaje
            SendTCPMessage(Message, nDefinition.ServerIPAddress, nDefinition.TCPPort);
        }

        #endregion

        #region Mensajes UDP

        internal void SendFileParts(FileTransferDefinition nDefinition, bool nAll, int[] nParts)
        {
            List<object> Parametros = new List<object>();

            Parametros.Add(nDefinition);
            Parametros.Add(nAll);
            Parametros.Add(nParts);

            Thread clientThread = new Thread(new ParameterizedThreadStart(SendDatagramThread));
            clientThread.Start(Parametros);
        }

        private void SendDatagramThread(object nParametros)
        {
            // Recuperar los parametros
            List<object> Parametros = (List<object>)nParametros;
            FileTransferDefinition Definition = (FileTransferDefinition)Parametros[0];
            bool All = (bool)Parametros[1];
            int[] Parts = (int[])Parametros[2];

            // Leer el archivo
            byte[] FileBuffer = null;
            using (Archivo = new FileStream(this.RepositoryPath + Definition.Identifier + ".source", FileMode.Open))
            {
                FileBuffer = new byte[Archivo.Length];
                Archivo.Read(FileBuffer, 0, FileBuffer.Length);
            }
            //Archivo.Close();

            if (All)
            {
                for (int i = 0; i < Definition.FilePackages && i < this.MaxSendingPackages; i++)
                {
                    SendDatagramThread(Definition, i, FileBuffer);
                }
            }
            else
            {
                for (int i = 0; i < Parts.Length && i < this.MaxSendingPackages; i++)
                {
                    SendDatagramThread(Definition, Parts[i], FileBuffer);
                }
            }

            SendOKSendMessage(Definition);
        }

        private void SendDatagramThread(FileTransferDefinition nDefinition, int nPartIndex, byte[] nFileBuffer)
        {
            byte[] Buffer;

            if (nPartIndex < nDefinition.FilePackages - 1)
                Buffer = new byte[nDefinition.PackageSize];
            else
                Buffer = new byte[nDefinition.FileSize - (nDefinition.PackageSize * (nDefinition.FilePackages - 1))];

            Program.ArrayCopy(ref Buffer, 0, nFileBuffer, nPartIndex * nDefinition.PackageSize, Buffer.Length);

            FileTransferMessageUDP Message = new FileTransferMessageUDP();

            Message.Identificador = nDefinition.Identifier;
            Message.PartIndex = nPartIndex;
            Message.Data = Buffer;

            SendUDPMessage(Message, nDefinition.ServerIPAddress, nDefinition.UDPPort);

            nDefinition.Parts[nPartIndex] = true;

            FileTransferDefinition.Serialize(nDefinition, this.RepositoryPath + nDefinition.Identifier + ".definition");
        }

        #endregion

        #region Funciones

        public Guid SendFile(string nFileName, string nServerIPAddress, int nTCPPort, int nUDPPort)
        {
            // Preparar el archivo
            FileTransferDefinition Definition = Publicar(nFileName, FileTransferDefinition.DefinitionType.PUSH, nServerIPAddress, nTCPPort, nUDPPort);

            // Enviar mensaje
            SendFileMessage(Definition);

            // Agregar el archivo a la pila de transmisiones


            return Definition.Identifier;
        }

        private FileTransferDefinition Publicar(string nFileName, FileTransferDefinition.DefinitionType nType, string nServerIPAddress, int nTCPPort, int nUDPPort)
        {
            // Validar que exista el archivo
            if (!File.Exists(nFileName))
                throw new Exception("El archivo " + nFileName + " no existe");

            // Validar que no exista el archivo en el repositorio
            //if (File.Exists(this.RepositoryPath + Path.GetFileName(nFileName)))
            //    throw new Exception("En el repositorio ya se encuentra un archivo con el nombre: " + Path.GetFileName(nFileName));

            // Leer el archivo
            FileStream Archivo = new FileStream(nFileName, FileMode.Open, FileAccess.Read);

            // Crear la definición
            FileTransferDefinition Definition = new FileTransferDefinition();

            Definition.Identifier = Guid.NewGuid();
            Definition.Type = nType;
            Definition.FileName = Path.GetFileName(nFileName);
            Definition.FileSize = (int)Archivo.Length;
            Definition.PackageSize = this.PackageSize;
            Definition.FilePackages = (Definition.FileSize / this.PackageSize) + (Definition.FileSize % this.PackageSize == 0 ? 0 : 1);

            switch (nType)
            {
                case FileTransferDefinition.DefinitionType.CLIENT:
                    Definition.Parts = new bool[Definition.FilePackages];
                    break;

                case FileTransferDefinition.DefinitionType.PUSH:
                    Definition.Parts = new bool[Definition.FilePackages];
                    break;

                case FileTransferDefinition.DefinitionType.SERVER:
                    Definition.Parts = null;
                    break;
            }

            Definition.CreationDate = DateTime.Now;
            Definition.ServerIPAddress = nServerIPAddress;
            Definition.TCPPort = nTCPPort;
            Definition.UDPPort = nUDPPort;

            // Almacenar la definición
            FileTransferDefinition.Serialize(Definition, this.RepositoryPath + Definition.Identifier + ".definition");

            // Crear el archivo en el repositorio
            File.Copy(nFileName, this.RepositoryPath + Definition.Identifier + ".source");

            return Definition;
        }

        #endregion        
    }
}
