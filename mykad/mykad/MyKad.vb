
Option Strict On

Imports System.Runtime.InteropServices
Imports System.Text

Public Class MyKad
    Private handle As UInteger




    Public Function init() As Boolean
        Dim result As Integer
        result = NativeMethods.SCardEstablishContext(NativeMethods.SCARD_SCOPE_USER, System.IntPtr.Zero, IntPtr.Zero, handle)
        If (result = 0) Then
            Return True
        Else
            Return False
        End If



    End Function

    Public Function cleanup() As Integer
        Return NativeMethods.SCardReleaseContext(handle)
    End Function


End Class
