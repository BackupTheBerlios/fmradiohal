Public Class Form1
    Inherits System.Windows.Forms.Form


    Public WithEvents mFMRadioHal As New FMRadio.FMRadioHAL.C_FMRadioHAL

    Public WithEvents mRDSInterpreter As New RDSInterpreter.RDSInterpreter.Interpreter

#Region " Vom Windows Form Designer generierter Code "



    Public Sub New()
        MyBase.New()

        ' Dieser Aufruf ist f�r den Windows Form-Designer erforderlich.
        InitializeComponent()

        ' Initialisierungen nach dem Aufruf InitializeComponent() hinzuf�gen


        Me.Timer1.Enabled = True

    End Sub

    ' Die Form �berschreibt den L�schvorgang der Basisklasse, um Komponenten zu bereinigen.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    ' F�r Windows Form-Designer erforderlich
    Private components As System.ComponentModel.IContainer

    'HINWEIS: Die folgende Prozedur ist f�r den Windows Form-Designer erforderlich
    'Sie kann mit dem Windows Form-Designer modifiziert werden.
    'Verwenden Sie nicht den Code-Editor zur Bearbeitung.
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents RdsInterpreter_Ctrl1 As HALTest.RDSInterpreter_Ctrl
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents FStxt As System.Windows.Forms.Label

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.RdsInterpreter_Ctrl1 = New HALTest.RDSInterpreter_Ctrl
        Me.Label1 = New System.Windows.Forms.Label
        Me.FStxt = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(104, 16)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(96, 20)
        Me.TextBox1.TabIndex = 2
        Me.TextBox1.Text = "8930"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(16, 16)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(80, 24)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "SetFreq"
        '
        'Timer1
        '
        '
        'RdsInterpreter_Ctrl1
        '
        Me.RdsInterpreter_Ctrl1.Location = New System.Drawing.Point(16, 56)
        Me.RdsInterpreter_Ctrl1.Name = "RdsInterpreter_Ctrl1"
        Me.RdsInterpreter_Ctrl1.Size = New System.Drawing.Size(696, 696)
        Me.RdsInterpreter_Ctrl1.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(208, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 24)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Quality"
        '
        'FStxt
        '
        Me.FStxt.Location = New System.Drawing.Point(248, 16)
        Me.FStxt.Name = "FStxt"
        Me.FStxt.Size = New System.Drawing.Size(120, 24)
        Me.FStxt.TabIndex = 5
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(712, 757)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RdsInterpreter_Ctrl1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.FStxt)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        mFMRadioHal.Freq = New FMRadio.FMRadioHAL.Frequency(CShort(TextBox1.Text))
    End Sub

    Private Sub mFMRadioHal_RDSRAWMessageavailable(ByRef RDSRAWMessage As FMRadio.FMRadioHAL.stRDSRAWMessage) Handles mFMRadioHal.RDSRAWMessageavailable
        mRDSInterpreter.decode(RDSRAWMessage)
        RdsInterpreter_Ctrl1.UpdateRDS(mRDSInterpreter)
        'RDSTextlbl.Text = mRDSInterpreter.CurrentProgram.RadioText()
        'PSNlbl.Text = mRDSInterpreter.CurrentProgram.ProgramServiceName()
        'PTName.Text = mRDSInterpreter.CurrentProgram.ProgramTypeName
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'If mFMRadioHal.mCom.ReportList Is Nothing Then
        'Else
        '    replbl.Text = mFMRadioHal.mCom.ReportList.Counter
        '    intlbl.Text = CStr(mRDSInterpreter.BlocksCounter(0))
        '    lbl4.Text = CStr(mFMRadioHal.GetRBC(0)) + " " + CStr(mFMRadioHal.GetRBC(1)) + " " + CStr(mFMRadioHal.GetRBC(2)) + " "
        '    Label3.Text = CStr(mFMRadioHal.mCom.Counter)
        '    Label7.Text = CStr(mFMRadioHal.GetRepIndex)
        'End If
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub lbl4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim Test As New System.DateTime(2006, 10, 14)


        mFMRadioHal.Connect()
    End Sub

    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        mFMRadioHal.DisConnect()
    End Sub

    Private Sub mFMRadioHal_FieldStrength(ByVal Level As Short) Handles mFMRadioHal.FieldStrength
        FStxt.Text = CStr(Level)
    End Sub
End Class
