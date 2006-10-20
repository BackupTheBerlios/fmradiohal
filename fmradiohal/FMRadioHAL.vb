Option Strict On

Imports System
' To use this .NET Assembly as COM Object
Imports System.Runtime.InteropServices
Imports System.Reflection


Namespace FMRadioHAL
    Public Delegate Sub RDSRAWMessageavailable_Delegate(ByRef RDSRAWMessage As stRDSRAWMessage)
    Public Delegate Sub AFNewFreq_Delegate(ByVal NewFreq As FMRadioHAL.Frequency)
    Public Delegate Sub FieldStrength_Delegate(ByVal Level As System.Int16)
    Public Delegate Sub Stereo_Delegate(ByVal Stereo_Pilot_Detected As System.Boolean)
    Public Delegate Sub AutoTuneFinished_Delegate(ByVal Freq As FMRadioHAL.Frequency)

    Public Delegate Sub AnyEvent_Delegate(ByRef Anything As Object)

    <ComSourceInterfaces(GetType(I_FMRadioHAL_Events))> _
    Public Class C_FMRadioHAL
        Implements FMRadioHAL.I_FMRadioHAL 'do not remove !!! it forces you to implement those PUBLIC members...

        Public Event AFNewFreq As AFNewFreq_Delegate
        Public Event RDSRAWMessageavailable As RDSRAWMessageavailable_Delegate
        Public Event FieldStrength As FieldStrength_Delegate
        Public Event Stereo As Stereo_Delegate
        Public Event AutoTuneFinished As AutoTuneFinished_Delegate
        Public Event AnyEvent As AnyEvent_Delegate

        Private mCom As HQCT.Communication
        Private mDevice As HQCT.Device

        Private mReportConsumerThread As System.Threading.Thread
        Private mReportConsumer As HQCT.ReportConsumer
        Private mTunerSettings As HQCT.TunerSettings
        Private WithEvents mSignalProcessorStatus As HQCT.SignalProcessorStatus

        Private WithEvents mRDSStatus As RDS.RDSStatus

        Private mMute As Boolean
        Private mStereoMono As Boolean
        Private mStrError As String

        REM to remove !!!
        Private mFreq As New FMRadioHAL.Frequency(CShort(9440))

        Public Function GetLastError() As FMRadioHAL.stError Implements FMRadioHAL.I_FMRadioHAL.GetLastError
            Dim mErr As FMRadioHAL.stError
            mErr.strError = mStrError
            Return mErr
        End Function

        ' returns the supported functions (see struct stSupFunc) 
        Public Function Supported_Functions() As FMRadioHAL.stSupFunc Implements FMRadioHAL.I_FMRadioHAL.Supported_Functions
            Dim SupFunc As FMRadioHAL.stSupFunc
            SupFunc.AF = True
            SupFunc.Autotune = True

            SupFunc.FM.MinFreq = 6000
            SupFunc.FM.MaxFreq = 16000
            SupFunc.FM.TuneStep = 10


            SupFunc.MW.MinFreq = 500
            SupFunc.MW.MaxFreq = 1600
            SupFunc.MW.TuneStep = 10

            SupFunc.LW.MinFreq = 150
            SupFunc.LW.MaxFreq = 280
            SupFunc.LW.TuneStep = 10

            'no SW, band
            SupFunc.SW.MinFreq = 0
            SupFunc.SW.MaxFreq = 0
            SupFunc.SW.TuneStep = 0

            SupFunc.Freq = True
            SupFunc.FreqUpDown = True

            SupFunc.Stereo = True
            SupFunc.Mute = True
            SupFunc.Vol = True
            SupFunc.VolUpDown = True
        End Function

        '
        Public Sub Connect() Implements FMRadioHAL.I_FMRadioHAL.Connect
            mDevice = New HQCT.Device
            mCom = New HQCT.Communication(mDevice)
            mTunerSettings = New HQCT.TunerSettings(mCom)
            mSignalProcessorStatus = New HQCT.SignalProcessorStatus
            mRDSStatus = New RDS.RDSStatus

            mReportConsumer = New HQCT.ReportConsumer(mCom, mDevice, mSignalProcessorStatus, mRDSStatus, mTunerSettings)
            mReportConsumerThread = New System.Threading.Thread(AddressOf mReportConsumer.ConsumeReport)
            mReportConsumerThread.Start()

        End Sub

        'close
        Public Sub DisConnect() Implements FMRadioHAL.I_FMRadioHAL.DisConnect
            mDevice = Nothing
            mCom = Nothing
            mTunerSettings = Nothing
            mSignalProcessorStatus = Nothing
            mRDSStatus = Nothing
            If Not (mReportConsumer Is Nothing) Then
                mReportConsumer.RequestStop()
            End If
        End Sub

        Public Sub AutoTune(ByVal Direction As FMRadioHAL.enDirections, ByVal StopLevel As Int16) Implements FMRadioHAL.I_FMRadioHAL.AutoTune 'Richtung Vorw�rts/R�ckw�rts , minimale Feldst�rke (DX Function)
        End Sub


        Public Property Band() As FMRadioHAL.enBand Implements FMRadioHAL.I_FMRadioHAL.Band
            Get
                Return FMRadioHAL.enBand.FM
            End Get
            Set(ByVal Value As FMRadioHAL.enBand)
                Select Case Value
                    Case FMRadioHAL.enBand.FM
                    Case Else
                        mStrError = "Radiator supports only FM Band"
                        Err.Raise(-1, , mStrError)
                End Select
            End Set
        End Property

        Public Property Freq() As Frequency Implements FMRadioHAL.I_FMRadioHAL.Freq
            Get
                Return mFreq
            End Get
            Set(ByVal lValue As Frequency)
                mFreq = lValue
                mCom.tune(lValue.Value * 10)
            End Set
        End Property

        Public Sub FreqUp() Implements FMRadioHAL.I_FMRadioHAL.FreqUP
        End Sub
        Public Sub FreqDown() Implements FMRadioHAL.I_FMRadioHAL.FreqDown
        End Sub
        Public Property Mono() As Boolean Implements FMRadioHAL.I_FMRadioHAL.Mono
            Get
                Return mStereoMono
            End Get
            Set(ByVal Value As Boolean)
            End Set
        End Property

        Public Property Mute() As Boolean Implements FMRadioHAL.I_FMRadioHAL.Mute
            Get
                Return mMute
            End Get
            Set(ByVal Value As Boolean)
            End Set
        End Property

        Public Property AFLevel() As Int16 Implements FMRadioHAL.I_FMRadioHAL.AFLevel '0=AF Funktion AUS, unterschreiten der hier angegeben Feldst�rke und der n�chste Sender (gem. AF Liste) wird vom �C "getunt"
            Get
                Return 0
            End Get
            Set(ByVal Value As Int16)
            End Set
        End Property
        Public Property Volume() As Int16 Implements FMRadioHAL.I_FMRadioHAL.Volume 'Percentage Volume
            Get
                Return 0
            End Get
            Set(ByVal Value As Int16)
                mStrError = ""
                Err.Raise(-1, , mStrError)
            End Set
        End Property
        Public Sub VolumeUP() Implements FMRadioHAL.I_FMRadioHAL.VolumeUP
        End Sub
        Public Sub VolumeDown() Implements FMRadioHAL.I_FMRadioHAL.VolumeDown
        End Sub

        Public Function Info() As String Implements I_FMRadioHAL.Info
            Dim m_AssInfo As System.Reflection.Assembly
            Dim m_Description As System.Reflection.AssemblyTitleAttribute
            m_AssInfo = m_AssInfo.GetCallingAssembly
            m_Description = CType(m_AssInfo.GetCustomAttributes(GetType(System.Reflection.AssemblyTitleAttribute), False)(0), System.Reflection.AssemblyTitleAttribute)
            Return m_Description.Title.ToString
        End Function
        Public Function AnyFunction(ByRef Anything As Object) As Object Implements I_FMRadioHAL.AnyFunction

        End Function
        Protected Overrides Sub Finalize()
            'DisConnect()
            MyBase.Finalize()
        End Sub

        Public Sub New()

        End Sub

        Private Sub mRDSStatus_RDS_Message(ByRef RDSMessage As stRDSRAWMessage) Handles mRDSStatus.RDS_Message
            RDSMessage.TunedFreq = Freq
            RaiseEvent RDSRAWMessageavailable(RDSMessage)
        End Sub

        Private Sub mSignalProcessorStatus_FS_Message(ByVal Strength As Short) Handles mSignalProcessorStatus.FS_Message
            RaiseEvent FieldStrength(Strength)
        End Sub
    End Class

End Namespace