using System;
using System.Collections.Generic;
using System.Text;

namespace myKad
{
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct SCARD_IO_REQUEST
    {

        /// DWORD->unsigned int
        public uint dwProtocol;

        /// DWORD->unsigned int
        public uint cbPciLength;
    }

    public class Reader
    {

        /// SCARD_SCOPE_USER -> 0
        private const int SCARD_SCOPE_USER = 0;

        /// SCARD_SCOPE_TERMINAL -> 1
        private const int SCARD_SCOPE_TERMINAL = 1;

        /// SCARD_SCOPE_SYSTEM -> 2
        private const int SCARD_SCOPE_SYSTEM = 2;


        /// SCARD_SHARE_EXCLUSIVE -> 1
        private const int SCARD_SHARE_EXCLUSIVE = 1;

        /// SCARD_SHARE_SHARED -> 2
        private const int SCARD_SHARE_SHARED = 2;

        /// SCARD_SHARE_DIRECT -> 3
        private const int SCARD_SHARE_DIRECT = 3;


        /// SCARD_PROTOCOL_UNDEFINED -> 0x00000000
        private const int SCARD_PROTOCOL_UNDEFINED = 0;

        /// SCARD_PROTOCOL_OPTIMAL -> 0x00000000
        private const int SCARD_PROTOCOL_OPTIMAL = 0;

        /// SCARD_PROTOCOL_DEFAULT -> 0x80000000
        private const int SCARD_PROTOCOL_DEFAULT = -2147483648;

        /// SCARD_PROTOCOL_RAW -> 0x00010000
        private const int SCARD_PROTOCOL_RAW = 65536;

        /// SCARD_PROTOCOL_Tx -> (SCARD_PROTOCOL_T0 | SCARD_PROTOCOL_T1)
        private const int SCARD_PROTOCOL_Tx = (SCARD_PROTOCOL_T0 | SCARD_PROTOCOL_T1);

        /// SCARD_PROTOCOL_T1 -> 0x00000002
        private const int SCARD_PROTOCOL_T1 = 2;

        /// SCARD_PROTOCOL_T0 -> 0x00000001
        private const int SCARD_PROTOCOL_T0 = 1;


        /// Return Type: LONG->int
        ///dwScope: DWORD->unsigned int
        ///pvReserved1: LPCVOID->void*
        ///pvReserved2: LPCVOID->void*
        ///phContext: LPSCARDCONTEXT->SCARDCONTEXT*
        [System.Runtime.InteropServices.DllImportAttribute("winscard.dll", EntryPoint = "SCardEstablishContext")]
        private static extern int SCardEstablishContext(uint dwScope, System.IntPtr pvReserved1, System.IntPtr pvReserved2, ref uint phContext);

        /// Return Type: LONG->int
        ///hContext: SCARDCONTEXT->ULONG_PTR->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("winscard.dll", EntryPoint = "SCardReleaseContext")]
        private static extern int SCardReleaseContext(uint hContext);

        /// Return Type: LONG->int
        ///hContext: SCARDCONTEXT->ULONG_PTR->unsigned int
        ///mszGroups: LPCWSTR->WCHAR*
        ///mszReaders: LPWSTR->WCHAR*
        ///pcchReaders: LPDWORD->DWORD*
        [System.Runtime.InteropServices.DllImportAttribute("winscard.dll", EntryPoint = "SCardListReadersW")]
        private static extern int SCardListReadersW(uint hContext, [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)] string mszGroups, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)] System.Text.StringBuilder mszReaders, ref uint pcchReaders);

        /// Return Type: LONG->int
        ///hContext: SCARDCONTEXT->ULONG_PTR->unsigned int
        ///szReader: LPCWSTR->WCHAR*
        ///dwShareMode: DWORD->unsigned int
        ///dwPreferredProtocols: DWORD->unsigned int
        ///phCard: LPSCARDHANDLE->SCARDHANDLE*
        ///pdwActiveProtocol: LPDWORD->DWORD*
        [System.Runtime.InteropServices.DllImportAttribute("winscard.dll", EntryPoint = "SCardConnectW")]
        private static extern int SCardConnectW(uint hContext, [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)] string szReader, uint dwShareMode, uint dwPreferredProtocols, ref uint phCard, ref uint pdwActiveProtocol);

        /// Return Type: LONG->int
        ///hCard: SCARDHANDLE->ULONG_PTR->unsigned int
        ///pioSendPci: LPCSCARD_IO_REQUEST->SCARD_IO_REQUEST*
        ///pbSendBuffer: LPCBYTE->BYTE*
        ///cbSendLength: DWORD->unsigned int
        ///pioRecvPci: LPSCARD_IO_REQUEST->_SCARD_IO_REQUEST*
        ///pbRecvBuffer: LPBYTE->BYTE*
        ///pcbRecvLength: LPDWORD->DWORD*
        [System.Runtime.InteropServices.DllImportAttribute("winscard.dll", EntryPoint = "SCardTransmit")]
        public static extern int SCardTransmit(uint hCard, ref SCARD_IO_REQUEST pioSendPci, ref byte pbSendBuffer, uint cbSendLength, ref SCARD_IO_REQUEST pioRecvPci, ref byte pbRecvBuffer, ref uint pcbRecvLength);

        private  byte[] CmdSelectAppJPN = { 0x00, 0xA4, 0x04, 0x00, 0x0A, 0x0A0, 0x00, 0x00, 0x00, 0x74, 0x4A, 0x50, 0x4E, 0x00, 0x10 };
        private  byte[] CmdAppResponse = { 0x00, 0xC0, 0x00, 0x00, 0x05 };


        private uint hContext;
        private uint hCard;
        private uint dwActiveProtocol;
        private SCARD_IO_REQUEST ioSendPci;
        private SCARD_IO_REQUEST ioRecvPci;

        private const byte SPLIT_LENGTH = 252;
        private const int FILE_LENGTH_1 = 459;
        private byte[] contentFile1 = new byte[FILE_LENGTH_1];

        public int init()
        {
            int errorCode;
            errorCode = SCardEstablishContext(SCARD_SCOPE_USER, IntPtr.Zero, IntPtr.Zero, ref hContext);
            if (errorCode != 0)
            {
                return errorCode;
            }

            StringBuilder sb = new StringBuilder();
            uint cchReaders = 256;
            errorCode = SCardListReadersW(hContext, null, sb, ref cchReaders);
            if (errorCode != 0)
            {
                return errorCode;
            }
            String readerName = sb.ToString();

            errorCode = SCardConnectW(hContext, readerName, SCARD_SHARE_SHARED, SCARD_PROTOCOL_T0, ref hCard, ref dwActiveProtocol);
            if (errorCode != 0)
            {
                return errorCode;
            }

            ioSendPci.dwProtocol = dwActiveProtocol;
            ioSendPci.cbPciLength = 8;


            ioRecvPci.dwProtocol = dwActiveProtocol;
            ioRecvPci.cbPciLength = 8;

            byte[] sendBuffer;
            byte[] recvBuffer = new byte[262];
            uint cbRecvLength;

            sendBuffer = CmdSelectAppJPN;
            cbRecvLength = 2;
            errorCode = SCardTransmit(hCard, ref ioSendPci, ref sendBuffer[0], (uint)sendBuffer.Length, ref ioRecvPci, ref recvBuffer[0], ref cbRecvLength);
            if (errorCode != 0)
            {
                return errorCode;
            }
            if (!(recvBuffer[0] == 0x61 && recvBuffer[1] == 0x05))
            {
                return -1;
            }

            sendBuffer = CmdAppResponse;
            cbRecvLength = 256;
            errorCode = SCardTransmit(hCard, ref ioSendPci, ref sendBuffer[0], (uint)sendBuffer.Length, ref ioRecvPci, ref recvBuffer[0], ref cbRecvLength);
            if (errorCode != 0)
            {
                return errorCode;
            }

            return 0;
        }

        private byte[] createLengthRequestBuffer(byte length)
        {
            byte[] CmdSetLength = {0xC8, 0x32, 0x00, 0x00, 0x05, 0x08, 0x00, 0x00};
            byte[] buffer = new byte[CmdSetLength.Length + 1];
            for (int i = 0; i < CmdSetLength.Length; i++)
            {
                buffer[i] = CmdSetLength[i];
            }
            buffer[CmdSetLength.Length] = length;
            return buffer;
        }
        public int readFile1()
        {
            byte[] request = createLengthRequestBuffer(SPLIT_LENGTH);
            int errorCode;
            byte[] recvBuffer = new byte[262];
            uint cbRecvLength = 256;
            errorCode = SCardTransmit(hCard, ref ioSendPci, ref request[0], (uint)request.Length, ref ioRecvPci, ref recvBuffer[0], ref cbRecvLength);

            return errorCode;
        }

        public void cleanUp()
        {
            int errorCode;
            errorCode = SCardReleaseContext(hContext);
            System.Console.WriteLine("SCardReleaseContext(): " + errorCode);
        }
    }
}
