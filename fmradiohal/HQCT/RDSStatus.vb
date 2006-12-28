Option Strict On

Imports System
Imports System.Text
Imports System.Diagnostics


Namespace RDS

    Enum enBlockType
        A = 0
        B = 1
        C = 2
        D = 3
        C2 = 4
        E = 5
        iE = 6
        ib = 7
    End Enum

    Friend Class RDSStatus

        Public Delegate Sub RDS_Delegate(ByRef RDSMessage As FMRadioHAL.stRDSRAWMessage)

        Public Event RDS_Message As RDS_Delegate

        'Private mSynchronizationFound As Boolean


        'Public Property SynchronizationFound() As Boolean
        '    Get
        '        Return mSynchronizationFound
        '    End Get
        '    Set(ByVal Value As Boolean)
        '        mSynchronizationFound = Value
        '    End Set
        'End Property
        'Private mdataOverflow As Boolean


        'Public Property DataOverflow() As Boolean
        '    Get
        '        Return mdataOverflow
        '    End Get
        '    Set(ByVal Value As Boolean)
        '        mdataOverflow = Value
        '    End Set
        'End Property
        'Private mresetDetected As Boolean


        'Public Property ResetDetected() As Boolean
        '    Get
        '        Return mresetDetected
        '    End Get
        '    Set(ByVal Value As Boolean)
        '        mresetDetected = Value
        '    End Set
        'End Property
        'Private mlastRDSBlockID As enBlockType


        'Public Property LastRDSBlockID() As enBlockType
        '    Get
        '        Return mlastRDSBlockID
        '    End Get
        '    Set(ByVal Value As enBlockType)
        '        mlastRDSBlockID = Value
        '    End Set
        'End Property
        'Private mlastRDSBlockError As Integer


        'Public Property LastRDSBlockError() As Integer
        '    Get
        '        Return mlastRDSBlockError
        '    End Get
        '    Set(ByVal Value As Integer)
        '        mlastRDSBlockError = Value
        '    End Set
        'End Property
        'Private mbadBlocksCounter As Integer


        'Public Property BadBlocksCounter() As Integer
        '    Get
        '        Return mbadBlocksCounter
        '    End Get
        '    Set(ByVal Value As Integer)
        '        mbadBlocksCounter = Value
        '    End Set
        'End Property
        'Private mgoodBlocksCounter As Integer


        'Public Property GoodBlocksCounter() As Integer
        '    Get
        '        Return mgoodBlocksCounter
        '    End Get
        '    Set(ByVal Value As Integer)
        '        mgoodBlocksCounter = Value
        '    End Set
        'End Property
        'Private mprevRDSBlockID As enBlockType


        'Public Property PrevRDSBlockID() As enBlockType
        '    Get
        '        Return mprevRDSBlockID
        '    End Get
        '    Set(ByVal Value As enBlockType)
        '        mprevRDSBlockID = Value
        '    End Set
        'End Property
        'Private mprevRDSBlockError As Integer


        'Public Property PrevRDSBlockError() As Integer
        '    Get
        '        Return mprevRDSBlockError
        '    End Get
        '    Set(ByVal Value As Integer)
        '        mprevRDSBlockError = Value
        '    End Set
        'End Property
        'Private mpreviousBlock As System.Int16


        'Public Property PreviousBlock() As System.Int16
        '    Get
        '        Return mpreviousBlock
        '    End Get
        '    Set(ByVal Value As System.Int16)
        '        mpreviousBlock = Value
        '    End Set
        'End Property

        ' Private mlastBlock As System.Int16


        'Public Property LastBlock() As System.Int16
        '    Get
        '        Return mlastBlock
        '    End Get
        '    Set(ByVal Value As System.Int16)
        '        mlastBlock = Value
        '    End Set
        'End Property


        Private rdsBlockGroup As FMRadioHAL.stRDSRAWMessage

        Private AReceived As Boolean = False
        Private BReceived As Boolean = False
        Private CReceived As Boolean = False
        Private DReceived As Boolean = False
        Private rdsBlocksAandBReceived As Boolean = False
        Private rdsBlocksDandAReceived As Boolean = False
        Private rdsGroupFound As Boolean = False
        Private mreceivedBlocksCounter(8) As Long
        Private charCount As Integer = 0
        Private ignorePreviousBlock As Boolean = True


        'Public Property ReceivedBlocksCounter() As Long()
        '    Get
        '        Return mreceivedBlocksCounter
        '    End Get
        '    Set(ByVal Value As Long())
        '        mreceivedBlocksCounter = Value

        '    End Set
        'End Property


        Public Sub decode(ByRef IncMessage As HQCT.stIncMessage)
            'Dim byteOffset As Integer = 4

            '/*
            ' * Byte 4 is RDS STATUS
            ' * 
            ' * BIT      SYMBOL      DESCRIPTION
            ' * 7        SYNC        Synchronization found status. 0 = synchronization is searched. 1 = synchronization
            ' *                       found.
            ' * 6        DOFL        Data overflow flag. 0 = normal operation. 1 = data overflow is detected (no update).
            ' * 5        RSTD        Reset detected. 0 = normal operation. 1 = decoder reset (POR) is in progress.
            ' * 4 to 2   LBI[2:0]    Last block identification. See Table 25.
            ' * 1 and 0  ELB[1:0]    Error status last block. See Table 26.
            ' * 
            ' * LBI2 LBI1 LBI0   BLOCK TYPE IDENTIFICATION OF LAST RECEIVED BLOCK DATA
            ' * 0    0    0      A
            ' * 0    0    1      B
            ' * 0    1    0      C
            ' * 0    1    1      D
            ' * 1    0    0      C’
            ' * 1    0    1      E (RBDS mode)
            ' * 1    1    0      invalid E (RDS mode)
            ' * 1    1    1      invalid block
            ' *
            ' * ELB1 ELB0        ERROR STATUS OF LAST RECEIVED BLOCK DATA
            ' * 0    0           no errors
            ' * 0    1           corrected burst error of maximum 2 bits
            ' * 1    0           corrected burst error of maximum 5 bits
            ' * 1    1           uncorrectable error
            ' */


            ' byteOffset should be 4 here.
            'Dim rdsStatus As Byte = inputBuffer(byteOffset) : byteOffset = byteOffset + 1

            'Dim IncMessage As HQCT.stIncMessage

            'SynchronizationFound = ((rdsStatus And &H80) = &H80)
            'DataOverflow = ((rdsStatus And &H40) = &H40)
            'ResetDetected = ((rdsStatus And &H20) = &H20)
            'LastRDSBlockID = CType((rdsStatus And &H1C) >> 2, enBlockType)
            'LastRDSBlockError = (rdsStatus And &H3)

            'ReceivedBlocksCounter(0) += 1

            'ReceivedBlocksCounter(LastRDSBlockID) += 1

            ' Only update RDS data if we have synch and normal operation



            'Debug.Write(Constants.BlockTypeDictionary[lastRDSBlockID] + " ");
            'charCount++;
            'if (charCount > 40)
            '{
            '    Debug.WriteLine("");
            '    charCount = 0;
            '}

            '/*
            ' * Byte 5 is RDS LDATM
            ' * 
            ' * BIT      SYMBOL      DESCRIPTION
            ' * 7 to 0   LM[15:8]    Block data of previously received RDS block, most significant byte.
            ' */



            'mlastBlock = CType(inputBuffer(byteOffset), System.Int16) << 8 : byteOffset = byteOffset + 1

            '/*
            ' * Byte 6 is RDS LDATL
            ' * 
            ' * BIT      SYMBOL      DESCRIPTION
            ' * 7 to 0   LM[7:0]    Block data of previously received RDS block, most significant byte.
            ' */
            '

            'mlastBlock = mlastBlock Or CType(inputBuffer(byteOffset), System.Int16) : byteOffset = byteOffset + 1
            'mlastBlock = IncMessage.LDAT

            '/*
            ' * Byte 7 is RDS PDATM
            ' * 
            ' * BIT      SYMBOL      DESCRIPTION
            ' * 7 to 0   LM[15:8]    Block data of previously received RDS block, most significant byte. Only relevant when reduced data request mode is active.
            ' */


            'PreviousBlock = CType(inputBuffer(byteOffset), System.Int16) << 8 : byteOffset = byteOffset + 1
            '/*
            ' * Byte 8 is RDS PDATL
            ' * 
            ' * BIT      SYMBOL      DESCRIPTION
            ' * 7 to 0   LM[7:0]    Block data of previously received RDS block, least significant byte. Only relevant when reduced data request mode is active.
            ' */

            'PreviousBlock = PreviousBlock Or CType(inputBuffer(byteOffset), System.Int16) : byteOffset = byteOffset + 1
            'PreviousBlock = IncMessage.PDAT

            '/*
            ' * Byte 9 is RDS COUNT
            ' * 
            ' * BIT      SYMBOL      DESCRIPTION
            ' * 7 to 2   BBC[5:0]    Bad block counter. Counter value of received invalid blocks; n = 0 to 63.
            ' * 1 and 0  GBC[5:4]    Good block counter. Two most significant bits of received valid blocks counter;
            ' *                       n = 0 to 62. Remark: the least significant bit is not available for reading (assume
            ' *                       GBC0 = 0).
            ' *
            ' * Byte 10 is RDS PREVIOUS BLOCK IDENTIFIER
            ' * 
            ' * BIT      SYMBOL      DESCRIPTION
            ' * 7 to 5   GBC[3:1]    Good block counter. Three least significant bits of received valid blocks counter;
            ' *                       n = 0 to 62. Remark: the least significant bit is not available for reading (assume
            ' *                       GBC0 = 0).
            ' * 4 to 2   PBI[2:0]    Previous block identification.
            ' * 1 and 0  EPB[1:0]    Error status previous block.
            ' */
            '

            ' byteOffset should be 9 here.
            'Dim rdsCount As Byte = inputBuffer(byteOffset) : byteOffset = byteOffset + 1

            ' byteOffset should be 10 here.
            'Dim rdsPrevious As Byte = inputBuffer(byteOffset) : byteOffset = byteOffset + 1

            'BadBlocksCounter = rdsCount And &HFC
            'BadBlocksCounter = IncMessage.RDS_COUNT_PBIN.BBC
            'GoodBlocksCounter = ((rdsCount And &H3) << 4) Or ((rdsPrevious And &HE0) >> 4)
            'GoodBlocksCounter = IncMessage.RDS_COUNT_PBIN.GBC

            If Not ((IncMessage.RDS_STATUS.SYNC) And (Not IncMessage.RDS_STATUS.DOFL) And (Not IncMessage.RDS_STATUS.RSTD)) Then
                ignorePreviousBlock = True
                Return
            End If

            If ignorePreviousBlock Then
                ignorePreviousBlock = False ' Reset
                If IncMessage.RDS_COUNT_PBIN.PBI <> HQCT.enBI.ib Then
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
            Dim blockBuildingMethod As Integer = 0

            Select Case blockBuildingMethod
                Case 0

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
                    If (IncMessage.RDS_STATUS.LBI = HQCT.enBI.C) And (IncMessage.RDS_STATUS.ELB < HQCT.enEB.UNCORRECTABLE_ERROR) And AReceived Then
                        rdsBlockGroup.Block2 = IncMessage.LDAT
                        CReceived = True
                        DReceived = False
                    Else
                        If (IncMessage.RDS_COUNT_PBIN.PBI = HQCT.enBI.C) And (IncMessage.RDS_COUNT_PBIN.EPB < HQCT.enEB.UNCORRECTABLE_ERROR) And BReceived Then
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


                Case 1

                    'If (PrevRDSBlockID = enBlockType.A) And (LastRDSBlockID = enBlockType.B) Then
                    '    rdsBlockGroup.Block0 = mpreviousBlock
                    '    rdsBlockGroup.Block1 = mlastBlock
                    '    rdsBlocksAandBReceived = True
                    'End If
                    'If (PrevRDSBlockID = enBlockType.C) And (LastRDSBlockID = enBlockType.D) Then
                    '    rdsBlockGroup.Block2 = mpreviousBlock
                    '    rdsBlockGroup.Block3 = mlastBlock

                    '    If rdsBlocksAandBReceived Then
                    '        rdsGroupFound = True
                    '        rdsBlocksAandBReceived = False
                    '        rdsBlocksDandAReceived = False
                    '    End If
                    'End If
                    'If (PrevRDSBlockID = enBlockType.D) And (LastRDSBlockID = enBlockType.A) Then
                    '    rdsBlockGroup.Block3 = mpreviousBlock

                    '    If rdsBlocksDandAReceived Then
                    '        rdsGroupFound = True
                    '        rdsBlocksAandBReceived = False
                    '        rdsBlocksDandAReceived = False
                    '    End If
                    'End If
                    'If (PrevRDSBlockID = enBlockType.B) And (LastRDSBlockID = enBlockType.C) Then
                    '    rdsBlockGroup.Block1 = mpreviousBlock

                    '    rdsBlockGroup.Block2 = mlastBlock
                    'End If



                    'If rdsGroupFound Then

                    '    'interpreter.decode(rdsBlockGroup, 8)
                    '    RaiseEvent RDS_Message(rdsBlockGroup)
                    '    rdsGroupFound = False
                    'End If

                    'If (PrevRDSBlockID = enBlockType.D) And (LastRDSBlockID = enBlockType.A) Then
                    '    rdsBlockGroup.Block0 = mlastBlock
                    '    rdsBlocksDandAReceived = True
                    'End If
            End Select
        End Sub 'decode
    End Class 'Status
End Namespace 'HQCTS.RDS
