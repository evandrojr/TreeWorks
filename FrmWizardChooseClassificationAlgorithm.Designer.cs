namespace Spartacus
{
    partial class FrmWizardChooseClassificationAlgorithm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWizardChooseClassificationAlgorithm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbEntropy = new System.Windows.Forms.RadioButton();
            this.rbMaxDif = new System.Windows.Forms.RadioButton();
            this.rbGini = new System.Windows.Forms.RadioButton();
            this.btNext = new System.Windows.Forms.Button();
            this.btBack = new System.Windows.Forms.Button();
            this.pbBanner = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbEntropy);
            this.groupBox1.Controls.Add(this.rbMaxDif);
            this.groupBox1.Controls.Add(this.rbGini);
            this.groupBox1.Location = new System.Drawing.Point(98, 95);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(191, 89);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Criterion";
            // 
            // rbEntropy
            // 
            this.rbEntropy.AutoSize = true;
            this.rbEntropy.Location = new System.Drawing.Point(47, 51);
            this.rbEntropy.Name = "rbEntropy";
            this.rbEntropy.Size = new System.Drawing.Size(116, 17);
            this.rbEntropy.TabIndex = 2;
            this.rbEntropy.Text = "Information Entropy";
            // 
            // rbMaxDif
            // 
            this.rbMaxDif.AutoSize = true;
            this.rbMaxDif.Location = new System.Drawing.Point(156, 28);
            this.rbMaxDif.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.rbMaxDif.Name = "rbMaxDif";
            this.rbMaxDif.Size = new System.Drawing.Size(58, 17);
            this.rbMaxDif.TabIndex = 1;
            this.rbMaxDif.Text = "MaxDif";
            this.rbMaxDif.Visible = false;
            // 
            // rbGini
            // 
            this.rbGini.AutoSize = true;
            this.rbGini.Checked = true;
            this.rbGini.Location = new System.Drawing.Point(47, 28);
            this.rbGini.Name = "rbGini";
            this.rbGini.Size = new System.Drawing.Size(72, 17);
            this.rbGini.TabIndex = 0;
            this.rbGini.TabStop = true;
            this.rbGini.Text = "Gini Index";
            // 
            // btNext
            // 
            this.btNext.Location = new System.Drawing.Point(335, 133);
            this.btNext.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(75, 23);
            this.btNext.TabIndex = 8;
            this.btNext.Text = "Finish";
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // btBack
            // 
            this.btBack.Location = new System.Drawing.Point(335, 161);
            this.btBack.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.btBack.Name = "btBack";
            this.btBack.Size = new System.Drawing.Size(75, 23);
            this.btBack.TabIndex = 7;
            this.btBack.Text = "<< Back";
            this.btBack.Click += new System.EventHandler(this.btBack_Click);
            // 
            // pbBanner
            // 
            this.pbBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbBanner.Image = global::Spartacus.Properties.Resources.tree_landscape;
            this.pbBanner.Location = new System.Drawing.Point(0, 0);
            this.pbBanner.Name = "pbBanner";
            this.pbBanner.Size = new System.Drawing.Size(424, 75);
            this.pbBanner.TabIndex = 6;
            this.pbBanner.TabStop = false;
            // 
            // FrmWizardChooseClassificationAlgorithm
            // 
            this.ClientSize = new System.Drawing.Size(424, 197);
            this.ControlBox = false;
            this.Controls.Add(this.btNext);
            this.Controls.Add(this.btBack);
            this.Controls.Add(this.pbBanner);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmWizardChooseClassificationAlgorithm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choose the split criterion";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbEntropy;
        private System.Windows.Forms.RadioButton rbGini;
        private System.Windows.Forms.RadioButton rbMaxDif;
        private System.Windows.Forms.PictureBox pbBanner;
        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.Button btBack;
    }
}