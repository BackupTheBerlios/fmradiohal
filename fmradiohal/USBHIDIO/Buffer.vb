Option Strict On
Option Explicit On 

Public Class BufferHandler
    Public PtrIn As Integer = 0
    Public PtrOut As Integer = 0
    Public Buffer() As Byte
    Private mSize As Integer
    Private bakPos As Integer

    Public Sub New(ByVal SizeOfReports As Integer, ByVal NumOfReports As Integer)
        Buffer = New Byte((SizeOfReports * NumOfReports) - 1) {}
        mSize = SizeOfReports
    End Sub

    Public Sub SetNextInPos()
        SyncLock Me
            PtrIn = PtrIn + mSize
            If PtrIn >= Buffer.GetLength(0) Then
                PtrIn = 0
            End If
        End SyncLock
    End Sub
    Public ReadOnly Property SizeOfReports() As Integer
        Get
            Return mSize
        End Get
    End Property
    Public ReadOnly Property GetPOS() As Integer
        Get
            Return PtrIn \ mSize
        End Get
    End Property

    Public ReadOnly Property GetLastPOSout() As Integer
        Get
            Return bakPos
        End Get
    End Property
    'Public ReadOnly Property NumOfReports() As Integer
    '    Get
    '        NumOfReports = Buffer.GetLength(0) \ mSize
    '    End Get
    'End Property

    Public Function Remove() As Byte()
        SyncLock Me
            If PtrIn = PtrOut Then
                REM Nothing in Buffer
            Else
                ReDim Remove(mSize - 1)
                Remove.Copy(Buffer, PtrOut, Remove, 0, mSize)
                bakPos = PtrOut \ mSize
                PtrOut = PtrOut + mSize
                If PtrOut >= Buffer.GetLength(0) Then
                    PtrOut = 0
                End If
            End If
        End SyncLock
    End Function

End Class
