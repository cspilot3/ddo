Imports System.Drawing
Imports System.Windows.Forms

Namespace DesktopTab

    Public Class DesktopTabControl
        Inherits TabControl

#Region " Declaraciones "

        Private ColorVarBackColor As Color = SystemColors.ActiveBorder

#End Region

#Region " Propiedades "

        Public Overrides Property BackColor() As Color
            Get
                Return ColorVarBackColor
            End Get
            Set(ByVal Value As Color)
                ColorVarBackColor = Value
            End Set
        End Property

#End Region

    End Class
End Namespace