Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim stopwatch As Stopwatch = New Stopwatch()
        stopwatch.Start()
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

        ListBox1.Items.Add("Original Name: [" + service.originalName + "]")
        ListBox1.Items.Add("IC Number: [" + service.icno + "]")
        ListBox1.Items.Add("Gender: [" + service.gender + "]")
        ListBox1.Items.Add("Birth Date: [" + service.birthdate + "]")
        ListBox1.Items.Add("Birth Place: [" + service.birthplace + "]")
        ListBox1.Items.Add("Date Issued: [" + service.dateIssued + "]")
        ListBox1.Items.Add("Citizenship: [" + service.citizenship + "]")
        ListBox1.Items.Add("Race: [" + service.race + "]")
        service.cleanUp()
        stopwatch.Stop()
        Me.Refresh()
        ListBox1.Items.Add("END :" + CStr(stopwatch.ElapsedMilliseconds()))


    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ListBox1.Items.Clear()
    End Sub
End Class
