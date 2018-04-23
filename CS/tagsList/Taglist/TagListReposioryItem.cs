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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.Utils.Drawing;

namespace tagsList {
    [UserRepositoryItem("Register")]
    public class TagListReposioryItem : RepositoryItemButtonEdit 
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public new RepositoryItemButtonEdit Properties { get { return this; } }    
        public override void CreateDefaultButton() {
        }
        public override void BeginInit() {
            base.BeginInit();
        }
 
        public TagCollection Tags {
            get { return _Tags; }
            set { _Tags = value; }
        }
        static TagListReposioryItem() {
            Register();
        }
        protected override void OnLoaded() {
            if(OwnerEdit == null) { 
            }
            base.OnLoaded();
        }
        public override string GetDisplayText(DevExpress.Utils.FormatInfo format, object editValue) {
            return "";
        }
        public override void EndInit() {
            base.EndInit();
        }

        [Browsable(false)]
        public override EditorButtonCollection Buttons {
            get {
                return base.Buttons;
            }
        }

        public static void Register() {
            EditorClassInfo myEditorCI = new EditorClassInfo(EditorName, typeof(Taglist), typeof(TagListReposioryItem), typeof(TagListViewInfo), new TagListButtonPainter(), true, null);
            EditorRegistrationInfo.Default.Editors.Add(myEditorCI);
        }

        private TagCollection _Tags;
        internal const string EditorName = "TagList";
        public override string EditorTypeName {
            get { return EditorName; }
        }
    }
}
