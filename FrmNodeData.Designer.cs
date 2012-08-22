namespace Spartacus {
    partial class FrmNodeData {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNodeData));
            this.Grid = new System.Windows.Forms.DataGridView();
            this.btNext = new System.Windows.Forms.Button();
            this.btPrevious = new System.Windows.Forms.Button();
            this.btFirst = new System.Windows.Forms.Button();
            this.btLast = new System.Windows.Forms.Button();
            this.lbRecordCount = new System.Windows.Forms.Label();
            this.lbPage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // Grid
            // 
            this.Grid.AllowUserToAddRows = false;
            this.Grid.AllowUserToDeleteRows = false;
            this.Grid.AllowUserToOrderColumns = true;
            this.Grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Grid.Location = new System.Drawing.Point(1, 0);
            this.Grid.Name = "Grid";
            this.Grid.ReadOnly = true;
            this.Grid.RowHeadersWidth = 80;
            this.Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Grid.Size = new System.Drawing.Size(634, 286);
            this.Grid.TabIndex = 0;
            this.Grid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Grid_ColumnHeaderMouseClick);
            this.Grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellContentClick);
            // 
            // btNext
            // 
            this.btNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btNext.Location = new System.Drawing.Point(151, 292);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(75, 23);
            this.btNext.TabIndex = 1;
            this.btNext.Text = "Next >>";
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // btPrevious
            // 
            this.btPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btPrevious.Location = new System.Drawing.Point(76, 292);
            this.btPrevious.Name = "btPrevious";
            this.btPrevious.Size = new System.Drawing.Size(75, 23);
            this.btPrevious.TabIndex = 2;
            this.btPrevious.Text = "<< Previous";
            this.btPrevious.Click += new System.EventHandler(this.btPrevious_Click);
            // 
            // btFirst
            // 
            this.btFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btFirst.Location = new System.Drawing.Point(1, 292);
            this.btFirst.Name = "btFirst";
            this.btFirst.Size = new System.Drawing.Size(75, 23);
            this.btFirst.TabIndex = 3;
            this.btFirst.Text = "<<< First";
            this.btFirst.Click += new System.EventHandler(this.btFirst_Click);
            // 
            // btLast
            // 
            this.btLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btLast.Location = new System.Drawing.Point(226, 292);
            this.btLast.Name = "btLast";
            this.btLast.Size = new System.Drawing.Size(75, 23);
            this.btLast.TabIndex = 4;
            this.btLast.Text = "Last >>>";
            this.btLast.Click += new System.EventHandler(this.btLast_Click);
            // 
            // lbRecordCount
            // 
            this.lbRecordCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbRecordCount.AutoSize = true;
            this.lbRecordCount.Location = new System.Drawing.Point(504, 297);
            this.lbRecordCount.Name = "lbRecordCount";
            this.lbRecordCount.Size = new System.Drawing.Size(130, 13);
            this.lbRecordCount.TabIndex = 5;
            this.lbRecordCount.Text = "XXXX records in this node";
            // 
            // lbPage
            // 
            this.lbPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbPage.AutoSize = true;
            this.lbPage.Location = new System.Drawing.Point(307, 297);
            this.lbPage.Name = "lbPage";
            this.lbPage.Size = new System.Drawing.Size(78, 13);
            this.lbPage.TabIndex = 6;
            this.lbPage.Text = "Page X of XXX";
            // 
            // FrmNodeData
            // 
            this.ClientSize = new System.Drawing.Size(633, 319);
            this.Controls.Add(this.lbPage);
            this.Controls.Add(this.lbRecordCount);
            this.Controls.Add(this.btLast);
            this.Controls.Add(this.btFirst);
            this.Controls.Add(this.btPrevious);
            this.Controls.Add(this.btNext);
            this.Controls.Add(this.Grid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmNodeData";
            this.Text = "Data of Node";
            this.Load += new System.EventHandler(this.FrmNodeData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView Grid;
        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.Button btPrevious;
        private System.Windows.Forms.Button btFirst;
        private System.Windows.Forms.Button btLast;
        private System.Windows.Forms.Label lbRecordCount;
        private System.Windows.Forms.Label lbPage;

    }
}