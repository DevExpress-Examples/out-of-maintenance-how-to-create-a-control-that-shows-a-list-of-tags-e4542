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
using DevExpress.XtraEditors.Drawing;
using DevExpress.Utils.Drawing;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraEditors.ViewInfo;
using System.Collections;


namespace tagsList {

    public class TagListButtonPainter : ButtonEditPainter {
        protected override void DrawContent(ControlGraphicsInfoArgs info) {
            if((info.ViewInfo as TagListViewInfo).OwnerEdit == null) {
                base.DrawButtons(info);
            }
            else {
                ((info.ViewInfo as TagListViewInfo).OwnerEdit as Taglist).Text = "";
                base.DrawContent(info);
            }
        }
        protected override void DrawButtons(ControlGraphicsInfoArgs info) {
            base.DrawButtons(info);
        }
        
        protected override void DrawButton(ButtonEditViewInfo viewInfo, EditorButtonObjectInfoArgs info) {
            base.DrawButton(viewInfo, info);
            TagListViewInfo vi = viewInfo as TagListViewInfo;
            Rectangle closeButtonRect = TagListViewInfo.GetCloseRectangle(info.Bounds);
            SkinEditorButtonPainter currentButtonPainter = (SkinEditorButtonPainter)viewInfo.GetButtonPainter(info);
            Skin currentSkin = EditorsSkins.GetSkin(currentButtonPainter.Provider);
            SkinElement closeButton = currentSkin[EditorsSkins.SkinCloseButton];
            CustomSkinElementPainter customSkinElementPainter = new CustomSkinElementPainter();
            if(closeButton != null) {
                SkinElementInfo skinInfo = new SkinElementInfo(EditorsSkins.GetSkin(currentButtonPainter.Provider)[EditorsSkins.SkinCloseButton], closeButtonRect);
                skinInfo.Cache = info.Cache;
                skinInfo.State = info.State;
                customSkinElementPainter.DrawObject(skinInfo);
            }
        }
    }
    public class CustomSkinElementPainter : SkinElementPainter {
        public override void DrawObject(ObjectInfoArgs e) {
            SkinElementInfo ee = e as SkinElementInfo;
            if(ee.Element == null) return;
            DrawSkinForeground(ee);
        }

        protected override void DrawSkinImage(SkinElementInfo elementInfo, SkinImage skinImage) {
            base.DrawSkinImage(elementInfo, skinImage);
        }
        public void PublicForeground(SkinElementInfo ee) {
            base.DrawSkinForeground(ee);
        }
    }
}
