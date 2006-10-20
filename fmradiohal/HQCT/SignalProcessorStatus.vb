Option Strict On

Imports System

Imports System.Text
Imports System.Diagnostics

Namespace HQCT
    Friend Class SignalProcessorStatus
        Private mStereoIndicator As Boolean
        Private mAsiActive As Boolean
        Private mafUpdateSample As Boolean
        Private mpowerOnReset As Boolean
        Private mrdsDataAvailable As Boolean
        Private mDeviceText As String
        Private mlevelVoltage As Integer
        Private multrasonicNoise As Integer
        Private mwidebandAM As Integer
        Private mifCount As Integer
        Private mportA As Integer
        Private mportB As Integer
        Private mportC As Integer
        Private mportD As Integer
        Private mlastSetting As Integer


        Public Delegate Sub FS_Delegate(ByVal Strength As System.Int16)

        Public Event FS_Message As FS_Delegate

        Public Property StereoIndicator() As Boolean
            Get
                Return mStereoIndicator
            End Get
            Set(ByVal Value As Boolean)
                mStereoIndicator = Value
            End Set
        End Property



        Public Property AsiActive() As Boolean
            Get
                Return mAsiActive
            End Get
            Set(ByVal Value As Boolean)
                mAsiActive = Value
            End Set
        End Property


        Public Property AfUpdateSample() As Boolean
            Get
                Return mafUpdateSample
            End Get
            Set(ByVal Value As Boolean)
                mafUpdateSample = Value
            End Set
        End Property


        Public Property PowerOnReset() As Boolean
            Get
                Return mpowerOnReset
            End Get
            Set(ByVal Value As Boolean)
                mpowerOnReset = Value
            End Set
        End Property


        Public Property RdsDataAvailable() As Boolean
            Get
                Return mrdsDataAvailable
            End Get
            Set(ByVal Value As Boolean)
                mrdsDataAvailable = Value
            End Set
        End Property


        Public Property DeviceText() As String
            Get
                Return mDeviceText
            End Get
            Set(ByVal Value As String)
                mDeviceText = Value
            End Set
        End Property


        Public Property LevelVoltage() As Integer
            Get
                Return mlevelVoltage
            End Get
            Set(ByVal Value As Integer)
                mlevelVoltage = Value
            End Set
        End Property


        Public Property UltrasonicNoise() As Integer
            Get
                Return multrasonicNoise
            End Get
            Set(ByVal Value As Integer)
                multrasonicNoise = Value
            End Set
        End Property


        Public Property WidebandAM() As Integer
            Get
                Return mwidebandAM
            End Get
            Set(ByVal Value As Integer)
                mwidebandAM = Value
            End Set
        End Property


        Public Property IfCount() As Integer
            Get
                Return mifCount
            End Get
            Set(ByVal Value As Integer)
                mifCount = Value
            End Set
        End Property


        Public Property PortA() As Integer
            Get
                Return mportA
            End Get
            Set(ByVal Value As Integer)
                mportA = Value
            End Set
        End Property


        Public Property PortB() As Integer
            Get
                Return mportB
            End Get
            Set(ByVal Value As Integer)
                mportB = Value
            End Set
        End Property


        Public Property PortC() As Integer
            Get
                Return mportC
            End Get
            Set(ByVal Value As Integer)
                mportC = Value
            End Set
        End Property


        Public Property LastSetting() As Integer
            Get
                Return mlastSetting
            End Get
            Set(ByVal Value As Integer)
                mlastSetting = Value
            End Set
        End Property

        Public Sub decode(ByVal inputBuffer() As Byte)
            ' Byte 0 is the HID Report ID
            ' What is byte 1? Seems like it's empty
            Dim byteOffset As Integer = 1
            '              Byte 1 is STATUS
            '              * 
            '              * BIT      SYMBOL      DESCRIPTION
            '              * 7        STIN        Stereo indicator. 0 = no pilot signal detected 1 = pilot signal detected.
            '              * 6        ASIA        ASI active. 0 = not active 1 = ASI step is in progress.
            '              * 5        AFUS        AF update sample. 0 = LEV, USN and WAM information is taken from main frequency
            '              *                       (continuous mode) 1 = LEV, USN and WAM information is taken from alternative
            '              *                       frequency. Continuous mode during AF update and sampled mode after AF update.
            '              *                       Sampled mode reverts to continuous main frequency information after read.
            '              * 4        POR         Power-on reset. 0 = standard operation (valid I2C-bus register settings) 1 = Power-on
            '              *                       reset detected since last read cycle (I2C-bus register reset). After read the bit will reset
            '              *                       to POR = 0.
            '              * 3        RDAV        RDS data available. This bit indicates, that RDS block data is available.
            '              * 2 to 0   ID(2:0)     Identification. TEF6892H device type identification ID(2:0) = 010.
            '              *
            '              * ID2 ID1 ID0      TYPE
            '              * 0   0   0        TEF6890H
            '              * 0   1   0        TEF6892H
            '              * 1   0   0        TEF6894H
            '              */

            ' byteOffset should be 1 here.
            Dim status As Byte = inputBuffer(byteOffset) : byteOffset = byteOffset + 1
            StereoIndicator = ((status And 128) = 128)
            AsiActive = ((status And 64) = 64)
            AfUpdateSample = ((status And 32) = 32)
            PowerOnReset = ((status And 16) = 16)
            RdsDataAvailable = ((status And 8) = 8)

            Select Case status And 7
                Case 0
                    DeviceText = "TEF6890H"

                Case 2
                    DeviceText = "TEF6892H"

                Case 4
                    DeviceText = "TEF6894H"

                Case Else
                    DeviceText = "Unknown"
            End Select
            '             
            '              * Byte 2 is LEVEL
            '              * 
            '              * BIT      SYMBOL      DESCRIPTION
            '              * 7 to 0   LEV(7:0)    Level. 8-bit value of level voltage from tuner.
            '              */

            ' byteOffset should be 2 here.
            Dim level As Byte = inputBuffer(byteOffset) : byteOffset = byteOffset + 1
            LevelVoltage = level
            '             
            '              * Byte 3 is QUALITY
            '              * 
            '              * BIT      SYMBOL      DESCRIPTION
            '              * 7 to 4   USN(3:0)    Ultrasonic noise detector. USN content of the MPXRDS audio signal.
            '              * 3 to 0   WAM(3:0)    Wideband AM detector. WAM content of the LEVEL voltage.
            '              */

            ' byteOffset should be 3 here.
            Dim quality As Byte = inputBuffer(byteOffset) : byteOffset = byteOffset + 1
            UltrasonicNoise = (quality And 240) >> 4
            WidebandAM = quality And 15
            Dim Result As Short = CShort(LevelVoltage)

            If UltrasonicNoise = 0 Then
                Result = Result << 7
            Else
                Result = (Result << 7) \ CShort(UltrasonicNoise)
            End If
            If WidebandAM > 0 Then
                Result = CShort(Result) \ CShort(WidebandAM)
            End If
            RaiseEvent FS_Message(Result)

            ' Custom details sent by onboard PIC
            IfCount = inputBuffer(20)
            PortA = inputBuffer(21)
            PortB = inputBuffer(22)
            PortC = inputBuffer(23)
            LastSetting = inputBuffer(24)
        End Sub
    End Class
End Namespace
