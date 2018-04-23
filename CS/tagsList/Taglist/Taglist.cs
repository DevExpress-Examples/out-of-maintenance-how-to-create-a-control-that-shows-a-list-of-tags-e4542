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
using System.Collections;

namespace tagsList {
    [ToolboxItem(true), Designer("DevExpress.XtraEditors.Design.ButtonEditDesigner, " + AssemblyInfo.SRAssemblyEditorsDesign)]
    public class Taglist : ButtonEdit {
        public void tagsList() {
        }
    
        protected override void OnLeave(EventArgs e) {
            base.OnLeave(e);
            ButtonsToEditValue();
        }

        protected virtual void ButtonsToEditValue() {
            string res = null;
            foreach(EditorButton t in ViewInfo.Item.Buttons) {
                if((res != null) && (t.Caption.Length > 1)) res = String.Format("{0},{1}", t.Caption, res);
                else if((res == null) && (t.Caption.Length > 1)) res = t.Caption;
            }
            this.EditValue = res;
        }
        public override bool DoValidate() {
            ButtonsToEditValue();
            return base.DoValidate();
        }
        protected override void OnCreateControl() {
            base.OnCreateControl();
            if(EditValue == null) return;
            if(EditValue is ArrayList) {
                foreach(var t in (EditValue as ArrayList)) {
                    this.Properties.Tags.Add(new EditorButton() { Caption = t.ToString() });
                }
            }
            if(EditValue.ToString().Split(",".ToCharArray()[0]).Length > 1) {
                string[] myTags = EditValue.ToString().Split(",".ToCharArray());
                foreach(string tag in myTags) {
                    this.Properties.Tags.Add(new EditorButton() { Caption = tag });
                }
            }
            else if((EditValue is string) && (EditValue.ToString().Length > 0)) {
                this.Properties.Tags.Add(new EditorButton() { Caption = EditValue.ToString() });
            }
            EditValue = null;
        }

        protected override void OnEnter(EventArgs e) {
            base.OnEnter(e);
            if(EditValue == null) { return; }
            string[] myTags = EditValue.ToString().Split(",".ToCharArray());
            Text = null;
            if((myTags.Length>1)&& (ViewInfo.Item.Tags.Count==0)) {           
                foreach(string tag in myTags) {
                    this.Properties.Tags.Add(new EditorButton() { Caption = tag });
                }
            }
            
        }

        protected internal new TagListViewInfo ViewInfo { get { return base.ViewInfo as TagListViewInfo; } }
        static Taglist() { TagListReposioryItem.Register(); }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new TagListReposioryItem Properties {
            get { return base.Properties as TagListReposioryItem; }
        }

        
        protected override void OnClickButton(EditorButtonObjectInfoArgs buttonInfo) {
            base.OnClickButton(buttonInfo);
            EditValue = null;
            ViewInfo.Item.Tags.RemoveAt(buttonInfo.Button.Index);
            ButtonsToEditValue();
            
        }
   
        protected virtual bool isKeyTagAdd(Keys d) {
            if((d == Keys.Enter) || (d == Keys.Space) || (d == Keys.Oemcomma)) {
                return true;
            }
            else return false;
        }

        protected override void OnEditorKeyDown(KeyEventArgs e) {
            if((this.EditValue != null) && (isKeyTagAdd(e.KeyCode)) && (!String.IsNullOrWhiteSpace(EditValue.ToString()))) {
                Properties.Tags.Add(new EditorButton() { Caption = EditValue.ToString().Trim() });
            }
        }

        public Taglist() {
            Properties.Tags = new TagCollection();
            Properties.Tags.Parent = this;
        }

        public override string EditorTypeName {
            get { return TagListReposioryItem.EditorName; }
        }
    }
    public class TagCollection : CollectionBase {
        private ButtonEdit _Parent;

        internal ButtonEdit Parent {
            get { return _Parent; }
            set { _Parent = value; }
        }
        public EditorButton this[int index] {
            get { return (List[index] as EditorButton); }
            set { List[index] = value; }
        }

        protected override void OnRemove(int index, object value) {
            if (Parent != null)
                Parent.Properties.Buttons.RemoveAt(index);
            base.OnRemove(index, value);
        }

        public void Add(EditorButton value) {
            value.IsLeft = true;
            value.Kind = ButtonPredefines.Glyph;
            value.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            if (Parent!=null)
                Parent.Properties.Buttons.Insert(0, value);
            List.Insert(0, value);
         }
    }
    

    
}
