namespace Spartacus
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.PanelMain = new System.Windows.Forms.Panel();
            this.lbInfo = new System.Windows.Forms.Label();
            this.PbBase = new System.Windows.Forms.PictureBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.StatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolBar = new System.Windows.Forms.ToolStrip();
            this.btNew = new System.Windows.Forms.ToolStripButton();
            this.btSave = new System.Windows.Forms.ToolStripButton();
            this.btImportData = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btFullGrow = new System.Windows.Forms.ToolStripButton();
            this.btPrune = new System.Windows.Forms.ToolStripButton();
            this.btGrowAndPrune = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btValidate = new System.Windows.Forms.ToolStripButton();
            this.btMoveData = new System.Windows.Forms.ToolStripButton();
            this.btOptions = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btAbout = new System.Windows.Forms.ToolStripButton();
            this.PanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbBase)).BeginInit();
            this.StatusBar.SuspendLayout();
            this.ToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelMain
            // 
            this.PanelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelMain.AutoScroll = true;
            this.PanelMain.BackColor = System.Drawing.Color.Moccasin;
            this.PanelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelMain.Controls.Add(this.lbInfo);
            this.PanelMain.Controls.Add(this.PbBase);
            this.PanelMain.Location = new System.Drawing.Point(1, 41);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Size = new System.Drawing.Size(594, 272);
            this.PanelMain.TabIndex = 4;
            // 
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.lbInfo.BackColor = System.Drawing.Color.White;
            this.lbInfo.Location = new System.Drawing.Point(-2, 0);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(0, 13);
            this.lbInfo.TabIndex = 5;
            // 
            // PbBase
            // 
            this.PbBase.BackColor = System.Drawing.Color.White;
            this.PbBase.Location = new System.Drawing.Point(-1, -1);
            this.PbBase.Name = "PbBase";
            this.PbBase.Size = new System.Drawing.Size(443, 170);
            this.PbBase.TabIndex = 4;
            this.PbBase.TabStop = false;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel1});
            this.StatusBar.Location = new System.Drawing.Point(0, 315);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(597, 22);
            this.StatusBar.TabIndex = 5;
            this.StatusBar.Text = "StatusBar";
            // 
            // StatusLabel1
            // 
            this.StatusLabel1.Name = "StatusLabel1";
            this.StatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // ToolBar
            // 
            this.ToolBar.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btNew,
            this.btSave,
            this.btImportData,
            this.toolStripSeparator1,
            this.btFullGrow,
            this.btPrune,
            this.btGrowAndPrune,
            this.toolStripSeparator2,
            this.btValidate,
            this.btMoveData,
            this.btOptions,
            this.toolStripSeparator3,
            this.btAbout});
            this.ToolBar.Location = new System.Drawing.Point(0, 0);
            this.ToolBar.Name = "ToolBar";
            this.ToolBar.Size = new System.Drawing.Size(597, 39);
            this.ToolBar.TabIndex = 6;
            this.ToolBar.Text = "ToolBar";
            // 
            // btNew
            // 
            this.btNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btNew.Image = ((System.Drawing.Image)(resources.GetObject("btNew.Image")));
            this.btNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btNew.Name = "btNew";
            this.btNew.Size = new System.Drawing.Size(36, 36);
            this.btNew.Text = "New";
            this.btNew.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // btSave
            // 
            this.btSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btSave.Image = ((System.Drawing.Image)(resources.GetObject("btSave.Image")));
            this.btSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(36, 36);
            this.btSave.Text = "Save";
            this.btSave.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // btImportData
            // 
            this.btImportData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btImportData.Image = ((System.Drawing.Image)(resources.GetObject("btImportData.Image")));
            this.btImportData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btImportData.Name = "btImportData";
            this.btImportData.Size = new System.Drawing.Size(36, 36);
            this.btImportData.Text = "Import Data";
            this.btImportData.Click += new System.EventHandler(this.ImportDataToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // btFullGrow
            // 
            this.btFullGrow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btFullGrow.Image = ((System.Drawing.Image)(resources.GetObject("btFullGrow.Image")));
            this.btFullGrow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btFullGrow.Name = "btFullGrow";
            this.btFullGrow.Size = new System.Drawing.Size(36, 36);
            this.btFullGrow.Text = "Full Grow";
            this.btFullGrow.Click += new System.EventHandler(this.fullgrowToolStripButton_Click);
            // 
            // btPrune
            // 
            this.btPrune.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btPrune.Image = ((System.Drawing.Image)(resources.GetObject("btPrune.Image")));
            this.btPrune.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btPrune.Name = "btPrune";
            this.btPrune.Size = new System.Drawing.Size(36, 36);
            this.btPrune.Text = "Prune";
            this.btPrune.Click += new System.EventHandler(this.pruneToolStripButton_Click);
            // 
            // btGrowAndPrune
            // 
            this.btGrowAndPrune.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btGrowAndPrune.Image = ((System.Drawing.Image)(resources.GetObject("btGrowAndPrune.Image")));
            this.btGrowAndPrune.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btGrowAndPrune.Name = "btGrowAndPrune";
            this.btGrowAndPrune.Size = new System.Drawing.Size(36, 36);
            this.btGrowAndPrune.Text = "Grow and Prune";
            this.btGrowAndPrune.Click += new System.EventHandler(this.growandPruneToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // btValidate
            // 
            this.btValidate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btValidate.Image = ((System.Drawing.Image)(resources.GetObject("btValidate.Image")));
            this.btValidate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btValidate.Name = "btValidate";
            this.btValidate.Size = new System.Drawing.Size(36, 36);
            this.btValidate.Text = "Validate";
            this.btValidate.Click += new System.EventHandler(this.toolStripButtonValidate_Click);
            // 
            // btMoveData
            // 
            this.btMoveData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btMoveData.Image = ((System.Drawing.Image)(resources.GetObject("btMoveData.Image")));
            this.btMoveData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btMoveData.Name = "btMoveData";
            this.btMoveData.Size = new System.Drawing.Size(36, 36);
            this.btMoveData.Text = "Move data";
            this.btMoveData.Click += new System.EventHandler(this.movedataToolStripButton_Click);
            // 
            // btOptions
            // 
            this.btOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btOptions.Image = ((System.Drawing.Image)(resources.GetObject("btOptions.Image")));
            this.btOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btOptions.Name = "btOptions";
            this.btOptions.Size = new System.Drawing.Size(36, 36);
            this.btOptions.Text = "Options";
            this.btOptions.Click += new System.EventHandler(this.optionsToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // btAbout
            // 
            this.btAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btAbout.Image = ((System.Drawing.Image)(resources.GetObject("btAbout.Image")));
            this.btAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btAbout.Name = "btAbout";
            this.btAbout.Size = new System.Drawing.Size(36, 36);
            this.btAbout.Text = "About";
            this.btAbout.Click += new System.EventHandler(this.toolStripButtonAbout_Click);
            // 
            // FrmMain
            // 
            this.ClientSize = new System.Drawing.Size(597, 337);
            this.Controls.Add(this.ToolBar);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.PanelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TreeWorks - Data Miner";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.PanelMain.ResumeLayout(false);
            this.PanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbBase)).EndInit();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.ToolBar.ResumeLayout(false);
            this.ToolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel PanelMain;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        public System.Windows.Forms.StatusStrip StatusBar;
        public System.Windows.Forms.ToolStripStatusLabel StatusLabel1;
        private System.Windows.Forms.PictureBox PbBase;
        private System.Windows.Forms.ToolStrip ToolBar;
        private System.Windows.Forms.ToolStripButton btNew;
        private System.Windows.Forms.ToolStripButton btSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btMoveData;
        private System.Windows.Forms.ToolStripButton btGrowAndPrune;
        private System.Windows.Forms.ToolStripButton btFullGrow;
        private System.Windows.Forms.ToolStripButton btPrune;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btOptions;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btImportData;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.ToolStripButton btAbout;
        private System.Windows.Forms.ToolStripButton btValidate;
    }
}

