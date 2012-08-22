namespace Spartacus {
    partial class FrmPredict {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPredict));
            this.Grid = new System.Windows.Forms.DataGridView();
            this.btNaiveBayes = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // Grid
            // 
            this.Grid.AllowUserToAddRows = false;
            this.Grid.AllowUserToDeleteRows = false;
            this.Grid.Location = new System.Drawing.Point(-1, 0);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(639, 92);
            this.Grid.TabIndex = 0;
            this.Grid.Text = "dataGridView1";
            // 
            // btNaiveBayes
            // 
            this.btNaiveBayes.Location = new System.Drawing.Point(549, 400);
            this.btNaiveBayes.Name = "btNaiveBayes";
            this.btNaiveBayes.Size = new System.Drawing.Size(75, 23);
            this.btNaiveBayes.TabIndex = 1;
            this.btNaiveBayes.Text = "Naive Bayes";
            this.btNaiveBayes.Click += new System.EventHandler(this.btNaiveBayes_Click);
            // 
            // FrmPredict
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 435);
            this.Controls.Add(this.btNaiveBayes);
            this.Controls.Add(this.Grid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPredict";
            this.Text = "Make a predicttion";
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView Grid;
        private System.Windows.Forms.Button btNaiveBayes;

    }
}