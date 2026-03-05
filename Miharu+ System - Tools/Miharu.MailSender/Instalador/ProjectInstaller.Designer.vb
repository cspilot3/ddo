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
        Me.MyServiceProcessInstaller = New System.ServiceProcess.ServiceProcessInstaller()
        Me.MyServiceInstaller = New System.ServiceProcess.ServiceInstaller()
        '
        'MyServiceProcessInstaller
        '
        Me.MyServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem
        Me.MyServiceProcessInstaller.Password = Nothing
        Me.MyServiceProcessInstaller.Username = Nothing
        '
        'MyServiceInstaller
        '
        Me.MyServiceInstaller.Description = "Servicio de envío de correos del sistema MiharuDDO"
        Me.MyServiceInstaller.DisplayName = "MiharuMailSenderDDOService"
        Me.MyServiceInstaller.ServiceName = "MiharuMailSenderDDOService"
        Me.MyServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.MyServiceProcessInstaller, Me.MyServiceInstaller})

    End Sub
    Friend WithEvents MyServiceProcessInstaller As System.ServiceProcess.ServiceProcessInstaller
    Friend WithEvents MyServiceInstaller As System.ServiceProcess.ServiceInstaller

End Class
