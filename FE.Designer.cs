namespace Spartacus {
    partial class FE {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FE));
            this.btClose = new System.Windows.Forms.Button();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.btDetails = new System.Windows.Forms.Button();
            this.tbDetails = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(266, 128);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 0;
            this.btClose.Text = "Close";
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(8, 10);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.ReadOnly = true;
            this.tbMessage.Size = new System.Drawing.Size(331, 110);
            this.tbMessage.TabIndex = 1;
            // 
            // btDetails
            // 
            this.btDetails.Location = new System.Drawing.Point(188, 128);
            this.btDetails.Name = "btDetails";
            this.btDetails.Size = new System.Drawing.Size(72, 23);
            this.btDetails.TabIndex = 2;
            this.btDetails.Text = "Tech info";
            this.btDetails.Click += new System.EventHandler(this.btDetails_Click);
            // 
            // tbDetails
            // 
            this.tbDetails.Location = new System.Drawing.Point(8, 167);
            this.tbDetails.Multiline = true;
            this.tbDetails.Name = "tbDetails";
            this.tbDetails.ReadOnly = true;
            this.tbDetails.Size = new System.Drawing.Size(331, 169);
            this.tbDetails.TabIndex = 3;
            // 
            // FE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 157);
            this.ControlBox = false;
            this.Controls.Add(this.tbDetails);
            this.Controls.Add(this.btDetails);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmException";
            this.Load += new System.EventHandler(this.FE_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btDetails;
        public System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.TextBox tbDetails;
    }
}