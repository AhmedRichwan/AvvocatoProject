Imports System.Data.OleDb


Public Class AddAvvocato
    Public dbname As String = "D:\my dbs\devy.mdb"
    Public connecta As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & My.Settings.MyDefdata)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try
            If (connecta.State = ConnectionState.Open) Then connecta.Close()
            connecta.Open()
            Dim avname As String
            avname = NewAvvocatoname.Text
            Dim Addavcomd As New OleDbCommand
            Dim sqladdavc As String = "INSERT INTO [avvocatolist] ([avvocatoname]) VALUES (@avvocatoname)"
            Addavcomd.Connection = connecta
            Addavcomd.CommandText = sqladdavc
            Addavcomd.Parameters.AddWithValue("@avvocatoname", avname)
            Addavcomd.ExecuteScalar()
            connecta.Close()
            '  MsgBox("تمت الاضافة بنجاح ")
            avcadded.Visible = True
            LoginForm.SendToBack()
            MessageBox.Show("تمت الاضافة بنجاح")
            ' Threading.Thread.Sleep(1000)
            Me.Hide()

        Catch
            LoginForm.SendToBack()
            MsgBox("لم تتم عملية الاضافة ، ربما يكون الاسم موجود من قبل، اختر اسما اخر")

        End Try

    End Sub


End Class