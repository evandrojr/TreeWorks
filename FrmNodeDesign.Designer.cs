namespace Spartacus {
    partial class FrmNodeDesign {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNodeDesign));
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nodeCategorical = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Selector = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewdataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manuallysplitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autosplitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullautogrowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.LabelCentral = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nodeCategorical)).BeginInit();
            this.Selector.SuspendLayout();
            this.SuspendLayout();
// 
// dataGridViewTextBoxColumn2
// 
            this.dataGridViewTextBoxColumn2.Frozen = true;
            this.dataGridViewTextBoxColumn2.HeaderText = "";
            this.dataGridViewTextBoxColumn2.Name = "Center";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
// 
// panel1
// 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Location = new System.Drawing.Point(33, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(143, 97);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
// 
// nodeCategorical
// 
            this.nodeCategorical.AllowUserToAddRows = false;
            this.nodeCategorical.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.nodeCategorical.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
//            this.nodeCategorical.AutoRelocate = true;
            this.nodeCategorical.AutoSize = true;
//            this.nodeCategorical.AutoSizeColumnHeadersEnabled = true;
//            this.nodeCategorical.AutoSizeRowHeadersMode = System.Windows.Forms.DataGridViewAutoSizeRowHeadersMode.AllRows;
//            this.nodeCategorical.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.ColumnsAllRows;
            this.nodeCategorical.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.nodeCategorical.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.nodeCategorical.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.nodeCategorical.ColumnHeadersVisible = false;
            this.nodeCategorical.Columns.Add(this.dataGridViewTextBoxColumn6);
            this.nodeCategorical.Columns.Add(this.dataGridViewTextBoxColumn7);
            this.nodeCategorical.Columns.Add(this.dataGridViewTextBoxColumn8);
            this.nodeCategorical.Columns.Add(this.dataGridViewTextBoxColumn12);
//            this.nodeCategorical.ColumnsResizable = false;
            this.nodeCategorical.ContextMenuStrip = this.Selector;
            this.nodeCategorical.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.nodeCategorical.DefaultCellStyle = dataGridViewCellStyle4;
            this.nodeCategorical.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.nodeCategorical.Location = new System.Drawing.Point(162, 150);
            this.nodeCategorical.Margin = new System.Windows.Forms.Padding(0);
            this.nodeCategorical.MultiSelect = false;
            this.nodeCategorical.Name = "nodeCategorical";
            this.nodeCategorical.ReadOnly = true;
            this.nodeCategorical.RowHeadersVisible = false;
            this.nodeCategorical.RowTemplate.Height = 10;
            this.nodeCategorical.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.nodeCategorical.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect;
            this.nodeCategorical.Size = new System.Drawing.Size(221, 190);
            this.nodeCategorical.TabIndex = 2;
// 
// dataGridViewTextBoxColumn6
// 
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn6.Frozen = true;
            this.dataGridViewTextBoxColumn6.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn6.Name = "c1";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 10;
// 
// dataGridViewTextBoxColumn7
// 
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn7.Frozen = true;
            this.dataGridViewTextBoxColumn7.HeaderText = "Column2";
            this.dataGridViewTextBoxColumn7.Name = "c2";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 10;
// 
// dataGridViewTextBoxColumn8
// 
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn8.Frozen = true;
            this.dataGridViewTextBoxColumn8.HeaderText = "Column3";
            this.dataGridViewTextBoxColumn8.Name = "c3";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 10;
// 
// dataGridViewTextBoxColumn12
// 
            this.dataGridViewTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn12.Frozen = true;
            this.dataGridViewTextBoxColumn12.HeaderText = "Column4";
            this.dataGridViewTextBoxColumn12.Name = "c4";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 10;
// 
// Selector
// 
            this.Selector.AllowDrop = true;
            this.Selector.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewdataToolStripMenuItem,
            this.manuallysplitToolStripMenuItem,
            this.autosplitToolStripMenuItem,
            this.fullautogrowToolStripMenuItem});
            this.Selector.Location = new System.Drawing.Point(23, 61);
            this.Selector.Name = "Menu";
            this.Selector.Size = new System.Drawing.Size(133, 92);
// 
// viewdataToolStripMenuItem
// 
            this.viewdataToolStripMenuItem.Name = "viewdataToolStripMenuItem";
//            this.viewdataToolStripMenuItem.SettingsKey = "FrmNodeDesign.viewdataToolStripMenuItem";
            this.viewdataToolStripMenuItem.Text = "View data";
            this.viewdataToolStripMenuItem.Click += new System.EventHandler(this.viewdataToolStripMenuItem_Click);
// 
// manuallysplitToolStripMenuItem
// 
            this.manuallysplitToolStripMenuItem.Name = "manuallysplitToolStripMenuItem";
//            this.manuallysplitToolStripMenuItem.SettingsKey = "FrmNodeDesign.manuallysplitToolStripMenuItem";
            this.manuallysplitToolStripMenuItem.Text = "Manually split";
            this.manuallysplitToolStripMenuItem.Click += new System.EventHandler(this.manuallysplitToolStripMenuItem_Click);
// 
// autosplitToolStripMenuItem
// 
            this.autosplitToolStripMenuItem.Name = "autosplitToolStripMenuItem";
//            this.autosplitToolStripMenuItem.SettingsKey = "FrmNodeDesign.autosplitToolStripMenuItem";
            this.autosplitToolStripMenuItem.Text = "Auto split";
            this.autosplitToolStripMenuItem.Click += new System.EventHandler(this.autosplitToolStripMenuItem_Click);
// 
// fullautogrowToolStripMenuItem
// 
            this.fullautogrowToolStripMenuItem.Name = "fullautogrowToolStripMenuItem";
//            this.fullautogrowToolStripMenuItem.SettingsKey = "FrmNodeDesign.fullautogrowToolStripMenuItem";
            this.fullautogrowToolStripMenuItem.Text = "Full auto grow";
            this.fullautogrowToolStripMenuItem.Click += new System.EventHandler(this.fullautogrowToolStripMenuItem_Click);
// 
// timer
// 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
// 
// LabelCentral
// 
            this.LabelCentral.BackColor = System.Drawing.Color.White;
            this.LabelCentral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelCentral.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LabelCentral.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelCentral.Location = new System.Drawing.Point(348, 36);
            this.LabelCentral.Name = "LabelCentral";
            this.LabelCentral.Size = new System.Drawing.Size(51, 42);
            this.LabelCentral.TabIndex = 3;
            this.LabelCentral.Text = "Node0001";
// 
// FrmNodeDesign
// 
            this.AutoScaleDimensions = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(595, 490);
            this.Controls.Add(this.LabelCentral);
            this.Controls.Add(this.nodeCategorical);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmNodeDesign";
            this.Text = "FrmNodeDesign";
            this.Load += new System.EventHandler(this.FrmNodeDesign_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nodeCategorical)).EndInit();
            this.Selector.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView nodeCategorical;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ContextMenuStrip Selector;
        private System.Windows.Forms.ToolStripMenuItem viewdataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manuallysplitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autosplitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fullautogrowToolStripMenuItem;
        private System.Windows.Forms.Label LabelCentral;


    }
}