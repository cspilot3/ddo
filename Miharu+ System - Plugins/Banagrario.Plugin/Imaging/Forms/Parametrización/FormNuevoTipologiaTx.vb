Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl

Namespace Imaging.Forms.Parametrización

    Public Class FormNuevoTipologiaTx

        Private _plugin As BanagrarioImagingPlugin

        Private _idTipologia As Short
        Public Property IdTipologia() As Short
            Get
                Return _idTipologia
            End Get
            Set(ByVal value As Short)
                _idTipologia = value
            End Set
        End Property
        
        Public Sub New(nBanagrarioImagingPlugin As BanagrarioImagingPlugin)

            InitializeComponent()
            _plugin = nBanagrarioImagingPlugin

        End Sub

        Private Sub FormNuevoTipologiaTx_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargarCombos()

            If _idTipologia <> 0 Then
                Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

                Try
                    dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)
                    dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    Dim TipologiasTxDataTable = dbmAgrario.SchemaConfig.TBL_Tipologia_CodigoTx.DBGet(_idTipologia)

                    If TipologiasTxDataTable.Rows.Count > 0 Then
                        Dim TipologiaTxType = TipologiasTxDataTable(0).ToTBL_Tipologia_CodigoTxType

                        With TipologiaTxType
                            TipologiaDesktopComboBox.SelectedValue = .fk_Tipologia
                            TipoMovimientoDesktopComboBox.SelectedValue = .fk_Tipo_Movimiento
                            ProductoDesktopComboBox.SelectedValue = .fk_Producto
                            CodigoTXMaskedTextBox.Text = .Codigo_Tx.ToString
                            DesmaterializadaDesktopCheckBox.Checked = .Desmaterializada
                            CamposMinimosCruceMaskedTextBox.Text = .Campos_Min_Cruce.ToString
                            CamposMinimosCruceSuperiorMaskedTextBox.Text = .Campos_Min_Cruce_Superior.ToString
                            UsaLlavesDesktopCheckBox.Checked = .Usa_Llaves_Cruce
                            MultiregistroDesktopCheckBox.Checked = .Multiregistro
                            NaturalezaMedioPagoComboBox.Text = .Naturaleza_Medio_Pago
                            EliminadoDesktopCheckBox.Checked = .Eliminado_Tipologia
                        End With

                    End If

                Catch ex As Exception
                    DMB.DesktopMessageShow("Llenar Combos", ex)
                Finally
                    If dbmAgrario IsNot Nothing Then dbmAgrario.Connection_Close()
                End Try
            End If

        End Sub

        Private Sub CargarCombos()

            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)
                dbmCore = New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)


                Dim TipologiasDataTable = dbmCore.SchemaConfig.TBL_Tipologia.DBGet(Nothing)
                Dim TipoMovimientoDataTable = dbmAgrario.SchemaConfig.TBL_Movimiento_Tipo.DBGet(Nothing)
                Dim productoDataTable = dbmAgrario.SchemaConfig.TBL_Producto.DBGet(Nothing)

                Utilities.LlenarCombo(TipologiaDesktopComboBox, TipologiasDataTable, TipologiasDataTable.id_TipologiaColumn.ColumnName, TipologiasDataTable.Nombre_TipologiaColumn.ColumnName)
                Utilities.LlenarCombo(TipoMovimientoDesktopComboBox, TipoMovimientoDataTable, TipoMovimientoDataTable.id_Movimiento_TipoColumn.ColumnName, TipoMovimientoDataTable.Nombre_Movimiento_TipoColumn.ColumnName)
                Utilities.LlenarCombo(ProductoDesktopComboBox, productoDataTable, productoDataTable.id_ProductoColumn.ColumnName, productoDataTable.Nombre_ProductoColumn.ColumnName)


            Catch ex As Exception
                DMB.DesktopMessageShow("Llenar Combos", ex)
            Finally
                If dbmAgrario IsNot Nothing Then dbmAgrario.Connection_Close()
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try

        End Sub

        Private Sub GuardarButton_Click(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

            Try
                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim TipologiasTxDataTable = dbmAgrario.SchemaConfig.TBL_Tipologia_CodigoTx.DBGet(_idTipologia)
                Dim TipologiaTxType = New DBAgrario.SchemaConfig.TBL_Tipologia_CodigoTxType

                With TipologiaTxType
                    .fk_Tipologia = CShort(TipologiaDesktopComboBox.SelectedValue)
                    .fk_Tipo_Movimiento = CShort(TipoMovimientoDesktopComboBox.SelectedValue)
                    .fk_Producto = CShort(ProductoDesktopComboBox.SelectedValue)
                    .Codigo_Tx = CShort(CodigoTXMaskedTextBox.Text)
                    .Desmaterializada = DesmaterializadaDesktopCheckBox.Checked
                    .Campos_Min_Cruce = CShort(CamposMinimosCruceMaskedTextBox.Text)
                    .Campos_Min_Cruce_Superior = CShort(CamposMinimosCruceSuperiorMaskedTextBox.Text)
                    .Usa_Llaves_Cruce = UsaLlavesDesktopCheckBox.Checked
                    .Multiregistro = MultiregistroDesktopCheckBox.Checked
                    .Naturaleza_Medio_Pago = NaturalezaMedioPagoComboBox.Text
                    .Eliminado_Tipologia = EliminadoDesktopCheckBox.Checked
                    .fk_Usuario_Log = _plugin.Manager.Sesion.Usuario.id
                    .Fecha_Log = SlygNullable.SysDate
                End With

                If TipologiasTxDataTable.Rows.Count = 0 Then
                    dbmAgrario.SchemaConfig.TBL_Tipologia_CodigoTx.DBInsert(TipologiaTxType)
                Else
                    dbmAgrario.SchemaConfig.TBL_Tipologia_CodigoTx.DBUpdate(TipologiaTxType, TipologiaTxType.fk_Tipologia)
                End If

                MessageBox.Show("La tipologia Codigo tx se guardo exitosamente", "Tipología Codigo TX", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
            Catch ex As Exception
                DMB.DesktopMessageShow("Guardar Tipologia TX", ex)
            Finally
                If dbmAgrario IsNot Nothing Then dbmAgrario.Connection_Close()
            End Try
        End Sub

    End Class

End Namespace