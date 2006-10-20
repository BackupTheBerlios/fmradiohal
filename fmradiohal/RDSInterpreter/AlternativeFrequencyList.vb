Option Strict On

Imports System
Imports System.Collections
Imports System.Text


Namespace RDSInterpreter

    Public Class AFDictionary
        Inherits DictionaryBase

        Default Public Property Item(ByVal key As FMRadio.FMRadioHAL.Frequency) As [AlternativeFrequency]
            Get
                Return CType(Dictionary(key), [AlternativeFrequency])
            End Get
            Set(ByVal Value As [AlternativeFrequency])
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

        Public Sub Add(ByVal key As FMRadio.FMRadioHAL.Frequency, ByVal value As [AlternativeFrequency])
            If dictionary.Contains(key) Then
                REM Problem !

            Else
                Dictionary.Add(key, value)
            End If
        End Sub 'Add

        Public Function Contains(ByVal key As FMRadio.FMRadioHAL.Frequency) As Boolean
            'Dim iD As DictionaryEntry
            'Contains = False
            'For Each iD In Dictionary
            '    If key.Value = CType(iD.Key, FMRadio.FMRadioHAL.Frequency).Value Then
            '        Return True
            '    End If
            'Next iD
            Return Dictionary.Contains(key)
        End Function 'Contains

        Public Sub Remove(ByVal key As FMRadio.FMRadioHAL.Frequency)
            Dictionary.Remove(key)
        End Sub 'Remove

        'Protected Overrides Sub OnInsert(ByVal key As [Object], ByVal value As [Object])
        '    If Not key.GetType() Is Type.GetType("System.String") Then
        '        Throw New ArgumentException("key must be of type String.", "key")
        '    Else
        '        Dim strKey As [String] = CType(key, [String])
        '        If strKey.Length > 5 Then
        '            Throw New ArgumentException("key must be no more than 5 characters in length.", "key")
        '        End If
        '    End If
        '    If Not value.GetType() Is Type.GetType("System.String") Then
        '        Throw New ArgumentException("value must be of type String.", "value")
        '    Else
        '        Dim strValue As [String] = CType(value, [String])
        '        If strValue.Length > 5 Then
        '            Throw New ArgumentException("value must be no more than 5 characters in length.", "value")
        '        End If
        '    End If
        'End Sub 'OnInsert

        'Protected Overrides Sub OnRemove(ByVal key As [Object], ByVal value As [Object])
        '    If Not key.GetType() Is Type.GetType("System.String") Then
        '        Throw New ArgumentException("key must be of type String.", "key")
        '    Else
        '        Dim strKey As [String] = CType(key, [String])
        '        If strKey.Length > 5 Then
        '            Throw New ArgumentException("key must be no more than 5 characters in length.", "key")
        '        End If
        '    End If
        'End Sub 'OnRemove

        'Protected Overrides Sub OnSet(ByVal key As [Object], ByVal oldValue As [Object], ByVal newValue As [Object])
        '    If Not key.GetType() Is Type.GetType("System.String") Then
        '        Throw New ArgumentException("key must be of type String.", "key")
        '    Else
        '        Dim strKey As [String] = CType(key, [String])
        '        If strKey.Length > 5 Then
        '            Throw New ArgumentException("key must be no more than 5 characters in length.", "key")
        '        End If
        '    End If
        '    If Not newValue.GetType() Is Type.GetType("System.String") Then
        '        Throw New ArgumentException("newValue must be of type String.", "newValue")
        '    Else
        '        Dim strValue As [String] = CType(newValue, [String])
        '        If strValue.Length > 5 Then
        '            Throw New ArgumentException("newValue must be no more than 5 characters in length.", "newValue")
        '        End If
        '    End If
        'End Sub 'OnSet

        'Protected Overrides Sub OnValidate(ByVal key As [Object], ByVal value As [Object])
        '    If Not key.GetType() Is Type.GetType("System.String") Then
        '        Throw New ArgumentException("key must be of type String.", "key")
        '    Else
        '        Dim strKey As [String] = CType(key, [String])
        '        If strKey.Length > 5 Then
        '            Throw New ArgumentException("key must be no more than 5 characters in length.", "key")
        '        End If
        '    End If
        '    If Not value.GetType() Is Type.GetType("System.String") Then
        '        Throw New ArgumentException("value must be of type String.", "value")
        '    Else
        '        Dim strValue As [String] = CType(value, [String])
        '        If strValue.Length > 5 Then
        '            Throw New ArgumentException("value must be no more than 5 characters in length.", "value")
        '        End If
        '    End If
        'End Sub 'OnValidate 

    End Class



    Public Class AlternativeFrequencyList


        Private mAFMethod As enAFMethod = enAFMethod.UNKNOWN

        Private mtuningFrequency As FMRadio.FMRadioHAL.Frequency
        Private afListLength As Integer = 0

        Private mAlternativeFrequencies As New AFDictionary

        'Private Duplicates As Integer = 0

        Public ReceiveCounter As System.Int32 = 0




        Public ReadOnly Property AFMethod() As enAFMethod
            Get
                AFMethod = mAFMethod
            End Get
        End Property

        ReadOnly Property AlternativeFrequencies() As AFDictionary
            Get
                'Dim iAFD As DictionaryEntry
                'Dim iAFD2 As New AFDictionary
                'Dim iAF As AlternativeFrequency
                'For Each iAFD In mAlternativeFrequencies
                '    iAF = CType(iAFD.Value, AlternativeFrequency)
                '    If mtuningFrequency.Value <> iAF.Frequency.Value Then
                '        iAFD2.Add(iAF.Frequency, iAF)
                '    End If
                'Next iAFD
                'Return iAFD2
                Return mAlternativeFrequencies
            End Get
        End Property



        Public Sub New(ByVal afListLength As Integer, ByVal tuningFrequency As FMRadio.FMRadioHAL.Frequency)
            Me.afListLength = afListLength
            Me.TuningFrequency = tuningFrequency
        End Sub 'New


        Public Function getHead() As String
            Return [String].Format("{0:##}AF {1:#00.0}", afListLength, TuningFrequency.DoubleValue)
        End Function 'getHead


        Friend Sub AddAlternativeFrequencies(ByVal af1byte As Byte, ByVal af2byte As Byte)
            Static previousAlternativeFrequency As AlternativeFrequency = Nothing
            Dim AF1 As FMRadio.FMRadioHAL.Frequency
            Dim AF2 As FMRadio.FMRadioHAL.Frequency
            Dim Freq As FMRadio.FMRadioHAL.Frequency
            Dim AF As AlternativeFrequency

            If (af1byte > 0) And (&HCD > af1byte) Then
                AF1 = New FMRadio.FMRadioHAL.Frequency(af1byte)
            End If
            If (af2byte > 0) And (&HCD > af2byte) Then
                If af1byte = &HFA Then
                    AF2 = New FMRadio.FMRadioHAL.Frequency(af2byte, False)
                Else
                    AF2 = New FMRadio.FMRadioHAL.Frequency(af2byte, True)
                End If
            End If
            'Debug.WriteLine("add:" +  af.Frequency.DisplayFormat + " to " + mtuningFrequency.DisplayFormat)

            If mAFMethod = enAFMethod.UNKNOWN Then
                ' Add frequency and try to find out which method is used
                If AF2.Equals(TuningFrequency) Or AF1.Equals(TuningFrequency) Then
                    mAFMethod = enAFMethod.B
                Else
                    mAFMethod = enAFMethod.A
                End If
            End If
            If mAFMethod = enAFMethod.A Then
                If Not (AF1 Is Nothing) Then
                    mAlternativeFrequencies.Add(AF1, New AlternativeFrequency(AF1))
                End If
                If Not (AF2 Is Nothing) Then
                    mAlternativeFrequencies.Add(AF2, New AlternativeFrequency(AF2))
                End If
            End If
            If mAFMethod = enAFMethod.B Then
                If (TuningFrequency.Equals(AF1)) Or (TuningFrequency.Equals(AF2)) Then
                    If Not (TuningFrequency.Equals(AF1)) Then
                        Freq = AF1
                        AF = New AlternativeFrequency(AF1)
                    End If
                    If Not (TuningFrequency.Equals(AF2)) Then
                        Freq = AF2
                        AF = New AlternativeFrequency(AF2)
                    End If
                    If AF1.Value < AF2.Value Then
                        AF.RegionalVariant = False
                    Else
                        AF.RegionalVariant = True
                    End If
                    If mAlternativeFrequencies.Contains(Freq) Then
                        REM Not possible !

                    Else
                        mAlternativeFrequencies.Add(Freq, AF)
                    End If
                Else
                    REM Not possible !
                    'Debug.Assert(False)
                End If
            Else
                REM Not possible !
                'Debug.Assert(False)
            End If

            'End If
            'If AF.Frequency.Equals(mtuningFrequency) Then
            '    If mAlternativeFrequencies.Count > 0 Then
            '        'Block0 AF, Block1 Tuningfreq ! 
            '        mAFMethod = enAFMethod.B
            '        mAlternativeFrequencies.Remove(previousAlternativeFrequency.Frequency)
            '        Me.AddAlternativeFrequency(AF)
            '        Exit Select
            '    End If
            '    mAFMethod = enAFMethod.B
            'Else
            '    If mAlternativeFrequencies.Count >= 2 Then
            '        mAFMethod = enAFMethod.A
            '    End If
            'End If

            'If Not mAlternativeFrequencies.Contains(AF.Frequency) Then
            '    mAlternativeFrequencies.Add(AF.Frequency, AF)
            'Else
            '    ' rem double Freq !
            'End If
            'previousAlternativeFrequency = AF
            ''afMethod = checkAFMethod()
            'Case enAFMethod.A
            '' All frequencies are added (except duplicates)
            'If Not mAlternativeFrequencies.Contains(AF.Frequency) Then
            '    mAlternativeFrequencies.Add(AF.Frequency, AF)
            'Else
            '    REM AFMethod B or Error ?
            '    Debug.Assert(False)
            'End If
            '    Case enAFMethod.B
            '' Every other frequency is added (except duplicates)
            '' Check if regional variant or not
            'If Not (previousAlternativeFrequency Is Nothing) Then
            '    If previousAlternativeFrequency.ComparedTo(AF) < 0 Then
            '        ' Normal AF
            '        If Not previousAlternativeFrequency.Frequency.Equals(TuningFrequency) Then
            '            AF = previousAlternativeFrequency
            '        Else
            '            Debug.Assert(False)
            '        End If
            '    Else
            '        If previousAlternativeFrequency.ComparedTo(AF) > 0 Then
            '            ' Regional variant AF
            '            If Not previousAlternativeFrequency.Frequency.Equals(TuningFrequency) Then
            '                AF = previousAlternativeFrequency
            '            Else
            '                Debug.Assert(False)
            '            End If
            '            AF.RegionalVariant = True
            '        End If
            '    End If
            '    If Not mAlternativeFrequencies.Contains(AF.Frequency) Then
            '        mAlternativeFrequencies.Add(AF.Frequency, AF)
            '    Else
            '        Debug.Assert(False)
            '    End If

            '    previousAlternativeFrequency = Nothing
            'Else
            '    previousAlternativeFrequency = AF
            'End If
            'End Select

        End Sub 'AddAlternativeFrequency

        Private Function checkAFMethod() As enAFMethod
            Dim result As enAFMethod = enAFMethod.UNKNOWN
            If mAlternativeFrequencies.Count >= 2 Then
                If mAlternativeFrequencies.Contains(TuningFrequency) Then
                    result = enAFMethod.B
                    'mAlternativeFrequencies.Remove(TuningFrequency)
                Else
                    result = enAFMethod.A
                End If
            End If
            Return result
        End Function 'checkAFMethod


        'Public ReadOnly Property AfMethodText() As String
        '    Get
        '        Dim result As String = "Method: ?"
        '        Select Case afMethod
        '            Case enAFMethod.A
        '                result = "Method: A"
        '            Case enAFMethod.B
        '                result = "Method: B"
        '        End Select
        '        Return result
        '    End Get
        'End Property


        Public Property TuningFrequency() As FMRadio.FMRadioHAL.Frequency
            Get
                Return mtuningFrequency
            End Get
            Set(ByVal Value As FMRadio.FMRadioHAL.Frequency)
                mtuningFrequency = Value
            End Set
        End Property
        Public Overrides Function GetHashCode() As Integer
            GetHashCode = Me.TuningFrequency.Value
        End Function

        Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
            Dim result As Boolean = False
            Dim l As DictionaryEntry
            Dim lAF As AlternativeFrequency
            Dim lAFList As AlternativeFrequencyList
            If TypeOf obj Is AlternativeFrequencyList Then
                lAFList = CType(obj, AlternativeFrequencyList)
                result = (lAFList.AlternativeFrequencies.Count = Me.AlternativeFrequencies.Count)
                If result Then
                    For Each l In Me.AlternativeFrequencies
                        lAF = CType(l.Value, AlternativeFrequency)
                        If lAFList.AlternativeFrequencies.Contains(lAF.Frequency) Then
                        Else
                            result = False
                        End If
                    Next l
                End If
            Else

            End If

            Return result
        End Function 'Equals


        Friend Function isComplete() As Boolean
            Dim result As Boolean = False
            Dim iAFD As DictionaryEntry

            Select Case mAFMethod
                Case enAFMethod.A
                    result = (afListLength - 1 = mAlternativeFrequencies.Count)
                Case enAFMethod.B
                    result = ((afListLength - 1) / 2 = mAlternativeFrequencies.Count)
                    For Each iAFD In mAlternativeFrequencies
                        ' CType(iAFD.Value, AlternativeFrequency).
                    Next iAFD
                Case Else
            End Select
            Return result
        End Function 'isComplete
    End Class 'AlternativeFrequencyList
End Namespace 'RDSInterpreter
