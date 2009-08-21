Class JpnConversion

    Public Function bcdByteToDDMMYYYY(ByVal bcd As Byte()) As String
        If bcd.Length <> 4 Then
            Return Nothing
        End If
        Dim result As String
        result = bcdBytetoHex(bcd(3)) + "/" + bcdBytetoHex(bcd(2)) + "/" + bcdBytetoHex(bcd(0)) + bcdBytetoHex(bcd(1))
        Return result
    End Function

    Public Function toPostcodeString(ByVal content As Byte()) As String
        Dim last2digits As String = bcdBytetoHex(content(2))
        Dim lastDigit As String = last2digits.Substring(1, 1)
        Return bcdBytetoHex(content(0)) + bcdBytetoHex(content(1)) + lastDigit
    End Function


    Private Function bcdBytetoHex(ByVal b As Byte) As String
        Dim zero As Char = CChar("0")
        Return Hex(b).PadLeft(2, zero)
    End Function
End Class
