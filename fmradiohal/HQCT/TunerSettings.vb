Option Strict On
Imports System
Imports System.Text


Namespace HQCT
    Friend Class TunerSettings
        Private mSettings(96) As Byte

        Private com As Communication


        Public Sub New(ByRef com As Communication)
            Me.com = com
        End Sub 'New


        Public Property Settings() As Byte()
            Get
                Return mSettings
            End Get
            Set(ByVal Value As Byte())
                mSettings = Value
            End Set
        End Property
    End Class 'TunerSettings
End Namespace 'HQCT