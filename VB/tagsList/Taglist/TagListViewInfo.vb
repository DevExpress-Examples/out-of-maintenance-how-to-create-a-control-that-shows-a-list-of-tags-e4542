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
Imports DevExpress.LookAndFeel
Imports System.Collections
Imports DevExpress.Utils

Namespace tagsList
	Public Class TagListViewInfo
		Inherits ButtonEditViewInfo
		Public Sub New(ByVal item As RepositoryItem)
			MyBase.New(item)
		End Sub

		Protected Overrides Sub Assign(ByVal info As BaseControlViewInfo)
			If OwnerEdit IsNot Nothing Then
				MyBase.Assign(info)
			End If

		End Sub
		Public Overrides Function ButtonInfoByPoint(ByVal p As Point) As EditorButtonObjectInfoArgs
			Dim res As EditorButtonObjectInfoArgs = MyBase.ButtonInfoByPoint(p)
			If res IsNot Nothing Then
				Dim r As Rectangle = res.Bounds
				Dim t As Rectangle = GetCloseRectangle(r)
				If Not(t.Contains(p)) Then
					res = Nothing
				End If
			End If
			Return res
		End Function
		Public Shared Function GetCloseRectangle(ByVal buttonRect As Rectangle) As Rectangle
			Dim res As New Rectangle()
			res.Location = New Point(buttonRect.Right -15,buttonRect.Top)
			res.Width = 12
			res.Height = buttonRect.Height
			Return res
		End Function

		Protected Overrides ReadOnly Property IsDrawButtons() As Boolean
			Get
				If EditValue IsNot Nothing Then
					Return True
				End If
				Return MyBase.IsDrawButtons
			End Get
		End Property

		Private Function ButtonsFromEditValue() As EditorButtonCollection
			Dim Captions() As String = EditValue.ToString().Split(",".ToCharArray()(0))
			Dim res As New EditorButtonCollection()
			For Each caption As String In Captions
				Dim button As New EditorButton() With {.Caption = caption, .IsLeft = True, .Kind = ButtonPredefines.Glyph}
				Dim appearance As New AppearanceObject()
				appearance.TextOptions.HAlignment = HorzAlignment.Near
				button.Appearance.Assign(appearance)
				res.Insert(0, button)
			Next caption
			Return res
		End Function

		Protected Overrides Function CalcButtons(ByVal cache As GraphicsCache) As Rectangle
			Dim autoFit As Boolean = Item.TextEditStyle = TextEditStyles.HideTextEditor
			Dim inpButtons As New EditorButtonCollection()
			If Me.OwnerEdit IsNot Nothing Then
				inpButtons = Item.Buttons
			Else
				inpButtons = ButtonsFromEditValue()
				'LeftButtons.Clear();
			End If
			For n As Integer = 0 To inpButtons.Count - 1
				Dim btn As EditorButton = inpButtons(n)
				If (Not btn.Visible) Then
					Continue For
				End If
				Dim info As EditorButtonObjectInfoArgs = CreateButtonInfo(btn, n)
				info.FillBackground = FillBackground
				CalcButtonState(info, n)
				info.Cache = cache
				LeftButtons.Add(info)
			Next n
			Dim targs As CustomCalc = CreateCustomCalcButtonsRectsArgs(ClientRect)
			targs.UseAutoFit = False
			targs.ViewInfo = Me
			targs.Calc()
			RightButtons.SetCache(Nothing)
			LeftButtons.SetCache(Nothing)
			Return targs.ClientRect

		End Function
		Protected Overridable Function CreateCustomCalcButtonsRectsArgs(ByVal clientBounds As Rectangle) As CustomCalc
			Dim res As New CustomCalc(LeftButtons, RightButtons, clientBounds)
			Return res
		End Function

		Protected Overrides Function CreateButtonInfo(ByVal button As EditorButton, ByVal index As Integer) As EditorButtonObjectInfoArgs
			Dim res As EditorButtonObjectInfoArgs = MyBase.CreateButtonInfo(button, index)
			Return res
		End Function
		Protected Overridable Function isMaskBoxAllowed() As Boolean
			If Me.MaskBoxRect.Width > 40 Then
				Return True
			Else
				Return False
			End If
		End Function
		Public Overrides ReadOnly Property AllowMaskBox() As Boolean
			Get
				Return isMaskBoxAllowed()
			End Get
		End Property
		Public Shadows ReadOnly Property Item() As TagListReposioryItem
			Get
				Return TryCast(MyBase.Item, TagListReposioryItem)
			End Get
		End Property

		Protected Class CustomCalc
			Inherits CalcButtonsRectsArgs
			Public Sub New()
				Me.New(Nothing, Nothing, Rectangle.Empty)
			End Sub
			Public Sub New(ByVal leftButtons As EditorButtonObjectCollection, ByVal rightButtons As EditorButtonObjectCollection, ByVal clientRect As Rectangle)
				ClearAutoWidthInfo()
				Me.UseAutoFit = False
				Me.LeftButtons = leftButtons
				Me.RightButtons = rightButtons
				Me.ClientRect = clientRect
				Me.IsLeft = False
				Me.StopCalc = False
				Me.NeedRecalc = False
				Me.ViewInfo = Nothing
			End Sub
			Protected Overrides Sub CalcButtonRectsCore(ByVal collection As EditorButtonObjectCollection)
				For n As Integer = collection.Count - 1 To 0 Step -1
					Dim info As EditorButtonObjectInfoArgs = collection(n)
					Dim painter As ObjectPainter = ViewInfo.GetButtonPainter(info)
					Dim buttonRect As Rectangle = painter.CalcObjectMinBounds(info)
					buttonRect.Width += 10
					buttonRect = New Rectangle(Me.ClientRect.X, Me.ClientRect.Y, buttonRect.Width, Me.ClientRect.Height)
					Dim realButtonRect As Rectangle = buttonRect
					info.Bounds = realButtonRect
					painter.CalcObjectBounds(info)
					Me.ClientRect.Width -= buttonRect.Width
					If Me.IsLeft Then
						Me.ClientRect.X += buttonRect.Width
					End If
				Next n
			End Sub
		End Class

	End Class
End Namespace
