Public Class JpnReaderService

    Private reader As JpnReader = New JpnReader()

    Public Function init() As Integer
        Return reader.init()
    End Function

    Public Sub cleanUp()
        reader.cleanUp()
    End Sub

    Public Function readTextInfo() As Boolean
        Dim file1Content As Byte() = reader.readFile1()
        If file1Content Is Nothing Then
            Return False
        End If
        Dim file4Content As Byte() = reader.readFile4()
        If file4Content Is Nothing Then
            Return False
        End If
        Return True

    End Function


    Private szOriginalName As String
    Public ReadOnly Property originalName() As String
        Get
            Return szOriginalName
        End Get
    End Property

End Class
