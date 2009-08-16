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
        szOriginalName = Text.Encoding.ASCII.GetString(file1Content, &H3, &H96).Trim()
        szIcno = Text.Encoding.ASCII.GetString(file1Content, &H111, &HD).Trim()
        szGender = Text.Encoding.ASCII.GetString(file1Content, &H11E, 1).Trim()
        szOldId = Text.Encoding.ASCII.GetString(file1Content, &H11F, &H8).Trim()
        szBirthdate = toDateString(file1Content, &H127)
        szBirthplace = Text.Encoding.ASCII.GetString(file1Content, &H12B, &H19).Trim()
        Return True
    End Function

    Private Function toDateString(ByVal content As Byte(), ByVal offset As Integer) As String
        Dim conversion As JpnConversion = New JpnConversion()
        Dim buffer(4 - 1) As Byte
        Array.Copy(content, offset, buffer, 0, 4)
        Return conversion.bcdByteToDDMMYYYY(buffer)
    End Function


    Private szOriginalName As String
    Public ReadOnly Property originalName() As String
        Get
            Return szOriginalName
        End Get
    End Property

    Private szIcno As String
    Public ReadOnly Property icno() As String
        Get
            Return szIcno
        End Get
    End Property
    Private szGender As String
    Public ReadOnly Property gender() As String
        Get
            Return szGender
        End Get
    End Property
    Private szOldId As String
    Public ReadOnly Property oldId() As String
        Get
            Return szOldId
        End Get
    End Property

    Private szBirthdate As String
    Public ReadOnly Property birthdate() As String
        Get
            Return szBirthdate
        End Get
    End Property
    Private szBirthplace As String
    Public ReadOnly Property birthplace() As String
        Get
            Return szBirthplace
        End Get
    End Property

End Class
