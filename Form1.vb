Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class Form1
    Public mystr As String = "11/11/2018"
    Public dayOfWeek = CInt(DateTime.Today.DayOfWeek)
    Public SCW = Format(DateTime.Today.AddDays(-1 * dayOfWeek), "MM/dd/yyyy")
    Public ECW = Format(DateTime.Today.AddDays(4 - dayOfWeek), "MM/dd/yyyy")
    Public SNW = Format(DateTime.Today.AddDays(7 - dayOfWeek), "MM/dd/yyyy")
    Public ENW = Format(DateTime.Today.AddDays(11 - dayOfWeek), "MM/dd/yyyy")
    Public SANW = Format(DateTime.Today.AddDays(14 - dayOfWeek), "MM/dd/yyyy")
    Public EANW = Format(DateTime.Today.AddDays(18 - dayOfWeek), "MM/dd/yyyy")
    Public SCM = Format(DateAdd("d", 1 - DatePart("d", Today()), Today()), "MM/dd/yyyy")
    Public ECM = Format(New Date(Now.Year, Now.Month, Date.DaysInMonth(Now.Year, Now.Month)), "MM/dd/yyyy")
    Public SNM = Format(Now.AddDays((Now.Day - 1) * -1).AddMonths(1), "MM/dd/yyyy")

    Public ENM = Format(New Date(Now.Year, Now.Month, Date.DaysInMonth(Now.Year, Now.Month)), "MM/dd/yyyy")
    '  Public ENM = Format(New Date(Now.Year, Now.Month + 1, Date.DaysInMonth(Now.Year, Now.Month + 1)), "MM/dd/yyyy")
    Public R As Integer
    Public CR As Integer
    Public lastnav As String
    Public rpt As New CrystalReport3
    Public avvocatovalue = " and (((CurCases.avvocato)))= '" & My.Settings.CurrentAvvocato & "'"
    Public avvocatovalueall = " where (((CurCases.avvocato)))= '" & My.Settings.CurrentAvvocato & "'"
    Public selectSqlall = "select * from CurCases " & avvocatovalueall & " ; "
    Public Sqltitle = "SELECT Config.[DbTitle] FROM Config where id =1 ;"
    Public sqlcombobox = "SELECT curcases.[avvocato] FROM curcases  ;"
    Public Sqldone = "SELECT * FROM curcases WHERE (((curcases.[Done])=yes))" & avvocatovalue & " ; "
    Public Sqlnotdone = "SELECT * FROM curcases WHERE (((curcases.[Done])=no))" & avvocatovalue & " ; "
    Public SqlDelayed = "SELECT * FROM CurCases WHERE (((curcases.[Delayed])=yes))" & avvocatovalue & " ; "
    Public SqlCW = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SCW & "# And #" & ECW & "#))" & avvocatovalue & " ; "
    Public SqlNW = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SNW & "# And #" & ENW & "#))" & avvocatovalue & " ; "
    Public SqlANW = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SANW & "# And #" & EANW & "#))" & avvocatovalue & " ; "
    Public SqlCM = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SCM & "# And #" & ECM & "#))" & avvocatovalue & " ; "
    Public SqlNM = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SNM & "# And #" & ENM & "#))" & avvocatovalue & " ; "
    Public adapter, doneadapter, notdoneadapter, delayedadapter, Cwadapter, NWadapter, ANWadapter, CMadapter, NMadapter, ComboBoxadapter As OleDbDataAdapter
    Public dt, donedt, notdonedt, delayeddt, CWdt, NWdt, Anwdt, Cmdt, NMdt, comboboxdt As DataTable
    Public dbname As String = "D:\my dbs\devy.mdb"
    Public connecta As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & My.Settings.MyDefdata)



    '  Public CONNECTA = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & mydb)


    'Dim querystr As String = "select value from MyTable where ID=5"
    'Dim mycmd As New SqlCommand(querystr, connection)
    'Dim value As Object = mycmd.ExecuteScalar()

    'Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '    Try
    '        'TODO: This line of code loads data into the 'Database1DataSet.DoneCases' table. You can move, or remove it, as needed.
    '        '  Me.CurCasesTableAdapter1.Fill(Me.Database1DataSet.CurCases)
    '    Catch
    '        MsgBox("لم يتم العثور على قاعدة البيانات")

    '    End Try

    'End Sub
    'Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    '    Me.DataGridView1.Item(0, 0).Value = 1 '"1" or any number where you want to start   
    '    DataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText
    '    ' DataGridView1.ClipboardCopyMode =
    'End Sub
    'Sub OpenDefDb()

    '    dt = New DataTable()
    '    Mydb = My.Settings.MyDefdata
    '    Mydb = Mydb
    '    Try
    '        connecta.Open()
    '        adapter = New OleDbDataAdapter(selectSqlall, connecta)
    '        adapter.Fill(dt)
    '        DataGridView1.AutoGenerateColumns = False
    '        DataGridView1.DataSource = dt


    '        connecta.Close()


    '        TabControl1.Visible = True


    '    Catch ex As Exception
    '        ' MessageBox.Show(ex.Message)
    '        MsgBox("خطأ اثناء فتح قاعدة البيانات الافتراضية")

    '    End Try
    '    Call Autoserial()
    '    '  Call AUTOGENERATE()
    'End Sub
    Sub Autoserial()

        For Each row As DataGridViewRow In DataGridView1.Rows
            row.Cells(0).Value = row.Index + 1
        Next
        For Each row As DataGridViewRow In DataGridView2.Rows
            row.Cells(0).Value = row.Index + 1
        Next
        For Each row As DataGridViewRow In DataGridView3.Rows
            row.Cells(0).Value = row.Index + 1
        Next
        For Each row As DataGridViewRow In DataGridViewDL.Rows
            row.Cells(0).Value = row.Index + 1
        Next
        For Each row As DataGridViewRow In DataGridViewCW.Rows
            row.Cells(0).Value = row.Index + 1
        Next
        For Each row As DataGridViewRow In DataGridViewCW.Rows
            row.Cells(0).Value = row.Index + 1
        Next
        For Each row As DataGridViewRow In DataGridViewANW.Rows
            row.Cells(0).Value = row.Index + 1
        Next
        For Each row As DataGridViewRow In DataGridViewCM.Rows
            row.Cells(0).Value = row.Index + 1
        Next
        For Each row As DataGridViewRow In DataGridViewNM.Rows
            row.Cells(0).Value = row.Index + 1
        Next


    End Sub


    Private Sub Button6_Click(sender As Object, e As EventArgs)
        Application.Restart()
    End Sub




    Private Sub Button4_Click(sender As Object, e As EventArgs)
        '  MsgBox()
        '   MsgBox(DateAdd("l", 1 - DatePart("l", Today()), Today()))

        ' Dim DaysInMonth As Integer = Date.DaysInMonth(Now.Year, Now.Month)
        ' Dim LastDayInMonthDate As Date = New Date(Now.Year, Now.Month, Date.DaysInMonth(Now.Year, Now.Month))
        '  MsgBox(LastDayInMonthDate)
        MsgBox(SNM)

        Try
            'Dim d As Date
            ' MsgBox(New DateTime(d.Year, d.Month, 1).AddDays(-1).ToLongDateString)

        Catch ex As Exception

        End Try
        '   MsgBox(ENW)
        'If ComboBox1.SelectedIndex = 0 Then Call OrgnizerT()



        'rpt.SetDataSource(dt)
        'Form2.CrystalReportViewer3.ReportSource = rpt
        'Form2.CrystalReportViewer3.Show()
        'Form2.Show()


        'If ComboBox1.SelectedIndex = 1 Then MsgBox(ComboBox1.Text)
        'rpt.SetDataSource(dt)
        'Form2.CrystalReportViewer3.ReportSource = rpt
        ''  Form2.CrystalReportViewer3.Show()
        '' Form2.Show()


        'If mydb <> "" Then
        '    dt = New DataTable()

        '    Dim conn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & mydb)
        '    'Dim selectSql = "select * from curCases where FIleNum='test'"
        '    Dim selectSql = sq.Text
        '    Try
        '        conn.Open()
        '        adapter = New OleDbDataAdapter(selectSql, conn)
        '        DataGridView1.AutoGenerateColumns = False
        '        adapter.Fill(dt)
        '        DataGridView1.DataSource = dt
        '    Catch ex As Exception
        '        'MsgBox("غير قادر على جلب البيانات")
        '        MessageBox.Show(ex.Message & "غير قادر على جلب البيانات")

        '    End Try


        ' Try

        ' Me.CurCasesTableAdapter1.Fill(Me.Database1DataSet.CurCases)
        ' DataGridView1.DataSource = CurCasesBindingSource

        'Catch
        'MsgBox("لم يتم العثور على قاعدة البيانات لاعادة تحميلها")
        'End Try
        'Else
        '    MsgBox("قم بفتح قاعدة بيانات اولا")
        'End If

        'Call Autoserial()

    End Sub



    Private Sub Button9_Click(sender As Object, e As EventArgs)
        Try
            Dim scb = New OleDbCommandBuilder(adapter)
            adapter.Update(dt)
            MessageBox.Show("OK!")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub




    ' Private Sub DataGridView1_DataError(sender As Object, e As DataGridViewDataErrorEventArgs)
    ' MsgBox("تم ادخال التاريخ بطريقة غير صحيحة ، الطريقة الصحيحة هي   23/1/2015")
    'End Sub



    Private Sub DataGridView1_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs)
        Call Autoserial()
    End Sub
    'Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs)
    'If DataGridView1.SelectedCells.Count > 0 Then
    '       DataGridView1.ContextMenuStrip = ContextMenuStrip1
    'End If
    'End Sub

    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        CopyToClipboard()
        For counter As Integer = 0 To DataGridView1.SelectedCells.Count - 1
            DataGridView1.SelectedCells(counter).Value = String.Empty
        Next
    End Sub

    Private Sub CopyToClipboard()
        Dim dataObj As DataObject = DataGridView1.GetClipboardContent
        If Not IsNothing(dataObj) Then
            Clipboard.SetDataObject(dataObj)
        End If
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        CopyToClipboard()
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        PasteClipboardValue()
    End Sub

    Private Sub PasteClipboardValue()
        If DataGridView1.SelectedCells.Count = 0 Then
            MessageBox.Show("No Cell selected", "Paste")
            Exit Sub
        End If

        Dim StartingCell As DataGridViewCell = GetStartingCell(DataGridView1)
        Dim rowCount = DataGridView1.SelectedCells.OfType(Of DataGridViewCell)().Select(Function(x) x.RowIndex).Distinct().Count()
        Dim cbvalue As Dictionary(Of Integer, Dictionary(Of Integer, String)) = ClipboardValues(Clipboard.GetText)
        Dim repeat As Integer = 0
        If rowCount > cbvalue.Keys.Count Then
            If rowCount Mod cbvalue.Keys.Count <> 0 Then
                MessageBox.Show("Selected destination doesn't match")
                Exit Sub
            Else
                repeat = CInt(rowCount / cbvalue.Keys.Count)
            End If
        End If


        Dim irowindex = StartingCell.RowIndex
        For x As Integer = 1 To repeat
            For Each rowkey As Integer In cbvalue.Keys
                Dim icolindex As Integer = StartingCell.ColumnIndex
                For Each cellkey As Integer In cbvalue(rowkey).Keys
                    If icolindex <= DataGridView1.Columns.Count - 1 And irowindex <= DataGridView1.Rows.Count - 1 Then
                        Dim cell As DataGridViewCell = DataGridView1(icolindex, irowindex)
                        cell.Value = cbvalue(rowkey)(cellkey)
                    End If
                    icolindex += 1
                Next
                irowindex += 1
            Next
        Next

    End Sub

    Private Function GetStartingCell(dgView As DataGridView) As DataGridViewCell
        If dgView.SelectedCells.Count = 0 Then Return Nothing

        Dim rowIndex As Integer = dgView.Rows.Count - 1
        Dim ColIndex As Integer = dgView.Columns.Count - 1


        For Each dgvcell As DataGridViewCell In dgView.SelectedCells

            If dgvcell.RowIndex < rowIndex Then rowIndex = dgvcell.RowIndex
            If dgvcell.ColumnIndex < ColIndex Then ColIndex = dgvcell.ColumnIndex
        Next

        Return dgView(ColIndex, rowIndex)
    End Function


    Private Function ClipboardValues(clipboardvalue As String) As Dictionary(Of Integer, Dictionary(Of Integer, String))
        Dim lines() As String = clipboardvalue.Split(CChar(Environment.NewLine))
        Dim copyValues As Dictionary(Of Integer, Dictionary(Of Integer, String)) = New Dictionary(Of Integer, Dictionary(Of Integer, String))
        For i As Integer = 0 To lines.Length - 1
            copyValues.Item(i) = New Dictionary(Of Integer, String)
            Dim linecontent() As String = lines(i).Split(ChrW(Keys.Tab))
            If linecontent.Length = 0 Then
                copyValues(i)(0) = String.Empty
            Else
                For j As Integer = 0 To linecontent.Length - 1
                    copyValues(i)(j) = linecontent(j)
                Next
            End If
        Next
        Return copyValues
    End Function

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        ' DataGridView1.EditMode = DataGridViewEditMode.EditOnEnter
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        ' DataGridView1.EditMode = DataGridViewEditMode.EditOnEnter
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then DataGridView1.EditMode = DataGridViewEditMode.EditOnEnter Else DataGridView1.EditMode = DataGridViewEditMode.EditOnKeystroke

    End Sub

    Private Sub PrintPreviewDialog1_Load(sender As Object, e As EventArgs)

    End Sub


    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click

        If (connecta.State = ConnectionState.Open) Then connecta.Close()
        '  Call Autoserial()
        Dim Expv1 As String = "Ahmed Rashwan Expval"
        Dim selectSql = "SELECT * FROM CurCases"
        Dim ReportAddress As String = "كشف عام"
        If TabControl1.SelectedTab Is Allcases Then
            selectSql = selectSqlall
            ReportAddress = "كشف تفصيلي بكل القضايا المتداولة والمحفوظة"
        End If
        If TabControl1.SelectedTab Is allcurrentcases Then
            selectSql = Sqlnotdone
            ReportAddress = " كشف تفصيلي بكل القضايا المتداولة"
        End If
        If TabControl1.SelectedTab Is allsavedcases Then
            selectSql = Sqldone
            ReportAddress = " كشف تفصيلي بكل القضايا المحكوم فيها"
        End If

        If TabControl1.SelectedTab Is DelayedCases Then
            selectSql = SqlDelayed
            ReportAddress = " كشف تفصيلي بكل القضايا المؤجلة"
        End If

        'If TabControl2.SelectedTab Is allcurrent Then
        '    selectSql = Sqlnotdone
        '    ReportAddress = " كشف تفصيلي بكل القضايا المتداولة"
        'End If
        If TabControl2.SelectedTab Is CW And TabControl1.SelectedTab Is allcurrentcases Then
            selectSql = SqlCW
            ReportAddress = " كشف تفصيلي بكل القضايا التى ستنظر الاسبوع الحالي "
        End If
        If TabControl2.SelectedTab Is NW And TabControl1.SelectedTab Is allcurrentcases Then
            selectSql = SqlNW
            ReportAddress = " كشف تفصيلي بكل القضايا التى ستنظر الاسبوع القادم"
        End If
        If TabControl2.SelectedTab Is ANW And TabControl1.SelectedTab Is allcurrentcases Then
            selectSql = SqlANW
            ReportAddress = " كشف تفصيلي بكل القضايا التي ستنظر الاسبوع بعد القادم"
        End If
        If TabControl2.SelectedTab Is CM And TabControl1.SelectedTab Is allcurrentcases Then
            selectSql = SqlCM
            ReportAddress = " كشف تفصيلي بكل القضايا التي تنظر هذا الشهر"
        End If
        If TabControl2.SelectedTab Is NM And TabControl1.SelectedTab Is allcurrentcases Then
            selectSql = SqlNM
            ReportAddress = " كشف تفصيلي بكل القضايا التى ستنظر الشهر القادم"
        End If
        dt = New DataTable()
        Try
            connecta.Open()


            sq.Text = selectSql
            adapter = New OleDbDataAdapter(selectSql, connecta)

            adapter.Fill(dt)

            rpt.SetDataSource(dt)
            rpt.SetParameterValue("ReportAddress", ReportAddress)
            rpt.SetParameterValue("expv8", Expv1.ToString)
            Form2.CrystalReportViewer3.ReportSource = rpt
            Form2.CrystalReportViewer3.Show()

            rpt.PrintToPrinter(1, False, 0, 0)
        Catch ex As Exception

            MessageBox.Show(ex.Message & "غير قادر على جلب البيانات")
        End Try
        ' Call Autoserial()

    End Sub





    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click

        'start declear

        Dim mystr As String = "11/11/2018"
        Dim dayOfWeek = CInt(DateTime.Today.DayOfWeek)
        Dim SCW = Format(DateTime.Today.AddDays(-1 * dayOfWeek), "MM/dd/yyyy")
        Dim ECW = Format(DateTime.Today.AddDays(4 - dayOfWeek), "MM/dd/yyyy")
        Dim SNW = Format(DateTime.Today.AddDays(7 - dayOfWeek), "MM/dd/yyyy")
        Dim ENW = Format(DateTime.Today.AddDays(11 - dayOfWeek), "MM/dd/yyyy")
        Dim SANW = Format(DateTime.Today.AddDays(14 - dayOfWeek), "MM/dd/yyyy")
        Dim EANW = Format(DateTime.Today.AddDays(18 - dayOfWeek), "MM/dd/yyyy")
        Dim SCM = Format(DateAdd("d", 1 - DatePart("d", Today()), Today()), "MM/dd/yyyy")
        Dim ECM = Format(New Date(Now.Year, Now.Month, Date.DaysInMonth(Now.Year, Now.Month)), "MM/dd/yyyy")
        Dim SNM = Format(Now.AddDays((Now.Day - 1) * -1).AddMonths(1), "MM/dd/yyyy")
        Dim ENM = Format(New Date(Now.Year, Now.Month + 1, Date.DaysInMonth(Now.Year, Now.Month + 1)), "MM/dd/yyyy")
        Dim rpt As New CrystalReport3
        Dim avvocatovalue = " and (((CurCases.avvocato)))= '" & My.Settings.CurrentAvvocato & "'"
        Dim avvocatovalueall = " where (((CurCases.avvocato)))= '" & My.Settings.CurrentAvvocato & "'"
        Dim selectSqlall = "select * from CurCases " & avvocatovalueall & " ; "
        Dim Sqltitle = "SELECT Config.[DbTitle] FROM Config where id =1 ;"
        Dim sqlcombobox = "SELECT curcases.[avvocato] FROM curcases  ;"
        Dim Sqldone = "SELECT * FROM curcases WHERE (((curcases.[Done])=yes))" & avvocatovalue & " ; "
        Dim Sqlnotdone = "SELECT * FROM curcases WHERE (((curcases.[Done])=no))" & avvocatovalue & " ; "
        Dim SqlDelayed = "SELECT * FROM CurCases WHERE (((curcases.[Delayed])=yes))" & avvocatovalue & " ; "
        Dim SqlCW = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SCW & "# And #" & ECW & "#))" & avvocatovalue & " ; "
        Dim SqlNW = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SNW & "# And #" & ENW & "#))" & avvocatovalue & " ; "
        Dim SqlANW = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SANW & "# And #" & EANW & "#))" & avvocatovalue & " ; "
        Dim SqlCM = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SCM & "# And #" & ECM & "#))" & avvocatovalue & " ; "
        Dim SqlNM = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SNM & "# And #" & ENM & "#))" & avvocatovalue & " ; "
        Dim adapter As OleDbDataAdapter
        Dim dt As DataTable
        Dim dbname As String = "D:\my dbs\devy.mdb"
        Dim connecta As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & dbname)

        'end declear

        '   Dim Expv1 As String = "Ahmed Rashwan Expval"
        If (connecta.State = ConnectionState.Open) Then connecta.Close()
        Dim selectSql As String = ""
        Dim ReportAddress As String = "كشف عام"
        If TabControl1.SelectedTab Is Allcases Then
            selectSql = selectSqlall
            ReportAddress = "كشف تفصيلي بكل القضايا المتداولة والمحفوظة والمؤجلة "
            ' MsgBox(selectSql)
        End If
        If TabControl1.SelectedTab Is allcurrentcases Then
            selectSql = Sqlnotdone
            ReportAddress = " كشف تفصيلي بكل القضايا المتداولة"
        End If
        If TabControl1.SelectedTab Is allsavedcases Then
            selectSql = Sqldone
            ReportAddress = " كشف تفصيلي بكل القضايا المحكوم فيها"
        End If

        If TabControl1.SelectedTab Is DelayedCases Then
            selectSql = SqlDelayed
            ReportAddress = " كشف تفصيلي بكل القضايا المؤجلة"
        End If

        'If TabControl2.SelectedTab Is allcurrent Then
        '    selectSql = Sqlnotdone
        '    ReportAddress = " كشف تفصيلي بكل القضايا المتداولة"
        'End If
        If TabControl2.SelectedTab Is CW And TabControl1.SelectedTab Is allcurrentcases Then
            selectSql = SqlCW
            ReportAddress = " كشف تفصيلي بكل القضايا التى ستنظر الاسبوع الحالي "
        End If
        If TabControl2.SelectedTab Is NW And TabControl1.SelectedTab Is allcurrentcases Then
            selectSql = SqlNW
            ReportAddress = " كشف تفصيلي بكل القضايا التى ستنظر الاسبوع القادم"
        End If
        If TabControl2.SelectedTab Is ANW And TabControl1.SelectedTab Is allcurrentcases Then
            selectSql = SqlANW
            ReportAddress = " كشف تفصيلي بكل القضايا التي ستنظر الاسبوع بعد القادم"
        End If
        If TabControl2.SelectedTab Is CM And TabControl1.SelectedTab Is allcurrentcases Then
            selectSql = SqlCM
            ReportAddress = " كشف تفصيلي بكل القضايا التي تنظر هذا الشهر"
        End If
        If TabControl2.SelectedTab Is NM And TabControl1.SelectedTab Is allcurrentcases Then
            selectSql = SqlNM
            ReportAddress = " كشف تفصيلي بكل القضايا التى ستنظر الشهر القادم"
        End If
        dt = New DataTable()
        Try
            connecta.Open()
            ' rpt.SetParameterValue("expv1", "قيمة افتراضية مرسلة")


            Dim crydt As New DataTable
            Dim expv1 As String
            expv1 = loggeduser.Text
            sq.Text = selectSql
            adapter = New OleDbDataAdapter(selectSql, connecta)
            adapter.Fill(crydt)
            rpt.SetDataSource(crydt)
            rpt.SetParameterValue("ReportAddress", ReportAddress)
            '   rpt.SetParameterValue("expv1", expv1)
            'rpt.SetParameterValue("expv8", expv1)
            ' DataGridView1.AutoGenerateColumns = False
            '  DataGridView1.DataSource = dt
            '  Call Autoserial()
            ' MsgBox(selectSql)
            Form2.CrystalReportViewer3.ReportSource = rpt
            Form2.CrystalReportViewer3.Show()
            Form2.Show()
        Catch ex As Exception
            'MsgBox("غير قادر على جلب البيانات")
            MessageBox.Show(ex.Message & "غير قادر على جلب البيانات")
        End Try


    End Sub

    Private Sub Opendata_Click(sender As Object, e As EventArgs)
        Dim fd As OpenFileDialog = New OpenFileDialog()

        fd.Title = "Open File Dialog"
        fd.InitialDirectory = "lastnav"
        fd.Filter = "Access Database (*.mdb)|*.mdb"
        fd.FilterIndex = 2
        fd.RestoreDirectory = True

        If fd.ShowDialog() = DialogResult.OK Then

            TabControl1.Visible = True
            ' Call AUTOGENERATE()
            changedbt.Visible = True
            Button1.Visible = True
            dblocation.Text = fd.FileName
            Select Case MsgBox("هل تود تعيين قاعدة البيانات كقاعدة اساسية", vbYesNo)
                '  If MsgBoxResult.Yes = True Then My.Settings.MyDefdata = Mydb Else My.Settings.MyDefdata = My.Settings.MyDefdata
                Case MsgBoxResult.Yes
                    'My.Settings.MyDefdata = Mydb

                    'MsgBox("  كداتا اساسية ( " & Mydb & " ) تم تعيين الداتا  ")
                Case MsgBoxResult.No
                    My.Settings.MyDefdata = My.Settings.MyDefdata
            End Select
        Else Exit Sub
        End If
        'Write Titles
        Try
            Dim dbtitlecommand As New OleDbCommand
            connecta.Open()
            dbtitlecommand.Connection = connecta
            dbtitlecommand.CommandText = Sqltitle
            changedbt.Text = (dbtitlecommand.ExecuteScalar) & My.Settings.CurrentAvvocato
            connecta.Close()
        Catch ex As Exception
            MsgBox(ex.Message & "قاعدة البيانات لا يوجد بها البيانات المطلوبة")

            Call AUTOGENERATE()
        End Try
    End Sub

    Private Sub CrystalReportViewer3_Load(sender As Object, e As EventArgs)

    End Sub

    'Private Sub Button8_Click_1(sender As Object, e As EventArgs)
    '    connecta = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & My.Settings.MyDefdata)

    '    Try
    '        If (connecta.State = ConnectionState.Open) Then connecta.Close()
    '        connecta.Open()
    '        adapter.Fill(dt)
    '        DataGridView1.AutoGenerateColumns = False
    '        DataGridView1.DataSource = dt

    '        Dim dbtitlecommand As New OleDbCommand
    '        dbtitlecommand.Connection = connecta
    '        dbtitlecommand.CommandText = Sqltitle
    '        changedbt.Text = (dbtitlecommand.ExecuteScalar)
    '        connecta.Close()
    '        TabControl1.Visible = True
    '        changedbt.Visible = True
    '        Button1.Visible = True
    '        dblocation.Text = My.Settings.MyDefdata
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        MsgBox("خطأ اثناء فتح قاعدة البيانات الافتراضية")

    '    End Try
    '    Call Autoserial()
    '    '  Call AUTOGENERATE()
    'End Sub
    Sub OpenDevy()



        TabControl1.Visible = True
        GroupBox1.Visible = True
        changedbt.Visible = True
        Button1.Visible = True
        avvocatolbl.Visible = True
        Loginbtn.Visible = True
        Logoutbtn.Visible = True
        GroupBox1.Visible = True
        loggeduser.Visible = True
        Label1.Visible = True
        avvocatolbl.Visible = True
        Label3.Visible = True
        SQCBX.Visible = True
        'Write Titles
        Try
            Dim dbtitlecommand As New OleDbCommand
            connecta.Open()
            dbtitlecommand.Connection = connecta
            dbtitlecommand.CommandText = Sqltitle
            changedbt.Text = (dbtitlecommand.ExecuteScalar)
            avvocatolbl.Text = My.Settings.CurrentAvvocato
            connecta.Close()
        Catch ex As Exception
            MsgBox(ex.Message & "قاعدة البيانات لا يوجد بها البيانات المطلوبة")

            Call AUTOGENERATE()
            DataGridView1.Refresh()
        End Try
        Loginbtn.Visible = False
        Logoutbtn.Visible = True
        If DGVhasChanged = True Then Call Saveupdated()
        Call AUTOGENERATE()
    End Sub
    Sub CloseDevy()
        If (connecta.State = ConnectionState.Open) Then connecta.Close()
        TabControl1.Visible = False
        changedbt.Visible = False
        Button1.Visible = False
        Loginbtn.Visible = True
        Logoutbtn.Visible = False
        GroupBox1.Visible = False
        loggeduser.Visible = False
        Label1.Visible = False
        avvocatolbl.Visible = False
        Label3.Visible = False
        SQCBX.Visible = False
        sq.Visible = False
        sqt.Visible = False

    End Sub



    Private Sub Savebtn_Click(sender As Object, e As EventArgs) Handles Savebtn.Click
        'start declear
        Dim mystr As String = "11/11/2018"
        Dim dayOfWeek = CInt(DateTime.Today.DayOfWeek)
        Dim SCW = Format(DateTime.Today.AddDays(-1 * dayOfWeek), "MM/dd/yyyy")
        Dim ECW = Format(DateTime.Today.AddDays(4 - dayOfWeek), "MM/dd/yyyy")
        Dim SNW = Format(DateTime.Today.AddDays(7 - dayOfWeek), "MM/dd/yyyy")
        Dim ENW = Format(DateTime.Today.AddDays(11 - dayOfWeek), "MM/dd/yyyy")
        Dim SANW = Format(DateTime.Today.AddDays(14 - dayOfWeek), "MM/dd/yyyy")
        Dim EANW = Format(DateTime.Today.AddDays(18 - dayOfWeek), "MM/dd/yyyy")
        Dim SCM = Format(DateAdd("d", 1 - DatePart("d", Today()), Today()), "MM/dd/yyyy")
        Dim ECM = Format(New Date(Now.Year, Now.Month, Date.DaysInMonth(Now.Year, Now.Month)), "MM/dd/yyyy")
        Dim SNM = Format(Now.AddDays((Now.Day - 1) * -1).AddMonths(1), "MM/dd/yyyy")
        Dim ENM = Format(New Date(Now.Year, Now.Month + 1, Date.DaysInMonth(Now.Year, Now.Month + 1)), "MM/dd/yyyy")

        Dim rpt As New CrystalReport3
        Dim avvocatovalue = " and (((CurCases.avvocato)))= '" & My.Settings.CurrentAvvocato & "'"
        Dim avvocatovalueall = " where (((CurCases.avvocato)))= '" & My.Settings.CurrentAvvocato & "'"
        Dim selectSqlall = "select * from CurCases " & avvocatovalueall & " ; "
        Dim Sqltitle = "SELECT Config.[DbTitle] FROM Config where id =1 ;"
        Dim sqlcombobox = "SELECT curcases.[avvocato] FROM curcases  ;"
        Dim Sqldone = "SELECT * FROM curcases WHERE (((curcases.[Done])=yes))" & avvocatovalue & " ; "
        Dim Sqlnotdone = "SELECT * FROM curcases WHERE (((curcases.[Done])=no))" & avvocatovalue & " ; "
        Dim SqlDelayed = "SELECT * FROM CurCases WHERE (((curcases.[Delayed])=yes))" & avvocatovalue & " ; "
        Dim SqlCW = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SCW & "# And #" & ECW & "#))" & avvocatovalue & " ; "
        Dim SqlNW = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SNW & "# And #" & ENW & "#))" & avvocatovalue & " ; "
        Dim SqlANW = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SANW & "# And #" & EANW & "#))" & avvocatovalue & " ; "
        Dim SqlCM = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SCM & "# And #" & ECM & "#))" & avvocatovalue & " ; "
        Dim SqlNM = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SNM & "# And #" & ENM & "#))" & avvocatovalue & " ; "
        Dim adapter, doneadapter, notdoneadapter, delayedadapter, Cwadapter, NWadapter, ANWadapter, CMadapter, NMadapter As OleDbDataAdapter
        Dim dt, donedt, notdonedt, delayeddt, CWdt, NWdt, Anwdt, Cmdt, NMdt As New DataTable
        Dim dbname As String = "D:\my dbs\devy.mdb"
        Dim connecta As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & My.Settings.MyDefdata)

        dt = DataGridView1.DataSource
        donedt = DataGridView3.DataSource
        notdonedt = DataGridView2.DataSource
        delayeddt = DataGridViewDL.DataSource
        CWdt = DataGridViewCW.DataSource
        NWdt = DataGridViewNW.DataSource
        Anwdt = DataGridViewANW.DataSource
        Cmdt = DataGridViewCM.DataSource
        NMdt = DataGridViewNM.DataSource

        'done declear
        If (connecta.State = ConnectionState.Open) Then connecta.Close()

        Try
            ''AllTableSave
            If TabControl1.SelectedIndex = 0 Then
                adapter = New OleDbDataAdapter(SqlNM, connecta)

                Dim scb = New OleDbCommandBuilder(adapter)
                '   MsgBox(scb.GetInsertCommand.CommandText.ToString())
                adapter.Update(dt)

            End If

            If TabControl1.SelectedIndex = 2 Then
                doneadapter = New OleDbDataAdapter(SqlNM, connecta)
                Dim donescb = New OleDbCommandBuilder(doneadapter)
                '  MsgBox(scb.GetInsertCommand.CommandText.ToString())
                doneadapter.Update(donedt)
            End If
            If TabControl1 Is DelayedCases Then
                delayedadapter = New OleDbDataAdapter(SqlDelayed, connecta)
                Dim delayedcb = New OleDbCommandBuilder(delayedadapter)
                '  MsgBox(scb.GetInsertCommand.CommandText.ToString())
                delayedadapter.Update(delayeddt)
            End If

            'save all current
            If TabControl2.SelectedTab Is allcurrent Then
                notdoneadapter = New OleDbDataAdapter(SqlNM, connecta)
                Dim Allcuole = New OleDbCommandBuilder(notdoneadapter)
                '   MsgBox(scb.GetInsertCommand.CommandText.ToString())
                notdoneadapter.Update(notdonedt)
            End If
            ' con.Close()
            'save CW
            If TabControl2.SelectedTab Is CW Then
                Cwadapter = New OleDbDataAdapter(SqlNM, connecta)

                Dim cwole = New OleDbCommandBuilder(Cwadapter)
                '   MsgBox(scb.GetInsertCommand.CommandText.ToString())
                Cwadapter.Update(CWdt)
            End If
            'save nw
            If TabControl2.SelectedTab Is NW Then
                NWadapter = New OleDbDataAdapter(SqlNM, connecta)
                Dim nwole = New OleDbCommandBuilder(NWadapter)
                '   MsgBox(scb.GetInsertCommand.CommandText.ToString())
                NWadapter.Update(NWdt)
            End If
            'save anw
            If TabControl2.SelectedTab Is ANW Then
                ANWadapter = New OleDbDataAdapter(SqlNM, connecta)

                Dim anwole = New OleDbCommandBuilder(ANWadapter)
                '   MsgBox(scb.GetInsertCommand.CommandText.ToString())
                ANWadapter.Update(Anwdt)
            End If
            'save cm
            If TabControl2.SelectedTab Is CM Then
                CMadapter = New OleDbDataAdapter(SqlNM, connecta)
                Dim cmole = New OleDbCommandBuilder(CMadapter)
                '   MsgBox(scb.GetInsertCommand.CommandText.ToString())
                CMadapter.Update(Cmdt)
            End If
            'save nm
            If TabControl2.SelectedTab Is NM Then

                NMadapter = New OleDbDataAdapter(SqlNM, connecta)
                Dim nmole = New OleDbCommandBuilder(NMadapter)
                '   MsgBox(scb.GetInsertCommand.CommandText.ToString())
                NMadapter.Update(NMdt)


            End If

            MsgBox("تم حفظ البيانات بنجاح")



        Catch ex As Exception



            'Dim ada As New OleDbDataAdapter

            'Dim scb = New OleDbCommandBuilder(ada)

            MsgBox("  حدث خطأ اثناء عملية الحفظ   " & ex.Message)
            '  MsgBox(scb.GetInsertCommand.CommandText.ToString())
            'MsgBox("  حدث خطأ اثناء عملية الحفظ   " & vbNewLine & ex.Message & vbNewLine & "Sent SQl Query:" & vbNewLine & scb.GetInsertCommand.CommandText.ToString(), Title:="Error Handling Screen")
            '  MessageBox.Show(ex.Message)
            ' con.Close()
        End Try




        Call AUTOGENERATE()
        Call Autoserial()

    End Sub
    Sub AUTOGENERATE()
        RemoveHandler DataGridView1.CellValueChanged, AddressOf DataGridView1_CellValueChanged
        RemoveHandler DataGridView2.CellValueChanged, AddressOf DataGridView2_CellValueChanged
        RemoveHandler DataGridView3.CellValueChanged, AddressOf DataGridView3_CellValueChanged
        RemoveHandler DataGridViewDL.CellValueChanged, AddressOf DataGridViewDL_CellValueChanged
        RemoveHandler DataGridViewCW.CellValueChanged, AddressOf DataGridViewCW_CellValueChanged
        RemoveHandler DataGridViewNW.CellValueChanged, AddressOf DataGridViewnw_CellValueChanged
        RemoveHandler DataGridViewANW.CellValueChanged, AddressOf DataGridViewanw_CellValueChanged
        RemoveHandler DataGridViewCM.CellValueChanged, AddressOf DataGridViewcm_CellValueChanged
        RemoveHandler DataGridViewNM.CellValueChanged, AddressOf DataGridViewnm_CellValueChanged
        'start declear

        Dim mystr As String = "11/11/2018"
        Dim dayOfWeek = CInt(DateTime.Today.DayOfWeek)
        Dim SCW = Format(DateTime.Today.AddDays(-1 * dayOfWeek), "MM/dd/yyyy")
        Dim ECW = Format(DateTime.Today.AddDays(4 - dayOfWeek), "MM/dd/yyyy")
        Dim SNW = Format(DateTime.Today.AddDays(7 - dayOfWeek), "MM/dd/yyyy")
        Dim ENW = Format(DateTime.Today.AddDays(11 - dayOfWeek), "MM/dd/yyyy")
        Dim SANW = Format(DateTime.Today.AddDays(14 - dayOfWeek), "MM/dd/yyyy")
        Dim EANW = Format(DateTime.Today.AddDays(18 - dayOfWeek), "MM/dd/yyyy")
        Dim SCM = Format(DateAdd("d", 1 - DatePart("d", Today()), Today()), "MM/dd/yyyy")
        Dim ECM = Format(New Date(Now.Year, Now.Month, Date.DaysInMonth(Now.Year, Now.Month)), "MM/dd/yyyy")
        Dim SNM = Format(Now.AddDays((Now.Day - 1) * -1).AddMonths(1), "MM/dd/yyyy")
        Dim ENM = Format(New Date(Now.Year, Now.Month + 1, Date.DaysInMonth(Now.Year, Now.Month + 1)), "MM/dd/yyyy")
        'Dim R As Integer
        'Dim CR As Integer
        'Dim lastnav As String
        Dim rpt As New CrystalReport3
        Dim avvocatovalue = " and (((CurCases.avvocato)))= '" & My.Settings.CurrentAvvocato & "'"
        Dim avvocatovalueall = " where (((CurCases.avvocato)))= '" & My.Settings.CurrentAvvocato & "'"
        Dim selectSqlall = "select * from CurCases " & avvocatovalueall & " ; "
        Dim Sqltitle = "SELECT Config.[DbTitle] FROM Config where id =1 ;"
        Dim sqlcombobox = "SELECT curcases.[avvocato] FROM curcases  ;"
        Dim Sqldone = "SELECT * FROM curcases WHERE (((curcases.[Done])=yes))" & avvocatovalue & " ; "
        Dim Sqlnotdone = "SELECT * FROM curcases WHERE (((curcases.[Done])=no))" & avvocatovalue & " ; "
        Dim SqlDelayed = "SELECT * FROM CurCases WHERE (((curcases.[Delayed])=yes))" & avvocatovalue & " ; "
        Dim SqlCW = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SCW & "# And #" & ECW & "#))" & avvocatovalue & " ; "
        Dim SqlNW = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SNW & "# And #" & ENW & "#))" & avvocatovalue & " ; "
        Dim SqlANW = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SANW & "# And #" & EANW & "#))" & avvocatovalue & " ; "
        Dim SqlCM = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SCM & "# And #" & ECM & "#))" & avvocatovalue & " ; "
        Dim SqlNM = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SNM & "# And #" & ENM & "#))" & avvocatovalue & " ; "
        Dim adapter, doneadapter, notdoneadapter, delayedadapter, Cwadapter, NWadapter, ANWadapter, CMadapter, NMadapter As OleDbDataAdapter
        Dim dt, donedt, notdonedt, delayeddt, CWdt, NWdt, Anwdt, Cmdt, NMdt, comboboxdt As DataTable
        Dim dbname As String = "D:\my dbs\devy.mdb"
        Dim connecta As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & My.Settings.MyDefdata)

        'end declear



        If (connecta.State = ConnectionState.Open) Then connecta.Close()


        dt = New DataTable()
        donedt = New DataTable()
        notdonedt = New DataTable()
        delayeddt = New DataTable()
        CWdt = New DataTable()
        NWdt = New DataTable()
        Anwdt = New DataTable()
        Cmdt = New DataTable()
        NMdt = New DataTable()
        comboboxdt = New DataTable()



        Try
            connecta.Open()
            'Allcases TAB
            adapter = New OleDbDataAdapter(selectSqlall, connecta)
            adapter.Fill(dt)
            DataGridView1.AutoGenerateColumns = False
            DataGridView1.DataSource = dt
            Call Autoserial()


            'NOTDONE TAB

            notdoneadapter = New OleDbDataAdapter(Sqlnotdone, connecta)
            notdoneadapter.Fill(notdonedt)
            DataGridView2.AutoGenerateColumns = False
            DataGridView2.DataSource = notdonedt
            Call Autoserial()
            'DONE TAB
            doneadapter = New OleDbDataAdapter(Sqldone, connecta)
            doneadapter.Fill(donedt)
            DataGridView3.AutoGenerateColumns = False
            DataGridView3.DataSource = donedt
            Call Autoserial()
            'DL TAB
            delayedadapter = New OleDbDataAdapter(SqlDelayed, connecta)
            delayedadapter.Fill(delayeddt)
            DataGridViewDL.AutoGenerateColumns = False
            DataGridViewDL.DataSource = delayeddt
            Call Autoserial()

            'CW TAB
            Cwadapter = New OleDbDataAdapter(SqlCW, connecta)
            Cwadapter.Fill(CWdt)
            DataGridViewCW.AutoGenerateColumns = False
            DataGridViewCW.DataSource = CWdt
            Call Autoserial()

            'NW TAB
            NWadapter = New OleDbDataAdapter(SqlNW, connecta)
            NWadapter.Fill(NWdt)
            DataGridViewNW.AutoGenerateColumns = False
            DataGridViewNW.DataSource = NWdt
            Call Autoserial()

            'ANW TAB
            ANWadapter = New OleDbDataAdapter(SqlANW, connecta)
            ANWadapter.Fill(Anwdt)
            DataGridViewANW.AutoGenerateColumns = False
            DataGridViewANW.DataSource = Anwdt
            Call Autoserial()

            'CM TAB
            CMadapter = New OleDbDataAdapter(SqlCM, connecta)
            CMadapter.Fill(Cmdt)
            DataGridViewCM.AutoGenerateColumns = False
            DataGridViewCM.DataSource = Cmdt
            Call Autoserial()

            'NM TAB
            NMadapter = New OleDbDataAdapter(SqlNM, connecta)
            NMadapter.Fill(NMdt)
            DataGridViewNM.AutoGenerateColumns = False
            DataGridViewNM.DataSource = NMdt
            Call Autoserial()

            connecta.Close()
        Catch ex As Exception
            MsgBox("خطأ اثناء فتح قاعدة البيانات " & ex.Message)

        End Try
        Call Autoserial()

        AddHandler DataGridView1.CellValueChanged, AddressOf DataGridView1_CellValueChanged
        AddHandler DataGridView2.CellValueChanged, AddressOf DataGridView2_CellValueChanged
        AddHandler DataGridView3.CellValueChanged, AddressOf DataGridView3_CellValueChanged
        AddHandler DataGridViewDL.CellValueChanged, AddressOf DataGridViewDL_CellValueChanged
        AddHandler DataGridViewCW.CellValueChanged, AddressOf DataGridViewCW_CellValueChanged
        AddHandler DataGridViewNW.CellValueChanged, AddressOf DataGridViewnw_CellValueChanged
        AddHandler DataGridViewANW.CellValueChanged, AddressOf DataGridViewanw_CellValueChanged
        AddHandler DataGridViewCM.CellValueChanged, AddressOf DataGridViewcm_CellValueChanged
        AddHandler DataGridViewNM.CellValueChanged, AddressOf DataGridViewnm_CellValueChanged

        If SQCBX.Checked = True Then
            sq.Visible = True
            sqt.Visible = True

            mystr = "11/11/2018"
            dayOfWeek = CInt(DateTime.Today.DayOfWeek)
            SCW = Format(DateTime.Today.AddDays(-1 * dayOfWeek), "MM/dd/yyyy")
            ECW = Format(DateTime.Today.AddDays(4 - dayOfWeek), "MM/dd/yyyy")
            SNW = Format(DateTime.Today.AddDays(7 - dayOfWeek), "MM/dd/yyyy")
            ENW = Format(DateTime.Today.AddDays(11 - dayOfWeek), "MM/dd/yyyy")
            SANW = Format(DateTime.Today.AddDays(14 - dayOfWeek), "MM/dd/yyyy")
            EANW = Format(DateTime.Today.AddDays(18 - dayOfWeek), "MM/dd/yyyy")
            SCM = Format(DateAdd("d", 1 - DatePart("d", Today()), Today()), "MM/dd/yyyy")
            ECM = Format(New Date(Now.Year, Now.Month, Date.DaysInMonth(Now.Year, Now.Month)), "MM/dd/yyyy")
            SNM = Format(Now.AddDays((Now.Day - 1) * -1).AddMonths(1), "MM/dd/yyyy")
            ENM = Format(New Date(Now.Year, Now.Month + 1, Date.DaysInMonth(Now.Year, Now.Month + 1)), "MM/dd/yyyy")
            avvocatovalue = " and (((CurCases.avvocato)))= '" & My.Settings.CurrentAvvocato & "'"
            avvocatovalueall = " where (((CurCases.avvocato)))= '" & My.Settings.CurrentAvvocato & "'"
            selectSqlall = "select * from CurCases " & avvocatovalueall & " ; "
            Sqltitle = "SELECT Config.[DbTitle] FROM Config where id =1 ;"
            sqlcombobox = "SELECT curcases.[avvocato] FROM curcases  ;"
            Sqldone = "SELECT * FROM curcases WHERE (((curcases.[Done])=yes))" & avvocatovalue & " ; "
            Sqlnotdone = "SELECT * FROM curcases WHERE (((curcases.[Done])=no))" & avvocatovalue & " ; "
            SqlDelayed = "SELECT * FROM CurCases WHERE (((curcases.[Delayed])=yes))" & avvocatovalue & " ; "
            SqlCW = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SCW & "# And #" & ECW & "#))" & avvocatovalue & " ; "
            SqlNW = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SNW & "# And #" & ENW & "#))" & avvocatovalue & " ; "
            SqlANW = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SANW & "# And #" & EANW & "#))" & avvocatovalue & " ; "
            SqlCM = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SCM & "# And #" & ECM & "#))" & avvocatovalue & " ; "
            SqlNM = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SNM & "# And #" & ENM & "#))" & avvocatovalue & " ; "

            'end declear

            '   Dim Expv1 As String = "Ahmed Rashwan Expval"
            If (connecta.State = ConnectionState.Open) Then connecta.Close()
            Dim selectSql As String = ""
            Dim ReportAddress As String = "كشف عام"
            If TabControl1.SelectedTab Is Allcases Then
                selectSql = selectSqlall
                ReportAddress = "كشف تفصيلي بكل القضايا المتداولة والمحفوظة والمؤجلة "

            End If
            If TabControl1.SelectedTab Is allcurrentcases Then
                selectSql = Sqlnotdone
                ReportAddress = " كشف تفصيلي بكل القضايا المتداولة"
            End If
            If TabControl1.SelectedTab Is allsavedcases Then
                selectSql = Sqldone
                ReportAddress = " كشف تفصيلي بكل القضايا المحكوم فيها"
            End If

            If TabControl1.SelectedTab Is DelayedCases Then
                selectSql = SqlDelayed
                ReportAddress = " كشف تفصيلي بكل القضايا المؤجلة"
            End If

            'If TabControl2.SelectedTab Is allcurrent Then
            '    selectSql = Sqlnotdone
            '    ReportAddress = " كشف تفصيلي بكل القضايا المتداولة"
            'End If
            If TabControl2.SelectedTab Is CW And TabControl1.SelectedTab Is allcurrentcases Then
                selectSql = SqlCW
                ReportAddress = " كشف تفصيلي بكل القضايا التى ستنظر الاسبوع الحالي "
            End If
            If TabControl2.SelectedTab Is NW And TabControl1.SelectedTab Is allcurrentcases Then
                selectSql = SqlNW
                ReportAddress = " كشف تفصيلي بكل القضايا التى ستنظر الاسبوع القادم"
            End If
            If TabControl2.SelectedTab Is ANW And TabControl1.SelectedTab Is allcurrentcases Then
                selectSql = SqlANW
                ReportAddress = " كشف تفصيلي بكل القضايا التي ستنظر الاسبوع بعد القادم"
            End If
            If TabControl2.SelectedTab Is CM And TabControl1.SelectedTab Is allcurrentcases Then
                selectSql = SqlCM
                ReportAddress = " كشف تفصيلي بكل القضايا التي تنظر هذا الشهر"
            End If
            If TabControl2.SelectedTab Is NM And TabControl1.SelectedTab Is allcurrentcases Then
                selectSql = SqlNM
                ReportAddress = " كشف تفصيلي بكل القضايا التى ستنظر الشهر القادم"
            End If
            sq.Text = selectSql
            sqt.Text = ReportAddress

        Else
            sq.Visible = False
            sqt.Visible = False


        End If



    End Sub


    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If DGVhasChanged = True Then Call Saveupdated()
        Call AUTOGENERATE()
    End Sub
    Private Sub TabControl2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl2.SelectedIndexChanged
        If DGVhasChanged = True Then Call Saveupdated()
        Call AUTOGENERATE()
    End Sub
    Sub Saveupdated()
        'start declear

        Dim mystr As String = "11/11/2018"
        Dim dayOfWeek = CInt(DateTime.Today.DayOfWeek)
        Dim SCW = Format(DateTime.Today.AddDays(-1 * dayOfWeek), "MM/dd/yyyy")
        Dim ECW = Format(DateTime.Today.AddDays(4 - dayOfWeek), "MM/dd/yyyy")
        Dim SNW = Format(DateTime.Today.AddDays(7 - dayOfWeek), "MM/dd/yyyy")
        Dim ENW = Format(DateTime.Today.AddDays(11 - dayOfWeek), "MM/dd/yyyy")
        Dim SANW = Format(DateTime.Today.AddDays(14 - dayOfWeek), "MM/dd/yyyy")
        Dim EANW = Format(DateTime.Today.AddDays(18 - dayOfWeek), "MM/dd/yyyy")
        Dim SCM = Format(DateAdd("d", 1 - DatePart("d", Today()), Today()), "MM/dd/yyyy")
        Dim ECM = Format(New Date(Now.Year, Now.Month, Date.DaysInMonth(Now.Year, Now.Month)), "MM/dd/yyyy")
        Dim SNM = Format(Now.AddDays((Now.Day - 1) * -1).AddMonths(1), "MM/dd/yyyy")
        Dim ENM = Format(New Date(Now.Year, Now.Month + 1, Date.DaysInMonth(Now.Year, Now.Month + 1)), "MM/dd/yyyy")
        'Dim R As Integer
        'Dim CR As Integer
        'Dim lastnav As String
        Dim rpt As New CrystalReport3
        Dim avvocatovalue = " and (((CurCases.avvocato)))= '" & My.Settings.CurrentAvvocato & "'"
        Dim avvocatovalueall = " where (((CurCases.avvocato)))= '" & My.Settings.CurrentAvvocato & "'"
        Dim selectSqlall = "select * from CurCases " & avvocatovalueall & " ; "
        Dim Sqltitle = "SELECT Config.[DbTitle] FROM Config where id =1 ;"
        Dim sqlcombobox = "SELECT curcases.[avvocato] FROM curcases  ;"
        Dim Sqldone = "SELECT * FROM curcases WHERE (((curcases.[Done])=yes))" & avvocatovalue & " ; "
        Dim Sqlnotdone = "SELECT * FROM curcases WHERE (((curcases.[Done])=no))" & avvocatovalue & " ; "
        Dim SqlDelayed = "SELECT * FROM CurCases WHERE (((curcases.[Delayed])=yes))" & avvocatovalue & " ; "
        Dim SqlCW = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SCW & "# And #" & ECW & "#))" & avvocatovalue & " ; "
        Dim SqlNW = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SNW & "# And #" & ENW & "#))" & avvocatovalue & " ; "
        Dim SqlANW = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SANW & "# And #" & EANW & "#))" & avvocatovalue & " ; "
        Dim SqlCM = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SCM & "# And #" & ECM & "#))" & avvocatovalue & " ; "
        Dim SqlNM = "SELECT * FROM CurCases WHERE (((curcases.[Done])=no)) and (((CurCases.Sessdate) Between #" & SNM & "# And #" & ENM & "#))" & avvocatovalue & " ; "
        Dim adapter, doneadapter, notdoneadapter, delayedadapter, Cwadapter, NWadapter, ANWadapter, CMadapter, NMadapter As OleDbDataAdapter
        Dim dt, donedt, notdonedt, delayeddt, CWdt, NWdt, Anwdt, Cmdt, NMdt As DataTable
        Dim dbname As String = "D:\my dbs\devy.mdb"
        Dim connecta As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & My.Settings.MyDefdata)

        dt = New DataTable()
        donedt = New DataTable()
        notdonedt = New DataTable()
        delayeddt = New DataTable()
        CWdt = New DataTable()
        NWdt = New DataTable()
        Anwdt = New DataTable()
        Cmdt = New DataTable()
        NMdt = New DataTable()


        'end declear


        If (connecta.State = ConnectionState.Open) Then connecta.Close()

        Try
            ''AllTableSave
            If TabControl1.SelectedIndex = 0 Then
                adapter = New OleDbDataAdapter(SqlNM, connecta)

                Dim scb = New OleDbCommandBuilder(adapter)
                '   MsgBox(scb.GetInsertCommand.CommandText.ToString())
                adapter.Update(dt)

            End If
            ' con.Close()
            'DoneTableSave
            If TabControl1.SelectedIndex = 2 Then
                doneadapter = New OleDbDataAdapter(SqlNM, connecta)
                Dim donescb = New OleDbCommandBuilder(doneadapter)
                '  MsgBox(scb.GetInsertCommand.CommandText.ToString())
                doneadapter.Update(donedt)
            End If
            If TabControl1.SelectedIndex = 3 Then
                delayedadapter = New OleDbDataAdapter(SqlDelayed, connecta)
                Dim delayedcb = New OleDbCommandBuilder(delayedadapter)
                '  MsgBox(scb.GetInsertCommand.CommandText.ToString())
                delayedadapter.Update(delayeddt)
            End If

            'save all current
            If TabControl2.SelectedTab Is allcurrent Then
                notdoneadapter = New OleDbDataAdapter(SqlNM, connecta)
                Dim Allcuole = New OleDbCommandBuilder(notdoneadapter)
                '   MsgBox(scb.GetInsertCommand.CommandText.ToString())
                notdoneadapter.Update(notdonedt)
            End If
            ' con.Close()
            'save CW
            If TabControl2.SelectedTab Is CW Then
                Cwadapter = New OleDbDataAdapter(SqlNM, connecta)

                Dim cwole = New OleDbCommandBuilder(Cwadapter)
                '   MsgBox(scb.GetInsertCommand.CommandText.ToString())
                Cwadapter.Update(CWdt)
            End If
            'save nw
            If TabControl2.SelectedTab Is NW Then
                NWadapter = New OleDbDataAdapter(SqlNM, connecta)
                Dim nwole = New OleDbCommandBuilder(NWadapter)
                '   MsgBox(scb.GetInsertCommand.CommandText.ToString())
                NWadapter.Update(NWdt)
            End If
            'save anw
            If TabControl2.SelectedTab Is ANW Then
                ANWadapter = New OleDbDataAdapter(SqlNM, connecta)

                Dim anwole = New OleDbCommandBuilder(ANWadapter)
                '   MsgBox(scb.GetInsertCommand.CommandText.ToString())
                ANWadapter.Update(Anwdt)
            End If
            'save cm
            If TabControl2.SelectedTab Is CM Then
                CMadapter = New OleDbDataAdapter(SqlNM, connecta)
                Dim cmole = New OleDbCommandBuilder(CMadapter)
                '   MsgBox(scb.GetInsertCommand.CommandText.ToString())
                CMadapter.Update(Cmdt)
            End If
            'save nm
            If TabControl2.SelectedTab Is NM Then

                NMadapter = New OleDbDataAdapter(SqlNM, connecta)
                Dim nmole = New OleDbCommandBuilder(NMadapter)
                '   MsgBox(scb.GetInsertCommand.CommandText.ToString())
                NMadapter.Update(NMdt)


            End If

            ' con.Close()
            'con.Close()
            ' MessageBox.Show(" تم الحفظ بنجاح " & TabControl1.SelectedIndex.ToString)
            '  MsgBox(TabControl1.SelectedIndex.ToString)
            'Catch ex As NoNullAllowedException
        Catch EX As Exception

            'Dim scb = New OleDbCommandBuilder(adapter)

            'MsgBox("حدث خطا")
            'MsgBox(scb.GetInsertCommand.CommandText.ToString())
            ' MsgBox("  حدث خطأ اثناء عملية الحفظ   " & vbNewLine & ex.Message & vbNewLine & "Sent SQl Query:" & vbNewLine & scb.GetInsertCommand.CommandText.ToString(), Title:="Error Handling Screen")
            '  MessageBox.Show(ex.Message)
            '  Catch exe As NotImplementedException
            '     MsgBox("حدث خطأ غير معالج")
        End Try




        ' Call AUTOGENERATE()
        'Call Autoserial()
        ' End If
    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        If CheckBox2.Enabled = True Then Call Saveupdated()


    End Sub
    Private Sub DataGridView2_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
        If CheckBox2.Enabled = True Then Call Saveupdated()
    End Sub
    Private Sub DataGridView3_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
        If CheckBox2.Enabled = True Then Call Saveupdated()
    End Sub

    Private Sub DataGridView1_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellLeave
        If CheckBox2.Enabled = True Then Call Saveupdated()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        changedbt.ReadOnly = False
        changedbt.Enabled = True
        doeditbt.Visible = True
        Button1.Visible = False


    End Sub

    Private Sub Loginbtn_Click(sender As Object, e As EventArgs) Handles Loginbtn.Click
        LoginForm.Show()
    End Sub

    Private Sub changedbt_TextChanged(sender As Object, e As EventArgs) Handles changedbt.TextChanged

    End Sub

    Private Sub doeditbt_Click(sender As Object, e As EventArgs) Handles doeditbt.Click

        If (connecta.State = ConnectionState.Open) Then connecta.Close()
        connecta.Open()
        Dim dbtitlecommand As New OleDbCommand
        dbtitlecommand.Connection = connecta
        dbtitlecommand.CommandText = "update Config  set [dbtitle]=  ' " & changedbt.Text & " ' where  (((config.[id])=1));"
        dbtitlecommand.ExecuteScalar()
        connecta.Close()
        changedbt.ReadOnly = True
        changedbt.Enabled = False
        doeditbt.Visible = False
        Button1.Visible = True

    End Sub

    Private Sub Logoutbtn_Click(sender As Object, e As EventArgs) Handles Logoutbtn.Click
        Call CloseDevy()
        LoginForm.Show()
    End Sub



    Private Sub RestartBt_Click(sender As Object, e As EventArgs) Handles RestartBt.Click
        If CheckBox2.Enabled = True Then Call Saveupdated()

        Application.Restart()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub DataGridViewCM_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewCM.CellContentClick

    End Sub

    Private Sub DataGridViewCW_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewCW.CellContentClick

    End Sub



    Private Sub DataGridViewNW_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewNW.CellContentClick

    End Sub

    Private Sub avvocatolbl_Click(sender As Object, e As EventArgs) Handles avvocatolbl.Click

    End Sub

    Private Sub DataGridViewANW_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewANW.CellContentClick

    End Sub

    Private Sub DataGridView3_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs)
        If CheckBox2.Enabled = True Then Call Saveupdated()

    End Sub



    Private Sub DataGridView1_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles DataGridView1.UserDeletingRow
        If Not MessageBox.Show("هل تريد فعلا مسح الصف؟", "عملية مسح", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            e.Cancel = True
        End If
    End Sub
    Private Sub DataGridView2_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs)
        If Not MessageBox.Show("هل تريد فعلا مسح الصف؟", "عملية مسح", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            e.Cancel = True
        End If
    End Sub
    Private Sub DataGridView3_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs)
        If Not MessageBox.Show("هل تريد فعلا مسح الصف؟", "عملية مسح", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            e.Cancel = True
        End If
    End Sub
    Private Sub DataGridViewcw_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs)
        If Not MessageBox.Show("هل تريد فعلا مسح الصف؟", "عملية مسح", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            e.Cancel = True
        End If
    End Sub
    Private Sub DataGridViewNw_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs)
        If Not MessageBox.Show("هل تريد فعلا مسح الصف؟", "عملية مسح", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            e.Cancel = True
        End If
    End Sub
    Private Sub DataGridViewANw_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs)
        If Not MessageBox.Show("هل تريد فعلا مسح الصف؟", "عملية مسح", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            e.Cancel = True
        End If
    End Sub
    Private Sub DataGridViewDl_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs)
        If Not MessageBox.Show("هل تريد فعلا مسح الصف؟", "عملية مسح", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            e.Cancel = True
        End If
    End Sub
    Private Sub TabControl1_Deselecting(sender As Object, e As TabControlCancelEventArgs) Handles TabControl1.Deselecting
        If CheckBox2.Enabled = True Then Call Saveupdated()
    End Sub
    Sub OrgnizerT()

        MsgBox(SCW)
        MsgBox(ECW)
    End Sub
    Dim DGVhasChanged As Boolean

    Private Sub testa()

        DGVhasChanged = False

        ' //Do stuff to populate the DataGridView

    End Sub



    '//This example check for changes on Form closing but you can check it on any other event (e.g When a button Is clicked)
    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If DGVhasChanged = True Then
            Dim response As MsgBoxResult
            response = MsgBox("Do you want to save the changes?", MsgBoxStyle.YesNo)
            If response = MsgBoxResult.Yes Then
                '//Do stuff to save the changes...
                Call Saveupdated()
                DGVhasChanged = False
            End If
        End If

    End Sub

    Private Sub Form1_TabIndexChanged(sender As Object, e As EventArgs) Handles Me.TabIndexChanged

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        MsgBox("Contact: Rashwanic@gmail.com" & vbNewLine & "Call +965 97875540 ")
    End Sub

    Private Sub Querymonitorchbox_CheckedChanged(sender As Object, e As EventArgs) Handles SQCBX.CheckedChanged
        If SQCBX.Checked = True Then
            sq.Visible = True
            sqt.Visible = True
            Call AUTOGENERATE()
        Else
            sq.Visible = False
            sqt.Visible = False
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    'Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
    '    '   DataGridView1.AutoSize = True
    'End Sub

    Private Sub DataGridView1_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridView1.RowsAdded
        Try

            DataGridView1.CurrentRow.Cells(14).Value = My.Settings.CurrentAvvocato

        Catch
        End Try
    End Sub
    Private Sub DataGridView2_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridView2.RowsAdded
        Try

            DataGridView2.CurrentRow.Cells(14).Value = My.Settings.CurrentAvvocato

        Catch
        End Try
    End Sub
    Private Sub DataGridView3_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridView3.RowsAdded
        Try

            DataGridView3.CurrentRow.Cells(14).Value = My.Settings.CurrentAvvocato

        Catch
        End Try
    End Sub
    Private Sub DataGridViewdl_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridViewDL.RowsAdded
        Try

            DataGridViewDL.CurrentRow.Cells(14).Value = My.Settings.CurrentAvvocato

        Catch
        End Try
    End Sub
    Private Sub DataGridViewcw_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridViewCW.RowsAdded
        Try

            DataGridViewCW.CurrentRow.Cells(14).Value = My.Settings.CurrentAvvocato


        Catch
        End Try
    End Sub
    Private Sub DataGridViewnw_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridViewNW.RowsAdded

        Try

            DataGridViewNW.CurrentRow.Cells(14).Value = My.Settings.CurrentAvvocato


        Catch
        End Try
    End Sub
    Private Sub DataGridViewanw_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridViewANW.RowsAdded
        Try

            DataGridViewANW.CurrentRow.Cells(14).Value = My.Settings.CurrentAvvocato


        Catch
        End Try
    End Sub
    Private Sub DataGridViewcm_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridViewCM.RowsAdded
        Try

            DataGridViewCM.CurrentRow.Cells(14).Value = My.Settings.CurrentAvvocato


        Catch
        End Try
    End Sub
    Private Sub DataGridViewnm_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridViewNM.RowsAdded
        Try

            DataGridViewNM.CurrentRow.Cells(14).Value = My.Settings.CurrentAvvocato


        Catch
        End Try
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged

        Try

            DataGridView1.CurrentRow.Cells(12).Value = My.Settings.CurrentUser
                DataGridView1.CurrentRow.Cells(13).Value = Now().ToString

        Catch
        End Try
    End Sub

    Private Sub DataGridView2_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellValueChanged
        Try

            DataGridView2.CurrentRow.Cells(12).Value = My.Settings.CurrentUser
                DataGridView2.CurrentRow.Cells(13).Value = Now().ToString

        Catch
        End Try
    End Sub

    Private Sub DataGridView3_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellValueChanged
        Try

            DataGridView3.CurrentRow.Cells(12).Value = My.Settings.CurrentUser
                DataGridView3.CurrentRow.Cells(13).Value = Now().ToString

        Catch
        End Try
    End Sub
    Private Sub DataGridViewDL_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewDL.CellValueChanged
        Try

            DataGridViewDL.CurrentRow.Cells(12).Value = My.Settings.CurrentUser
                DataGridViewDL.CurrentRow.Cells(13).Value = Now().ToString

        Catch
        End Try
    End Sub

    Private Sub DataGridViewCW_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewCW.CellValueChanged
        Try

            DataGridViewCW.CurrentRow.Cells(12).Value = My.Settings.CurrentUser
                DataGridViewCW.CurrentRow.Cells(13).Value = Now().ToString

        Catch
        End Try
    End Sub


    Private Sub DataGridViewnw_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewNW.CellValueChanged
        Try

            DataGridViewNW.CurrentRow.Cells(12).Value = My.Settings.CurrentUser
                DataGridViewNW.CurrentRow.Cells(13).Value = Now().ToString

        Catch
        End Try
    End Sub

    Private Sub DataGridViewanw_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewANW.CellValueChanged
        Try

            DataGridViewANW.CurrentRow.Cells(12).Value = My.Settings.CurrentUser
                DataGridViewANW.CurrentRow.Cells(13).Value = Now().ToString

        Catch
        End Try
    End Sub

    Private Sub DataGridViewcm_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewCM.CellValueChanged
        Try

            DataGridViewCM.CurrentRow.Cells(12).Value = My.Settings.CurrentUser
                DataGridViewCM.CurrentRow.Cells(13).Value = Now().ToString

        Catch
        End Try
    End Sub

    Private Sub DataGridViewnm_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewNM.CellValueChanged
        Try

            DataGridViewNM.CurrentRow.Cells(12).Value = My.Settings.CurrentUser
                DataGridViewNM.CurrentRow.Cells(13).Value = Now().ToString

        Catch
        End Try




    End Sub

    Private Sub DataGridView1_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridView1.DataError
        MsgBox("عفواً ، يرجى ادخال التاريخ بالصيغة الصحيحة" & vbNewLine & "الصيغة الصحيحة هي يوم/شهر/سنة" & vbNewLine & "مثال اذا اردنا اضافة اليوم الرابع من شهر نوفمبر لعام 2019 فانه سيكون كالتالي" & vbNewLine & "14/11/2019")
    End Sub
    Private Sub DataGridView2_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridView2.DataError
        MsgBox("عفواً ، يرجى ادخال التاريخ بالصيغة الصحيحة" & vbNewLine & "الصيغة الصحيحة هي يوم/شهر/سنة" & vbNewLine & "مثال اذا اردنا اضافة اليوم الرابع من شهر نوفمبر لعام 2019 فانه سيكون كالتالي" & vbNewLine & "14/11/2019")
    End Sub
    Private Sub DataGridView3_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridView3.DataError
        MsgBox("عفواً ، يرجى ادخال التاريخ بالصيغة الصحيحة" & vbNewLine & "الصيغة الصحيحة هي يوم/شهر/سنة" & vbNewLine & "مثال اذا اردنا اضافة اليوم الرابع من شهر نوفمبر لعام 2019 فانه سيكون كالتالي" & vbNewLine & "14/11/2019")
    End Sub
    Private Sub DataGridViewdl_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridViewDL.DataError
        MsgBox("عفواً ، يرجى ادخال التاريخ بالصيغة الصحيحة" & vbNewLine & "الصيغة الصحيحة هي يوم/شهر/سنة" & vbNewLine & "مثال اذا اردنا اضافة اليوم الرابع من شهر نوفمبر لعام 2019 فانه سيكون كالتالي" & vbNewLine & "14/11/2019")
    End Sub
    Private Sub DataGridViewcw_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridViewCW.DataError
        MsgBox("عفواً ، يرجى ادخال التاريخ بالصيغة الصحيحة" & vbNewLine & "الصيغة الصحيحة هي يوم/شهر/سنة" & vbNewLine & "مثال اذا اردنا اضافة اليوم الرابع من شهر نوفمبر لعام 2019 فانه سيكون كالتالي" & vbNewLine & "14/11/2019")
    End Sub
    Private Sub DataGridViewnw_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridViewNW.DataError
        MsgBox("عفواً ، يرجى ادخال التاريخ بالصيغة الصحيحة" & vbNewLine & "الصيغة الصحيحة هي يوم/شهر/سنة" & vbNewLine & "مثال اذا اردنا اضافة اليوم الرابع من شهر نوفمبر لعام 2019 فانه سيكون كالتالي" & vbNewLine & "14/11/2019")
    End Sub
    Private Sub DataGridViewanw_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridViewANW.DataError
        MsgBox("عفواً ، يرجى ادخال التاريخ بالصيغة الصحيحة" & vbNewLine & "الصيغة الصحيحة هي يوم/شهر/سنة" & vbNewLine & "مثال اذا اردنا اضافة اليوم الرابع من شهر نوفمبر لعام 2019 فانه سيكون كالتالي" & vbNewLine & "14/11/2019")
    End Sub
    Private Sub DataGridViewcm_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridViewCM.DataError
        MsgBox("عفواً ، يرجى ادخال التاريخ بالصيغة الصحيحة" & vbNewLine & "الصيغة الصحيحة هي يوم/شهر/سنة" & vbNewLine & "مثال اذا اردنا اضافة اليوم الرابع من شهر نوفمبر لعام 2019 فانه سيكون كالتالي" & vbNewLine & "14/11/2019")
    End Sub
    Private Sub DataGridViewnm_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridViewNM.DataError
        MsgBox("عفواً ، يرجى ادخال التاريخ بالصيغة الصحيحة" & vbNewLine & "الصيغة الصحيحة هي يوم/شهر/سنة" & vbNewLine & "مثال اذا اردنا اضافة اليوم الرابع من شهر نوفمبر لعام 2019 فانه سيكون كالتالي" & vbNewLine & "14/11/2019")
    End Sub

End Class
