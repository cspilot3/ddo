using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using DBCore;
using DBStorage;
using DBSecurity;
using DBImaging;
using DBImaging.SchemaProcess;
using DBAgrario;
using DBStorage.SchemaImaging;
using DBAgrario.SchemaConfig;
using Banagrario.Imaging;
using System.DirectoryServices.Protocols;
using System.Net;
using System.DirectoryServices;
using System.Collections;
using System.Diagnostics;
using System.Collections.Specialized;
using DBSecurity.SchemaSecurity;
using Slyg.Tools;
using Miharu.Security.WebService.App_Code;

namespace Banagrario.WebService
{
    /// <summary>
    /// Soporte para oficinas tipo B
    /// </summary>
    [WebService(Namespace = "http://www.slyg.com.co/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class BanagrarioService : System.Web.Services.WebService
    {
        #region Servicio
        
        [WebMethod]
        public PR_getPermisos getPermisos(string nUser)
        {
            PR_getPermisos Respuesta = new PR_getPermisos();

            DBSecurityDataBaseManager dbmSecurity = null;

            try
            {
                dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
                dbmSecurity.Connection_Open(1);

                // Leer los permisos
                var UsuarioDataTable = dbmSecurity.SchemaSecurity.TBL_Usuario.DBFindByLogin_Usuario(nUser);

                if (UsuarioDataTable.Count > 0)
                {
                    Respuesta.UserID = UsuarioDataTable[0].id_Usuario;
                    var UsuarioPerfilesDataTable = dbmSecurity.SchemaSecurity.TBL_Usuario_Perfiles.DBGet(UsuarioDataTable[0].id_Usuario, null);
                    short idModulo = 13;
                    
                    // Permisos por perfil
                    Respuesta.Permisos = new List<string>();

                    foreach (var PerfilUsuarioDataRow in UsuarioPerfilesDataTable)
                    {
                        var PerfilPermisosDataTable = dbmSecurity.SchemaSecurity.TBL_Perfil_Permisos.DBFindByfk_Perfilfk_Modulo(PerfilUsuarioDataRow.fk_Perfil, idModulo);

                        foreach (var RowPerfilPermiso in PerfilPermisosDataTable)
                        {
                            Respuesta.Permisos.Add(RowPerfilPermiso.Cadena_Permiso);
                        }
                    }

                    if (Respuesta.Permisos.Count > 0)
                    {
                        Respuesta.Result = true;
                        Respuesta.Message = "";
                    }
                    else
                    {
                        throw new Exception("El usuario: [" + nUser + "] no cuenta con permisos para ingresar al módulo");
                    }
                }
                else
                {
                    throw new Exception("El usuario: [" + nUser + "] no se encuentra registrado en el sistema");
                }
            }
            catch (Exception ex)
            {
                if (dbmSecurity != null) dbmSecurity.Transaction_Rollback();

                Respuesta.Result = false;
                Respuesta.Message = ex.Message;
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Banagrario.WebService/PR_getPermisos/")]
        public class PR_getPermisos : ResultBase
        {
            [System.Xml.Serialization.XmlAttribute]
            public int UserID;
         
            [System.Xml.Serialization.XmlElement]
            public List<string> Permisos;
        }


        [WebMethod]
        public PR_getOfficeList getOfficeList()
        {
            PR_getOfficeList Respuesta = new PR_getOfficeList();

            DBAgrarioDataBaseManager dbmBanagrario = new DBAgrarioDataBaseManager(Program.BanagrarioConnectionString);

            try
            {
                dbmBanagrario.Connection_Open(1);

                Respuesta.Result = true;
                Respuesta.Message = "";

              //  TBL_OficinaDataTable OficinaDataTable = dbmBanagrario.SchemaConfig.TBL_Oficina.DBFindByfk_Oficina_Tipo(3, 0, new TBL_OficinaEnumList(TBL_OficinaEnum.Nombre_Oficina, true)); // Tipo B
               CTA_Oficina_TransmisionDataTable OficinaDataTable = dbmBanagrario.SchemaConfig.CTA_Oficina_Transmision.DBGet();

                if (OficinaDataTable.Count > 0)
                {
                    Respuesta.Offices = new PR_getOfficeList.TypeOffice[OficinaDataTable.Count];

                    for (int i = 0; i < OficinaDataTable.Count; i++)
                    {
                        Respuesta.Offices[i] = new PR_getOfficeList.TypeOffice() { Id = OficinaDataTable[i].id_Oficina, Name = OficinaDataTable[i].Nombre_Oficina };
                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = ex.Message;
            }
            finally
            {
                dbmBanagrario.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Banagrario.WebService/PR_getOfficeList/")]
        public class PR_getOfficeList : ResultBase
        {
            public struct TypeOffice
            {
                [System.Xml.Serialization.XmlAttribute]
                public int Id;

                [System.Xml.Serialization.XmlAttribute]
                public string Name;
            }

            [System.Xml.Serialization.XmlElement]
            public TypeOffice[] Offices;
        }


        [WebMethod]
        public PR_getMovementList getMovementList()
        {
            PR_getMovementList Respuesta = new PR_getMovementList();

            DBAgrarioDataBaseManager dbmBanagrario = new DBAgrarioDataBaseManager(Program.BanagrarioConnectionString);

            try
            {
                dbmBanagrario.Connection_Open(1);

                Respuesta.Result = true;
                Respuesta.Message = "";


                TBL_Movimiento_TipoDataTable MovimientoTipoDataTable = dbmBanagrario.SchemaConfig.TBL_Movimiento_Tipo.DBGet(null);

                if (MovimientoTipoDataTable.Count > 0)
                {
                    Respuesta.Movements = new PR_getMovementList.TypeMovement[MovimientoTipoDataTable.Count];

                    for (int i = 0; i < MovimientoTipoDataTable.Count; i++)
                    {
                        Respuesta.Movements[i] = new PR_getMovementList.TypeMovement() { Id = MovimientoTipoDataTable[i].id_Movimiento_Tipo, Name = MovimientoTipoDataTable[i].Nombre_Movimiento_Tipo };
                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = ex.Message;
            }
            finally
            {
                dbmBanagrario.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Banagrario.WebService/PR_MovementList/")]
        public class PR_getMovementList : ResultBase
        {
            public struct TypeMovement
            {
                [System.Xml.Serialization.XmlAttribute]
                public int Id;

                [System.Xml.Serialization.XmlAttribute]
                public string Name;
            }
            
            [System.Xml.Serialization.XmlElement]
            public TypeMovement[] Movements;
        }


        [WebMethod]
        public PR_getErrorList getErrorList(int nOffice)
        {
            PR_getErrorList Respuesta = new PR_getErrorList();
            DBAgrarioDataBaseManager dbmBanagrario = new DBAgrarioDataBaseManager(Program.BanagrarioConnectionString);

            try
            {
                dbmBanagrario.Connection_Open(1);

                var ReprocesosDataTable = dbmBanagrario.SchemaProcess.CTA_Reprocesos.DBFindByid_OficinaActualizado(nOffice.ToString(), false);

                Respuesta.Result = true;
                Respuesta.Message = "";
                Respuesta.Errors = null;

                if (ReprocesosDataTable.Count > 0)
                {
                    Respuesta.Errors = new PR_getErrorList.TypeError[ReprocesosDataTable.Count];

                    for (int i = 0; i < ReprocesosDataTable.Count; i++)
                    {
                        Respuesta.Errors[i] = new PR_getErrorList.TypeError() { Fecha = ReprocesosDataTable[i].Fecha_Movimiento, Token = ReprocesosDataTable[i].File_Unique_Identifier, Name = "", Document = ReprocesosDataTable[i].Nombre_Documento, ErrorID = ReprocesosDataTable[i].id_Reproceso_Motivo, Description = ReprocesosDataTable[i].Nombre_Reproceso_Motivo };
                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = ex.Message;
            }
            finally
            {
                dbmBanagrario.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Banagrario.WebService/PR_getErrorList/")]
        public class PR_getErrorList : ResultBase
        {
            public struct TypeError
            {
                [System.Xml.Serialization.XmlAttribute]
                public string Fecha;

                [System.Xml.Serialization.XmlAttribute]
                public Guid Token;

                [System.Xml.Serialization.XmlAttribute]
                public string Name;

                [System.Xml.Serialization.XmlAttribute]
                public string Document;

                [System.Xml.Serialization.XmlAttribute]
                public short ErrorID;

                [System.Xml.Serialization.XmlAttribute]
                public string Description;
            }

            [System.Xml.Serialization.XmlElement]
            public TypeError[] Errors;
        }


        [WebMethod]
        public PR_getFolios getFolios(Guid nToken)
        {
            PR_getFolios Respuesta = new PR_getFolios();

            DBCoreDataBaseManager dbmCore = new DBCoreDataBaseManager(Program.CoreConnectionString);

            try
            {
                dbmCore.Connection_Open(1);

                var FileCoreDataTable = dbmCore.SchemaImaging.TBL_File.DBFindByFile_Unique_Identifier(nToken);

                if (FileCoreDataTable.Count > 0)
                {
                    var FolderDataTable = dbmCore.SchemaImaging.TBL_Folder.DBGet(FileCoreDataTable[0].fk_Expediente, FileCoreDataTable[0].fk_Folder);
                    var ServidorDataTable = dbmCore.SchemaImaging.TBL_Servidor.DBGet(FolderDataTable[0].fk_Entidad_Servidor, FolderDataTable[0].fk_Servidor);

                    DBStorageDataBaseManager DBMStorage = new DBStorageDataBaseManager(ServidorDataTable[0].ConnectionString_Servidor);

                    try
                    {
                        DBMStorage.Connection_Open(1);

                        var FileStorageDataTable = DBMStorage.SchemaImaging.CTA_File.DBFindByfk_Expedientefk_Folderfk_Fileid_Version(FileCoreDataTable[0].fk_Expediente, FileCoreDataTable[0].fk_Folder, FileCoreDataTable[0].fk_File, FileCoreDataTable[0].id_Version);

                        Respuesta.Result = true;
                        Respuesta.Message = "";
                        Respuesta.Folios = (short)FileStorageDataTable[0].Folios;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        DBMStorage.Connection_Close();
                    }
                }
                else
                {
                    Respuesta.Result = false;
                    Respuesta.Message = "No se encontro la imágen";
                }
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = ex.Message;
            }
            finally
            {
                dbmCore.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Banagrario.WebService/PR_getFolios/")]
        public class PR_getFolios : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public short Folios;
        }


        [WebMethod]
        public PR_getFolio getFolio(Guid nToken, short nFolio)
        {
            PR_getFolio Respuesta = new PR_getFolio();

            DBCoreDataBaseManager dbmCore = new DBCoreDataBaseManager(Program.CoreConnectionString);

            try
            {
                dbmCore.Connection_Open(1);

                var FileCoreDataTable = dbmCore.SchemaImaging.TBL_File.DBFindByFile_Unique_Identifier(nToken);

                if (FileCoreDataTable.Count > 0)
                {
                    var FolderDataTable = dbmCore.SchemaImaging.TBL_Folder.DBGet(FileCoreDataTable[0].fk_Expediente, FileCoreDataTable[0].fk_Folder);
                    var ServidorDataTable = dbmCore.SchemaImaging.TBL_Servidor.DBGet(FolderDataTable[0].fk_Entidad_Servidor, FolderDataTable[0].fk_Servidor);

                    DBStorageDataBaseManager DBMStorage = new DBStorageDataBaseManager(ServidorDataTable[0].ConnectionString_Servidor);

                    try
                    {
                        DBMStorage.Connection_Open(1);

                        var FolioStorageDataTable = DBMStorage.SchemaImaging.TBL_File_Folio.DBGet(FileCoreDataTable[0].fk_Expediente, FileCoreDataTable[0].fk_Folder, FileCoreDataTable[0].fk_File, FileCoreDataTable[0].id_Version, nFolio);

                        if (FolioStorageDataTable.Count > 0)
                        {
                            Respuesta.Result = true;
                            Respuesta.Message = "";

                            Bitmap Imagen = new Bitmap(new MemoryStream(FolioStorageDataTable[0].Image_Binary));
                            Imagen = ImageManager.GetThumbnail(Imagen, 90, 120);

                            MemoryStream Data = new MemoryStream();

                            Imagen.Save(Data, ImageFormat.Jpeg);

                            Respuesta.Data = Data.GetBuffer();
                        }
                        else
                        {
                            Respuesta.Result = false;
                            Respuesta.Message = "No se encontro el folio";
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        DBMStorage.Connection_Close();
                    }
                }
                else
                {
                    Respuesta.Result = false;
                    Respuesta.Message = "No se encontro la imágen";
                }
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = ex.Message;
            }
            finally
            {
                dbmCore.Connection_Close();
            }

            return Respuesta;
        }
        
        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Banagrario.WebService/PR_getFolio/")]
        public class PR_getFolio : ResultBase
        {            
            [System.Xml.Serialization.XmlElement]
            public byte[] Data;
        }


        [WebMethod]
        public PR_NewImage NewImage(Guid nToken, byte[] nData, int nUsuario, bool nUltimo)
        {
            PR_NewImage Respuesta = new PR_NewImage();

            DBCoreDataBaseManager dbmCore = new DBCoreDataBaseManager(Program.CoreConnectionString);

            Respuesta.Result = true;
            Respuesta.Message = "";

            try
            {
                dbmCore.Connection_Open(1);

                dbmCore.Transaction_Begin();

                var FileCoreDataTable = dbmCore.SchemaImaging.TBL_File.DBFindByFile_Unique_Identifier(nToken);

                if (FileCoreDataTable.Count == 0)
                    throw new Exception("No se encontro un registro de imagen asignado al Token");

                var EsquemaDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBGet(Program.EntidadCliente, Program.Proyecto, Program.Esquema);

                if (EsquemaDataTable.Count > 0)
                {
                    var ServidorDataTable = dbmCore.SchemaImaging.TBL_Servidor.DBGet(EsquemaDataTable[0].fk_Entidad_Servidor, EsquemaDataTable[0].fk_Servidor);

                    DBImagingDataBaseManager dbmImaging = new DBImagingDataBaseManager(Program.ImagingConnectionString);
                    DBStorageDataBaseManager DBMStorage = new DBStorageDataBaseManager(ServidorDataTable[0].ConnectionString_Servidor);

                    try
                    {
                        dbmImaging.Connection_Open(1);
                        DBMStorage.Connection_Open(1);

                        dbmImaging.Transaction_Begin();
                        DBMStorage.Transaction_Begin();



                        // Crear la nueva versión del File
                        DBCore.SchemaImaging.TBL_FileType FileType = new DBCore.SchemaImaging.TBL_FileType();

                        FileType.fk_Expediente = FileCoreDataTable[0].fk_Expediente;
                        FileType.fk_Folder = FileCoreDataTable[0].fk_Folder;
                        FileType.fk_File = FileCoreDataTable[0].fk_File;
                        FileType.id_Version = dbmCore.SchemaImaging.TBL_File.DBNextId_for_id_Version(FileCoreDataTable[0].fk_Expediente, FileCoreDataTable[0].fk_Folder, FileCoreDataTable[0].fk_File);
                        FileType.File_Unique_Identifier = Guid.NewGuid();
                        FileType.Folios_Documento_File = 1;
                        FileType.Tamaño_Imagen_File = nData.Length;
                        FileType.Nombre_Imagen_File = FileCoreDataTable[0].Nombre_Imagen_File;
                        FileType.Key_Cargue_Item = FileCoreDataTable[0].Key_Cargue_Item;
                        FileType.Save_FileName = FileCoreDataTable[0].Save_FileName;
                        FileType.fk_Content_Type = FileCoreDataTable[0].fk_Content_Type;
                        FileType.fk_Usuario_Log = nUsuario;
                        //FileType.Bloqueado = false;

                        dbmCore.SchemaImaging.TBL_File.DBInsert(FileType);

                        Respuesta.Token = FileType.File_Unique_Identifier;

                        // File Storage
                        DBStorage.SchemaImaging.TBL_FileType ItemType = new DBStorage.SchemaImaging.TBL_FileType();

                        ItemType.fk_Expediente = FileType.fk_Expediente;
                        ItemType.fk_Folder = FileType.fk_Folder;
                        ItemType.fk_File = FileType.fk_File;
                        ItemType.id_Version = FileType.id_Version;
                        ItemType.fk_Content_Type = FileType.fk_Content_Type;
                        ItemType.File_Unique_Identifier = FileType.File_Unique_Identifier;

                        DBMStorage.SchemaImaging.TBL_File.DBInsert(ItemType);

                        // Folio Storage
                        TBL_File_FolioType ItemFolioType = new TBL_File_FolioType();

                        ItemFolioType.fk_Expediente = ItemType.fk_Expediente;
                        ItemFolioType.fk_Folder = ItemType.fk_Folder;
                        ItemFolioType.fk_File = ItemType.fk_File;
                        ItemFolioType.fk_Version = ItemType.id_Version;
                        ItemFolioType.id_File_Record_Folio = 1;
                        ItemFolioType.Image_Binary = new byte[1] { 0 };
                        ItemFolioType.Thumbnail_Binary = new byte[1] { 0 };

                        DBMStorage.SchemaImaging.TBL_File_Folio.DBInsert(ItemFolioType);

                        byte[] DataT;

                        using (MemoryStream ms = new MemoryStream(nData))
                        {
                            Bitmap I = new Bitmap(ms);
                            Bitmap T = ImageManager.GetThumbnail(I, 160, 220);

                            using (MemoryStream tms = new MemoryStream())
                            {
                                T.Save(tms, ImageFormat.Jpeg);

                                DataT = new byte[tms.Length];
                                tms.Read(DataT, 0, DataT.Length);
                                T.Dispose();
                                I.Dispose();
                            }
                        }

                        DBMStorage.ActualizarFileFolio(ItemFolioType.fk_Expediente, ItemFolioType.fk_Folder, ItemFolioType.fk_File, ItemFolioType.fk_Version, ItemFolioType.id_File_Record_Folio, nData, DataT);

                        // Actualizar el File del proceso actual
                        DBImaging.SchemaProcess.TBL_FileType FileProcessType = new DBImaging.SchemaProcess.TBL_FileType();
                        FileProcessType.id_Version = FileType.id_Version;
                        FileProcessType.Actualizado = nUltimo;
                        dbmImaging.SchemaProcess.TBL_File.DBUpdate(FileProcessType, ItemFolioType.fk_Expediente, ItemFolioType.fk_Folder, ItemFolioType.fk_File, FileCoreDataTable[0].id_Version);

                        var FileCore = dbmCore.SchemaProcess.TBL_File.DBGet(ItemFolioType.fk_Expediente, ItemFolioType.fk_Folder, ItemFolioType.fk_File);

                        FileProcessType.fk_Expediente = ItemFolioType.fk_Expediente;
                        FileProcessType.fk_Folder = ItemFolioType.fk_Folder;
                        FileProcessType.fk_File = ItemFolioType.fk_File;
                        FileProcessType.id_Version = FileCoreDataTable[0].id_Version;
                        FileProcessType.fk_Reproceso = DBNull.Value;
                        FileProcessType.fk_Reproceso_Motivo = DBNull.Value;
                        FileProcessType.Actualizado = true;
                        FileProcessType.fk_Documento = FileCore[0].fk_Documento;
                        dbmImaging.SchemaProcess.TBL_File.DBInsert(FileProcessType);

                        dbmImaging.Transaction_Commit();
                        DBMStorage.Transaction_Commit();

                        Respuesta.Result = true;
                        Respuesta.Message = "";
                    }
                    catch (Exception ex)
                    {
                        dbmImaging.Transaction_Rollback();
                        DBMStorage.Transaction_Rollback();

                        throw ex;
                    }
                    finally
                    {
                        DBMStorage.Connection_Close();
                    }
                }
                else
                {
                    Respuesta.Result = false;
                    Respuesta.Message = "No se encontro el esquema con código: fk_Entidad=" + Program.EntidadCliente + ", fk_Proyecto=" + Program.Proyecto + ", id_Esquema=" + Program.Esquema;
                }

                dbmCore.Transaction_Commit();
            }
            catch (Exception ex)
            {
                dbmCore.Transaction_Rollback();

                Respuesta.Result = false;
                Respuesta.Message = ex.Message;
            }
            finally
            {
                dbmCore.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Banagrario.WebService/PR_NewImage/")]
        public class PR_NewImage : ResultBase
        {            
            [System.Xml.Serialization.XmlAttribute]
            public Guid Token;
        }


        [WebMethod]
        public PR_AddFolio AddFolio(Guid nToken, byte[] nData, bool nUltimo)
        {
            PR_AddFolio Respuesta = new PR_AddFolio();

            DBCoreDataBaseManager dbmCore = new DBCoreDataBaseManager(Program.CoreConnectionString);

            Respuesta.Result = true;
            Respuesta.Message = "";

            try
            {
                dbmCore.Connection_Open(1);

                dbmCore.Transaction_Begin();

                var FileCoreDataTable = dbmCore.SchemaImaging.TBL_File.DBFindByFile_Unique_Identifier(nToken);

                if (FileCoreDataTable.Count == 0)
                    throw new Exception("No se encontro un registro de imagen asignado al Token");

                var EsquemaDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBGet(Program.EntidadCliente, Program.Proyecto, Program.Esquema);

                if (EsquemaDataTable.Count > 0)
                {
                    var ServidorDataTable = dbmCore.SchemaImaging.TBL_Servidor.DBGet(EsquemaDataTable[0].fk_Entidad_Servidor, EsquemaDataTable[0].fk_Servidor);

                    DBImagingDataBaseManager dbmImaging = new DBImagingDataBaseManager(Program.ImagingConnectionString);
                    DBStorageDataBaseManager DBMStorage = new DBStorageDataBaseManager(ServidorDataTable[0].ConnectionString_Servidor);

                    try
                    {
                        dbmImaging.Connection_Open(1);
                        DBMStorage.Connection_Open(1);

                        dbmImaging.Transaction_Begin();
                        DBMStorage.Transaction_Begin();

                        // Crear la nueva versión del File
                        DBCore.SchemaImaging.TBL_FileType FileType = new DBCore.SchemaImaging.TBL_FileType();

                        FileType.Folios_Documento_File = (short)(FileCoreDataTable[0].Folios_Documento_File + 1);
                        FileType.Tamaño_Imagen_File = FileCoreDataTable[0].Tamaño_Imagen_File + nData.Length;

                        dbmCore.SchemaImaging.TBL_File.DBUpdate(FileType, FileCoreDataTable[0].fk_Expediente, FileCoreDataTable[0].fk_Folder, FileCoreDataTable[0].fk_File, FileCoreDataTable[0].id_Version);

                        Respuesta.Folio = FileType.Folios_Documento_File;

                        // Folio Storage
                        TBL_File_FolioType ItemFolioType = new TBL_File_FolioType();

                        ItemFolioType.fk_Expediente = FileCoreDataTable[0].fk_Expediente;
                        ItemFolioType.fk_Folder = FileCoreDataTable[0].fk_Folder;
                        ItemFolioType.fk_File = FileCoreDataTable[0].fk_File;
                        ItemFolioType.fk_Version = FileType.id_Version;
                        ItemFolioType.id_File_Record_Folio = FileType.Folios_Documento_File;
                        ItemFolioType.Image_Binary = new byte[1] { 0 };
                        ItemFolioType.Thumbnail_Binary = new byte[1] { 0 };

                        DBMStorage.SchemaImaging.TBL_File_Folio.DBInsert(ItemFolioType);

                        byte[] DataT;

                        using (MemoryStream ms = new MemoryStream(nData))
                        {
                            Bitmap I = new Bitmap(ms);
                            Bitmap T = ImageManager.GetThumbnail(I, 160, 220);

                            using (MemoryStream tms = new MemoryStream())
                            {
                                T.Save(tms, ImageFormat.Jpeg);

                                DataT = new byte[tms.Length];
                                tms.Read(DataT, 0, DataT.Length);
                                T.Dispose();
                                I.Dispose();
                            }
                        }

                        DBMStorage.ActualizarFileFolio(ItemFolioType.fk_Expediente, ItemFolioType.fk_Folder, ItemFolioType.fk_File, ItemFolioType.fk_Version, ItemFolioType.id_File_Record_Folio, nData, DataT);

                        if (nUltimo)
                        {
                            DBImaging.SchemaProcess.TBL_FileType FileProcessType = new DBImaging.SchemaProcess.TBL_FileType();

                            FileProcessType.Actualizado = true;
                            dbmImaging.SchemaProcess.TBL_File.DBUpdate(FileProcessType, ItemFolioType.fk_Expediente, ItemFolioType.fk_Folder, ItemFolioType.fk_File, ItemFolioType.fk_Version);
                        }

                        dbmImaging.Transaction_Commit();
                        DBMStorage.Transaction_Commit();

                        Respuesta.Result = true;
                        Respuesta.Message = "";
                    }
                    catch (Exception ex)
                    {
                        dbmImaging.Transaction_Rollback();
                        DBMStorage.Transaction_Rollback();

                        throw ex;
                    }
                    finally
                    {
                        DBMStorage.Connection_Close();
                    }
                }
                else
                {
                    Respuesta.Result = false;
                    Respuesta.Message = "No se encontro el esquema con código: fk_Entidad=" + Program.EntidadCliente + ", fk_Proyecto=" + Program.Proyecto + ", id_Esquema=" + Program.Esquema;
                }

                dbmCore.Transaction_Commit();
            }
            catch (Exception ex)
            {
                dbmCore.Transaction_Rollback();

                Respuesta.Result = false;
                Respuesta.Message = ex.Message;
            }
            finally
            {
                dbmCore.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Banagrario.WebService/PR_AddFolio/")]
        public class PR_AddFolio : ResultBase
        {            
            [System.Xml.Serialization.XmlAttribute]
            public short Folio;
        }

        [WebMethod]
        public PR_SedeProcesamiento SedeProcesamiento()
        {
            PR_SedeProcesamiento Respuesta = new PR_SedeProcesamiento();
          
            DBAgrarioDataBaseManager dbmBanagrario = new DBAgrarioDataBaseManager(Program.BanagrarioConnectionString);

            try
            {

                dbmBanagrario.Connection_Open(1);
                dbmBanagrario.Transaction_Begin();

                var ParametrosOficinas = dbmBanagrario.SchemaConfig.PA_Parametros_OficinasTipoB.DBExecute();

                if (ParametrosOficinas.Count > 0)
                {
                    
                    foreach (CTA_Parametros_OficinasTipoBRow ParametroRow in ParametrosOficinas)
                    {
                        if (ParametroRow.Nombre_Parametro_Sistema == "Sede_OficinasTipoB")
                        {
                            Respuesta.Result = true;
                            Respuesta.Message = "";
                            Respuesta.SedeProcesamiento = ParametroRow.Valor_Parametro_Sistema;
                        }
                        
                    }
                }
                else
                {
                    Respuesta.Result = false;
                    Respuesta.Message = "No se encontro Sede asociada a OficinasTipoB";
                }
            }
             catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = ex.Message;
            }
            finally
            {
                dbmBanagrario.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Banagrario.WebService/PR_SedeProcesamiento/")]
        public class PR_SedeProcesamiento : ResultBase
        {
            [System.Xml.Serialization.XmlAttribute]
            public string SedeProcesamiento;
        }


        [WebMethod]
        public PR_CentroProcesamiento CentroProcesamiento()
        {
            PR_CentroProcesamiento Respuesta = new PR_CentroProcesamiento();

            DBAgrarioDataBaseManager dbmBanagrario = new DBAgrarioDataBaseManager(Program.BanagrarioConnectionString);

            try
            {

                dbmBanagrario.Connection_Open(1);
                dbmBanagrario.Transaction_Begin();

                var ParametrosOficinas = dbmBanagrario.SchemaConfig.PA_Parametros_OficinasTipoB.DBExecute();

                if (ParametrosOficinas.Count > 0)
                {

                    foreach (CTA_Parametros_OficinasTipoBRow ParametroRow in ParametrosOficinas)
                    {
                        if (ParametroRow.Nombre_Parametro_Sistema == "CentroProcesamiento_OficinasTipoB")
                        {
                            Respuesta.Result = true;
                            Respuesta.Message = "";
                            Respuesta.CentroProcesamiento = ParametroRow.Valor_Parametro_Sistema;
                        }

                    }
                }
                else
                {
                    Respuesta.Result = false;
                    Respuesta.Message = "No se encontro Sede asociada a OficinasTipoB";
                }
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = ex.Message;
            }
            finally
            {
                dbmBanagrario.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Banagrario.WebService/PR_CentroProcesamiento/")]
        public class PR_CentroProcesamiento : ResultBase
        {
            [System.Xml.Serialization.XmlAttribute]
            public string CentroProcesamiento;
        }

        #endregion

        #region Interno
        
        #endregion
    }
}