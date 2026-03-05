Imports System.Text
Imports DBCore
Imports DBCore.SchemaConfig
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config

Namespace Forms.Imaging

    Public Class FormParProyectoLlaveImaging
        Inherits Miharu.Desktop.Library.FormBase

#Region " Declaraciones "
        Private _Entidad As Short
        Private _Proyecto As Short

        Private _ListLlaves As List(Of DesktopConfig.LlavePosicion)
        Private _TableForm As New TableLayoutPanel

        Private ProyectoLlaves As TBL_Proyecto_LlaveDataTable
#End Region

#Region " Constructores "
        Sub New(ByVal nEntidad As Short, ByVal nProyecto As Short, ByVal nLlaves As List(Of DesktopConfig.LlavePosicion))
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _Entidad = nEntidad
            _Proyecto = nProyecto
            _ListLlaves = nLlaves
        End Sub
#End Region

#Region " Propiedades "
        Public ReadOnly Property ListaLLaves As List(Of DesktopConfig.LlavePosicion)
            Get
                Return _ListLlaves
            End Get
        End Property
#End Region

#Region " Eventos "
        Private Sub FormParProyectoLlaveImaging_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CrearControles()
        End Sub

        Private Sub GuardarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GuardarButton.Click
            If Validar() Then
                _ListLlaves = GuardarLlaves()
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub PruebaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PruebaButton.Click
            If Validar() Then
                If Not ValorPruebaDesktopTextBox.Text.Length = 0 Then
                    EjecucionPrueba()
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Se debe ingresar un valor para realizar la prueba.", "Campos de Prueba", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    ValorPruebaDesktopTextBox.Focus()
                End If
            End If
        End Sub
#End Region

#Region " Metodos "
        Private Sub CrearControles()
            Dim dmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Dim DataLlave As Boolean = Not (IsNothing(_ListLlaves))

            Try
                dmCore.Connection_Open(Program.Sesion.Usuario.id)

                LlavesGroupBox.Controls.Clear()

                'Se obtienen las llaves del proyecto.
                ProyectoLlaves = dmCore.SchemaConfig.TBL_Proyecto_Llave.DBFindByfk_Entidadfk_Proyecto(_Entidad, _Proyecto)

                'Se crea la tabla donde se muestran los controles.
                _TableForm = New TableLayoutPanel
                _TableForm.Name = "LlavesTableLayoutPanel"
                _TableForm.ColumnCount = 3
                _TableForm.RowCount = ProyectoLlaves.Count + 1 ' Unidad para el título
                _TableForm.Height = 35 * ProyectoLlaves.Count + 1

                _TableForm.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3!))
                _TableForm.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3!))
                _TableForm.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3!))


                Dim filaTabla As Integer = 0

                'Se crean los títulos de la tabla.
                Dim NombreLlaveLabel, PosicionInicialLabel, PosicionLongitudLabel As New Label()

                NombreLlaveLabel.Text = "Nombre llave"
                NombreLlaveLabel.TextAlign = ContentAlignment.MiddleCenter
                NombreLlaveLabel.ForeColor = Color.SeaGreen

                PosicionInicialLabel.Text = "Posición Inicial"
                PosicionInicialLabel.TextAlign = ContentAlignment.MiddleCenter
                PosicionInicialLabel.ForeColor = Color.SeaGreen

                PosicionLongitudLabel.Text = "Longitud Llave"
                PosicionLongitudLabel.TextAlign = ContentAlignment.MiddleCenter
                PosicionLongitudLabel.ForeColor = Color.SeaGreen

                _TableForm.Controls.Add(NombreLlaveLabel, 0, filaTabla)
                _TableForm.Controls.Add(PosicionInicialLabel, 1, filaTabla)
                _TableForm.Controls.Add(PosicionLongitudLabel, 2, filaTabla)
                filaTabla += 1

                'Se crean las llaves dentro de la tabla.
                For Each Llave In ProyectoLlaves

                    Dim LlaveLabel As New Label
                    LlaveLabel.Name = Llave.Nombre_Proyecto_Llave.Replace(" ", "_") & "_Nombre"
                    LlaveLabel.Text = Llave.Nombre_Proyecto_Llave
                    LlaveLabel.Dock = DockStyle.Fill
                    _TableForm.Controls.Add(LlaveLabel, 0, filaTabla)

                    Dim LlavePosicionInicialTextBox As New Controls.DesktopTextBox.DesktopTextBoxControl
                    LlavePosicionInicialTextBox.Name = Llave.id_Proyecto_Llave & "_Inicial"
                    LlavePosicionInicialTextBox.Type = Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Numerico
                    LlavePosicionInicialTextBox.Dock = DockStyle.Fill
                    If DataLlave Then LlavePosicionInicialTextBox.Text = _ListLlaves(filaTabla - 1).Posicion_Inicial.ToString()
                    _TableForm.Controls.Add(LlavePosicionInicialTextBox, 1, filaTabla)

                    Dim LlavePosicionLongitudTextBox As New Controls.DesktopTextBox.DesktopTextBoxControl
                    LlavePosicionLongitudTextBox.Name = Llave.id_Proyecto_Llave & "_Longitud"
                    LlavePosicionLongitudTextBox.Type = Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Numerico
                    LlavePosicionLongitudTextBox.Dock = DockStyle.Fill
                    If DataLlave Then LlavePosicionLongitudTextBox.Text = _ListLlaves(filaTabla - 1).Posicion_Longitud.ToString()
                    _TableForm.Controls.Add(LlavePosicionLongitudTextBox, 2, filaTabla)

                    filaTabla += 1
                Next
                _TableForm.Dock = DockStyle.Fill
                _TableForm.Refresh()

                LlavesGroupBox.Controls.Add(_TableForm)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CrearControles", ex)
            Finally
                dmCore.Connection_Close()
            End Try
        End Sub

        Private Sub EjecucionPrueba()
            Try
                Dim SumaPosicionesLongitud As Integer = 0
                Dim SumaLongitud As Integer = 0
                Dim ValorPrueba = ValorPruebaDesktopTextBox.Text
                Dim ListLlavesPrueba = New List(Of DesktopConfig.LlavePosicion)


                For Each Llave In ProyectoLlaves
                    Dim LlavePosicion As New DesktopConfig.LlavePosicion()
                    LlavePosicion.Id_Llave = Llave.id_Proyecto_Llave
                    LlavePosicion.Nombre_Llave = CType(Utilities.FindControl(_TableForm, Llave.Nombre_Proyecto_Llave.Replace(" ", "_") & "_Nombre"), Label).Text
                    LlavePosicion.Posicion_Inicial = CShort(CType(Utilities.FindControl(_TableForm, Llave.id_Proyecto_Llave & "_Inicial"), Controls.DesktopTextBox.DesktopTextBoxControl).Text)
                    LlavePosicion.Posicion_Longitud = CShort(CType(Utilities.FindControl(_TableForm, Llave.id_Proyecto_Llave & "_Longitud"), Controls.DesktopTextBox.DesktopTextBoxControl).Text)
                    ListLlavesPrueba.Add(LlavePosicion)

                    SumaPosicionesLongitud += (LlavePosicion.Posicion_Inicial + LlavePosicion.Posicion_Longitud)
                    SumaLongitud += LlavePosicion.Posicion_Longitud
                Next

                If ValorPrueba.Length > SumaPosicionesLongitud Then
                    DesktopMessageBoxControl.DesktopMessageShow("Demasiados caracteres en el campo de pruebas.", "Valor incorrecto", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    ValorPruebaDesktopTextBox.Focus()
                ElseIf ValorPrueba.Length < SumaLongitud Then
                    DesktopMessageBoxControl.DesktopMessageShow("Pocos caracteres en el campo de pruebas.", "Valor incorrecto", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    ValorPruebaDesktopTextBox.Focus()
                Else
                    Dim Mensaje As New StringBuilder
                    For Each LLavePrueba In ListLlavesPrueba
                        Mensaje.AppendLine(LLavePrueba.Nombre_Llave & ": " & ValorPrueba.Substring(LLavePrueba.Posicion_Inicial, LLavePrueba.Posicion_Longitud))
                    Next
                    DesktopMessageBoxControl.DesktopMessageShow(Mensaje.ToString(), "Resultado Prueba", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("EjecucionPrueba", ex)
            End Try
        End Sub
#End Region

#Region " Funciones "
        Private Function Validar() As Boolean
            For Each Control In _TableForm.Controls
                If Control.GetType().Name = "DesktopTextBox" Then
                    Dim objTextBox = CType(Control, Desktop.Controls.DesktopTextBox.DesktopTextBoxControl)
                    If objTextBox.Text.Length = 0 Then
                        DesktopMessageBoxControl.DesktopMessageShow("Este campo requiere un valor.", "Campos Obligatorios", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        objTextBox.Focus()
                        Return False
                    End If
                End If
            Next
            Return True
        End Function

        Private Function GuardarLlaves() As List(Of DesktopConfig.LlavePosicion)
            Try
                _ListLlaves = New List(Of DesktopConfig.LlavePosicion)
                For Each Llave In ProyectoLlaves

                    Dim LlavePosicion As New DesktopConfig.LlavePosicion()
                    LlavePosicion.Id_Llave = Llave.id_Proyecto_Llave
                    LlavePosicion.Nombre_Llave = CType(Utilities.FindControl(_TableForm, Llave.Nombre_Proyecto_Llave.Replace(" ", "_") & "_Nombre"), Label).Text
                    LlavePosicion.Posicion_Inicial = CShort(CType(Utilities.FindControl(_TableForm, Llave.id_Proyecto_Llave & "_Inicial"), Controls.DesktopTextBox.DesktopTextBoxControl).Text)
                    LlavePosicion.Posicion_Longitud = CShort(CType(Utilities.FindControl(_TableForm, Llave.id_Proyecto_Llave & "_Longitud"), Controls.DesktopTextBox.DesktopTextBoxControl).Text)
                    _ListLlaves.Add(LlavePosicion)
                Next
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("GuardarLlaves", ex)
            End Try

            Return _ListLlaves
        End Function
#End Region

    End Class
End Namespace