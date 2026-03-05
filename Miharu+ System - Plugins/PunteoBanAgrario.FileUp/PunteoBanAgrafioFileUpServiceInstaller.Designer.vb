<System.ComponentModel.RunInstaller(True)> Partial Class PunteoBanAgrafioFileUpServiceInstaller
    Inherits System.Configuration.Install.Installer

    'Installer overrides dispose to clean up the component list.
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

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.sInstallerBanAgrarioFileUp = New System.ServiceProcess.ServiceInstaller()
        Me.sProcessInstallerBanAgrarioFileUp = New System.ServiceProcess.ServiceProcessInstaller()
        '
        'sInstallerBanAgrarioFileUp
        '
        Me.sInstallerBanAgrarioFileUp.Description = "Servicio para la carga del Log Punteo Electrónico Diario para BanAgrario"
        Me.sInstallerBanAgrarioFileUp.DisplayName = "PunteoAgrarioFileUpService"
        Me.sInstallerBanAgrarioFileUp.ServiceName = "PunteoAgrarioFileUpService"
        Me.sInstallerBanAgrarioFileUp.StartType = System.ServiceProcess.ServiceStartMode.Automatic
        '
        'sProcessInstallerBanAgrarioFileUp
        '
        Me.sProcessInstallerBanAgrarioFileUp.Account = System.ServiceProcess.ServiceAccount.LocalSystem
        Me.sProcessInstallerBanAgrarioFileUp.Password = Nothing
        Me.sProcessInstallerBanAgrarioFileUp.Username = Nothing
        '
        'PunteoBanAgrafioFileUpServiceInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.sInstallerBanAgrarioFileUp, Me.sProcessInstallerBanAgrarioFileUp})

    End Sub
    Friend WithEvents sInstallerBanAgrarioFileUp As System.ServiceProcess.ServiceInstaller
    Friend WithEvents sProcessInstallerBanAgrarioFileUp As System.ServiceProcess.ServiceProcessInstaller

End Class
