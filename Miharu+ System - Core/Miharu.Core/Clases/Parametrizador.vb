Imports DBCore
Imports DBCore.Schemadbo

Namespace Clases

    Public Class Parametrizador
        Inherits FormBase

#Region "variables"

        Private Varentidad As String = "-1"
        Private conexion As String
        Private Const DBTabla As String = "Tabla"
        Private Const DBColumna As String = "Columna"
        Private Const DBTablaRef As String = "TablaRef"
        Private Const DBEsquemaRef As String = "esquemaRef"
        Private Const DBColumnaRef As String = "ColumnaRef"
        Private Const DBReferencia As String = "Referencia"
        Private Const DBLlavePrimaria As String = "LlavePrimaria"
        Private Const DBNulo As String = "Nulo"
        Private Const DBTipo As String = "TipoDato"
        Private Const DBEsquema As String = "Esquema"
        Private Const DBLongitud As String = "Longitud"
        Private Const DBPresicion As String = "PrecisionNumerica"

        Private Strings As String() = {"VARCHAR", "VARCHAR2", "NVARCHAR", "NCHAR", "CHAR"}
        Private Numbers As String() = {"NUMERIC", "SMALLINT", "INT", "NUMBER", "TINYINT"}
        Private Booleans As String() = {"BIT", "BOOLEAN"}
        Private Dates As String() = {"DATETIME", "DATE"}

        Private GrdData As CoreGridView
        Private TableSQL As String
        Private _varTableGlobal As Table
        Private dtSource As DataTable

        Private usuario As Integer = 1

        Public Property VarTableGlobal As Table
            Get
                Return _varTableGlobal
            End Get
            Set(value As Table)
                _varTableGlobal = value
            End Set
        End Property

#End Region

        ''' <summary>
        ''' Enlaza una Grilla y una tabla a un conjunto de datos para que sean parametrizados desde el aspx.
        ''' </summary>
        ''' <param name="GridViewControl">Grilla que va a contener todos los datos en la tabla.</param>
        ''' <param name="TableDB">Nombre de la tabla en DB   Esquema.Tabla</param>
        ''' <param name="TableControl">Tabla en la que se crearan los Controles dinamicos.</param>
        ''' <param name="ConectionString">Cadena de conexion para el enlace de datos.</param>
        ''' <returns>Control Table que contiene los controles creados.</returns>
        ''' <remarks></remarks>
        Public Function BeginLink(ByRef GridViewControl As CoreGridView, ByVal TableDB As String, ByRef TableControl As Table, ByVal ConectionString As String, ByVal nEntidad As Short, ByVal nUsuario As Integer) As Table

            If TableDB = "" Then
                Throw New Exception("No Hay ninguna tabla para buscar.")
            End If

            Varentidad = CStr(nEntidad)
            GrdData = GridViewControl
            TableSQL = TableDB
            VarTableGlobal = TableControl
            conexion = ConectionString
            usuario = nUsuario

            Dim Tablecontrol_ As Table = load_()

            Return Tablecontrol_
        End Function

        Private Function load_() As Table
            Dim TableControl As Table
            Dim dmCore As New DBCoreDataBaseManager(conexion)
            dmCore.Connection_Open(usuario)

            Try
                Dim tablaEsquema As String() = Split(TableSQL, ".")
                Dim Ordenador = New CTA_DiccionarioDatosEnumList()
                Ordenador.Add(CTA_DiccionarioDatosEnum.PosicionColumna, True)

                Session("table") = dmCore.Schemadbo.CTA_DiccionarioDatos.DBFindByTablaEsquema(tablaEsquema(1), tablaEsquema(0), 0, Ordenador)
                Session("DataTable") = dmCore.DataBase.ExecuteQueryGet(CreateSelectPar(CType(Session("table"), DataTable)))

                'Formatea los Headers de la grilla para que los nombres sean presentables
                AddHandler GrdData.RowDataBound, AddressOf grdData_RowDataBound

                'Obtiene el nombre de las llaves foraneas
                Dim dataSource = CType(Session("DataTable"), DataTable)
                dataSource = procesaDataTable(dataSource, conexion)

                GrdData.DataSource = dataSource
                GrdData.DataBind()

                TableControl = CreateControls(CType(Session("table"), DataTable))
                dmCore.Connection_Close()
            Catch ex As Exception
                TableControl = Nothing
                dmCore.Connection_Close()
            End Try

            Return TableControl
        End Function

#Region "Automatizaciones"

        Private Function CreateControls(ByVal table_ As DataTable) As Table
            Dim ValidaSedeEntidad(1) As Boolean
            ValidaSedeEntidad(0) = False
            ValidaSedeEntidad(1) = False

            Dim dmCore As New DBCoreDataBaseManager(conexion)
            dmCore.Connection_Open(usuario)

            Dim TableControl As New Table

            Dim table As DataTable = table_

            For Each row1 In table.Rows

                Dim row As DataRow = CType(row1, DataRow)
                Dim TabRow As New TableRow()
                Dim TabCell As New TableCell()


                Dim Tipo As String = row(DBTipo).ToString.ToUpper

                'Creacion de los label de descripcion del campo
                Dim VarNombreLabel As String = ""
                VarNombreLabel = row(DBColumna).ToString.Replace("_", " ")
                VarNombreLabel = VarNombreLabel.Replace("fk", "")
                VarNombreLabel = VarNombreLabel.Trim
                VarNombreLabel = VarNombreLabel.Substring(0, 1).ToUpper & VarNombreLabel.Substring(1).ToLower

                Dim Lablabel As Label = DLabel("Lbl_" & row(DBColumna).ToString, True, VarNombreLabel)
                Lablabel.CssClass = "Label"
                Lablabel.Width = 200
                TabCell.Controls.Add(Lablabel)
                TabRow.Cells.Add(TabCell)

                'creacion de control segun tipo
                Dim TabCell2 As New TableCell()
                Dim Control As Object = Nothing

                If row(DBReferencia).ToString <> "1" Then

                    If row(DBColumna).ToString.ToUpper = "FK_ENTIDAD" Then ValidaSedeEntidad(1) = True
                    If row(DBColumna).ToString.ToUpper = "FK_SEDE" Then ValidaSedeEntidad(0) = True

                    'Crea los controles de Texto
                    For Each a In Strings
                        If a = Tipo Then Control = DTextControl(row(DBColumna).ToString, CBool(row(DBNulo).ToString), CInt(IIf(Math.Abs(CDbl(row(DBLongitud).ToString) - 0) > 0, row(DBLongitud).ToString, row(DBPresicion).ToString)))
                    Next

                    'Crea los controles numericos
                    For Each a In Numbers
                        If a = Tipo Then
                            Control = DNumberControl(row(DBColumna).ToString, CBool(row(DBNulo).ToString), CInt(IIf(Math.Abs(CDbl(row(DBLongitud).ToString) - 0) > 0, row(DBLongitud).ToString, row(DBPresicion).ToString)))

                            'Si la llave es ID_ la deshabilita para calcularla automaticamente
                            If row(DBColumna).ToString.ToUpper.IndexOf("ID_", System.StringComparison.Ordinal) >= 0 Then
                                Dim txtId As DNumber = CType(Control, DNumber)
                                txtId.IsRequiered = CBool(0)
                                txtId.Enabled = False
                                txtId.WaterText = "Automatico"
                            End If

                            If row(DBColumna).ToString.ToUpper = "FK_ENTIDAD" Then
                                Dim TxtEntidad As DNumber = CType(Control, DNumber)
                                TxtEntidad.Text = Varentidad
                                TxtEntidad.Enabled = False
                            End If

                        End If
                    Next

                    'Crea los controles de fecha
                    For Each a In Dates
                        If a = Tipo Then
                            Control = DDateControl(row(DBColumna).ToString, CBool(row(DBNulo).ToString))
                            CType(Control, DFecha).Width = 70
                        End If
                    Next

                    'Crea los controles booleanos
                    For Each a In Booleans
                        If a = Tipo Then Control = DCheckBox(row(DBColumna).ToString)
                    Next

                    'Crea los controles que no tienen llave foranea pro que empiezan por FK_, busca en las vistas de la DB
                    If row(DBColumna).ToString.Substring(0, 3).ToUpper = "FK_" Then
                        Try
                            Dim TableRefView As DataTable = dmCore.DataBase.ExecuteQueryGet("select * from dbo." & row(DBColumna).ToString.ToUpper.Replace("FK_", "CTA_"))
                            Control = DDropdownList(row(DBColumna).ToString, TableRefView, row(DBColumna).ToString.ToUpper.Replace("FK_", "ID_"))
                        Catch ex As Exception
                        End Try
                    End If

                Else
                    'Crea los controles que tienen llaves foraneas

                    Dim DatosLista As DataTable = Nothing
                    Dim nId = String.Empty

                    Try

                        If row(DBTablaRef).ToString.ToUpper.Replace("TBL_", "") <> row(DBColumnaRef).ToString.ToUpper.Replace("FK_", "").Replace("ID_", "") Then
                            DatosLista = dmCore.DataBase.ExecuteQueryGet("select * from dbo.CTA_" & row(DBColumnaRef).ToString.ToUpper.Replace("FK_", "") & " where " & row(DBColumnaRef).ToString.ToUpper.Replace("FK_", "ID_") & " IN (select " & row(DBColumnaRef).ToString.ToUpper.Replace("FK_", "ID_") & " from " & row(DBEsquemaRef).ToString.ToUpper & "." & row(DBTablaRef).ToString.ToUpper & ")")
                            nId = row(DBColumnaRef).ToString.ToUpper.Replace("FK_", "ID_")
                        Else
                            DatosLista = dmCore.DataBase.ExecuteQueryGet("select * from " & row(DBEsquemaRef).ToString & "." & row(DBTablaRef).ToString)
                            nId = row(DBColumnaRef).ToString
                        End If

                    Catch ex As Exception
                    End Try

                    Control = DDropdownList(row(DBColumna).ToString, DatosLista, nId)
                End If

                TabCell2.Controls.Add(CType(Control, UI.Control))
                TabRow.Cells.Add(TabCell2)

                TableControl.Rows.Add(TabRow)
            Next


            If ValidaSedeEntidad(0) And ValidaSedeEntidad(1) Then
                If Not IsPostBack Then
                    Session("SEDE_DATA") = dmCore.DataBase.ExecuteQueryGet("select * from dbo." & "FK_SEDE".ToString.ToUpper.Replace("FK_", "CTA_"))
                End If

            End If

            dmCore.Connection_Close()
            Return TableControl
        End Function

#Region "Creacion de controles"

        Private Shared Function DLabel(ByVal nId As String, Optional ByVal _enabled As Boolean = True, Optional ByVal Text As String = "") As Label
            Dim VarLabel As New Label
            VarLabel.ID = nId
            VarLabel.Text = Text
            VarLabel.Enabled = _enabled
            Return VarLabel
        End Function
        Private Shared Function DCheckBox(ByVal nId As String, Optional ByVal _enabled As Boolean = True, Optional ByVal Checked As Boolean = False) As CheckBox
            Dim VarCheck As New CheckBox
            VarCheck.ID = nId
            VarCheck.Checked = Checked
            VarCheck.Enabled = _enabled
            Return VarCheck
        End Function
        Private Shared Function DDropdownList(ByVal nId As String, ByVal data As DataTable, ByVal value As String, Optional ByVal _enabled As Boolean = True) As DropDownList
            Dim VarDrop As New DropDownList
            VarDrop.ID = nId

            Dim text As String = ""

            'Busca que la columna sea como el ID_xxxxx pero con Nombre_xxxxx
            For Each columna In data.Columns
                If columna.ToString.ToUpper = ("Nombre_" & nId.Replace("id_", "").Replace("fk_", "")).ToUpper Then
                    text = columna.ToString
                End If
            Next

            'Si no entra en la funcion anterios entonces busca los que contengan NOMBRE
            If text = "" Then
                For Each columna In data.Columns
                    If columna.ToString.ToUpper.IndexOf("NOMBRE", System.StringComparison.Ordinal) > 0 Then
                        text = columna.ToString
                    End If
                Next
            End If

            'Si no entra en la funcion anterios entonces busca los que contengan DESCR
            If text = "" Then
                For Each columna In data.Columns
                    If columna.ToString.ToUpper.IndexOf("DESCR", System.StringComparison.Ordinal) > 0 Then
                        text = columna.ToString
                    End If
                Next
            End If

            'Si no entra a ninguna pone el Id
            If text = "" Then
                text = value
            End If

            VarDrop.DataSource = data
            VarDrop.DataValueField = value
            VarDrop.DataTextField = text
            VarDrop.DataBind()

            VarDrop.Enabled = _enabled

            Return VarDrop
        End Function

        Private Shared Function DDateControl(ByVal nId As String, ByVal Obligatorio As Boolean, Optional ByVal _enabled As Boolean = True) As DFecha
            Dim Fecha As New Core.DFecha
            Fecha.ID = nId
            Fecha.IsRequiered = Obligatorio
            Fecha.Enabled = _enabled
            Fecha.IsRequiered = True
            Fecha.ValidationGroup = "Guardar"
            Return Fecha
        End Function
        Private Shared Function DNumberControl(ByVal nId As String, ByVal Obligatorio As Boolean, ByVal Ancho As Integer, Optional ByVal _enabled As Boolean = True) As DNumber
            Dim Number_ As New Core.DNumber
            Number_.ID = nId
            Number_.IsRequiered = Obligatorio
            Number_.Enabled = _enabled
            Number_.IsRequiered = True
            Number_.ValidationGroup = "Guardar"

            If Ancho <> 0 Then
                Number_.Width = Ancho * 10
            End If

            Number_.MaxLength = Ancho

            Return Number_
        End Function
        Private Shared Function DTextControl(ByVal nId As String, ByVal Obligatorio As Boolean, ByVal Ancho As Integer, Optional ByVal _enabled As Boolean = True) As DTexto
            Dim Text_ As New Core.DTexto
            Text_.ID = nId
            Text_.IsRequiered = Obligatorio
            Text_.Enabled = _enabled
            Text_.IsRequiered = True
            'Text_.EmptyValueMessage = Id & " es obligatorio"

            If Ancho <> 0 Then
                Dim longitudFinal As Integer = Ancho * 10
                If longitudFinal > 400 Then
                    longitudFinal = 400
                    'Text_.Multiline = TextBoxMode.MultiLine
                Else
                    'Text_.Multiline = TextBoxMode.SingleLine
                End If

                Text_.Width = longitudFinal
            End If

            Text_.MaxLength = Ancho

            Text_.ValidationGroup = "Guardar"
            Return Text_
        End Function

#End Region

#Region "Base de datos"

        Private Function CreateSelectPar(ByVal table As DataTable) As String
            Return "select * from " & table.Rows(0)(DBEsquema).ToString & "." & table.Rows(0)(DBTabla).ToString
        End Function

#End Region

#Region "Eventos"

        Private Sub Guardar(ByVal Tabla As Table)
            If CInt(Session("EstadoFuncion")) = Funcion.Ingresar Then
                CreateInsert(usuario, Tabla, CType(Session("table"), DataTable), conexion)
            Else
                CreateUpdate(usuario, Tabla, CType(Session("table"), DataTable), conexion)
            End If
            load_()
        End Sub

        Private Sub LinkControls(ByVal tabla As Table, ByVal Row As Integer)
            Dim table As DataTable = CType(Session("DataTable"), DataTable)
            Dim tableDic As DataTable = CType(Session("table"), DataTable)

            For i As Integer = 0 To table.Columns.Count - 1 Step 1
                SetControlValue(tabla, table.Columns(i).Caption, table.Rows(Row)(i))

                Try
                    Dim EnabledControl As DataView = tableDic.DefaultView
                    EnabledControl.RowFilter = DBColumna & "='" & table.Columns(i).Caption.ToString & "'"
                    SetControlEnabled(tabla, table.Columns(i).Caption, Not CBool(EnabledControl.ToTable.Rows(0)(DBLlavePrimaria).ToString))
                Catch : End Try
            Next

            Session("EstadoFuncion") = Funcion.Actualizar

        End Sub

#End Region

        Enum Funcion
            Ingresar
            Actualizar
        End Enum

#End Region

#Region "Eventos controles"

        Public Sub EnlazaControles(ByVal tabla As Table, ByVal Registro As Integer)
            If Registro <> -1 Then
                LinkControls(tabla, Registro)
            End If
        End Sub

        Public Sub Nuevo(ByVal tabla As Table)
            LimpiarControles(tabla, CType(Session("table"), DataTable), Varentidad)
            Session("EstadoFuncion") = Funcion.Ingresar
        End Sub

        Public Sub Eliminar(ByVal tabla As Table)
            CreateDelete(usuario, tabla, CType(Session("table"), DataTable), conexion)
            LimpiarControles(tabla, CType(Session("table"), DataTable), Varentidad)
            Session("EstadoFuncion") = Funcion.Ingresar
            load_()
        End Sub

        Public Sub Guardar_(ByVal Tabla As Table)
            Guardar(Tabla)
        End Sub

#End Region

        Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            If e.Row.RowType = DataControlRowType.Header Then
                For Each Columna As TableCell In e.Row.Cells
                    Columna.Text = Columna.Text.Replace("fk_", "")
                    Columna.Text = Columna.Text.Replace("_", " ")
                Next
            End If
        End Sub

    End Class
End Namespace