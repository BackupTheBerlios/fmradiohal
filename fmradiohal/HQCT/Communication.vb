Option Strict On

Imports System

Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Diagnostics
Imports System.Threading

Namespace HQCT
    Friend Enum enHQCT_CMD
        HQCT_TUNE = 0
        HQCT_SEEK_UP = 1
        HQCT_SEEK_DOWN = 2
        HQCT_READ = 3
        HQCT_SET_BAND = 4
        HQCT_GET_FREQUENCY = 5
        HQCT_SCAN_UP = 6
        HQCT_SCAN_DOWN = 7
        HQCT_SET_MIN_LEVEL = 8
        HQCT_CANCEL = 9
        HQCT_SET_TEF = 10
        HQCT_EEPROM = 11
        HQCT_AF_UPDATE = 12
    End Enum

    Public Class Communication
        Public Const HQCT_TUNE As Integer = 0
        Public Const HQCT_SEEK_UP As Integer = 1
        Public Const HQCT_SEEK_DOWN As Integer = 2
        Public Const HQCT_READ As Integer = 3
        Public Const HQCT_SET_BAND As Integer = 4
        Public Const HQCT_GET_FREQUENCY As Integer = 5
        Public Const HQCT_SCAN_UP As Integer = 6
        Public Const HQCT_SCAN_DOWN As Integer = 7
        Public Const HQCT_SET_MIN_LEVEL As Integer = 8
        Public Const HQCT_CANCEL As Integer = 9
        Public Const HQCT_SET_TEF As Integer = 10
        Public Const HQCT_EEPROM As Integer = 11
        Public Const HQCT_AF_UPDATE As Integer = 12
        'Private mHandle As IntPtr
        Private mDevice As Device
        Private WithEvents mHIDForm As HIDForm

        Private m_reportlist As New RingBuffer(1000)





        Public ReadOnly Property ReportList() As RingBuffer
            Get
                Return m_reportlist
            End Get
        End Property

        Public Sub New(ByRef ldevice As Device)
            mHIDForm = New HIDForm
            mHIDForm.Show()
            Me.mDevice = ldevice
            mcHID.Connect(mHIDForm.Handle)
        End Sub
        'Friend Sub OnDeviceChange(ByVal m As Message)

        'End Sub
        '*
        '         *  Tunes the radio to a frequency in kHz, even in FM. So 94.3MHz should be
        '         *  entered as 94300.  If you would like to store the frequency, in MHz as a
        '         *  double or float then simply multiply by 1000.
        '         *  
        '         *  Returns zero on success or non-zero on failure (device not ready).
        '         
        Public Sub tune(ByVal frequency As Integer)
            sendData(enHQCT_CMD.HQCT_TUNE, frequency)
        End Sub
        '*
        '         *  Tunes the radio to a frequency in kHz, even in FM. So 94.3MHz should be
        '         *  entered as 94300.  If you would like to store the frequency, in MHz as a
        '         *  double or float then simply multiply by 1000.
        '         *  
        '         *  Returns zero on success or non-zero on failure (device not ready).
        '         
        'Public Sub afUpdate(ByVal frequency As Integer)
        '    sendData(HQCT_AF_UPDATE, frequency)
        'End Sub
        '*
        '         *  Seeks upward while trying to find a decent reception. You can change the
        '         *  tolerance with hqct_set_sensitivity(). Will give up if it has checked
        '         *  every frequency without any reception. It takes around 20seconds to 
        '         *  complete a lap.
        '         *
        '         *  Returns zero on success or non-zero on failure (device not ready).
        '         
        'Public Sub seekUp()
        '    sendData(HQCT_SEEK_UP, Nothing)
        'End Sub
        '*
        '         *  Behaves like the above function but seeks down instead of up.
        '         * 
        '         *  Returns zero on success or non-zero on failure (device not ready).
        '         
        'Public Sub seekDown()
        '    sendData(HQCT_SEEK_DOWN, Nothing)
        'End Sub
        '*
        '         *  Cancels the current seeking operation, if any.
        '         *
        '         *  Returns zero on success or non-zero on failure (device not ready or 
        '         *  not currently seeking).
        '         
        'Public Sub cancelSeek()
        '    sendData(HQCT_CANCEL, Nothing)
        'End Sub
        '*
        '         *  Sets the minimum level acceptable while seeking the radio. The default value
        '         *  is defined in the header of hqct.c as 0x75.  Try using a higher level if you
        '         *  are picking up too much static and choose a lower number if the seek is
        '         *  skipping stations.
        '         *  Note, this is this is the only option that is global to all attached radios.
        '         
        'Public Sub setMinLevel(ByVal level As Byte)
        '    sendData(HQCT_SET_MIN_LEVEL, level)
        'End Sub
        Friend Sub sendData(ByVal type As enHQCT_CMD, ByVal data As Object)
            ' Get EEPROM 
            'If type = HQCT_EEPROM Then
            '    device.Status = Device.HQCT_STATUS_INIT
            '    SendOutputReport(New Byte() {128})
            'End If
            ' TEF commands, status does not matter 
            Select Case type
                Case enHQCT_CMD.HQCT_SET_TEF
                    'Dim buffer As Byte() = New Byte(23) {}
                    'Dim tef As Byte() = DirectCast(data, Byte())
                    'Dim i As Integer = 0
                    'While i < tef.Length AndAlso i + 3 < buffer.Length
                    '    buffer(i + 3) = tef(i)
                    '    i += 1
                    'End While
                    'buffer(0) = 24
                    'buffer(1) = 48
                    'buffer(2) = 64
                    '' DUMB SLEEP
                    'Thread.Sleep(100)
                    'SendOutputReport(buffer)
                    '' TODO, cancel seeking if Band change.
                    'If (buffer(13) And 128) = 128 Then
                    '    Device.Band = Device.HQCT_BAND_AM
                    'Else
                    '    Device.Band = Device.HQCT_BAND_FM
                    'End If
                    Exit Select
                Case enHQCT_CMD.HQCT_SET_MIN_LEVEL
                    'Device.MinLevel = (DirectCast(data, Byte)) And 255
                    Exit Select
            End Select
            ' The radio isn't initialized, fail 
            If mDevice.Status <= enHQCT_STATUS.HQCT_STATUS_EEPROM3 Then
                Return
            End If
            Dim frequency As Integer
            ' (Tuner Commands) Commands that must be past initialization 
            Select Case type
                Case enHQCT_CMD.HQCT_TUNE
                    ' Should tune no matter what 
                    frequency = CType(data, Integer)
                    Thread.Sleep(100)
                    mDevice.Status = enHQCT_STATUS.HQCT_STATUS_NORMAL
                    rawTuneFrequency(frequency)
                    Exit Select
                Case enHQCT_CMD.HQCT_AF_UPDATE
                    'frequency = DirectCast(data, Integer)
                    'Thread.Sleep(100)
                    'Device.Status = Device.HQCT_STATUS_NORMAL
                    'Device.Param = Device.Freq
                    'rawTuneAFFrequency(frequency)
                    Exit Select
                Case enHQCT_CMD.HQCT_SEEK_UP
                    ' Begin upward seek 
                    'If Device.Status = Device.HQCT_STATUS_SEEKING_UP1 OrElse Device.Status = Device.HQCT_STATUS_SEEKING_UP2 Then
                    '    Exit Select
                    'End If
                    'Thread.Sleep(100)
                    'Device.Status = Device.HQCT_STATUS_SEEKING_UP1
                    'Device.Param = Device.Freq
                    'If Device.Band = Device.HQCT_BAND_FM Then
                    '    rawTuneFrequency(Device.Param + 100)
                    'Else
                    '    rawTuneFrequency(Device.Param + 10)
                    'End If
                    Exit Select
                Case enHQCT_CMD.HQCT_SEEK_DOWN
                    ' Begin downward seek 
                    'If Device.Status = Device.HQCT_STATUS_SEEKING_DN1 OrElse Device.Status = Device.HQCT_STATUS_SEEKING_DN2 Then
                    '    Exit Select
                    'End If
                    'Thread.Sleep(100)
                    'Device.Status = Device.HQCT_STATUS_SEEKING_DN1
                    'Device.Param = Device.Freq
                    'If Device.Band = Device.HQCT_BAND_FM Then
                    '    rawTuneFrequency(Device.Param - 100)
                    'Else
                    '    rawTuneFrequency(Device.Param - 10)
                    'End If
                    Exit Select
                    'Case HQCT_CANCEL
                    '    ' Let the last tune finish, but cancel any seeking. 
                    '    Device.Status = Device.HQCT_STATUS_NORMAL
                    Exit Select
            End Select
        End Sub
        ' Tune to the frequency in kHz. 
        Public Sub rawTuneFrequency(ByVal freq As Integer)
            Dim data As Byte() = New Byte(10) {}
            setTunerData(freq, data)
            ' send out the first tuning packet 
            Thread.Sleep(5)
            SendOutputReport(data)
        End Sub
        ' Tune to the AF frequency in kHz. 
        Private Sub rawTuneAFFrequency(ByVal freq As Integer)
            'Dim data As Byte() = New Byte(10) {}
            'setTunerData(freq, data)
            'data(2) = data(2) Or 128
            '' send out the first tuning packet 
            'Thread.Sleep(5)
            'SendOutputReport(data)
        End Sub
        Private Sub setTunerData(ByVal freq As Integer, ByRef data As Byte())
            Const interm_fr As Integer = 10700
            ' Fif: intermediate Frequency    
            Const FM_ref_fr As Integer = 100
            ' Fref: reference frequency      
            Const FM_VCO As Byte = 2
            ' p: VCO_Div                     
            Const AM_ref_fr As Integer = 1
            Const AM_VCO As Byte = 20
            Dim slope As Integer
            Dim DAA As Integer
            Dim PLL As Integer
            Dim t As Integer

            ' Out of range ???
            If (freq < 87500 OrElse freq > 108000) And (mDevice.Band = enHQCT_BAND.HQCT_BAND_FM) Then
                freq = 87500
            End If

            mDevice.Freq = freq
            If mDevice.Band = enHQCT_BAND.HQCT_BAND_FM Then
                ' Find upper alignment frequency 
                t = 0
                While (t < mDevice.AlignFreq.Length - 1) And (mDevice.AlignFreq(t) <= freq)
                    t = t + 1
                End While
                ' Find the slope 
                slope = CInt(Fix(1000000.0 * ((CInt(mDevice.DAAs(t)) - CInt(mDevice.DAAs(t - 1))) / (mDevice.AlignFreq(t) - mDevice.AlignFreq(t - 1)))))
                'Debug.WriteLine(string.Format("HQCTS: Tuning to: {0:d}.{1:d} MHz", freq / 1000, (freq % 1000) / 10));
                DAA = CInt(Fix(((CLng(mDevice.DAAs(t)) * CLng(1000000)) + (CLng(freq) * CLng(slope)) - (CLng(mDevice.AlignFreq(t)) * CLng(slope))) / CLng(1000000)))
                PLL = CInt(Fix(((freq + interm_fr) * FM_VCO)) / FM_ref_fr)
                For i As Integer = 0 To mDevice.FmTrailer.Length - 1
                    data(i + 5) = mDevice.FmTrailer(i)
                Next
            Else
                'Debug.WriteLine(string.Format("HQCTS: Tuning to: {0:d} kHz", freq));
                DAA = 0
                PLL = CInt(((((freq / 10) + interm_fr) * AM_VCO)) / AM_ref_fr)
                For i As Integer = 0 To mDevice.AmTrailer.Length - 1
                    data(i + 5) = mDevice.AmTrailer(i)
                Next
            End If
            ' Create string 
            data(0) = 11
            data(1) = 194
            data(2) = CType(((PLL >> 8) And 255), Byte)
            data(3) = CType(((PLL) And 255), Byte)
            data(4) = CType(((DAA Or 128) And 255), Byte)
            ' Set bit 7 High 
        End Sub
        Private Sub postSend(ByVal buffer As Byte())
            ' Just sent the first tuning packet, prepare the second. 
            If buffer(0) = 11 AndAlso buffer(1) = 194 Then
                mDevice.TuneBuffer(0) = 6
                mDevice.TuneBuffer(1) = buffer(1)
                mDevice.TuneBuffer(2) = buffer(2)
                mDevice.TuneBuffer(3) = buffer(3)
                mDevice.TuneBuffer(4) = CType((buffer(4) And 127), Byte)
                ' Set bit 7 low 
                'Debug.WriteLine("HQCTS: Finished first packet.");
                'TimerCallback timerDelegate = new TimerCallback(tuneTimeout);
                'System.Threading.Timer stateTimer = new System.Threading.Timer(timerDelegate);
                'stateTimer.Change(50, System.Threading.Timeout.Infinite);
                Thread.Sleep(50)
                SendOutputReport(mDevice.TuneBuffer)
                mDevice.TuneBuffer(0) = 0
            ElseIf (buffer(0) = 6) AndAlso (buffer(1) = 194) Then
                ' Just sent the second tuning packet, see if we are seeking. 
                'Debug.WriteLine("HQCT: Finished second packet.");
                ' We should check the quality of the next report 
                If mDevice.Status = enHQCT_STATUS.HQCT_STATUS_SEEKING_UP1 Then
                    mDevice.Status = enHQCT_STATUS.HQCT_STATUS_SEEKING_UP2
                End If
                If mDevice.Status = enHQCT_STATUS.HQCT_STATUS_SEEKING_DN1 Then
                    mDevice.Status = enHQCT_STATUS.HQCT_STATUS_SEEKING_DN2
                End If
            End If
        End Sub
        Private Sub tuneTimeout(ByVal state As Object)
            SendOutputReport(mDevice.TuneBuffer)
            mDevice.TuneBuffer(0) = 0
        End Sub
        Private Sub SendOutputReport(ByVal reportData As Byte())
            Dim handle As IntPtr = mcHID.GetHandle(mcHID.VENDOR_ID, mcHID.PRODUCT_ID)
            If Not (handle.Equals(IntPtr.Zero)) Then
                Dim outputBuffer As Byte() = New Byte(mcHID.OUTPUT_BUFFER_SIZE) {}
                Dim sb As New StringBuilder
                Dim i As Integer = 0
                While i < outputBuffer.Length - 1 AndAlso i < reportData.Length
                    outputBuffer(i + 1) = reportData(i)
                    sb.Append(String.Format("{0:x2} ", reportData(i)))
                    i += 1
                End While
                Debug.WriteLine(sb.ToString())
                ' Fix the byte array.
                ' Make the call here, passing in the array.
                mcHID.Write(handle, outputBuffer)

            End If
            postSend(reportData)
        End Sub
        Public Function ReadInputReport() As Byte()
            SyncLock m_reportlist
                If m_reportlist.Counter > 0 Then
                    Return m_reportlist.Remove
                End If
            End SyncLock
        End Function

        Protected Overrides Sub Finalize()

            MyBase.Finalize()
        End Sub

        Private Sub mHIDForm_WM_HID_EVENT(ByRef m As System.Windows.Forms.Message) Handles mHIDForm.WM_HID_EVENT
            Dim wParam As Integer = m.WParam.ToInt32
            Dim lParam As IntPtr = m.LParam
            Select Case wParam
                Case mcHID.NOTIFY_PLUGGED
                    If mcHID.GetVendorID(lParam) = mcHID.VENDOR_ID AndAlso mcHID.GetProductID(lParam) = mcHID.PRODUCT_ID Then
                    End If
                    Return
                Case mcHID.NOTIFY_UNPLUGGED
                    If mcHID.GetVendorID(lParam) = mcHID.VENDOR_ID AndAlso mcHID.GetProductID(lParam) = mcHID.PRODUCT_ID Then
                    End If
                    Return
                Case mcHID.NOTIFY_CHANGED
                    Dim Handle As IntPtr = mcHID.GetHandle(mcHID.VENDOR_ID, mcHID.PRODUCT_ID)
                    If Not (Handle.Equals(IntPtr.Zero)) Then
                        Debug.WriteLine("Setting read notification!")
                        mcHID.SetReadNotify(Handle, True)
                        mDevice.Status = enHQCT_STATUS.HQCT_STATUS_INIT
                        SendOutputReport(New Byte() {128})
                    End If
                    Return
                Case mcHID.NOTIFY_READ
                    If mcHID.GetVendorID(lParam) = mcHID.VENDOR_ID AndAlso mcHID.GetProductID(lParam) = mcHID.PRODUCT_ID Then
                        Dim inputBuffer As Byte() = New Byte(mcHID.INPUT_BUFFER_SIZE) {}
                        Dim successfulRead As Boolean = False
                        ' Fix the byte array.
                        ' Make the call here, passing in the array.

                        successfulRead = mcHID.Read(lParam, inputBuffer)

                        If successfulRead Then
                            SyncLock m_reportlist
                                m_reportlist.Add(inputBuffer)
                            End SyncLock
                        Else
                            Debug.Assert(False)
                        End If
                    End If
                    Return
            End Select
        End Sub
    End Class
End Namespace
