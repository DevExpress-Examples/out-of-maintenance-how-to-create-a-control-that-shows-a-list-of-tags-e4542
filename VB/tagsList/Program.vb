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
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace tagsList
	Friend NotInheritable Class Program
		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		Private Sub New()
		End Sub
		<STAThread> _
		Shared Sub Main()

			'DevExpress.UserSkins.BonusSkins.Register();
			'DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.BonusSkins).Assembly);


			DevExpress.UserSkins.BonusSkins.Register()
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)

			Application.Run(New XtraForm1())
		End Sub
	End Class
End Namespace
