Public Class JpnReaderService

    Private reader As JpnReader = New JpnReader()

    Public Function init() As Integer
        Return reader.init()
    End Function

    Public Sub cleanUp()
        reader.cleanUp()
    End Sub



    Private szOriginalName As String
    Public ReadOnly Property originalName() As String
        Get
            Return szOriginalName
        End Get
    End Property

End Class
