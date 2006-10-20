Option Strict On
Public Delegate Sub WM_HID_EVENT_Delegate(ByRef m As System.Windows.Forms.Message)
Public Class HIDForm
    Inherits System.Windows.Forms.Form

#Region " Vom Windows Form Designer generierter Code "
    Public Event WM_HID_EVENT As WM_HID_EVENT_Delegate
    Public Sub New()
        MyBase.New()

        ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
        InitializeComponent()

        ' Initialisierungen nach dem Aufruf InitializeComponent() hinzufügen

    End Sub

    ' Die Form überschreibt den Löschvorgang der Basisklasse, um Komponenten zu bereinigen.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    ' Für Windows Form-Designer erforderlich
    Private components As System.ComponentModel.IContainer

    'HINWEIS: Die folgende Prozedur ist für den Windows Form-Designer erforderlich
    'Sie kann mit dem Windows Form-Designer modifiziert werden.
    'Verwenden Sie nicht den Code-Editor zur Bearbeitung.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        '
        'HIDForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(152, 29)
        Me.ControlBox = False
        Me.Name = "HIDForm"
        Me.ShowInTaskbar = False
        Me.Text = "HIDForm"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized

    End Sub

#End Region

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)

        If m.Msg = HQCT.mcHID.WM_HID_EVENT Then

            RaiseEvent WM_HID_EVENT(m)
            m.Result = IntPtr.Zero

            Exit Sub
        End If

        ' Let the base form process the message.
        MyBase.WndProc(m)
    End Sub
End Class
