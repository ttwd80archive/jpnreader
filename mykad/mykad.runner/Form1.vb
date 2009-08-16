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
        ListBox1.Items.Add("Religion: [" + service.religion + "]")
        ListBox1.Items.Add("East Malaysian: [" + service.eastMalaysian + "]")
        ListBox1.Items.Add("Address:")
        ListBox1.Items.Add("[" + service.address1 + "]")
        ListBox1.Items.Add("[" + service.address2 + "]")
        ListBox1.Items.Add("[" + service.address3 + "]")
        ListBox1.Items.Add("Postcode: [" + service.postcode + "]")
        ListBox1.Items.Add("City: [" + service.city + "]")
        ListBox1.Items.Add("State: [" + service.state + "]")
        Me.Refresh()
        Const MAX_IMAGE_BLOCK As Integer = 15
        Dim imageContent(4000 - 1) As Byte
        Dim imageOffset = 0
        For i As Integer = 0 To MAX_IMAGE_BLOCK
            Dim base64 As String = service.getImageContent(i)
            Dim imageBlock As Byte() = Convert.FromBase64String(base64)
            Array.Copy(imageBlock, 0, imageContent, imageOffset, imageBlock.Length)
            imageOffset = imageOffset + imageBlock.Length
            ProgressBar1.Value = CInt((i / MAX_IMAGE_BLOCK) * 100)
            Me.Refresh()
        Next

        Dim icPicture As Image = Drawing.Image.FromStream(New IO.MemoryStream(imageContent))
        PictureBox1.Image = icPicture
        service.cleanUp()
        stopwatch.Stop()
        ListBox1.Items.Add("Image size: [" + CStr(imageOffset) + "] bytes")
        ListBox1.Items.Add("END :" + CStr(stopwatch.ElapsedMilliseconds()))


    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ListBox1.Items.Clear()
    End Sub
End Class
