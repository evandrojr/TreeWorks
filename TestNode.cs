#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#endregion

namespace Spartacus {
    public class TestNode : DataGridView{

        public NodeTargetContinuous Node;
        public Label LabelTop, LabelBottom;
        public DataGridViewTextBoxColumn C1;
        public DataGridViewTextBoxColumn C2;

        public TestNode() {

            Size = new System.Drawing.Size(1, 1);
            AutoSize = true;
            LabelTop = new Label();
            LabelBottom = new Label();

///////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            DataGridViewCellStyle Style1 = new DataGridViewCellStyle();
            DataGridViewCellStyle Style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle Style3 = new DataGridViewCellStyle();
            C1 = new DataGridViewTextBoxColumn();
            C2 = new DataGridViewTextBoxColumn();
            //           C3 = new DataGridViewTextBoxColumn();

            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            Style1.Alignment = DataGridViewContentAlignment.TopLeft;
            Style1.WrapMode = DataGridViewTriState.False;
            AlternatingRowsDefaultCellStyle = Style1;
//            AutoRelocate = true;
//            AutoSizeColumnHeadersEnabled = true;
//            AutoSizeRowHeadersMode = DataGridViewAutoSizeRowHeadersMode.AllRows;
//            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.ColumnsAllRows;
            CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            Style2.Alignment = DataGridViewContentAlignment.TopLeft;
            Style2.BackColor = System.Drawing.SystemColors.Control;
            Style2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Style2.ForeColor = System.Drawing.SystemColors.WindowText;
            Style2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            Style2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            Style2.WrapMode = DataGridViewTriState.True;
            ColumnHeadersDefaultCellStyle = Style2;
            ColumnHeadersVisible = false;
            Columns.Add(this.C1);
            Columns.Add(this.C2);
//            Columns.Add(this.C3);



//            Columns.Add(this.dataGridViewTextBoxColumn12);
//            ColumnsResizable = false;
            Cursor = Cursors.Hand;

/*
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            DefaultCellStyle = dataGridViewCellStyle4;
 */
            EditMode = DataGridViewEditMode.EditProgrammatically;
            Location = new System.Drawing.Point(162, 150);
            Margin = new Padding(0);
            MultiSelect = false;
            Name = "nodeCategorical";
            ReadOnly = true;
            RowHeadersVisible = false;
            RowTemplate.Height = 10;
            ScrollBars = ScrollBars.None;
            SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;

            TabIndex = 2;
// 
// dataGridViewTextBoxColumn6
// 
            this.C1.DefaultCellStyle = Style1;
            this.C1.Frozen = true;
            this.C1.HeaderText = "Column1";
            this.C1.Name = "c1";
            this.C1.ReadOnly = true;
            this.C1.Width = 10;
// 
// dataGridViewTextBoxColumn7
// 
            this.C2.DefaultCellStyle = Style2;
            this.C2.Frozen = true;
            this.C2.HeaderText = "Column2";
            this.C2.Name = "c2";
            this.C2.ReadOnly = true;
            this.C2.Width = 10;
// 
// dataGridViewTextBoxColumn8
// 

/*
            this.C3.DefaultCellStyle = Style3;
            this.C3.Frozen = true;
            this.C3.HeaderText = "Column3";
            this.C3.Name = "c3";
            this.C3.ReadOnly = true;
            this.C3.Width = 10;

*/
            int x, y, w, h;

            x = y = 0;

            Rows.Add(5);
            Rows[y].Cells[x].Value = "Node 0"; ++y;
            Rows[y].Cells[x].Value = "Mean"; ++y;
            Rows[y].Cells[x].Value = "Std Dev"; ++y;
            Rows[y].Cells[x].Value = "n"; ++y;
            Rows[y].Cells[x].Value = "%"; ++y;
            ++x; y = 1;


//            Rows[y].Cells[x].Value = Node.Mean; ++y;
//            Rows[y].Cells[x].Value = Node.StdDev; ++y;
//            Rows[y].Cells[x].Value = Node.Table.RowCount; ++y;
 //           Rows[y].Cells[x].Value = Node.Table.RowCount / Node.Tree.Schema.RowCount; ++y;
            ++x; y = 1;

//            AutoSizeColumn(DataGridViewAutoSizeColumnCriteria.HeaderAndRows, 0);
//            AutoSizeColumn(DataGridViewAutoSizeColumnCriteria.HeaderAndRows, 1);
//            AutoSizeColumn(DataGridViewAutoSizeColumnCriteria.HeaderAndRows, 2);

  
// 
// dataGridViewTextBoxColumn12
// 
/*
            this.dataGridViewTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn12.Frozen = true;
            this.dataGridViewTextBoxColumn12.HeaderText = "Column4";
            this.dataGridViewTextBoxColumn12.Name = "c4";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 10;
*/



/*            
            this.Controls.Add(this.nodeCategorical);
            this.Controls.Add(this.panel1);
*/
            Height = Width = 1;

            Def.PbBase.Controls.Add(this);
            Def.PbBase.Controls.Add(this.LabelTop);
            Def.PbBase.Controls.Add(this.LabelBottom);
            LabelTop.Text = "";
            LabelBottom.Text = "";


            h = Height - 15;
            w = Width - 15;
            AutoSize = false;


            Height = h ;
            Width = w;






        }
    }
}



