namespace Spartacus {
    partial class FrmWizardImportDataDefineSchema {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWizardImportDataDefineSchema));
            this.grid = new System.Windows.Forms.DataGridView();
            this.Import = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.VariableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VariableType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Blanks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mean = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Minimum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Maximum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btNext = new System.Windows.Forms.Button();
            this.btBack = new System.Windows.Forms.Button();
            this.lblRegistryCount = new System.Windows.Forms.Label();
            this.pbBanner = new System.Windows.Forms.PictureBox();
            this.mtbTableName = new System.Windows.Forms.MaskedTextBox();
            this.cbRemoveRowsWithBlanks = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Import,
            this.VariableName,
            this.VariableType,
            this.Blanks,
            this.Mean,
            this.Minimum,
            this.Maximum});
            this.grid.Location = new System.Drawing.Point(0, 123);
            this.grid.Name = "grid";
            this.grid.RowHeadersVisible = false;
            this.grid.Size = new System.Drawing.Size(595, 226);
            this.grid.TabIndex = 13;
            this.grid.Text = "dataGridView1";
            // 
            // Import
            // 
            this.Import.HeaderText = "Import?";
            this.Import.Name = "Import";
            this.Import.Width = 50;
            // 
            // VariableName
            // 
            this.VariableName.HeaderText = "Name";
            this.VariableName.Name = "VariableName";
            this.VariableName.Width = 200;
            // 
            // VariableType
            // 
            this.VariableType.HeaderText = "Type";
            this.VariableType.Name = "VariableType";
            this.VariableType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.VariableType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Blanks
            // 
            this.Blanks.HeaderText = "Blanks";
            this.Blanks.Name = "Blanks";
            this.Blanks.ReadOnly = true;
            // 
            // Mean
            // 
            this.Mean.HeaderText = "Mean";
            this.Mean.Name = "Mean";
            this.Mean.ReadOnly = true;
            // 
            // Minimum
            // 
            this.Minimum.HeaderText = "Minimum";
            this.Minimum.Name = "Minimum";
            this.Minimum.ReadOnly = true;
            // 
            // Maximum
            // 
            this.Maximum.HeaderText = "Maximum";
            this.Maximum.Name = "Maximum";
            this.Maximum.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Name of the database table to be created:";
            // 
            // btNext
            // 
            this.btNext.Location = new System.Drawing.Point(513, 362);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(75, 23);
            this.btNext.TabIndex = 16;
            this.btNext.Text = "Next >>";
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // btBack
            // 
            this.btBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btBack.Location = new System.Drawing.Point(427, 362);
            this.btBack.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.btBack.Name = "btBack";
            this.btBack.Size = new System.Drawing.Size(75, 23);
            this.btBack.TabIndex = 17;
            this.btBack.Text = "<< Back";
            this.btBack.Click += new System.EventHandler(this.btBack_Click);
            // 
            // lblRegistryCount
            // 
            this.lblRegistryCount.AutoSize = true;
            this.lblRegistryCount.Location = new System.Drawing.Point(432, 97);
            this.lblRegistryCount.Name = "lblRegistryCount";
            this.lblRegistryCount.Size = new System.Drawing.Size(83, 13);
            this.lblRegistryCount.TabIndex = 18;
            this.lblRegistryCount.Text = "lblRegistryCount";
            // 
            // pbBanner
            // 
            this.pbBanner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbBanner.Image = global::Spartacus.Properties.Resources.tree_landscape;
            this.pbBanner.Location = new System.Drawing.Point(0, 0);
            this.pbBanner.Name = "pbBanner";
            this.pbBanner.Size = new System.Drawing.Size(595, 73);
            this.pbBanner.TabIndex = 12;
            this.pbBanner.TabStop = false;
            // 
            // mtbTableName
            // 
            this.mtbTableName.Location = new System.Drawing.Point(234, 90);
            this.mtbTableName.Mask = "Laaaaaaaaaaaaaa   ";
            this.mtbTableName.Name = "mtbTableName";
            this.mtbTableName.Size = new System.Drawing.Size(100, 20);
            this.mtbTableName.TabIndex = 21;
            this.mtbTableName.Validated += new System.EventHandler(this.mtbTableName_Validated);
            // 
            // cbRemoveRowsWithBlanks
            // 
            this.cbRemoveRowsWithBlanks.AutoSize = true;
            this.cbRemoveRowsWithBlanks.Checked = true;
            this.cbRemoveRowsWithBlanks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRemoveRowsWithBlanks.Location = new System.Drawing.Point(10, 368);
            this.cbRemoveRowsWithBlanks.Name = "cbRemoveRowsWithBlanks";
            this.cbRemoveRowsWithBlanks.Size = new System.Drawing.Size(176, 17);
            this.cbRemoveRowsWithBlanks.TabIndex = 22;
            this.cbRemoveRowsWithBlanks.Text = "Remove rows with blank values";
            this.cbRemoveRowsWithBlanks.UseVisualStyleBackColor = true;
            // 
            // FrmWizardImportDataDefineSchema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 396);
            this.ControlBox = false;
            this.Controls.Add(this.cbRemoveRowsWithBlanks);
            this.Controls.Add(this.mtbTableName);
            this.Controls.Add(this.lblRegistryCount);
            this.Controls.Add(this.btBack);
            this.Controls.Add(this.btNext);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.pbBanner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmWizardImportDataDefineSchema";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Define variables";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmWizardImportDataDefineSchema_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.Button btBack;
        private System.Windows.Forms.Label lblRegistryCount;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.PictureBox pbBanner;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Import;
        private System.Windows.Forms.DataGridViewTextBoxColumn VariableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn VariableType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Blanks;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mean;
        private System.Windows.Forms.DataGridViewTextBoxColumn Minimum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Maximum;
        private System.Windows.Forms.MaskedTextBox mtbTableName;
        private System.Windows.Forms.CheckBox cbRemoveRowsWithBlanks;

    }
}