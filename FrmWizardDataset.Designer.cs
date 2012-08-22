namespace Spartacus
{
    partial class FrmWizardDataset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWizardDataset));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbTables = new System.Windows.Forms.ListBox();
            this.btBack = new System.Windows.Forms.Button();
            this.btNext = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbMultivariate = new System.Windows.Forms.RadioButton();
            this.rbUnivariate = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::Spartacus.Properties.Resources.tree_landscape;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(384, 76);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbTables);
            this.groupBox1.Location = new System.Drawing.Point(13, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 228);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select the base table:";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // lbTables
            // 
            this.lbTables.FormattingEnabled = true;
            this.lbTables.Location = new System.Drawing.Point(14, 31);
            this.lbTables.Name = "lbTables";
            this.lbTables.Size = new System.Drawing.Size(332, 186);
            this.lbTables.TabIndex = 0;
            this.lbTables.Validated += new System.EventHandler(this.lbTables_Validated);
            this.lbTables.SelectedValueChanged += new System.EventHandler(this.lbTables_SelectedValueChanged);
            // 
            // btBack
            // 
            this.btBack.Location = new System.Drawing.Point(218, 339);
            this.btBack.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.btBack.Name = "btBack";
            this.btBack.Size = new System.Drawing.Size(75, 23);
            this.btBack.TabIndex = 2;
            this.btBack.Text = "<< Back";
            this.btBack.Click += new System.EventHandler(this.btBack_Click);
            // 
            // btNext
            // 
            this.btNext.Enabled = false;
            this.btNext.Location = new System.Drawing.Point(299, 339);
            this.btNext.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(75, 23);
            this.btNext.TabIndex = 3;
            this.btNext.Text = "Next >>";
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbMultivariate);
            this.groupBox2.Controls.Add(this.rbUnivariate);
            this.groupBox2.Location = new System.Drawing.Point(252, 231);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(122, 88);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Type of analysis";
            this.groupBox2.Visible = false;
            // 
            // rbMultivariate
            // 
            this.rbMultivariate.AutoSize = true;
            this.rbMultivariate.Location = new System.Drawing.Point(28, 54);
            this.rbMultivariate.Name = "rbMultivariate";
            this.rbMultivariate.Size = new System.Drawing.Size(79, 17);
            this.rbMultivariate.TabIndex = 1;
            this.rbMultivariate.Text = "Multivariate";
            this.rbMultivariate.Visible = false;
            // 
            // rbUnivariate
            // 
            this.rbUnivariate.AutoSize = true;
            this.rbUnivariate.Checked = true;
            this.rbUnivariate.Location = new System.Drawing.Point(28, 31);
            this.rbUnivariate.Name = "rbUnivariate";
            this.rbUnivariate.Size = new System.Drawing.Size(73, 17);
            this.rbUnivariate.TabIndex = 0;
            this.rbUnivariate.TabStop = true;
            this.rbUnivariate.Text = "Univariate";
            this.rbUnivariate.Visible = false;
            // 
            // FrmWizardDataset
            // 
            this.AcceptButton = this.btNext;
            this.ClientSize = new System.Drawing.Size(384, 371);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btNext);
            this.Controls.Add(this.btBack);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmWizardDataset";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Where is the data?";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmWizardDataset_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btBack;
        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.ListBox lbTables;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbMultivariate;
        private System.Windows.Forms.RadioButton rbUnivariate;
    }
}