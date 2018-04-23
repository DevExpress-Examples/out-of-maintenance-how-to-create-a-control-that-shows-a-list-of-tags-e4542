// Developer Express Code Central Example:
// How to create a control that shows a list of tags
// 
// This example demonstrates how to create a TagList control editor that supports
// both standalone and in-place modes.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E4542

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tagsList {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
        
            //DevExpress.UserSkins.BonusSkins.Register();
            //DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.BonusSkins).Assembly);


            DevExpress.UserSkins.BonusSkins.Register();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new XtraForm1());
        }
    }
}
