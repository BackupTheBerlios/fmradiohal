Public Class RDSInterpreter_Ctrl
    Inherits System.Windows.Forms.UserControl

    Private mRDSInterpreter As RDSInterpreter.RDSInterpreter.Interpreter

#Region " Vom Windows Form Designer generierter Code "

    Public Sub New()
        MyBase.New()

        ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
        InitializeComponent()

        ' Initialisierungen nach dem Aufruf InitializeComponent() hinzufügen
        '    mRDSInterpreter = lRDSInterpreter
    End Sub

    'UserControl überschreibt den Löschvorgang zum Bereinigen der Komponentenliste.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    Public Sub UpdateRDS(ByRef lRDSInterpreter As RDSInterpreter.RDSInterpreter.Interpreter)
        Dim iEON As DictionaryEntry
        Dim PIEON As RDSInterpreter.RDSInterpreter.ProgramIdentifier
        Dim iItem As Integer


        Me.ProgramIdentifier_Ctrl1.UpdatePI(lRDSInterpreter.CurrentProgram)
        'CF
        Exit Sub

        iItem = EONLB.SelectedIndex
        EONLB.Items.Clear()
        For Each iEON In lRDSInterpreter.EEONList
            EONLB.Items.Add(CType(iEON.Value, RDSInterpreter.RDSInterpreter.ProgramIdentifier).PiCode)
        Next
        If EONLB.Items.Count <= iItem Then
            iItem = -1
        End If
        EONLB.SelectedIndex = iItem
        If EONLB.SelectedIndex >= 0 Then

            'Debug.WriteLine(CShort(EONLB.Items.Item(EONLB.SelectedIndex)))
            EONProgramIdentifier_Ctrl.UpdatePI(lRDSInterpreter.EEONList.Item(EONLB.Items.Item(EONLB.SelectedIndex)))
        End If
    End Sub

    ' Für Windows Form-Designer erforderlich
    Private components As System.ComponentModel.IContainer

    'HINWEIS: Die folgende Prozedur ist für den Windows Form-Designer erforderlich
    'Sie kann mit dem Windows Form-Designer modifiziert werden.
    'Verwenden Sie nicht den Code-Editor zur Bearbeitung.
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ProgramIdentifier_Ctrl1 As HALTest.ProgramIdentifier_Ctrl
    Friend WithEvents EONLB As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents EONProgramIdentifier_Ctrl As HALTest.ProgramIdentifier_Ctrl
    Friend WithEvents Label2 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.EONProgramIdentifier_Ctrl = New HALTest.ProgramIdentifier_Ctrl
        Me.ProgramIdentifier_Ctrl1 = New HALTest.ProgramIdentifier_Ctrl
        Me.EONLB = New System.Windows.Forms.ListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.EONProgramIdentifier_Ctrl)
        Me.GroupBox1.Controls.Add(Me.ProgramIdentifier_Ctrl1)
        Me.GroupBox1.Controls.Add(Me.EONLB)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(664, 688)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "RDSInterpreter"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 168)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 16)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "CurrentProgram"
        '
        'EONProgramIdentifier_Ctrl
        '
        Me.EONProgramIdentifier_Ctrl.Location = New System.Drawing.Point(112, 352)
        Me.EONProgramIdentifier_Ctrl.Name = "EONProgramIdentifier_Ctrl"
        Me.EONProgramIdentifier_Ctrl.Size = New System.Drawing.Size(544, 328)
        Me.EONProgramIdentifier_Ctrl.TabIndex = 19
        '
        'ProgramIdentifier_Ctrl1
        '
        Me.ProgramIdentifier_Ctrl1.Location = New System.Drawing.Point(112, 16)
        Me.ProgramIdentifier_Ctrl1.Name = "ProgramIdentifier_Ctrl1"
        Me.ProgramIdentifier_Ctrl1.Size = New System.Drawing.Size(544, 336)
        Me.ProgramIdentifier_Ctrl1.TabIndex = 0
        '
        'EONLB
        '
        Me.EONLB.Location = New System.Drawing.Point(8, 384)
        Me.EONLB.Name = "EONLB"
        Me.EONLB.Size = New System.Drawing.Size(96, 199)
        Me.EONLB.TabIndex = 17
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(16, 360)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 16)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "EON"
        '
        'RDSInterpreter_Ctrl
        '
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "RDSInterpreter_Ctrl"
        Me.Size = New System.Drawing.Size(672, 696)
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
