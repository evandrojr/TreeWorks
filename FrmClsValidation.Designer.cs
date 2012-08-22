namespace Spartacus {
    partial class FrmClsValidation {
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
            this.grid = new System.Windows.Forms.DataGridView();
            this.tN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tNperc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vNperc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ewew = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tN,
            this.vN,
            this.tNperc,
            this.vNperc,
            this.Column3,
            this.Column4,
            this.Column1,
            this.Column2,
            this.ewew,
            this.Result,
            this.Column5,
            this.Column6});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersWidth = 50;
            this.grid.Size = new System.Drawing.Size(905, 306);
            this.grid.TabIndex = 5;
            this.grid.Text = "dataGridView1";
            this.grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellContentClick);
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
            // Column3
            // 
            this.Column3.HeaderText = "tClass";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 60;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "vClass";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 63;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Correct";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 66;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Misclassified";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 91;
            // 
            // ewew
            // 
            this.ewew.HeaderText = "Correct %";
            this.ewew.Name = "ewew";
            this.ewew.ReadOnly = true;
            this.ewew.Width = 77;
            // 
            // Result
            // 
            this.Result.HeaderText = "Misclassifed %";
            this.Result.Name = "Result";
            this.Result.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Relative Correct %";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 119;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Relative Misclassified %";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 144;
            // 
            // FrmClsValidation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 306);
            this.Controls.Add(this.grid);
            this.Name = "FrmClsValidation";
            this.Text = "FrmClsValidation";
            this.Load += new System.EventHandler(this.FrmClsValidation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn tN;
        private System.Windows.Forms.DataGridViewTextBoxColumn vN;
        private System.Windows.Forms.DataGridViewTextBoxColumn tNperc;
        private System.Windows.Forms.DataGridViewTextBoxColumn vNperc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ewew;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}