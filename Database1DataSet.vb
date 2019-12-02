Partial Class Database1DataSet
    Partial Public Class DoneCasesDataTable
        Private Sub DoneCasesDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.SessionColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class
End Class


