namespace Spartacus {
    partial class FrmContinuousSplitProperties {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmContinuousSplitProperties));
            this.label1 = new System.Windows.Forms.Label();
            this.lbLower = new System.Windows.Forms.Label();
            this.lbHigher = new System.Windows.Forms.Label();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSplitValue = new System.Windows.Forms.TextBox();
            this.btClose = new System.Windows.Forms.Button();
            this.btGainCalc = new System.Windows.Forms.Button();
            this.tbGain = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Variable range:";
            // 
            // lbLower
            // 
            this.lbLower.AutoSize = true;
            this.lbLower.Location = new System.Drawing.Point(22, 32);
            this.lbLower.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.lbLower.Name = "lbLower";
            this.lbLower.Size = new System.Drawing.Size(38, 13);
            this.lbLower.TabIndex = 1;
            this.lbLower.Text = "inferior";
            // 
            // lbHigher
            // 
            this.lbHigher.AutoSize = true;
            this.lbHigher.Location = new System.Drawing.Point(349, 32);
            this.lbHigher.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.lbHigher.Name = "lbHigher";
            this.lbHigher.Size = new System.Drawing.Size(44, 13);
            this.lbHigher.TabIndex = 3;
            this.lbHigher.Text = "superior";
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(15, 51);
            this.trackBar.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(344, 45);
            this.trackBar.TabIndex = 4;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(165, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Split at:";
            // 
            // tbSplitValue
            // 
            this.tbSplitValue.Location = new System.Drawing.Point(209, 8);
            this.tbSplitValue.MaxLength = 50;
            this.tbSplitValue.Multiline = true;
            this.tbSplitValue.Name = "tbSplitValue";
            this.tbSplitValue.ReadOnly = true;
            this.tbSplitValue.Size = new System.Drawing.Size(46, 17);
            this.tbSplitValue.TabIndex = 6;
            this.tbSplitValue.TabStop = false;
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(281, 122);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 7;
            this.btClose.Text = "Close";
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btGainCalc
            // 
            this.btGainCalc.Location = new System.Drawing.Point(13, 21);
            this.btGainCalc.Name = "btGainCalc";
            this.btGainCalc.Size = new System.Drawing.Size(106, 23);
            this.btGainCalc.TabIndex = 8;
            this.btGainCalc.Text = "Recalculate Gain";
            this.btGainCalc.Click += new System.EventHandler(this.btGainCalc_Click);
            // 
            // tbGain
            // 
            this.tbGain.Location = new System.Drawing.Point(139, 25);
            this.tbGain.MaxLength = 50;
            this.tbGain.Multiline = true;
            this.tbGain.Name = "tbGain";
            this.tbGain.ReadOnly = true;
            this.tbGain.Size = new System.Drawing.Size(52, 17);
            this.tbGain.TabIndex = 9;
            this.tbGain.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbGain);
            this.groupBox1.Controls.Add(this.btGainCalc);
            this.groupBox1.Location = new System.Drawing.Point(26, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(202, 55);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gain";
            // 
            // FrmContinuousSplitProperties
            // 
            this.ClientSize = new System.Drawing.Size(393, 167);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.tbSplitValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.lbHigher);
            this.Controls.Add(this.lbLower);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(12, 13);
            this.MaximizeBox = false;
            this.Name = "FrmContinuousSplitProperties";
            this.Text = "Split properties";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCartContinuousSplitProperties_FormClosing);
            this.Load += new System.EventHandler(this.FrmCartContinuousSplitProperties_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbLower;
        private System.Windows.Forms.Label lbHigher;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSplitValue;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btGainCalc;
        private System.Windows.Forms.TextBox tbGain;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}