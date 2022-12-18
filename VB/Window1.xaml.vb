Imports System
Imports System.Globalization
Imports System.Windows
Imports DevExpress.Xpf.PivotGrid

Namespace DXPivotGrid_FindCells

    Public Partial Class Window1
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            AddHandler Me.pivotGrid.CustomFieldValueCells, New PivotCustomFieldValueCellsEventHandler(AddressOf pivotGrid_CustomFieldValueCells)
        End Sub

        Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            PivotHelper.FillPivot(Me.pivotGrid)
            Me.pivotGrid.DataSource = GetDataTable()
            Me.pivotGrid.BestFit()
        End Sub

        ' Handles the CustomFieldValueCells event to remove columns with
        ' zero summary values.
        Private Sub pivotGrid_CustomFieldValueCells(ByVal sender As Object, ByVal e As PivotCustomFieldValueCellsEventArgs)
            If Me.pivotGrid.DataSource Is Nothing Then Return
            If Me.rbDefault.IsChecked = True Then Return
            ' Obtains the first encountered column header whose column
            ' matches the specified condition, represented by a predicate.
            ' Defines the predicate returning true for columns
            ' that contain only zero summary values.
            Dim cell As FieldValueCell = e.FindCell(True, New Predicate(Of Object())(Function(ByVal dataCellValues)
                For Each value As Object In dataCellValues
                    If Not Equals(CDec(0), value) Then Return False
                Next

                Return True
            End Function))
            ' If any column header matches the condition, this column is removed.
            If cell IsNot Nothing Then e.Remove(cell)
        End Sub

        Private Sub pivotGrid_FieldValueDisplayText(ByVal sender As Object, ByVal e As PivotFieldDisplayTextEventArgs)
            If e.Field Is Me.pivotGrid.Fields(Month) Then
                e.DisplayText = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(CInt(e.Value))
            End If
        End Sub

        Private Sub rbDefault_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.pivotGrid.LayoutChanged()
        End Sub
    End Class
End Namespace
