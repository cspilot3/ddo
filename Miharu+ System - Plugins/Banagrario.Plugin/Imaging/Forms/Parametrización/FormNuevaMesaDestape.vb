Imports System.Windows.Forms
Imports DBAgrario
Imports DBSecurity
Imports Miharu.Desktop.Library.Config

Namespace Imaging.Forms.Parametrización

    Public Class FormNuevaMesaDestape

#Region " Declaraciones "

        Private _Plugin As BanagrarioImagingPlugin
        Public _idMesaDestape As Integer = 0
        Public _idSede As Integer
        Public _idCentroProcesamiento As Integer
#End Region

#Region " Contructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioImagingPlugin)
            InitializeComponent()

            _Plugin = nBanagrarioDesktopPlugin

        End Sub

#End Region

#Region " Eventos "

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            GuardarMesaDestape()
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            DialogResult = DialogResult.Cancel
        End Sub

#End Region

#Region " Métodos "

        Private Sub GuardarMesaDestape()

            If (Validar()) Then

                Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing

                Try

                    dbmAgrario = New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)

                    dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    dbmAgrario.Transaction_Begin()

                    Dim MesaDestapeDataTable = dbmAgrario.SchemaConfig.TBL_Mesa_Destape.DBFindByid_Mesa_Destape(CShort(_idMesaDestape))
                    Dim MesaDestapeType As New DBAgrario.SchemaConfig.TBL_Mesa_DestapeType

                    If MesaDestapeDataTable.Count > 0 Then
                        With MesaDestapeType
                            .fk_Entidad = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad
                            .fk_Sede = CShort(SedeDesktopComboBox.SelectedValue)
                            .fk_Centro_Procesamiento = CShort(CentroProcDesktopComboBox.SelectedValue)
                            .PC_Name = PCNameTextBox.Text
                            .Activa = CheckBoxActivo.Checked
                        End With
                        dbmAgrario.SchemaConfig.TBL_Mesa_Destape.DBUpdate(MesaDestapeType, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, CShort(SedeDesktopComboBox.SelectedValue), CShort(CentroProcDesktopComboBox.SelectedValue), CShort(_idMesaDestape))
                    Else
                        With MesaDestapeType
                            .fk_Entidad = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad
                            .fk_Sede = CShort(SedeDesktopComboBox.SelectedValue)
                            .fk_Centro_Procesamiento = CShort(CentroProcDesktopComboBox.SelectedValue)
                            .PC_Name = PCNameTextBox.Text
                            .Activa = CheckBoxActivo.Checked
                        End With

                        dbmAgrario.SchemaConfig.TBL_Mesa_Destape.DBInsert(MesaDestapeType)

                    End If
                    
                    dbmAgrario.Transaction_Commit()
                    MessageBox.Show("La Mesa Destape se guardo exitosamente", "Mesa Destape", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.OK
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Mesa Destape", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
                End Try
            End If

        End Sub

#End Region

#Region " Funciones "

#End Region

        Private Function Validar() As Boolean

            If SedeDesktopComboBox.SelectedIndex = 0 Then
                MessageBox.Show("Debe seleccionar una sede", "MesaDestape", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            If CentroProcDesktopComboBox.SelectedIndex = 0 Then
                MessageBox.Show("Debe seleccionar un centro de procesamiento", "MesaDestape", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
            If PCNameTextBox.Text = "" Then
                MessageBox.Show("Debe digitar el nombre de la máquina", "MesaDestape", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            Else
                Return True
            End If


        End Function

        Private Sub FormNuevaMesaDestape_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            CargarSedes()
        End Sub

        Private Sub CargarSedes()
            Dim dbmSecurity As DBSecurityDataBaseManager = Nothing


            Try
                dbmSecurity = New DBSecurityDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Security)
                dbmSecurity.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim SedesDataTable = dbmSecurity.SchemaConfig.PA_Get_Sedes.DBExecute(_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad)
                Utilities.LlenarCombo(SedeDesktopComboBox, SedesDataTable, SedesDataTable.id_SedeColumn.ColumnName, SedesDataTable.Nombre_SedeColumn.ColumnName, True, "-1", "- Seleccione... -")

                If _idMesaDestape > 0 Then
                    SedeDesktopComboBox.SelectedValue = _idSede
                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
            End Try
        End Sub

        Private Sub SedeDesktopComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles SedeDesktopComboBox.SelectedIndexChanged
            CargarCentroProcesamiento()
        End Sub

        Private Sub CargarCentroProcesamiento()
            Dim dbmSecurity As DBSecurityDataBaseManager = Nothing

            Try
                dbmSecurity = New DBSecurityDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Security)

                dbmSecurity.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim CentroProcesamientoDataTable = dbmSecurity.SchemaConfig.PA_Get_CentroProcesamineto_Sedes.DBExecute(CShort(SedeDesktopComboBox.SelectedValue), _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad)
                Utilities.LlenarCombo(CentroProcDesktopComboBox, CentroProcesamientoDataTable, CentroProcesamientoDataTable.id_Centro_ProcesamientoColumn.ColumnName, CentroProcesamientoDataTable.Nombre_Centro_ProcesamientoColumn.ColumnName, True, "-1", "- Seleccione... -")

                If _idMesaDestape > 0 Then
                    CentroProcDesktopComboBox.SelectedValue = _idCentroProcesamiento
                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
            End Try
        End Sub

    End Class

End Namespace