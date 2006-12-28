Option Strict On
Option Explicit On 
Imports System.Runtime.InteropServices

Public Delegate Sub NewMessage_Delegate(ByRef Buffer() As Byte)

Public Class frmHID
    Inherits System.Windows.Forms.Form
    Public Event NewMessage As NewMessage_Delegate
    Public AbortReadThread As Boolean
    Private mReadHandle As Integer
    Private mWriteHandle As Integer
    Private mBufferHandler As BufferHandler

    Private BufferFullFlag As New System.Threading.AutoResetEvent(False)

    Private mReadConsumerThread As System.Threading.Thread
    Private mReadThread As System.Threading.Thread

    Private GetPOS_BUF As Integer

    Private GetPOS_BUF_Sync As New Object

    Private mTrace As Boolean

    Private mReadTimeOut As Integer
    Private mWriteTimeOut As Integer
    'Project: usbhidio_vbdotnet
    'Version: 2.2
    'Date: 7/13/05
    '
    'Purpose: 
    'Demonstrates USB communications with a generic HID-class device

    'Description:
    'Finds an attached device that matches the vendor and product IDs in the form's 
    'text boxes.
    'Retrieves the device's capabilities.
    'Sends and requests HID reports.

    'Uses RegisterDeviceNotification() and WM_DEVICE_CHANGE messages
    'to detect when a device is attached or removed.
    'RegisterDeviceNotification doesn't work under Windows 98 (not sure why).

    'A list box displays the data sent and received,
    'along with error and status messages.

    'Combo boxes select data to send, and 1-time or timed, periodic transfers.

    'You can change the size of the host's Input report buffer and request to use control
    'transfers only to exchange Input and Output reports.

    'To view additional debugging messages, in the Visual Studio development environment,
    'select the Debug build (Build > Configuration Manager > Active Solution Configuration)
    'and view the Output window (View > Other Windows > Output)

    'The application uses a Delegate and the BeginInvoke and EndInvoke methods to read
    'from the device asynchronously. 

    'If you want to only receive data or only send data, comment out the unwanted code 
    '(In ExchangeInputAndOutputReports or ExchangeFeatureReports, comment out
    'the "Success = " line and the "If Success" block that follows it).

    'This project includes the following modules:
    'frmMain.vb - routines specific to the form.
    'Hid.vb - routines specific to HID communications.
    'DeviceManagement.vb - routines for obtaining a handle to a device from its GUID
    'and receiving device notifications. This routines are not specific to HIDs.
    'Debugging.vb - contains a routine for displaying API error messages.

    'HidDeclarations.vb - Declarations for API functions used by Hid.vb.
    'FileIODeclarations.vb - Declarations for file-related API functions.
    'DeviceManagementDeclarations.vb - Declarations for API functions used by DeviceManagement.vb.
    'DebuggingDeclarations.vb - Declarations for API functions used by Debugging.vb.

    'Companion device firmware for several device CPUs is available from www.Lvr.com/hidpage.htm.
    'You can use any generic HID (not a system mouse or keyboard) that sends and receives reports.

    'New in version 2.2:
    'The application obtains separate handles for device information/Feature reports,
    'Input reports, and Output reports. This enables getting information about
    'mice and keyboards.
    'The application detects if the device is a mouse or keyboard
    'and warns that Windows 2000/XP will not allow exchanging Input or Output reports.
    'The list box's contents are trimmed periodically. 

    'For more information about HIDs and USB, and additional example device firmware to use
    'with this application, visit Lakeview Research at http://www.Lvr.com .

    'Send comments, bug reports, etc. to jan@Lvr.com .

    'This application has been tested under Windows 98SE, Windows 2000, and Windows XP.

    Dim DeviceNotificationHandle As IntPtr
    Dim ExclusiveAccess As Boolean
    Dim HIDHandle As Integer
    Dim HIDUsage As String
    Dim MyDeviceDetected As Boolean

    Dim MyDevicePathName As String
    Dim MyHID As New Hid
    'Dim ReadHandle As Integer
    'Dim WriteHandle As Integer

    'Used only in viewing results of API calls in debug.write statements:
    Dim MyDebugging As New Debugging

    Dim MyDeviceManagement As New DeviceManagement

    'Friend frmMy As frmMain

    'Define a class of delegates that point to the Hid.DeviceReport.Read function.
    'The delegate has the same parameters as Hid.DeviceReport.Read.
    'Used for asynchronous reads from the device.
    Private Delegate Sub ReadInputReportDelegate _
        (ByVal readHandle As Integer, _
        ByVal hidHandle As Integer, _
        ByRef myDeviceDetected As Boolean, _
        ByRef readBuffer() As Byte, _
        ByRef success As Boolean)

    Friend Sub OnDeviceChange(ByVal m As Message)

        'Purpose    : Called when a WM_DEVICECHANGE message has arrived,
        '           : indicating that a device has been attached or removed.

        'Accepts    : m - a message with information about the device

        Debug.WriteLine("WM_DEVICECHANGE")

        Try
            If (m.WParam.ToInt32 = DBT_DEVICEARRIVAL) Then

                'If WParam contains DBT_DEVICEARRIVAL, a device has been attached.
                Debug.WriteLine("A device has been attached.")

                'Find out if it's the device we're communicating with.
                If MyDeviceManagement.DeviceNameMatch(m, MyDevicePathName) Then
                    Debug.WriteLine("My device attached")
                    Trace.WriteLine("USB HID device attached")
                    'lstResults.Items.Add("My device attached.")
                End If

            ElseIf (m.WParam.ToInt32 = DBT_DEVICEREMOVECOMPLETE) Then

                'If WParam contains DBT_DEVICEREMOVAL, a device has been removed.
                Debug.WriteLine("A device has been removed.")

                'Find out if it's the device we're communicating with.
                If MyDeviceManagement.DeviceNameMatch(m, MyDevicePathName) Then
                    Trace.WriteLine("USB HID device removed")
                    Debug.WriteLine("My device removed")
                    'lstResults.Items.Add("My device removed.")

                    'Set MyDeviceDetected False so on the next data-transfer attempt,
                    'FindTheHid() will be called to look for the device 
                    'and get a new handle.
                    Me.MyDeviceDetected = False
                End If
            End If

            ''Call ScrollToBottomOfListBox()

        Catch ex As Exception
            'Call HandleException(Me.Name, ex)
        End Try
    End Sub

    Public Structure stHandles
        Dim ReadHandle As Integer
        Dim WriteHandle As Integer
    End Structure

    Friend Function PrepareForOverlappedTransfer(ByRef hidOverlapped As OVERLAPPED) As Integer

        'Purpose    : Creates an event object for the overlapped structure used with 
        '           : ReadFile.
        '           ; Called before the first call to ReadFile.

        Dim Security As SECURITY_ATTRIBUTES

        Try

            'Values for the SECURITY_ATTRIBUTES structure:
            Security.lpSecurityDescriptor = 0
            Security.bInheritHandle = CInt(True)
            Security.nLength = Len(Security)

            '***
            'API function: CreateEvent

            'Purpose: Creates an event object for the overlapped structure used with ReadFile.

            'Accepts:
            'A security attributes structure.
            'Manual Reset = False (The system automatically resets the state to nonsignaled 
            'after a waiting thread has been released.)
            'Initial state = True (signaled)
            'An event object name (optional)

            'Returns: a handle to the event object
            '***

            PrepareForOverlappedTransfer = CreateEvent(Security, CInt(False), CInt(True), "")

            Debug.WriteLine(MyDebugging.ResultOfAPICall("CreateEvent"))
            Debug.WriteLine("")

            'Set the members of the overlapped structure.
            hidOverlapped.Offset = 0
            hidOverlapped.OffsetHigh = 0
            hidOverlapped.hEvent = PrepareForOverlappedTransfer


        Catch ex As Exception
            'Call HandleException(ModuleName, ex)
        End Try

    End Function

    Public Function WriteHID(ByRef Buffer() As Byte) As String
        Static EventObject As Integer
        Static HIDOverlapped As OVERLAPPED
        Dim NumberOfBytesWrite As Integer
        Dim Result As Integer
        Dim TraceStr As System.Text.StringBuilder

        If EventObject = 0 Then
            EventObject = PrepareForOverlappedTransfer(HIDOverlapped)
        End If


        BufferFullFlag.WaitOne()
        If mTrace Then
            TraceStr = New System.Text.StringBuilder
            TraceStr.Append("out:")
            SyncLock GetPOS_BUF_Sync
                TraceStr.Append(GetPOS_BUF)
            End SyncLock
            TraceStr.Append(";")
            Dim L As Integer
            For L = 0 To CByte(UBound(Buffer))
                TraceStr.Append((Hex(Buffer(L)) + ", "))
            Next
            System.Diagnostics.Trace.WriteLine(TraceStr)
        End If

        Result = WriteFile _
            (mWriteHandle, _
            Buffer(0), _
            UBound(Buffer) + 1, _
            NumberOfBytesWrite, _
            HIDOverlapped)

        If Result <> 0 Then
            If mTrace Then
                Trace.WriteLine("WriteHID failed: " + MyDebugging.ResultOfAPICall("WriteFile"))
            End If
        End If

        Result = WaitForSingleObject(EventObject, mWriteTimeOut)

        'Debug.WriteLine(MyDebugging.ResultOfAPICall("WaitForSingleObject"))
        'Debug.WriteLine("")

        'Find out if ReadFile completed or timeout.
        Select Case Result
            Case WAIT_OBJECT_0

                'ReadFile has completed
                'Debug.WriteLine("")
                'success = True
                'Debug.WriteLine("ReadFile completed successfully.")

            Case WAIT_TIMEOUT
                If mTrace Then
                    Trace.WriteLine("WAIT_TIMEOUT in WriteHID:" + MyDebugging.ResultOfAPICall("WaitForSingleObject"))
                End If
                WriteHID = "USB HID TimeOut while writing!"
                'Cancel the operation on timeout
                'Call CancelTransfer(readHandle, HIDHandle)
                'Debug.WriteLine("Readfile timeout")
                'Debug.WriteLine("")
                'success = False
                MyDeviceDetected = False
            Case Else
                If mTrace Then
                    Trace.WriteLine("Write Error in WriteHID:" + MyDebugging.ResultOfAPICall("WaitForSingleObject"))
                End If
                WriteHID = "USB HID Error while writing!" + MyDebugging.ResultOfAPICall("WaitForSingleObject")
                'Cancel the operation on other error.
                'Call CancelTransfer(readHandle, HIDHandle)
                'Debug.WriteLine("")
                'Debug.WriteLine("Readfile undefined error")
                'success = False
                MyDeviceDetected = False
        End Select


    End Function




    Private Sub ReadThread()
        Dim EventObject As Integer
        Dim HIDOverlapped As OVERLAPPED
        Dim NumberOfBytesRead As Integer
        Dim Result As Integer
        EventObject = PrepareForOverlappedTransfer(HIDOverlapped)

        Do



            '***
            'API function: ReadFile
            'Purpose: Attempts to read an Input report from the device.

            'Accepts:
            'A device handle returned by CreateFile
            '(for overlapped I/O, CreateFile must have been called with FILE_FLAG_OVERLAPPED),
            'A pointer to a buffer for storing the report.
            'The Input report length in bytes returned by HidP_GetCaps,
            'A pointer to a variable that will hold the number of bytes read. 
            'An overlapped structure whose hEvent member is set to an event object.

            'Returns: the report in ReadBuffer.

            'The overlapped call returns immediately, even if the data hasn't been received yet.

            'To read multiple reports with one ReadFile, increase the size of ReadBuffer
            'and use NumberOfBytesRead to determine how many reports were returned.
            'Use a larger buffer if the application can't keep up with reading each report
            'individually. 
            '***
            'Debug.Write("input report length = " & UBound(inputReportBuffer) + 1)

            Result = ReadFile _
                (mReadHandle, _
                mBufferHandler.Buffer(mBufferHandler.PtrIn), _
                mBufferHandler.SizeOfReports, _
                NumberOfBytesRead, _
                HIDOverlapped)
            If Result <> 0 Then
                If mTrace Then
                    Trace.WriteLine("ReadThread - ReadFile failed: " + MyDebugging.ResultOfAPICall("ReadFile"))
                End If
            End If

            'Debug.WriteLine(MyDebugging.ResultOfAPICall("ReadFile"))
            'Debug.WriteLine("")
            'Debug.WriteLine("waiting for ReadFile")

            'API function: WaitForSingleObject

            'Purpose: waits for at least one report or a timeout.
            'Used with overlapped ReadFile.

            'Accepts:
            'An event object created with CreateEvent
            'A timeout value in milliseconds.

            'Returns: A result code.

            Result = WaitForSingleObject(EventObject, mReadTimeOut)


            'Debug.WriteLine(MyDebugging.ResultOfAPICall("WaitForSingleObject"))
            'Debug.WriteLine("")

            'Find out if ReadFile completed or timeout.
            Select Case Result
                Case WAIT_OBJECT_0

                    mBufferHandler.SetNextInPos()

                    SyncLock Me.GetPOS_BUF_Sync
                        GetPOS_BUF = mBufferHandler.GetPOS()
                    End SyncLock
                    BufferFullFlag.Set()


                    'ReadFile has completed
                    'Debug.WriteLine("")
                    'success = True
                    'Debug.WriteLine("ReadFile completed successfully.")

                Case WAIT_TIMEOUT

                    If mTrace Then
                        Trace.WriteLine("WAIT_TIMEOUT in ReadThread:" + MyDebugging.ResultOfAPICall("WaitForSingleObject"))
                    End If
                    'Cancel the operation on timeout
                    'Call CancelTransfer(readHandle, HIDHandle)
                    'Debug.WriteLine("Readfile timeout")
                    'Debug.WriteLine("")
                    'success = False
                    MyDeviceDetected = False
                Case Else
                    If mTrace Then
                        Trace.WriteLine("Read Error in ReadThread:" + MyDebugging.ResultOfAPICall("WaitForSingleObject"))
                    End If
                    'Cancel the operation on other error.
                    'Call CancelTransfer(readHandle, HIDHandle)
                    'Debug.WriteLine("")
                    'Debug.WriteLine("Readfile undefined error")
                    'success = False
                    MyDeviceDetected = False
            End Select

        Loop Until AbortReadThread = True
        Debug.WriteLine("ReadThread ended!")
        If mTrace Then
            Trace.WriteLine("ReadThread ended!")
        End If
    End Sub

    Public Sub ReadConsumerThread()
        Dim Buffer() As Byte
        Do
            BufferFullFlag.WaitOne()
            Do
                Buffer = mBufferHandler.Remove()
                If Buffer Is Nothing Then
                    Exit Do
                Else
                    Dim TraceStr As System.Text.StringBuilder
                    If mTrace Then
                        TraceStr = New System.Text.StringBuilder
                        TraceStr.Append("in:")
                        TraceStr.Append(mBufferHandler.GetLastPOSout)
                        TraceStr.Append(";")
                        Dim L As Integer
                        For L = 0 To CByte(UBound(Buffer))
                            TraceStr.Append((Hex(Buffer(L)) + ", "))
                        Next
                        System.Diagnostics.Trace.WriteLine(TraceStr)
                    End If

                    RaiseEvent NewMessage(Buffer)
                End If
            Loop

        Loop Until AbortReadThread = True
        Debug.WriteLine("ConsumerThread ended!")
    End Sub

    Public Sub StartRead(ByVal Trace As Boolean, ByVal BufferSize As Integer, ByVal HIDKernelBuffer As Integer, ByVal ReadTimeOut As Integer, ByVal WriteTimeOut As Integer)
        If mReadHandle = 0 Then
            GetHandle(&H4D8, &HA)
        End If

        'mReadHandle = ReadHandle
        mTrace = Trace
        mBufferHandler = New BufferHandler(33, BufferSize)
        mReadTimeOut = ReadTimeOut
        mWriteTimeOut = WriteTimeOut

        MyHID.SetNumberOfInputBuffers(mReadHandle, HIDKernelBuffer)

        mReadConsumerThread = New System.Threading.Thread(AddressOf Me.ReadConsumerThread)
        mReadConsumerThread.Start()

        mReadThread = New System.Threading.Thread(AddressOf Me.ReadThread)
        'mReportConsumerThread.Start()
        mReadThread.Priority = Threading.ThreadPriority.Highest
        mReadThread.Start()
    End Sub



    Private Sub GetHandle(ByVal MyVendorID As Short, ByVal MyProductID As Short, Optional ByVal Reconnect As Boolean = False)

        'Purpose    : Uses a series of API calls to locate a HID-class device
        '           ; by its Vendor ID and Product ID.

        'Returns    : True if the device is detected, False if not detected.

        Dim Count As Short
        Dim DeviceFound As Boolean
        Dim DevicePathName(127) As String
        Dim GUIDString As String
        Dim HidGuid As System.Guid
        Dim LastDevice As Boolean
        Dim MemberIndex As Integer
        'Dim MyProductID As Short
        'Dim MyVendorID As Short
        Dim Result As Boolean
        Dim Security As SECURITY_ATTRIBUTES
        Dim Success As Boolean
        Dim ErrorStr As New System.Text.StringBuilder
        Try

            HidGuid = Guid.Empty
            LastDevice = False
            MyDeviceDetected = False

            'Values for the SECURITY_ATTRIBUTES structure:
            Security.lpSecurityDescriptor = 0
            Security.bInheritHandle = CInt(True)
            Security.nLength = Len(Security)

            'Get the device's Vendor ID and Product ID from the form's text boxes.
            'GetVendorAndProductIDsFromTextBoxes(MyVendorID, MyProductID)

            '***
            'API function: 'HidD_GetHidGuid

            'Purpose: Retrieves the interface class GUID for the HID class.

            'Accepts: 'A System.Guid object for storing the GUID.
            '***

            HidD_GetHidGuid(HidGuid)
            ErrorStr.Append("HidD_GetHidGuid:" + MyDebugging.ResultOfAPICall("GetHidGuid"))

            'Display the GUID.
            GUIDString = HidGuid.ToString
            'Debug.WriteLine("  GUID for system HIDs: " & GUIDString)

            'Fill an array with the device path names of all attached HIDs.
            DeviceFound = MyDeviceManagement.FindDeviceFromGuid _
                (HidGuid, _
                DevicePathName)

            'If there is at least one HID, attempt to read the Vendor ID and Product ID
            'of each device until there is a match or all devices have been examined.

            If DeviceFound = True Then
                MemberIndex = 0
                Do
                    '***
                    'API function:
                    'CreateFile

                    'Purpose:
                    'Retrieves a handle to a device.

                    'Accepts:
                    'A device path name returned by SetupDiGetDeviceInterfaceDetail
                    'The type of access requested (read/write).
                    'FILE_SHARE attributes to allow other processes to access the device while this handle is open.
                    'A Security structure. Using Null for this may cause problems under Windows XP.
                    'A creation disposition value. Use OPEN_EXISTING for devices.
                    'Flags and attributes for files. Not used for devices.
                    'Handle to a template file. Not used.

                    'Returns: a handle without read or write access.
                    'This enables obtaining information about all HIDs, even system
                    'keyboards and mice. 
                    'Separate handles are used for reading and writing.
                    '***

                    HIDHandle = CreateFile _
                        (DevicePathName(MemberIndex), _
                        0, _
                        FILE_SHARE_READ Or FILE_SHARE_WRITE, _
                        Security, _
                        OPEN_EXISTING, _
                        0, _
                        0)

                    Debug.WriteLine(MyDebugging.ResultOfAPICall("CreateFile"))
                    Debug.WriteLine("  Returned handle: " & Hex(HIDHandle) & "h")

                    If (HIDHandle <> INVALID_HANDLE_VALUE) Then

                        'The returned handle is valid, 
                        'so find out if this is the device we're looking for.

                        'Set the Size property of DeviceAttributes to the number of bytes in the structure.
                        MyHID.DeviceAttributes.Size = Marshal.SizeOf(MyHID.DeviceAttributes)

                        '***
                        'API function:
                        'HidD_GetAttributes

                        'Purpose:
                        'Retrieves a HIDD_ATTRIBUTES structure containing the Vendor ID, 
                        'Product ID, and Product Version Number for a device.

                        'Accepts:
                        'A handle returned by CreateFile.
                        'A pointer to receive a HIDD_ATTRIBUTES structure.

                        'Returns:
                        'True on success, False on failure.
                        '***

                        Result = HidD_GetAttributes(HIDHandle, MyHID.DeviceAttributes)


                        Debug.WriteLine(MyDebugging.ResultOfAPICall("HidD_GetAttributes"))

                        If Result Then

                            'Debug.WriteLine("  HIDD_ATTRIBUTES structure filled without error.")

                            'Debug.WriteLine("  Structure size: " & MyHID.DeviceAttributes.Size)

                            'Debug.WriteLine("  Vendor ID: " & Hex(MyHID.DeviceAttributes.VendorID))
                            'Debug.WriteLine("  Product ID: " & Hex(MyHID.DeviceAttributes.ProductID))
                            'Debug.WriteLine("  Version Number: " & Hex(MyHID.DeviceAttributes.VersionNumber))

                            'Find out if the device matches the one we're looking for.
                            If (MyHID.DeviceAttributes.VendorID = MyVendorID) And _
                                (MyHID.DeviceAttributes.ProductID = MyProductID) Then

                                'It's the desired device.
                                Debug.WriteLine("  My device detected")

                                'Display the information in form's list box.
                                'lstResults.Items.Add("Device detected:")
                                'lstResults.Items.Add("  Vendor ID= " & Hex(MyHID.DeviceAttributes.VendorID))
                                'lstResults.Items.Add("  Product ID = " & Hex(MyHID.DeviceAttributes.ProductID))

                                ''Call ScrollToBottomOfListBox()

                                MyDeviceDetected = True

                                'Save the DevicePathName so OnDeviceChange() knows which name is my device.
                                MyDevicePathName = DevicePathName(MemberIndex)
                            Else

                                'It's not a match, so close the handle.
                                MyDeviceDetected = False

                                Result = CloseHandle(HIDHandle)

                                Debug.WriteLine(MyDebugging.ResultOfAPICall("CloseHandle"))
                            End If
                        Else
                            'There was a problem in retrieving the information. 
                            Debug.WriteLine("  Error in filling HIDD_ATTRIBUTES structure.")
                            MyDeviceDetected = False
                            Result = CloseHandle(HIDHandle)
                        End If

                    End If


                    'Keep looking until we find the device or there are no more left to examine.
                    MemberIndex = MemberIndex + 1

                Loop Until ((MyDeviceDetected = True) Or _
                    (MemberIndex = UBound(DevicePathName) + 1))
            End If

            If MyDeviceDetected Then

                'The device was detected.
                'Register to receive notifications if the device is removed or attached.
                Success = MyDeviceManagement.RegisterForDeviceNotifications _
                    (MyDevicePathName, _
                    Me.Handle, _
                    HidGuid, _
                    DeviceNotificationHandle)

                Debug.WriteLine("RegisterForDeviceNotifications = " & Success)

                'Learn the capabilities of the device.
                MyHID.Capabilities = MyHID.GetDeviceCapabilities _
                    (HIDHandle)

                If Success Then

                    'Find out if the device is a system mouse or keyboard.
                    HIDUsage = MyHID.GetHIDUsage(MyHID.Capabilities)

                    'Get and display the Input report buffer size.
                    GetInputReportBufferSize()
                    'cmdInputReportBufferSize.Enabled = True

                    'Get handles to use in requesting Input and Output reports.

                    mReadHandle = CreateFile _
                        (MyDevicePathName, _
                        GENERIC_READ, _
                        FILE_SHARE_READ Or FILE_SHARE_WRITE, _
                        Security, _
                        OPEN_EXISTING, _
                        FILE_FLAG_OVERLAPPED, _
                        0)

                    Debug.WriteLine(MyDebugging.ResultOfAPICall("CreateFile, ReadHandle"))
                    Debug.WriteLine("  Returned handle: " & Hex(mReadHandle) & "h")

                    If (mReadHandle = INVALID_HANDLE_VALUE) Then
                        If mTrace Then
                            Trace.WriteLine("CreateFile(ReadHandle) failed:" + MyDevicePathName)
                            Trace.WriteLine(MyDebugging.ResultOfAPICall("CreateFile, ReadHandle"))
                            Trace.WriteLine("Returned handle: " & Hex(mReadHandle) & "h")
                        End If
                        ExclusiveAccess = True
                        'lstResults.Items.Add("The device is a system " + HIDUsage + ".")
                        'lstResults.Items.Add("Windows 2000 and Windows XP obtain exclusive access to Input and Output reports for this devices.")
                        'lstResults.Items.Add("Applications can access Feature reports only.")
                        ''Call ScrollToBottomOfListBox()

                    Else

                        mWriteHandle = CreateFile _
                             (MyDevicePathName, _
                             GENERIC_WRITE, _
                             FILE_SHARE_READ Or FILE_SHARE_WRITE, _
                             Security, _
                             OPEN_EXISTING, _
                             0, _
                             0)

                        Debug.WriteLine(MyDebugging.ResultOfAPICall("CreateFile, WriteHandle"))
                        Debug.WriteLine("  Returned handle: " & Hex(mWriteHandle) & "h")

                        If mTrace Then
                            Trace.WriteLine("CreateFile(WriteHandle) failed:" + MyDevicePathName)
                            Trace.WriteLine(MyDebugging.ResultOfAPICall("CreateFile, WriteHandle"))
                            Trace.WriteLine("Returned handle: " & Hex(mWriteHandle) & "h")
                        End If

                        '(optional)
                        'Flush any waiting reports in the input buffer.
                        If Reconnect = False Then
                            MyHID.FlushQueue(mReadHandle)
                        End If
                    End If

                End If

            Else

                'The device wasn't detected.
                'lstResults.Items.Add("Device not found.")
                'cmdInputReportBufferSize.Enabled = False
                'cmdOnce.Enabled = True

                Throw (New ApplicationException("USB Device not found."))

                ''Call ScrollToBottomOfListBox()
            End If

        Catch ex As Exception

            Throw (New ApplicationException(ex.Message))
            'Call HandleException(Me.Name, ex)

        End Try
    End Sub





    'Private Sub cmdInputReportBufferSize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    'Set the number of Input reports the host will store.
    '    Try
    '        Call SetInputReportBufferSize()

    '    Catch ex As Exception
    '        Call HandleException(Me.Name, ex)
    '    End Try
    'End Sub




    Private Sub DeviceHasChanged()

        'Called if the user changes the Vendor ID or Product ID in the text box.
        Try
            'If a device was previously detected, stop receiving notifications about it.
            If MyDeviceDetected Then
                Call MyDeviceManagement.StopReceivingDeviceNotifications(DeviceNotificationHandle)
            End If

            'Search for the device the next time FindTheHid is called.
            MyDeviceDetected = False

        Catch ex As Exception
            'Call HandleException(Me.Name, ex)
        End Try
    End Sub




    Private Function GetInputReportBufferSize() As Integer

        'Purpose    : Finds and displays the number of Input buffers
        '           : (the number of Input reports the host will store). 

        Dim NumberOfInputBuffers As Integer

        Try
            'Get the number of input buffers.
            MyHID.GetNumberOfInputBuffers _
                (HIDHandle, _
                NumberOfInputBuffers)

            'Display the result in the text box.
            GetInputReportBufferSize = NumberOfInputBuffers

        Catch ex As Exception
            'Call HandleException(Me.Name, ex)
        End Try

    End Function



    Private Sub SetInputReportBufferSize(ByVal Size As Integer)

        'Purpose    : Set the number of Input buffers (the number of Input reports 
        '           : the host will store) from the value in the text box.
        Try
            'Get the number of buffers from the text box.


            'Set the number of buffers.
            MyHID.SetNumberOfInputBuffers _
                (HIDHandle, _
                Size)

            'Verify and display the result.
            GetInputReportBufferSize()

        Catch ex As Exception
            'Call HandleException(Me.Name, ex)
        End Try
    End Sub

    Private Sub Startup()

        'Purpose    : Perform actions that must execute when the program starts.

        Try
            MyHID = New Hid
            'Call InitializeDisplay()
            'tmrContinuousDataCollect.Enabled = False
            'tmrContinuousDataCollect.Interval = 1000

        Catch ex As Exception
            'Call HandleException(Me.Name, ex)
        End Try

    End Sub

    Protected Overrides Sub Finalize()




        MyBase.Finalize()
    End Sub


    Protected Overrides Sub WndProc(ByRef m As Message)

        'Purpose    : Overrides WndProc to enable checking for and handling
        '           : WM_DEVICECHANGE(messages)

        'Accepts    : m - a Windows Message                   

        Try
            'The OnDeviceChange routine processes WM_DEVICECHANGE messages.
            If m.Msg = WM_DEVICECHANGE Then
                OnDeviceChange(m)
            End If

            'Let the base form process the message.
            MyBase.WndProc(m)

        Catch ex As Exception
            'Call HandleException(Me.Name, ex)
        End Try

    End Sub


    Shared Sub HandleException(ByVal moduleName As String, ByVal e As Exception)

        'Purpose    : Provides a central mechanism for exception handling.
        '           : Displays a message box that describes the exception.

        'Accepts    : moduleName - the module where the exception occurred.
        '           : e - the exception

        Dim Message As String
        Dim Caption As String

        Try
            'Create an error message.
            Message = "Exception: " & e.Message & ControlChars.CrLf & _
            "Module: " & moduleName & ControlChars.CrLf & _
            "Method: " & e.TargetSite.Name

            'Specify a caption.
            Caption = "Unexpected Exception"

            'Display the message in a message box.
            MessageBox.Show(Message, Caption, MessageBoxButtons.OK)
            Debug.Write(Message)

        Finally
        End Try

    End Sub

    Private Sub lstResults_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub InitializeComponent()
        '
        'frmHID
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(8, 8)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmHID"
        Me.ShowInTaskbar = False
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized

    End Sub

    Public Sub New()

    End Sub

    Private Sub frmHID_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        AbortReadThread = True
    End Sub
End Class