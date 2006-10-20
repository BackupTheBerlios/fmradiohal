Option Strict On

Namespace HQCT

    Public Class RingBuffer
        Private mPtrIn As Integer = 0
        Private mPtrOut As Integer = 0

        Private mRing() As Byte
        Private Function GetPtr(ByVal Ptr As Integer) As Integer
            GetPtr = Ptr * mcHID.INPUT_BUFFER_SIZE
        End Function
        Private Function RingElemSize() As Integer
            RingElemSize = mRing.GetLength(0) \ mcHID.INPUT_BUFFER_SIZE
        End Function
        Private mRingElemNum As Integer

        ReadOnly Property Counter() As Integer
            Get
                Return mRingElemNum
            End Get
        End Property




        Public Sub New(ByVal SizeForReports As Integer)
            'mRing(SizeReports)(3)
            mRing = New Byte((SizeForReports * mcHID.INPUT_BUFFER_SIZE) - 1) {}
        End Sub
        Public Sub Add(ByRef Buffer As Byte())
            If mRingElemNum >= RingElemSize() Then
                Err.Raise(-1, , "Ringbuffer Full!")
            End If
            If Buffer.Length - 1 <> mcHID.INPUT_BUFFER_SIZE Then
                Err.Raise(-1, , "Wrong size to add as element in buffer")
            End If
            [Array].Copy(Buffer, 1, mRing, GetPtr(mPtrIn), mcHID.INPUT_BUFFER_SIZE)
            mPtrIn = mPtrIn + 1
            mRingElemNum = mRingElemNum + 1
            If mPtrIn >= RingElemSize() Then
                mPtrIn = 0
            End If
        End Sub
        Public Function Remove() As Byte()
            If mRingElemNum <= 0 Then
                REM Nothing inside!
            Else
                ReDim Remove(mcHID.INPUT_BUFFER_SIZE - 1)


                [Array].Copy(mRing, GetPtr(mPtrOut), Remove, 0, mcHID.INPUT_BUFFER_SIZE)
                mPtrOut = mPtrOut + 1
                mRingElemNum = mRingElemNum - 1
                If mPtrOut >= RingElemSize() Then
                    mPtrOut = 0
                End If
            End If
        End Function
        'Public Sub TRemove(ByRef Arr As Byte())
        '    SyncLock Me
        '        If mRingElemNum <= 0 Then
        '            REM Nothing inside!
        '        Else
        '            Arr()
        '            '[Array].Copy(mRing, GetPtr(mPtrOut), Remove, 0, 3)
        '            mPtrOut = mPtrOut + 1
        '            mRingElemNum = mRingElemNum - 1
        '            If mPtrOut >= RingElemSize() Then
        '                mPtrOut = 0
        '            End If
        '        End If
        '    End SyncLock
        'End Sub
    End Class

End Namespace
