Option Strict On

Imports System

Imports System.Text

Imports System.Windows.Forms
Imports System.Diagnostics
Imports System.Threading
Imports System.Diagnostics.Trace


Namespace HQCT

    Friend Class Communication
        Private mDevice As Device
        'Dim mHandles As HID.frmHID.stHandles
        Private WithEvents mfrmHID As HID.frmHID

        'Friend m_reportlist As New RingBuffer(1000)

        Public Const VENDOR_ID As Integer = &H4D8
        Public Const PRODUCT_ID As Integer = &HA
        Public Const INPUT_BUFFER_SIZE As Integer = 32
        Public Const OUTPUT_BUFFER_SIZE As Integer = 32



        'Friend mMCHIDThread As System.Threading.Thread
        'Friend mReportConsumerThread As System.Threading.Thread
        'Friend WithEvents mReportConsumer As ReportConsumer

        Public Delegate Sub RDS_Delegate(ByRef RDSMessage As FMRadioHAL.stRDSRAWMessage)
        Public Delegate Sub FS_Delegate(ByVal Value As Short)

        Public Event RDS_Message As RDS_Delegate
        Public Event FS_Message As FS_Delegate

        Public SomethingInBuffer As New AutoResetEvent(False)

        Public SomethingToSend As New AutoResetEvent(False)

        Public ToSend As Boolean

        Dim IncMessageCounter As System.Int32

        'Public ReadOnly Property ReportList() As RingBuffer
        '    Get
        '        Return m_reportlist
        '    End Get
        'End Property

        Public Sub New(ByRef ldevice As Device) ', ByRef ReportConsumerThread As System.Threading.Thread)
            'Me.mReportConsumerThread = ReportConsumerThread
            Me.mDevice = ldevice

            'Me.mDevice.Status = enHQCT_STATUS.HQCT_STATUS_INIT
            REM wait fo mdevice.init!
        End Sub





        Public Sub Disconnect()
            mfrmHID.Close()
        End Sub

        Public Function Connect() As Boolean
            Dim StartTime As DateTime = DateTime.Now
            Dim outputBuffer As Byte() = New Byte(32) {}
            Dim ResultStr As String
            Connect = False
            mfrmHID = New HID.frmHID
            'mfrmHID.Show()

            'mHandles = mfrmHID.GetHandle(&H4D8, &HA)
            mfrmHID.StartRead(CBool(AssemblySettings.GetItem("Trace")), CInt(AssemblySettings.GetItem("HIDBufferSize")), CInt(AssemblySettings.GetItem("HIDKernelBuffer")), CInt(AssemblySettings.GetItem("ReadTimeOut")), CInt(AssemblySettings.GetItem("WriteTimeOut")))
            Application.DoEvents()
            If CInt(AssemblySettings.GetItem("gs-ireland_wait")) > 0 Then
                [Thread].CurrentThread.Sleep(CInt(AssemblySettings.GetItem("gs-ireland_wait")))
            End If
            Application.DoEvents()
            outputBuffer(1) = 128
            ResultStr = mfrmHID.WriteHID(outputBuffer)
            If ResultStr = "" Then


                Do

                    mDevice.DEVICE_STATUS_NORMAL_FLAG.WaitOne(100, False)

                    If mDevice.Status = enHQCT_STATUS.HQCT_STATUS_NORMAL Then
                        Exit Do
                    End If

                    Application.DoEvents()
                    'Thread.Sleep(50)
                Loop While DateTime.Now.Subtract(StartTime).TotalSeconds < CInt(AssemblySettings.GetItem("StartTimeOut"))
                If mDevice.Status <> enHQCT_STATUS.HQCT_STATUS_NORMAL Then
                    Throw (New ApplicationException("Trying to establish a connection - HQCT time out"))
                Else
                    Connect = True
                End If
            Else
                Throw (New ApplicationException(ResultStr))
            End If
        End Function


        'Friend Sub OnDeviceChange(ByVal m As Message)

        'End Sub
        '*
        '         *  Tunes the radio to a frequency in kHz, even in FM. So 94.3MHz should be
        '         *  entered as 94300.  If you would like to store the frequency, in MHz as a
        '         *  double or float then simply multiply by 1000.
        '         *  
        '         *  Returns zero on success or non-zero on failure (device not ready).
        '         
        Public Sub Tune(ByVal Freq As FMRadioHAL.Frequency)
            'sendData(enHQCT_CMD.HQCT_TUNE, frequency)
            Dim stM As stTunerSettingMessage
            Dim ResultStr As String
            stM = TunerSettingMessage(Freq, FMRadioHAL.enBand.FM)

            ResultStr = mfrmHID.WriteHID(stM.Bytes)
            If ResultStr <> "" Then
                Throw (New ApplicationException(ResultStr))
            End If

        End Sub

        Private Function TunerSettingMessage(ByVal Freq As FMRadioHAL.Frequency, ByVal Band As FMRadioHAL.enBand) As stTunerSettingMessage
            'Dim TunerSettingMessage As stTunerSettingMessage
            Const interm_fr As Integer = 10700
            ' Fif: intermediate Frequency    
            'Const FM_ref_fr As Integer = 100
            'Dim REF_FR As Short
            ' Fref: reference frequency      
            'Const FM_VCO As Byte = 2
            ' p: VCO_Div                     
            'Const AM_ref_fr As Integer = 1
            'Const AM_VCO As Byte = 20
            'Dim VCO As Byte
            Dim slope As Integer
            'Dim DAA As Byte
            'Dim PLL As Short
            Dim t As Integer

            ' Out of range ???
            'If (freq < 87500 OrElse freq > 108000) And (mDevice.Band = enHQCT_BAND.HQCT_BAND_FM) Then
            'Freq = 87500
            'End If


            'mDevice.Freq = Freq

            Select Case Band
                Case FMRadioHAL.enBand.FM
                    ' Find upper alignment frequency

                    Dim AlignFreq(mDevice.EEPROM(10) - 1) As Integer
                    Dim DAAs(mDevice.EEPROM(10) - 1) As Integer
                    Dim i As Byte
                    For i = 0 To (mDevice.EEPROM(10)) - CByte(1)
                        AlignFreq(i) = 87500 + mDevice.EEPROM((&HB + 2 * i)) * 100
                        DAAs(i) = mDevice.EEPROM((&HC + 2 * i))
                    Next i

                    t = 0
                    While (t < AlignFreq.Length - 1) And (AlignFreq(t) <= Freq.Value * 10)
                        t = t + 1
                    End While
                    ' Find the slope

                    'For i As Integer = 0 To mDevice.FmTrailer.Length - 1
                    '    data(i + 5) = mDevice.FmTrailer(i)
                    'Next

                    'TunerSettingMessage.Byte3 = mDevice.FmTrailer(0)'136 &H88
                    TunerSettingMessage.AMFM = stTunerSettingMessage.enAMFM.FM
                    TunerSettingMessage.BND = stTunerSettingMessage.enBND.FM_STD
                    TunerSettingMessage.IFPR = stTunerSettingMessage.enIFPR.PRESCALER_10
                    TunerSettingMessage.REF = stTunerSettingMessage.enREF.f100khz
                    TunerSettingMessage.IFMT = stTunerSettingMessage.enIFMT.IFCounter_2ms

                    'TunerSettingMessage.Byte4 = mDevice.FmTrailer(1) '160 &HA0
                    TunerSettingMessage.BW = stTunerSettingMessage.enBW.Dynamic
                    TunerSettingMessage.FLAG = stTunerSettingMessage.enFLAG.Flag_High
                    TunerSettingMessage.LODX = stTunerSettingMessage.enLODX.Distance_Mode
                    TunerSettingMessage.AMSM_FMBW = stTunerSettingMessage.enAMSM_FMBW.BW_Sel_MUTE_OFF
                    TunerSettingMessage.AGC = stTunerSettingMessage.enAGC.AGC275mV_12mV
                    TunerSettingMessage.KAGC = True

                    TunerSettingMessage.Byte5 = mDevice.EEPROM(64 + 5)

                    'TunerSettingMessage.Byte6 = mDevice.FmTrailer(3)
                    TunerSettingMessage.CF = mDevice.EEPROM(64 + 12)
                    TunerSettingMessage.TE = True
                    'mDevice.FmTrailer(3) = CByte(&H80 Or (Buffer(&H4A - &H3E) And &H7F)) '12
                    'mDevice.FmTrailer(4) = CByte(((Buffer((&H4E - &H3E)) And &HF) << 4) Or (Buffer((&H51 - &H3E)) And &HF)) '16  19 

                    'Byte7
                    TunerSettingMessage.FOF = mDevice.EEPROM(64 + 16)
                    TunerSettingMessage.FGN = mDevice.EEPROM(64 + 19)

                    slope = CInt(Fix(1000000.0 * ((CInt(DAAs(t)) - CInt(DAAs(t - 1))) / (AlignFreq(t) - AlignFreq(t - 1)))))
                    'Debug.WriteLine(string.Format("HQCTS: Tuning to: {0:d}.{1:d} MHz", freq / 1000, (freq % 1000) / 10));
                    'Byte 0,´1, 2
                    TunerSettingMessage.ANT = CByte(Fix(((CLng(DAAs(t)) * CLng(1000000)) + (CLng(Freq.Value * 10) * CLng(slope)) - (CLng(AlignFreq(t)) * CLng(slope))) / CLng(1000000)))
                    TunerSettingMessage.PLL = CShort(Fix((((Freq.Value * 10) + interm_fr) * VCO(TunerSettingMessage.BND))) / REF_FR(TunerSettingMessage.REF))
                    TunerSettingMessage.MUTE = True
                    'TunerSettingMessage.AF = True

                Case FMRadioHAL.enBand.LW, FMRadioHAL.enBand.MW
                    TunerSettingMessage.ANT = 0
                    TunerSettingMessage.PLL = CShort(((((Freq.Value) + interm_fr) * VCO(TunerSettingMessage.BND))) / REF_FR(TunerSettingMessage.REF))
                    'For i As Integer = 0 To mDevice.AmTrailer.Length - 1
                    '    data(i + 5) = mDevice.AmTrailer(i)
                    'Next
                    'TunerSettingMessage.Byte3 = mDevice.AmTrailer(0) '157 9D
                    TunerSettingMessage.BND = stTunerSettingMessage.enBND.AM_LW_MW_MONO
                    TunerSettingMessage.IFPR = stTunerSettingMessage.enIFPR.PRESCALER_10
                    TunerSettingMessage.REF = stTunerSettingMessage.enREF.f10khz
                    TunerSettingMessage.IFMT = stTunerSettingMessage.enIFMT.IFCounter_2ms

                    'TunerSettingMessage.Byte4 = mDevice.AmTrailer(1) '16 10
                    TunerSettingMessage.BW = stTunerSettingMessage.enBW.Dynamic
                    TunerSettingMessage.FLAG = stTunerSettingMessage.enFLAG.Flag_High
                    TunerSettingMessage.LODX = stTunerSettingMessage.enLODX.Distance_Mode
                    TunerSettingMessage.AMSM_FMBW = stTunerSettingMessage.enAMSM_FMBW.Alig_Mode_MUTE_ON
                    TunerSettingMessage.AGC = stTunerSettingMessage.enAGC.AGC150mV_16mV
                    TunerSettingMessage.KAGC = False

                    'TunerSettingMessage.Byte5 = mDevice.AmTrailer(2) '114 72
                    TunerSettingMessage.LSL = 2
                    TunerSettingMessage.LST = 14
                    TunerSettingMessage.Byte6 = 0
                    TunerSettingMessage.Byte7 = 0
            End Select

            ' Create string 
            'Data(0) = 11
            'Data(1) = 194




            'TunerSettingMessage.PLL = PLL
            'TunerSettingMessage.MUTE = True
            'TunerSettingMessage.ANT = DAA
            'data(2) = CType(((PLL >> 8) And 255), Byte)
            'data(3) = CType(((PLL) And 255), Byte)
            'data(4) = CType(((DAA Or 128) And 255), Byte)
            ' Set bit 7 High 
        End Function

        Public Function VCO(ByVal BND As HQCT.stTunerSettingMessage.enBND) As Byte
            Select Case BND
                Case stTunerSettingMessage.enBND.FM_STD
                    VCO = 2
                Case stTunerSettingMessage.enBND.AM_SW_MONO, stTunerSettingMessage.enBND.AM_SW_STEREO
                    VCO = 10
                Case stTunerSettingMessage.enBND.FM_JAPAN, stTunerSettingMessage.enBND.FM_EAST
                    VCO = 3
                Case stTunerSettingMessage.enBND.AM_LW_MW_MONO, stTunerSettingMessage.enBND.AM_LW_MW_STEREO
                    VCO = 20
                Case stTunerSettingMessage.enBND.FM_WEATHER
                    VCO = 1
            End Select
        End Function
        Public Function REF_FR(ByVal REF As HQCT.stTunerSettingMessage.enREF) As Short
            Select Case REF
                Case stTunerSettingMessage.enREF.f100khz
                    REF_FR = 100
                Case stTunerSettingMessage.enREF.f10khz, stTunerSettingMessage.enREF.f10khz_2, stTunerSettingMessage.enREF.f10khz_3, stTunerSettingMessage.enREF.f10khz_4
                    REF_FR = 10
                Case stTunerSettingMessage.enREF.f20khz
                    REF_FR = 20
                Case stTunerSettingMessage.enREF.f25khz
                    REF_FR = 25
                Case stTunerSettingMessage.enREF.f50khz
                    REF_FR = 50
            End Select
        End Function


        Protected Overrides Sub Finalize()

            MyBase.Finalize()
        End Sub


        Public Sub decodeRDS(ByRef IncMessage As HQCT.stIncMessage)
            Dim ignorePreviousBlock As Boolean
            Static AReceived As Boolean
            Static BReceived As Boolean
            Static CReceived As Boolean
            Static DReceived As Boolean
            Static rdsBlockGroup As FMRadioHAL.stRDSRAWMessage

            If Not ((IncMessage.RDS_STATUS.SYNC) And (Not IncMessage.RDS_STATUS.DOFL) And (Not IncMessage.RDS_STATUS.RSTD)) Then
                ignorePreviousBlock = True
                Return
            End If

            If ignorePreviousBlock Then
                ignorePreviousBlock = False ' Reset
                If IncMessage.RDS_COUNT_PBIN.PBI <> enBI.ib Then
                    Debug.Write("IncMessage.RDS_COUNT_PBIN.PBI <> HQCT.enBI.ib!")
                End If
                'PrevRDSBlockID = enBlockType.ib ' ib
                'PrevRDSBlockError = &H3 ' uncorrectable error
            Else
                'PrevRDSBlockID = CType(((rdsPrevious And &H1C) >> 2), enBlockType)
                'PrevRDSBlockError = rdsPrevious And &H3
                'ReceivedBlocksCounter(PrevRDSBlockID) += 1
            End If


            ' Check for A, B, C, D blocks to form a group



            If (IncMessage.RDS_STATUS.LBI = HQCT.enBI.A) And IncMessage.RDS_STATUS.ELB < HQCT.enEB.UNCORRECTABLE_ERROR Then
                rdsBlockGroup.Block0 = IncMessage.LDAT

                AReceived = True
                BReceived = False
                CReceived = False
                DReceived = False
            Else
                If (IncMessage.RDS_COUNT_PBIN.PBI = HQCT.enBI.A) And IncMessage.RDS_COUNT_PBIN.EPB < HQCT.enEB.UNCORRECTABLE_ERROR Then
                    rdsBlockGroup.Block0 = IncMessage.PDAT
                    AReceived = True
                    BReceived = False
                    CReceived = False
                    DReceived = False
                End If
            End If
            If (IncMessage.RDS_STATUS.LBI = HQCT.enBI.B) And IncMessage.RDS_STATUS.ELB < HQCT.enEB.UNCORRECTABLE_ERROR And AReceived Then
                rdsBlockGroup.Block1 = IncMessage.LDAT

                BReceived = True
                CReceived = False
                DReceived = False
            Else
                If (IncMessage.RDS_COUNT_PBIN.PBI = HQCT.enBI.B) And (IncMessage.RDS_COUNT_PBIN.EPB < HQCT.enEB.UNCORRECTABLE_ERROR) And AReceived Then
                    rdsBlockGroup.Block1 = IncMessage.PDAT
                    BReceived = True
                    CReceived = False
                    DReceived = False
                End If
            End If
            If ((IncMessage.RDS_STATUS.LBI = HQCT.enBI.C) Or (IncMessage.RDS_STATUS.LBI = HQCT.enBI.C2)) And (IncMessage.RDS_STATUS.ELB < HQCT.enEB.UNCORRECTABLE_ERROR) And AReceived Then
                rdsBlockGroup.Block2 = IncMessage.LDAT
                CReceived = True
                DReceived = False
            Else
                If ((IncMessage.RDS_COUNT_PBIN.PBI = HQCT.enBI.C) Or (IncMessage.RDS_COUNT_PBIN.PBI = HQCT.enBI.C2)) And (IncMessage.RDS_COUNT_PBIN.EPB < HQCT.enEB.UNCORRECTABLE_ERROR) And BReceived Then
                    rdsBlockGroup.Block2 = IncMessage.PDAT
                    CReceived = True
                    DReceived = False
                End If
            End If
            If (IncMessage.RDS_STATUS.LBI = HQCT.enBI.D) And (IncMessage.RDS_STATUS.ELB < HQCT.enEB.UNCORRECTABLE_ERROR) And CReceived Then
                rdsBlockGroup.Block3 = IncMessage.LDAT
                DReceived = True
            Else
                If (IncMessage.RDS_COUNT_PBIN.PBI = HQCT.enBI.D) And (IncMessage.RDS_COUNT_PBIN.EPB < HQCT.enEB.UNCORRECTABLE_ERROR) And CReceived Then
                    rdsBlockGroup.Block3 = IncMessage.PDAT
                    DReceived = True
                End If
            End If

            If AReceived And BReceived And CReceived And DReceived Then
                RaiseEvent RDS_Message(rdsBlockGroup)
                'interpreter.decode(rdsBlockGroup, 8)
                AReceived = False
                BReceived = False
                CReceived = False
                DReceived = False
            End If

            ' Try not to miss a trailing A block
            If (IncMessage.RDS_STATUS.LBI = HQCT.enBI.A) And (IncMessage.RDS_STATUS.ELB < HQCT.enEB.UNCORRECTABLE_ERROR) Then
                rdsBlockGroup.Block0 = IncMessage.LDAT
                AReceived = True
                BReceived = False
                CReceived = False
                DReceived = False
            End If

        End Sub 'decode



        Private Sub mfrmHID_NewMessage(ByRef Buffer() As Byte) Handles mfrmHID.NewMessage
            Dim IncMessage As stIncMessage
            Dim Status As FMRadio.HQCT.enHQCT_STATUS
            Dim L As Byte

            'For L = 0 To CByte(UBound(Buffer))
            '    Debug.Write(Hex(Buffer(L)) + ", ")
            'Next
            'Debug.WriteLine(" ")
            If IncMessageCounter >= IncMessageCounter.MaxValue Then
                IncMessageCounter = 0
            Else
                IncMessageCounter = IncMessageCounter + CInt(1)
            End If

            Status = mDevice.Status
            Select Case Status
                Case enHQCT_STATUS.HQCT_STATUS_INIT
                    Status = enHQCT_STATUS.HQCT_STATUS_EEPROM1
                    For L = 1 To 32
                        If Buffer(L) = &H54 Then
                        Else
                            Status = enHQCT_STATUS.HQCT_STATUS_INIT
                            Exit For
                        End If
                    Next L
                Case enHQCT_STATUS.HQCT_STATUS_EEPROM1
                    [Array].Copy(Buffer, 1, mDevice.EEPROM, 0, 32)
                    Status = enHQCT_STATUS.HQCT_STATUS_EEPROM2
                Case enHQCT_STATUS.HQCT_STATUS_EEPROM2
                    [Array].Copy(Buffer, 1, mDevice.EEPROM, 32, 32)
                    Status = enHQCT_STATUS.HQCT_STATUS_EEPROM3
                Case enHQCT_STATUS.HQCT_STATUS_EEPROM3
                    [Array].Copy(Buffer, 1, mDevice.EEPROM, 64, 32)
                    Status = enHQCT_STATUS.HQCT_STATUS_EEPROM_END
                Case enHQCT_STATUS.HQCT_STATUS_EEPROM_END
                    Status = enHQCT_STATUS.HQCT_STATUS_NORMAL
                    For L = 1 To 32
                        If Buffer(L) = &H74 Then
                        Else
                            Status = enHQCT_STATUS.HQCT_STATUS_INIT
                            Exit For
                        End If
                    Next L
                Case enHQCT_STATUS.HQCT_STATUS_NORMAL

                    IncMessage.Bytes = Buffer
                    decodeRDS(IncMessage)

                    'RaiseEvent NewMessage(IncMessage)

                    Dim Result As Short = CShort(IncMessage.LEVEL.LEVEL)
                    If IncMessage.USNWAM.USN = 0 Then
                        Result = Result << 7
                    Else
                        Result = (Result << 7) \ CShort(IncMessage.USNWAM.USN)
                    End If
                    If IncMessage.USNWAM.WAM > 0 Then
                        Result = CShort(Result) \ CShort(IncMessage.USNWAM.WAM)
                    End If
                    mDevice.Level = Result
                    RaiseEvent FS_Message(Result)


            End Select
            mDevice.Status = Status




            'decodeRDS(IncMessage)
            'RaiseEvent NewMessage(IncMessage)


        End Sub


    End Class


End Namespace
