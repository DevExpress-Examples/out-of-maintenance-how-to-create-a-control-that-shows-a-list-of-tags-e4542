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
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Registrator
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.Utils.Drawing
Imports System.Collections

Namespace tagsList
	<ToolboxItem(True), Designer("DevExpress.XtraEditors.Design.ButtonEditDesigner, " & AssemblyInfo.SRAssemblyEditorsDesign)> _
	Public Class Taglist
		Inherits ButtonEdit
		Public Sub tagsList()
		End Sub

		Protected Overrides Sub OnLeave(ByVal e As EventArgs)
			MyBase.OnLeave(e)
			ButtonsToEditValue()
		End Sub

		Protected Overridable Sub ButtonsToEditValue()
			Dim res As String = Nothing
			For Each t As EditorButton In ViewInfo.Item.Buttons
				If (res IsNot Nothing) AndAlso (t.Caption.Length > 1) Then
					res = String.Format("{0},{1}", t.Caption, res)
				ElseIf (res Is Nothing) AndAlso (t.Caption.Length > 1) Then
					res = t.Caption
				End If
			Next t
			Me.EditValue = res
		End Sub
		Public Overrides Overloads Function DoValidate() As Boolean
			ButtonsToEditValue()
			Return MyBase.DoValidate()
		End Function
		Protected Overrides Sub OnCreateControl()
			MyBase.OnCreateControl()
			If EditValue Is Nothing Then
				Return
			End If
			If TypeOf EditValue Is ArrayList Then
				For Each t In (TryCast(EditValue, ArrayList))
					Me.Properties.Tags.Add(New EditorButton() With {.Caption = t.ToString()})
				Next t
			End If
			If EditValue.ToString().Split(",".ToCharArray()(0)).Length > 1 Then
				Dim myTags() As String = EditValue.ToString().Split(",".ToCharArray())
				For Each tag As String In myTags
					Me.Properties.Tags.Add(New EditorButton() With {.Caption = tag})
				Next tag
			ElseIf (TypeOf EditValue Is String) AndAlso (EditValue.ToString().Length > 0) Then
				Me.Properties.Tags.Add(New EditorButton() With {.Caption = EditValue.ToString()})
			End If
			EditValue = Nothing
		End Sub

		Protected Overrides Sub OnEnter(ByVal e As EventArgs)
			MyBase.OnEnter(e)
			If EditValue Is Nothing Then
				Return
			End If
			Dim myTags() As String = EditValue.ToString().Split(",".ToCharArray())
			Text = Nothing
			If (myTags.Length>1) AndAlso (ViewInfo.Item.Tags.Count=0) Then
				For Each tag As String In myTags
					Me.Properties.Tags.Add(New EditorButton() With {.Caption = tag})
				Next tag
			End If

		End Sub

		Protected Friend Shadows ReadOnly Property ViewInfo() As TagListViewInfo
			Get
				Return TryCast(MyBase.ViewInfo, TagListViewInfo)
			End Get
		End Property
		Shared Sub New()
			TagListReposioryItem.Register()
		End Sub

		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public Shadows ReadOnly Property Properties() As TagListReposioryItem
			Get
				Return TryCast(MyBase.Properties, TagListReposioryItem)
			End Get
		End Property


		Protected Overrides Sub OnClickButton(ByVal buttonInfo As EditorButtonObjectInfoArgs)
			MyBase.OnClickButton(buttonInfo)
			EditValue = Nothing
			ViewInfo.Item.Tags.RemoveAt(buttonInfo.Button.Index)
			ButtonsToEditValue()

		End Sub

		Protected Overridable Function isKeyTagAdd(ByVal d As Keys) As Boolean
			If (d = Keys.Enter) OrElse (d = Keys.Space) OrElse (d = Keys.Oemcomma) Then
				Return True
			Else
				Return False
			End If
		End Function

		Protected Overrides Sub OnEditorKeyDown(ByVal e As KeyEventArgs)
			If (Me.EditValue IsNot Nothing) AndAlso (isKeyTagAdd(e.KeyCode)) AndAlso ((Not String.IsNullOrWhiteSpace(EditValue.ToString()))) Then
				Properties.Tags.Add(New EditorButton() With {.Caption = EditValue.ToString().Trim()})
			End If
		End Sub

		Public Sub New()
			Properties.Tags = New TagCollection()
			Properties.Tags.Parent = Me
		End Sub

		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return TagListReposioryItem.EditorName
			End Get
		End Property
	End Class
	Public Class TagCollection
		Inherits CollectionBase
		Private _Parent As ButtonEdit

		Friend Property Parent() As ButtonEdit
			Get
				Return _Parent
			End Get
			Set(ByVal value As ButtonEdit)
				_Parent = value
			End Set
		End Property
		Default Public Property Item(ByVal index As Integer) As EditorButton
			Get
				Return (TryCast(List(index), EditorButton))
			End Get
			Set(ByVal value As EditorButton)
				List(index) = value
			End Set
		End Property

		Protected Overrides Sub OnRemove(ByVal index As Integer, ByVal value As Object)
			If Parent IsNot Nothing Then
				Parent.Properties.Buttons.RemoveAt(index)
			End If
			MyBase.OnRemove(index, value)
		End Sub

		Public Sub Add(ByVal value As EditorButton)
			value.IsLeft = True
			value.Kind = ButtonPredefines.Glyph
			value.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
			If Parent IsNot Nothing Then
				Parent.Properties.Buttons.Insert(0, value)
			End If
			List.Insert(0, value)
		End Sub
	End Class



End Namespace
