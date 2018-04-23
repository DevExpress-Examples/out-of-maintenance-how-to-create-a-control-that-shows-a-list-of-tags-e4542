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
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.Utils.Drawing
Imports DevExpress.Skins
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.LookAndFeel
Imports DevExpress.Utils
Imports DevExpress.XtraEditors.ViewInfo
Imports System.Collections


Namespace tagsList

	Public Class TagListButtonPainter
		Inherits ButtonEditPainter
		Protected Overrides Sub DrawContent(ByVal info As ControlGraphicsInfoArgs)
			If (TryCast(info.ViewInfo, TagListViewInfo)).OwnerEdit Is Nothing Then
				MyBase.DrawButtons(info)
			Else
				TryCast((TryCast(info.ViewInfo, TagListViewInfo)).OwnerEdit, Taglist).Text = ""
				MyBase.DrawContent(info)
			End If
		End Sub
		Protected Overrides Overloads Sub DrawButtons(ByVal info As ControlGraphicsInfoArgs)
			MyBase.DrawButtons(info)
		End Sub

		Protected Overrides Sub DrawButton(ByVal viewInfo As ButtonEditViewInfo, ByVal info As EditorButtonObjectInfoArgs)
			MyBase.DrawButton(viewInfo, info)
			Dim vi As TagListViewInfo = TryCast(viewInfo, TagListViewInfo)
			Dim closeButtonRect As Rectangle = TagListViewInfo.GetCloseRectangle(info.Bounds)
			Dim currentButtonPainter As SkinEditorButtonPainter = CType(viewInfo.GetButtonPainter(info), SkinEditorButtonPainter)
			Dim currentSkin As Skin = EditorsSkins.GetSkin(currentButtonPainter.Provider)
			Dim closeButton As SkinElement = currentSkin(EditorsSkins.SkinCloseButton)
			Dim customSkinElementPainter As New CustomSkinElementPainter()
			If closeButton IsNot Nothing Then
				Dim skinInfo As New SkinElementInfo(EditorsSkins.GetSkin(currentButtonPainter.Provider)(EditorsSkins.SkinCloseButton), closeButtonRect)
				skinInfo.Cache = info.Cache
				skinInfo.State = info.State
				customSkinElementPainter.DrawObject(skinInfo)
			End If
		End Sub
	End Class
	Public Class CustomSkinElementPainter
		Inherits SkinElementPainter
		Public Overrides Overloads Sub DrawObject(ByVal e As ObjectInfoArgs)
			Dim ee As SkinElementInfo = TryCast(e, SkinElementInfo)
			If ee.Element Is Nothing Then
				Return
			End If
			DrawSkinForeground(ee)
		End Sub

		Protected Overrides Sub DrawSkinImage(ByVal elementInfo As SkinElementInfo, ByVal skinImage As SkinImage)
			MyBase.DrawSkinImage(elementInfo, skinImage)
		End Sub
		Public Sub PublicForeground(ByVal ee As SkinElementInfo)
			MyBase.DrawSkinForeground(ee)
		End Sub
	End Class
End Namespace
