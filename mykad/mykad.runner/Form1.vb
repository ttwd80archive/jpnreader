Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim service As Tabuk.MyKad.JpnReaderService = New Tabuk.MyKad.JpnReaderService()
        Dim result As Integer
        result = service.init()
        If (result <> 0) Then
            MsgBox("Bad Init")
            service.cleanUp()
            Return
        End If
        If service.readTextInfo() = False Then
            MsgBox("Error reading info")
            service.cleanUp()
            Return
        End If
        ListBox1.Items.Add(service.icno)


    End Sub
End Class
