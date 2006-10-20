Option Strict On

Imports System
' To use this .NET Assembly as COM Object
Imports System.Runtime.InteropServices
Imports System.Reflection

'don't change anything here

Namespace FMRadioHAL
    'This Interface must be implemented by your FMRadioHAL class
    Public Interface I_FMRadioHAL
        Property AFLevel() As System.Int16 '0=switches AF Function OFF, if Fieldstrength falls under this value Tuner searches for a better AF

        Sub AutoTune(ByVal Direction As enDirections, ByVal StopLevel As System.Int16)  'commands the tuner to move to the next station

        Property Mono() As System.Boolean 'switches Stereodecoder off - forces Signal to Mono 

        Property Mute() As System.Boolean
        Property Freq() As Frequency
        Sub FreqUP() 'Call if you want to move Frequency one Step up
        Sub FreqDown() 'Call if you want to move Frequency one Step down

        Property Band() As enBand 'Get/Set current Tuner Band
        Property Volume() As System.Int16 'min.Volume=0, max.Volume=32767
        Sub VolumeUP()
        Sub VolumeDown()

        Sub Connect() 'Establish connection (via RS232, Parallelport, USB, ...) to Radio
        Sub DisConnect() 'free resources,...

        Function Supported_Functions() As stSupFunc 'Check this value if you want to know which functions are supported by the current tuner

        Function GetLastError() As stError 'Reports the last occured error

        Function Info() As String 'Reports the Assembly-Attribute "Title" to COM Clients

        Function AnyFunction(ByRef Anything As Object) As Object 'this Function/Sub is for expandibility (if you need a special Event for your frontend which your radio supports!)'this Event is for expandibility (if you need a special Function for your frontend which your radio supports!)

        '...more to add ?

    End Interface


    Public Interface I_Frequency
        Property Value() As System.Int16
        Property RDSFormat(Optional ByVal LF_MF As Boolean = False) As Byte
        Property DoubleValue() As Double
        Property DisplayFormat(Optional ByVal Seperator As String = ",") As String
        ReadOnly Property BandType() As enBand
    End Interface

    Public Class Frequency
        Inherits System.Object
        Implements I_Frequency

        Public Const FREQ_UNDEFINED As System.Int16 = 0

        Private mValue As System.Int16 '8950 for 89,50MHz (800 for 800KHz for AM bands)
        Public Property Value() As System.Int16 Implements I_Frequency.Value
            Get
                Return mValue
            End Get
            Set(ByVal Value As System.Int16)
                mValue = Value
            End Set
        End Property
        Public ReadOnly Property BandType() As enBand Implements I_Frequency.BandType
            Get
                Select Case mValue
                    Case 150 To 280
                        BandType = enBand.LW
                    Case 500 To 1600
                        BandType = enBand.MW
                    Case 8700 To 10800
                        BandType = enBand.FM
                    Case Else
                        BandType = enBand.NotDefined
                End Select
            End Get
        End Property
        Public Property RDSFormat(Optional ByVal LF_MF As Boolean = False) As Byte Implements I_Frequency.RDSFormat
            Get
                Select Case BandType
                    Case enBand.FM
                        Return CByte(mValue - 8750) \ CByte(10)
                    Case enBand.LW
                        Return CByte((mValue - 153) / 9)
                    Case enBand.LW
                        Return CByte((mValue - 531) / 9) + CByte(16)
                    Case Else
                End Select
            End Get
            Set(ByVal Value As Byte)
                mValue = System.Convert.ToInt16(Value) * CShort(10) + CShort(8750)
            End Set
        End Property
        Public Property DoubleValue() As Double Implements I_Frequency.DoubleValue
            Get
                Return CDbl(mValue) / 100
            End Get
            Set(ByVal Value As Double)
                mValue = CShort(Fix(Value * 100))
            End Set
        End Property

        Public Property DisplayFormat(Optional ByVal Seperator As String = ",") As String Implements I_Frequency.DisplayFormat
            Set(ByVal Value As String)
                Dim i As Integer
                i = InStr(Value, Seperator)
                If i > 0 Then
                    mValue = CShort(Left(Value, i - 1)) * CShort(100)
                    mValue = mValue + CShort(Right(Value, Len(Value) - i))
                End If
            End Set
            Get
                DisplayFormat = CStr(mValue \ 100) + Seperator + CStr(mValue Mod 100)
            End Get
        End Property

        'Overloaded
        Public Sub New()

        End Sub
        'Overloaded
        Public Sub New(ByVal Value As System.Int16)
            mValue = Value
        End Sub
        'Overloaded
        Public Sub New(ByVal DoubleValue As Double)
            DoubleValue = DoubleValue
        End Sub
        'Overloaded
        Public Sub New(ByVal RDSFormat As Byte, Optional ByVal LF_MF As Boolean = False)
            Me.RDSFormat(LF_MF) = RDSFormat
        End Sub
        'Overloaded
        Public Sub New(ByVal DisplayFormat As String, Optional ByVal Seperator As String = ",")
            Me.DisplayFormat(Seperator) = DisplayFormat
        End Sub

        Public Overrides Function GetHashCode() As Integer
            GetHashCode = mValue
        End Function

        Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
            Equals = False
            If obj.GetType Is Me.GetType Then
                Equals = (Me.Value = CType(obj, Frequency).Value)
            Else
                Debug.Assert(False)
            End If
        End Function

    End Class

    'This Interface must be implemented by your FMRadioHAL class for Events
    <InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch)> _
    Public Interface I_FMRadioHAL_Events
        Sub RDSRAWMessageavailable(ByRef RDSRAWMessage As stRDSRAWMessage) 'This Event is fired if the radio has received a new RDS Message and sends it to the PC
        Sub AFNewFreq(ByVal NewFreq As System.Int16) ' It is a good idea to implement a function which selects another station if the field strength of the current station falls under a certain level (this should be done very quickly and so it should be done by the µC), The new tuned Frequency is passed
        Sub FieldStrength(ByVal Level As System.Int16) 'new value of current fieldstrength is available (Level ranges from minimum=0 to maximum=32767)
        Sub Stereo(ByVal StereoPilotDetected As System.Boolean) 'is fired if the Reception changes from Stereo to Mono and back
        Sub AutoTuneFinished(ByVal Freq As System.Boolean) 'is fired if the AutoTune is finished

        Sub AnyEvent(ByRef Anything As Object) 'this Event is for expandibility (if you need a special Event for your frontend which your radio supports!)

    End Interface


    Public Enum enDirections
        Down = -1
        Halt = 0
        UP = 1
    End Enum

    Public Enum enBand
        Illegal = -1
        NotDefined = 0
        FM = 1
        MW = 2
        LW = 3
        SW = 4
        '... ?
    End Enum

    'every Band has such a structure
    Public Structure stBandCap
        Public MinFreq As System.Int16 '20000 (=200Mhz,20000KHz) the highest Frequency the tuner ;) is able to
        Public MaxFreq As System.Int16 '1000 (=10MHZ,1000KHz) the lowest Frequency the tuner ;) is able to
        Public TuneStep As System.Int16 '5 (87,50MHz;87,55MHz;87,60MHz, ....
    End Structure

    'This Structure is passed back and shows the capabilities of the tuner
    Public Structure stSupFunc
        Public AF As System.Boolean 'AF Tuning possible - Property AF_Level could be set and Event AF_NewFreq is fired
        Public Freq As System.Boolean 'direct setting of Frequency works through Property Freq works
        Public FreqUpDown As System.Boolean 'it is possible to step frequncy with Sub FreqUP/Down (this is useful if you hack just the buttons of your Car HU!))
        Public Vol As System.Boolean
        Public VolUpDown As System.Boolean
        Public FM As stBandCap 'Tuner's FM capabiltiy here
        Public MW As stBandCap
        Public LW As stBandCap
        Public SW As stBandCap
        Public Stereo As System.Boolean 'Tuner does "Stereo" (Event Stereo fired if switched, Mono can be forced with Property Mono)
        Public Mute As System.Boolean
        Public Autotune As System.Boolean 'Autotune (Sub Autotune, Event Autotune_finished is fired) is possible
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
        Public Structure stRDSRAWMessage
        <FieldOffset(0)> Public Block0 As System.Int16
        <FieldOffset(2)> Public Block1 As System.Int16
        <FieldOffset(4)> Public Block2 As System.Int16
        <FieldOffset(6)> Public Block3 As System.Int16
        Public TunedFreq As Frequency
        Public Anything As Object

    End Structure

    'Public Structure stRDSRAWMessage
    '    Public Block0 As System.Int16
    '    Public Block1 As System.Int16
    '    Public Block2 As System.Int16
    '    Public Block3 As System.Int16
    '    Public TunedFreq As Frequency
    '    Public Anything As Object
    'End Structure

    Public Structure stError
        Public strError As System.String
        '... ?
    End Structure


End Namespace