namespace Spartacus
{
    partial class FrmWizardNewProject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWizardNewProject));
            this.pbBanner = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbOpenProject = new System.Windows.Forms.RadioButton();
            this.rbNewProject = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btNext = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbBanner
            // 
            this.pbBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbBanner.Image = global::Spartacus.Properties.Resources.tree_landscape;
            this.pbBanner.Location = new System.Drawing.Point(0, 0);
            this.pbBanner.Name = "pbBanner";
            this.pbBanner.Size = new System.Drawing.Size(399, 75);
            this.pbBanner.TabIndex = 0;
            this.pbBanner.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbOpenProject);
            this.groupBox1.Controls.Add(this.rbNewProject);
            this.groupBox1.Location = new System.Drawing.Point(93, 86);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 78);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // rbOpenProject
            // 
            this.rbOpenProject.AutoSize = true;
            this.rbOpenProject.Location = new System.Drawing.Point(17, 44);
            this.rbOpenProject.Name = "rbOpenProject";
            this.rbOpenProject.Size = new System.Drawing.Size(142, 17);
            this.rbOpenProject.TabIndex = 1;
            this.rbOpenProject.Text = "Open an existing project.";
            // 
            // rbNewProject
            // 
            this.rbNewProject.AutoSize = true;
            this.rbNewProject.Checked = true;
            this.rbNewProject.Location = new System.Drawing.Point(17, 20);
            this.rbNewProject.Name = "rbNewProject";
            this.rbNewProject.Size = new System.Drawing.Size(117, 17);
            this.rbNewProject.TabIndex = 0;
            this.rbNewProject.TabStop = true;
            this.rbNewProject.Text = "Start a new project.";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(99, 177);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(192, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Don’t show this dialog in the future.";
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btNext
            // 
            this.btNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btNext.Location = new System.Drawing.Point(316, 173);
            this.btNext.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(75, 23);
            this.btNext.TabIndex = 4;
            this.btNext.Text = "Next >>";
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // FrmWizardNewProject
            // 
            this.AcceptButton = this.btNext;
            this.ClientSize = new System.Drawing.Size(399, 202);
            this.Controls.Add(this.btNext);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pbBanner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmWizardNewProject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "What would you like to do?";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmWizardNewProject_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbBanner;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btNext;
        public System.Windows.Forms.RadioButton rbOpenProject;
        public System.Windows.Forms.RadioButton rbNewProject;
    }
}