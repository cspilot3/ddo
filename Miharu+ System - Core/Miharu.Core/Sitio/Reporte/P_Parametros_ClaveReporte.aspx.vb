Imports DBCore

Imports Slyg.Tools
Imports System.Security.Cryptography
Imports Slyg.Tools.Cryptographic
Imports Miharu.Security.Library.WebService
Imports System.Xml.Serialization
Imports System.ComponentModel
Imports Miharu.Security.Library

Namespace Sitio.Reporte

    Public Class P_Parametros_ClaveReporte
        Inherits PopupBase

#Region " Declaraciones "

        Private _idReporte As Integer

#End Region

#Region " Eventos "

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            If Not IsPostBack Then
                Config_Page()
            End If
        End Sub

        Private Sub Clave_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Clave.Click
            Dim cadenaError As New StringBuilder()
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            _idReporte = CInt(GlobalParameterCollection("id_Reporte").DefaultValue)

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)
                If Valida() Then
                    dbmCore.Transaction_Begin()

                    Dim parametro As New SchemaConfig.TBL_ReporteType
                    parametro.Id_Reporte = _idReporte
                    parametro.Genera_ZIP_Reporte = True
                    parametro.Clave_Reporte = Encrypt(Clave_Reporte.Text)

                    'Inserta un nuevo parametro
                    If fk_Reporte.Text <> "" Then
                        dbmCore.SchemaConfig.TBL_Reporte.DBUpdate(parametro, CInt(fk_Reporte.Text))
                    End If

                    dbmCore.Transaction_Commit()

                    Dim TablaReporte = dbmCore.SchemaConfig.TBL_Reporte.DBGet(CInt(fk_Reporte.Text))
                    Dim clave As Byte()
                    Try
                        'Formato de generacion de archivo
                        clave = TablaReporte(0).Clave_Reporte
                        If Clave_Reporte.Text <> Decrypt(clave) Then
                            cadenaError.AppendLine("Ha ocurrido un error al asignar la clave. por favor asigne una nueva clave.")
                            ShowMessage(cadenaError.ToString)
                        Else
                            cadenaError.AppendLine("La clave ha sido asignada correctamente.")
                            ShowMessage(cadenaError.ToString)
                        End If
                        ClearCampos()
                    Catch

                    End Try

                End If
            Catch ex As Exception
                If dbmCore IsNot Nothing Then dbmCore.Transaction_Rollback()
                MyMasterPage.ShowMessage("Ha ocurrido un problema al guardar los cambios, por favor comuniquese con el administrador.", MsgBoxIcon.IconWarning, "Error guardando cambios")
                ClearCampos()
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try


        End Sub

#End Region

#Region " Metodos "

        Private Sub Config_Page()
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                _idReporte = CInt(GlobalParameterCollection("id_Reporte").DefaultValue)

                fk_Reporte.Text = CInt(GlobalParameterCollection("id_Reporte").DefaultValue)
            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try

        End Sub

        Private Sub ClearCampos()
            Me.Clave_Reporte.Text = ""
            Me.ConfirmarClave.Text = ""
        End Sub

        Public Function Valida() As Boolean
            Dim validacion As Boolean = True
            Dim cadenaError As New StringBuilder()

            If Clave_Reporte.Text = "" Then
                cadenaError.AppendLine("El campo clave no puede ser vacio.")
                validacion = False
            End If

            If Clave_Reporte.Text <> ConfirmarClave.Text Then
                cadenaError.AppendLine("Las claves no son iguales.")
                validacion = False
            End If

            If validacion = False Then
                ShowMessage(cadenaError.ToString)
            End If

            Return validacion
        End Function

        Public Shared Function Encrypt(ByVal nData As String) As Byte()
            Return Crypto.DPAPI.Encrypt(nData, MemoryProtectionScope.CrossProcess, 51)
        End Function

        Public Shared Function Decrypt(ByVal nData() As Byte) As String
            Return Crypto.DPAPI.Decrypt(nData, MemoryProtectionScope.CrossProcess, 51)
        End Function

#End Region

    End Class

End Namespace