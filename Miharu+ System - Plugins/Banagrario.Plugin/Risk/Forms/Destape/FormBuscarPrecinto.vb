Imports DBCore
Imports DBCore.SchemaConfig
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms
Imports DBAgrario
Imports DBAgrario.SchemaProcess
Imports SLYG.Tools
Imports Miharu.Desktop.Library.Config

Namespace Risk.Forms.Destape
    
    Public Class FormBuscarPrecinto

#Region " Declaraciones "

        Private _Plugin As BanagrarioRiskPlugin

        Private _PermiteIgualarValor As Boolean = True

        Public Property OT_Actual() As Integer = Nothing

#End Region

#Region " Propiedades "

        Public Property PrecintoSeleccionado() As String = ""

#End Region

#Region " Contructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioRiskPlugin, ByVal nOTActual As Integer)
            InitializeComponent()
            _Plugin = nBanagrarioDesktopPlugin
            OT_Actual = nOTActual
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormBuscarPrecinto_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            LlenarCombos()
        End Sub

        Private Sub CodOficina1ComboBox_SelectedValueChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CodOficina1ComboBox.SelectedValueChanged
            IgualarOficinaValor(CodOficina1ComboBox, NombreOficina1ComboBox)
        End Sub

        Private Sub NombreOficina1ComboBox_SelectedValueChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles NombreOficina1ComboBox.SelectedValueChanged
            IgualarOficinaValor(NombreOficina1ComboBox, CodOficina1ComboBox)
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            DialogResult = DialogResult.Cancel
            Close()
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            Buscar()
        End Sub

        Private Sub ContenedoresDataGridView_CellDoubleClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles ContenedoresDataGridView.CellDoubleClick
            SeleccionarPrecinto()
        End Sub

        Private Sub InformeButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles InformeButton.Click
            Exportar()
        End Sub

#End Region

#Region " Metodos "

        Private Sub LlenarCombos()
            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Try
                dbmCore = New DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                ''dbmCore.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim EntidadId As Short = _Plugin.Manager.RiskGlobal.Entidad

                Dim oficinaData = dbmCore.SchemaConfig.TBL_Campo_Lista_Item.DBFindByfk_Entidadfk_Campo_Lista(EntidadId, Program.Banagrario_ListaOficinaId)
                Dim seleccioneRow = oficinaData.NewTBL_Campo_Lista_ItemRow()
                seleccioneRow.Etiqueta_Campo_Lista_Item = "_Todas"
                seleccioneRow.Valor_Campo_Lista_Item = ""
                seleccioneRow.fk_Campo_Lista = 1
                seleccioneRow.fk_Entidad = 0
                seleccioneRow.id_Campo_Lista_Item = 1
                oficinaData.Rows.InsertAt(seleccioneRow, 0)

                Dim ofiView = oficinaData.DefaultView
                ofiView.Sort = TBL_Campo_Lista_ItemEnum.Etiqueta_Campo_Lista_Item.ColumnName

                _PermiteIgualarValor = False
                Utilities.LlenarCombo(CodOficina1ComboBox, ofiView.ToTable(), TBL_Campo_Lista_ItemEnum.Valor_Campo_Lista_Item.ColumnName, TBL_Campo_Lista_ItemEnum.Etiqueta_Campo_Lista_Item.ColumnName)

                For Each ofi In oficinaData
                    Dim posGuion = ofi.Etiqueta_Campo_Lista_Item.IndexOf("-"c)
                    ofi.Etiqueta_Campo_Lista_Item = ofi.Etiqueta_Campo_Lista_Item.Substring(posGuion + 1, ofi.Etiqueta_Campo_Lista_Item.Length - posGuion - 1).Trim()
                Next

                ofiView = oficinaData.DefaultView
                ofiView.Sort = TBL_Campo_Lista_ItemEnum.Etiqueta_Campo_Lista_Item.ColumnName

                Utilities.LlenarCombo(NombreOficina1ComboBox, ofiView.ToTable(), TBL_Campo_Lista_ItemEnum.Valor_Campo_Lista_Item.ColumnName, TBL_Campo_Lista_ItemEnum.Etiqueta_Campo_Lista_Item.ColumnName)


                CodOficina1ComboBox.SelectedIndex = -1
                NombreOficina1ComboBox.SelectedIndex = -1

                _PermiteIgualarValor = True

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Destape", ex)
            Finally
                Try : dbmCore.Connection_Close() : Catch : End Try
            End Try

        End Sub

        Private Sub IgualarOficinaValor(ByVal nComboBoxOrigen As ComboBox, ByVal nComboBoxDestino As ComboBox)
            If (_PermiteIgualarValor) Then
                _PermiteIgualarValor = False

                If (Not nComboBoxOrigen.SelectedValue Is Nothing) Then
                    Dim valor = nComboBoxOrigen.SelectedValue.ToString()

                    Dim destinoSelectedIndex = -1
                    For i As Integer = 0 To nComboBoxDestino.Items.Count - 1
                        Dim itemRow = DirectCast(nComboBoxDestino.Items(i), DataRowView).Row
                        Dim valorItem As String = CStr(itemRow(TBL_Campo_Lista_ItemEnum.Valor_Campo_Lista_Item.ColumnName))
                        If (valor = valorItem) Then
                            destinoSelectedIndex = i
                            Exit For
                        End If
                    Next
                    If (destinoSelectedIndex = -1) Then
                        DesktopMessageBoxControl.DesktopMessageShow("La lista de destino no contiene el elemento seleccionado " + valor, "Destape", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                    End If
                    nComboBoxDestino.SelectedIndex = destinoSelectedIndex
                End If

                _PermiteIgualarValor = True
            End If
        End Sub

        Private Sub Buscar()
            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing
            Try
                dbmAgrario = New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
                ''dbmAgrario.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim precinto As String = "*"
                Dim oficina As SlygNullable(Of Integer) = Nothing

                If (CodOficina1ComboBox.SelectedIndex > 0) Then
                    oficina = CInt(CodOficina1ComboBox.SelectedValue)
                End If
                If (PrecintoTextBox.Text.Trim() <> "") Then
                    precinto = PrecintoTextBox.Text.Trim() & "*"
                End If

                If (PrecintoTextBox.Text.Trim() = "0") Then
                    precinto = "*"
                End If

                If (precinto = "*" And oficina.IsNull) Then
                    DesktopMessageBoxControl.DesktopMessageShow("Se debe ingresar al menos un criterio de busqueda", "Buscar precinto", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    Return
                End If

                Dim precintosData = dbmAgrario.SchemaProcess.CTA_Destape_Buscar.DBFindByid_Oficinaid_Precinto(oficina, precinto)
                BanAgrarioData.Clear()

                BanAgrarioData.CTA_Destape_Buscar.Merge(precintosData)

                'ContenedoresDataGridView.DataSource = precintosData

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Destape", ex)
            Finally
                Try : dbmAgrario.Connection_Close() : Catch : End Try
            End Try
        End Sub

        Private Sub SeleccionarPrecinto()
            Try
                If (ContenedoresDataGridView.SelectedCells.Count > 0) Then
                    Dim row = DirectCast(DirectCast(DirectCast(ContenedoresDataGridView.SelectedCells(0), DataGridViewTextBoxCell).OwningRow.DataBoundItem, DataRowView).Row, CTA_Destape_BuscarRow)
                    If (row.id_OT = OT_Actual) Then
                        PrecintoSeleccionado = row.id_Precinto
                        DialogResult = DialogResult.OK
                        Close()
                    Else
                        MessageBox.Show("No se permite seleccionar precintos de una ot diferente a la que se está trabajando", "Seleccionar precinto", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Destape", ex)
            End Try
        End Sub

        Private Sub Exportar()
            Dim Cuadro As New SaveFileDialog()

            Cuadro.Filter = "Archivo de Excel (*.xls)|*.xls"

            Dim Respuesta = Cuadro.ShowDialog()

            If (Respuesta = DialogResult.OK) Then
                Try
                    Dim Exportador As New Slyg.Tools.CSV.CSVData(vbTab, """", True)

                    Dim Datos = New DataTable()
                    Datos.Merge(BanAgrarioData.CTA_Destape_Buscar)

                    Exportador.DataTable = New Slyg.Tools.CSV.CSVTable(Datos)
                    Exportador.SaveAsCSV(Cuadro.FileName, False)

                    MessageBox.Show("La información se exportó exitosamente", "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Sub

#End Region

    End Class

End Namespace