Option Strict On
Imports System

Imports System.Windows.Forms
Imports System.Text
Imports System.Threading
'Imports HQCTS.RDS
Imports System.Diagnostics


'using HQCTS.USBHIDIO;
Namespace HQCT

    Friend Class ReportConsumer
        Private mCom As Communication
        Private mDevice As Device
        Private mSignalProcessorStatus As SignalProcessorStatus
        Private mrdsStatus As RDS.RDSStatus
        Private mTunerSettings As TunerSettings

        Private shouldStop As Boolean '


        Private mreportIndex As Integer = 0

        Public ReadOnly Property ReportIndex() As Integer
            Get
                Return mreportIndex
            End Get
        End Property

        'Public Sub New(ByRef com As Communication, ByRef device As Device, ByRef signalProcessorStatus As SignalProcessorStatus, ByRef rdsStatus As Status, ByRef tunerSettings As TunerSettings)
        Public Sub New(ByRef lcom As Communication, ByRef ldevice As Device, ByRef lsignalProcessorStatus As SignalProcessorStatus, ByRef lrdsStatus As RDS.RDSStatus, ByRef lTunerSettings As TunerSettings)
            Me.mCom = lcom
            Me.mDevice = ldevice
            Me.mSignalProcessorStatus = lsignalProcessorStatus
            Me.mrdsStatus = lrdsStatus
            Me.mTunerSettings = lTunerSettings
        End Sub 'New


        Public Sub ConsumeReport()
            Dim i As Integer

            While Not shouldStop
                Dim buffer As Byte() = mCom.ReadInputReport()
                If Not (buffer Is Nothing) Then

                    mreportIndex += 1

                    If buffer(0) = &H54 And buffer(31) = &H54 Then
                        mDevice.Status = enHQCT_STATUS.HQCT_STATUS_INIT
                    End If

                    Select Case mDevice.Status
                        Case mDevice.Status.HQCT_STATUS_INIT
                            If buffer(0) = &H54 And buffer(31) = &H54 Then
                                mDevice.Status = mDevice.Status.HQCT_STATUS_EEPROM1
                            End If

                        Case mDevice.Status.HQCT_STATUS_EEPROM1
                            ' Gather the DAAs and alignment frequencies  
                            For i = 0 To (Math.Min(7, CInt(buffer(&HA)))) - 1
                                mDevice.AlignFreq(i) = 87500 + buffer((&HB + 2 * i)) * 100
                                mDevice.DAAs(i) = buffer((&HC + 2 * i))
                            Next i

                            Dim offset As Integer
                            For offset = 0 To 30
                                mTunerSettings.Settings(offset) = buffer(offset)
                            Next offset

                            mDevice.Status = mDevice.Status.HQCT_STATUS_EEPROM2

                        Case mDevice.Status.HQCT_STATUS_EEPROM2
                            Dim offset As Integer
                            For offset = 0 To 30
                                mTunerSettings.Settings((32 + offset)) = buffer(offset)
                            Next offset

                            ' Subtract 31 from addresses. 
                            mDevice.Status = mDevice.Status.HQCT_STATUS_EEPROM3

                        Case mDevice.Status.HQCT_STATUS_EEPROM3
                            mDevice.FmTrailer(2) = buffer((&H43 - &H3E)) ' Subtract 62 from addresses 
                            mDevice.FmTrailer(3) = CByte(&H80 Or (buffer((&H4A - &H3E)) And &H7F))
                            mDevice.FmTrailer(4) = CByte(((buffer((&H4E - &H3E)) And &HF) << 4) Or (buffer((&H51 - &H3E)) And &HF))
                            mDevice.AmTrailer(2) = buffer((&H48 - &H3E))

                            Dim offset As Integer
                            For offset = 0 To 30
                                mTunerSettings.Settings((64 + offset)) = buffer(offset)
                            Next offset

                            mDevice.Status = mDevice.Status.HQCT_STATUS_NORMAL

                            Console.Write("HQCTS: AlignFr = {")
                            For i = 0 To mDevice.AlignFreq.Length - 1
                                Console.Write(String.Format("{0:d}, ", mDevice.AlignFreq(i)))
                            Next i
                            Console.Write("} " + ControlChars.Lf + "HQCTS: DAAs = {")
                            For i = 0 To mDevice.DAAs.Length - 1
                                Console.Write(String.Format("{0:d}, ", mDevice.DAAs(i)))
                            Next i
                            Console.WriteLine("}")

                        Case mDevice.Status.HQCT_STATUS_SEEKING_UP2
                            i = mDevice.Freq
                            If buffer(2) >= mDevice.MinLevel Or (mDevice.Band = enHQCT_BAND.HQCT_BAND_FM And i <= mDevice.Param And i > mDevice.Param - 100) Or (mDevice.Band = enHQCT_BAND.HQCT_BAND_AM And i <= mDevice.Param And i > mDevice.Param - 10) Then
                                mDevice.Status = enHQCT_STATUS.HQCT_STATUS_NORMAL
                            Else
                                mDevice.Status = enHQCT_STATUS.HQCT_STATUS_SEEKING_UP1
                                If mDevice.Band = enHQCT_BAND.HQCT_BAND_FM Then
                                    i += 100
                                Else
                                    i += 10
                                End If
                                If mDevice.Band = enHQCT_BAND.HQCT_BAND_FM And i > mDevice.AlignFreq(6) Then
                                    mCom.rawTuneFrequency(mDevice.AlignFreq(0))
                                Else
                                    If mDevice.Band = enHQCT_BAND.HQCT_BAND_AM And i > 1500 Then
                                        mCom.rawTuneFrequency(500)
                                    Else
                                        mCom.rawTuneFrequency(i)
                                    End If
                                End If
                            End If
                        Case mDevice.Status.HQCT_STATUS_SEEKING_DN2
                            i = mDevice.Freq
                            If buffer(2) >= mDevice.MinLevel Or (mDevice.Band = enHQCT_BAND.HQCT_BAND_FM And i >= mDevice.Param And i < mDevice.Param + 100) Or (mDevice.Band = enHQCT_BAND.HQCT_BAND_AM And i >= mDevice.Param And i < mDevice.Param + 10) Then
                                mDevice.Status = enHQCT_STATUS.HQCT_STATUS_NORMAL
                            Else
                                mDevice.Status = enHQCT_STATUS.HQCT_STATUS_SEEKING_DN1
                                If mDevice.Band = enHQCT_BAND.HQCT_BAND_FM Then
                                    i -= 100
                                Else
                                    i -= 10
                                End If
                                If mDevice.Band = enHQCT_BAND.HQCT_BAND_FM And i < mDevice.AlignFreq(0) Then
                                    mCom.rawTuneFrequency(mDevice.AlignFreq(6))
                                Else
                                    If mDevice.Band = enHQCT_BAND.HQCT_BAND_AM And i < 500 Then
                                        mCom.rawTuneFrequency(1500)
                                    Else
                                        mCom.rawTuneFrequency(i)
                                    End If
                                End If
                            End If
                        Case enHQCT_STATUS.HQCT_STATUS_NORMAL
                            If buffer(31) <> &H54 And buffer(31) <> &H74 Then
                                mSignalProcessorStatus.decode(buffer)

                                'StringBuilder sb = new StringBuilder();
                                'for (int index = 0; index < buffer.Length; index++)
                                '{
                                '    sb.Append(string.Format("{0:x2}", buffer[index]));
                                '}
                                'sb.Append(" ");
                                'sb.Append(signalProcessorStatus.RdsDataAvailable);
                                'Debug.WriteLine(sb.ToString());
                                ' For some reason, this bit should be ignored to avoid skipping vaild RDS data 
                                'If (mSignalProcessorStatus.RdsDataAvailable) Then
                                mrdsStatus.decode(buffer)


                                'End If
                                '}
                                If mSignalProcessorStatus.AfUpdateSample Then
                                    Debug.WriteLine("AF Update sample recevied!")
                                    mCom.rawTuneFrequency(mDevice.Param)
                                End If
                            End If
                    End Select
                Else
                    Thread.Sleep(10)
                End If
            End While
        End Sub 'ConsumeReport


        Public Sub RequestStop()
            shouldStop = True
        End Sub 'RequestStop
    End Class 'ReportConsumer
End Namespace 'HQCT
