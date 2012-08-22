namespace Spartacus {
    partial class FrmWizardImportDataChooseDimensionality {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWizardImportDataChooseDimensionality));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbMultivariate = new System.Windows.Forms.RadioButton();
            this.rbUnivariate = new System.Windows.Forms.RadioButton();
            this.btNext = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.pbBanner = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbMultivariate);
            this.groupBox1.Controls.Add(this.rbUnivariate);
            this.groupBox1.Location = new System.Drawing.Point(10, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 86);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "How many variables will be used for the splits?";
            // 
            // rbMultivariate
            // 
            this.rbMultivariate.AutoSize = true;
            this.rbMultivariate.Location = new System.Drawing.Point(16, 54);
            this.rbMultivariate.Name = "rbMultivariate";
            this.rbMultivariate.Size = new System.Drawing.Size(187, 17);
            this.rbMultivariate.TabIndex = 4;
            this.rbMultivariate.TabStop = false;
            this.rbMultivariate.Text = "One or more variables (Multivariate)";
            // 
            // rbUnivariate
            // 
            this.rbUnivariate.AutoSize = true;
            this.rbUnivariate.Checked = true;
            this.rbUnivariate.Location = new System.Drawing.Point(16, 30);
            this.rbUnivariate.Name = "rbUnivariate";
            this.rbUnivariate.Size = new System.Drawing.Size(138, 17);
            this.rbUnivariate.TabIndex = 3;
            this.rbUnivariate.Text = "One variable (Univariate)";
            // 
            // btNext
            // 
            this.btNext.Location = new System.Drawing.Point(267, 180);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(75, 23);
            this.btNext.TabIndex = 17;
            this.btNext.Text = "Next >>";
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "CSV files|*.csv|Text files|*.txt";
            this.openFileDialog.Title = "Choose the CSV file";
            // 
            // pbBanner
            // 
            this.pbBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbBanner.Image = Spartacus.Properties.Resources.tree_landscape;
            this.pbBanner.Location = new System.Drawing.Point(0, 0);
            this.pbBanner.Name = "pbBanner";
            this.pbBanner.Size = new System.Drawing.Size(353, 75);
            this.pbBanner.TabIndex = 15;
            this.pbBanner.TabStop = false;
            // 
            // FrmWizardImportDataChooseDimensionality
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 209);
            this.Controls.Add(this.btNext);
            this.Controls.Add(this.pbBanner);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmWizardImportDataChooseDimensionality";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "What kind of analysis will be performed?";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.PictureBox pbBanner;
        private System.Windows.Forms.RadioButton rbUnivariate;
        private System.Windows.Forms.RadioButton rbMultivariate;
    }
}