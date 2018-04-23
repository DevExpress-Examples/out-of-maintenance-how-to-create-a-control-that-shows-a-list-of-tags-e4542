// Developer Express Code Central Example:
// How to create a control that shows a list of tags
// 
// This example demonstrates how to create a TagList control editor that supports
// both standalone and in-place modes.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E4542

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace tagsList {
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm {
        public XtraForm1() {
            InitializeComponent();
            string t = "Haskell,Ocaml,Scala,Ruby";
            taglist1.EditValue = t;
            BindingList<myDataSource> ds = new BindingList<myDataSource>();
            for(int i = 0; i < 10; i++) {
                if(i < 5) ds.Add(new myDataSource() { Langs = "Perl,Python" });
                else ds.Add(new myDataSource() { Langs = "Perl,Python,Java" });
            }
            gridControl1.DataSource = ds;
            (gridControl1.MainView as GridView).Columns["Langs"].ColumnEdit = new TagListReposioryItem();
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
        }
    }
    public class myDataSource {
        private string _Langs;

        public string Langs {
            get { return _Langs; }
            set { _Langs = value; }
        }    
    }
}