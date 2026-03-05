Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config

Namespace Forms.Risk

    Public Class FormParProyecto
        Inherits Library.FormBase

#Region " Metodos "

        Public Sub GuardarCambios()
            Dim dmarchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dmarchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim Registro As New DBArchiving.SchemaConfig.TBL_ProyectoType
                Registro.fk_Boveda_Custodia = Utilities.DShort(fk_Boveda_CustodiaComboBox.SelectedValue)
                Registro.fk_Entidad_custodia = Utilities.DShort(fk_Entidad_custodiaComboBox.SelectedValue)
                Registro.fk_Sede_Custodia = Utilities.DShort(fk_Sede_CustodiaComboBox.SelectedValue)
                Registro.Usa_Cargue_Parcial = Usa_Cargue_ParcialCheckBox.Checked
                Registro.Usa_Cargue_Universal = Usa_Cargue_UniversalCheckBox.Checked
                Registro.Usa_Mesa_Control_Imagenes = Usa_Mesa_Control_ImagenesCheckBox.Checked
                Registro.Usa_Custodia_Externa = Usa_Custodia_ExternaCheckBox.Checked
                Registro.Usa_Solo_Cargue_Carpeta = Usa_Solo_Cargue_CarpetaCheckBox.Checked
                Registro.Captura_Folios_Destape = FoliosDestapeCheckBox.Checked
                Registro.Captura_Monto_Destape = MontoDestapeCheckBox.Checked
                Registro.Empaca_Tipologia = EmpacaTipologiaCheckBox.Checked
                Registro.Requiere_Impresion_CBarras = RequiereImpresionCBarrasCheckBox.Checked
                Registro.Acepta_Devoluciones = AceptaDevolucionesCheckBox.Checked
                Registro.Usa_Validacion_Cargue = Usa_Validacion_CargueCheckBox.Checked
                Registro.Dias_Vencimiento = CInt(DiasVencimientoDesktopNumericTextBoxControl.Text)
                Registro.Usa_Empaque_Adicion = Usa_Empaque_AdicionCheckBox.Checked
                Registro.Usa_Validacion_Destape = Usa_Validacion_DestapeCheckBox.Checked
                Registro.Usar_Cancelados_Carpeta = Usar_Empaque_Carpeta_Cancelados.Checked
                Registro.Usar_Cancelados_Documento = Usar_Empaque_Documentos_Cancelados.Checked
                Registro.Usa_control_envío_de_documentos = Usa_Control_Envio_DocumentosCheckBox.Checked
                Registro.Usa_Proyecto_Deceval = Usa_Proyecto_DecevalCheckBox.Checked

                dmarchiving.Transaction_Begin()
                dmarchiving.SchemaConfig.TBL_Proyecto.DBUpdate(Registro, CShort(EntidadDesktopComboBox.SelectedValue), CShort(ProyectoComboBox.SelectedValue))
                dmarchiving.Transaction_Commit()

                DesktopMessageBoxControl.DesktopMessageShow("Datos actualizados con éxito.", "Actualización Proyecto", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
            Catch ex As Exception
                dmarchiving.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("GuardarCambios", ex)
            Finally
                dmarchiving.Connection_Close()
            End Try
        End Sub

        Public Sub llenarCombos()
            Dim dmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)


            Dim Orden As New DBArchiving.SchemaSecurity.CTA_EntidadEnumList
            Orden.Add(DBArchiving.SchemaSecurity.CTA_EntidadEnum.Nombre_Entidad, True)
            Dim Entidad = dmArchiving.SchemaSecurity.CTA_Entidad.DBGet(0, Orden)

            Utilities.LlenarCombo(EntidadDesktopComboBox, Entidad, Entidad.id_EntidadColumn.ColumnName, Entidad.Nombre_EntidadColumn.ColumnName)
            Utilities.LlenarCombo(fk_Entidad_custodiaComboBox, Entidad, Entidad.id_EntidadColumn.ColumnName, Entidad.Nombre_EntidadColumn.ColumnName)

            Dim Sede = dmArchiving.SchemaSecurity.CTA_Sede.DBFindByfk_Entidad(CShort(fk_Entidad_custodiaComboBox.SelectedValue))
            Utilities.LlenarCombo(fk_Sede_CustodiaComboBox, Sede, Sede.id_SedeColumn.ColumnName, Sede.Nombre_SedeColumn.ColumnName, True)

            Dim Boveda = dmArchiving.SchemaCore.CTA_Boveda.DBFindByfk_Entidadfk_Sede(CShort(fk_Entidad_custodiaComboBox.SelectedValue), CShort(fk_Sede_CustodiaComboBox.SelectedValue))
            Utilities.LlenarCombo(fk_Boveda_CustodiaComboBox, Boveda, Boveda.id_BovedaColumn.ColumnName, Boveda.Nombre_BovedaColumn.ColumnName, True)

            dmArchiving.Connection_Close()
        End Sub

        Public Sub LlenarProyecto()
            Dim dmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Dim Proyecto = dmArchiving.Schemadbo.CTA_Proyecto.DBFindByfk_Entidad(CShort(EntidadDesktopComboBox.SelectedValue))

            Utilities.LlenarCombo(ProyectoComboBox, Proyecto, Proyecto.id_ProyectoColumn.ColumnName, Proyecto.Nombre_ProyectoColumn.ColumnName)

            dmArchiving.Connection_Close()
        End Sub

        Public Sub SeleccionarProyecto()
            Dim dmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            'Comprueba que el proyecto exista. si no existe lo crea y permite parametrizarlo
            Dim nEntidad = CShort(EntidadDesktopComboBox.SelectedValue)
            Dim nProyecto = CShort(ProyectoComboBox.SelectedValue)
            Dim TableProyectoVerifica = dmArchiving.SchemaConfig.TBL_Proyecto.DBGet(nEntidad, nProyecto)
            If TableProyectoVerifica.Count = 0 And nProyecto <> 0 Then
                Try
                    dmArchiving.Transaction_Begin()

                    Dim Registro As New DBArchiving.SchemaConfig.TBL_ProyectoType
                    Registro.fk_Entidad = nEntidad
                    Registro.fk_Proyecto = nProyecto
                    Registro.fk_Modulo = DesktopConfig.Modulo.Archiving
                    Registro.Usa_Cargue_Parcial = False
                    Registro.Usa_Cargue_Universal = False
                    Registro.Usa_Mesa_Control_Imagenes = False
                    Registro.Usa_Mesa_Control_Imagenes = False
                    Registro.Usa_Custodia_Externa = False
                    Registro.Empaca_Tipologia = False
                    Registro.Usa_Validacion_Cargue = False
                    Registro.Dias_Vencimiento = 0
                    Registro.Usa_Empaque_Adicion = False
                    Registro.Usa_Validacion_Destape = False
                    Registro.Usar_Cancelados_Carpeta = False
                    Registro.Usar_Cancelados_Documento = False
                    Registro.Usa_control_envío_de_documentos = False
                    Registro.Usa_Proyecto_Deceval = False

                    dmArchiving.SchemaConfig.TBL_Proyecto.DBInsert(Registro)
                    dmArchiving.Transaction_Commit()
                Catch ex As Exception
                    dmArchiving.Transaction_Rollback()
                End Try
            End If

            'Datos base de parametrizacion del proyecto
            Dim TableProyecto = dmArchiving.Schemadbo.CTA_Proyecto_parametrizacion.DBFindByfk_Entidadid_Proyecto(CShort(EntidadDesktopComboBox.SelectedValue), CShort(ProyectoComboBox.SelectedValue))

            For Each row As DBArchiving.Schemadbo.CTA_Proyecto_parametrizacionRow In TableProyecto
                fk_Entidad_custodiaComboBox.SelectedValue = row.fk_Entidad_custodia
                fk_Sede_CustodiaComboBox.SelectedValue = row.fk_Sede_Custodia
                fk_Boveda_CustodiaComboBox.SelectedValue = row.fk_Boveda_Custodia
                Usa_Cargue_ParcialCheckBox.Checked = row.Usa_Cargue_Parcial
                Usa_Cargue_UniversalCheckBox.Checked = row.Usa_Cargue_Universal
                Usa_Mesa_Control_ImagenesCheckBox.Checked = row.Usa_Mesa_Control_Imagenes
                Usa_Custodia_ExternaCheckBox.Checked = row.Usa_custodia_externa
                Usa_Solo_Cargue_CarpetaCheckBox.Checked = row.Usa_Solo_Cargue_Carpeta
                FoliosDestapeCheckBox.Checked = row.Captura_Folios_Destape
                MontoDestapeCheckBox.Checked = row.Captura_Monto_Destape
                RequiereImpresionCBarrasCheckBox.Checked = row.Requiere_Impresion_CBarras
                AceptaDevolucionesCheckBox.Checked = row.Acepta_Devoluciones
                EmpacaTipologiaCheckBox.Checked = row.Empaca_Tipologia
                Usa_Validacion_CargueCheckBox.Checked = row.Usa_Validacion_Cargue
                DiasVencimientoDesktopNumericTextBoxControl.Text = CStr(row.Dias_Vencimiento)
                Usa_Empaque_AdicionCheckBox.Checked = row.Usa_Empaque_Adicion
                Usa_Validacion_DestapeCheckBox.Checked = row.Usa_Validacion_Destape
                Usar_Empaque_Carpeta_Cancelados.Checked = row.Usar_Cancelados_Carpeta
                Usar_Empaque_Documentos_Cancelados.Checked = row.Usar_Cancelados_Documento
                Usa_Control_Envio_DocumentosCheckBox.Checked = row.Usa_control_envío_de_documentos
                Usa_Proyecto_DecevalCheckBox.Checked = row.Usa_Proyecto_Deceval
            Next

            dmArchiving.Connection_Close()
        End Sub

        Public Sub LimpiarControles()
            fk_Entidad_custodiaComboBox.SelectedIndex = -1
            fk_Sede_CustodiaComboBox.SelectedIndex = -1
            fk_Boveda_CustodiaComboBox.SelectedIndex = -1
            Usa_Cargue_ParcialCheckBox.Checked = False
            Usa_Cargue_UniversalCheckBox.Checked = False
            Usa_Solo_Cargue_CarpetaCheckBox.Checked = False
            Usa_Mesa_Control_ImagenesCheckBox.Checked = False
            Usa_Custodia_ExternaCheckBox.Checked = False
            Usa_Validacion_CargueCheckBox.Checked = False
            DiasVencimientoDesktopNumericTextBoxControl.Text = "0"
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormParProyecto_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            llenarCombos()
            SeleccionarProyecto()
        End Sub

        Private Sub EntidadDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EntidadDesktopComboBox.SelectedIndexChanged
            LlenarProyecto()
            LimpiarControles()
            SeleccionarProyecto()
        End Sub

        Private Sub fk_Entidad_custodiaComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles fk_Entidad_custodiaComboBox.SelectedValueChanged
            Dim dmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Dim Sede = dmArchiving.SchemaSecurity.CTA_Sede.DBFindByfk_Entidad(CShort(fk_Entidad_custodiaComboBox.SelectedValue))
            Utilities.LlenarCombo(fk_Sede_CustodiaComboBox, Sede, Sede.id_SedeColumn.ColumnName, Sede.Nombre_SedeColumn.ColumnName, True)

            Dim Boveda = dmArchiving.SchemaCore.CTA_Boveda.DBFindByfk_Entidadfk_Sede(CShort(fk_Entidad_custodiaComboBox.SelectedValue), CShort(fk_Sede_CustodiaComboBox.SelectedValue))
            Utilities.LlenarCombo(fk_Boveda_CustodiaComboBox, Boveda, Boveda.id_BovedaColumn.ColumnName, Boveda.Nombre_BovedaColumn.ColumnName, True)

            dmArchiving.Connection_Close()
        End Sub

        Private Sub fk_Sede_CustodiaComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles fk_Sede_CustodiaComboBox.SelectedValueChanged
            If CStr(fk_Sede_CustodiaComboBox.SelectedValue) <> "-1" Then
                Usa_Custodia_ExternaCheckBox.Checked = False
            End If

            Dim dmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim Boveda = dmArchiving.SchemaCore.CTA_Boveda.DBFindByfk_Entidadfk_Sede(CShort(fk_Entidad_custodiaComboBox.SelectedValue), CShort(fk_Sede_CustodiaComboBox.SelectedValue))
            Utilities.LlenarCombo(fk_Boveda_CustodiaComboBox, Boveda, Boveda.id_BovedaColumn.ColumnName, Boveda.Nombre_BovedaColumn.ColumnName, True)
            dmArchiving.Connection_Close()
        End Sub

        Private Sub ProyectoComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ProyectoComboBox.SelectedValueChanged
            SeleccionarProyecto()
        End Sub

        Private Sub GuardarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GuardarButton.Click
            GuardarCambios()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub Usa_Custodia_ExternaCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles Usa_Custodia_ExternaCheckBox.CheckedChanged
            If Usa_Custodia_ExternaCheckBox.Checked = True Then
                fk_Sede_CustodiaComboBox.SelectedValue = "-1"
                fk_Boveda_CustodiaComboBox.SelectedValue = "-1"
                EmpacaTipologiaCheckBox.Enabled = True
            Else
                EmpacaTipologiaCheckBox.Enabled = False
                EmpacaTipologiaCheckBox.Checked = False
            End If
        End Sub

        Private Sub fk_Boveda_CustodiaComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles fk_Boveda_CustodiaComboBox.SelectedValueChanged
            If CStr(fk_Boveda_CustodiaComboBox.SelectedValue) <> "-1" Then
                Usa_Custodia_ExternaCheckBox.Checked = False
            End If
        End Sub

#End Region

    End Class
End Namespace