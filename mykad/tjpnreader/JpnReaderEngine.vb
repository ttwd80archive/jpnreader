Public Class JpnReaderEngine
    <System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)> _
    Public Structure SCARD_IO_REQUEST

        '''DWORD->unsigned int
        Public dwProtocol As UInteger

        '''DWORD->unsigned int
        Public cbPciLength As UInteger
    End Structure


    '''SCARD_SCOPE_TERMINAL -> 1
    Public Const SCARD_SCOPE_TERMINAL As Integer = 1

    '''SCARD_SCOPE_SYSTEM -> 2
    Public Const SCARD_SCOPE_SYSTEM As Integer = 2

    '''SCARD_SCOPE_USER -> 0
    Public Const SCARD_SCOPE_USER As Integer = 0

    '''SCARD_SHARE_EXCLUSIVE -> 1
    Public Const SCARD_SHARE_EXCLUSIVE As Integer = 1

    '''SCARD_SHARE_SHARED -> 2
    Public Const SCARD_SHARE_SHARED As Integer = 2

    '''SCARD_SHARE_DIRECT -> 3
    Public Const SCARD_SHARE_DIRECT As Integer = 3

    '''SCARD_PROTOCOL_UNDEFINED -> 0x00000000
    Public Const SCARD_PROTOCOL_UNDEFINED As Integer = 0

    '''SCARD_PROTOCOL_OPTIMAL -> 0x00000000
    Public Const SCARD_PROTOCOL_OPTIMAL As Integer = 0

    '''SCARD_PROTOCOL_DEFAULT -> 0x80000000
    Public Const SCARD_PROTOCOL_DEFAULT As Integer = -2147483648

    '''SCARD_PROTOCOL_RAW -> 0x00010000
    Public Const SCARD_PROTOCOL_RAW As Integer = 65536

    '''SCARD_PROTOCOL_Tx -> (SCARD_PROTOCOL_T0 | SCARD_PROTOCOL_T1)
    Public Const SCARD_PROTOCOL_Tx As Integer = (SCARD_PROTOCOL_T0 Or SCARD_PROTOCOL_T1)

    '''SCARD_PROTOCOL_T1 -> 0x00000002
    Public Const SCARD_PROTOCOL_T1 As Integer = 2

    '''SCARD_PROTOCOL_T0 -> 0x00000001
    Public Const SCARD_PROTOCOL_T0 As Integer = 1

    '''Return Type: LONG->int
    '''dwScope: DWORD->unsigned int
    '''pvReserved1: LPCVOID->void*
    '''pvReserved2: LPCVOID->void*
    '''phContext: LPSCARDCONTEXT->SCARDCONTEXT*
    <System.Runtime.InteropServices.DllImportAttribute("winscard.dll", EntryPoint:="SCardEstablishContext")> _
    Public Shared Function SCardEstablishContext(ByVal dwScope As UInteger, ByVal pvReserved1 As System.IntPtr, ByVal pvReserved2 As System.IntPtr, ByRef phContext As UInteger) As Integer
    End Function
    '''Return Type: LONG->int
    '''hContext: SCARDCONTEXT->ULONG_PTR->unsigned int
    <System.Runtime.InteropServices.DllImportAttribute("winscard.dll", EntryPoint:="SCardReleaseContext")> _
    Public Shared Function SCardReleaseContext(ByVal hContext As UInteger) As Integer
    End Function
    '''Return Type: LONG->int
    '''hContext: SCARDCONTEXT->ULONG_PTR->unsigned int
    '''mszGroups: LPCWSTR->WCHAR*
    '''mszReaders: LPWSTR->WCHAR*
    '''pcchReaders: LPDWORD->DWORD*
    <System.Runtime.InteropServices.DllImportAttribute("winscard.dll", EntryPoint:="SCardListReadersW")> _
    Public Shared Function SCardListReadersW(ByVal hContext As UInteger, <System.Runtime.InteropServices.InAttribute(), System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)> ByVal mszGroups As String, <System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)> ByVal mszReaders As System.Text.StringBuilder, ByRef pcchReaders As UInteger) As Integer
    End Function

    '''Return Type: LONG->int
    '''hContext: SCARDCONTEXT->ULONG_PTR->unsigned int
    '''szReader: LPCWSTR->WCHAR*
    '''dwShareMode: DWORD->unsigned int
    '''dwPreferredProtocols: DWORD->unsigned int
    '''phCard: LPSCARDHANDLE->SCARDHANDLE*
    '''pdwActiveProtocol: LPDWORD->DWORD*
    <System.Runtime.InteropServices.DllImportAttribute("winscard.dll", EntryPoint:="SCardConnectW")> _
    Public Shared Function SCardConnectW(ByVal hContext As UInteger, <System.Runtime.InteropServices.InAttribute(), System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)> ByVal szReader As String, ByVal dwShareMode As UInteger, ByVal dwPreferredProtocols As UInteger, ByRef phCard As UInteger, ByRef pdwActiveProtocol As UInteger) As Integer
    End Function
    '''Return Type: LONG->int
    '''hCard: SCARDHANDLE->ULONG_PTR->unsigned int
    '''pioSendPci: LPCSCARD_IO_REQUEST->SCARD_IO_REQUEST*
    '''pbSendBuffer: LPCBYTE->BYTE*
    '''cbSendLength: DWORD->unsigned int
    '''pioRecvPci: LPSCARD_IO_REQUEST->_SCARD_IO_REQUEST*
    '''pbRecvBuffer: LPBYTE->BYTE*
    '''pcbRecvLength: LPDWORD->DWORD*
    <System.Runtime.InteropServices.DllImportAttribute("winscard.dll", EntryPoint:="SCardTransmit")> _
    Public Shared Function SCardTransmit(ByVal hCard As UInteger, ByRef pioSendPci As SCARD_IO_REQUEST, ByRef pbSendBuffer As Byte, ByVal cbSendLength As UInteger, ByRef pioRecvPci As SCARD_IO_REQUEST, ByRef pbRecvBuffer As Byte, ByRef pcbRecvLength As UInteger) As Integer
    End Function

    Private hCard As UInteger
    Private ioSendPci As SCARD_IO_REQUEST
    Private ioRecvPci As SCARD_IO_REQUEST
    Private hContext As UInteger

    Public Function init() As Integer
        Dim result As Integer
        result = SCardEstablishContext(SCARD_SCOPE_USER, IntPtr.Zero, IntPtr.Zero, hContext)
        Return result
    End Function

    Public Function readSegment(ByVal fileNumber As Integer, ByVal offset As Integer, ByVal length As Integer) As Byte()
        Dim result As UInteger
        Dim bufferLength As Integer
        Dim buffer(256) As Byte
        bufferLength = 256
        result = issueSetLengthRequest(length, buffer, bufferLength)
        If (result <> 0) Then
            Return Nothing
        End If

        bufferLength = 2
        result = issueSelectFileRequest(fileNumber, offset, length, buffer, bufferLength)
        If (result <> 0) Then
            Return Nothing
        End If

        bufferLength = 254
        result = issueGetDataRequest(length, buffer, bufferLength)

        Dim contentLength As Integer = bufferLength - 2 - 1
        Dim content(contentLength) As Byte
        For i As Integer = 0 To contentLength
            content(i) = buffer(i)
        Next
        Return content
    End Function

    Public Sub cleanUp()
        SCardReleaseContext(hContext)
    End Sub

    Private Function issueSelectFileRequest(ByVal fileNumber As Byte, ByVal offset As Integer, ByVal length As Byte, ByRef receiveBuffer As Byte(), ByRef bufferLength As UInteger)
        Dim cmd(12) As Byte
        Dim result As Integer
        Dim top As Integer
        Dim bottom As Integer
        Dim d As Integer


        cmd(0) = 204
        cmd(1) = 0
        cmd(2) = 0
        cmd(3) = 0
        cmd(4) = 8

        top = fileNumber
        bottom = 256

        d = top \ bottom
        cmd(5) = top Mod bottom
        cmd(6) = d
        cmd(7) = 1
        cmd(8) = 0

        top = offset
        bottom = 256
        d = top \ bottom
        cmd(9) = top Mod bottom
        cmd(10) = d

        cmd(11) = length
        cmd(12) = 0
        result = SCardTransmit(hCard, ioSendPci, cmd(0), cmd.Length, ioRecvPci, receiveBuffer(0), bufferLength)
        Return result
    End Function


    Private Function issueGetDataRequest(ByVal length As Byte, ByRef receiveBuffer As Byte(), ByRef bufferLength As UInteger)
        Dim result As UInteger = 0
        Dim cmd(5) As Byte
        cmd(0) = 204
        cmd(1) = 6
        cmd(2) = 0
        cmd(3) = 0
        cmd(4) = length
        result = SCardTransmit(hCard, ioSendPci, cmd(0), 5, ioRecvPci, receiveBuffer(0), bufferLength)
        Return result
    End Function

    Private Function issueSetLengthRequest(ByVal length As UInteger, ByRef receiveBuffer As Byte(), ByRef bufferLength As UInteger)
        Dim result As UInteger = 0
        Dim CmdSetLength(9) As Byte
        CmdSetLength(0) = 200
        CmdSetLength(1) = 50
        CmdSetLength(2) = 0
        CmdSetLength(3) = 0
        CmdSetLength(4) = 5
        CmdSetLength(5) = 8
        CmdSetLength(6) = 0
        CmdSetLength(7) = 0
        CmdSetLength(8) = CByte(length Mod 256)
        CmdSetLength(9) = CByte(length \ 256)
        result = SCardTransmit(hCard, ioSendPci, CmdSetLength(0), 10, ioRecvPci, receiveBuffer(0), bufferLength)
        Return result
    End Function
End Class
