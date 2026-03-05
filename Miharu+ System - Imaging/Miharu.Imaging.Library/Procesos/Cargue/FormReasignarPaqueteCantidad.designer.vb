Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Procesos.Cargue
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormReasignarPaqueteCantidad
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
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

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormReasignarPaqueteCantidad))
            Me.GroupBoxProcesamiento = New System.Windows.Forms.GroupBox()
            Me.ValidacionesPanel = New System.Windows.Forms.Panel()
            Me.ValidacionesNumericUpDown = New System.Windows.Forms.NumericUpDown()
            Me.Label9 = New System.Windows.Forms.Label()
            Me.ReprocesoPanel = New System.Windows.Forms.Panel()
            Me.ReprocesoNumericUpDown = New System.Windows.Forms.NumericUpDown()
            Me.Label8 = New System.Windows.Forms.Label()
            Me.CalidadPanel = New System.Windows.Forms.Panel()
            Me.CalidadNumericUpDown = New System.Windows.Forms.NumericUpDown()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.TerceraCapturaPanel = New System.Windows.Forms.Panel()
            Me.TerceraCapturaNumericUpDown = New System.Windows.Forms.NumericUpDown()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.SegundaCapturaPanel = New System.Windows.Forms.Panel()
            Me.SegundaCapturaNumericUpDown = New System.Windows.Forms.NumericUpDown()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.CapturaPanel = New System.Windows.Forms.Panel()
            Me.CapturaNumericUpDown = New System.Windows.Forms.NumericUpDown()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.PreCapturaPanel = New System.Windows.Forms.Panel()
            Me.PreCapturaNumericUpDown = New System.Windows.Forms.NumericUpDown()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.RetenidosPanel = New System.Windows.Forms.Panel()
            Me.RetenidosNumericUpDown = New System.Windows.Forms.NumericUpDown()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.IndexacionPanel = New System.Windows.Forms.Panel()
            Me.IndexacionNumericUpDown = New System.Windows.Forms.NumericUpDown()
            Me.IndexacionLabel = New System.Windows.Forms.Label()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.LabelSedeProcesamiento = New System.Windows.Forms.Label()
            Me.CentroDeProcesamientoLabel = New System.Windows.Forms.Label()
            Me.SedeProcesamientoDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.CentroProcesamientoDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.AsignarCargueButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.CorreccionCapturaPanel = New System.Windows.Forms.Panel()
            Me.CorreccionCapturaNumericUpDown = New System.Windows.Forms.NumericUpDown()
            Me.Label12 = New System.Windows.Forms.Label()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.ValidacionListasNumericUpDown = New System.Windows.Forms.NumericUpDown()
            Me.ValidacionListasPanel = New System.Windows.Forms.Panel()
            Me.GroupBoxProcesamiento.SuspendLayout()
            Me.ValidacionesPanel.SuspendLayout()
            CType(Me.ValidacionesNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ReprocesoPanel.SuspendLayout()
            CType(Me.ReprocesoNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.CalidadPanel.SuspendLayout()
            CType(Me.CalidadNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.TerceraCapturaPanel.SuspendLayout()
            CType(Me.TerceraCapturaNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SegundaCapturaPanel.SuspendLayout()
            CType(Me.SegundaCapturaNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.CapturaPanel.SuspendLayout()
            CType(Me.CapturaNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.PreCapturaPanel.SuspendLayout()
            CType(Me.PreCapturaNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.RetenidosPanel.SuspendLayout()
            CType(Me.RetenidosNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.IndexacionPanel.SuspendLayout()
            CType(Me.IndexacionNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Panel1.SuspendLayout()
            Me.CorreccionCapturaPanel.SuspendLayout()
            CType(Me.CorreccionCapturaNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.ValidacionListasNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ValidacionListasPanel.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBoxProcesamiento
            '
            Me.GroupBoxProcesamiento.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBoxProcesamiento.Controls.Add(Me.CorreccionCapturaPanel)
            Me.GroupBoxProcesamiento.Controls.Add(Me.ValidacionListasPanel)
            Me.GroupBoxProcesamiento.Controls.Add(Me.ValidacionesPanel)
            Me.GroupBoxProcesamiento.Controls.Add(Me.ReprocesoPanel)
            Me.GroupBoxProcesamiento.Controls.Add(Me.CalidadPanel)
            Me.GroupBoxProcesamiento.Controls.Add(Me.TerceraCapturaPanel)
            Me.GroupBoxProcesamiento.Controls.Add(Me.SegundaCapturaPanel)
            Me.GroupBoxProcesamiento.Controls.Add(Me.CapturaPanel)
            Me.GroupBoxProcesamiento.Controls.Add(Me.PreCapturaPanel)
            Me.GroupBoxProcesamiento.Controls.Add(Me.RetenidosPanel)
            Me.GroupBoxProcesamiento.Controls.Add(Me.IndexacionPanel)
            Me.GroupBoxProcesamiento.Controls.Add(Me.Panel1)
            Me.GroupBoxProcesamiento.Controls.Add(Me.AsignarCargueButton)
            Me.GroupBoxProcesamiento.Controls.Add(Me.CerrarButton)
            Me.GroupBoxProcesamiento.Location = New System.Drawing.Point(12, 12)
            Me.GroupBoxProcesamiento.Name = "GroupBoxProcesamiento"
            Me.GroupBoxProcesamiento.Size = New System.Drawing.Size(426, 517)
            Me.GroupBoxProcesamiento.TabIndex = 0
            Me.GroupBoxProcesamiento.TabStop = False
            Me.GroupBoxProcesamiento.Text = "Asignar a..."
            '
            'ValidacionesPanel
            '
            Me.ValidacionesPanel.Controls.Add(Me.ValidacionesNumericUpDown)
            Me.ValidacionesPanel.Controls.Add(Me.Label9)
            Me.ValidacionesPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.ValidacionesPanel.Location = New System.Drawing.Point(3, 355)
            Me.ValidacionesPanel.Name = "ValidacionesPanel"
            Me.ValidacionesPanel.Size = New System.Drawing.Size(420, 29)
            Me.ValidacionesPanel.TabIndex = 45
            '
            'ValidacionesNumericUpDown
            '
            Me.ValidacionesNumericUpDown.Location = New System.Drawing.Point(161, 3)
            Me.ValidacionesNumericUpDown.Name = "ValidacionesNumericUpDown"
            Me.ValidacionesNumericUpDown.Size = New System.Drawing.Size(120, 20)
            Me.ValidacionesNumericUpDown.TabIndex = 30
            Me.ValidacionesNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'Label9
            '
            Me.Label9.AutoSize = True
            Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label9.Location = New System.Drawing.Point(4, 5)
            Me.Label9.Name = "Label9"
            Me.Label9.Size = New System.Drawing.Size(83, 13)
            Me.Label9.TabIndex = 31
            Me.Label9.Text = "Validaciones:"
            '
            'ReprocesoPanel
            '
            Me.ReprocesoPanel.Controls.Add(Me.ReprocesoNumericUpDown)
            Me.ReprocesoPanel.Controls.Add(Me.Label8)
            Me.ReprocesoPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.ReprocesoPanel.Location = New System.Drawing.Point(3, 325)
            Me.ReprocesoPanel.Name = "ReprocesoPanel"
            Me.ReprocesoPanel.Size = New System.Drawing.Size(420, 30)
            Me.ReprocesoPanel.TabIndex = 44
            '
            'ReprocesoNumericUpDown
            '
            Me.ReprocesoNumericUpDown.Location = New System.Drawing.Point(161, 3)
            Me.ReprocesoNumericUpDown.Name = "ReprocesoNumericUpDown"
            Me.ReprocesoNumericUpDown.Size = New System.Drawing.Size(120, 20)
            Me.ReprocesoNumericUpDown.TabIndex = 30
            Me.ReprocesoNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'Label8
            '
            Me.Label8.AutoSize = True
            Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label8.Location = New System.Drawing.Point(4, 5)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(72, 13)
            Me.Label8.TabIndex = 31
            Me.Label8.Text = "Reproceso:"
            '
            'CalidadPanel
            '
            Me.CalidadPanel.Controls.Add(Me.CalidadNumericUpDown)
            Me.CalidadPanel.Controls.Add(Me.Label5)
            Me.CalidadPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.CalidadPanel.Location = New System.Drawing.Point(3, 295)
            Me.CalidadPanel.Name = "CalidadPanel"
            Me.CalidadPanel.Size = New System.Drawing.Size(420, 30)
            Me.CalidadPanel.TabIndex = 43
            '
            'CalidadNumericUpDown
            '
            Me.CalidadNumericUpDown.Location = New System.Drawing.Point(161, 3)
            Me.CalidadNumericUpDown.Name = "CalidadNumericUpDown"
            Me.CalidadNumericUpDown.Size = New System.Drawing.Size(120, 20)
            Me.CalidadNumericUpDown.TabIndex = 30
            Me.CalidadNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label5.Location = New System.Drawing.Point(4, 5)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(53, 13)
            Me.Label5.TabIndex = 31
            Me.Label5.Text = "Calidad:"
            '
            'TerceraCapturaPanel
            '
            Me.TerceraCapturaPanel.Controls.Add(Me.TerceraCapturaNumericUpDown)
            Me.TerceraCapturaPanel.Controls.Add(Me.Label4)
            Me.TerceraCapturaPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.TerceraCapturaPanel.Location = New System.Drawing.Point(3, 265)
            Me.TerceraCapturaPanel.Name = "TerceraCapturaPanel"
            Me.TerceraCapturaPanel.Size = New System.Drawing.Size(420, 30)
            Me.TerceraCapturaPanel.TabIndex = 42
            '
            'TerceraCapturaNumericUpDown
            '
            Me.TerceraCapturaNumericUpDown.Location = New System.Drawing.Point(161, 3)
            Me.TerceraCapturaNumericUpDown.Name = "TerceraCapturaNumericUpDown"
            Me.TerceraCapturaNumericUpDown.Size = New System.Drawing.Size(120, 20)
            Me.TerceraCapturaNumericUpDown.TabIndex = 30
            Me.TerceraCapturaNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(4, 5)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(103, 13)
            Me.Label4.TabIndex = 31
            Me.Label4.Text = "Tercera Captura:"
            '
            'SegundaCapturaPanel
            '
            Me.SegundaCapturaPanel.Controls.Add(Me.SegundaCapturaNumericUpDown)
            Me.SegundaCapturaPanel.Controls.Add(Me.Label3)
            Me.SegundaCapturaPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.SegundaCapturaPanel.Location = New System.Drawing.Point(3, 235)
            Me.SegundaCapturaPanel.Name = "SegundaCapturaPanel"
            Me.SegundaCapturaPanel.Size = New System.Drawing.Size(420, 30)
            Me.SegundaCapturaPanel.TabIndex = 41
            '
            'SegundaCapturaNumericUpDown
            '
            Me.SegundaCapturaNumericUpDown.Location = New System.Drawing.Point(161, 3)
            Me.SegundaCapturaNumericUpDown.Name = "SegundaCapturaNumericUpDown"
            Me.SegundaCapturaNumericUpDown.Size = New System.Drawing.Size(120, 20)
            Me.SegundaCapturaNumericUpDown.TabIndex = 30
            Me.SegundaCapturaNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(4, 5)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(109, 13)
            Me.Label3.TabIndex = 31
            Me.Label3.Text = "Segunda Captura:"
            '
            'CapturaPanel
            '
            Me.CapturaPanel.Controls.Add(Me.CapturaNumericUpDown)
            Me.CapturaPanel.Controls.Add(Me.Label2)
            Me.CapturaPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.CapturaPanel.Location = New System.Drawing.Point(3, 205)
            Me.CapturaPanel.Name = "CapturaPanel"
            Me.CapturaPanel.Size = New System.Drawing.Size(420, 30)
            Me.CapturaPanel.TabIndex = 40
            '
            'CapturaNumericUpDown
            '
            Me.CapturaNumericUpDown.Location = New System.Drawing.Point(161, 3)
            Me.CapturaNumericUpDown.Name = "CapturaNumericUpDown"
            Me.CapturaNumericUpDown.Size = New System.Drawing.Size(120, 20)
            Me.CapturaNumericUpDown.TabIndex = 30
            Me.CapturaNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(4, 5)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(55, 13)
            Me.Label2.TabIndex = 31
            Me.Label2.Text = "Captura:"
            '
            'PreCapturaPanel
            '
            Me.PreCapturaPanel.Controls.Add(Me.PreCapturaNumericUpDown)
            Me.PreCapturaPanel.Controls.Add(Me.Label1)
            Me.PreCapturaPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.PreCapturaPanel.Location = New System.Drawing.Point(3, 175)
            Me.PreCapturaPanel.Name = "PreCapturaPanel"
            Me.PreCapturaPanel.Size = New System.Drawing.Size(420, 30)
            Me.PreCapturaPanel.TabIndex = 39
            '
            'PreCapturaNumericUpDown
            '
            Me.PreCapturaNumericUpDown.Location = New System.Drawing.Point(161, 3)
            Me.PreCapturaNumericUpDown.Name = "PreCapturaNumericUpDown"
            Me.PreCapturaNumericUpDown.Size = New System.Drawing.Size(120, 20)
            Me.PreCapturaNumericUpDown.TabIndex = 30
            Me.PreCapturaNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(4, 5)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(78, 13)
            Me.Label1.TabIndex = 31
            Me.Label1.Text = "Pre-Captura:"
            '
            'RetenidosPanel
            '
            Me.RetenidosPanel.Controls.Add(Me.RetenidosNumericUpDown)
            Me.RetenidosPanel.Controls.Add(Me.Label7)
            Me.RetenidosPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.RetenidosPanel.Location = New System.Drawing.Point(3, 145)
            Me.RetenidosPanel.Name = "RetenidosPanel"
            Me.RetenidosPanel.Size = New System.Drawing.Size(420, 30)
            Me.RetenidosPanel.TabIndex = 38
            '
            'RetenidosNumericUpDown
            '
            Me.RetenidosNumericUpDown.Location = New System.Drawing.Point(161, 3)
            Me.RetenidosNumericUpDown.Name = "RetenidosNumericUpDown"
            Me.RetenidosNumericUpDown.Size = New System.Drawing.Size(120, 20)
            Me.RetenidosNumericUpDown.TabIndex = 30
            Me.RetenidosNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label7.Location = New System.Drawing.Point(4, 5)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(68, 13)
            Me.Label7.TabIndex = 31
            Me.Label7.Text = "Retenidos:"
            '
            'IndexacionPanel
            '
            Me.IndexacionPanel.Controls.Add(Me.IndexacionNumericUpDown)
            Me.IndexacionPanel.Controls.Add(Me.IndexacionLabel)
            Me.IndexacionPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.IndexacionPanel.Location = New System.Drawing.Point(3, 115)
            Me.IndexacionPanel.Name = "IndexacionPanel"
            Me.IndexacionPanel.Size = New System.Drawing.Size(420, 30)
            Me.IndexacionPanel.TabIndex = 37
            '
            'IndexacionNumericUpDown
            '
            Me.IndexacionNumericUpDown.Location = New System.Drawing.Point(161, 3)
            Me.IndexacionNumericUpDown.Name = "IndexacionNumericUpDown"
            Me.IndexacionNumericUpDown.Size = New System.Drawing.Size(120, 20)
            Me.IndexacionNumericUpDown.TabIndex = 30
            Me.IndexacionNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'IndexacionLabel
            '
            Me.IndexacionLabel.AutoSize = True
            Me.IndexacionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.IndexacionLabel.Location = New System.Drawing.Point(4, 5)
            Me.IndexacionLabel.Name = "IndexacionLabel"
            Me.IndexacionLabel.Size = New System.Drawing.Size(73, 13)
            Me.IndexacionLabel.TabIndex = 31
            Me.IndexacionLabel.Text = "Indexación:"
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.LabelSedeProcesamiento)
            Me.Panel1.Controls.Add(Me.CentroDeProcesamientoLabel)
            Me.Panel1.Controls.Add(Me.SedeProcesamientoDesktopComboBox)
            Me.Panel1.Controls.Add(Me.CentroProcesamientoDesktopComboBox)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(3, 16)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(420, 99)
            Me.Panel1.TabIndex = 35
            '
            'LabelSedeProcesamiento
            '
            Me.LabelSedeProcesamiento.AutoSize = True
            Me.LabelSedeProcesamiento.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LabelSedeProcesamiento.Location = New System.Drawing.Point(2, 10)
            Me.LabelSedeProcesamiento.Name = "LabelSedeProcesamiento"
            Me.LabelSedeProcesamiento.Size = New System.Drawing.Size(145, 13)
            Me.LabelSedeProcesamiento.TabIndex = 0
            Me.LabelSedeProcesamiento.Text = "Sede de Procesamiento:"
            '
            'CentroDeProcesamientoLabel
            '
            Me.CentroDeProcesamientoLabel.AutoSize = True
            Me.CentroDeProcesamientoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CentroDeProcesamientoLabel.Location = New System.Drawing.Point(4, 56)
            Me.CentroDeProcesamientoLabel.Name = "CentroDeProcesamientoLabel"
            Me.CentroDeProcesamientoLabel.Size = New System.Drawing.Size(153, 13)
            Me.CentroDeProcesamientoLabel.TabIndex = 1
            Me.CentroDeProcesamientoLabel.Text = "Centro de Procesamiento:"
            '
            'SedeProcesamientoDesktopComboBox
            '
            Me.SedeProcesamientoDesktopComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SedeProcesamientoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.SedeProcesamientoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.SedeProcesamientoDesktopComboBox.DisabledEnter = False
            Me.SedeProcesamientoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.SedeProcesamientoDesktopComboBox.fk_Campo = 0
            Me.SedeProcesamientoDesktopComboBox.fk_Documento = 0
            Me.SedeProcesamientoDesktopComboBox.fk_Validacion = 0
            Me.SedeProcesamientoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.SedeProcesamientoDesktopComboBox.FormattingEnabled = True
            Me.SedeProcesamientoDesktopComboBox.Location = New System.Drawing.Point(3, 26)
            Me.SedeProcesamientoDesktopComboBox.Name = "SedeProcesamientoDesktopComboBox"
            Me.SedeProcesamientoDesktopComboBox.Size = New System.Drawing.Size(414, 21)
            Me.SedeProcesamientoDesktopComboBox.TabIndex = 26
            '
            'CentroProcesamientoDesktopComboBox
            '
            Me.CentroProcesamientoDesktopComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CentroProcesamientoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.CentroProcesamientoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.CentroProcesamientoDesktopComboBox.DisabledEnter = False
            Me.CentroProcesamientoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CentroProcesamientoDesktopComboBox.fk_Campo = 0
            Me.CentroProcesamientoDesktopComboBox.fk_Documento = 0
            Me.CentroProcesamientoDesktopComboBox.fk_Validacion = 0
            Me.CentroProcesamientoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.CentroProcesamientoDesktopComboBox.FormattingEnabled = True
            Me.CentroProcesamientoDesktopComboBox.Location = New System.Drawing.Point(3, 72)
            Me.CentroProcesamientoDesktopComboBox.Name = "CentroProcesamientoDesktopComboBox"
            Me.CentroProcesamientoDesktopComboBox.Size = New System.Drawing.Size(414, 21)
            Me.CentroProcesamientoDesktopComboBox.TabIndex = 27
            '
            'AsignarCargueButton
            '
            Me.AsignarCargueButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AsignarCargueButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AsignarCargueButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.refresh
            Me.AsignarCargueButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AsignarCargueButton.Location = New System.Drawing.Point(237, 478)
            Me.AsignarCargueButton.Name = "AsignarCargueButton"
            Me.AsignarCargueButton.Size = New System.Drawing.Size(89, 33)
            Me.AsignarCargueButton.TabIndex = 29
            Me.AsignarCargueButton.Text = "Asignar"
            Me.AsignarCargueButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AsignarCargueButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CerrarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.cancelar
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(332, 478)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(88, 33)
            Me.CerrarButton.TabIndex = 28
            Me.CerrarButton.Text = "Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'CorreccionCapturaPanel
            '
            Me.CorreccionCapturaPanel.Controls.Add(Me.CorreccionCapturaNumericUpDown)
            Me.CorreccionCapturaPanel.Controls.Add(Me.Label12)
            Me.CorreccionCapturaPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.CorreccionCapturaPanel.Location = New System.Drawing.Point(3, 413)
            Me.CorreccionCapturaPanel.Name = "CorreccionCapturaPanel"
            Me.CorreccionCapturaPanel.Size = New System.Drawing.Size(420, 29)
            Me.CorreccionCapturaPanel.TabIndex = 49
            '
            'CorreccionCapturaNumericUpDown
            '
            Me.CorreccionCapturaNumericUpDown.Location = New System.Drawing.Point(161, 3)
            Me.CorreccionCapturaNumericUpDown.Name = "CorreccionCapturaNumericUpDown"
            Me.CorreccionCapturaNumericUpDown.Size = New System.Drawing.Size(120, 20)
            Me.CorreccionCapturaNumericUpDown.TabIndex = 30
            Me.CorreccionCapturaNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'Label12
            '
            Me.Label12.AutoSize = True
            Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label12.Location = New System.Drawing.Point(4, 5)
            Me.Label12.Name = "Label12"
            Me.Label12.Size = New System.Drawing.Size(120, 13)
            Me.Label12.TabIndex = 31
            Me.Label12.Text = "Corrección Captura:"
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label6.Location = New System.Drawing.Point(4, 5)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(107, 13)
            Me.Label6.TabIndex = 31
            Me.Label6.Text = "Validacion Listas:"
            '
            'ValidacionListasNumericUpDown
            '
            Me.ValidacionListasNumericUpDown.Location = New System.Drawing.Point(161, 3)
            Me.ValidacionListasNumericUpDown.Name = "ValidacionListasNumericUpDown"
            Me.ValidacionListasNumericUpDown.Size = New System.Drawing.Size(120, 20)
            Me.ValidacionListasNumericUpDown.TabIndex = 30
            Me.ValidacionListasNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            'ValidacionListasPanel
            '
            Me.ValidacionListasPanel.Controls.Add(Me.ValidacionListasNumericUpDown)
            Me.ValidacionListasPanel.Controls.Add(Me.Label6)
            Me.ValidacionListasPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.ValidacionListasPanel.Location = New System.Drawing.Point(3, 384)
            Me.ValidacionListasPanel.Name = "ValidacionListasPanel"
            Me.ValidacionListasPanel.Size = New System.Drawing.Size(420, 29)
            Me.ValidacionListasPanel.TabIndex = 46
            '
            'FormReasignarPaqueteCantidad
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(451, 541)
            Me.Controls.Add(Me.GroupBoxProcesamiento)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormReasignarPaqueteCantidad"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Reasignación de Cargues"
            Me.GroupBoxProcesamiento.ResumeLayout(False)
            Me.ValidacionesPanel.ResumeLayout(False)
            Me.ValidacionesPanel.PerformLayout()
            CType(Me.ValidacionesNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ReprocesoPanel.ResumeLayout(False)
            Me.ReprocesoPanel.PerformLayout()
            CType(Me.ReprocesoNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
            Me.CalidadPanel.ResumeLayout(False)
            Me.CalidadPanel.PerformLayout()
            CType(Me.CalidadNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
            Me.TerceraCapturaPanel.ResumeLayout(False)
            Me.TerceraCapturaPanel.PerformLayout()
            CType(Me.TerceraCapturaNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SegundaCapturaPanel.ResumeLayout(False)
            Me.SegundaCapturaPanel.PerformLayout()
            CType(Me.SegundaCapturaNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
            Me.CapturaPanel.ResumeLayout(False)
            Me.CapturaPanel.PerformLayout()
            CType(Me.CapturaNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
            Me.PreCapturaPanel.ResumeLayout(False)
            Me.PreCapturaPanel.PerformLayout()
            CType(Me.PreCapturaNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
            Me.RetenidosPanel.ResumeLayout(False)
            Me.RetenidosPanel.PerformLayout()
            CType(Me.RetenidosNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
            Me.IndexacionPanel.ResumeLayout(False)
            Me.IndexacionPanel.PerformLayout()
            CType(Me.IndexacionNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.CorreccionCapturaPanel.ResumeLayout(False)
            Me.CorreccionCapturaPanel.PerformLayout()
            CType(Me.CorreccionCapturaNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.ValidacionListasNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ValidacionListasPanel.ResumeLayout(False)
            Me.ValidacionListasPanel.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBoxProcesamiento As System.Windows.Forms.GroupBox
        Friend WithEvents LabelSedeProcesamiento As System.Windows.Forms.Label
        Friend WithEvents CentroDeProcesamientoLabel As System.Windows.Forms.Label
        Friend WithEvents CentroProcesamientoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents SedeProcesamientoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents AsignarCargueButton As System.Windows.Forms.Button
        Friend WithEvents ValidacionesPanel As System.Windows.Forms.Panel
        Friend WithEvents ValidacionesNumericUpDown As System.Windows.Forms.NumericUpDown
        Friend WithEvents Label9 As System.Windows.Forms.Label
        Friend WithEvents ReprocesoPanel As System.Windows.Forms.Panel
        Friend WithEvents ReprocesoNumericUpDown As System.Windows.Forms.NumericUpDown
        Friend WithEvents Label8 As System.Windows.Forms.Label
        Friend WithEvents CalidadPanel As System.Windows.Forms.Panel
        Friend WithEvents CalidadNumericUpDown As System.Windows.Forms.NumericUpDown
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents TerceraCapturaPanel As System.Windows.Forms.Panel
        Friend WithEvents TerceraCapturaNumericUpDown As System.Windows.Forms.NumericUpDown
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents SegundaCapturaPanel As System.Windows.Forms.Panel
        Friend WithEvents SegundaCapturaNumericUpDown As System.Windows.Forms.NumericUpDown
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents CapturaPanel As System.Windows.Forms.Panel
        Friend WithEvents CapturaNumericUpDown As System.Windows.Forms.NumericUpDown
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents PreCapturaPanel As System.Windows.Forms.Panel
        Friend WithEvents PreCapturaNumericUpDown As System.Windows.Forms.NumericUpDown
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents RetenidosPanel As System.Windows.Forms.Panel
        Friend WithEvents RetenidosNumericUpDown As System.Windows.Forms.NumericUpDown
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents IndexacionPanel As System.Windows.Forms.Panel
        Friend WithEvents IndexacionNumericUpDown As System.Windows.Forms.NumericUpDown
        Friend WithEvents IndexacionLabel As System.Windows.Forms.Label
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents CorreccionCapturaPanel As System.Windows.Forms.Panel
        Friend WithEvents CorreccionCapturaNumericUpDown As System.Windows.Forms.NumericUpDown
        Friend WithEvents Label12 As System.Windows.Forms.Label
        Friend WithEvents ValidacionListasPanel As System.Windows.Forms.Panel
        Friend WithEvents ValidacionListasNumericUpDown As System.Windows.Forms.NumericUpDown
        Friend WithEvents Label6 As System.Windows.Forms.Label
    End Class
End Namespace