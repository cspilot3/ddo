Imports System.Windows.Forms

Namespace DesktopReportViewer

    Public Class DesktopReportViewer1Control

#Region " Declaraciones "

        Private _ReportList As New List(Of DesktopReport1)
        Private ReportListComboBox As New System.Windows.Forms.ComboBox

#End Region

#Region " Propiedades "

        Public ReadOnly Property ReportList As List(Of DesktopReport1)
            Get
                Return Me._ReportList
            End Get
        End Property

#End Region

#Region " Metodos "
        Public Sub Load_Reports()
            'ReportListComboBox.Items.Clear()
            ReportListComboBox.Items.Add("- Selecionar reporte -")
            ReportListComboBox.SelectedIndex() = 0
            ReportListComboBox.DisplayMember = "ReportName"

            For Each Reporte In Me._ReportList
                ReportListComboBox.Items.Add(Reporte)
            Next
        End Sub

        Public Sub ShowReport(ByVal Report As DesktopReport1, Carpeta As String)
            Report.Launch(Carpeta)
        End Sub
        Public Sub ShowReport(ByVal Report As DesktopReport1, Carpeta As String, OT As Integer, RotuloCarpeta As Boolean, Reportegenericofuid As Boolean, TipoFuid As Integer, TipoGestion As String)
            Report.Launch(Carpeta, OT, RotuloCarpeta, Reportegenericofuid, TipoFuid, TipoGestion)
        End Sub

#End Region




    End Class
End Namespace