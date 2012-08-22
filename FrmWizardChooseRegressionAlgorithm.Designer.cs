namespace Spartacus {
    partial class FrmWizardChooseRegressionAlgorithm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWizardChooseRegressionAlgorithm));
            this.rbGrossReductionInVariance = new System.Windows.Forms.RadioButton();
            this.btNext = new System.Windows.Forms.Button();
            this.btBack = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbNetReductionInVariance = new System.Windows.Forms.RadioButton();
            this.pbBanner = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // rbGrossReductionInVariance
            // 
            this.rbGrossReductionInVariance.AutoSize = true;
            this.rbGrossReductionInVariance.Location = new System.Drawing.Point(20, 43);
            this.rbGrossReductionInVariance.Name = "rbGrossReductionInVariance";
            this.rbGrossReductionInVariance.Size = new System.Drawing.Size(156, 17);
            this.rbGrossReductionInVariance.TabIndex = 2;
            this.rbGrossReductionInVariance.TabStop = false;
            this.rbGrossReductionInVariance.Text = "Gross Reduction in Variance";
            // 
            // btNext
            // 
            this.btNext.Location = new System.Drawing.Point(289, 142);
            this.btNext.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(75, 23);
            this.btNext.TabIndex = 12;
            this.btNext.Text = "Finish";
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // btBack
            // 
            this.btBack.Location = new System.Drawing.Point(289, 170);
            this.btBack.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.btBack.Name = "btBack";
            this.btBack.Size = new System.Drawing.Size(75, 23);
            this.btBack.TabIndex = 11;
            this.btBack.Text = "<< Back";
            this.btBack.Click += new System.EventHandler(this.btBack_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbGrossReductionInVariance);
            this.groupBox1.Controls.Add(this.rbNetReductionInVariance);
            this.groupBox1.Location = new System.Drawing.Point(27, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 95);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Criterion";
            // 
            // rbNetReductionInVariance
            // 
            this.rbNetReductionInVariance.AutoSize = true;
            this.rbNetReductionInVariance.Checked = true;
            this.rbNetReductionInVariance.Location = new System.Drawing.Point(20, 19);
            this.rbNetReductionInVariance.Name = "rbNetReductionInVariance";
            this.rbNetReductionInVariance.Size = new System.Drawing.Size(146, 17);
            this.rbNetReductionInVariance.TabIndex = 0;
            this.rbNetReductionInVariance.Text = "Net Reduction in Variance";
            // 
            // pbBanner
            // 
            this.pbBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbBanner.Image = Spartacus.Properties.Resources.tree_landscape;
            this.pbBanner.Location = new System.Drawing.Point(0, 0);
            this.pbBanner.Name = "pbBanner";
            this.pbBanner.Size = new System.Drawing.Size(378, 75);
            this.pbBanner.TabIndex = 10;
            this.pbBanner.TabStop = false;
            // 
            // FrmWizardChooseRegressionAlgorithm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 210);
            this.ControlBox = false;
            this.Controls.Add(this.btNext);
            this.Controls.Add(this.btBack);
            this.Controls.Add(this.pbBanner);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmWizardChooseRegressionAlgorithm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choose the split criterion";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbGrossReductionInVariance;
        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.Button btBack;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbNetReductionInVariance;
        private System.Windows.Forms.PictureBox pbBanner;
    }
}