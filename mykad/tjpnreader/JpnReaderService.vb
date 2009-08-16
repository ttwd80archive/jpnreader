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
        szDateIssued = toDateString(file1Content, &H144)
        szCitizenship = Text.Encoding.ASCII.GetString(file1Content, &H148, &H12).Trim()
        szRace = Text.Encoding.ASCII.GetString(file1Content, &H15A, &H19).Trim()
        szReligion = Text.Encoding.ASCII.GetString(file1Content, &H173, &HB).Trim()
        szEastMalaysian = Text.Encoding.ASCII.GetString(file1Content, &H17E, &H1).Trim()
        szAddress1 = Text.Encoding.ASCII.GetString(file4Content, &H3, &H1E).Trim()
        szAddress2 = Text.Encoding.ASCII.GetString(file4Content, &H21, &H1E).Trim()
        szAddress3 = Text.Encoding.ASCII.GetString(file4Content, &H3F, &H1E).Trim()
        szPostcode = toPostcodeString(file4Content, &H5D)
        szCity = Text.Encoding.ASCII.GetString(file4Content, &H60, &H19).Trim()
        szState = Text.Encoding.ASCII.GetString(file4Content, &H79, &H1E).Trim()
        Return True
    End Function
    Private Function toPostcodeString(ByVal content As Byte(), ByVal offset As Integer) As String
        Dim conversion As JpnConversion = New JpnConversion()
        Dim buffer(3 - 1) As Byte
        Array.Copy(content, offset, buffer, 0, 3)
        Return conversion.toPostcodeString(buffer)
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
    Private szDateIssued As String
    Public ReadOnly Property dateIssued() As String
        Get
            Return szDateIssued
        End Get
    End Property
    Private szCitizenship As String
    Public ReadOnly Property citizenship() As String
        Get
            Return szCitizenship
        End Get
    End Property
    Private szRace As String
    Public ReadOnly Property race() As String
        Get
            Return szRace
        End Get
    End Property
    Private szReligion As String
    Public ReadOnly Property religion() As String
        Get
            Return szReligion
        End Get
    End Property
    Private szEastMalaysian As String
    Public ReadOnly Property eastMalaysian() As String
        Get
            Return szEastMalaysian
        End Get
    End Property
    Private szAddress1 As String
    Public ReadOnly Property address1() As String
        Get
            Return szAddress1
        End Get
    End Property
    Private szAddress2 As String
    Public ReadOnly Property address2() As String
        Get
            Return szAddress2
        End Get
    End Property
    Private szAddress3 As String
    Public ReadOnly Property address3() As String
        Get
            Return szAddress3
        End Get
    End Property

    Private szPostcode As String
    Public ReadOnly Property postcode() As String
        Get
            Return szPostcode
        End Get
    End Property
    Private szCity As String
    Public ReadOnly Property city() As String
        Get
            Return szCity
        End Get
    End Property
    Private szState As String
    Public ReadOnly Property state() As String
        Get
            Return szState
        End Get
    End Property
    Public Function getImageContent(ByVal index As Integer) As String
        If (index < 0 Or index > 15) Then
            Return Nothing
        End If
        Dim content As Byte()
        content = reader.readImagePart(CUInt(index))
        If content Is Nothing Then
            Return Nothing
        Else
            Return Convert.ToBase64String(content)
        End If
    End Function
End Class
