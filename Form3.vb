Imports System.Data.OleDb

Public Class LoginForm
    Public dt, donedt, notdonedt, delayeddt, CWdt, NWdt, Anwdt, Cmdt, NMdt As DataTable
    Public dbname As String = "D:\my dbs\devy.mdb"
    Public connecta As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & My.Settings.MyDefdata)
    'Public Mydb As String = My.Settings.CurrentDb
    Public mystr As String = "11/11/2018"
    Public dayOfWeek = CInt(DateTime.Today.DayOfWeek)
    'Public SCW = DateTime.Today.AddDays(-1 * dayOfWeek)
    'Public ECW = DateTime.Today.AddDays(4 - dayOfWeek)
    ''Public SNW = DateTime.Today.AddDays(7 - dayOfWeek)
    ''Public ENW = DateTime.Today.AddDays(11 - dayOfWeek)
    'Public SNW = Format(DateTime.Today.AddDays(7 - dayOfWeek), "MM/dd/yyyy")
    'Public ENW = Format(DateTime.Today.AddDays(11 - dayOfWeek), "MM/dd/yyyy")
    'Public SANW = Format(DateTime.Today.AddDays(14 - dayOfWeek), "MM/dd/yyyy")
    'Public EANW = Format(DateTime.Today.AddDays(18 - dayOfWeek), "MM/dd/yyyy")


    'Public SCM = Format(DateAdd("d", 1 - DatePart("d", Today()), Today()), "MM/dd/yyyy")
    'Public ECM = Format(New Date(Now.Year, Now.Month, Date.DaysInMonth(Now.Year, Now.Month)), "MM/dd/yyyy")
    'Public SNM = Format(Now.AddDays((Now.Day - 1) * -1).AddMonths(1), "MM/dd/yyyy")
    'Public ENM = Format(New Date(Now.Year, Now.Month + 1, Date.DaysInMonth(Now.Year, Now.Month + 1)), "MM/dd/yyyy")

    ''Public  = DateTime.Today.AddDays(11 - dayOfWeek)
    'Public R As Integer 'Number in column 0 based on value set in Form1_Load   
    'Public CR As Integer ' Current row index  
    'Public lastnav As String
    'Public rpt As New CrystalReport3
    'Public selectSqlall = "select * from CurCases "
    'Public Sqltitle = "SELECT Config.[DbTitle] FROM Config where id =1 ;"
    '' Public Sqltitle = "SELECT *FROM Config;"
    'Public Sqldone = "SELECT * FROM curcases WHERE (((curcases.[Done])=yes)); "
    'Public Sqlnotdone = "SELECT * FROM curcases WHERE (((curcases.[Done])=no)); "
    'Public SqlDelayed = "SELECT * FROM CurCases WHERE (((curcases.[Delayed])=yes)); "
    'Public SqlCW = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SCW & "# And #" & ECW & "#));"
    'Public SqlNW = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SNW & "# And #" & ENW & "#));"
    'Public SqlANW = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SANW & "# And #" & EANW & "#));"
    'Public SqlCM = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SCM & "# And #" & ECM & "#));"
    'Public SqlNM = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SNM & "# And #" & ENM & "#));"

    Public adapter, doneadapter, notdoneadapter, delayedadapter, Cwadapter, NWadapter, ANWadapter, CMadapter, NMadapter As OleDbDataAdapter

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form1.dbname = "D:\my dbs\devy.mdb"
        Try
            If (connecta.State = ConnectionState.Open) Then connecta.Close()

            Dim logincommend As New OleDbCommand
            Dim Reader As OleDbDataReader

            logincommend.Connection = connecta
            connecta.Open()
            logincommend.CommandText = " SELECT Login.[UserName], Login.[Password] FROM Login where username='" & TextBox1.Text & "'and password = '" & TextBox2.Text & "'"
            '  logincommend.CommandText = SqlDelayed

            Reader = logincommend.ExecuteReader

            If Reader.HasRows Then
                'MsgBox("successfull")
                'Call Form1.OpenDevy()
                '  Me.Hide()
                Try
                    Dim ComboBoxds As New DataSet
                    Dim ComboBoxadapter As New OleDbDataAdapter
                    Dim sqlcombobox = "SELECT avvocatolist.[AvvocatoName] FROM avvocatolist ;" ' where avvocatolist.[AvvocatoName] is not null  ;"
                    ' Dim sqlcombobox = "SELECT AvvocatoList.[AvvocatoName] FROM AvvocatoList;"
                    ComboBoxadapter = New OleDbDataAdapter(sqlcombobox, connecta)
                    ComboBoxadapter.Fill(ComboBoxds)
                    ComboBox1.DataSource = ComboBoxds.Tables(0)
                    ComboBox1.ValueMember = "AvvocatoName"
                    ComboBox1.DisplayMember = "AvvocatoName"
                    connecta.Close()
                    addavcbtn.Enabled = True
                Catch ex As Exception
                    MessageBox.Show(ex.Message & "combox")

                End Try

                My.Settings.CurrentUser = TextBox1.Text
                'If CheckBox1.Checked = False Then
                '    TextBox1.Text = ""
                '    TextBox2.Text = ""
                'End If
            Else
                If TextBox1.Text <> "" Then MsgBox("Wrong Username or Password") Else MsgBox("Please enter your Username First")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)

            ' MsgBox("insert data first")
        End Try
        connecta.Close()


        'Catch ex As Exception
        '' MessageBox.Show(ex.Message)
        'MsgBox("خطأ اثناء فتح قاعدة البيانات الافتراضية-ربما الاتصال لا زال مفتوحا وانت تحاول فتحه مرة اخرى" & ex.Message)

        'End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.Click

        Form1.dbname = "D:\my dbs\devy.mdb"
        Try
            If (connecta.State = ConnectionState.Open) Then connecta.Close()

            Dim logincommend As New OleDbCommand
            Dim Reader As OleDbDataReader

            logincommend.Connection = connecta
            connecta.Open()
            logincommend.CommandText = " SELECT Login.[UserName], Login.[Password] FROM Login where username='" & TextBox1.Text & "'and password = '" & TextBox2.Text & "'"


            Reader = logincommend.ExecuteReader

            If Reader.HasRows And ComboBox1.SelectedIndex < 0 Then
                'ComboBox1.Enabled = True
                addavcbtn.Enabled = True
                Try
                    Dim ComboBoxds As New DataSet
                    Dim ComboBoxadapter As New OleDbDataAdapter
                    Dim sqlcombobox = "SELECT avvocatolist.[AvvocatoName] FROM avvocatolist ;"
                    ComboBoxadapter = New OleDbDataAdapter(sqlcombobox, connecta)
                    ComboBoxadapter.Fill(ComboBoxds)
                    ComboBox1.DataSource = ComboBoxds.Tables(0)
                    ComboBox1.ValueMember = "AvvocatoName"
                    ComboBox1.DisplayMember = "AvvocatoName"
                    connecta.Close()
                Catch ex As Exception
                    MessageBox.Show(ex.Message & "combox")

                End Try

                My.Settings.CurrentUser = TextBox1.Text
            Else
                ' ComboBox1.Enabled = False

            End If
        Catch ex As Exception
            MsgBox(ex.Message)

            ' MsgBox("insert data first")
        End Try
        connecta.Close()





    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles addavcbtn.Click
        AddAvvocato.Show()
    End Sub

    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    '


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try
            If (connecta.State = ConnectionState.Open) Then connecta.Close()

            Dim logincommend As New OleDbCommand
            Dim Reader As OleDbDataReader

            logincommend.Connection = connecta
            connecta.Open()
            logincommend.CommandText = " SELECT Login.[UserName], Login.[Password] FROM Login where username='" & TextBox1.Text & "'and password = '" & TextBox2.Text & "'"
            '  logincommend.CommandText = SqlDelayed

            Reader = logincommend.ExecuteReader

            If Reader.HasRows Then 'ComboBox1.SelectedIndex >= 0 Then
                'MsgBox("successfull")
                ' MsgBox(ComboBox1.SelectedValue.ToString)
                Try
                    My.Settings.CurrentAvvocato = ComboBox1.SelectedValue.ToString
                Catch
                    MsgBox("اختر محامي اولا")
                    Exit Sub
                End Try
                Call Form1.OpenDevy()
                Form1.loggeduser.Visible = True
                Form1.loggeduser.Text = My.Settings.CurrentUser
                Me.Hide()
                My.Settings.CurrentUser = TextBox1.Text
                If CheckBox1.Checked = False Then
                    TextBox1.Text = ""
                    TextBox2.Text = ""
                End If
            Else
                If TextBox1.Text <> "" Or ComboBox1.SelectedIndex < 0 Then MsgBox("اسم المستخدم أو كلمة المرور غير صحيحة") Else MsgBox("ادخل اسم المستخدم وكلمة المرور")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)

            ' MsgBox("insert data first")
        End Try
        connecta.Close()

    End Sub

    Private Sub LoginForm_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Dim tmp As System.Windows.Forms.KeyPressEventArgs = e

        If tmp.KeyChar = ChrW(Keys.Enter) Then MsgBox("Ahmed") 'Call Button1_Click(sender, e)

    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call Button1_Click(sender, e)
        End If
    End Sub
    Private Sub TextBox2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call Button1_Click(sender, e)
        End If
    End Sub

End Class