Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports DBCore.SchemaCustody
Imports Miharu.Desktop.Library

Namespace Forms.Boveda

    Public Class FormVisualizacionEstante2
        Inherits FormBase

#Region " Declaraciones "

        Private _nEntidad As Short
        Private _nSede As Short
        Private _nBoveda As Short
        Private _nSeccion As Short
        Private _nEstante As Short
        Private _DataPosiciones As CTA_Estado_EstanteriaDataTable
        Private _ProfundidadSeleccionada As Integer

#End Region

#Region " Constructor "

        Public Sub New(ByVal nEntidad As Short, ByVal nSede As Short, ByVal nBoveda As Short, ByVal nSeccion As Short, ByVal nEstante As Short)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _nEntidad = nEntidad
            _nSede = nSede
            _nBoveda = nBoveda
            _nSeccion = nSeccion
            _nEstante = nEstante
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormVisualizacionEstante_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaValorPosiciones()

            CreaProdundidades()
            If _ProfundidadSeleccionada <> 0 Then CreaProsiciones()
        End Sub

        Private Sub FormVisualizacionEstante_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        End Sub

        Private Sub CambiarProfundidad(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim button = CType(sender, Button)
                _ProfundidadSeleccionada = CInt(button.Name.Replace("Prof", ""))
                Me.Cursor = Cursors.AppStarting
                EstanteSplitContainer.Visible = False
                CreaProsiciones()
                EstanteSplitContainer.Visible = True
                Me.Cursor = Cursors.Default
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CambiarProfundidad", ex)
            End Try
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargaValorPosiciones()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                _DataPosiciones = dbmCore.SchemaCustody.CTA_Estado_Estanteria.DBFindByfk_Entidadfk_Sedefk_Bovedafk_Boveda_Seccionfk_Boveda_Estante(_nEntidad, _nSede, _nBoveda, _nSeccion, _nEstante)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaValorPosiciones", ex)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub CreaProdundidades()
            Try
                Dim flipPanel As New FlowLayoutPanel()
                With flipPanel
                    .Dock = DockStyle.Fill
                    .FlowDirection = FlowDirection.LeftToRight
                End With
                EstanteSplitContainer.Panel1.Controls.Add(flipPanel)

                'Se consultan las profundidades que contiene el estante.
                Dim Produndidades = _DataPosiciones.DefaultView.ToTable(True, "Profundidad_Boveda_Posicion")

                For Each rowProfundidad As DataRow In Produndidades.Rows
                    Dim boton As Button = CrearBotonProfundidad(CInt(rowProfundidad("Profundidad_Boveda_Posicion")))
                    flipPanel.Controls.Add(boton)
                Next
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CreaProdundidades", ex)
            End Try
        End Sub

        Private Sub CreaProsiciones()
            Try
                'Limpia los controles que existan en el panel.
                EstanteSplitContainer.Panel2.Controls.Clear()

                Dim Filas = _DataPosiciones.Compute("max(Fila_Boveda_Posicion)", "Profundidad_Boveda_Posicion=" & _ProfundidadSeleccionada.ToString())
                Dim Columnas = _DataPosiciones.Compute("max(Columna_Boveda_Posicion)", "Profundidad_Boveda_Posicion=" & _ProfundidadSeleccionada.ToString())

                'Crea el TableLayoutPanel
                Dim tlp As New TableLayoutPanel()
                With tlp
                    .Dock = DockStyle.Fill
                    .CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset
                    .ColumnCount = CInt(Columnas)
                    .RowCount = CInt(Filas)
                    .GrowStyle = TableLayoutPanelGrowStyle.AddColumns
                    .Padding = New Padding(1, 1, 1, 1)
                    .AutoSize = True
                    .AutoScroll = True
                End With
                EstanteSplitContainer.Panel2.Controls.Add(tlp)

                'Crea los elementos dentro de la tabla.
                For f = Filas To 1 Step -1 'De abajo hacia arriba
                    For c = 1 To Columnas
                        Dim viewPosicion = _DataPosiciones.DefaultView
                        viewPosicion.RowFilter = "Fila_Boveda_Posicion=" & CStr(f) & " AND Columna_Boveda_Posicion=" & CStr(c) & " AND Profundidad_Boveda_Posicion=" & CStr(_ProfundidadSeleccionada)
                        tlp.Controls.Add(CrearBotonPosicion(viewPosicion.ToTable().Rows(0)))
                    Next
                Next

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CreaProsiciones", ex)
            End Try
        End Sub

#End Region

#Region "Funciones"

        Private Function CrearBotonProfundidad(ByVal id As Integer) As Button
            Dim objBoton As New Button()
            Try
                With objBoton
                    .Name = "Prof" & id.ToString()
                    .Text = "Profundidad " & id.ToString()
                    .Width = 100
                    .FlatStyle = FlatStyle.Flat
                    .FlatAppearance.BorderSize = 1
                    .FlatAppearance.BorderColor = Drawing.Color.Silver

                    AddHandler .Click, AddressOf CambiarProfundidad
                End With
                If id = 1 Then _ProfundidadSeleccionada = id
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CrearBotonProfundidad", ex)
            End Try
            Return objBoton
        End Function

        Private Function CrearBotonPosicion(ByVal posicion As DataRow) As ControlPosicion
            Dim objPosicion As New ControlPosicion

            Try
                With objPosicion
                    .Posicion = CStr(posicion(_DataPosiciones.Codigo_Boveda_PosicionColumn.ColumnName))
                    .Caja = CStr(posicion(_DataPosiciones.Codigo_CajaColumn.ColumnName))
                    .Entidad = CStr(posicion(_DataPosiciones.Nombre_EntidadColumn.ColumnName))
                    .Proyecto = CStr(posicion(_DataPosiciones.Nombre_ProyectoColumn.ColumnName))
                    .TipoCaja = CStr(posicion(_DataPosiciones.Nombre_Caja_TipoColumn.ColumnName))
                    .Carpetas = CInt(posicion(_DataPosiciones.CarpetasColumn.ColumnName))
                End With

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CrearBotonPosicion", ex)
            End Try
            Return objPosicion
        End Function

#End Region
        
    End Class

End Namespace