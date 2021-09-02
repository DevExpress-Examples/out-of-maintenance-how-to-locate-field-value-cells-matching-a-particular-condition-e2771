Imports System
Imports System.Globalization
Imports System.Windows
Imports DevExpress.Xpf.PivotGrid

Namespace DXPivotGrid_FindCells
	Partial Public Class Window1
		Inherits Window

		Public Sub New()
			InitializeComponent()
			AddHandler pivotGrid.CustomFieldValueCells, AddressOf pivotGrid_CustomFieldValueCells
		End Sub
		Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			PivotHelper.FillPivot(pivotGrid)
			pivotGrid.DataSource = PivotHelper.GetDataTable()
			pivotGrid.BestFit()
		End Sub

		' Handles the CustomFieldValueCells event to remove columns with
		' zero summary values.
		Private Sub pivotGrid_CustomFieldValueCells(ByVal sender As Object, ByVal e As PivotCustomFieldValueCellsEventArgs)
			If pivotGrid.DataSource Is Nothing Then
				Return
			End If
			If rbDefault.IsChecked = True Then
				Return
			End If

			' Obtains the first encountered column header whose column
			' matches the specified condition, represented by a predicate.
			Dim cell As FieldValueCell = e.FindCell(True, New Predicate(Of Object())(Function(dataCellValues() As Object)
				For Each value As Object In dataCellValues
					If Not Object.Equals(CDec(0), value) Then
						Return False
					End If
				Next value
				Return True
			End Function))

			' If any column header matches the condition, this column is removed.
			If cell IsNot Nothing Then
				e.Remove(cell)
			End If
		End Sub
		Private Sub pivotGrid_FieldValueDisplayText(ByVal sender As Object, ByVal e As PivotFieldDisplayTextEventArgs)
			If e.Field = pivotGrid.Fields(PivotHelper.Month) Then
				e.DisplayText = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(CInt(Math.Truncate(e.Value)))
			End If
		End Sub
		Private Sub rbDefault_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
			pivotGrid.LayoutChanged()
		End Sub
	End Class
End Namespace
