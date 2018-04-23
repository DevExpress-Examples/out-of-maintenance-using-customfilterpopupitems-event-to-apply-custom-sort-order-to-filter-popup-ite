Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraPivotGrid
Imports DevExpress.XtraPivotGrid.Data

Namespace WindowsApplication1
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Function CreateDataView() As DataView
			Dim table As New DataTable()
			table.Columns.Add("Category", GetType(String))
			table.Columns.Add("Product", GetType(String))
			table.Columns.Add("Count", GetType(Integer))

			table.Rows.Add("Beverages", "Juice", 10)
			table.Rows.Add("Beverages", "Juice", 15)
			table.Rows.Add("Beverages", "Vermouth", 150)
			table.Rows.Add("Beverages", "Vermouth", 160)
			table.Rows.Add("Meat", "Beef", 80)
			table.Rows.Add("Meat", "Beef", 150)
			table.Rows.Add("Meat", "Sausage", 40)
			table.Rows.Add("Meat", "Sausage", 48)
			table.Rows.Add("Milk", "Kefir", 24)
			table.Rows.Add("Milk", "Kumiss", 54)
			table.Rows.Add("Milk", "Curd", 40)
			table.Rows.Add("Milk", "Curd", 20)
			table.Rows.Add("Milk", "Cheese", 50)
			table.Rows.Add("Milk", "Cheese", 120)
			table.Rows.Add("Milk", "Cheese", 150)
			Return table.DefaultView
		End Function

		Private columnField As PivotGridField
		Private rowField As PivotGridField
		Private dataField As PivotGridField

		Private Sub AddFields()
			columnField = pivotGridControl1.Fields.Add("Category", PivotArea.ColumnArea)
			rowField = pivotGridControl1.Fields.Add("Product", PivotArea.RowArea)
			rowField.SortMode = PivotSortMode.Custom
			dataField = pivotGridControl1.Fields.Add("Count", PivotArea.DataArea)
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			pivotGridControl1.DataSource = CreateDataView()
			AddFields()
		End Sub

		Private Sub pivotGridControl1_CustomFieldSort(ByVal sender As Object, ByVal e As PivotGridCustomFieldSortEventArgs) Handles pivotGridControl1.CustomFieldSort
			If (Not rowField.Equals(e.Field)) Then
				Return
			End If
			Dim value1 As String = e.Value1.ToString()
			Dim value2 As String = e.Value2.ToString()
			Dim length1 As Integer = value1.Length
			Dim length2 As Integer = value2.Length
			e.Result = Comparer(Of Integer).Default.Compare(length1, length2)
			If e.Result = 0 Then
				e.Result = Comparer(Of String).Default.Compare(value1, value2)
			End If
			e.Handled = True
		End Sub

		Private Sub pivotGridControl1_CustomFilterPopupItems(ByVal sender As Object, ByVal e As PivotCustomFilterPopupItemsEventArgs) Handles pivotGridControl1.CustomFilterPopupItems
			If (Not rowField.Equals(e.Field)) Then
				Return
			End If
			If rowField.SortOrder = PivotSortOrder.Ascending Then
				Return
			End If
			Dim items As New List(Of PivotGridFilterItem)(e.Items)
			items.Reverse()
			e.Items.Clear()
			For Each item As PivotGridFilterItem In items
				e.Items.Add(item)
			Next item
		End Sub
	End Class
End Namespace
