Public Class JpnConversion
    Public Function bcdByteToDDMMYY(ByVal bcd As Byte()) As String
        If bcd.Length <> 4 Then
            Return Nothing
        End If
        Dim result As String
        result = Hex(bcd(3)) + "/" + Hex(bcd(2)) + "/" + Hex(bcd(0)) + Hex(bcd(1))
        Return result
    End Function
End Class
