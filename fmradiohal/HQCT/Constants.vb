Imports System.Runtime.InteropServices
Namespace HQCT

#Region "stIncMessage"
    Friend Enum enEB As Byte
        NO_ERROR = 0
        MAX_2_BITS = 1
        MAX_5_BITS = 2
        UNCORRECTABLE_ERROR = 3
    End Enum
    Friend Enum enBI As Byte
        A = 0
        B = 1
        C = 2
        D = 3
        C2 = 4
        E = 5
        iE = 6
        ib = 7
    End Enum

    '<StructLayout(LayoutKind.Explicit)> _
    'Friend Structure stOutTEF

    'End Structure


    <StructLayout(LayoutKind.Explicit)> _
        Friend Structure stIncMessage
        Friend WriteOnly Property Bytes() As Byte()
            Set(ByVal Value As Byte())
                Reserved1 = Value(0)
                Reserved2 = Value(1)
                STATUS.ByteValue = Value(2)
                LEVEL.LEVEL = Value(3)
                USNWAM.ByteValue = Value(4)
                RDS_STATUS.ByteValue = Value(5)
                LDATM = Value(6)
                LDATL = Value(7)
                PDATM = Value(8)
                PDATL = Value(9)
                RDS_COUNT_PBIN.RDS_COUNT = Value(10)
                RDS_COUNT_PBIN.RDS_PBIN = Value(11)
                '<FieldOffset(12)> Friend not_defined1 As Byte
                '<FieldOffset(13)> Friend not_defined2 As Byte
                '<FieldOffset(14)> Friend not_defined3 As Byte
                '<FieldOffset(15)> Friend not_defined4 As Byte
                '<FieldOffset(16)> Friend not_defined5 As Byte
                '<FieldOffset(17)> Friend not_defined6 As Byte
                '<FieldOffset(18)> Friend not_defined7 As Byte
                '<FieldOffset(19)> Friend not_defined8 As Byte
                '<FieldOffset(20)> Friend not_defined9 As Byte
                IFCount = Value(21)
                PortA = Value(22)   ' (status of i/o ports of PIC)
                PortB.ByteValue = Value(23) ' (bit 3 on means Phone switch input active, bit 4 on means NAV switc input active)
                PortC = Value(24)
                HQCTType.ByteValue = Value(25)
                Amp_status_1 = Value(26)
                Amp_status_2 = Value(27)
                Amp_status_3 = Value(28)
                Amp_status_4 = Value(29)
                '<FieldOffset(30)> Friend not_defined10 As Byte
                '<FieldOffset(31)> Friend not_defined11 As Byte
                '<FieldOffset(32)> Friend not_defined12 As Byte
            End Set
        End Property
        <FieldOffset(0)> Friend Reserved1 As System.Byte
        <FieldOffset(1)> Friend Reserved2 As System.Byte
        <FieldOffset(2)> Friend STATUS As stSTATUS
        <FieldOffset(3)> Friend LEVEL As stLEVEL
        <FieldOffset(4)> Friend USNWAM As stUSNWAM
        <FieldOffset(5)> Friend RDS_STATUS As stRDS_STATUS
        <FieldOffset(6)> Friend LDATM As Byte
        <FieldOffset(7)> Friend LDATL As Byte
        Friend ReadOnly Property LDAT() As System.Int16
            Get
                LDAT = CShort(LDATL) Or (CShort(LDATM) << 8)
            End Get
        End Property
        <FieldOffset(8)> Friend PDATM As Byte
        <FieldOffset(9)> Friend PDATL As Byte
        Friend ReadOnly Property PDAT() As System.Int16
            Get
                PDAT = CShort(PDATL) Or (CShort(PDATM) << 8)
            End Get
        End Property
        <FieldOffset(10)> Friend RDS_COUNT_PBIN As stRDS_COUNT_PBIN
        <FieldOffset(12)> Friend not_defined1 As Byte
        <FieldOffset(13)> Friend not_defined2 As Byte
        <FieldOffset(14)> Friend not_defined3 As Byte
        <FieldOffset(15)> Friend not_defined4 As Byte
        <FieldOffset(16)> Friend not_defined5 As Byte
        <FieldOffset(17)> Friend not_defined6 As Byte

        <FieldOffset(18)> Friend not_defined7 As Byte
        <FieldOffset(19)> Friend not_defined8 As Byte
        <FieldOffset(20)> Friend not_defined9 As Byte
        <FieldOffset(21)> Friend IFCount As Byte
        <FieldOffset(22)> Friend PortA As Byte    ' (status of i/o ports of PIC)
        <FieldOffset(23)> Friend PortB As stPortB ' (bit 3 on means Phone switch input active, bit 4 on means NAV switc input active)
        <FieldOffset(24)> Friend PortC As Byte
        <FieldOffset(25)> Friend HQCTType As stType
        <FieldOffset(26)> Friend Amp_status_1 As Byte ' (for HQCT-eA)
        <FieldOffset(27)> Friend Amp_status_2 As Byte
        <FieldOffset(28)> Friend Amp_status_3 As Byte
        <FieldOffset(29)> Friend Amp_status_4 As Byte
        <FieldOffset(30)> Friend not_defined10 As Byte
        <FieldOffset(31)> Friend not_defined11 As Byte
        <FieldOffset(32)> Friend not_defined12 As Byte

        <StructLayout(LayoutKind.Explicit)> _
            Friend Structure stType
            Friend Enum enHQCTType As Byte
                HQCTi = 0
                HQCTeA = 1
            End Enum

            <FieldOffset(0)> Friend ByteValue As System.Byte
            Friend ReadOnly Property HQCTType() As enHQCTType
                Get
                    Return CType(ByteValue And CByte(1), enHQCTType)
                End Get
            End Property
        End Structure
        <StructLayout(LayoutKind.Explicit)> _
            Friend Structure stPortB
            <FieldOffset(0)> Friend ByteValue As System.Byte
            Friend ReadOnly Property PHONE() As System.Boolean
                Get
                    Return (ByteValue And 4) = 4
                End Get
            End Property
            Friend ReadOnly Property NAV() As System.Boolean
                Get
                    Return (ByteValue And 8) = 8
                End Get
            End Property
        End Structure

        <StructLayout(LayoutKind.Explicit)> _
            Friend Structure stSTATUS
            <FieldOffset(0)> Friend ByteValue As System.Byte
            Friend ReadOnly Property ID() As System.Byte
                Get
                    Return (ByteValue And CByte(7))
                End Get

            End Property

            Friend ReadOnly Property RDAV() As System.Boolean
                Get
                    Return (ByteValue And 8) = 8
                End Get
            End Property
            Friend ReadOnly Property POR() As System.Boolean
                Get
                    Return (ByteValue And 16) = 16
                End Get

            End Property
            Friend ReadOnly Property AFUS() As System.Boolean
                Get
                    Return (ByteValue And 32) = 32
                End Get
            End Property
            Friend ReadOnly Property ASIA() As System.Boolean
                Get
                    Return (ByteValue And 64) = 64
                End Get
            End Property
            Friend ReadOnly Property STIN() As System.Boolean
                Get
                    Return (ByteValue And 128) = 128
                End Get
            End Property
        End Structure

        <StructLayout(LayoutKind.Explicit)> _
            Friend Structure stLEVEL
            <FieldOffset(0)> Friend LEVEL As System.Byte
        End Structure

        <StructLayout(LayoutKind.Explicit)> _
        Friend Structure stUSNWAM
            <FieldOffset(0)> Friend ByteValue As System.Byte
            Friend ReadOnly Property WAM() As System.Byte
                Get
                    Return (ByteValue And CByte(15))
                End Get
            End Property
            Friend ReadOnly Property USN() As System.Byte
                Get
                    Return (ByteValue And CByte(240)) >> 4
                End Get
            End Property
        End Structure

        <StructLayout(LayoutKind.Explicit)> _
            Friend Structure stRDS_STATUS
            <FieldOffset(0)> Friend ByteValue As System.Byte
            Friend ReadOnly Property ELB() As enEB
                Get
                    Return CType(ByteValue And CByte(3), enEB)
                End Get
            End Property
            Friend ReadOnly Property LBI() As enBI
                Get
                    Return CType((ByteValue And CByte(28)) >> 2, enBI)
                End Get
            End Property
            Friend ReadOnly Property RSTD() As System.Boolean
                Get
                    Return (ByteValue And 32) = 32
                End Get
            End Property
            Friend ReadOnly Property DOFL() As System.Boolean
                Get
                    Return (ByteValue And 64) = 64
                End Get
            End Property
            Friend ReadOnly Property SYNC() As System.Boolean
                Get
                    Return (ByteValue And 128) = 128
                End Get
            End Property
        End Structure

        <StructLayout(LayoutKind.Explicit)> _
        Friend Structure stRDS_COUNT_PBIN
            <FieldOffset(0)> Friend RDS_COUNT As System.Byte
            <FieldOffset(1)> Friend RDS_PBIN As System.Byte
            Friend ReadOnly Property IntValue() As System.Int16
                Get
                    IntValue = RDS_PBIN Or (RDS_COUNT << 8)
                End Get
            End Property

            'Friend ReadOnly Property HiByte() As System.Byte
            '    Get

            '    End Get
            'End Property
            'Friend ReadOnly Property LoByte() As System.Byte
            '    Get

            '    End Get
            'End Property

            Friend ReadOnly Property BBC() As System.Byte
                Get
                    Return CByte((IntValue And 64512) >> 10)
                End Get
            End Property
            Friend ReadOnly Property GBC() As System.Byte
                Get
                    Return CByte((IntValue And 992) >> 4)
                End Get
            End Property
            Friend ReadOnly Property PBI() As enBI
                Get
                    Return CType((IntValue And 28) >> 2, enBI)
                End Get
            End Property
            Friend ReadOnly Property EPB() As enEB
                Get
                    Return CType(IntValue And 3, enEB)
                End Get
            End Property
        End Structure
    End Structure

#End Region



#Region "TunerMessage"
    <StructLayout(LayoutKind.Explicit)> _
    Friend Structure stTunerSettingMessage
        '<FieldOffset(0)> Friend Const Reserved1 As System.Byte = 0
        <FieldOffset(0)> Friend Const Reserved As System.Byte = 0
        <FieldOffset(1)> Friend FIRST_INVALID_BYTE As System.Byte
        <FieldOffset(2)> Friend Const DEVICE_ADDRESS As System.Byte = &HC2
        <FieldOffset(3)> Friend Byte0 As System.Byte
        <FieldOffset(4)> Friend Byte1 As System.Byte
        <FieldOffset(5)> Friend Byte2 As System.Byte
        <FieldOffset(6)> Friend Byte3 As System.Byte
        <FieldOffset(7)> Friend Byte4 As System.Byte
        <FieldOffset(8)> Friend Byte5 As System.Byte
        <FieldOffset(9)> Friend Byte6 As System.Byte
        <FieldOffset(10)> Friend Byte7 As System.Byte

        Friend ReadOnly Property Bytes() As Byte()
            Get
                Dim Values() As Byte = New Byte(32) {}
                'Values(0) = Me.Reserved1
                Values(0) = Me.Reserved
                'Values(1) = FIRST_INVALID_BYTE
                Values(2) = DEVICE_ADDRESS
                Values(3) = Byte0
                Values(4) = Byte1
                Values(5) = Byte2
                Values(6) = Byte3
                Values(7) = Byte4
                Values(8) = Byte5
                Values(9) = Byte6
                Values(10) = Byte7
                Values(1) = 11
                Bytes = Values
            End Get
        End Property
        Friend ReadOnly Property BytesShort() As Byte()
            Get
                Dim Values() As Byte = New Byte(32) {}
                'Values(0) = Me.Reserved1
                Values(0) = Me.Reserved
                'Values(1) = FIRST_INVALID_BYTE
                Values(2) = DEVICE_ADDRESS
                Values(3) = Byte0
                Values(4) = Byte1
                Values(5) = Byte2
                Values(6) = 0
                Values(7) = 0
                Values(8) = 0
                Values(9) = 0
                Values(10) = 0
                Values(1) = 6
                BytesShort = Values
            End Get
        End Property


        Friend Property AF() As Boolean
            Get
                AF = ((Byte0 And 128) = 128)
            End Get
            Set(ByVal Value As Boolean)
                If Value Then
                    Byte0 = Byte0 Or CByte(128)
                Else
                    Byte0 = Byte0 And (CByte(255 - 128))
                End If
            End Set
        End Property
        Friend Property PLL() As System.Int16
            Get
                PLL = (CShort(Byte0 And &H7F) << 8) Or Byte1
            End Get
            Set(ByVal Value As System.Int16)
                'Value = Value And CShort(&H7FFF)
                Byte0 = Byte0 And CByte(128)
                Byte0 = Byte0 Or CByte(Value >> 8)

                Byte1 = CByte(Value And CShort(&HFF))
            End Set
        End Property
        Friend Property ANT() As Byte
            Get
                ANT = (Byte2 And CByte(&H7F))
            End Get
            Set(ByVal Value As Byte)
                Byte2 = Byte2 And CByte(128)
                Byte2 = Byte2 Or (Value And CByte(&H7F))
            End Set
        End Property
        Friend Property MUTE() As System.Boolean
            Get
                MUTE = ((Byte2 And 128) = 128)
            End Get
            Set(ByVal Value As System.Boolean)
                If Value Then
                    Byte2 = Byte2 Or CByte(128)
                Else
                    Byte2 = Byte2 And (CByte(255 - 128))
                End If
            End Set
        End Property
        Friend Enum enAMFM
            FM = 0
            AM = 1
        End Enum
        Friend Property AMFM() As enAMFM
            Get
                AMFM = CType(Byte3 And 1, enAMFM)
            End Get
            Set(ByVal Value As enAMFM)
                Byte3 = Byte3 And CByte(254)
                Byte3 = Byte3 Or CByte(Value)
            End Set
        End Property
        Friend Enum enBND
            FM_STD = 0
            AM_SW_MONO = 1
            FM_JAPAN = 2
            AM_SW_STEREO = 3
            FM_EAST = 4
            AM_LW_MW_MONO = 5
            FM_WEATHER = 6
            AM_LW_MW_STEREO = 7
        End Enum
        Friend Property BND() As enBND
            Get
                AMFM = CType(Byte3 And 7, enAMFM)
            End Get
            Set(ByVal Value As enBND)
                Byte3 = Byte3 And CByte(255 - 7)
                Byte3 = Byte3 Or CByte(Value)
            End Set
        End Property
        Friend Enum enIFPR
            PRESCALER_40 = 0
            PRESCALER_10 = 1
        End Enum
        Friend Property IFPR() As enIFPR
            Get
                IFPR = CType((Byte3 And 8) >> 3, enIFPR)
            End Get
            Set(ByVal Value As enIFPR)
                Byte3 = Byte3 And CByte(&HF7)
                Byte3 = Byte3 Or (CByte(Value) << 3)
            End Set
        End Property
        Friend Enum enREF
            f100khz = 0
            f50khz = 4
            f25khz = 2
            f20khz = 6
            f10khz = 1
            f10khz_2 = 5
            f10khz_3 = 3
            f10khz_4 = 7
        End Enum
        Friend Property REF() As enREF
            Get
                REF = CType((Byte3 And &H70) >> 4, enREF)
            End Get
            Set(ByVal Value As enREF)
                Byte3 = Byte3 And CByte(&H8F)
                Byte3 = Byte3 Or (CByte(Value) << 4)
            End Set
        End Property
        Friend Enum enIFMT
            IFCounter_20ms = 0
            IFCounter_2ms = 1
        End Enum
        Friend Property IFMT() As enIFMT
            Get
                IFMT = CType(Byte3 And 128, enIFMT)
            End Get
            Set(ByVal Value As enIFMT)
                Byte3 = Byte3 And CByte(&H7F)
                Byte3 = Byte3 Or (CByte(Value) << 7)
            End Set
        End Property
        Friend Enum enBW
            Dynamic = 0
            Wide = 1
            Medium = 2
            Narrow = 3
        End Enum
        Friend Property BW() As enBW
            Get
                BW = CType(Byte4 And 3, enBW)
            End Get
            Set(ByVal Value As enBW)
                Byte4 = Byte4 And CByte(&HFC)
                Byte4 = Byte4 Or CByte(Value)
            End Set
        End Property
        Friend Enum enFLAG
            Flag_High = 0
            Flag_Low = 1
        End Enum
        Friend Property FLAG() As enFLAG
            Get
                FLAG = CType((Byte4 And 4) >> 2, enFLAG)
            End Get
            Set(ByVal Value As enFLAG)
                Byte4 = Byte4 And CByte(&HFB)
                Byte4 = Byte4 Or (CByte(Value) << 2)
            End Set
        End Property
        Friend Enum enLODX
            Distance_Mode = 0
            Local_Mode = 1
        End Enum
        Friend Property LODX() As enLODX
            Get
                LODX = CType((Byte4 And 8) >> 3, enLODX)
            End Get
            Set(ByVal Value As enLODX)
                Byte4 = Byte4 And CByte(&HF7)
                Byte4 = Byte4 Or (CByte(Value) << 3)
            End Set
        End Property
        Friend Enum enAMSM_FMBW
            BW_Sel_MUTE_OFF = 0
            Alig_Mode_MUTE_ON = 1
        End Enum
        Friend Property AMSM_FMBW() As enAMSM_FMBW
            Get
                AMSM_FMBW = CType(((Byte4 And &H10) >> 4), enAMSM_FMBW)
            End Get
            Set(ByVal Value As enAMSM_FMBW)
                Byte4 = Byte4 And CByte(&HEF)
                Byte4 = Byte4 Or (CByte(Value) << 4)
            End Set
        End Property
        Friend Enum enAGC
            AGC150mV_16mV = 0
            AGC275mV_12mV = 1
            AGC400mV_8mV = 2
            AGC525mV_4mV = 3
        End Enum
        Friend Property AGC() As enAGC
            Get
                AGC = CType(((Byte4 And &H60) >> 5), enAGC)
            End Get
            Set(ByVal Value As enAGC)
                Byte4 = Byte4 And CByte(&H9F)
                Byte4 = Byte4 Or (CByte(Value) << 5)
            End Set
        End Property
        Friend Property KAGC() As System.Boolean
            Get
                KAGC = ((Byte4 And &H80) = &H80)
            End Get
            Set(ByVal Value As System.Boolean)
                Byte4 = Byte4 And CByte(&H7F)
                Byte4 = Byte4 Or (CByte(Value) << 7)
            End Set
        End Property
        Friend Property LSL() As System.Byte
            Get
                LSL = Byte5 And CByte(7)
            End Get
            Set(ByVal Value As System.Byte)
                Byte5 = Byte5 And CByte(&HF8)
                Byte5 = Byte5 Or (Value And CByte(7))
            End Set
        End Property

        Friend Property LST() As System.Byte
            Get
                LST = (Byte5 And CByte(&HF8)) >> 3
            End Get
            Set(ByVal Value As System.Byte)
                Byte5 = Byte5 And CByte(&H7)
                Byte5 = Byte5 Or ((Value And CByte(&H1F)) << 3)
            End Set
        End Property
        Friend Property CF() As System.Byte
            Get
                CF = (Byte6 And CByte(&H7F))
            End Get
            Set(ByVal Value As System.Byte)
                Byte6 = Byte6 And CByte(&H80)
                Byte6 = Byte6 Or (Value And CByte(&H7F))
            End Set
        End Property
        Friend Property TE() As System.Boolean
            Get
                TE = ((Byte6 And &H80) = &H80)
            End Get
            Set(ByVal Value As System.Boolean)
                Byte6 = Byte6 And CByte(&H7F)
                Byte6 = Byte6 Or (CByte(Value) << 7)
            End Set
        End Property
        Friend Property FGN() As System.Byte
            Get
                FGN = (Byte7 And CByte(&HF))
            End Get
            Set(ByVal Value As System.Byte)
                Byte7 = Byte7 And CByte(&HF0)
                Byte7 = Byte7 Or (CByte(Value) And CByte(&HF))
            End Set
        End Property
        Friend Property FOF() As System.Byte
            Get
                FOF = ((Byte7 And CByte(&HF0)) >> 4)
            End Get
            Set(ByVal Value As System.Byte)
                Byte7 = Byte7 And CByte(&HF)
                Byte7 = Byte7 Or ((CByte(Value) And CByte(&HF)) << 4)
            End Set
        End Property
    End Structure
#End Region
End Namespace
