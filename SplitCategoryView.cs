#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Spartacus {


public class SplitCategoryView : Label {

    

        public SplitCategoryView(int top, int left) {

            Location = new System.Drawing.Point(left, top);
            BackColor = Color.White;
            Size = new System.Drawing.Size(1, 1);
            AutoSize = true;
        }


        public void Fill(Node nd) {

            //Finds nd inside of Descendents of nd.ancestor
            int childPos=-1;

            for(int i = 0; i < nd.Ancestor.DescendentLst.Count; ++i)
                if (nd.Ancestor.DescendentLst[i] == nd) {
                    childPos = i;
                    break;
                }

            List<string> CategoriesIdx = nd.Ancestor.SplitVariable.ChildrenGroups.ValueGroupLst[childPos];
            for(int idx = 0; idx <  CategoriesIdx.Count-1; ++idx)
                Text = Text + CategoriesIdx[idx] + ", ";
            Text = Text + CategoriesIdx[CategoriesIdx.Count - 1];
        }

        public void addCategory(string cat) {

//            int col;
//
//            categoryCount++;
//            col = (categoryCount - 1) % 3;
//            if (col == 0 && categoryCount > 0)
//                Rows.Insert(Rows.Count + 1, 1);
//            this.Rows[RowCount - 1].Cells[col].Value = cat;

        }
    }

    public class SplitCategoryViewBichado : DataGridView {

    
        //private Predictor pred;
        //      private Li

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        int w, h;

        public SplitCategoryViewBichado(int top, int left) {





            Location = new System.Drawing.Point(20, 20);
            Size = new System.Drawing.Size(1, 1);
            AutoSize = true;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
//            AutoRelocate = true;
//            AutoSizeColumnHeadersEnabled = true;
//            AutoSizeRowHeadersMode = DataGridViewAutoSizeRowHeadersMode.AllRows;
//            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.ColumnsAllRows;
            ReadOnly = true;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
//            this.SplitCategoryView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.label1 = new System.Windows.Forms.Label();
//            ((System.ComponentModel.ISupportInitialize)(this.SplitCategoryView)).BeginInit();
//            this.SuspendLayout();
// 
// SplitCategoryView
// 

//            AllowUserToAddRows = false;
//            AllowUserToDeleteRows = false;
//            AutoRelocate = true;
//            AutoSize = true;
//            AutoSizeColumnHeadersEnabled = true;

            Columns.Add(this.dataGridViewTextBoxColumn1);
            Columns.Add(this.dataGridViewTextBoxColumn2);
            Columns.Add(this.dataGridViewTextBoxColumn3);
            Location = new System.Drawing.Point(left, top);
            Name = "SplitCategoryView";
            RowHeadersVisible = false;
// 
// dataGridViewTextBoxColumn1
// 
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn1.HeaderText = "";
            this.dataGridViewTextBoxColumn1.Name = "C1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
// 
// dataGridViewTextBoxColumn2
// 
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn2.HeaderText = "Categories";
            this.dataGridViewTextBoxColumn2.Name = "C2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
// 
// dataGridViewTextBoxColumn3
// 
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn3.HeaderText = "";
            this.dataGridViewTextBoxColumn3.Name = "C3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
 
        }


        public void AutoFit() {
            h = Height - 15;
            w = Width - 15;
            AutoSize = false;
            Height = h;
            Width = w;
        }


        public void Fill(Node nd) {

            //Finds nd inside of Descendents of nd.ancestor
            int childPos=-1;

            for(int i = 0; i < nd.Ancestor.DescendentLst.Count; ++i)
                if (nd.Ancestor.DescendentLst[i] == nd) {
                    childPos = i;
                    break;
                }

            int col=0, row=0;

            List<string> Categories = nd.Ancestor.SplitVariable.ChildrenGroups.ValueGroupLst[childPos];

            RowCount = (int)Math.Ceiling((double)(Categories.Count) / 3);
            foreach(string cat in  Categories){
                Rows[row].Cells[col].Value = cat;
                ++col;
                if (col == 3) {
                    ++row;
                    col = 0;
                }
            }
         }

        public void addCategory(string cat) {

//            int col;
//
//            categoryCount++;
//            col = (categoryCount - 1) % 3;
//            if (col == 0 && categoryCount > 0)
//                Rows.Insert(Rows.Count + 1, 1);
//            this.Rows[RowCount - 1].Cells[col].Value = cat;

        }
    }
}
