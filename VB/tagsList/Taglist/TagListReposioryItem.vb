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

Namespace tagsList
	<UserRepositoryItem("Register")> _
	Public Class TagListReposioryItem
		Inherits RepositoryItemButtonEdit
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(False)> _
		Public Shadows ReadOnly Property Properties() As RepositoryItemButtonEdit
			Get
				Return Me
			End Get
		End Property
		Public Overrides Sub CreateDefaultButton()
		End Sub
		Public Overrides Sub BeginInit()
			MyBase.BeginInit()
		End Sub

		Public Property Tags() As TagCollection
			Get
				Return _Tags
			End Get
			Set(ByVal value As TagCollection)
				_Tags = value
			End Set
		End Property
		Shared Sub New()
			Register()
		End Sub
		Protected Overrides Sub OnLoaded()
			If OwnerEdit Is Nothing Then
			End If
			MyBase.OnLoaded()
		End Sub
		Public Overrides Overloads Function GetDisplayText(ByVal format As DevExpress.Utils.FormatInfo, ByVal editValue As Object) As String
			Return ""
		End Function
		Public Overrides Sub EndInit()
			MyBase.EndInit()
		End Sub

		<Browsable(False)> _
		Public Overrides ReadOnly Property Buttons() As EditorButtonCollection
			Get
				Return MyBase.Buttons
			End Get
		End Property

        Public Shared Sub Register()
            Dim myPainter As TagListButtonPainter = New TagListButtonPainter()
            Dim myEditorCI As New EditorClassInfo(EditorName, GetType(Taglist), GetType(TagListReposioryItem), GetType(TagListViewInfo), myPainter, True)
            EditorRegistrationInfo.Default.Editors.Add(myEditorCI)
        End Sub

		Private _Tags As TagCollection
		Friend Const EditorName As String = "TagList"
		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return EditorName
			End Get
		End Property
	End Class
End Namespace
