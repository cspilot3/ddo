Imports System.Windows.Forms

Namespace DesktopReportViewer

    Public Class DesktopReportViewerHojaControl

#Region " Declaraciones "

        Private _ReportList As New List(Of DesktopReportHojaControl)
        Private ReportListComboBox As New System.Windows.Forms.ComboBox

#End Region

#Region " Propiedades "

        Public ReadOnly Property ReportList As List(Of DesktopReportHojaControl)
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

        Public Sub ShowReport(ByVal Report As DesktopReportHojaControl, Hoja As DataTable)
            Report.Launch(Hoja)
        End Sub

        Public Sub ShowReport(ByVal Report As DesktopReportHojaControl, ByVal Ruta As String, Cedulas As DataTable)
            Report.Launch(Ruta, Cedulas)
        End Sub

        
#End Region




    End Class
End Namespace