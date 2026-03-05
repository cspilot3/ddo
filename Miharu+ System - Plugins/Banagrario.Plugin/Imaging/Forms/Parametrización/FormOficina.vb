Imports System.Windows.Forms
Imports DBAgrario
Imports DBAgrario.SchemaConfig
Imports DBAgrario.SchemaSecurity
Imports Miharu.Desktop.Library.Config

Namespace Imaging.Forms.Parametrización

    Public Class FormOficina

#Region " Declaraciones "

        Private _plugin As BanagrarioImagingPlugin
        Private _ctaOficinaDataTable As New CTA_OficinaDataTable
        Private _oficinaDataTable As New TBL_OficinaDataTable
        Private _oficinaTipoDataTable As New TBL_Oficina_TipoDataTable
        Private _regionalDataTable As New TBL_RegionalDataTable
        Private _cobDataTable As New TBL_COBDataTable
        Private _departamentoDataTable As New CTA_Config_DepartamentoDataTable
        Private _isNuevo As Boolean


#End Region

#Region "Constructor"

        Public Sub New(ByVal nBanagrarioImaginPlugin As BanagrarioImagingPlugin)
            InitializeComponent()
            _plugin = nBanagrarioImaginPlugin
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormOficina_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargarRegional()
            cmbCOB.Enabled = False
        End Sub
        Private Sub cmbCOB_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmbCOB.SelectedIndexChanged
            CargarGrillaOficina()
        End Sub
        Private Sub cmbRegional_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmbRegional.SelectedIndexChanged
            CargarCOB()
            CargarOficinaTipo()
            CargarDepartamento()
            cmbCOB.Enabled = True
        End Sub

        Private Sub DataGridViewOficina_CellDoubleClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridViewOficina.CellDoubleClick
            TabControlOficina.SelectTab(1)
            Dim rowOficinaData = CType(CType(DataGridViewOficina.CurrentRow.DataBoundItem, DataRowView).Row, CTA_OficinaRow)
            ShowDetalle(rowOficinaData)

        End Sub

        Private Sub BtnNuevaOfic_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnNuevaOfic.Click
            _isNuevo = True
        End Sub
        Private Sub BtnGuardarOfic_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnGuardarOfic.Click
            GuardarOficina()
        End Sub

#End Region

#Region " Metodos "

        Private Sub ShowDetalle(ByVal nOficinaRow As CTA_OficinaRow)

            CkBoxLunes.Checked = nOficinaRow.Lunes
            CkBoxMartes.Checked = nOficinaRow.Martes
            CkBoxMiercoles.Checked = nOficinaRow.Miercoles
            CkBoxJueves.Checked = nOficinaRow.Jueves
            CkBoxViernes.Checked = nOficinaRow.Viernes
            CkBoxViernes.Checked = nOficinaRow.Sabado
            CkBoxDomingo.Checked = nOficinaRow.Domingo
            DesktopComboCOB.SelectedValue = nOficinaRow.id_COB
            DesktopComboBoxDepartamento.SelectedValue = nOficinaRow.fk_Departamento
            DesktopComboTipOficina.SelectedValue = nOficinaRow.id_Oficina_Tipo
            txtCodOficina.Text = CStr(nOficinaRow.id_Oficina)
            CkBoxOficinaActiva.Checked = nOficinaRow.Activa
            txtTipCuadre.Text = CStr(nOficinaRow.Tipo_Cuadre)
            txtCodOficinaCruce.Text = CStr(nOficinaRow.Codigo_Oficina_Cruce)
            txtNombreOficina.Text = nOficinaRow.Nombre_Oficina
            Me.Refresh()


        End Sub
        Private Sub CargarRegional()
            Dim dbmBancoAgrario As New DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
            dbmBancoAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
            Try
                _regionalDataTable = dbmBancoAgrario.SchemaConfig.TBL_Regional.DBGet(Nothing)
                Utilities.LlenarCombo(cmbRegional, _regionalDataTable, _regionalDataTable.id_RegionalColumn.ColumnName, _regionalDataTable.Nombre_RegionalColumn.ColumnName, True, "-1", "- Todos -")
            Catch ex As Exception
            Finally
                dbmBancoAgrario.Connection_Close()
            End Try


        End Sub

        Private Sub CargarCob()
            Dim dbmBancoAgrario As New DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
            dbmBancoAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
            Try
                _cobDataTable = dbmBancoAgrario.SchemaConfig.TBL_COB.DBFindByfk_Regional(CShort(cmbRegional.SelectedValue))
                Utilities.LlenarCombo(cmbCOB, _cobDataTable, _cobDataTable.id_COBColumn.ColumnName, _cobDataTable.Nombre_COBColumn.ColumnName, True, "-1", "- Todos -")
                Utilities.LlenarCombo(DesktopComboCOB, _cobDataTable, _cobDataTable.id_COBColumn.ColumnName, _cobDataTable.Nombre_COBColumn.ColumnName, True, "-1", "- Todos -")
            Catch ex As Exception
            Finally
                dbmBancoAgrario.Connection_Close()
            End Try


        End Sub

        Private Sub CargarOficinaTipo()

            Dim dbmBancoAgrario As New DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
            dbmBancoAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
            Try
                _oficinaTipoDataTable = dbmBancoAgrario.SchemaConfig.TBL_Oficina_Tipo.DBGet(Nothing)
                Utilities.LlenarCombo(DesktopComboTipOficina, _oficinaTipoDataTable, _oficinaTipoDataTable.id_Oficina_TipoColumn.ColumnName, _oficinaTipoDataTable.Nombre_Oficina_TipoColumn.ColumnName, True, "-1", "- -")
            Catch ex As Exception
            Finally
                dbmBancoAgrario.Connection_Close()
            End Try



        End Sub

        Private Sub CargarDepartamento()
            Dim dbmBancoAgrario As New DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
            dbmBancoAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
            Try
                _departamentoDataTable = dbmBancoAgrario.SchemaSecurity.CTA_Config_Departamento.DBGet(Nothing, Nothing)
                Utilities.LlenarCombo(DesktopComboBoxDepartamento, _departamentoDataTable, _departamentoDataTable.id_RegionColumn.ColumnName, _departamentoDataTable.Nombre_RegionColumn.ColumnName, True, "-1", "- -")
            Catch ex As Exception
            Finally
                dbmBancoAgrario.Connection_Close()
            End Try
        End Sub

        Private Sub CargarGrillaOficina()

            Dim dbmBancoAgrario As New DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
            dbmBancoAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
            Try
                _ctaOficinaDataTable = dbmBancoAgrario.SchemaConfig.CTA_Oficina.DBFindByid_COB(CShort(cmbCOB.SelectedValue))
                DataGridViewOficina.DataSource = _ctaOficinaDataTable
            Catch ex As Exception
            Finally

                dbmBancoAgrario.Connection_Close()
            End Try

        End Sub
        Private Sub GuardarOficina()
            Dim dbmBancoAgrario As New DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
            dbmBancoAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
            Dim rowOficina As TBL_OficinaRow

            Try
                rowOficina = _oficinaDataTable.NewTBL_OficinaRow
                rowOficina.id_Oficina = CInt(txtCodOficina.Text)
                rowOficina.fk_COB = CShort(DesktopComboCOB.SelectedValue)
                rowOficina.fk_Oficina_Tipo = CShort(DesktopComboTipOficina.SelectedValue)
                rowOficina.Nombre_Oficina = txtNombreOficina.Text
                rowOficina.Tipo_Cuadre = CShort(txtTipCuadre.Text)
                rowOficina = _oficinaDataTable.NewTBL_OficinaRow
                rowOficina.Activa = CkBoxOficinaActiva.Checked
                rowOficina.Codigo_Oficina_Cruce = CInt(txtCodOficinaCruce.Text)
                rowOficina.Lunes = CkBoxLunes.Checked
                rowOficina.Martes = CkBoxMartes.Checked
                rowOficina.Miercoles = CkBoxMiercoles.Checked
                rowOficina.Jueves = CkBoxJueves.Checked
                rowOficina.Viernes = CkBoxViernes.Checked
                rowOficina.Sabado = CkBoxSabado.Checked
                rowOficina.Domingo = CkBoxDomingo.Checked
                rowOficina.fk_Departamento = CInt(DesktopComboBoxDepartamento.SelectedValue)
                rowOficina.Correo_Contacto = CorreoContactoDesktopTextBox.Text

                If _isNuevo Then
                    'Insertar la nueva Oficina
                    dbmBancoAgrario.SchemaConfig.TBL_Oficina.DBInsert(rowOficina)
                Else
                    dbmBancoAgrario.SchemaConfig.TBL_Oficina.DBUpdate(rowOficina.id_Oficina, rowOficina.fk_COB, rowOficina.fk_Oficina_Tipo, rowOficina.Nombre_Oficina, rowOficina.Tipo_Cuadre, rowOficina.Lunes, rowOficina.Martes, rowOficina.Miercoles, rowOficina.Jueves, rowOficina.Viernes, rowOficina.Sabado, rowOficina.Domingo, rowOficina.Activa, rowOficina.fk_Departamento, rowOficina.Codigo_Oficina_Cruce, rowOficina.Correo_Contacto, rowOficina.id_Oficina)
                End If
            Catch ex As Exception
            Finally
                dbmBancoAgrario.Connection_Close()
            End Try

        End Sub

#End Region

    End Class

End Namespace