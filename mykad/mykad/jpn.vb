Option Strict On

Imports System.Runtime.InteropServices
Imports System.Text
<StructLayout(LayoutKind.Sequential)> _
Public Structure SCARD_IO_REQUEST
    Public dwProtocol As Integer
    Public cbPciLength As Integer
End Structure
Module jpn
    Const SCARD_SCOPE_USER As Integer = 0
    Const SCARD_SCOPE_TERMINAL As Integer = 1
    Const SCARD_SCOPE_SYSTEM As Integer = 2
End Module
