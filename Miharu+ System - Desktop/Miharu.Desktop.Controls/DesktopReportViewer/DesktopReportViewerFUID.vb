Imports System.Windows.Forms

Namespace DesktopReportViewer

    Public Class DesktopReportViewerFUID

#Region " Declaraciones "

        Private _ReportList As New List(Of DesktopReportFUID)
        Private ReportListComboBox As New System.Windows.Forms.ComboBox

#End Region

#Region " Propiedades "

        Public ReadOnly Property ReportList As List(Of DesktopReportFUID)
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

        Public Sub ShowReport(ByVal Report As DesktopReportFUID, reportFuid As DataTable, tipoFuid As Integer, Fuidgeneral As Boolean, tipoGestion As String)
            Report.Launch(reportFuid, tipoFuid, Fuidgeneral, tipoGestion)
        End Sub

#End Region




    End Class
End Namespace