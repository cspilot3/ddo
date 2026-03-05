<System.ComponentModel.RunInstaller(True)> Partial Class ProjectInstaller
    Inherits System.Configuration.Install.Installer

    'Installer reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de componentes
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de componentes requiere el siguiente procedimiento
    'Se puede modificar usando el Diseñador de componentes.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ServiceInstaller = New System.ServiceProcess.ServiceInstaller()
        Me.ServiceProcessInstaller = New System.ServiceProcess.ServiceProcessInstaller()
        '
        'ServiceInstaller
        '
        Me.ServiceInstaller.Description = "Sevicio de transferencia de carpetas de imagenes SFTPTransferService"
        Me.ServiceInstaller.DisplayName = "Miharu.SFTPTransferService"
        Me.ServiceInstaller.ServiceName = "Miharu.SFTPTransferService"
        '
        'ServiceProcessInstaller
        '
        Me.ServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem
        Me.ServiceProcessInstaller.Password = Nothing
        Me.ServiceProcessInstaller.Username = Nothing
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.ServiceInstaller, Me.ServiceProcessInstaller})

    End Sub
    Private WithEvents ServiceInstaller As System.ServiceProcess.ServiceInstaller
    Private WithEvents ServiceProcessInstaller As System.ServiceProcess.ServiceProcessInstaller

End Class
