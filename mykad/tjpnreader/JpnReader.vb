Public Class JpnReader
    Private engine As JpnReaderEngine = New JpnReaderEngine()
    Public Function init() As Integer
        engine.init()

    End Function

    Public Sub cleanUp()
        engine.cleanUp()
    End Sub

    Public Function readFile1() As Byte()
        Const LENGTH_1 As Integer = 252
        Const LENGTH_2 As Integer = 207
        Dim content1 As Byte() = engine.readSegment(1, 0, LENGTH_1)
        If (content1 Is Nothing) Then
            Return Nothing
        End If

        Dim content2 As Byte() = engine.readSegment(1, LENGTH_1, LENGTH_2)
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
        Dim content As Byte() = engine.readSegment(4, 0, LENGTH)
        Return content
    End Function
    Public Function readImagePart(ByVal index As UInteger) As Byte()
        Const LIMIT As UInteger = 4011
        Const IMAGE_OFFSET As UInteger = 3
        Const LENGTH As UInteger = 252
        Const END_PADDING_LENGTH As UInteger = 8
        Dim offset As UInteger
        If index = 0 Then
            offset = IMAGE_OFFSET
        Else
            offset = 0
        End If
        Dim blockAlign As UInteger = (LENGTH * index)
        Dim blockSize As UInteger = LENGTH
        If blockAlign + blockSize > LIMIT Then
            blockSize = CUInt(LIMIT - blockAlign - END_PADDING_LENGTH)
        End If
        Dim blockContent As Byte() = engine.readSegment(2, blockAlign, blockSize)
        Dim imageContent(CInt(blockSize - offset - 1)) As Byte
        Array.Copy(blockContent, offset, imageContent, 0, blockSize - offset)
        Return imageContent
    End Function
    Public Function readImageFileFull() As Byte()
        Const LIMIT As UInteger = 4011
        Const IMAGE_OFFSET As Integer = 3
        Const IMAGE_LENGTH As Integer = 4000
        Const LENGTH As Integer = 252
        Dim content(LIMIT - 1) As Byte
        Dim contentLength As UInteger = 0
        Dim readCount As Integer = 0
        While contentLength < LIMIT
            Dim blockSize As UInteger
            Dim unread As UInteger = LIMIT - contentLength
            If unread > LENGTH Then
                blockSize = LENGTH
            Else
                blockSize = unread
            End If
            Dim blockContent As Byte() = engine.readSegment(2, contentLength, blockSize)
            readCount = readCount + 1
            If (blockContent Is Nothing) Then
                Return Nothing
            End If
            Array.Copy(blockContent, 0, content, contentLength, blockSize)
            contentLength = contentLength + CUInt(blockContent.Length())
        End While
        Dim imageContent(IMAGE_LENGTH - 1) As Byte
        Array.Copy(content, IMAGE_OFFSET, imageContent, 0, IMAGE_LENGTH)
        Return imageContent
    End Function

End Class
