
Public Class ProgramIdentifier_Ctrl
    Inherits System.Windows.Forms.UserControl

#Region " Vom Windows Form Designer generierter Code "

    Public Sub New()
        MyBase.New()

        ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
        InitializeComponent()

        ' Initialisierungen nach dem Aufruf InitializeComponent() hinzufügen

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

    ' Für Windows Form-Designer erforderlich
    Private components As System.ComponentModel.IContainer

    'HINWEIS: Die folgende Prozedur ist für den Windows Form-Designer erforderlich
    'Sie kann mit dem Windows Form-Designer modifiziert werden.
    'Verwenden Sie nicht den Code-Editor zur Bearbeitung.
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ProgramServiceNameLbl As System.Windows.Forms.Label
    Friend WithEvents RadioTextLbl As System.Windows.Forms.Label
    Friend WithEvents ProgramTypeNameLbl As System.Windows.Forms.Label
    Friend WithEvents RBDSChk As System.Windows.Forms.CheckBox
    Friend WithEvents ProgramTypeLbl As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ProgramTypeNameABChk As System.Windows.Forms.CheckBox
    Friend WithEvents RadioTextABChk As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents AlternativeFrequencyListsLB As System.Windows.Forms.ListBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents AFLB As System.Windows.Forms.ListBox
    Friend WithEvents NrLbl As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents FreqTB As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents LanguageCode As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents CountryCode As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents AreaCoverage As System.Windows.Forms.Label
    Friend WithEvents TP As System.Windows.Forms.CheckBox
    Friend WithEvents TA As System.Windows.Forms.CheckBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Audio As System.Windows.Forms.Label
    Friend WithEvents DecoderIdentification As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents RDSDateTime As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents ExtendedCounTryCode As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.RadioTextLbl = New System.Windows.Forms.Label
        Me.ProgramServiceNameLbl = New System.Windows.Forms.Label
        Me.label4 = New System.Windows.Forms.Label
        Me.ProgramTypeNameLbl = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.FreqTB = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.NrLbl = New System.Windows.Forms.Label
        Me.AFLB = New System.Windows.Forms.ListBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.AlternativeFrequencyListsLB = New System.Windows.Forms.ListBox
        Me.RadioTextABChk = New System.Windows.Forms.CheckBox
        Me.RBDSChk = New System.Windows.Forms.CheckBox
        Me.ProgramTypeLbl = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.ProgramTypeNameABChk = New System.Windows.Forms.CheckBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.LanguageCode = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.CountryCode = New System.Windows.Forms.Label
        Me.AreaCoverage = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.TP = New System.Windows.Forms.CheckBox
        Me.TA = New System.Windows.Forms.CheckBox
        Me.Audio = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.DecoderIdentification = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.RDSDateTime = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.ExtendedCounTryCode = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 16)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "RadioText"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 24)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "ProgramServiceName"
        '
        'RadioTextLbl
        '
        Me.RadioTextLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RadioTextLbl.Location = New System.Drawing.Point(96, 56)
        Me.RadioTextLbl.Name = "RadioTextLbl"
        Me.RadioTextLbl.Size = New System.Drawing.Size(160, 40)
        Me.RadioTextLbl.TabIndex = 18
        '
        'ProgramServiceNameLbl
        '
        Me.ProgramServiceNameLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ProgramServiceNameLbl.Location = New System.Drawing.Point(96, 24)
        Me.ProgramServiceNameLbl.Name = "ProgramServiceNameLbl"
        Me.ProgramServiceNameLbl.Size = New System.Drawing.Size(160, 16)
        Me.ProgramServiceNameLbl.TabIndex = 19
        '
        'label4
        '
        Me.label4.Location = New System.Drawing.Point(8, 112)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(88, 24)
        Me.label4.TabIndex = 20
        Me.label4.Text = "ProgramTypeName"
        '
        'ProgramTypeNameLbl
        '
        Me.ProgramTypeNameLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ProgramTypeNameLbl.Location = New System.Drawing.Point(96, 112)
        Me.ProgramTypeNameLbl.Name = "ProgramTypeNameLbl"
        Me.ProgramTypeNameLbl.Size = New System.Drawing.Size(160, 32)
        Me.ProgramTypeNameLbl.TabIndex = 21
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TA)
        Me.GroupBox1.Controls.Add(Me.TP)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.FreqTB)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.NrLbl)
        Me.GroupBox1.Controls.Add(Me.AFLB)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.AlternativeFrequencyListsLB)
        Me.GroupBox1.Controls.Add(Me.RadioTextABChk)
        Me.GroupBox1.Controls.Add(Me.RBDSChk)
        Me.GroupBox1.Controls.Add(Me.ProgramTypeNameLbl)
        Me.GroupBox1.Controls.Add(Me.label4)
        Me.GroupBox1.Controls.Add(Me.ProgramServiceNameLbl)
        Me.GroupBox1.Controls.Add(Me.RadioTextLbl)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.ProgramTypeLbl)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.ProgramTypeNameABChk)
        Me.GroupBox1.Controls.Add(Me.LanguageCode)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.CountryCode)
        Me.GroupBox1.Controls.Add(Me.AreaCoverage)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Audio)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.DecoderIdentification)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.RDSDateTime)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.ExtendedCounTryCode)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(536, 328)
        Me.GroupBox1.TabIndex = 22
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "ProgramIdentifier"
        '
        'FreqTB
        '
        Me.FreqTB.Location = New System.Drawing.Point(184, 200)
        Me.FreqTB.Name = "FreqTB"
        Me.FreqTB.Size = New System.Drawing.Size(72, 20)
        Me.FreqTB.TabIndex = 30
        Me.FreqTB.Text = ""
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(96, 200)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 16)
        Me.Label7.TabIndex = 29
        Me.Label7.Text = "AssignedFreq"
        '
        'NrLbl
        '
        Me.NrLbl.Location = New System.Drawing.Point(456, 8)
        Me.NrLbl.Name = "NrLbl"
        Me.NrLbl.Size = New System.Drawing.Size(40, 32)
        Me.NrLbl.TabIndex = 28
        '
        'AFLB
        '
        Me.AFLB.Location = New System.Drawing.Point(424, 40)
        Me.AFLB.Name = "AFLB"
        Me.AFLB.Size = New System.Drawing.Size(104, 160)
        Me.AFLB.TabIndex = 27
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(416, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 16)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "AFList"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(344, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 16)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "AFLists"
        '
        'AlternativeFrequencyListsLB
        '
        Me.AlternativeFrequencyListsLB.Location = New System.Drawing.Point(336, 40)
        Me.AlternativeFrequencyListsLB.Name = "AlternativeFrequencyListsLB"
        Me.AlternativeFrequencyListsLB.Size = New System.Drawing.Size(80, 160)
        Me.AlternativeFrequencyListsLB.TabIndex = 24
        '
        'RadioTextABChk
        '
        Me.RadioTextABChk.Location = New System.Drawing.Point(264, 56)
        Me.RadioTextABChk.Name = "RadioTextABChk"
        Me.RadioTextABChk.Size = New System.Drawing.Size(64, 32)
        Me.RadioTextABChk.TabIndex = 23
        Me.RadioTextABChk.Text = "RadioTextAB"
        '
        'RBDSChk
        '
        Me.RBDSChk.Location = New System.Drawing.Point(8, 200)
        Me.RBDSChk.Name = "RBDSChk"
        Me.RBDSChk.Size = New System.Drawing.Size(56, 16)
        Me.RBDSChk.TabIndex = 22
        Me.RBDSChk.Text = "RBDS"
        '
        'ProgramTypeLbl
        '
        Me.ProgramTypeLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ProgramTypeLbl.Location = New System.Drawing.Point(96, 160)
        Me.ProgramTypeLbl.Name = "ProgramTypeLbl"
        Me.ProgramTypeLbl.Size = New System.Drawing.Size(160, 32)
        Me.ProgramTypeLbl.TabIndex = 21
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 160)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 24)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "ProgramType"
        '
        'ProgramTypeNameABChk
        '
        Me.ProgramTypeNameABChk.Location = New System.Drawing.Point(264, 112)
        Me.ProgramTypeNameABChk.Name = "ProgramTypeNameABChk"
        Me.ProgramTypeNameABChk.Size = New System.Drawing.Size(72, 40)
        Me.ProgramTypeNameABChk.TabIndex = 23
        Me.ProgramTypeNameABChk.Text = "ProgramTypeNameAB"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(8, 232)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 16)
        Me.Label8.TabIndex = 31
        Me.Label8.Text = "LanguageCode"
        '
        'LanguageCode
        '
        Me.LanguageCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LanguageCode.Location = New System.Drawing.Point(96, 232)
        Me.LanguageCode.Name = "LanguageCode"
        Me.LanguageCode.Size = New System.Drawing.Size(160, 16)
        Me.LanguageCode.TabIndex = 21
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(8, 256)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 16)
        Me.Label9.TabIndex = 31
        Me.Label9.Text = "CountryCode"
        '
        'CountryCode
        '
        Me.CountryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CountryCode.Location = New System.Drawing.Point(96, 256)
        Me.CountryCode.Name = "CountryCode"
        Me.CountryCode.Size = New System.Drawing.Size(160, 16)
        Me.CountryCode.TabIndex = 21
        '
        'AreaCoverage
        '
        Me.AreaCoverage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AreaCoverage.Location = New System.Drawing.Point(96, 280)
        Me.AreaCoverage.Name = "AreaCoverage"
        Me.AreaCoverage.Size = New System.Drawing.Size(160, 16)
        Me.AreaCoverage.TabIndex = 21
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(8, 280)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 16)
        Me.Label11.TabIndex = 31
        Me.Label11.Text = "AreaCoverage"
        '
        'TP
        '
        Me.TP.Location = New System.Drawing.Point(264, 152)
        Me.TP.Name = "TP"
        Me.TP.Size = New System.Drawing.Size(40, 24)
        Me.TP.TabIndex = 32
        Me.TP.Text = "TP"
        '
        'TA
        '
        Me.TA.Location = New System.Drawing.Point(264, 176)
        Me.TA.Name = "TA"
        Me.TA.Size = New System.Drawing.Size(40, 24)
        Me.TA.TabIndex = 33
        Me.TA.Text = "TA"
        '
        'Audio
        '
        Me.Audio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Audio.Location = New System.Drawing.Point(96, 304)
        Me.Audio.Name = "Audio"
        Me.Audio.Size = New System.Drawing.Size(160, 16)
        Me.Audio.TabIndex = 21
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(8, 304)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(88, 16)
        Me.Label12.TabIndex = 31
        Me.Label12.Text = "Audio"
        '
        'DecoderIdentification
        '
        Me.DecoderIdentification.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DecoderIdentification.Location = New System.Drawing.Point(272, 224)
        Me.DecoderIdentification.Name = "DecoderIdentification"
        Me.DecoderIdentification.Size = New System.Drawing.Size(256, 16)
        Me.DecoderIdentification.TabIndex = 21
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(272, 208)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(112, 16)
        Me.Label10.TabIndex = 31
        Me.Label10.Text = "DecoderIdentification"
        '
        'RDSDateTime
        '
        Me.RDSDateTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RDSDateTime.Location = New System.Drawing.Point(352, 256)
        Me.RDSDateTime.Name = "RDSDateTime"
        Me.RDSDateTime.Size = New System.Drawing.Size(176, 16)
        Me.RDSDateTime.TabIndex = 21
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(272, 256)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(80, 16)
        Me.Label13.TabIndex = 31
        Me.Label13.Text = "RDSDateTime"
        '
        'ExtendedCounTryCode
        '
        Me.ExtendedCounTryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ExtendedCounTryCode.Location = New System.Drawing.Point(352, 296)
        Me.ExtendedCounTryCode.Name = "ExtendedCounTryCode"
        Me.ExtendedCounTryCode.Size = New System.Drawing.Size(176, 16)
        Me.ExtendedCounTryCode.TabIndex = 21
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(272, 280)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(112, 24)
        Me.Label14.TabIndex = 31
        Me.Label14.Text = "ExtendedCounTryCode"
        '
        'ProgramIdentifier_Ctrl
        '
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ProgramIdentifier_Ctrl"
        Me.Size = New System.Drawing.Size(544, 336)
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub UpdatePI(ByRef lPI As RDSInterpreter.RDSInterpreter.ProgramIdentifier)
        Dim iAFD As DictionaryEntry
        Dim lAF As RDSInterpreter.RDSInterpreter.AlternativeFrequency
        Dim iItem As Integer


        ProgramServiceNameLbl.Text = lPI.ProgramServiceName
        RadioTextLbl.Text = lPI.RadioText
        ProgramTypeNameLbl.Text = lPI.ProgramTypeName
        ProgramTypeLbl.Text = [Enum].GetName(lPI.ProgramType.GetType, lPI.ProgramType)
        ProgramTypeNameABChk.Checked = lPI.ProgramTypeNameABFlag
        RadioTextABChk.Checked = lPI.RadioTextABFlag
        'lPI.MusicSpeechSwitch()
        RBDSChk.Checked = lPI.RBDS
        GroupBox1.Text = "ProgramIdentifier: " + CStr(lPI.PiCode)
        FreqTB.Text = lPI.TuningFrequency.DisplayFormat
        LanguageCode.Text = [Enum].GetName(lPI.LanguageCode.GetType, lPI.LanguageCode)
        CountryCode.Text = CStr(lPI.CountryCode)
        AreaCoverage.Text = [Enum].GetName(lPI.AreaCoverage.GetType, lPI.AreaCoverage)
        TA.Checked = lPI.TrafficAnnouncement
        TP.Checked = lPI.TrafficProgram
        Audio.Text = [Enum].GetName(lPI.MusicSpeechSwitch.GetType, lPI.MusicSpeechSwitch)
        DecoderIdentification.Text = [Enum].GetName(lPI.DecoderIdentification.GetType, lPI.DecoderIdentification)
        RDSDateTime.Text = CStr(lPI.RDSDateTime)
        ExtendedCounTryCode.Text = [Enum].GetName(lPI.ExtendedCounTryCode.GetType, lPI.ExtendedCounTryCode)

        iItem = AlternativeFrequencyListsLB.SelectedIndex
        AlternativeFrequencyListsLB.Items.Clear()
        For Each iAFD In lPI.AlternativeFrequencyLists
            AlternativeFrequencyListsLB.Items.Add(CType(iAFD.Key, FMRadio.FMRadioHAL.Frequency).DisplayFormat + " - " + CStr(CType(iAFD.Value, RDSInterpreter.RDSInterpreter.AlternativeFrequencyList).ReceiveCounter))
        Next iAFD

        If AlternativeFrequencyListsLB.Items.Count <= iItem Then
            iItem = -1
        End If

        'oops !!!
        If iItem >= 0 Then
            'If iItem >= AlternativeFrequencyListsLB.Items.Count - 1 Then
            AFLB.Items.Clear()
            'Debug.WriteLine(CStr(AlternativeFrequencyListsLB.Items.Item(iItem)))
            Dim s As String = CStr(AlternativeFrequencyListsLB.Items.Item(iItem))
            s = Trim(s.Split("-")(0))
            Dim f As New FMRadio.FMRadioHAL.Frequency(s)
            'If f.Value > 0 Then
            If Not (lPI.AlternativeFrequencyLists.Item(f) Is Nothing) Then
                NrLbl.Text = CStr(lPI.AlternativeFrequencyLists.Item(f).AlternativeFrequencies.Count) + " " + [Enum].GetName(GetType(RDSInterpreter.RDSInterpreter.enAFMethod), lPI.AlternativeFrequencyLists.Item(f).AFMethod)
                For Each iAFD In lPI.AlternativeFrequencyLists.Item(f).AlternativeFrequencies
                    lAF = CType(iAFD.Value, RDSInterpreter.RDSInterpreter.AlternativeFrequency)
                    Dim St As String = lAF.Frequency.DisplayFormat + " " + CStr(lAF.RegionalVariant)
                    If Not (lAF.LinkedFrequency Is Nothing) Then
                        St = St + " " + lAF.LinkedFrequency.DisplayFormat
                    End If
                    AFLB.Items.Add(St)
                    'Debug.WriteLine("TEST:" + lAF.Frequency.DisplayFormat)
                Next iAFD

            End If
            'End If
            'End If
        End If
        AlternativeFrequencyListsLB.SelectedIndex = iItem
    End Sub

End Class
