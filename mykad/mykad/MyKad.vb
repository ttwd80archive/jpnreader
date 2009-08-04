
Option Strict On

Imports System.Runtime.InteropServices
Imports System.Text

Public Class MyKad
    Private hContext As UInteger
    Private phCard As UInteger
    Private activeProtocol As UInteger
    Public Function init() As Boolean
        Dim result As Integer
        Dim readers As StringBuilder
        Dim pcchReaders As UInteger
        readers = New StringBuilder()
        result = NativeMethods.SCardEstablishContext(NativeMethods.SCARD_SCOPE_USER, System.IntPtr.Zero, IntPtr.Zero, hContext)
        If (result <> 0) Then
            Return False
        End If

        pcchReaders = 256
        result = NativeMethods.SCardListReadersW(hContext, Nothing, readers, pcchReaders)
        If (result <> 0) Then
            Return False
        End If

        result = NativeMethods.SCardConnectW(hContext, readers.ToString(), NativeMethods.SCARD_SHARE_SHARED, NativeMethods.SCARD_PROTOCOL_T0, phCard, activeProtocol)
        If (result <> 0) Then
            Return False
        End If

        Return True




    End Function

    Public Function cleanup() As Integer
        Return NativeMethods.SCardReleaseContext(hContext)
    End Function


End Class
