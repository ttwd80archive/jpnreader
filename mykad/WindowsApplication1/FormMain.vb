
<System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)> _
Public Structure SCARD_IO_REQUEST

    '''DWORD->unsigned int
    Public dwProtocol As UInteger

    '''DWORD->unsigned int
    Public cbPciLength As UInteger
End Structure


Public Class FormMain

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
        textId.Text = ""
        textName.Text = ""
        textReligion.Text = ""
        textDOB.Text = ""
        textRace.Text = ""
        textGender.Text = ""
        textAddress.Text = ""
        textNationality.Text = ""
        'PictureBox1.Image = Nothing
        Me.Refresh()
    End Sub

    Private hCard As UInteger
    Private ioSendPci As SCARD_IO_REQUEST
    Private ioRecvPci As SCARD_IO_REQUEST
    Private imageLoadingProgress As Integer
    Private hContext As UInteger

    Private Function decodeCitizenship(ByVal c As String) As String
        If c = "WARGANEGARA" Then
            Return "W"
        Else
            Return "B"
        End If
    End Function
    Private Function decodeReligion(ByVal r As String) As String
        Return r.Substring(0, 1)
    End Function
    Private Function decodeRace(ByVal r As String) As String
        If r = "MELAYU" Then
            Return "0100"
        End If
        If r = "IBAN/SEA DAYAK" Then
            Return "1004"
        End If
        If r = "LAIN - LAIN" Then
            Return "0000"
        End If
        If r = "SRI LANKA" Then
            Return "0600"
        End If
        If r = "BUGIS" Then
            Return "0101"
        End If
        If r = "BANJAR" Then
            Return "0103"
        End If
        If r = "JAWA" Then
            Return "0104"
        End If
        If r = "BAJAU" Then
            Return "0801"
        End If
        If r = "DUSUN" Then
            Return "0802"
        End If
        If r = "KADAZAN" Then
            Return "0803"
        End If
        If r = "MURUT" Then
            Return "0804"
        End If
        If r = "SULUK" Then
            Return "0806"
        End If
        If r = "BISAYA" Then
            Return "0902"
        End If
        If r = "BRUNEI" Then
            Return "0904"
        End If
        If r = "KEDAYAN" Then
            Return "0909"
        End If
        If r = "MELANAU" Then
            Return "0913"
        End If
        If r = "SUNGAI" Then
            Return "0922"
        End If
        If r = "TAGAL" Then
            Return "0926"
        End If
        If r = "MELANAU" Then
            Return "1002"
        End If
        If r = "KAYAN" Then
            Return "1006"
        End If
        If r = "KENYAH" Then
            Return "1007"
        End If
        If r = "KELABIT" Then
            Return "1009"
        End If
        If r = "DUSUN" Then
            Return "1111"
        End If
        If r = "KEMBOJA" Then
            Return "1480"
        End If
        If r = "LAIN-LAIN" Then
            Return "1500"
        End If
        If r = "MELAYU INDONESIA" Then
            Return "D"
        End If
        If r = "KADAZAN" Then
            Return "15"
        End If
        If r = "KADAYUN" Then
            Return "16"
        End If
        If r = "LOH DAYUH" Then
            Return "17"
        End If
        If r = "MELANAU" Then
            Return "18"
        End If
        If r = "MURUT" Then
            Return "20"
        End If
        If r = "ORANG ASLI (SEMENANJUNG)" Then
            Return "1200"
        End If
        If r = "PAKISTAN" Then
            Return "22"
        End If
        If r = "SINO KADAZAN" Then
            Return "23"
        End If
        If r = "SULUK" Then
            Return "24"
        End If
        If r = "SUNGAI" Then
            Return "25"
        End If
        If r = "ZAMBOANGA" Then
            Return "26"
        End If
        If r = "KENYAH" Then
            Return "27"
        End If
        If r = "KAYAN" Then
            Return "28"
        End If
        If r = "BISAYAH" Then
            Return "29"
        End If
        If r = "INDIA MUSLIM" Then
            Return "0307"
        End If
        If r = "LAIN-LAIN" Then
            Return "1400"
        End If
        If r = "KEMBOJA" Then
            Return "B"
        End If
        If r = "THAILAND/SIAM" Then
            Return "S"
        End If
        If r = "BUMIPUTERA SARAWAK" Then
            Return "1000"
        End If
        If r = "BUMIPUTERA SABAH" Then
            Return "0800"
        End If
        If r = "BADANG" Then
            Return "1110"
        End If
        If r = "BALAU" Then
            Return "1105"
        End If
        If r = "BATANG AI" Then
            Return "1106"
        End If
        If r = "BATU ELAH" Then
            Return "1107"
        End If
        If r = "BELOT" Then
            Return "1103"
        End If
        If r = "BERAWAN" Then
            Return "1102"
        End If
        If r = "BIDAYUH/LAND DAYAK" Then
            Return "1005"
        End If
        If r = "BINADAN" Then
            Return "0901"
        End If
        If r = "BINTULU" Then
            Return "1109"
        End If
        If r = "BISAYA" Then
            Return "1101"
        End If
        If r = "BOLONGAN" Then
            Return "0930"
        End If
        If r = "BONGOL" Then
            Return "0903"
        End If
        If r = "BUKIT" Then
            Return "1104"
        End If
        If r = "BUKITAN" Then
            Return "1108"
        End If
        If r = "BUTON" Then
            Return "0931"
        End If
        If r = "COCOS" Then
            Return "0928"
        End If
        If r = "DUMPAS" Then
            Return "0905"
        End If
        If r = "IDAHAN" Then
            Return "0907"
        End If
        If r = "INDONESIA" Then
            Return "0700"
        End If
        If r = "IRRANUN" Then
            Return "0906"
        End If
        If r = "JAGOI" Then
            Return "1112"
        End If
        If r = "KAJAMAN" Then
            Return "1115"
        End If
        If r = "KAJANG" Then
            Return "1114"
        End If
        If r = "KANOWIT" Then
            Return "1116"
        End If
        If r = "KEDAYAN" Then
            Return "1003"
        End If
        If r = "KHMER (KEMBOJA)" Then
            Return "1308"
        End If
        If r = "KIMARAGANG" Then
            Return "0929"
        End If
        If r = "KIPUT" Then
            Return "1113"
        End If
        If r = "KWIJAU" Then
            Return "0908"
        End If
        If r = "LAHANAN" Then
            Return "1119"
        End If
        If r = "LASAU" Then
            Return "0912"
        End If
        If r = "LEMANAK" Then
            Return "1118"
        End If
        If r = "LINGKABAU" Then
            Return "0910"
        End If
        If r = "LIRONG" Then
            Return "1117"
        End If
        If r = "LUNDAYEH" Then
            Return "0911"
        End If
        If r = "LUSUM/LUGUM" Then
            Return "1120"
        End If
        If r = "MALOH" Then
            Return "1122"
        End If
        If r = "MANGKAK" Then
            Return "0914"
        End If
        If r = "MATAGANG" Then
            Return "0915"
        End If
        If r = "MATU" Then
            Return "1121"
        End If
        If r = "MELAING" Then
            Return "1124"
        End If
        If r = "MELAYU SARAWAK" Then
            Return "1001"
        End If
        If r = "MELIKIN" Then
            Return "1123"
        End If
        If r = "MENONDO" Then
            Return "1126"
        End If
        If r = "MINOKOK" Then
            Return "0916"
        End If
        If r = "MOMOGUN" Then
            Return "0918"
        End If
        If r = "MURIK" Then
            Return "1125"
        End If
        If r = "MURUT/LUN BAWANG" Then
            Return "1008"
        End If
        If r = "NYAMOK" Then
            Return "1127"
        End If
        If r = "PAITAN" Then
            Return "0919"
        End If
        If r = "PENAN/PUNAN" Then
            Return "1010"
        End If
        If r = "RAMANAU" Then
            Return "0920"
        End If
        If r = "RUNGUS" Then
            Return "0921"
        End If
        If r = "SABAH" Then
            Return "1137"
        End If
        If r = "SARIBAS" Then
            Return "1134"
        End If
        If r = "SEBOB" Then
            Return "1128"
        End If
        If r = "SEBUYAU" Then
            Return "1135"
        End If
        If r = "SEDUAN" Then
            Return "1129"
        End If
        If r = "SEGALANG" Then
            Return "1131"
        End If
        If r = "SEKAPAN" Then
            Return "1130"
        End If
        If r = "SELAKAN" Then
            Return "1138"
        End If
        If r = "SELAKAU" Then
            Return "1139"
        End If
        If r = "SIAMESE (THAILAND)" Then
            Return "1312"
        End If
        If r = "SIAN" Then
            Return "1132"
        End If
        If r = "SINO-NATIVE" Then
            Return "0805"
        End If
        If r = "SINULAHAN" Then
            Return "0924"
        End If
        If r = "SIPENG" Then
            Return "1133"
        End If
        If r = "SKRANG" Then
            Return "1136"
        End If
        If r = "SONSONGAN" Then
            Return "0923"
        End If
        If r = "TABUN" Then
            Return "1141"
        End If
        If r = "TAGAL" Then
            Return "1140"
        End If
        If r = "TAK LOREK" Then
            Return "XXXX"
        End If
        If r = "TAMBANUO" Then
            Return "0925"
        End If
        If r = "TANJONG" Then
            Return "1143"
        End If
        If r = "TATAU" Then
            Return "1144"
        End If
        If r = "TAUP" Then
            Return "1145"
        End If
        If r = "TINAGAS" Then
            Return "0927"
        End If
        If r = "TUTONG" Then
            Return "1142"
        End If
        If r = "UKIT" Then
            Return "1146"
        End If
        If r = "ULU AI" Then
            Return "1148"
        End If
        If r = "UNKOP" Then
            Return "1147"
        End If
        If r = "AFGHAN" Then
            Return "1315"
        End If
        If r = "ALBANIA" Then
            Return "1407"
        End If
        If r = "ALGERIA" Then
            Return "1402"
        End If
        If r = "ANGOLA" Then
            Return "1405"
        End If
        If r = "ANTIGUA-BARBUDA" Then
            Return "1403"
        End If
        If r = "ARGENTINA" Then
            Return "1406"
        End If
        If r = "AUSTRALIA" Then
            Return "1404"
        End If
        If r = "AUSTRIA" Then
            Return "1408"
        End If
        If r = "BAHAMAS" Then
            Return "1413"
        End If
        If r = "BAHRAIN" Then
            Return "1412"
        End If
        If r = "BANGLADESHI" Then
            Return "0400"
        End If
        If r = "BARABADOS" Then
            Return "1414"
        End If
        If r = "BELARUS" Then
            Return "1425"
        End If
        If r = "BELGIUM" Then
            Return "1424"
        End If
        If r = "BELIZE" Then
            Return "1415"
        End If
        If r = "BENIN" Then
            Return "1418"
        End If
        If r = "BHUTAN" Then
            Return "1419"
        End If
        If r = "BOLIVIA" Then
            Return "1420"
        End If
        If r = "BOSNIA-HERZEGOVINA" Then
            Return "1427"
        End If
        If r = "BOTSWANA" Then
            Return "1416"
        End If
        If r = "BOYAN" Then
            Return "0102"
        End If
        If r = "BRAZIL" Then
            Return "1421"
        End If
        If r = "BRITISH" Then
            Return "1401"
        End If
        If r = "BULGARIA" Then
            Return "1423"
        End If
        If r = "BURMESE" Then
            Return "1302"
        End If
        If r = "BURUNDI" Then
            Return "1422"
        End If
        If r = "CAICASIAN" Then
            Return "1316"
        End If
        If r = "CAMEROON" Then
            Return "1428"
        End If
        If r = "CANADA" Then
            Return "1430"
        End If
        If r = "CANTONESE" Then
            Return "0201"
        End If
        If r = "CAPE VERDE" Then
            Return "1432"
        End If
        If r = "CHAD" Then
            Return "1429"
        End If
        If r = "CHILE" Then
            Return "1434"
        End If
        If r = "COLOMBIA" Then
            Return "1435"
        End If
        If r = "COMOROS" Then
            Return "1436"
        End If
        If r = "COSTARICA" Then
            Return "1437"
        End If
        If r = "CROTIA" Then
            Return "1433"
        End If
        If r = "CUBA" Then
            Return "1438"
        End If
        If r = "CYPRUS" Then
            Return "1431"
        End If
        If r = "DAHOMEY" Then
            Return "1441"
        End If
        If r = "DENMARK" Then
            Return "1442"
        End If
        If r = "DJBOUTI" Then
            Return "1439"
        End If
        If r = "DOMINICA" Then
            Return "1440"
        End If
        If r = "EL SALVADOR" Then
            Return "1444"
        End If
        If r = "EQUADOR" Then
            Return "1443"
        End If
        If r = "EQUATORIAL GUINEA" Then
            Return "1445"
        End If
        If r = "ETOPIA" Then
            Return "1446"
        End If
        If r = "EURASIAN" Then
            Return "1303"
        End If
        If r = "FIJIAN" Then
            Return "1304"
        End If
        If r = "FILIPINOS" Then
            Return "1305"
        End If
        If r = "FINLAND" Then
            Return "1449"
        End If
        If r = "FOOCHOW" Then
            Return "0202"
        End If
        If r = "FRANCE" Then
            Return "1448"
        End If
        If r = "GABON" Then
            Return "1451"
        End If
        If r = "GAMBIA" Then
            Return "1452"
        End If
        If r = "GERMANY" Then
            Return "1460"
        End If
        If r = "GHANA" Then
            Return "1455"
        End If
        If r = "GREECE" Then
            Return "1459"
        End If
        If r = "GRENADA" Then
            Return "1456"
        End If
        If r = "GUATEMALA" Then
            Return "1458"
        End If
        If r = "GUINEA" Then
            Return "1453"
        End If
        If r = "GUINEA-BISSAU" Then
            Return "1454"
        End If
        If r = "GURKHA" Then
            Return "1306"
        End If
        If r = "GUYANA" Then
            Return "1457"
        End If
        If r = "HAINANESE" Then
            Return "0203"
        End If
        If r = "HAITI" Then
            Return "1461"
        End If
        If r = "HENGHUA" Then
            Return "0204"
        End If
        If r = "HOKCHIA" Then
            Return "0205"
        End If
        If r = "HOKCHIU" Then
            Return "0206"
        End If
        If r = "HOKKIEN" Then
            Return "0207"
        End If
        If r = "HONDURAS" Then
            Return "1462"
        End If
        If r = "HONG KONG" Then
            Return "1464"
        End If
        If r = "HUNGARY" Then
            Return "1463"
        End If
        If r = "ICELAND" Then
            Return "1471"
        End If
        If r = "IRANIAN" Then
            Return "1314"
        End If
        If r = "IRAQ" Then
            Return "1466"
        End If
        If r = "IRELAND" Then
            Return "1469"
        End If
        If r = "ISRAEL" Then
            Return "1468"
        End If
        If r = "ITALY" Then
            Return "1470"
        End If
        If r = "IVORY COAST" Then
            Return "1467"
        End If
        If r = "JAKUN" Then
            Return "1201"
        End If
        If r = "JAMAICA" Then
            Return "1473"
        End If
        If r = "JAPANESE" Then
            Return "1307"
        End If
        If r = "JAWI PEKAN" Then
            Return "0105"
        End If
        If r = "JORDAN" Then
            Return "1472"
        End If
        If r = "KENYA" Then
            Return "1476"
        End If
        If r = "KHEK(HAIKA)" Then
            Return "0208"
        End If
        If r = "KIRIBATI" Then
            Return "1477"
        End If
        If r = "KOREA(UTARA)" Then
            Return "1479"
        End If
        If r = "KOREAN" Then
            Return "1309"
        End If
        If r = "KUWAIT" Then
            Return "1475"
        End If
        If r = "KWONGSAI" Then
            Return "0209"
        End If
        If r = "KYRGYZ" Then
            Return "1317"
        End If
        If r = "LAIN-LAIN ASIA" Then
            Return "1300"
        End If
        If r = "LAOS" Then
            Return "1484"
        End If
        If r = "LEBANON" Then
            Return "1481"
        End If
        If r = "LESOTHO" Then
            Return "1483"
        End If
        If r = "LIBERIA" Then
            Return "1485"
        End If
        If r = "LIBYA" Then
            Return "1482"
        End If
        If r = "LUXEMBOURG" Then
            Return "1486"
        End If
        If r = "MACEDONIA" Then
            Return "1561"
        End If
        If r = "MADAGASCAR" Then
            Return "1493"
        End If
        If r = "MALABARI" Then
            Return "0306"
        End If
        If r = "ARAB" Then
            Return "1301"
        End If
        If r = "BAJAU" Then
            Return "2"
        End If
        If r = "BANJAR" Then
            Return "3"
        End If
        If r = "MALAWI" Then
            Return "1491"
        End If
        If r = "MALAY ALI" Then
            Return "0301"
        End If
        If r = "MALDIVES" Then
            Return "1488"
        End If
        If r = "MALI" Then
            Return "1487"
        End If
        If r = "MALTESE" Then
            Return "1310"
        End If
        If r = "MAURITANIA" Then
            Return "1489"
        End If
        If r = "MAURITIUS" Then
            Return "1494"
        End If
        If r = "MELAYU SABAH" Then
            Return "0917"
        End If
        If r = "MELAYU SRI LANGKA" Then
            Return "0602"
        End If
        If r = "MESIR" Then
            Return "1498"
        End If
        If r = "MEXICO" Then
            Return "1495"
        End If
        If r = "MIDDLE AFRICA" Then
            Return "1410"
        End If
        If r = "MINANGKABAU" Then
            Return "0106"
        End If
        If r = "MONGOLIA" Then
            Return "1497"
        End If
        If r = "MOROCCO" Then
            Return "1490"
        End If
        If r = "MOZAMBIQUE" Then
            Return "1496"
        End If
        If r = "MYANMAR" Then
            Return "1499"
        End If
        If r = "NAMIBIA" Then
            Return "1501"
        End If
        If r = "NAURU" Then
            Return "1502"
        End If
        If r = "NEGRITO" Then
            Return "1202"
        End If
        If r = "NEPAL" Then
            Return "1505"
        End If
        If r = "NETHERLAND" Then
            Return "1507"
        End If
        If r = "NEW ZEALAND" Then
            Return "1503"
        End If
        If r = "NICARAGUA" Then
            Return "1506"
        End If
        If r = "NIGERIA" Then
            Return "1504"
        End If
        If r = "NORWAY" Then
            Return "1508"
        End If
        If r = "OMAN" Then
            Return "1509"
        End If
        If r = "PAKISTANI" Then
            Return "0500"
        End If
        If r = "PALESTIN" Then
            Return "1511"
        End If
        If r = "PANAMA" Then
            Return "1512"
        End If
        If r = "PAPUA NEW GUINEA" Then
            Return "1510"
        End If
        If r = "PARAGUAY" Then
            Return "1513"
        End If
        If r = "PENAN" Then
            Return "1208"
        End If
        If r = "PERU" Then
            Return "1514"
        End If
        If r = "POLAND" Then
            Return "1515"
        End If
        If r = "PORTUGESE" Then
            Return "1311"
        End If
        If r = "PUNJABI" Then
            Return "0302"
        End If
        If r = "QATAR" Then
            Return "1517"
        End If
        If r = "REPUBLIC CZECH" Then
            Return "1520"
        End If
        If r = "REPUBLIC SLOVAKIA" Then
            Return "1521"
        End If
        If r = "ROMANIA" Then
            Return "1518"
        End If
        If r = "RUSSIA" Then
            Return "1553"
        End If
        If r = "RWANDA" Then
            Return "1519"
        End If
        If r = "SAKAI" Then
            Return "1203"
        End If
        If r = "SAMAO BARAT" Then
            Return "1536"
        End If
        If r = "SAMOA" Then
            Return "1533"
        End If
        If r = "SAO TOME & PRINCIPE" Then
            Return "1534"
        End If
        If r = "SEMAI" Then
            Return "1204"
        End If
        If r = "SEMALAI" Then
            Return "1205"
        End If
        If r = "SENEGAL" Then
            Return "1522"
        End If
        If r = "SENOI" Then
            Return "1207"
        End If
        If r = "SIERRA LEONE" Then
            Return "1523"
        End If
        If r = "SIKH" Then
            Return "0303"
        End If
        If r = "SINHALESE" Then
            Return "0603"
        End If
        If r = "SOLOMON ISLAND" Then
            Return "1530"
        End If
        If r = "SOMALIA" Then
            Return "1524"
        End If
        If r = "SOUTH AFRICA" Then
            Return "1411"
        End If
        If r = "SPAIN" Then
            Return "1538"
        End If
        If r = "SRI LANKA" Then
            Return "1531"
        End If
        If r = "ST VINCENT" Then
            Return "1528"
        End If
        If r = "ST.LUCIA" Then
            Return "1527"
        End If
        If r = "SUDAN" Then
            Return "1525"
        End If
        If r = "SURINAM" Then
            Return "1535"
        End If
        If r = "SWAZILAND" Then
            Return "1532"
        End If
        If r = "SWEDEN" Then
            Return "1537"
        End If
        If r = "SWITZERLAND" Then
            Return "1539"
        End If
        If r = "SYCHELLES" Then
            Return "1529 "
        End If
        If r = "SYRIA" Then
            Return "1526"
        End If
        If r = "TAIWAN" Then
            Return "1548"
        End If
        If r = "TAMIL" Then
            Return "0304"
        End If
        If r = "TAMIL SRI LANGKA" Then
            Return "0601"
        End If
        If r = "TANZANIA" Then
            Return "1542"
        End If
        If r = "TELEGU" Then
            Return "0305"
        End If
        If r = "TEMIAR" Then
            Return "1206"
        End If
        If r = "TEOCHEW" Then
            Return "0210"
        End If
        If r = "TIADA MAKLUMAT" Then
            Return "9999"
        End If
        If r = "TIDUNG" Then
            Return "0701"
        End If
        If r = "TOGO" Then
            Return "1547"
        End If
        If r = "TONGA" Then
            Return "1543"
        End If
        If r = "TRINIDAD&TOBACO" Then
            Return "1544"
        End If
        If r = "TUNISIA" Then
            Return "1540"
        End If
        If r = "TURKEY" Then
            Return "1541"
        End If
        If r = "TUVALI" Then
            Return "1545"
        End If
        If r = "BUGIS" Then
            Return "4"
        End If
        If r = "BRUNEI" Then
            Return "5"
        End If
        If r = "CINA" Then
            Return "0200"
        End If
        If r = "BIDAYUH" Then
            Return "7"
        End If
        If r = "KELABIT" Then
            Return "8"
        End If
        If r = "DUSUN" Then
            Return "9"
        End If
        If r = "JAWA" Then
            Return "10"
        End If
        If r = "IBAN (DAYAK LAUT)" Then
            Return "11"
        End If
        If r = "ILLANUM" Then
            Return "12"
        End If
        If r = "INDIA" Then
            Return "0300"
        End If
        If r = "UBIAN" Then
            Return "1318"
        End If
        If r = "UGANDA" Then
            Return "1549"
        End If
        If r = "UKRAINE" Then
            Return "1554"
        End If
        If r = "UNITED ARAB EMIRATES" Then
            Return "1550"
        End If
        If r = "UNITED STATES" Then
            Return "1555"
        End If
        If r = "UPPER VOLTA" Then
            Return "1551"
        End If
        If r = "URUGUAY" Then
            Return "1552"
        End If
        If r = "VANUATU" Then
            Return "1556"
        End If
        If r = "VENEZUELA" Then
            Return "1557"
        End If
        If r = "VIETNAMESE" Then
            Return "1313"
        End If
        If r = "YEMEN" Then
            Return "1559"
        End If
        If r = "YUGOSLAVIA" Then
            Return "1560"
        End If
        If r = "ZAIRE" Then
            Return "1562"
        End If
        If r = "ZAMBIA" Then
            Return "1563"
        End If
        If r = "ZIMBABWE" Then
            Return "1564"
        End If
        Return ""
    End Function

    Public Function toStateCode(ByVal code As String) As String
        Dim map As Hashtable = New Hashtable()
        map.Add("01", "01")
        map.Add("21", "01")
        map.Add("22", "01")
        map.Add("23", "01")
        map.Add("24", "01")

        map.Add("02", "02")
        map.Add("25", "02")
        map.Add("26", "02")
        map.Add("27", "02")

        map.Add("03", "03")
        map.Add("28", "03")
        map.Add("29", "03")

        map.Add("04", "04")
        map.Add("30", "04")

        map.Add("05", "05")
        map.Add("31", "05")
        map.Add("59", "05")

        map.Add("06", "06")
        map.Add("32", "06")
        map.Add("33", "06")

        map.Add("07", "07")
        map.Add("34", "07")
        map.Add("35", "07")

        map.Add("08", "08")
        map.Add("36", "08")
        map.Add("37", "08")
        map.Add("38", "08")
        map.Add("39", "08")

        map.Add("09", "09")
        map.Add("40", "09")

        map.Add("10", "10")
        map.Add("41", "10")
        map.Add("42", "10")
        map.Add("43", "10")
        map.Add("44", "10")

        map.Add("11", "11")
        map.Add("45", "11")
        map.Add("46", "11")

        map.Add("12", "12")
        map.Add("47", "12")
        map.Add("48", "12")
        map.Add("49", "12")

        map.Add("13", "13")
        map.Add("50", "13")
        map.Add("51", "13")
        map.Add("52", "13")
        map.Add("53", "13")

        map.Add("14", "14")
        map.Add("54", "14")
        map.Add("55", "14")
        map.Add("56", "14")
        map.Add("57", "14")

        map.Add("15", "15")
        map.Add("58", "15")

        map.Add("16", "16")

        map.Add("60", "17")

        map.Add("67", "18")
        'missing 19, because 19 is the last option
        map.Add("66", "20")
        map.Add("61", "21")
        Dim stateCode As String = map(code)
        '19 = lain lain
        If (stateCode = Nothing) Then
            Return "19"
        Else
            Return stateCode
        End If
    End Function
    Private Function bcdBytetoHex(ByVal b As Byte) As String
        Dim zero As Char = CChar("0")
        Return Hex(b).PadLeft(2, zero)
    End Function
    Public Function toPostcodeString(ByVal content As Byte()) As String
        Dim last2digits As String = bcdBytetoHex(content(2))
        Dim lastDigit As String = last2digits.Substring(1, 1)
        Return bcdBytetoHex(content(0)) + bcdBytetoHex(content(1)) + lastDigit
    End Function




    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Button2_Click(sender, e)
        Dim result As Integer
        Dim sb As System.Text.StringBuilder
        Dim cchReaders As UInteger
        Dim szReader As String
        Dim protocol As UInteger

        Dim stopwatch As Stopwatch = New Stopwatch()
        stopwatch.Start()

        sb = New System.Text.StringBuilder()
        result = SCardEstablishContext(SCARD_SCOPE_USER, IntPtr.Zero, IntPtr.Zero, hContext)
        If (result <> 0) Then
            MsgBox("Error: SCardEstablishContext")
            Return
        End If
        cchReaders = 256

        result = SCardListReadersW(hContext, Nothing, sb, cchReaders)
        If (result <> 0) Then
            MsgBox("Error: Unable to find a smart card reader", MsgBoxStyle.Critical)
            cleanUp()
            Return
        End If

        szReader = sb.ToString()

        result = SCardConnectW(hContext, szReader, SCARD_SHARE_SHARED, SCARD_PROTOCOL_T0, hCard, protocol)
        If (result <> 0) Then
            MsgBox("Error: SCardConnectW")
            Return
        End If

        ioSendPci.cbPciLength = 8
        ioSendPci.dwProtocol = protocol
        ioRecvPci.cbPciLength = 8
        ioRecvPci.dwProtocol = protocol

        Dim CmdSelectAppJPN As Byte() = {&H0, &HA4, &H4, &H0, &HA, &HA0, &H0, &H0, &H0, &H74, &H4A, &H50, &H4E, &H0, &H10}
        Dim receiveBuffer(262) As Byte
        Dim receiveBufferLength As UInteger
        receiveBufferLength = 2
        result = SCardTransmit(hCard, ioSendPci, CmdSelectAppJPN(0), CUInt(CmdSelectAppJPN.Length), ioRecvPci, receiveBuffer(0), receiveBufferLength)
        If (result <> 0) Then
            MsgBox("Error: Select JPN Application")
            cleanUp()
            Return
        End If

        Dim CmdAppResponse As Byte() = {&H0, &HC0, &H0, &H0, &H5}
        receiveBufferLength = 7
        result = SCardTransmit(hCard, ioSendPci, CmdAppResponse(0), CUInt(CmdAppResponse.Length), ioRecvPci, receiveBuffer(0), receiveBufferLength)
        If (result <> 0) Then
            MsgBox("Error: JPN Response")
            cleanUp()
            Return
        End If

        Dim fileContent1() As Byte = readFile1()
        If (fileContent1 Is Nothing) Then
            MsgBox("Error: Empty Content File 1")
            cleanUp()
            Return
        End If

        Dim birthdateByte(4 - 1) As Byte
        Array.Copy(fileContent1, &H127, birthdateByte, 0, 4)

        Dim dateIssuedByte(4 - 1) As Byte
        Array.Copy(fileContent1, &H127, dateIssuedByte, 0, 4)

        Dim originalName As String = System.Text.Encoding.ASCII.GetString(fileContent1, &H3, &H96).Trim()
        Dim fullName As String = System.Text.Encoding.ASCII.GetString(fileContent1, &HE9, &H28).Trim()
        Dim id As String = System.Text.Encoding.ASCII.GetString(fileContent1, &H111, &HD).Trim()
        Dim gender As String = System.Text.Encoding.ASCII.GetString(fileContent1, &H11E, 1).Trim()
        Dim icOld As String = System.Text.Encoding.ASCII.GetString(fileContent1, &H11F, &H8).Trim()
        Dim birthdate As String = bcdDateToString(birthdateByte)
        Dim birthplace As String = System.Text.Encoding.ASCII.GetString(fileContent1, &H12B, &H19).Trim()
        Dim issuedDate As String = bcdDateToString(dateIssuedByte)
        Dim citizenship As String = System.Text.Encoding.ASCII.GetString(fileContent1, &H148, &H12).Trim()
        Dim race As String = System.Text.Encoding.ASCII.GetString(fileContent1, &H15A, &H19).Trim().ToUpper
        Dim religion As String = System.Text.Encoding.ASCII.GetString(fileContent1, &H173, &HB).Trim().ToUpper

        textId.Text = id
        textName.Text = originalName
        textReligion.Text = religion
        textRace.Text = race
        textDOB.Text = birthdate
        textGender.Text = gender
        textNationality.Text = citizenship

        Me.Refresh()

        Dim fileContent4() As Byte = readFile4()
        If (fileContent4 Is Nothing) Then
            MsgBox("Error: Empty Content File 4")
            cleanUp()
            Return
        End If

        Dim postCodeByte(3 - 1) As Byte
        Array.Copy(fileContent4, &H5D, postCodeByte, 0, 3)
        Dim address1 As String = System.Text.Encoding.ASCII.GetString(fileContent4, &H3, &H1E).Trim()
        Dim address2 As String = System.Text.Encoding.ASCII.GetString(fileContent4, &H21, &H1E).Trim()
        Dim address3 As String = System.Text.Encoding.ASCII.GetString(fileContent4, &H3F, &H1E).Trim()
        Dim postcode As String = toPostcodeString(postCodeByte)
        Dim city As String = System.Text.Encoding.ASCII.GetString(fileContent4, &H60, &H19).Trim()
        Dim state As String = System.Text.Encoding.ASCII.GetString(fileContent4, &H79, &H1E).Trim()
        Dim address As String
        Dim stateCode As String = id.Substring(6, 2)

        address = address1 + ControlChars.CrLf + address2 + ControlChars.CrLf + address3 + ControlChars.CrLf + postcode + " " + city + ControlChars.CrLf + city
        textAddress.Text = address


        Dim pictureContent() As Byte = Nothing
        'ToolStripStatusLabel1.Text = "Reading Image..."
        'Me.Refresh()
        'pictureContent = loadImage()
        'ToolStripStatusLabel1.Text = ""
        'If (pictureContent Is Nothing) Then
        'MsgBox("Error: Empty Content Image")
        'cleanUp()
        'Return
        'End If

        insertIntoDb(id, fullName, originalName, decodeCitizenship(citizenship), decodeRace(race), decodeReligion(religion), gender, birthdate, icOld, birthplace, address1, address2, address3, postcode, city, toStateCode(stateCode))

    End Sub
    Private Sub cleanUp()
        SCardReleaseContext(hContext)
    End Sub
    Private Sub insertIntoDb(ByVal id As String, ByVal name As String, ByVal originalName As String, ByVal citizenship As String, ByVal race As String, ByVal religion As String, ByVal gender As String, ByVal dob As String, ByRef icOld As String, ByVal placeOfBirth As String, ByVal a1 As String, ByVal a2 As String, ByVal a3 As String, ByVal postcode As String, ByVal city As String, ByVal stateCode As String)
        'Dim connectionString As String = "Dsn=PostgreSQL35W;database=reg;server=localhost;port=5432;uid=uitm;sslmode=disable;readonly=0;protocol=7.4;fakeoidindex=0;showoidcolumn=0;rowversioning=0;showsystemtables=0;fetch=100;socket=4096;unknownsizes=0;maxvarcharsize=255;maxlongvarcharsize=8190;debug=0;commlog=0;optimizer=0;ksqo=1;usedeclarefetch=0;textaslongvarchar=1;unknownsaslongvarchar=0;boolsaschar=1;parse=0;cancelasfreestmt=0;extrasystableprefixes=dd_;lfconversion=1;updatablecursors=1;disallowpremature=0;trueisminus1=0;bi=0;byteaaslongvarbinary=0;useserversideprepare=0;lowercaseidentifier=0;xaopt=1"
        'Dim connectionString As String = "Dsn=mysql-reg-system"
        'Dim connectionString As String = "Dsn=INTEGRASI_INTAKE;Uid=MYKAD;Pwd=janganhilang"
        Dim connectionString As String = "Dsn=oracle-intake;Uid=INTEGRASI_INTAKE;Pwd=1234;"


        Dim sql As String = "insert into INTAKE.INTEGRATION_MYCARD (INM_NEWICNUM, INM_OLDICNUM, INM_ORIGINALFULLNAME, INM_FULLNAME, INM_GENDER, INM_RELIGIONCODE, INM_CITIZENSHIPCODE, INM_RACECODE, INM_DOB, INM_PLACEOFBIRTH, INM_ADDRESS1, INM_ADDRESS2, INM_ADDRESS3, INM_POSTCODE, INM_CITY, INM_STATECODE) values (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
        Dim c As Odbc.OdbcConnection = New Odbc.OdbcConnection(connectionString)

        Dim cmd As Odbc.OdbcCommand = New Odbc.OdbcCommand(sql, c)

        cmd.Parameters.Add(New Odbc.OdbcParameter("ICNEW", Odbc.OdbcType.VarChar, 12)).Value = id
        cmd.Parameters.Add(New Odbc.OdbcParameter("ICOLD", Odbc.OdbcType.VarChar, 12)).Value = icOld
        cmd.Parameters.Add(New Odbc.OdbcParameter("ORIGINALFULLNAME", Odbc.OdbcType.VarChar, 80)).Value = originalName
        cmd.Parameters.Add(New Odbc.OdbcParameter("FULLNAME", Odbc.OdbcType.VarChar, 80)).Value = name
        cmd.Parameters.Add(New Odbc.OdbcParameter("GENDER", Odbc.OdbcType.VarChar, 10)).Value = gender
        cmd.Parameters.Add(New Odbc.OdbcParameter("RELIGIONCODE", Odbc.OdbcType.VarChar, 10)).Value = religion
        cmd.Parameters.Add(New Odbc.OdbcParameter("CITIZENSHIPCODE", Odbc.OdbcType.VarChar, 10)).Value = citizenship
        cmd.Parameters.Add(New Odbc.OdbcParameter("RACECODE", Odbc.OdbcType.VarChar, 10)).Value = race
        cmd.Parameters.Add(New Odbc.OdbcParameter("DOB", Odbc.OdbcType.VarChar, 10)).Value = dob
        cmd.Parameters.Add(New Odbc.OdbcParameter("PLACEOFBIRTH", Odbc.OdbcType.VarChar, 25)).Value = placeOfBirth
        cmd.Parameters.Add(New Odbc.OdbcParameter("ADDRESS1", Odbc.OdbcType.VarChar, 40)).Value = a1
        cmd.Parameters.Add(New Odbc.OdbcParameter("ADDRESS2", Odbc.OdbcType.VarChar, 40)).Value = a2
        cmd.Parameters.Add(New Odbc.OdbcParameter("ADDRESS3", Odbc.OdbcType.VarChar, 40)).Value = a3
        cmd.Parameters.Add(New Odbc.OdbcParameter("POSTCODE", Odbc.OdbcType.VarChar, 5)).Value = postcode
        cmd.Parameters.Add(New Odbc.OdbcParameter("CITY", Odbc.OdbcType.VarChar, 30)).Value = city
        cmd.Parameters.Add(New Odbc.OdbcParameter("STATECODE", Odbc.OdbcType.VarChar, 10)).Value = stateCode

        Try
            c.Open()
            cmd.ExecuteNonQuery()
        Catch e As Exception
            MsgBox("Duplicate" + e.ToString())
        Finally
            cmd.Dispose()
            c.Close()
        End Try
        MsgBox("Your information has been successfully saved.", MsgBoxStyle.Information, "Information")
    End Sub
    Private Function bcdNumberToString(ByVal bcd As Byte()) As String
        Dim result As String = ""
        Dim n As Integer = bcd.Length
        For i As Integer = 0 To n - 1
            result = result + Hex(bcd(i))
        Next
        Return result
    End Function
    Private Function bcdDateToString(ByVal bcdDate As Byte()) As String
        Dim result As String
        result = Hex(bcdDate(3)) + "/" + Hex(bcdDate(2)) + "/" + Hex(bcdDate(0)) + Hex(bcdDate(1))
        Return result
    End Function
    Private Function loadImage()
        Dim pictureContent() As Byte = readFile2()
        Dim icPicture As Image = Drawing.Image.FromStream(New IO.MemoryStream(pictureContent))
        'PictureBox1.Image = icPicture
        Return pictureContent
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

    Public Function readFile1() As Byte()
        Const LENGTH_1 As Integer = 252
        Const LENGTH_2 As Integer = 207
        Dim content1 As Byte() = readSegment(1, 0, LENGTH_1)
        If (content1 Is Nothing) Then
            Return Nothing
        End If

        Dim content2 As Byte() = readSegment(1, LENGTH_1, LENGTH_2)
        If (content2 Is Nothing) Then
            Return Nothing
        End If
        Dim content(LENGTH_1 + LENGTH_2 - 1) As Byte
        Array.Copy(content1, 0, content, 0, LENGTH_1)
        Array.Copy(content2, 0, content, LENGTH_1, LENGTH_2)
        Return content
    End Function
    Public Function readFile4() As Byte()
        Const LENGTH As Integer = 171
        Dim content As Byte() = readSegment(4, 0, LENGTH)
        Return content
    End Function

    Public Function readFile2() As Byte()
        Const LIMIT As Integer = 4011
        Const IMAGE_OFFSET As Integer = 3
        Const IMAGE_LENGTH As Integer = 4000
        Const LENGTH As Integer = 252
        Dim content(LIMIT - 1) As Byte
        Dim contentLength As Integer = 0
        Dim readCount As Integer = 0
        While contentLength < LIMIT
            Dim blockSize As Integer
            Dim unread As Integer = LIMIT - contentLength
            If unread > LENGTH Then
                blockSize = LENGTH
            Else
                blockSize = unread
            End If
            Dim blockContent As Byte() = readSegment(2, contentLength, blockSize)
            readCount = readCount + 1
            'MsgBox("readCount = " + CStr(readCount) + "readSegment contentLength: " + CStr(contentLength) + "/" + CStr(blockSize))
            If (blockContent Is Nothing) Then
                Return Nothing
            End If
            Array.Copy(blockContent, 0, content, contentLength, blockSize)
            contentLength = contentLength + blockContent.Length()
        End While
        'MsgBox("Read Count : " + CStr(readCount))
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

    Private Sub BindingSource1_CurrentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        countExam()
    End Sub
    Private Sub countExam()
        'Dim connectionString As String = "Dsn=PostgreSQL35W;database=reg;server=localhost;port=5432;uid=uitm;sslmode=disable;readonly=0;protocol=7.4;fakeoidindex=0;showoidcolumn=0;rowversioning=0;showsystemtables=0;fetch=100;socket=4096;unknownsizes=0;maxvarcharsize=255;maxlongvarcharsize=8190;debug=0;commlog=0;optimizer=0;ksqo=1;usedeclarefetch=0;textaslongvarchar=1;unknownsaslongvarchar=0;boolsaschar=1;parse=0;cancelasfreestmt=0;extrasystableprefixes=dd_;lfconversion=1;updatablecursors=1;disallowpremature=0;trueisminus1=0;bi=0;byteaaslongvarbinary=0;useserversideprepare=0;lowercaseidentifier=0;xaopt=1"
        'Dim connectionString As String = "Dsn=mysql-reg-system"
        Dim connectionString As String = "Dsn=oracle-intake;Uid=INTEGRASI_INTAKE;Pwd=1234;"
        Dim sql As String = "select count(*) from INTAKE.EXAM"
        Dim c As Odbc.OdbcConnection = New Odbc.OdbcConnection(connectionString)
        Dim cmd As Odbc.OdbcCommand = New Odbc.OdbcCommand(sql, c)
        Try
            c.Open()
            Dim r As Odbc.OdbcDataReader = cmd.ExecuteReader()
            If (r.Read()) Then
                MsgBox(r.GetString(0))
            Else
                MsgBox("Unable to read")
            End If
            r.Close()
        Catch e As Exception
            MsgBox("Duplicate" + e.ToString())
        Finally
            cmd.Dispose()
            c.Close()
        End Try
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        checkOkSimpleConnection()
    End Sub
    Private Sub checkOkSimpleConnection()
        Dim connectionString As String = "Dsn=oracle-intake;Uid=INTEGRASI_INTAKE;Pwd=1234;"
        Dim sql As String = "select SYSDATE from DUAL"
        Dim c As Odbc.OdbcConnection = New Odbc.OdbcConnection(connectionString)
        Dim cmd As Odbc.OdbcCommand = New Odbc.OdbcCommand(sql, c)
        Dim message As String = ""
        Try
            c.Open()
            Dim r As Odbc.OdbcDataReader = cmd.ExecuteReader()
            If (r.Read()) Then
                r.GetString(0)
                textboxInfo.ForeColor = Color.Green
                textboxInfo.Text = "Connection to database: OK"
                Return
            End If
            r.Close()
        Catch e As Exception
            message = e.Message
        Finally
            cmd.Dispose()
            c.Close()
        End Try
        textboxInfo.ForeColor = Color.Red
        textboxInfo.Text = message
    End Sub


End Class
