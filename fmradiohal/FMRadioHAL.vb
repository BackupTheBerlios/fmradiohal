Option Strict On

Imports System
' To use this .NET Assembly as COM Object
Imports System.Runtime.InteropServices
Imports System.Reflection
Imports System.Threading

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

        Private WithEvents mCom As HQCT.Communication
        Private mDevice As HQCT.Device

        'Private mReportConsumerThread As System.Threading.Thread
        'Private mReportConsumer As HQCT.ReportConsumer
        'Private mTunerSettings As HQCT.TunerSettings
        'Private WithEvents offmSignalProcessorStatus As HQCT.SignalProcessorStatus

        'Private WithEvents mRDSStatus As RDS.RDSStatus

        Private mMute As Boolean
        Private mStereoMono As Boolean
        Private mStrError As String

        Private mConnected As System.Boolean

        'Private mFreq As New FMRadioHAL.Frequency

        'Public Function GetLastError() As FMRadioHAL.stError Implements FMRadioHAL.I_FMRadioHAL.GetLastError
        '    Dim mErr As FMRadioHAL.stError
        '    mErr.strError = mStrError
        '    Return mErr
        'End Function

        ' returns the supported functions (see struct stSupFunc) 

        Public Function Supported_Functions() As FMRadioHAL.stSupFunc Implements FMRadioHAL.I_FMRadioHAL.Supported_Functions

            Supported_Functions.AF = True
            Supported_Functions.Autotune = True

            Supported_Functions.FM.MinFreq = 8750
            Supported_Functions.FM.MaxFreq = 10800
            Supported_Functions.FM.TuneStep = 10


            Supported_Functions.MW.MinFreq = 500
            Supported_Functions.MW.MaxFreq = 1600
            Supported_Functions.MW.TuneStep = 10

            Supported_Functions.LW.MinFreq = 150
            Supported_Functions.LW.MaxFreq = 280
            Supported_Functions.LW.TuneStep = 10

            'no SW, band
            Supported_Functions.SW.MinFreq = 0
            Supported_Functions.SW.MaxFreq = 0
            Supported_Functions.SW.TuneStep = 0

            Supported_Functions.Freq = True
            Supported_Functions.FreqUpDown = True

            Supported_Functions.Stereo = True
            Supported_Functions.Mute = True
            Supported_Functions.Vol = True
            Supported_Functions.VolUpDown = True
        End Function

        '
        Public Sub Connect() Implements FMRadioHAL.I_FMRadioHAL.Connect

            mDevice = New HQCT.Device

            'mReportConsumer = New HQCT.ReportConsumer(mCom, mDevice)
            'mReportConsumerThread = New System.Threading.Thread(AddressOf mReportConsumer.ConsumeReport)

            'mTunerSettings = New HQCT.TunerSettings(mCom)
            'mSignalProcessorStatus = New HQCT.SignalProcessorStatus
            'mRDSStatus = New RDS.RDSStatus


            mCom = New HQCT.Communication(mDevice) ' , mReportConsumerThread)

            'If mCom.Connect Then
            'mReportConsumer.mCom = mCom
            mConnected = mCom.Connect()


            'mDevice.Freq.Value = Me.Supported_Functions.FM.MinFreq
            'mCom.Tune(New Frequency(mDevice.Freq.Value))
            'End If
        End Sub

        'close
        Public Sub DisConnect() Implements FMRadioHAL.I_FMRadioHAL.DisConnect

            mCom.Disconnect()
            mCom = Nothing

            mDevice = Nothing


            'mSignalProcessorStatus = Nothing
            'mRDSStatus = Nothing
            'If Not (mReportConsumer Is Nothing) Then
            '    mReportConsumer.RequestStop()
            'End If
            mConnected = False
        End Sub

        Public Sub AutoTune(ByVal Direction As FMRadioHAL.enDirections, ByVal StopLevel As Int16) Implements FMRadioHAL.I_FMRadioHAL.AutoTune 'Richtung Vorwärts/Rückwärts , minimale Feldstärke (DX Function)
            Select Case Direction
                Case enDirections.Down
                    Do
                        Me.FreqDown()
                        System.Windows.Forms.Application.DoEvents()
                        Thread.Sleep(200)
                        System.Windows.Forms.Application.DoEvents()
                    Loop Until Me.mDevice.Level > StopLevel
                Case enDirections.UP
                    Do
                        Me.FreqUp()
                        System.Windows.Forms.Application.DoEvents()
                        Thread.Sleep(200)
                        System.Windows.Forms.Application.DoEvents()
                    Loop Until Me.mDevice.Level > StopLevel
            End Select

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
                Return mDevice.Freq
            End Get
            Set(ByVal lValue As Frequency)
                mDevice.Freq = lValue
                mCom.Tune(mDevice.Freq)
            End Set
        End Property

        Public Sub FreqUp() Implements FMRadioHAL.I_FMRadioHAL.FreqUP
            If mDevice.Freq.Value = mDevice.Freq.FREQ_UNDEFINED Then
                mDevice.Freq.Value = Me.Supported_Functions.FM.MinFreq
            Else
                If mDevice.Freq.Value >= Me.Supported_Functions.FM.MaxFreq Then
                    mDevice.Freq.Value = Me.Supported_Functions.FM.MinFreq
                Else
                    mDevice.Freq.Value = mDevice.Freq.Value + CShort(10)
                End If
            End If
            Me.Freq = mDevice.Freq
        End Sub
        Public Sub FreqDown() Implements FMRadioHAL.I_FMRadioHAL.FreqDown
            If mDevice.Freq.Value = mDevice.Freq.FREQ_UNDEFINED Then
                mDevice.Freq.Value = Me.Supported_Functions.FM.MaxFreq
            Else
                If mDevice.Freq.Value <= Me.Supported_Functions.FM.MinFreq Then
                    mDevice.Freq.Value = Me.Supported_Functions.FM.MaxFreq
                Else
                    mDevice.Freq.Value = mDevice.Freq.Value - CShort(10)
                End If
            End If
            Me.Freq = mDevice.Freq
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

        Public Property AFLevel() As Int16 Implements FMRadioHAL.I_FMRadioHAL.AFLevel '0=AF Funktion AUS, unterschreiten der hier angegeben Feldstärke und der nächste Sender (gem. AF Liste) wird vom µC "getunt"
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
            If CBool(AssemblySettings.GetItem("Trace")) = True Then
                Dim Listener As New System.Diagnostics.TextWriterTraceListener([Assembly].GetExecutingAssembly.Location + ".log")
                System.Diagnostics.Trace.Listeners.Add(Listener)
                Trace.AutoFlush = True
                Trace.WriteLine("Trace started:" + DateTime.Now.ToString)
            End If
        End Sub

        Private Sub mCom_RDS_Message(ByRef RDSMessage As stRDSRAWMessage) Handles mCom.RDS_Message
            RDSMessage.TunedFreq = Freq
            RaiseEvent RDSRAWMessageavailable(RDSMessage)
        End Sub



        Private Sub mCom_FS_Message(ByVal Value As Short) Handles mCom.FS_Message
            RaiseEvent FieldStrength(Value)
        End Sub
    End Class

End Namespace