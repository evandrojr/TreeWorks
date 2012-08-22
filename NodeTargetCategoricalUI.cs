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
    public class NodeTargetCategoricalUI : Label {

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
           
            Grid.Dispose();
            base.Dispose(disposing);
            
            LabelTop.Dispose();
            LabelBottom.Dispose();
        }

        public NodeTargetCategorical Node;
        public Label LabelTop, LabelBottom;
        public DataGridView Grid;
        public DataGridViewTextBoxColumn C1;
        public DataGridViewTextBoxColumn C2;
        public DataGridViewTextBoxColumn C3;
        public DataGridViewTextBoxColumn C4;
        public System.Windows.Forms.ContextMenuStrip Selector;
        public System.Windows.Forms.ToolStripMenuItem viewdataToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem manuallysplitToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem autosplitToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem fullautogrowToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem removeSubNodesToolStripMenuItem;
        
        public NodeTargetCategoricalUI(NodeTargetCategorical node) {

            if (Def.ExperimentRunning)
                return;

            Def.PbBase.Controls.Add(this);
            this.components = new System.ComponentModel.Container();
            int x, y;
            x = y = 0;
            int caseCount, freq;

            this.Node = node;

            this.ForeColor = Color.Black;
            Text = Node.Id.ToString();
            BackColor = System.Drawing.Color.White;
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            Cursor = System.Windows.Forms.Cursors.Hand;
//            Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Size = new System.Drawing.Size(60, 60);
            Location = new Point(100, 100);
            Visible = true;

            Grid = new DataGridView();
            Grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            Grid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.None;
            this.Grid.Visible = false;
            LabelTop = new Label();
            LabelBottom = new Label();
            LabelBottom.ForeColor = System.Drawing.Color.Blue;
            LabelTop.ForeColor = System.Drawing.Color.Red;
            LabelTop.AutoSize = true;
            LabelBottom.AutoSize = true;
            this.MouseHover += new System.EventHandler(MainLabel_MouseHover);
            this.MouseLeave += new System.EventHandler(MainLabel_MouseLeave);


            //this.Selector = new ContextMenuStrip();
            this.Selector = new ContextMenuStrip(this.components);
            this.viewdataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manuallysplitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autosplitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullautogrowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullautogrowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSubNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            //((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
//            this.Selector.SuspendLayout();

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

//            Grid.Location = new System.Drawing.Point(20, 20);
//           Grid.Size = new System.Drawing.Size(1, 1);
            //Grid.AutoSize = true;
            DataGridViewCellStyle Style1 = new DataGridViewCellStyle();
            DataGridViewCellStyle Style2 = new DataGridViewCellStyle();
            C1 = new DataGridViewTextBoxColumn();
            C2 = new DataGridViewTextBoxColumn();
            C3 = new DataGridViewTextBoxColumn();
            C4 = new DataGridViewTextBoxColumn();
            Grid.AllowUserToAddRows = false;
            Grid.AllowUserToDeleteRows = false;
            Style1.Alignment = DataGridViewContentAlignment.TopLeft;
            Style1.WrapMode = DataGridViewTriState.False;
            Grid.AlternatingRowsDefaultCellStyle = Style1;
//            Grid.AutoRelocate = true;
//            Grid.AutoSizeColumnHeadersEnabled = true;
//            Grid.AutoSizeRowHeadersMode = DataGridViewAutoSizeRowHeadersMode.AllRows;
//            Grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.ColumnsAllRows;
            Grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            Grid.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            Style2.Alignment = DataGridViewContentAlignment.TopLeft;
            Style2.BackColor = System.Drawing.SystemColors.Control;
            Style2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Style2.ForeColor = System.Drawing.SystemColors.WindowText;
            Style2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            Style2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            Style2.WrapMode = DataGridViewTriState.True;
            Grid.ColumnHeadersDefaultCellStyle = Style2;
            Grid.ColumnHeadersVisible = false;
            Grid.Columns.Add(this.C1);
            Grid.Columns.Add(this.C2);
            Grid.Columns.Add(this.C3);
            Grid.Columns.Add(this.C4);
//            Grid.ColumnsResizable = false;
            Grid.Cursor = Cursors.Hand;
            Grid.EditMode = DataGridViewEditMode.EditProgrammatically;
            Grid.Margin = new Padding(0);
            Grid.MultiSelect = false;
            Grid.Name = "nodeCategorical";
            Grid.ReadOnly = true;
            Grid.RowHeadersVisible = false;
            Grid.RowTemplate.Height = 18;
            Grid.ScrollBars = ScrollBars.None;
//            Grid.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
            this.C1.DefaultCellStyle = Style1;
            this.C1.Frozen = true;
            this.C1.HeaderText = "Column1";
            this.C1.Name = "c1";
            this.C1.ReadOnly = true;
            this.C1.Width = 80;

            this.C2.DefaultCellStyle = Style2;
            this.C2.Frozen = true;
            this.C2.HeaderText = "Column2";
            this.C2.Name = "c2";
            this.C2.ReadOnly = true;
            this.C2.Width = 40;

            this.C3.DefaultCellStyle = Style2;
            this.C3.Frozen = true;
            this.C3.HeaderText = "Column3";
            this.C3.Name = "c2";
            this.C3.ReadOnly = true;
            this.C3.Width = 40;

            this.C4.DefaultCellStyle = Style2;
            this.C4.Frozen = true;
            this.C4.HeaderText = "Column4";
            this.C4.Name = "c2";
            this.C4.ReadOnly = true;
            this.C4.Width = 40;

            Grid.Rows.Add(3); // 2 for header + 1 for total

            //Header row
            Grid.Rows[y].Cells[x].Value = "Node " + Node.Id;
            Grid.Rows[y].Cells[x + 1].Value = Math.Round(Node.Imp, 3) + "i";
            ++y;
            Grid.Rows[y].Cells[x].Value = "Class"; ++x;
            Grid.Rows[y].Cells[x].Value = "%r"; ++x;
            Grid.Rows[y].Cells[x].Value = "%a"; ++x;
            Grid.Rows[y].Cells[x].Value = "n";

            caseCount = Node.TargetClasses.ClassSd.Count;
            if(caseCount > 0)
                Grid.Rows.Add(caseCount);
            for (int i = 0; i < caseCount; ++i) {
                Grid.Rows[i + 2].Cells[0].Value = Node.TargetClasses.ClassSd.Keys[i];
                freq = Node.TargetClasses.ClassSd.Values[i];
                Grid.Rows[i + 2].Cells[1].Value = Math.Round(((double)freq / Node.Table.RowCount) * 100);
                Grid.Rows[i + 2].Cells[2].Value = Math.Round(((double)freq / Def.TrainingSetRowCount) * 100);
                Grid.Rows[i + 2].Cells[3].Value = freq;
            }

            //Total row
            Grid.Rows[caseCount + 2].Cells[0].Value = "Total";
            if(caseCount > 0)
                Grid.Rows[caseCount + 2].Cells[1].Value = "100";
            else
                Grid.Rows[caseCount + 2].Cells[1].Value = "0";
            Grid.Rows[caseCount + 2].Cells[2].Value = Math.Round(((double)Node.Table.RowCount / Def.TrainingSetRowCount) * 100);
            Grid.Rows[caseCount + 2].Cells[3].Value = Node.Table.RowCount;

//            Grid.AutoSizeColumn(DataGridViewAutoSizeColumnCriteria.HeaderAndRows, 0);
//            Grid.AutoSizeColumn(DataGridViewAutoSizeColumnCriteria.HeaderAndRows, 1);
//            Grid.AutoSizeColumn(DataGridViewAutoSizeColumnCriteria.HeaderAndRows, 2);
//            Grid.AutoSizeColumn(DataGridViewAutoSizeColumnCriteria.HeaderAndRows, 3);

            Def.PbBase.Controls.Add(this);
            Def.PbBase.Controls.Add(this.LabelTop);
            Def.PbBase.Controls.Add(this.LabelBottom);
            Def.PbBase.Controls.Add(this.Grid);

            LabelTop.Text = "";
            LabelBottom.Text = "";

            //h = Grid.Height - 15;
            //w = Grid.Width - 15;
            //Grid.AutoSize = false;
            //Grid.Height = h;
            //Grid.Width = w;
            LabelTop.Show();
            LabelBottom.Show();
            Grid.Width = 200;
            Grid.Height = (Node.TargetClasses.ClassSd.Count+3) * Grid.RowTemplate.Height;
            removeSubNodesToolStripMenuItem.Enabled = false;
            this.Selector.Hide();


            /*

            Grid.AutoSize = true;
            Grid.Height = 500;
            Grid.Width = 500;
            Grid.Top = 10;
            Grid.Left = 10;
            Grid.Visible = true;
            Grid.Enabled = true;
   */
        }

        public void DisplayUpdate() {

//            int x, y;
//            x = y = 0;
            int caseCount, freq;

            caseCount = Node.TargetClasses.ClassSd.Count;
            for (int i = 0; i < caseCount; ++i) {
                Grid.Rows[i + 2].Cells[0].Value = Node.TargetClasses.ClassSd.Keys[i];
                freq = Node.TargetClasses.ClassSd.Values[i];
                Grid.Rows[i + 2].Cells[1].Value = Math.Round(((double)freq / Node.Table.RowCount) * 100);
                Grid.Rows[i + 2].Cells[2].Value = Math.Round(((double)freq / Def.TrainingSetRowCount) * 100);
                Grid.Rows[i + 2].Cells[3].Value = freq;
            }

            //Total row
            Grid.Rows[caseCount + 2].Cells[0].Value = "Total";
            Grid.Rows[caseCount + 2].Cells[1].Value = "100";
            Grid.Rows[caseCount + 2].Cells[2].Value = Math.Round(((double)Node.Table.RowCount / Def.TrainingSetRowCount) * 100);
            Grid.Rows[caseCount + 2].Cells[3].Value = Node.Table.RowCount;
        }

        private void MainLabel_MouseHover(object sender, EventArgs e) {
            this.Grid.Visible = true;
            this.Grid.BringToFront();

            if (this.Node.Ancestor == null)
                return;
            if (Def.Multivariate == false) {
                if (this.Node.Ancestor.SplitVariable.Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical) {
                    SplitCategoryView scv = new SplitCategoryView(Top - 20, Left);
                    scv.Fill(this.Node);
                    Def.PbBase.Controls.Add(scv);
                    scv.BringToFront();
                    FrmMain.SplitCategoryViewActive = scv;
                }
            } else {

            }


        }

        private void MainLabel_MouseLeave(object sender, EventArgs e) {
            this.Grid.Visible = false;
            if (FrmMain.SplitCategoryViewActive == null)
                return;
            Def.PbBase.Controls.Remove(FrmMain.SplitCategoryViewActive);
            FrmMain.SplitCategoryViewActive.Dispose();
            FrmMain.SplitCategoryViewActive = null;
        }

        private void viewdataToolStripMenuItem_Click(object sender, EventArgs e) {
            DataShow();
        }

        private void manuallysplitToolStripMenuItem_Click(object sender, EventArgs e) {
                ManualSplit();
        }

        private void autosplitToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!Def.Multivariate)
                AutoSplit();
            else
                AutoSplitMv();
        }

        private void removeSubNodesToolStripMenuItem_Click(object sender, EventArgs e) {
            Def.Tree.RemoveDescendents(this.Node);
            Def.Tree.ReductionInImpCalc();
            Def.PbBase.Invalidate();
        }

        private void fullautogrowToolStripMenuItem_Click(object sender, EventArgs e) {
            Def.Tree.FullAutogrow(Node);
            Def.Tree.ReductionInImpCalc();
            Def.PbBase.Invalidate();
            //
//            
//            Def.FrmMain.WindowState = FormWindowState.Minimized;
//            FrmMessage fmsg = new FrmMessage("Please, wait", "Calculating...");
//            fmsg.Show(this);
//            Application.DoEvents();
//            try {
//                FullAutoGrow(Node);
//            } catch {
//                MessageBox.Show("Problem with autogrow");
//            } finally {
//                Def.FrmMain.WindowState = FormWindowState.Maximized;
//                fmsg.Close();
//                Def.FrmMain.TopMost = true;
//                Application.DoEvents();
//                Def.FrmMain.TopMost = false;
//                Def.FrmMain.Focus();
//                Def.Tree.GrowthState = Tree.GrowthStateEnum.FullGrow;
//            }
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

        public void AutoSplitMv() {
            Node.AllGainsCalc();

            if (Node.SearchBestSplit()) {
                Node.MvTb.DataFill();
                Node.LocalMinimumSearchMv();
                if (Node.Tree.AutoSplitMv(Node) == false) {
                    MessageBox.Show("Unable to split", "Warning");
                    return;
                }
                Def.Tree.ReductionInImpCalc();
                Node.MvTb.DataEmpty();
            } else {
                MessageBox.Show("Unable to split", "Warning");
            }
            Def.PbBase.Invalidate();             
        }

        private void AutoSplit() {
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

//        private void FullAutoGrow(Node nd) {
//            
//            bool couldSplit = false;
//            nd.AllGainsCalc();
//            nd.SearchBestSplit();
//            nd.SplitVariableIdx = nd.BestSplitIdx;
//            couldSplit = nd.Tree.AutoSplit(nd);
//            if (couldSplit)
//                foreach (Node ndDescendent in nd.DescendentLst)
//                    FullAutoGrow(ndDescendent);
//        }
    }
}
