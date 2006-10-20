Option Strict On
Imports System
Imports System.Text
Imports System.Runtime.InteropServices
' DllImport
Namespace HQCT

    Class mcHID
        Private Const DLL_NAME As String = "mcHID.dll"

        Public Const VENDOR_ID As Integer = &H4D8
        Public Const PRODUCT_ID As Integer = &HA

        Public Const WM_HID_EVENT As Integer = &H8000 + 200
        Public Const NOTIFY_PLUGGED As Integer = &H1
        Public Const NOTIFY_UNPLUGGED As Integer = &H2
        Public Const NOTIFY_CHANGED As Integer = &H3
        Public Const NOTIFY_READ As Integer = &H4

        Public Const INPUT_BUFFER_SIZE As Integer = 32
        Public Const OUTPUT_BUFFER_SIZE As Integer = 32


        <DllImport(DLL_NAME)> _
        Public Shared Function Connect(ByVal pHostWin As IntPtr) As Boolean
        End Function


        <DllImport(DLL_NAME)> _
        Public Shared Function Disconnect() As Boolean

        End Function


        <DllImport(DLL_NAME)> _
        Public Shared Function GetItem(ByVal pIndex As Integer) As IntPtr

        End Function


        <DllImport(DLL_NAME)> _
        Public Shared Function GetItemCount() As Integer

        End Function


        <DllImport(DLL_NAME)> _
        Public Shared Function Read(ByVal pHandle As IntPtr, ByVal pData As Byte()) As Boolean

        End Function

        <DllImport(DLL_NAME)> _
        Public Shared Function Write(ByVal pHandle As IntPtr, ByVal pData As Byte()) As Boolean

        End Function

        <DllImport(DLL_NAME)> _
        Public Shared Function ReadEx(ByVal pVendorID As Integer, ByVal pProductID As Integer, ByVal pData As Byte()) As Boolean

        End Function

        <DllImport(DLL_NAME)> _
        Public Shared Function WriteEx(ByVal pVendorID As Integer, ByVal pProductID As Integer, ByVal pData As Byte()) As Boolean

        End Function


        <DllImport(DLL_NAME)> _
        Public Shared Function GetHandle(ByVal pVendorID As Integer, ByVal pProductID As Integer) As IntPtr

        End Function




        <DllImport(DLL_NAME)> _
        Public Shared Function GetVendorID(ByVal pHandle As IntPtr) As Integer

        End Function


        <DllImport(DLL_NAME)> _
        Public Shared Function GetProductID(ByVal pHandle As IntPtr) As Integer

        End Function


        <DllImport(DLL_NAME)> _
        Public Shared Function GetVersion(ByVal pHandle As IntPtr) As Integer

        End Function


        <DllImport(DLL_NAME)> _
        Public Overloads Shared Sub GetVendorName(ByVal pHandle As IntPtr, ByVal pVendorID As StringBuilder, ByVal pLen As Integer)

        End Sub


        Public Overloads Shared Function GetVendorName(ByVal pHandle As IntPtr) As String
            Dim vendorName As New StringBuilder(256)
            GetVendorName(pHandle, vendorName, vendorName.Capacity)
            Return vendorName.ToString()
        End Function 'GetVendorName


        <DllImport(DLL_NAME)> _
        Public Overloads Shared Sub GetProductName(ByVal pHandle As IntPtr, ByVal pVendorID As StringBuilder, ByVal pLen As Integer)

        End Sub


        Public Overloads Shared Function GetProductName(ByVal pHandle As IntPtr) As String
            Dim productName As New StringBuilder(256)
            GetProductName(pHandle, productName, productName.Capacity)
            Return productName.ToString()
        End Function 'GetProductName


        <DllImport(DLL_NAME)> _
        Public Overloads Shared Sub GetSerialNumber(ByVal pHandle As IntPtr, ByVal pVendorID As StringBuilder, ByVal pLen As Integer)

        End Sub


        Public Overloads Shared Function GetSerialNumber(ByVal pHandle As IntPtr) As String
            Dim serialNumber As New StringBuilder(256)
            GetSerialNumber(pHandle, serialNumber, serialNumber.Capacity)
            Return serialNumber.ToString()
        End Function 'GetSerialNumber


        <DllImport(DLL_NAME)> _
        Public Shared Function GetInputReportLength(ByVal pHandle As IntPtr) As Integer

        End Function


        <DllImport(DLL_NAME)> _
        Public Shared Function GetOutputReportLength(ByVal pHandle As IntPtr) As Integer

        End Function


        <DllImport(DLL_NAME)> _
        Public Shared Sub SetReadNotify(ByVal pHandle As IntPtr, ByVal pValue As Boolean)

        End Sub


        <DllImport(DLL_NAME)> _
        Public Shared Function IsReadNotifyEnabled(ByVal pHandle As IntPtr) As Boolean

        End Function


        <DllImport(DLL_NAME)> _
        Public Shared Function IsAvailable(ByVal pVendorID As Integer, ByVal pProductID As Integer) As Boolean

        End Function
    End Class 'mcHID
End Namespace 'HQCT
