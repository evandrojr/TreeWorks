#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#endregion

namespace Spartacus
{
    public class NodeTargetContinuousUI : DataGridView {

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {

            LabelTop.Dispose();
            LabelBottom.Dispose();
            if (disposing && (components != null)) {
                components.Dispose();
            }
            //TEMPORARY BUG WALK AROUND
            //base.Dispose(disposing);
            //base.Dispose(disposing);
            Visible = false;

        }

        public NodeTargetContinuous Node;
        public Label LabelTop, LabelBottom;
        public DataGridViewTextBoxColumn C1;
        public DataGridViewTextBoxColumn C2;
        public System.Windows.Forms.ContextMenuStrip Selector;
        public System.Windows.Forms.ToolStripMenuItem viewdataToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem manuallysplitToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem autosplitToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem fullautogrowToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem removeSubNodesToolStripMenuItem;


        public NodeTargetContinuousUI(NodeTargetContinuous node) {

            int x, y;
            x = y = 0;

            Def.PbBase.Controls.Add(this);
            this.components = new System.ComponentModel.Container();
            this.Selector = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Selector.GripMargin = new System.Windows.Forms.Padding(2);
            this.Selector.SuspendLayout();
            this.SuspendLayout();
//            this.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
//            this.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;


            this.Node = node;
            LabelTop = new Label();
            LabelBottom = new Label();
            LabelTop.SuspendLayout();
            LabelBottom.SuspendLayout();
            Def.PbBase.Controls.Add(this.LabelTop);
            Def.PbBase.Controls.Add(this.LabelBottom);
            LabelTop.AutoSize = true;
            LabelBottom.AutoSize = true;
            this.viewdataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manuallysplitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autosplitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullautogrowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSubNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MouseHover += new System.EventHandler(grid_MouseHover);
            this.MouseLeave += new System.EventHandler(grid_MouseLeave);
            this.ContextMenuStrip = this.Selector;
// 
// Selector
// 
            this.Selector.AllowDrop = true;
            this.Selector.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewdataToolStripMenuItem,
            this.manuallysplitToolStripMenuItem,
            this.autosplitToolStripMenuItem,
            this.fullautogrowToolStripMenuItem,
            this.removeSubNodesToolStripMenuItem});
            this.Selector.Location = new System.Drawing.Point(23, 61);
            this.Selector.Name = "Menu";
            this.Selector.Size = new System.Drawing.Size(133, 148);
            this.Selector.Visible = true;
// 
// viewdataToolStripMenuItem
// 
            this.viewdataToolStripMenuItem.Name = "viewdataToolStripMenuItem";
            this.viewdataToolStripMenuItem.Text = "View data";
            this.viewdataToolStripMenuItem.Click += new System.EventHandler(this.viewdataToolStripMenuItem_Click);
// 
// manuallysplitToolStripMenuItem
// 
            this.manuallysplitToolStripMenuItem.Name = "manuallysplitToolStripMenuItem";
            this.manuallysplitToolStripMenuItem.Text = "Manually split";
            this.manuallysplitToolStripMenuItem.Click += new System.EventHandler(this.manuallysplitToolStripMenuItem_Click);
// 
// autosplitToolStripMenuItem
// 
            this.autosplitToolStripMenuItem.Name = "autosplitToolStripMenuItem";
            this.autosplitToolStripMenuItem.Text = "Auto split";
            this.autosplitToolStripMenuItem.Click += new System.EventHandler(this.autosplitToolStripMenuItem_Click);
// 
// fullautogrowToolStripMenuItem
// 
            this.fullautogrowToolStripMenuItem.Name = "fullautogrowToolStripMenuItem";
            this.fullautogrowToolStripMenuItem.Text = "Full auto grow";
            this.fullautogrowToolStripMenuItem.Click += new System.EventHandler(this.fullautogrowToolStripMenuItem_Click);
// 
// removeSubNodesToolStripMenuItem
// 
            this.removeSubNodesToolStripMenuItem.Name = "removeSubNodesToolStripMenuItem";
            this.removeSubNodesToolStripMenuItem.Text = "Remove subnodes";
            this.removeSubNodesToolStripMenuItem.Click += new System.EventHandler(this.removeSubNodesToolStripMenuItem_Click);

            DataGridViewCellStyle Style1 = new DataGridViewCellStyle();
            DataGridViewCellStyle Style2 = new DataGridViewCellStyle();
            C1 = new DataGridViewTextBoxColumn();
            C2 = new DataGridViewTextBoxColumn();
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToOrderColumns = false;
            AllowUserToResizeColumns = false;
            AllowUserToResizeRows = false;
            Style1.Alignment = DataGridViewContentAlignment.TopLeft;
            Style1.WrapMode = DataGridViewTriState.False;
            AlternatingRowsDefaultCellStyle = Style1;
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
            Cursor = Cursors.Hand;
            EditMode = DataGridViewEditMode.EditProgrammatically;
            Margin = new Padding(0);
            MultiSelect = false;
            Name = "nodeContinuous";
            ReadOnly = true;
            RowHeadersVisible = false;
            RowTemplate.Height = 18;
            ScrollBars = ScrollBars.None;
//            SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
            this.C1.DefaultCellStyle = Style1;
            this.C1.Frozen = true;
            this.C1.HeaderText = "Column1";
            this.C1.Name = "c1";
            this.C1.ReadOnly = true;
            this.C1.Width = 50;
            this.C2.DefaultCellStyle = Style2;
            this.C2.Frozen = true;
            this.C2.HeaderText = "Column2";
            this.C2.Name = "c2";
            this.C2.ReadOnly = true;
            this.C2.Width = 60;

            Rows.Add(6);
            Rows[y].Cells[x].Value = "Node " + Node.Id; ++y;
            Rows[y].Cells[x].Value = "Mean"; ++y;
            Rows[y].Cells[x].Value = "Std Dev"; ++y;
            Rows[y].Cells[x].Value = "Cf Var"; ++y;
            Rows[y].Cells[x].Value = "n"; ++y;
            Rows[y].Cells[x].Value = "%"; ++y;
            ++x; y = 1;
            
            double mean = Node.Mean;
            double stdDev = Node.StdDev;

            Rows[y].Cells[x].Value = Math.Round(mean, 2); ++y;
            Rows[y].Cells[x].Value = Math.Round(stdDev, 2); ++y;
            Rows[y].Cells[x].Value = Math.Round(stdDev / mean, 2); ++y;
            Rows[y].Cells[x].Value = Node.Table.RowCount; ++y;
            Rows[y].Cells[x].Value = Math.Round((double)Node.Table.RowCount / (double)Def.TrainingSetRowCount * (double)100, 2); ++y;
            ++x; y = 1;

//            AutoSizeColumn(DataGridViewAutoSizeColumnCriteria.HeaderAndRows, 0);
//            AutoSizeColumn(DataGridViewAutoSizeColumnCriteria.HeaderAndRows, 1);

            LabelTop.Text = "";            
            LabelBottom.Text = "";

            Height = (RowTemplate.Height * 6) + 4;
            Width = 110;
            removeSubNodesToolStripMenuItem.Enabled = false;
            this.Selector.ResumeLayout(false);
            this.Selector.Hide();
            this.ResumeLayout(false);
        }

        public void DisplayUpdate() {
            int x=1, y=1;
            double mean = Node.Mean;
            double stdDev = Node.StdDev;

            Rows[y].Cells[x].Value = Math.Round(mean, 2); ++y;
            Rows[y].Cells[x].Value = Math.Round(stdDev, 2); ++y;
            Rows[y].Cells[x].Value = Math.Round(stdDev / mean, 2); ++y;
            Rows[y].Cells[x].Value = Node.Table.RowCount; ++y;
            Rows[y].Cells[x].Value = Math.Round((double)Node.Table.RowCount / (double)Def.TrainingSetRowCount * (double)100, 2); ++y;
            Def.Tree.ReductionInImpCalc();
            Def.PbBase.Invalidate();
        }

        private void viewdataToolStripMenuItem_Click(object sender, EventArgs e) {
            DataShow();
        }

        private void manuallysplitToolStripMenuItem_Click(object sender, EventArgs e) {
            ManualSplit();
        }

        private void autosplitToolStripMenuItem_Click(object sender, EventArgs e) {
            AutoSplit();
        }

        private void grid_MouseHover(object sender, EventArgs e) {
            if (this.Node.Ancestor == null)
                return;
            if (this.Node.Ancestor.SplitVariable.Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical) {
                SplitCategoryView scv = new SplitCategoryView(Top - 20, Left);
                scv.Fill(this.Node);
                Def.PbBase.Controls.Add(scv);
                scv.BringToFront();
                FrmMain.SplitCategoryViewActive = scv;
            }
        }

        private void grid_MouseLeave(object sender, EventArgs e) {

            if (FrmMain.SplitCategoryViewActive == null)
                return;
            Def.PbBase.Controls.Remove(FrmMain.SplitCategoryViewActive);
            FrmMain.SplitCategoryViewActive.Dispose();
            FrmMain.SplitCategoryViewActive = null;
        }

        private void fullautogrowToolStripMenuItem_Click(object sender, EventArgs e) {
            Def.Tree.FullAutogrow(Node);
            Def.Tree.ReductionInImpCalc();
            Def.PbBase.Invalidate();
        }

        private void removeSubNodesToolStripMenuItem_Click(object sender, EventArgs e) {
            Def.Tree.RemoveDescendents(this.Node);
            Def.Tree.ReductionInImpCalc();
            Def.PbBase.Invalidate();
        }

        public void DataShow() {
            FrmNodeData fnd = new FrmNodeData(Node);
            fnd.Show();
        }

        public void ManualSplit() {
            Node.AllGainsCalc();
            Node.SearchBestSplit();
            FrmPredictor fcp = new FrmPredictor(Node);
            fcp.ShowDialog();
            Def.Tree.ReductionInImpCalc();
            Def.PbBase.Invalidate();
        }

        private void AutoSplit(){
            Node.AllGainsCalc();
            Node.SearchBestSplit();
            Node.SplitVariableIdx = Node.BestSplitIdx;
            if (Node.Tree.AutoSplit(Node) == false) {
                MessageBox.Show("Unable to split", "Warning");
                return;
            }
            Def.Tree.ReductionInImpCalc();
            Def.PbBase.Invalidate();
        }
    }
}
