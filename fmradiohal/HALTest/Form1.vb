Imports System.Runtime.InteropServices
Imports System.Reflection

Public Class Form1
    Inherits System.Windows.Forms.Form


    Public WithEvents mFMRadioHal As New FMRadio.FMRadioHAL.C_FMRadioHAL

    Public WithEvents mRDSInterpreter As New RDSInterpreter.RDSInterpreter.Interpreter

#Region " Vom Windows Form Designer generierter Code "



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
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents RdsInterpreter_Ctrl1 As HALTest.RDSInterpreter_Ctrl
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents FStxt As System.Windows.Forms.Label
    Friend WithEvents MinusBtn As System.Windows.Forms.Button
    Friend WithEvents PlusBtn As System.Windows.Forms.Button
    Friend WithEvents RadioTextChangeLbl As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.RdsInterpreter_Ctrl1 = New HALTest.RDSInterpreter_Ctrl
        Me.Label1 = New System.Windows.Forms.Label
        Me.FStxt = New System.Windows.Forms.Label
        Me.MinusBtn = New System.Windows.Forms.Button
        Me.PlusBtn = New System.Windows.Forms.Button
        Me.RadioTextChangeLbl = New System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(120, 16)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(96, 20)
        Me.TextBox1.TabIndex = 2
        Me.TextBox1.Text = "8930"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(48, 16)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(64, 24)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "SetFreq"
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
        Me.Label1.Location = New System.Drawing.Point(280, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 24)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Quality"
        '
        'FStxt
        '
        Me.FStxt.Location = New System.Drawing.Point(328, 16)
        Me.FStxt.Name = "FStxt"
        Me.FStxt.Size = New System.Drawing.Size(120, 24)
        Me.FStxt.TabIndex = 5
        '
        'MinusBtn
        '
        Me.MinusBtn.Location = New System.Drawing.Point(0, 32)
        Me.MinusBtn.Name = "MinusBtn"
        Me.MinusBtn.Size = New System.Drawing.Size(40, 16)
        Me.MinusBtn.TabIndex = 6
        Me.MinusBtn.Text = "-"
        '
        'PlusBtn
        '
        Me.PlusBtn.Location = New System.Drawing.Point(224, 32)
        Me.PlusBtn.Name = "PlusBtn"
        Me.PlusBtn.Size = New System.Drawing.Size(40, 16)
        Me.PlusBtn.TabIndex = 6
        Me.PlusBtn.Text = "+"
        '
        'RadioTextChangeLbl
        '
        Me.RadioTextChangeLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RadioTextChangeLbl.Location = New System.Drawing.Point(464, 8)
        Me.RadioTextChangeLbl.Name = "RadioTextChangeLbl"
        Me.RadioTextChangeLbl.Size = New System.Drawing.Size(224, 40)
        Me.RadioTextChangeLbl.TabIndex = 7
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(0, 8)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(40, 16)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "<<"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(224, 8)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(40, 16)
        Me.Button3.TabIndex = 9
        Me.Button3.Text = ">>"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(712, 757)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.RadioTextChangeLbl)
        Me.Controls.Add(Me.MinusBtn)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RdsInterpreter_Ctrl1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.FStxt)
        Me.Controls.Add(Me.PlusBtn)
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

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
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

    Private Sub PlusBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlusBtn.Click
        mFMRadioHal.FreqUp()
        TextBox1.Text = CStr(mFMRadioHal.Freq.Value)
    End Sub

    Private Sub MinusBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MinusBtn.Click
        mFMRadioHal.FreqDown()
        TextBox1.Text = CStr(mFMRadioHal.Freq.Value)
    End Sub



    Private Sub mFMRadioHal_Stereo(ByVal Stereo_Pilot_Detected As Boolean) Handles mFMRadioHal.Stereo

    End Sub

    Private Sub mRDSInterpreter_RadioTextChange(ByVal oldText() As Char) Handles mRDSInterpreter.RadioTextChange
        RadioTextChangeLbl.Text = oldText
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        mFMRadioHal.AutoTune(FMRadio.FMRadioHAL.enDirections.UP, 10000)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        mFMRadioHal.AutoTune(FMRadio.FMRadioHAL.enDirections.Down, 10000)
    End Sub
End Class








