' Developer Express Code Central Example:
' How to create a control that shows a list of tags
' 
' This example demonstrates how to create a TagList control editor that supports
' both standalone and in-place modes.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E4542


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid

Namespace tagsList
	Partial Public Class XtraForm1
		Inherits DevExpress.XtraEditors.XtraForm
		Public Sub New()
			InitializeComponent()
			Dim t As String = "Haskell,Ocaml,Scala,Ruby"
			taglist1.EditValue = t
			Dim ds As New BindingList(Of myDataSource)()
			For i As Integer = 0 To 9
				If i < 5 Then
					ds.Add(New myDataSource() With {.Langs = "Perl,Python"})
				Else
					ds.Add(New myDataSource() With {.Langs = "Perl,Python,Java"})
				End If
			Next i
			gridControl1.DataSource = ds
			TryCast(gridControl1.MainView, GridView).Columns("Langs").ColumnEdit = New TagListReposioryItem()
		End Sub

		Protected Overrides Sub OnLoad(ByVal e As EventArgs)
			MyBase.OnLoad(e)
		End Sub
	End Class
	Public Class myDataSource
		Private _Langs As String

		Public Property Langs() As String
			Get
				Return _Langs
			End Get
			Set(ByVal value As String)
				_Langs = value
			End Set
		End Property
	End Class
End Namespace