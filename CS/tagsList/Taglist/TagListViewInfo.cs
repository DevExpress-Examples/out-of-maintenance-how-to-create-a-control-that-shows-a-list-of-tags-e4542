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
using DevExpress.LookAndFeel;
using System.Collections;
using DevExpress.Utils;

namespace tagsList {
    public class TagListViewInfo : ButtonEditViewInfo {
        public TagListViewInfo(RepositoryItem item)
            : base(item) {
        }

        protected override void Assign(BaseControlViewInfo info) {
            if(OwnerEdit != null) {
                base.Assign(info);
            }

        }
        public override EditorButtonObjectInfoArgs ButtonInfoByPoint(Point p) {
            EditorButtonObjectInfoArgs res = base.ButtonInfoByPoint(p);
            if(res != null) {
                Rectangle r = res.Bounds;
                Rectangle t = GetCloseRectangle(r);
                if(!(t.Contains(p))) { res = null; }
            }
            return res;
        }
        public static Rectangle GetCloseRectangle(Rectangle buttonRect) {
            Rectangle res = new Rectangle();
            res.Location = new Point(buttonRect.Right -15,buttonRect.Top);
            res.Width = 12;
            res.Height = buttonRect.Height;
            return res;
        }
       
        protected override bool IsDrawButtons {
            get {
                if(EditValue != null) return true;
                return base.IsDrawButtons;
            }
        }
     
        private EditorButtonCollection ButtonsFromEditValue() {
            string[] Captions = EditValue.ToString().Split(",".ToCharArray()[0]);
            EditorButtonCollection res = new EditorButtonCollection();
            foreach(string caption in Captions) {
                EditorButton button = new EditorButton() { Caption = caption, IsLeft = true, Kind = ButtonPredefines.Glyph };
                AppearanceObject appearance = new AppearanceObject();
                appearance.TextOptions.HAlignment = HorzAlignment.Near;
                button.Appearance.Assign(appearance);
                res.Insert(0, button);
            }
            return res;
        }

        protected override Rectangle CalcButtons(GraphicsCache cache) {
            bool autoFit = Item.TextEditStyle == TextEditStyles.HideTextEditor;
            EditorButtonCollection inpButtons = new EditorButtonCollection();
            if(this.OwnerEdit != null) {
                inpButtons = Item.Buttons;
            }
            else {
                inpButtons = ButtonsFromEditValue();
                //LeftButtons.Clear();
            }
            for(int n = 0; n < inpButtons.Count; n++) {
                EditorButton btn = inpButtons[n];
                if(!btn.Visible) continue;
                EditorButtonObjectInfoArgs info = CreateButtonInfo(btn, n);
                info.FillBackground = FillBackground;
                CalcButtonState(info, n);
                info.Cache = cache;
                LeftButtons.Add(info);
            }
            CustomCalc targs = CreateCustomCalcButtonsRectsArgs(ClientRect);
            targs.UseAutoFit = false;
            targs.ViewInfo = this;
            targs.Calc();
            RightButtons.SetCache(null);
            LeftButtons.SetCache(null);
            return targs.ClientRect;

        }
        protected virtual CustomCalc CreateCustomCalcButtonsRectsArgs(Rectangle clientBounds) {
            CustomCalc res = new CustomCalc(LeftButtons, RightButtons, clientBounds);
            return res;
        }

        protected override EditorButtonObjectInfoArgs CreateButtonInfo(EditorButton button, int index) {
            EditorButtonObjectInfoArgs res = base.CreateButtonInfo(button, index);
            return res;
        }
        protected virtual bool isMaskBoxAllowed() {
            if(this.MaskBoxRect.Width > 40) return true;
            else {
                return false; }
        }
        public override bool AllowMaskBox {
            get {
                return isMaskBoxAllowed();
            }
        }
        public new TagListReposioryItem Item { get { return base.Item as TagListReposioryItem; } }

        protected class CustomCalc : CalcButtonsRectsArgs {
            public CustomCalc() : this(null, null, Rectangle.Empty) { }
            public CustomCalc(EditorButtonObjectCollection leftButtons, EditorButtonObjectCollection rightButtons, Rectangle clientRect) {
				ClearAutoWidthInfo();
				this.UseAutoFit = false;
				this.LeftButtons = leftButtons;
				this.RightButtons = rightButtons;
				this.ClientRect = clientRect;
				this.IsLeft = false;
				this.StopCalc = false;
				this.NeedRecalc = false;
				this.ViewInfo = null;
			}
            protected override void CalcButtonRectsCore(EditorButtonObjectCollection collection) {
                for(int n = collection.Count - 1; n >= 0; n--) {
                    EditorButtonObjectInfoArgs info = collection[n];
                    ObjectPainter painter = ViewInfo.GetButtonPainter(info);
                    Rectangle buttonRect = painter.CalcObjectMinBounds(info);
                    buttonRect.Width += 10;
                    buttonRect = new Rectangle(this.ClientRect.X, this.ClientRect.Y, buttonRect.Width, this.ClientRect.Height);
                    Rectangle realButtonRect = buttonRect;
                    info.Bounds = realButtonRect;
                    painter.CalcObjectBounds(info);
                    this.ClientRect.Width -= buttonRect.Width;
                    if(this.IsLeft) this.ClientRect.X += buttonRect.Width;
                }
            }
        }
      
    }
}
