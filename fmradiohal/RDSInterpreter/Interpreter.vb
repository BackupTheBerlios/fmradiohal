Option Strict On

Imports System
Imports System.Text
Imports System.Diagnostics
Imports System.Runtime.InteropServices

Namespace RDSInterpreter

    Public Class ProgramListDictionary
        Inherits DictionaryBase

        Default Public Property Item(ByVal key As System.Int16) As ProgramIdentifier
            Get
                Return CType(Dictionary(key), [ProgramIdentifier])
            End Get
            Set(ByVal Value As [ProgramIdentifier])
                Dictionary(key) = Value
            End Set
        End Property

        Public ReadOnly Property Keys() As ICollection
            Get
                Return Dictionary.Keys
            End Get
        End Property

        Public ReadOnly Property Values() As ICollection
            Get
                Return Dictionary.Values
            End Get
        End Property

        Public Sub Add(ByVal key As System.Int16, ByVal value As [ProgramIdentifier])
            Dictionary.Add(key, value)
        End Sub 'Add

        Public Function Contains(ByVal key As System.Int16) As Boolean
            Return Dictionary.Contains(key)
        End Function 'Contains

        Public Sub Remove(ByVal key As System.Int16)
            Dictionary.Remove(key)
        End Sub 'Remove

    End Class


    Public Delegate Sub ProgramTypeNameChange_Delegate(ByVal oldText As Char())
    Public Delegate Sub RadioTextChange_Delegate(ByVal oldText As Char())

    <ComSourceInterfaces(GetType(I_Interpreter_Events))> _
        Public Class Interpreter
        Implements I_Interpreter


        Public Event ProgramTypeNameChange As ProgramTypeNameChange_Delegate
        Public Event RadioTextChange As RadioTextChange_Delegate
        Public RDSCharMap() As Char = {ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, _
ControlChars.Lf, ControlChars.NullChar, ControlChars.NullChar, _
ControlChars.Cr, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, ControlChars.NullChar, _
" "c, _
    "!"c, _
    ControlChars.Quote, _
    "#"c, _
    "¤"c, _
    "%"c, _
    "&"c, _
    "'"c, _
    "("c, _
    ")"c, _
    "*"c, _
    "+"c, _
    ","c, _
    "-"c, _
    "."c, _
    "/"c, _
    "0"c, _
    "1"c, _
    "2"c, _
    "3"c, _
    "4"c, _
    "5"c, _
    "6"c, _
    "7"c, _
    "8"c, _
    "9"c, _
    ":"c, _
    ";"c, _
    "<"c, _
    "="c, _
    ">"c, _
    "?"c, _
    "@"c, _
    "A"c, _
    "B"c, _
    "C"c, _
    "D"c, _
    "E"c, _
    "F"c, _
    "G"c, _
    "H"c, _
    "I"c, _
    "J"c, _
    "K"c, _
    "L"c, _
    "M"c, _
    "N"c, _
    "O"c, _
    "P"c, _
    "Q"c, _
    "R"c, _
    "S"c, _
    "T"c, _
    "U"c, _
    "V"c, _
    "W"c, _
    "X"c, _
    "Y"c, _
    "Z"c, _
    "["c, _
    "\"c, _
    "]"c, _
    "─"c, _
    "_"c, _
    "║"c, _
    "a"c, _
    "b"c, _
    "c"c, _
    "d"c, _
    "e"c, _
    "f"c, _
    "g"c, _
    "h"c, _
    "i"c, _
    "j"c, _
    "k"c, _
    "l"c, _
    "m"c, _
    "n"c, _
    "o"c, _
    "p"c, _
    "q"c, _
    "r"c, _
    "s"c, _
    "t"c, _
    "u"c, _
    "v"c, _
    "w"c, _
    "x"c, _
    "y"c, _
    "z"c, _
    "{"c, _
    "│"c, _
    "}"c, _
    "¯"c, _
    " "c, _
    "á"c, _
    "à"c, _
    "é"c, _
    "è"c, _
    "í"c, _
    "ì"c, _
    "ó"c, _
    "ò"c, _
    "ú"c, _
    "ù"c, _
    "Ñ"c, _
    "Ç"c, _
    "Ş"c, _
    "β"c, _
    "¡"c, _
    "Ĳ"c, _
    "â"c, _
    "ä"c, _
    "ê"c, _
    "ë"c, _
    "î"c, _
    "ï"c, _
    "ô"c, _
    "ö"c, _
    "û"c, _
    "ü"c, _
    "ñ"c, _
    "ç"c, _
    "ş"c, _
    "ğ"c, _
    "¹"c, _
    "ĳ"c, _
    "ª"c, _
    "α"c, _
    "©"c, _
    "‰"c, _
    "Ğ"c, _
    "ě"c, _
    "ň"c, _
    "ő"c, _
    "π"c, _
    "Œ"c, _
    "₤"c, _
    "$"c, _
    "←"c, _
    "↑"c, _
    "→"c, _
    "↓"c, _
    "º"c, _
    "¹"c, _
    "²"c, _
    "³"c, _
    "±"c, _
    "İ"c, _
    "ń"c, _
    "ű"c, _
    "µ"c, _
    "¿"c, _
    "÷"c, _
    "°"c, _
    "¼"c, _
    "½"c, _
    "¾"c, _
    "§"c, _
    "Á"c, _
    "À"c, _
    "É"c, _
    "È"c, _
    "Í"c, _
    "Ì"c, _
    "Ó"c, _
    "Ò"c, _
    "Ú"c, _
    "Ù"c, _
    "Ř"c, _
    "Č"c, _
    "Š"c, _
    "Ž"c, _
    "Đ"c, _
    "Ŀ"c, _
    "Â"c, _
    "Ä"c, _
    "Ê"c, _
    "Ë"c, _
    "Î"c, _
    "Ï"c, _
    "Ô"c, _
    "Ö"c, _
    "Û"c, _
    "Ü"c, _
    "ř"c, _
    "č"c, _
    "š"c, _
    "ž"c, _
    "đ"c, _
    "ŀ"c, _
    "Ã"c, _
    "Å"c, _
    "Æ"c, _
    "Œ"c, _
    "ŷ"c, _
    "Ý"c, _
    "Õ"c, _
    "Ø"c, _
    "Þ"c, _
    "Ŋ"c, _
    "Ŕ"c, _
    "Ć"c, _
    "Ś"c, _
    "Ź"c, _
    "Ŧ"c, _
    "δ"c, _
    "ã"c, _
    "å"c, _
    "æ"c, _
    "œ"c, _
    "ŵ"c, _
    "ý"c, _
    "õ"c, _
    "ø"c, _
    "þ"c, _
    "ŋ"c, _
    "ŕ"c, _
    "ć"c, _
    "ś"c, _
    "ź"c, _
    "ŧ"c, " "c}


        Private mCurrentProgram As ProgramIdentifier = Nothing
        Private Block As System.Int16
        'Private alternativeFrequency As alternativeFrequency

        Private mblocksCounter(4) As Long
        Private piCounter As Integer = 0
        Private charCount As Integer = 0

        REM Enhanced (!) EON List
        Private mEEONList As ProgramListDictionary


        REM This is the EON list but could be enhanced by using a second tuner
        Public ReadOnly Property EEONList() As ProgramListDictionary Implements I_Interpreter.EEONList
            Get
                Return mEEONList
            End Get
        End Property



        Public Property CurrentProgram() As ProgramIdentifier Implements I_Interpreter.CurrentProgram
            Get
                Return mCurrentProgram
            End Get
            Set(ByVal Value As ProgramIdentifier)
                mCurrentProgram = Value
            End Set
        End Property

        Public Property BlocksCounter() As Long() Implements I_Interpreter.BlocksCounter
            Get
                Return mblocksCounter
            End Get
            Set(ByVal Value As Long())
                mblocksCounter = Value
            End Set
        End Property

        Public Sub decode(ByRef rdsGroupData As FMRadio.FMRadioHAL.stRDSRAWMessage) Implements I_Interpreter.decode
            'If bytes >= 2 Then

            Try

                BlocksCounter(0) += 1

                'CF
                CurrentProgram = getProgramIdentifier(rdsGroupData.Block0)
                'CurrentProgram.TuningFrequency = rdsGroupData.TunedFreq
                readPICode(rdsGroupData.Block0)
                'End If

                'If bytes >= 4 Then
                BlocksCounter(1) += 1

                readGroup(rdsGroupData.Block1)
                readGroupData(enBlockType.B, rdsGroupData.Block1)
                'End If

                'If bytes >= 6 Then
                BlocksCounter(2) += 1

                'If CurrentProgram.GroupVersion = enGroupVersionMap.A Then
                readGroupData(enBlockType.C, rdsGroupData.Block2)
                'Else
                'End If ' Ignoring second PI code block
                'End If
                'currentProgram = getProgramIdentifier(block);
                'readPICode(block);

                'If bytes >= 8 Then
                BlocksCounter(3) += 1

                readGroupData(enBlockType.D, rdsGroupData.Block3)
                'End If
            Catch
            End Try
            'disconnected for CF prerealease
            ' readCompleteGroup(rdsGroupData)
        End Sub 'decode


        '* Keep track of all the unique program identifiers and their data for faster
        '* UI updates.
        '

        Private Function getProgramIdentifier(ByVal piCode As System.Int16) As ProgramIdentifier

            ' if (currentProgram != null && currentProgram.PiCode != piCode)
            If (CurrentProgram Is Nothing) Then  'And CurrentProgram.PiCode <> piCode Then
                Debug.WriteLine(String.Format("PI Changed -> {0:x4}", piCode))
                piCounter = 0
                CurrentProgram = Nothing
                CurrentProgram = New ProgramIdentifier(piCode)
                'mEEONList = Nothing
                'mEEONList = New ProgramListDictionary
            Else
                If CurrentProgram.PiCode <> piCode Then
                    Debug.WriteLine(String.Format("PI Changed -> {0:x4}", piCode))
                    piCounter = 0
                    CurrentProgram.PiCode = piCode
                    'CurrentProgram = Nothing
                    'CurrentProgram = New ProgramIdentifier(piCode)
                    'mEEONList = Nothing
                    'mEEONList = New ProgramListDictionary
                Else
                    'CF
                    'piCounter += 1
                End If
            End If


            'If (!programIdentifiers.ContainsKey(piCode)) Then
            '{
            '    programIdentifiers.Add(piCode, new ProgramIdentifier(piCode));
            '}

            'if (piCounter > 20 && currentProgram != null)
            '{
            '    programIdentifiers.Clear();
            '    programIdentifiers.Add(currentProgram.PiCode, currentProgram);
            '}

            Return CurrentProgram
        End Function 'getProgramIdentifier


        Private Sub readPICode(ByVal blockData As System.Int16)
            '
            '
            ' * BIT          DESCRIPTION
            ''
            ' * 15 to 12     Country code
            ' * 11 to 8      Program type in terms of area coverage
            ' * 7 to 0       Program reference number
            '

            CurrentProgram.CountryCode = CByte((blockData And &HF000) >> 12)
            CurrentProgram.AreaCoverage = CType((blockData And &HF00) >> 8, enCoverageAreaCode)
            CurrentProgram.ReferenceNumber = CByte(blockData And &HFF)

            CurrentProgram.RBDS = ((blockData And &HFF00) >> 8) < &HB0

        End Sub 'readPICode


        'Private Sub readGroup(ByVal blockType As Integer, ByVal blockData() As Byte)
        Private Sub readGroup(ByVal blockData As System.Int16)
            '
            '
            '* BIT          SYMBOL      DESCRIPTION
            '* 15 to 12     A[3:0]      Group type code
            '* 11           B           Group version. 0 = version A. 1 = version B.
            '* 10                       Traffic Program (TP) Identification code. 0 = TP supported.
            '*                           1 = TP not supported
            '* 9 to 5                   Program Type (PTY) code
            '

            ' Type and version is stored together
            mCurrentProgram.GroupType = CType((blockData And &HF800) >> (3 + 8), engroupTypeMap)
            mCurrentProgram.GroupVersion = CType((blockData And &H800) >> (3 + 8), enGroupVersionMap)
            mCurrentProgram.TrafficProgram = CBool((blockData And &H400) >> (8 + 2))
            mCurrentProgram.ProgramType = CType((blockData And &H3E0) >> 5, enPTY)
        End Sub 'readGroup
        Private Sub readCompleteGroup(ByRef rdsGroupData As FMRadio.FMRadioHAL.stRDSRAWMessage)
            Dim CurrentEONPI As ProgramIdentifier
            Dim Block2HI As Byte = System.Convert.ToByte((rdsGroupData.Block2 And &HFF00) >> 8)
            Dim Block2LO As Byte = System.Convert.ToByte(rdsGroupData.Block2 And &HFF)
            Select Case mCurrentProgram.GroupType
                Case engroupTypeMap.group0B
                    CurrentEONPI = mEEONList(rdsGroupData.Block3)
                    CurrentEONPI.TrafficProgram = CBool(rdsGroupData.Block1 And 16)
                    CurrentEONPI.TrafficAnnouncement = CBool(rdsGroupData.Block1 And 8)
                Case engroupTypeMap.group14A
                    If mEEONList(rdsGroupData.Block3) Is Nothing Then
                        'First Time this ON PICode was received so add it
                        mEEONList.Add(rdsGroupData.Block3, New ProgramIdentifier(rdsGroupData.Block3))
                    End If
                    CurrentEONPI = mEEONList(rdsGroupData.Block3)
                    CurrentEONPI.TrafficProgram = CBool(rdsGroupData.Block1 And 16)
                    Select Case CInt(rdsGroupData.Block1 And &HF)
                        Case 0, 1, 2, 3
                            CurrentEONPI.ProgramServiceName((rdsGroupData.Block1 And &HF) * 2) = RDSCharMap(Block2HI)
                            CurrentEONPI.ProgramServiceName(((rdsGroupData.Block1 And &HF) * 2) + 1) = RDSCharMap(Block2LO)
                        Case 4
                            AFHandling(CurrentEONPI, Block2HI, Block2LO)
                        Case 5, 6, 7, 8, 9
                            Debug.Write(CStr(rdsGroupData.Block3) + ": " + CStr(rdsGroupData.Block1 And &HF) + " ")
                            Dim f As New FMRadio.FMRadioHAL.Frequency(Block2HI)
                            Debug.Write(f.DisplayFormat + " ")
                            f.RDSFormat = Block2LO
                            Debug.WriteLine(f.DisplayFormat)
                            If CurrentEONPI.AlternativeFrequencyLists.Count = 0 Then
                                Dim lAFlist As New AlternativeFrequencyList(0, New FMRadio.FMRadioHAL.Frequency(FMRadio.FMRadioHAL.Frequency.FREQ_UNDEFINED))
                                CurrentEONPI.AlternativeFrequencyLists.Add(New FMRadio.FMRadioHAL.Frequency(FMRadio.FMRadioHAL.Frequency.FREQ_UNDEFINED), lAFlist)
                            End If
                            If Not CurrentEONPI.CurrentAlternativeFrequencyList.AlternativeFrequencies.Contains(New FMRadio.FMRadioHAL.Frequency(Block2LO)) Then
                                Dim lAF As New AlternativeFrequency(New FMRadio.FMRadioHAL.Frequency(Block2LO))
                                lAF.LinkedFrequency = New FMRadio.FMRadioHAL.Frequency(Block2HI)
                                CurrentEONPI.CurrentAlternativeFrequencyList.AlternativeFrequencies.Add(New FMRadio.FMRadioHAL.Frequency(Block2LO), lAF)
                            End If


                            If New FMRadio.FMRadioHAL.Frequency(Block2HI).Equals(Me.CurrentProgram.TuningFrequency) Then
                                'If CurrentEONPI.AlternativeFrequencyLists.Contains(New FMRadio.FMRadioHAL.Frequency(FMRadio.FMRadioHAL.Frequency.FREQ_UNDEFINED)) Then
                                'CurrentEONPI.AlternativeFrequencyLists(New FMRadio.FMRadioHAL.Frequency(FMRadio.FMRadioHAL.Frequency.FREQ_UNDEFINED))
                                'End If
                                CurrentEONPI.TuningFrequency = Nothing
                                CurrentEONPI.TuningFrequency = New FMRadio.FMRadioHAL.Frequency(Block2LO)
                                If CurrentEONPI.TuningFrequency Is Nothing Then

                                Else
                                    'Dim lAF As New AlternativeFrequency(New FMRadio.FMRadioHAL.Frequency(Block2LO))
                                    'If CurrentEONPI.CurrentAlternativeFrequencyList Is Nothing Then
                                    '    Dim lAFList As New AlternativeFrequencyList(1, New FMRadio.FMRadioHAL.Frequency(Block2HI))
                                    '    lAFList.AlternativeFrequencies.Add(New FMRadio.FMRadioHAL.Frequency(Block2LO), lAF)
                                    'Else
                                    '    CurrentEONPI.CurrentAlternativeFrequencyList.AlternativeFrequencies.Add(New FMRadio.FMRadioHAL.Frequency(Block2LO), lAF)
                                    'End If
                                End If
                            End If

                        Case 13
                            CurrentEONPI.ProgramType = CType((Block2HI And &HF8) >> 3, enPTY)
                            'CurrentEONPI.TrafficAnnouncement = CBool(Block2HI And 1)
                    End Select
                Case engroupTypeMap.group3A
                    If rdsGroupData.Block3 = -12986 Then
                        CurrentProgram.TMCGroup = CType(rdsGroupData.Block1 And &H1F, engroupTypeMap)

                    Else
                        Debug.WriteLine("3A without CD46 !:" + CStr(rdsGroupData.Block3))
                    End If

            End Select
            If CurrentProgram.TMCGroup <> engroupTypeMap.NotSet Then
                If mCurrentProgram.GroupType = CurrentProgram.TMCGroup Then
                    Select Case CType((rdsGroupData.Block1 And &H18) >> 3, TMC.enGrouptype)
                        Case TMC.enGrouptype.TMC_SINGLE

                    End Select

                End If
            End If
        End Sub

        Private Sub readGroupData(ByVal blockType As enBlockType, ByVal Block As System.Int16)
            Dim BlockHI As Byte = System.Convert.ToByte((Block And &HFF00) >> 8)
            Dim BlockLO As Byte = System.Convert.ToByte(Block And &HFF)

            Dim groupType As engroupTypeMap = mCurrentProgram.GroupType
            Static lHour As Integer
            Static lMinute As Integer
            Static lOffset As Integer


            'Debug.WriteLine(Constants.BlockTypeDictionary[blockType] + " " + Convert.ToString(blockData[0], 16) + Convert.ToString(blockData[1], 16) + " " + groupType);
            Select Case groupType

                Case engroupTypeMap.group0A, engroupTypeMap.group0B
                    'End Select
                    'If groupType.Equals(groupType.group0A) Or groupType.Equals(groupType.group0B) Then
                    Select Case blockType
                        Case enBlockType.B
                            mCurrentProgram.TrafficAnnouncement = CBool((BlockLO And &H10) >> 4)
                            mCurrentProgram.MusicSpeechSwitch = CType((BlockLO And &H8) >> 3, enMS)
                            mCurrentProgram.ProgramServiceSegmentAddress = BlockLO And CByte(&H3)
                            Select Case mCurrentProgram.ProgramServiceSegmentAddress

                            End Select
                            Select Case CurrentProgram.ProgramServiceSegmentAddress
                                Case &H0
                                    CurrentProgram.DecoderIdentification = CType(CurrentProgram.DecoderIdentification Or ((BlockLO And &H4) << 1), enDICode)
                                Case &H1
                                    CurrentProgram.DecoderIdentification = CType(CurrentProgram.DecoderIdentification Or (BlockLO And &H4), enDICode)
                                Case &H2
                                    CurrentProgram.DecoderIdentification = CType(CurrentProgram.DecoderIdentification Or ((BlockLO And &H4) >> 1), enDICode)
                                Case &H3
                                    CurrentProgram.DecoderIdentification = CType(CurrentProgram.DecoderIdentification Or ((BlockLO And &H4) >> 2), enDICode)
                            End Select
                            'Debug.Write(currentProgram.ProgramServiceSegmentAddress + " ");
                            'charCount++;
                            'if (charCount > 40)
                            '{
                            '    Debug.WriteLine();
                            '    charCount = 0;
                            '}
                        Case enBlockType.C
                            If groupType = engroupTypeMap.group0B Then
                                If CurrentProgram.PiCode <> Block Then
                                    Debug.WriteLine("Warning: 0B Group different PICode in Block A and C: " + CStr(CurrentProgram.PiCode) + ":" + CStr(Block))
                                    Exit Select
                                End If

                            End If

                            'CF
                            'AFHandling(CurrentProgram, BlockHI, BlockLO)


                        Case enBlockType.D
                            If (RDSCharMap(BlockHI) = ControlChars.NullChar) Or (RDSCharMap(BlockLO) = ControlChars.NullChar) Then
                                Debug.WriteLine("Null Char in 0A/0B")
                            End If
                            CurrentProgram.ProgramServiceName((CurrentProgram.ProgramServiceSegmentAddress * 2)) = RDSCharMap(BlockHI)
                            CurrentProgram.ProgramServiceName((CurrentProgram.ProgramServiceSegmentAddress * 2) + 1) = RDSCharMap(BlockLO)
                            'Select Case CurrentProgram.ProgramServiceSegmentAddress
                            '    Case &H0
                            '        CurrentProgram.ProgramServiceName(0) = RDSCharMap(BlockHI)
                            '        CurrentProgram.ProgramServiceName(1) = RDSCharMap(BlockLO)
                            '    Case &H1
                            '        CurrentProgram.ProgramServiceName(2) = RDSCharMap(BlockHI)
                            '        CurrentProgram.ProgramServiceName(3) = RDSCharMap(BlockLO)
                            '    Case &H2
                            '        CurrentProgram.ProgramServiceName(4) = RDSCharMap(BlockHI)
                            '        CurrentProgram.ProgramServiceName(5) = RDSCharMap(BlockLO)
                            '    Case &H3
                            '        CurrentProgram.ProgramServiceName(6) = RDSCharMap(BlockHI)
                            '        CurrentProgram.ProgramServiceName(7) = RDSCharMap(BlockLO)
                            'End Select
                    End Select
                Case engroupTypeMap.group1A ', engroupTypeMap.group1B
                    'CF
                    Exit Select

                    'If (groupType = engroupTypeMap.group1A) Or (groupType = engroupTypeMap.group1B) Then
                    Select Case blockType
                        Case enBlockType.B
                            If groupType = engroupTypeMap.group1B Then
                                Exit Select
                            End If
                            ' bits 4 to 0 (5 bits) contain radio paging codes - Not implemented
                        Case enBlockType.C
                            If groupType = engroupTypeMap.group1B Then
                                Exit Select
                            End If
                            ' LA - Linkage Actuator
                            CurrentProgram.LinkageActuator = CBool((BlockHI And &H80))

                            ' Variant
                            Select Case BlockHI And &H70
                                Case &H0
                                    ' bits 3 to 0 (4 bits) of block 1 contain radio paging codes - Not implemented
                                    ' (blockData[0] & 0x0F)
                                    CurrentProgram.ExtendedCounTryCode = CType((CInt(BlockLO) << 4) Or (CurrentProgram.CountryCode), enCountryCodeMapExt)
                                Case &H1
                                    ' todo !
                                    Dim tmcIdentification As Integer = ((BlockHI And &HF) << 8) Or BlockLO
                                Case &H2
                                    ' Paging Indetification - Not implemented
                                Case &H3
                                    CurrentProgram.LanguageCode = CType(Block And &HFFF, enLanguage)
                                Case &H7
                                    ' Identification of Emergency Warning Systems (EWS) channel
                                    Dim ewsChannel As Integer = ((BlockHI And &HF) << 8) Or BlockLO
                            End Select
                        Case enBlockType.D
                            Debug.WriteLine("huh")

                    End Select
                    ' The Program Item Number is the scheduled broadcast start time and day of month as
                    ''
                    '     * published by the broadcaster. The day of month is transmitted as a five-bit binary
                    '     * number in the range 1-31. Hours are transmitted as a five-bit binary number in the
                    '     * range 0-23. The spare codes are not used. Minutes are transmitted as a six-bit binary
                    '    * number in the range 0-59. The spare codes are not used.
                    ''


                Case engroupTypeMap.group2A, engroupTypeMap.group2B
                    'If groupType = engroupTypeMap.group2A Or groupType = engroupTypeMap.group2B Then
                    Select Case blockType
                        Case enBlockType.B
                            Dim oldTextABFlag As System.Boolean = CurrentProgram.RadioTextABFlag
                            CurrentProgram.RadioTextABFlag = CBool((BlockLO And &H10) >> 4)
                            If oldTextABFlag <> CurrentProgram.RadioTextABFlag Then
                                RaiseEvent RadioTextChange(CurrentProgram.RadioText)
                                Dim i As Integer
                                For i = 0 To CurrentProgram.RadioText.Length - 1
                                    CurrentProgram.RadioText(i) = " "c
                                Next i
                            End If
                            CurrentProgram.TextSegmentAddressCode = BlockLO And CByte(&HF)

                            'Debug.WriteLine(currentProgram.TextSegmentAddressCode);
                        Case enBlockType.C
                            If groupType = engroupTypeMap.group2B Then
                                Exit Select
                            End If
                            If (BlockHI = &HF) And (BlockLO = &HF) Then
                                Debug.WriteLine("Code-table E.1")
                            Else
                                If (BlockHI = &HE) And (BlockLO = &HE) Then
                                    Debug.WriteLine("Code-table E.2")
                                Else
                                    If (BlockHI = &H1B) And (BlockLO = &H6E) Then
                                        Debug.WriteLine("Code-table E.3")
                                    Else
                                        If BlockHI <> &HD And (RDSCharMap(BlockHI) <> "") Then
                                            CurrentProgram.RadioText((CurrentProgram.TextSegmentAddressCode * 4)) = RDSCharMap(BlockHI)
                                            If (BlockLO <> &HD) And (RDSCharMap(BlockLO) <> "") Then
                                                CurrentProgram.RadioText((CurrentProgram.TextSegmentAddressCode * 4 + 1)) = RDSCharMap(BlockLO)
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Case enBlockType.D
                            If BlockHI = &HF And BlockLO = &HF Then
                                Debug.WriteLine("Code-table E.1")
                            Else
                                If BlockHI = &HE And BlockLO = &HE Then
                                    Debug.WriteLine("Code-table E.2")
                                Else
                                    If BlockHI = &H1B And BlockLO = &H6E Then
                                        Debug.WriteLine("Code-table E.3")
                                    Else
                                        If groupType = engroupTypeMap.group2A Then
                                            If BlockHI <> &HD And (RDSCharMap(BlockHI) <> "") Then
                                                CurrentProgram.RadioText((CurrentProgram.TextSegmentAddressCode * 4 + 2)) = RDSCharMap(BlockHI)
                                                If BlockLO <> &HD And (RDSCharMap(BlockLO) <> "") Then
                                                    CurrentProgram.RadioText((CurrentProgram.TextSegmentAddressCode * 4 + 3)) = RDSCharMap(BlockLO)
                                                End If
                                            End If
                                        Else
                                            If BlockHI <> &HD And (RDSCharMap(BlockHI) <> "") Then
                                                CurrentProgram.RadioText((CurrentProgram.TextSegmentAddressCode * 2)) = RDSCharMap(BlockHI)
                                                If BlockLO <> &HD And (RDSCharMap(BlockLO) <> "") Then
                                                    CurrentProgram.RadioText((CurrentProgram.TextSegmentAddressCode * 2 + 1)) = RDSCharMap(BlockLO)
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                    End Select
                Case engroupTypeMap.group4A
                    'CF
                    Exit Select
                    'If groupType = engroupTypeMap.group4A Then
                    Select Case blockType
                        Case enBlockType.B
                            CurrentProgram.ModifiedJulianDayCode = (CInt(BlockLO And &H3) << 15)
                        Case enBlockType.C
                            CurrentProgram.ModifiedJulianDayCode = CurrentProgram.ModifiedJulianDayCode Or (CInt(BlockHI) << 7)
                            CurrentProgram.ModifiedJulianDayCode = CurrentProgram.ModifiedJulianDayCode Or (CInt(BlockLO And &HFE) >> 1)

                            '
                            '
                            '* a) To find Y, M, D from MJD
                            '* U.S. RBDS Standard - April 1998
                            '* Page 104
                            '* Y' = int [ (MJD - 15 078,2) / 365,25 ]
                            '* M' = int { [ MJD - 14 956,1 - int (Y' × 365,25) ] / 30,6001 }
                            '* D = MJD - 14 956 - int ( Y' × 365,25 ) - int ( M' × 30,6001 )
                            '* If M' = 14 or M' = 15, then K = 1; else K = 0
                            '* Y = Y' + K
                            '* M = M' - 1 - K × 12
                            '* b) To find MJD from Y, M, D
                            '* If M = 1 or M = 2, then L = 1; else L = 0
                            '* MJD = 14 956 + D + int [ (Y - L) × 365,25] + int [ (M + 1 + L × 12) × 30,6001 ]
                            '* c) To find WD from MJD
                            '* WD = [ (MJD + 2) mod 7 ] + 1
                            '* d) To find MJD from WY, WN, WD
                            '* MJD = 15 012 + WD + 7 × { WN + int [ (WY × 1 461 / 28) + 0,41] }
                            '* e) To find WY, WN from MJD
                            '* W = int [ (MJD / 7) - 2 144,64 ]
                            '* WY = int [ (W × 28 / 1 461) - 0,0079]
                            '* WN = W - int [ (WY × 1 461 / 28) + 0,41]
                            '

                            Dim yearPrim As Integer = CInt(Fix((CDbl(CurrentProgram.ModifiedJulianDayCode) - 15078.2) / 365.25))
                            Dim monthPrim As Integer = CInt(Fix((CurrentProgram.ModifiedJulianDayCode - 14956.1 - CInt(yearPrim * 365.25)) / 30.6001))
                            Dim day As Integer = CurrentProgram.ModifiedJulianDayCode - 14956 - CInt(Fix(yearPrim * 365.25)) - CInt(Fix(monthPrim * 30.6001))
                            Dim k As Integer = 0
                            If monthPrim = 14 Or monthPrim = 15 Then
                                k = 1
                            End If
                            Dim year As Integer = 1900 + yearPrim + k
                            Dim month As Integer = monthPrim - 1 - k * 12
                            CurrentProgram.RDSDateTime = Nothing
                            CurrentProgram.RDSDateTime = New DateTime(year, month, day, 0, 0, 0)
                            lHour = (BlockLO And 1) << 4
                        Case enBlockType.D
                            lHour = lHour Or ((BlockHI And &HF0) >> 4)
                            lMinute = ((BlockHI And &HF) << 2)
                            lMinute = lMinute Or ((BlockLO And &HC0) >> 6)
                            lOffset = BlockLO And &H1F
                            If (BlockLO And &H20) = &H20 Then
                                lOffset = -lOffset
                            End If

                            CurrentProgram.RDSDateTime = CurrentProgram.RDSDateTime.AddHours(CDbl(lHour) + (lOffset / 2))
                            CurrentProgram.RDSDateTime = CurrentProgram.RDSDateTime.AddMinutes(lMinute)

                    End Select
                Case engroupTypeMap.group8A
                    'If groupType = engroupTypeMap.group8A Then
                    'Debug.WriteLine("TMC Data");
                    Select Case blockType
                        Case enBlockType.B
                        Case enBlockType.C
                        Case enBlockType.D
                    End Select
                Case engroupTypeMap.group9A
                    'If groupType = engroupTypeMap.group9A Then
                    Debug.WriteLine("EWS Message")
                    Select Case blockType
                        Case enBlockType.B
                        Case enBlockType.C
                        Case enBlockType.D
                    End Select
                Case engroupTypeMap.group10A
                    'If groupType.Equals("10A") Then
                    Select Case blockType
                        Case enBlockType.B
                            If CurrentProgram.ProgramTypeNameABFlag <> CBool(BlockLO And &H10) Then
                                RaiseEvent ProgramTypeNameChange(Nothing)
                            End If
                            CurrentProgram.ProgramTypeNameABFlag = CBool(BlockLO And &H10)
                            CurrentProgram.ProgramTypeNameSegmentAddress = BlockLO And CByte(&H1)
                        Case enBlockType.C
                            If BlockHI <> &HD And RDSCharMap(BlockHI) <> "" Then
                                CurrentProgram.ProgramTypeName((CurrentProgram.ProgramTypeNameSegmentAddress * 4)) = RDSCharMap(BlockHI)
                            End If
                            If BlockLO <> &HD And RDSCharMap(BlockLO) <> "" Then
                                CurrentProgram.ProgramTypeName((CurrentProgram.ProgramTypeNameSegmentAddress * 4 + 1)) = RDSCharMap(BlockLO)
                            End If
                        Case enBlockType.D
                            If BlockHI <> &HD And RDSCharMap(BlockHI) <> "" Then
                                CurrentProgram.ProgramTypeName((CurrentProgram.ProgramTypeNameSegmentAddress * 4 + 2)) = RDSCharMap(BlockHI)
                            End If
                            If BlockLO <> &HD And RDSCharMap(BlockLO) <> "" Then
                                CurrentProgram.ProgramTypeName((CurrentProgram.ProgramTypeNameSegmentAddress * 4 + 3)) = RDSCharMap(BlockLO)
                            End If
                    End Select
                Case engroupTypeMap.group14A, engroupTypeMap.group14B
                    'If groupType = engroupTypeMap.group14A Or groupType = engroupTypeMap.group14B Then
                    'Select Case blockType
                    '    Case enBlockType.B
                    '    Case enBlockType.C
                    '    Case enBlockType.D
                    'End Select
                Case engroupTypeMap.group15A
                    'RBDS only
                    Select Case blockType
                        Case enBlockType.B
                            If CurrentProgram.ProgramTypeNameABFlag <> CBool(BlockLO And &H10) Then
                                RaiseEvent ProgramTypeNameChange(CurrentProgram.RadioText)
                            End If
                            CurrentProgram.ProgramTypeNameABFlag = CBool(BlockLO And &H10)
                            CurrentProgram.ProgramTypeNameSegmentAddress = BlockLO And CByte(&H1)
                        Case enBlockType.C
                            If BlockHI <> &HD And RDSCharMap(BlockHI) <> "" Then
                                CurrentProgram.ProgramServiceName((CurrentProgram.ProgramTypeNameSegmentAddress * 4)) = RDSCharMap(BlockHI)
                            End If
                            If BlockLO <> &HD And RDSCharMap(BlockLO) <> "" Then
                                CurrentProgram.ProgramServiceName((CurrentProgram.ProgramTypeNameSegmentAddress * 4 + 1)) = RDSCharMap(BlockLO)
                            End If
                        Case enBlockType.D
                            If BlockHI <> &HD And RDSCharMap(BlockHI) <> "" Then
                                CurrentProgram.ProgramServiceName((CurrentProgram.ProgramTypeNameSegmentAddress * 4 + 2)) = RDSCharMap(BlockHI)
                            End If
                            If BlockLO <> &HD And RDSCharMap(BlockLO) <> "" Then
                                CurrentProgram.ProgramServiceName((CurrentProgram.ProgramTypeNameSegmentAddress * 4 + 3)) = RDSCharMap(BlockLO)
                            End If
                    End Select
                Case engroupTypeMap.group15B
                    'If groupType = engroupTypeMap.group15B Then
                    Select Case blockType
                        Case enBlockType.B
                            CurrentProgram.TrafficAnnouncement = CBool((BlockLO And &H10) >> 4)
                            CurrentProgram.MusicSpeechSwitch = CType(((BlockLO And &H8) >> 3), enMS)
                            CurrentProgram.ProgramServiceSegmentAddress = BlockLO And CByte(&H3)
                            Select Case CurrentProgram.ProgramServiceSegmentAddress
                                Case &H0
                                    CurrentProgram.DecoderIdentification = CType(CurrentProgram.DecoderIdentification Or ((BlockLO And &H4) << 1), enDICode)
                                Case &H1
                                    CurrentProgram.DecoderIdentification = CType(CurrentProgram.DecoderIdentification Or (BlockLO And &H4), enDICode)
                                Case &H2
                                    CurrentProgram.DecoderIdentification = CType(CurrentProgram.DecoderIdentification Or ((BlockLO And &H4) >> 1), enDICode)
                                Case &H3
                                    CurrentProgram.DecoderIdentification = CType(CurrentProgram.DecoderIdentification Or ((BlockLO And &H4) >> 2), enDICode)
                            End Select

                        Case enBlockType.D
                            CurrentProgram.TrafficAnnouncement = CBool((BlockLO And &H10) >> 4)
                            CurrentProgram.MusicSpeechSwitch = CType(((BlockLO And &H8) >> 3), enMS)
                            CurrentProgram.ProgramServiceSegmentAddress = BlockLO And CByte(&H3)
                            Select Case CurrentProgram.ProgramServiceSegmentAddress
                                Case &H0
                                    CurrentProgram.DecoderIdentification = CType(CurrentProgram.DecoderIdentification Or ((BlockLO And &H4) << 1), enDICode)
                                Case &H1
                                    CurrentProgram.DecoderIdentification = CType(CurrentProgram.DecoderIdentification Or (BlockLO And &H4), enDICode)
                                Case &H2
                                    CurrentProgram.DecoderIdentification = CType(CurrentProgram.DecoderIdentification Or ((BlockLO And &H4) >> 1), enDICode)
                                Case &H3
                                    CurrentProgram.DecoderIdentification = CType(CurrentProgram.DecoderIdentification Or ((BlockLO And &H4) >> 2), enDICode)
                            End Select
                    End Select
                Case Else
                    'Debug.Write("Unimplemented Group " + groupType.GetName(groupType.GetType, groupType))
            End Select
        End Sub 'readGroupData


        Private Sub AFHandling(ByRef PI As ProgramIdentifier, ByVal BlockHI As Byte, ByVal BlockLO As Byte)


            Static alternativeFrequency As AlternativeFrequency
            Static alternativeFrequencyHeadDetected As Boolean
            Static afListLength As Integer
            Select Case BlockHI
                Case &HCD
                    ' Filler
                    'Debug.WriteLine("Filler in block 0");
                Case &HFA
                    ' An LF/MF frequency follows
                    Debug.WriteLine("An LF/MF frequency follows in block 0")
                Case &HE0 To &HF9
                    alternativeFrequencyHeadDetected = True
                    afListLength = BlockHI - &HE0
                    'Debug.Write(String.Format("{0:##}AF  ", afListLength));

                Case 1 To &HCC
                    alternativeFrequency = New AlternativeFrequency(New FMRadio.FMRadioHAL.Frequency(BlockHI))
                Case Else

            End Select
            Select Case BlockLO
                Case &HCD
                    ' Filler
                    'Debug.WriteLine("Filler in block 1");
                Case &HFA
                    ' An LF/MF frequency follows
                    ' Is this possible?
                    Debug.WriteLine("An 'LF/MF frequency follows' in block 1!!!")
                Case &HE0 To &HF9
                    Debug.WriteLine("AFHead in Block 1!!!")
                Case 1 To &HCC
                    'Debug.WriteLine(String.Format("{0:000.0}", new AlternativeFrequency(blockData[1]).DisplayableFrequency));
                    If alternativeFrequencyHeadDetected Then
                        Debug.WriteLine(CStr(afListLength) + "AF " + New FMRadio.FMRadioHAL.Frequency(BlockLO).DisplayFormat)
                        PI.AddAlternativeFrequencyList(afListLength, New FMRadio.FMRadioHAL.Frequency(BlockLO))
                        'CurrentProgram.CurrentAlternativeFrequencyList = New AlternativeFrequencyList(afListLength, BlockLO)
                        alternativeFrequencyHeadDetected = False
                        'CurrentProgram.TuningFrequency = BlockLO
                    Else
                        'If Not (alternativeFrequency Is Nothing) Then
                        '    PI.AddAlternativeFrequency(alternativeFrequency)
                        'End If
                        Debug.WriteLine(New FMRadio.FMRadioHAL.Frequency(BlockHI).DisplayFormat + " -AF- " + New FMRadio.FMRadioHAL.Frequency(BlockLO).DisplayFormat)
                        PI.AddAlternativeFrequencies(BlockHI, BlockLO)
                    End If
            End Select


            'If BlockHI = &HCD Then
            '    ' Filler
            '    'Debug.WriteLine("Filler in block 0");
            'Else
            '    If BlockHI = &HFA Then
            '        ' An LF/MF frequency follows
            '        Debug.WriteLine("An LF/MF frequency follows in block 0")
            '    Else
            '        If (BlockHI >= &HE0) And (BlockHI < &HFA) Then
            '            PI.alternativeFrequencyHeadDetected = True
            '            PI.afListLength = BlockHI - &HE0
            '            'Debug.Write(String.Format("{0:##}AF  ", afListLength));
            '        Else
            '            If (BlockHI > &H0) And (BlockHI < &HCD) Then
            '                alternativeFrequency = New AlternativeFrequency(New FMRadio.FMRadioHAL.Frequency(BlockHI))
            '            End If 'Debug.Write(String.Format("{0:#00.0} ", alternativeFrequency.DisplayableFrequency));
            '        End If
            '    End If
            'End If
            'If BlockLO = &HCD Then
            '    ' Filler
            '    'Debug.WriteLine("Filler in block 1");
            'Else
            '    If BlockLO = &HFA Then
            '        ' An LF/MF frequency follows
            '        ' Is this possible?
            '        Debug.WriteLine("An LF/MF frequency follows in block 1")
            '    Else
            '        If (BlockLO > &H0) And (BlockLO < &HCD) Then
            '            'Debug.WriteLine(String.Format("{0:000.0}", new AlternativeFrequency(blockData[1]).DisplayableFrequency));
            '            If PI.alternativeFrequencyHeadDetected Then
            '                Debug.WriteLine(CStr(PI.afListLength) + "AF " + New FMRadio.FMRadioHAL.Frequency(BlockLO).DisplayFormat)
            '                CurrentProgram.AddAlternativeFrequencyList(PI.afListLength, New FMRadio.FMRadioHAL.Frequency(BlockLO))
            '                'CurrentProgram.CurrentAlternativeFrequencyList = New AlternativeFrequencyList(afListLength, BlockLO)
            '                PI.alternativeFrequencyHeadDetected = False
            '                'CurrentProgram.TuningFrequency = BlockLO
            '            Else
            '                If Not (alternativeFrequency Is Nothing) Then
            '                    CurrentProgram.AddAlternativeFrequency(alternativeFrequency)
            '                End If
            '                CurrentProgram.AddAlternativeFrequency(New AlternativeFrequency(New FMRadio.FMRadioHAL.Frequency(BlockLO)))
            '            End If
            '        End If
            '    End If
            'End If
        End Sub


        'Public Property BlocksCounter1() As Long() Implements I_Interpreter.BlocksCounter
        '    Get

        '    End Get
        '    Set(ByVal Value() As Long)

        '    End Set
        'End Property

        'Public Property CurrentProgram1() As ProgramIdentifier Implements I_Interpreter.CurrentProgram
        '    Get

        '    End Get
        '    Set(ByVal Value As ProgramIdentifier)

        '    End Set
        'End Property

        'Public Sub decode1(ByVal rdsGroupData() As Byte, ByVal bytes As Integer) Implements I_Interpreter.decode

        'End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class 'Interpreter

    Public Class TMCmessage

    End Class

End Namespace 'RDSInterpreter