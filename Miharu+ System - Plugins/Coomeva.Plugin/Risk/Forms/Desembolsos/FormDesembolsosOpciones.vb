Imports Coomeva.Plugin.Risk.FormWrappers
Imports Miharu.Desktop.Library


Namespace Forms.Desembolsos

    Public Class FormDesembolsosOpciones
        Inherits FormBase



#Region " Declaraciones "
        Private _Plugin As CoomevaRiskPlugin
        'Private _File As Stream = Nothing
        'Private _DataFile As DataTable = Nothing
        'Private _DataRegistros As Integer = 0
        'Private _DataColumnas As Integer = 0
        'Private _EstadoProceso As Short = 0 '0 Validando, 1 Procesando.
        'Private objCSV As New Slyg.Tools.CSV.CSVData
        'Private objXLS As New XLSData
        'Private trResultado As DesktopConfig.TypeResult
        'Private Segundos As Integer = 0
        'Private Minuto As Integer = 0
        'Private Hora As Integer = 0

#End Region

#Region " Constructor "

        Sub New(ByVal nCoomevaDesktopPlugin As CoomevaRiskPlugin)
            ' This call is required by the designer.
            InitializeComponent()
            _Plugin = nCoomevaDesktopPlugin
            ' Add any initialization after the InitializeComponent() call.

        End Sub

#End Region

        Private Sub FormDesembolsosOpciones_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        End Sub

        Private Sub CargarButton_Click(sender As System.Object, e As System.EventArgs) Handles CargarButton.Click
            Dim formDesembolsos = New Desembolsos.FormCargueDesembolsos(_Plugin)
            formDesembolsos.ShowDialog()
        End Sub

        Private Sub CruzarButton_Click(sender As System.Object, e As System.EventArgs) Handles CruzarButton.Click

            Dim formCruceDesembolsos = New Desembolsos.FormCruceDesembolsos(_Plugin)
            formCruceDesembolsos.ShowDialog()



        End Sub
    End Class

End Namespace