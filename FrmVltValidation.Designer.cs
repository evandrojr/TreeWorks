namespace Spartacus {
    partial class FrmVltValidation {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVltValidation));
            this.grid = new System.Windows.Forms.DataGridView();
            this.tN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tNperc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vNperc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tMean = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vMean = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tStdev = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vStdev = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TTest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbTrTreeFitness = new System.Windows.Forms.Label();
            this.lbVlTreeFitness = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tN,
            this.vN,
            this.tNperc,
            this.vNperc,
            this.tMean,
            this.vMean,
            this.tStdev,
            this.vStdev,
            this.T,
            this.TTest,
            this.Result});
            this.grid.Location = new System.Drawing.Point(-2, 56);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersWidth = 50;
            this.grid.Size = new System.Drawing.Size(697, 211);
            this.grid.TabIndex = 0;
            this.grid.Text = "dataGridView1";
            // 
            // tN
            // 
            this.tN.HeaderText = "tN";
            this.tN.Name = "tN";
            this.tN.ReadOnly = true;
            this.tN.Width = 43;
            // 
            // vN
            // 
            this.vN.HeaderText = "vN";
            this.vN.Name = "vN";
            this.vN.ReadOnly = true;
            this.vN.Width = 46;
            // 
            // tNperc
            // 
            this.tNperc.HeaderText = "tN%";
            this.tNperc.Name = "tNperc";
            this.tNperc.ReadOnly = true;
            this.tNperc.Width = 51;
            // 
            // vNperc
            // 
            this.vNperc.HeaderText = "vN%";
            this.vNperc.Name = "vNperc";
            this.vNperc.ReadOnly = true;
            this.vNperc.Width = 54;
            // 
            // tMean
            // 
            this.tMean.HeaderText = "tMean";
            this.tMean.Name = "tMean";
            this.tMean.ReadOnly = true;
            this.tMean.Width = 62;
            // 
            // vMean
            // 
            this.vMean.HeaderText = "vMean";
            this.vMean.Name = "vMean";
            this.vMean.ReadOnly = true;
            this.vMean.Width = 65;
            // 
            // tStdev
            // 
            this.tStdev.HeaderText = "tStdev";
            this.tStdev.Name = "tStdev";
            this.tStdev.ReadOnly = true;
            this.tStdev.Width = 63;
            // 
            // vStdev
            // 
            this.vStdev.HeaderText = "vStdev";
            this.vStdev.Name = "vStdev";
            this.vStdev.ReadOnly = true;
            this.vStdev.Width = 66;
            // 
            // T
            // 
            this.T.HeaderText = "T";
            this.T.Name = "T";
            this.T.ReadOnly = true;
            this.T.Width = 39;
            // 
            // TTest
            // 
            this.TTest.HeaderText = "T(alfa/2, df)";
            this.TTest.Name = "TTest";
            this.TTest.ReadOnly = true;
            this.TTest.Width = 88;
            // 
            // Result
            // 
            this.Result.HeaderText = "Result";
            this.Result.Name = "Result";
            this.Result.ReadOnly = true;
            this.Result.Width = 62;
            // 
            // lbTrTreeFitness
            // 
            this.lbTrTreeFitness.AutoSize = true;
            this.lbTrTreeFitness.Location = new System.Drawing.Point(8, 8);
            this.lbTrTreeFitness.Name = "lbTrTreeFitness";
            this.lbTrTreeFitness.Size = new System.Drawing.Size(110, 13);
            this.lbTrTreeFitness.TabIndex = 1;
            this.lbTrTreeFitness.Text = "Training tree error: XX";
            // 
            // lbVlTreeFitness
            // 
            this.lbVlTreeFitness.AutoSize = true;
            this.lbVlTreeFitness.Location = new System.Drawing.Point(8, 28);
            this.lbVlTreeFitness.Name = "lbVlTreeFitness";
            this.lbVlTreeFitness.Size = new System.Drawing.Size(118, 13);
            this.lbVlTreeFitness.TabIndex = 2;
            this.lbVlTreeFitness.Text = "Validation tree error: XX";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(195, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Significance Level: 5%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "(95% certain that this is the correct decision)";
            // 
            // FrmVltValidation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 266);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbVlTreeFitness);
            this.Controls.Add(this.lbTrTreeFitness);
            this.Controls.Add(this.grid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmVltValidation";
            this.Text = "Model Validation";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmRValidation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Label lbTrTreeFitness;
        private System.Windows.Forms.Label lbVlTreeFitness;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn tN;
        private System.Windows.Forms.DataGridViewTextBoxColumn vN;
        private System.Windows.Forms.DataGridViewTextBoxColumn tNperc;
        private System.Windows.Forms.DataGridViewTextBoxColumn vNperc;
        private System.Windows.Forms.DataGridViewTextBoxColumn tMean;
        private System.Windows.Forms.DataGridViewTextBoxColumn vMean;
        private System.Windows.Forms.DataGridViewTextBoxColumn tStdev;
        private System.Windows.Forms.DataGridViewTextBoxColumn vStdev;
        private System.Windows.Forms.DataGridViewTextBoxColumn T;
        private System.Windows.Forms.DataGridViewTextBoxColumn TTest;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
    }
}