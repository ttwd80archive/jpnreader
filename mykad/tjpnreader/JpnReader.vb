Public Class JpnReader
    Private engine As JpnReaderEngine = New JpnReaderEngine()
    Public Function init() As Integer
        engine.init()

    End Function

    Public Sub cleanUp()
        engine.cleanUp()
    End Sub


End Class
