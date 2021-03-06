Option Strict On

Imports System
Imports System.Text
Imports System.Diagnostics

Namespace RDSInterpreter

    Public Class AFListDictionary
        Inherits DictionaryBase

        Default Public Property Item(ByVal key As FMRadio.FMRadioHAL.Frequency) As [AlternativeFrequencyList]
            Get
                If key Is Nothing Then

                Else
                    Return CType(Dictionary(key), [AlternativeFrequencyList])
                End If
            End Get
            Set(ByVal Value As [AlternativeFrequencyList])
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

        Public Sub Add(ByVal key As FMRadio.FMRadioHAL.Frequency, ByVal value As [AlternativeFrequencyList])
            Dictionary.Add(key, value)
        End Sub 'Add

        Public Function Contains(ByVal key As FMRadio.FMRadioHAL.Frequency) As Boolean
            'Dim iD As DictionaryEntry
            'Contains = False
            'For Each iD In Dictionary
            '    If key.Value = CType(iD.Key, FMRadio.FMRadioHAL.Frequency).Value Then
            '        Return True
            '    End If
            'Next iD
            If key Is Nothing Then
                Contains = False
            Else
                Return Dictionary.Contains(key)
            End If
        End Function 'Contains

        Public Sub Remove(ByVal key As FMRadio.FMRadioHAL.Frequency)
            Dictionary.Remove(key)
        End Sub 'Remove

        Protected Overrides Function OnGet(ByVal key As Object, ByVal currentValue As Object) As Object

        End Function
    End Class

    Public Interface I_ProgramIdentifier
        Property PiCode() As System.Int16
        Property languageCode() As enLanguage
        Property CountryCode() As System.Int16
        Property AreaCoverage() As enCoverageAreaCode
        Property ReferenceNumber() As System.Int16
        Property GroupType() As engroupTypeMap
        Property GroupVersion() As enGroupVersionMap
        Property TrafficProgram() As System.Boolean
        Property ProgramType() As enPTY
        Property TrafficAnnouncement() As System.Boolean
        ReadOnly Property EnhancedOtherNetwork() As Boolean
        Property MusicSpeechSwitch() As enMS
        Property ProgramServiceSegmentAddress() As System.Int16
        Property DecoderIdentification() As enDICode
        Property TuningFrequency() As System.Int16
        ReadOnly Property AlternativeFrequencyLists() As AFListDictionary
        Property ProgramServiceName() As Char()
        Property RadioTextABFlag() As System.Boolean
        Property TextSegmentAddressCode() As System.Int16

        Property RadioText() As Char()



        Property ModifiedJulianDayCode() As System.Int16
        Property RDSDateTime() As DateTime
        Property ExtendedCounTryCode() As enCountryCodeMapExt
        Property LinkageActuator() As System.Boolean
        Property ProgramTypeNameABFlag() As System.Boolean
        Property ProgramTypeNameSegmentAddress() As System.Int16
        Property ProgramTypeName() As Char()
        Property TMCGroup() As engroupTypeMap
        ReadOnly Property RBDS() As System.Boolean

        REM What the hell ? Did I smoke crack ?
        REM This is the only needed feature for a future expandibilty - which is the dual-tuner
        REM Second tuner can add more PI's than currently transmitted by EON and also the fieldstrength of the other PI's (!)
        ReadOnly Property FieldStrength() As System.Int16

    End Interface





    Public Class ProgramIdentifier
        'Implements I_ProgramIdentifier



        Private mPiCode As System.Int16
        Private mCounTryCode As System.Byte
        Private mAreaCoverage As enCoverageAreaCode = enCoverageAreaCode.Undefined
        Private mReferenceNumber As System.Int16
        Private mGroupType As engroupTypeMap
        Private mGroupVersion As enGroupVersionMap
        Private mTrafficProgram As System.Boolean
        Private mProgramType As enPTY
        Private mTrafficAnnouncement As System.Boolean

        'Missing!
        Private mTMC As Integer

        Private mMusicSpeechSwitch As enMS = enMS.Undefined
        Private mProgramServiceSegmentAddress As System.Byte
        Private mDecoderIdentification As enDICode = enDICode.Undefined
        Private mTuningFrequency As New FMRadio.FMRadioHAL.Frequency(FMRadio.FMRadioHAL.Frequency.FREQ_UNDEFINED)

        Private mtempAlternativeFrequencyList As AlternativeFrequencyList


        Private mAlternativeFrequencyLists As New AFListDictionary



        Private mProgramServiceName() As Char = New Char(8) {}
        Private mTextABFlag As System.Boolean
        Private mTextSegmentAddressCode As System.Int16
        Private mRadioText() As Char = New Char(64) {}
        Private mModifiedJulianDayCode As System.Int32
        Private mHour As System.Int16
        Private mMinute As System.Int16
        Private mOffset As System.Int16
        Private mDate As DateTime
        Private mExtendedCounTryCode As enCountryCodeMapExt
        Private mLinkageActuator As System.Boolean
        Private mProgramTypeNameABFlag As System.Boolean
        Private mProgramTypeNameSegmentAddress As System.Byte
        Private mProgramTypeName() As Char = New Char(8) {}
        Private mlanguageCode As enLanguage = enLanguage.Unkown

        Private mTMCGroup As engroupTypeMap = engroupTypeMap.NotSet

        'Friend alternativeFrequencyHeadDetected As Boolean = False
        'Friend afListLength As Integer = 0
        Public RBDS As System.Boolean


        'Public Property RBDS() As System.Boolean
        '    Set(ByVal Value As System.Boolean)
        '        mlanguageCode = Value
        '    End Set
        '    Get
        '        RBDS = mRBDS
        '    End Get
        'End Property




        Public Sub New(ByVal piCode As System.Int16)
            Me.mPiCode = piCode
        End Sub

        Public Overrides Function ToString() As String
            Return String.Format("({0:x4})", PiCode)
        End Function

        Public Property PiCode() As System.Int16 'Implements I_ProgramIdentifier.PiCode
            Get
                Return mPiCode
            End Get
            Set(ByVal Value As System.Int16)
                mPiCode = Value
            End Set
        End Property

        Public Property LanguageCode() As enLanguage 'Implements I_ProgramIdentifier.languageCode
            Get
                Return mlanguageCode
            End Get
            Set(ByVal Value As enLanguage)
                mlanguageCode = Value
            End Set
        End Property

        Public Property CountryCode() As System.Byte
            Get
                Return mCounTryCode
            End Get
            Set(ByVal Value As System.Byte)
                mCounTryCode = Value
            End Set
        End Property

        Public Property AreaCoverage() As enCoverageAreaCode
            Get
                Return mAreaCoverage
            End Get
            Set(ByVal Value As enCoverageAreaCode)
                mAreaCoverage = Value
            End Set
        End Property

        Public Property ReferenceNumber() As System.Int16
            Get
                Return mReferenceNumber
            End Get
            Set(ByVal Value As System.Int16)
                mReferenceNumber = Value
            End Set
        End Property

        Public Property GroupType() As engroupTypeMap
            Get
                Return mGroupType
            End Get
            Set(ByVal Value As engroupTypeMap)
                mGroupType = Value
            End Set
        End Property

        Public Property GroupVersion() As enGroupVersionMap
            Get
                Return mGroupVersion
            End Get
            Set(ByVal Value As enGroupVersionMap)
                mGroupVersion = Value
            End Set
        End Property

        Public Property TrafficProgram() As System.Boolean
            Get
                Return mTrafficProgram
            End Get
            Set(ByVal Value As System.Boolean)
                mTrafficProgram = Value
            End Set
        End Property

        Public Property ProgramType() As enPTY
            Get
                Return mProgramType
            End Get
            Set(ByVal Value As enPTY)
                If RBDS Then
                    Select Case CInt(Value)
                        Case 0, 1, 30, 31
                            mProgramType = Value
                        Case 2
                            mProgramType = enPTY.Information
                        Case 3
                            mProgramType = enPTY.Sport
                        Case 4
                            mProgramType = enPTY.Talk
                        Case 5
                            mProgramType = enPTY.RockMusic
                        Case 6
                            mProgramType = enPTY.ClassicRock
                        Case 7
                            mProgramType = enPTY.AdultHits
                        Case 8
                            mProgramType = enPTY.SoftRock
                        Case 9
                            mProgramType = enPTY.Top40
                        Case 10
                            mProgramType = enPTY.Country
                        Case 11
                            mProgramType = enPTY.Oldies
                        Case 12
                            mProgramType = enPTY.Soft
                        Case 13
                            mProgramType = enPTY.Nostalgica
                        Case 14
                            mProgramType = enPTY.Jazz
                        Case 15
                            mProgramType = enPTY.Classic
                        Case 16
                            mProgramType = enPTY.RhytmBlues
                        Case 17
                            mProgramType = enPTY.SoftRhytmBlues
                        Case 18
                            mProgramType = enPTY.ForeignLanguage
                        Case 19
                            mProgramType = enPTY.ReligiousMusic
                        Case 20
                            mProgramType = enPTY.ReligiousTalk
                        Case 21
                            mProgramType = enPTY.Personality
                        Case 22
                            mProgramType = enPTY.Public_
                        Case 23
                            mProgramType = enPTY.College
                        Case 24 - 28
                            mProgramType = enPTY.Unassigned
                        Case 29
                            mProgramType = enPTY.Weather
                        Case Else
                            REM BUG!
                    End Select
                Else
                    mProgramType = Value
                End If
            End Set
        End Property

        Public Property TrafficAnnouncement() As System.Boolean
            Get
                Return mTrafficAnnouncement
            End Get
            Set(ByVal Value As System.Boolean)
                mTrafficAnnouncement = Value
            End Set
        End Property

        Public ReadOnly Property EnhancedOtherNetwork() As Boolean
            Get
                Return (Not (mTrafficProgram) And (mTrafficAnnouncement))
            End Get
        End Property

        Public Property MusicSpeechSwitch() As enMS 'Implements I_ProgramIdentifier.MusicSpeechSwitch
            Get
                Return mMusicSpeechSwitch
            End Get
            Set(ByVal Value As enMS)
                mMusicSpeechSwitch = Value
            End Set
        End Property

        Public Property ProgramServiceSegmentAddress() As System.Byte
            Get
                Return mProgramServiceSegmentAddress
            End Get
            Set(ByVal Value As System.Byte)
                mProgramServiceSegmentAddress = Value
            End Set
        End Property

        Public Property DecoderIdentification() As enDICode
            Get
                Return mDecoderIdentification
            End Get
            Set(ByVal Value As enDICode)
                mDecoderIdentification = Value
            End Set
        End Property

        Public Property TuningFrequency() As FMRadio.FMRadioHAL.Frequency
            Get
                Return mTuningFrequency
            End Get
            Set(ByVal Value As FMRadio.FMRadioHAL.Frequency)
                mTuningFrequency = Value
            End Set
        End Property


        Friend Sub AddAlternativeFrequencyList(ByVal afListLength As Integer, ByVal tuningFrequency As FMRadio.FMRadioHAL.Frequency)
            ' If the previous AF list was not complete, scrap it.
            If Not (mtempAlternativeFrequencyList Is Nothing) AndAlso mtempAlternativeFrequencyList.isComplete() Then
                ' is there and complete !
                If Not mAlternativeFrequencyLists.Contains(mtempAlternativeFrequencyList.TuningFrequency) Then
                    ' this freq.list ist not in the common f.list collection - so add it
                    Debug.WriteLine("Adding AF Header '" + mtempAlternativeFrequencyList.getHead() + "'")
                    mAlternativeFrequencyLists.Add(mtempAlternativeFrequencyList.TuningFrequency, mtempAlternativeFrequencyList)
                Else
                    'already there !    
                    'compare 
                    If mtempAlternativeFrequencyList.Equals(mAlternativeFrequencyLists.Item(mtempAlternativeFrequencyList.TuningFrequency)) Then
                        mAlternativeFrequencyLists.Item(mtempAlternativeFrequencyList.TuningFrequency).ReceiveCounter += 1
                    Else
                        'different !!!
                        'delete current if it was only transmitted once 
                        If mAlternativeFrequencyLists.Item(mtempAlternativeFrequencyList.TuningFrequency).ReceiveCounter = 0 Then
                            mAlternativeFrequencyLists.Remove(mtempAlternativeFrequencyList.TuningFrequency)
                            mAlternativeFrequencyLists.Add(mtempAlternativeFrequencyList.TuningFrequency, mtempAlternativeFrequencyList)
                        End If
                    End If
                End If
            End If

            mtempAlternativeFrequencyList = New AlternativeFrequencyList(afListLength, tuningFrequency)
        End Sub

        Friend Sub AddAlternativeFrequencies(ByVal af1 As Byte, ByVal af2 As Byte)
            If mtempAlternativeFrequencyList Is Nothing Then
                Debug.WriteLine("!noAFHead until now")
            Else
                mtempAlternativeFrequencyList.AddAlternativeFrequencies(af1, af2)
            End If
        End Sub

        Public ReadOnly Property CurrentAlternativeFrequencyList() As AlternativeFrequencyList 'returns the AlternativeFrequencyList of the currrent PI
            Get
                'Me.TuningFrequency()
                If AlternativeFrequencyLists.Contains(Me.TuningFrequency) Then
                    Return AlternativeFrequencyLists(Me.TuningFrequency)
                Else
                    REM then there is only 1 AF List from EON!
                    If (AlternativeFrequencyLists.Contains(New FMRadio.FMRadioHAL.Frequency(FMRadio.FMRadioHAL.Frequency.FREQ_UNDEFINED))) And (AlternativeFrequencyLists.Count = 1) Then
                        Return AlternativeFrequencyLists(New FMRadio.FMRadioHAL.Frequency(FMRadio.FMRadioHAL.Frequency.FREQ_UNDEFINED))
                    End If
                End If
            End Get
        End Property

        Public ReadOnly Property AlternativeFrequencyLists() As AFListDictionary
            Get
                Return mAlternativeFrequencyLists
            End Get
        End Property


        Public Property ProgramServiceName() As Char()
            Get
                Return mProgramServiceName
            End Get
            Set(ByVal Value As Char())
                mProgramServiceName = Value
            End Set
        End Property

        Public Property RadioTextABFlag() As System.Boolean
            Get
                Return mTextABFlag
            End Get
            Set(ByVal Value As System.Boolean)
                mTextABFlag = Value
            End Set
        End Property

        Public Property TextSegmentAddressCode() As System.Int16
            Get
                Return mTextSegmentAddressCode
            End Get
            Set(ByVal Value As System.Int16)
                mTextSegmentAddressCode = Value
            End Set
        End Property

        Public Property RadioText() As Char()
            Get
                Return mRadioText
            End Get
            Set(ByVal Value As Char())
                mRadioText = Value
            End Set
        End Property

        Public Property ModifiedJulianDayCode() As System.Int32
            Get
                Return mModifiedJulianDayCode
            End Get
            Set(ByVal Value As System.Int32)
                mModifiedJulianDayCode = Value
            End Set
        End Property

        Public Property RDSDateTime() As DateTime
            Get
                Return mDate
            End Get
            Set(ByVal Value As DateTime)
                mDate = Value
            End Set
        End Property


        'Public Property Hour() As Integer
        '    Get
        '        Return mHour
        '    End Get
        '    Set(ByVal Value As Integer)
        '        mHour = Value
        '    End Set
        'End Property

        'Public Property Minute() As Integer
        '    Get
        '        Return mMinute
        '    End Get
        '    Set(ByVal Value As Integer)
        '        mMinute = Value
        '    End Set
        'End Property

        'Public Property Offset() As Integer
        '    Get
        '        Return mOffset
        '    End Get
        '    Set(ByVal Value As Integer)
        '        mOffset = Value
        '    End Set
        'End Property

        Public Property ExtendedCounTryCode() As enCountryCodeMapExt
            Get
                Return mExtendedCounTryCode
            End Get
            Set(ByVal Value As enCountryCodeMapExt)
                mExtendedCounTryCode = Value
            End Set
        End Property

        Public Property LinkageActuator() As System.Boolean
            Get
                Return mLinkageActuator
            End Get
            Set(ByVal Value As System.Boolean)
                mLinkageActuator = Value
            End Set
        End Property

        Public Property ProgramTypeNameABFlag() As System.Boolean
            Get
                Return mProgramTypeNameABFlag
            End Get
            Set(ByVal Value As System.Boolean)
                mProgramTypeNameABFlag = Value
            End Set
        End Property

        Public Property ProgramTypeNameSegmentAddress() As System.Byte
            Get
                Return mProgramTypeNameSegmentAddress
            End Get
            Set(ByVal Value As System.Byte)
                mProgramTypeNameSegmentAddress = Value
            End Set
        End Property

        Public Property ProgramTypeName() As Char()
            Get
                Return mProgramTypeName
            End Get
            Set(ByVal Value As Char())
                mProgramTypeName = Value
            End Set
        End Property

        REM What the hell ? Did I smoke crack ?
        REM This is the only needed feature for a future expandibilty - which is the dual-tuner
        REM Second tuner can add more PI's than currently transmitted by EON and also the fieldstrength of the other PI's (!)
        Public ReadOnly Property FieldStrength() As System.Int16
            Get

            End Get
        End Property


        Public Property TMCGroup() As engroupTypeMap
            Get
                Return mTMCGroup
            End Get
            Set(ByVal Value As engroupTypeMap)
                mTMCGroup = Value
            End Set
        End Property

    End Class
End Namespace

