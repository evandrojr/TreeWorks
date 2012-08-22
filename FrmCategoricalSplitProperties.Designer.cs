namespace Spartacus {
    partial class FrmCategoricalSplitProperties {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCategoricalSplitProperties));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbLeft = new System.Windows.Forms.ListBox();
            this.lbRight = new System.Windows.Forms.ListBox();
            this.btToRight = new System.Windows.Forms.Button();
            this.btToLeft = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.lbGain = new System.Windows.Forms.Label();
            this.btRecalculateGain = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btLeftShowAsText = new System.Windows.Forms.Button();
            this.btRightShowAsText = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Left Node";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(234, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Right Node";
            // 
            // lbLeft
            // 
            this.lbLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lbLeft.FormattingEnabled = true;
            this.lbLeft.Location = new System.Drawing.Point(11, 47);
            this.lbLeft.Name = "lbLeft";
            this.lbLeft.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbLeft.Size = new System.Drawing.Size(154, 212);
            this.lbLeft.TabIndex = 2;
            // 
            // lbRight
            // 
            this.lbRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lbRight.FormattingEnabled = true;
            this.lbRight.Location = new System.Drawing.Point(192, 47);
            this.lbRight.Name = "lbRight";
            this.lbRight.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbRight.Size = new System.Drawing.Size(154, 212);
            this.lbRight.TabIndex = 3;
            // 
            // btToRight
            // 
            this.btToRight.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btToRight.Location = new System.Drawing.Point(11, 297);
            this.btToRight.Name = "btToRight";
            this.btToRight.Size = new System.Drawing.Size(154, 23);
            this.btToRight.TabIndex = 4;
            this.btToRight.Text = "Send to right >>";
            this.btToRight.Click += new System.EventHandler(this.btToRight_Click);
            // 
            // btToLeft
            // 
            this.btToLeft.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btToLeft.Location = new System.Drawing.Point(192, 297);
            this.btToLeft.Name = "btToLeft";
            this.btToLeft.Size = new System.Drawing.Size(154, 23);
            this.btToLeft.TabIndex = 5;
            this.btToLeft.Text = "<< Send to left";
            this.btToLeft.Click += new System.EventHandler(this.btToLeft_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btClose.Location = new System.Drawing.Point(249, 344);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(79, 33);
            this.btClose.TabIndex = 6;
            this.btClose.Text = "Close";
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // lbGain
            // 
            this.lbGain.AutoSize = true;
            this.lbGain.Location = new System.Drawing.Point(145, 21);
            this.lbGain.Name = "lbGain";
            this.lbGain.Size = new System.Drawing.Size(9, 13);
            this.lbGain.TabIndex = 7;
            this.lbGain.Text = "?";
            // 
            // btRecalculateGain
            // 
            this.btRecalculateGain.Location = new System.Drawing.Point(6, 16);
            this.btRecalculateGain.Name = "btRecalculateGain";
            this.btRecalculateGain.Size = new System.Drawing.Size(120, 23);
            this.btRecalculateGain.TabIndex = 8;
            this.btRecalculateGain.Text = "Recalculate gain";
            this.btRecalculateGain.Click += new System.EventHandler(this.btRecalculateGain_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.groupBox1.Controls.Add(this.lbGain);
            this.groupBox1.Controls.Add(this.btRecalculateGain);
            this.groupBox1.Location = new System.Drawing.Point(11, 333);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(208, 49);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gain";
            // 
            // btLeftShowAsText
            // 
            this.btLeftShowAsText.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btLeftShowAsText.Location = new System.Drawing.Point(48, 265);
            this.btLeftShowAsText.Name = "btLeftShowAsText";
            this.btLeftShowAsText.Size = new System.Drawing.Size(79, 23);
            this.btLeftShowAsText.TabIndex = 11;
            this.btLeftShowAsText.Text = "Show as text";
            this.btLeftShowAsText.Click += new System.EventHandler(this.btLeftShowAsText_Click);
            // 
            // btRightShowAsText
            // 
            this.btRightShowAsText.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btRightShowAsText.Location = new System.Drawing.Point(235, 266);
            this.btRightShowAsText.Name = "btRightShowAsText";
            this.btRightShowAsText.Size = new System.Drawing.Size(79, 23);
            this.btRightShowAsText.TabIndex = 12;
            this.btRightShowAsText.Text = "Show as text";
            this.btRightShowAsText.Click += new System.EventHandler(this.btRightShowAsText_Click);
            // 
            // FrmCartCategoricalSplitProperties
            // 
            this.ClientSize = new System.Drawing.Size(362, 392);
            this.Controls.Add(this.btRightShowAsText);
            this.Controls.Add(this.btLeftShowAsText);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btToLeft);
            this.Controls.Add(this.btToRight);
            this.Controls.Add(this.lbRight);
            this.Controls.Add(this.lbLeft);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(370, 900);
            this.MinimumSize = new System.Drawing.Size(370, 300);
            this.Name = "FrmCartCategoricalSplitProperties";
            this.Text = "Split properties";
            this.Load += new System.EventHandler(this.FrmCartCategoricalSplitProperties_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCartCategoricalSplitProperties_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbLeft;
        private System.Windows.Forms.ListBox lbRight;
        private System.Windows.Forms.Button btToRight;
        private System.Windows.Forms.Button btToLeft;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Label lbGain;
        private System.Windows.Forms.Button btRecalculateGain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btLeftShowAsText;
        private System.Windows.Forms.Button btRightShowAsText;
    }
}