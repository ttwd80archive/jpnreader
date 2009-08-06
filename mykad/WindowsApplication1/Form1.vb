
<System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)> _
Public Structure SCARD_IO_REQUEST

    '''DWORD->unsigned int
    Public dwProtocol As UInteger

    '''DWORD->unsigned int
    Public cbPciLength As UInteger
End Structure


Public Class Form1

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



    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ListBox1.Items.Clear()
        PictureBox1.Image = Nothing
    End Sub

    Private hCard As UInteger
    Private ioSendPci As SCARD_IO_REQUEST
    Private ioRecvPci As SCARD_IO_REQUEST

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim hContext As UInteger
        Dim result As Integer
        Dim sb As System.Text.StringBuilder
        Dim cchReaders As UInteger
        Dim szReader As String
        Dim protocol As UInteger

        Dim stopwatch As Stopwatch = New Stopwatch()
        stopwatch.Start()
        ListBox1.Items.Add(DateTime.Now.ToLongTimeString())
        ListBox1.Refresh()

        sb = New System.Text.StringBuilder()
        result = SCardEstablishContext(SCARD_SCOPE_USER, IntPtr.Zero, IntPtr.Zero, hContext)
        ListBox1.Items.Add("SCardEstablishContext(): " + CStr(result))
        cchReaders = 256
        SCardListReadersW(hContext, Nothing, sb, cchReaders)


        ListBox1.Items.Add(sb.ToString())
        szReader = sb.ToString()

        result = SCardConnectW(hContext, szReader, SCARD_SHARE_SHARED, SCARD_PROTOCOL_T0, hCard, protocol)
        ListBox1.Items.Add("SCardConnectW(): " + CStr(result))

        ioSendPci.cbPciLength = 8
        ioSendPci.dwProtocol = protocol
        ioRecvPci.cbPciLength = 8
        ioRecvPci.dwProtocol = protocol

        Dim CmdSelectAppJPN As Byte() = {&H0, &HA4, &H4, &H0, &HA, &HA0, &H0, &H0, &H0, &H74, &H4A, &H50, &H4E, &H0, &H10}
        Dim receiveBuffer(262) As Byte
        Dim receiveBufferLength As UInteger
        receiveBufferLength = 2
        result = SCardTransmit(hCard, ioSendPci, CmdSelectAppJPN(0), CUInt(CmdSelectAppJPN.Length), ioRecvPci, receiveBuffer(0), receiveBufferLength)
        ListBox1.Items.Add("SCardTransmit() Select App : " + CStr(result) + ": Length " + CStr(receiveBufferLength))

        Dim CmdAppResponse As Byte() = {&H0, &HC0, &H0, &H0, &H5}
        receiveBufferLength = 7
        result = SCardTransmit(hCard, ioSendPci, CmdAppResponse(0), CUInt(CmdAppResponse.Length), ioRecvPci, receiveBuffer(0), receiveBufferLength)
        ListBox1.Items.Add("SCardTransmit() App Response: " + CStr(result) + ": Length " + CStr(receiveBufferLength))

        Dim fileContent1() As Byte = readFile1()
        ListBox1.Items.Add("")
        ListBox1.Items.Add("Name : [" + System.Text.Encoding.ASCII.GetString(fileContent1, &H3, &H96).Trim() + "]")
        ListBox1.Items.Add("ID : [" + System.Text.Encoding.ASCII.GetString(fileContent1, &H111, &HD).Trim() + "]")
        ListBox1.Items.Add("Gender : [" + System.Text.Encoding.ASCII.GetString(fileContent1, &H11E, 1).Trim() + "]")
        Dim birthdateByte(4 - 1) As Byte
        Array.Copy(fileContent1, &H127, birthdateByte, 0, 4)
        '+Hex(birthdateByte(1) +""+Hex(birthdateByte(2) +""+Hex(birthdateByte(3)  + 
        ListBox1.Items.Add("Birth Date : [" + bcdDateToString(birthdateByte) + "]")
        ListBox1.Items.Add("BirthPlace : [" + System.Text.Encoding.ASCII.GetString(fileContent1, &H12B, &H19).Trim() + "]")
        Dim dateIssuedByte(4 - 1) As Byte
        Array.Copy(fileContent1, &H127, dateIssuedByte, 0, 4)
        ListBox1.Items.Add("Birth Date : [" + bcdDateToString(dateIssuedByte) + "]")
        ListBox1.Items.Add("Citizenship : [" + System.Text.Encoding.ASCII.GetString(fileContent1, &H148, &H12).Trim() + "]")
        ListBox1.Items.Add("")

        'loadImage()

        stopwatch.Stop()
        SCardReleaseContext(hContext)
        ListBox1.Items.Add(DateTime.Now.ToLongTimeString())
        ListBox1.Items.Add("ms elapsed : " + CStr(stopwatch.Elapsed.ToString()))
        ListBox1.Refresh()

    End Sub
    Private Function bcdDateToString(ByVal bcdDate As Byte()) As String
        Dim result As String
        result = Hex(bcdDate(3)) + "/" + Hex(bcdDate(2)) + "/" + Hex(bcdDate(0)) + Hex(bcdDate(1))
        Return result
    End Function
    Private Sub loadImage()
        Dim pictureContent() As Byte = readFile2()
        Dim icPicture As Image = Drawing.Image.FromStream(New IO.MemoryStream(pictureContent))
        PictureBox1.Image = icPicture
    End Sub
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

        Dim content(bufferLength - 2 - 1) As Byte
        For i As Integer = 0 To (bufferLength - 2 - 1)
            content(i) = buffer(i)
        Next
        Return content
    End Function

    Public Function readFile1() As Byte()
        Const LENGTH_1 As Integer = 252
        Const LENGTH_2 As Integer = 207
        Dim content1 As Byte() = readSegment(1, 0, LENGTH_1)
        Dim content2 As Byte() = readSegment(1, LENGTH_1, LENGTH_2)
        Dim content(LENGTH_1 + LENGTH_2 - 1) As Byte
        Array.Copy(content1, 0, content, 0, LENGTH_1)
        Array.Copy(content2, 0, content, LENGTH_1, LENGTH_2)
        Return content
    End Function

    Public Function readFile2() As Byte()
        Const LIMIT As Integer = 4011
        Const IMAGE_OFFSET As Integer = 3
        Const IMAGE_LENGTH As Integer = 4000
        Const LENGTH As Integer = 252
        Dim content(LIMIT - 1) As Byte
        Dim contentLength As Integer = 0
        While contentLength < LIMIT
            Dim blockSize As Integer
            Dim unread As Integer = LIMIT - contentLength
            If unread > LENGTH Then
                blockSize = LENGTH
            Else
                blockSize = unread
            End If
            Dim blockContent As Byte() = readSegment(2, contentLength, blockSize)
            Array.Copy(blockContent, 0, content, contentLength, blockSize)
            contentLength = contentLength + blockContent.Length()
        End While
        Dim imageContent(IMAGE_LENGTH - 1) As Byte
        Array.Copy(content, IMAGE_OFFSET, imageContent, 0, IMAGE_LENGTH)
        Return imageContent
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


End Class
