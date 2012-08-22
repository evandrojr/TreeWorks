namespace Spartacus {
    partial class FrmReferenceTableData {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReferenceTableData));
            this.lbPage = new System.Windows.Forms.Label();
            this.lbRecordCount = new System.Windows.Forms.Label();
            this.btLast = new System.Windows.Forms.Button();
            this.btFirst = new System.Windows.Forms.Button();
            this.btPrevious = new System.Windows.Forms.Button();
            this.btNext = new System.Windows.Forms.Button();
            this.Grid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // lbPage
            // 
            this.lbPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbPage.AutoSize = true;
            this.lbPage.Location = new System.Drawing.Point(306, 251);
            this.lbPage.Name = "lbPage";
            this.lbPage.Size = new System.Drawing.Size(78, 13);
            this.lbPage.TabIndex = 20;
            this.lbPage.Text = "Page X of XXX";
            // 
            // lbRecordCount
            // 
            this.lbRecordCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbRecordCount.AutoSize = true;
            this.lbRecordCount.Location = new System.Drawing.Point(490, 251);
            this.lbRecordCount.Name = "lbRecordCount";
            this.lbRecordCount.Size = new System.Drawing.Size(129, 13);
            this.lbRecordCount.TabIndex = 19;
            this.lbRecordCount.Text = "XXXX records in this table";
            // 
            // btLast
            // 
            this.btLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btLast.Location = new System.Drawing.Point(225, 246);
            this.btLast.Name = "btLast";
            this.btLast.Size = new System.Drawing.Size(75, 23);
            this.btLast.TabIndex = 18;
            this.btLast.Text = "Last >>>";
            this.btLast.Click += new System.EventHandler(this.btLast_Click);
            // 
            // btFirst
            // 
            this.btFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btFirst.Location = new System.Drawing.Point(0, 246);
            this.btFirst.Name = "btFirst";
            this.btFirst.Size = new System.Drawing.Size(75, 23);
            this.btFirst.TabIndex = 17;
            this.btFirst.Text = "<<< First";
            this.btFirst.Click += new System.EventHandler(this.btFirst_Click);
            // 
            // btPrevious
            // 
            this.btPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btPrevious.Location = new System.Drawing.Point(75, 246);
            this.btPrevious.Name = "btPrevious";
            this.btPrevious.Size = new System.Drawing.Size(75, 23);
            this.btPrevious.TabIndex = 16;
            this.btPrevious.Text = "<< Previous";
            this.btPrevious.Click += new System.EventHandler(this.btPrevious_Click);
            // 
            // btNext
            // 
            this.btNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btNext.Location = new System.Drawing.Point(150, 246);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(75, 23);
            this.btNext.TabIndex = 15;
            this.btNext.Text = "Next >>";
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // Grid
            // 
            this.Grid.AllowUserToAddRows = false;
            this.Grid.AllowUserToDeleteRows = false;
            this.Grid.AllowUserToOrderColumns = true;
            this.Grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Grid.Location = new System.Drawing.Point(0, -1);
            this.Grid.Name = "Grid";
            this.Grid.ReadOnly = true;
            this.Grid.RowHeadersWidth = 80;
            this.Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Grid.Size = new System.Drawing.Size(626, 241);
            this.Grid.TabIndex = 14;
            // 
            // FrmReferenceTableData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 275);
            this.Controls.Add(this.lbPage);
            this.Controls.Add(this.lbRecordCount);
            this.Controls.Add(this.btLast);
            this.Controls.Add(this.btFirst);
            this.Controls.Add(this.btPrevious);
            this.Controls.Add(this.btNext);
            this.Controls.Add(this.Grid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmReferenceTableData";
            this.Text = "Table Data";
            this.Load += new System.EventHandler(this.FrmTableData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbPage;
        private System.Windows.Forms.Label lbRecordCount;
        private System.Windows.Forms.Button btLast;
        private System.Windows.Forms.Button btFirst;
        private System.Windows.Forms.Button btPrevious;
        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.DataGridView Grid;
    }
}