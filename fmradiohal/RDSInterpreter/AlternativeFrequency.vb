Option Strict On
Imports System
Imports System.Text


Namespace RDSInterpreter


    Public Class AlternativeFrequency

        Private mfrequency As FMRadio.FMRadioHAL.Frequency
        Private mlinkfreq As FMRadio.FMRadioHAL.Frequency
        Private mregionalVariant As Boolean = False


        Public Sub New(ByVal Frequency As FMRadio.FMRadioHAL.Frequency)
            'MyBase..New(Frequency.Value)
            Me.Frequency = Frequency
        End Sub 'New

        Public Property LinkedFrequency() As FMRadio.FMRadioHAL.Frequency
            Get
                Return mlinkfreq
            End Get
            Set(ByVal Value As FMRadio.FMRadioHAL.Frequency)
                mlinkfreq = Value
            End Set
        End Property


        Public Property Frequency() As FMRadio.FMRadioHAL.Frequency
            Get
                Return mfrequency
            End Get
            Set(ByVal Value As FMRadio.FMRadioHAL.Frequency)
                mfrequency = Value
            End Set
        End Property

        'Public ReadOnly Property DisplayableFrequency() As Double
        '    Get
        '        Return 87.5 + frequency * 0.1
        '    End Get
        'End Property

        'Public ReadOnly Property TunableFrequency() As Integer
        '    Get
        '        Return CInt((87.5 + frequency * 0.1) * 1000)
        '    End Get
        'End Property

        Public Property RegionalVariant() As Boolean
            Get
                Return mregionalVariant
            End Get
            Set(ByVal Value As Boolean)
                mregionalVariant = Value
            End Set
        End Property

        Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
            Dim result As Boolean = False

            If TypeOf obj Is AlternativeFrequency Then
                result = (CType(obj, AlternativeFrequency).Frequency.Value = Frequency.Value) And (CType(obj, AlternativeFrequency).mregionalVariant = mregionalVariant)
            Else

            End If

            Return result
        End Function 'Equals


        Public Function ComparedTo(ByVal af As AlternativeFrequency) As Integer
            Dim result As Integer = 0

            If Me.Frequency.Value < af.Frequency.Value Then
                result = -1
            Else
                If Me.Frequency.Value > af.Frequency.Value Then
                    result = 1
                End If
            End If
            Return result
        End Function 'ComparedTo
    End Class 'AlternativeFrequency 
End Namespace 'RDSInterpreter