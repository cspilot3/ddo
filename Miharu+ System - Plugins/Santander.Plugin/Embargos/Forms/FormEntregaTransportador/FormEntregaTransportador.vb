Imports System.Windows.Forms
Imports System.IO
Imports System.Drawing
Imports DBCore
Imports DBCore.SchemaProcess

Namespace Embargos.Forms.FormEntregaTransportador
    Public Class FormEntregaTransportador
        Private _plugin As EmbargosImagingPlugin

        Public Sub New(nPlugin As EmbargosImagingPlugin)
            _plugin = nPlugin
            InitializeComponent()
        End Sub

        Private Sub CbarrasCartaTextBox_Enter(sender As System.Object, e As System.EventArgs) Handles CbarrasCartaTextBox.Enter
            'If Not String.IsNullOrWhiteSpace(CbarrasCartaTextBox.Text) Then
            '    Dim dbmCore As DBCoreDataBaseManager = Nothing
            '    Try
            '        dbmCore = New DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
            '        dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

            '        Dim File = dbmCore.SchemaProcess.TBL_File.DBFindByCBarras_File(CbarrasCartaTextBox.Text)

            '        If File.Rows.Count > 0 Then
            '            Dim EstadoFile = dbmCore.SchemaProcess.TBL_File_Estado.DBFindByfk_Expedientefk_Folderfk_FileModulo(File(0).fk_Expediente, File(0).fk_Folder, File(0).id_File, 2)
            '            ' Estado 42 - Generado
            '            If EstadoFile(0).fk_Estado = 42 Then
            '                Dim Registro As New TBL_File_EstadoType

            '                Registro.fk_Estado = 43 ' Estado 43 - Entregado a Transportador

            '                dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(Registro, EstadoFile(0).fk_Expediente, EstadoFile(0).fk_Folder, EstadoFile(0).fk_File, EstadoFile(0).Modulo)
            '                ResultadoLabel.Text = "El documento " & CbarrasCartaTextBox.Text & " fue actualizado correctamente."
            '                ResultadoLabel.ForeColor = Color.Green
            '                CbarrasCartaTextBox.Clear()
            '            Else
            '                ResultadoLabel.Text = "El documento no se encuentra en un estado válido para el proceso"
            '                ResultadoLabel.ForeColor = Color.Red
            '            End If
            '        Else
            '            ResultadoLabel.Text = "No se encontró ningún documento con ese código de barras."
            '            ResultadoLabel.ForeColor = Color.Red
            '        End If
            '    Catch ex As Exception
            '        MessageBox.Show("Error: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Finally
            '        If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            '    End Try


            'Else
            '    MessageBox.Show("Debe ingresar un código de barras.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'End If
        End Sub

        Private Sub CbarrasCartaTextBox_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles CbarrasCartaTextBox.KeyPress
            If e.KeyChar = Convert.ToChar(13) Then
                NoGuiaTextBox.Focus()
            End If
        End Sub

        Private Sub NoGuiaTextBox_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles NoGuiaTextBox.KeyPress
            If e.KeyChar = Convert.ToChar(13) Then
                If Validar() Then
                    Entrega()
                End If
            End If
        End Sub

        Private Function Validar() As Boolean

            If String.IsNullOrWhiteSpace(CbarrasCartaTextBox.Text) And String.IsNullOrWhiteSpace(NoGuiaTextBox.Text) Then
                MessageBox.Show("No se han ingresado datos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            If String.IsNullOrWhiteSpace(CbarrasCartaTextBox.Text) And NoGuiaTextBox.Text <> "" Then
                MessageBox.Show("Debe ingresar un código de barras.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            If CbarrasCartaTextBox.Text <> "" And String.IsNullOrWhiteSpace(NoGuiaTextBox.Text) Then
                MessageBox.Show("Debe ingresar el número de guia.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            Return True

        End Function

        Private Sub Entrega()
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim guia = dbmCore.SchemaProcess.TBL_File.DBFindByCBarras_File(NoGuiaTextBox.Text)
                If guia.Rows.Count > 0 Then
                    ResultadoTextBox.Text = "La guía ingresada es el código de barras de una carta.  ¡Por favor corrija!"
                    ResultadoTextBox.ForeColor = Color.Red
                    Return
                Else

                    Dim File = dbmCore.SchemaProcess.TBL_File.DBFindByCBarras_File(CbarrasCartaTextBox.Text)

                    If File.Rows.Count > 0 Then
                        Dim EstadoFile = dbmCore.SchemaProcess.TBL_File_Estado.DBFindByfk_Expedientefk_Folderfk_FileModulo(File(0).fk_Expediente, File(0).fk_Folder, File(0).id_File, 2)
                        ' Estado 42 - Generado
                        If EstadoFile(0).fk_Estado = 42 Then
                            Dim Registro As New TBL_File_EstadoType
                            Dim Data As New TBL_File_DataType

                            'Estado 43 - Entregado a Transportador
                            Registro.fk_Estado = 43
                            dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(Registro, EstadoFile(0).fk_Expediente, EstadoFile(0).fk_Folder, EstadoFile(0).fk_File, EstadoFile(0).Modulo)

                            Data.fk_Expediente = File(0).fk_Expediente
                            Data.fk_Folder = File(0).fk_Folder
                            Data.fk_File = File(0).id_File
                            Data.fk_Documento = File(0).fk_Documento
                            Data.fk_Campo = 3
                            Data.Valor_File_Data = NoGuiaTextBox.Text
                            Data.Conteo_File_Data = 1
                            dbmCore.SchemaProcess.TBL_File_Data.DBInsert(Data)

                            ResultadoTextBox.Text = "El documento " & CbarrasCartaTextBox.Text & " fue actualizado correctamente."
                            ResultadoTextBox.ForeColor = Color.Green
                            CbarrasCartaTextBox.Clear()
                            NoGuiaTextBox.Clear()
                            CbarrasCartaTextBox.Focus()
                        Else
                            ResultadoTextBox.Text = "El documento no se encuentra en un estado válido para el proceso"
                            ResultadoTextBox.ForeColor = Color.Red
                        End If
                    Else
                        ResultadoTextBox.Text = "No se encontró ningún documento con ese código de barras."
                        ResultadoTextBox.ForeColor = Color.Red
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show("Error: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try
        End Sub

    End Class


End Namespace
