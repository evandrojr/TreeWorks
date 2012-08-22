namespace Spartacus {
    partial class FrmGraph {
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
            this.tbScale = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbNone = new System.Windows.Forms.RadioButton();
            this.rbOriginal = new System.Windows.Forms.RadioButton();
            this.rbBisector = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbScale)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbScale
            // 
            this.tbScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbScale.Location = new System.Drawing.Point(675, 25);
            this.tbScale.Maximum = 500;
            this.tbScale.Name = "tbScale";
            this.tbScale.Size = new System.Drawing.Size(118, 45);
            this.tbScale.TabIndex = 0;
            this.tbScale.Value = 100;
            this.tbScale.Scroll += new System.EventHandler(this.tbScale_Scroll);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbNone);
            this.groupBox1.Controls.Add(this.rbOriginal);
            this.groupBox1.Controls.Add(this.rbBisector);
            this.groupBox1.Location = new System.Drawing.Point(675, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(116, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Divisor";
            // 
            // rbNone
            // 
            this.rbNone.AutoSize = true;
            this.rbNone.Location = new System.Drawing.Point(16, 67);
            this.rbNone.Name = "rbNone";
            this.rbNone.Size = new System.Drawing.Size(51, 17);
            this.rbNone.TabIndex = 2;
            this.rbNone.Text = "None";
            this.rbNone.UseVisualStyleBackColor = true;
            this.rbNone.CheckedChanged += new System.EventHandler(this.rbNone_CheckedChanged);
            // 
            // rbOriginal
            // 
            this.rbOriginal.AutoSize = true;
            this.rbOriginal.Location = new System.Drawing.Point(16, 45);
            this.rbOriginal.Name = "rbOriginal";
            this.rbOriginal.Size = new System.Drawing.Size(60, 17);
            this.rbOriginal.TabIndex = 1;
            this.rbOriginal.TabStop = true;
            this.rbOriginal.Text = "Original";
            this.rbOriginal.UseVisualStyleBackColor = true;
            this.rbOriginal.CheckedChanged += new System.EventHandler(this.rbOriginal_CheckedChanged);
            // 
            // rbBisector
            // 
            this.rbBisector.AutoSize = true;
            this.rbBisector.Checked = true;
            this.rbBisector.Location = new System.Drawing.Point(16, 22);
            this.rbBisector.Name = "rbBisector";
            this.rbBisector.Size = new System.Drawing.Size(92, 17);
            this.rbBisector.TabIndex = 0;
            this.rbBisector.TabStop = true;
            this.rbBisector.Text = "Angle bisector";
            this.rbBisector.UseVisualStyleBackColor = true;
            this.rbBisector.CheckedChanged += new System.EventHandler(this.rbBisector_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(708, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Scale";
            // 
            // FrmGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 404);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbScale);
            this.Name = "FrmGraph";
            this.Text = "FrmGraph";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmGraph_Paint);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmGraph_FormClosing);
            this.Load += new System.EventHandler(this.FrmGraph_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbScale)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar tbScale;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbNone;
        private System.Windows.Forms.RadioButton rbOriginal;
        private System.Windows.Forms.RadioButton rbBisector;
        private System.Windows.Forms.Label label1;


    }
}