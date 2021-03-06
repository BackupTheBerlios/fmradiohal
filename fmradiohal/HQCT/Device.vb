Option Strict On
Imports System
'Imports System.Collections.Generic
Imports System.Text
Namespace HQCT
    Friend Enum enHQCT_STATUS
        HQCT_STATUS_ILLEGAL = -1

        HQCT_STATUS_INIT = 0
        HQCT_STATUS_EEPROM1 = 1
        HQCT_STATUS_EEPROM2 = 2
        HQCT_STATUS_EEPROM3 = 3
        HQCT_STATUS_EEPROM_END = 4
        HQCT_STATUS_NORMAL = 5

        'HQCT_STATUS_SEEKING_UP1 = 5
        'HQCT_STATUS_SEEKING_UP2 = 6
        'HQCT_STATUS_SEEKING_DN1 = 7
        'HQCT_STATUS_SEEKING_DN2 = 8
    End Enum


    Friend Class Device
        ' Status 


        Private Const SETTINGS_FILE_NAME As String = "device.dat"
        ' Tuning characteristics 

        Public DEVICE_STATUS_NORMAL_FLAG As New System.Threading.AutoResetEvent(False)
        ' bytes used in tuning - computed from EEPROM 

        Private m_fmTrailer As Byte() = {136, 160, 92, 192, 118}
        ' bytes used in tuning - computed from EEPROM 
        Private m_amTrailer As Byte() = {157, 16, 114, 0, 0}
        ' alignment data - computed from EEPROM 
        Private daa As Byte() = {91, 89, 86, 84, 84, 85, 90}
        ' alignment frequencies - computed from EEPROM 
        'Private m_alignFreq As Integer() = {87500, 90900, 94300, 97700, 101200, 104600, 108000}
        ' state data 
        Private m_freq As New FMRadio.FMRadioHAL.Frequency
        ' current frequency 
        'Private otherBandFreq As Integer = 450
        ' current frequency 
        Private m_status As enHQCT_STATUS = enHQCT_STATUS.HQCT_STATUS_INIT
        ' driver state 
        Private m_band As FMRadioHAL.enBand = FMRadioHAL.enBand.NotDefined
        'Private m_param As Integer
        ' extra storage for the current state 
        Private m_Level As Short
        Private m_tuneBuffer As Byte() = New Byte(5) {}

        Private mSettings(96) As Byte
        Private mEEPROM(32 * 3) As Byte
        Public Property EEPROM() As Byte()
            Get
                EEPROM = mEEPROM
            End Get
            Set(ByVal Value As Byte())
                mEEPROM = Value
            End Set
        End Property

        Public Property TunerSettings() As Byte()
            Get
                Return mSettings
            End Get
            Set(ByVal Value As Byte())
                mSettings = Value
            End Set
        End Property

        ' temporary buffer for tuning control 
        Public Sub New()
            'readSettingsFromFile()
        End Sub
        'Private Sub readSettingsFromFile()
        '    If System.IO.File.Exists(SETTINGS_FILE_NAME) Then
        '        Dim input As Byte() = System.IO.File.ReadAllBytes(SETTINGS_FILE_NAME)
        '        If input.Length > 5 Then
        '            m_freq = input(0) << 24 Or input(1) << 16 Or input(2) << 8 Or input(3)
        '            m_band = input(4)
        '            m_minLevel = input(5)
        '        End If
        '    End If
        'End Sub
        'Public Sub writeSettingsToFile()
        '    Dim output As Byte() = New Byte() {DirectCast(((m_freq >> 24) And 255), Byte), DirectCast(((m_freq >> 16) And 255), Byte), DirectCast(((m_freq >> 8) And 255), Byte), DirectCast((m_freq And 255), Byte), DirectCast(m_band, Byte), DirectCast(m_minLevel, Byte)}
        '    System.IO.File.WriteAllBytes(SETTINGS_FILE_NAME, output)
        'End Sub
        'Public Property FmTrailer() As Byte()
        '    Get
        '        Return m_fmTrailer
        '    End Get
        '    Set(ByVal Value As Byte())
        '        m_fmTrailer = Value
        '    End Set
        'End Property
        'Public Property AmTrailer() As Byte()
        '    Get
        '        Return m_amTrailer
        '    End Get
        '    Set(ByVal Value As Byte())
        '        m_amTrailer = Value
        '    End Set
        'End Property
        'Public Property DAAs() As Byte()
        '    Get
        '        Return daa
        '    End Get
        '    Set(ByVal Value As Byte())
        '        daa = Value
        '    End Set
        'End Property
        'Public Property AlignFreq() As Integer()
        '    Get
        '        Return m_alignFreq
        '    End Get
        '    Set(ByVal Value As Integer())
        '        m_alignFreq = Value
        '    End Set
        'End Property
        Public Property TuneBuffer() As Byte()
            Get
                Return m_tuneBuffer
            End Get
            Set(ByVal Value As Byte())
                m_tuneBuffer = Value
            End Set
        End Property
        'Public Property Param() As Integer
        '    Get
        '        Return m_param
        '    End Get
        '    Set(ByVal Value As Integer)
        '        m_param = Value
        '    End Set
        'End Property
        Public Property Band() As FMRadioHAL.enBand
            Get
                Return m_band
            End Get
            Set(ByVal Value As FMRadioHAL.enBand)

            End Set
        End Property
        'Friend Property Band() As enHQCT_BAND
        '    Get
        '        Return m_band
        '    End Get
        '    Set(ByVal Value As enHQCT_BAND)
        '        If m_band <> Value Then
        '            Dim temp As Integer = m_freq
        '            m_freq = otherBandFreq
        '            otherBandFreq = temp
        '        End If
        '        m_band = Value
        '    End Set
        'End Property
        Friend Property Status() As enHQCT_STATUS
            Get
                SyncLock Me
                    Status = m_status
                End SyncLock
            End Get
            Set(ByVal Value As enHQCT_STATUS)
                SyncLock Me
                    m_status = Value
                End SyncLock
                If Value = enHQCT_STATUS.HQCT_STATUS_NORMAL Then
                    DEVICE_STATUS_NORMAL_FLAG.Set()
                End If
            End Set
        End Property
        Public Property Freq() As FMRadio.FMRadioHAL.Frequency
            Get
                Return m_freq
            End Get
            Set(ByVal Value As FMRadio.FMRadioHAL.Frequency)
                m_freq = Value
            End Set
        End Property
        Public Property Level() As Short
            Get
                SyncLock Me
                    Return m_Level
                End SyncLock
            End Get
            Set(ByVal Value As Short)
                SyncLock Me
                    m_Level = Value
                End SyncLock
            End Set
        End Property
    End Class
End Namespace
