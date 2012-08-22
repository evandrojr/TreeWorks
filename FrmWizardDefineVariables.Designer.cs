namespace Spartacus
{
    partial class FrmWizardDefineVariables
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWizardDefineVariables));
            this.btNext = new System.Windows.Forms.Button();
            this.pbBanner = new System.Windows.Forms.PictureBox();
            this.btBack = new System.Windows.Forms.Button();
            this.gridVariables = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLearningSample = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.lbLearningSampleValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVariables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLearningSample)).BeginInit();
            this.SuspendLayout();
            // 
            // btNext
            // 
            this.btNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btNext.Enabled = false;
            this.btNext.Location = new System.Drawing.Point(352, 372);
            this.btNext.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(75, 23);
            this.btNext.TabIndex = 12;
            this.btNext.Text = "Next >>";
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // pbBanner
            // 
            this.pbBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbBanner.Image = Spartacus.Properties.Resources.tree_landscape;
            this.pbBanner.Location = new System.Drawing.Point(0, 0);
            this.pbBanner.Name = "pbBanner";
            this.pbBanner.Size = new System.Drawing.Size(437, 75);
            this.pbBanner.TabIndex = 10;
            this.pbBanner.TabStop = false;
            // 
            // btBack
            // 
            this.btBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btBack.Enabled = false;
            this.btBack.Location = new System.Drawing.Point(270, 372);
            this.btBack.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.btBack.Name = "btBack";
            this.btBack.Size = new System.Drawing.Size(75, 23);
            this.btBack.TabIndex = 11;
            this.btBack.Text = "<< Back";
            this.btBack.Click += new System.EventHandler(this.btBack_Click);
            // 
            // gridVariables
            // 
            this.gridVariables.AllowUserToAddRows = false;
            this.gridVariables.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gridVariables.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridVariables.Columns.Add(this.dataGridViewTextBoxColumn1);
            this.gridVariables.Columns.Add(this.dataGridViewTextBoxColumn3);
            this.gridVariables.Columns.Add(this.dataGridViewCheckBoxColumn1);
            this.gridVariables.Columns.Add(this.dataGridViewCheckBoxColumn4);
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridVariables.DefaultCellStyle = dataGridViewCellStyle4;
            this.gridVariables.Enabled = false;
            this.gridVariables.GridColor = System.Drawing.Color.White;
            this.gridVariables.Location = new System.Drawing.Point(0, 130);
            this.gridVariables.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.gridVariables.MultiSelect = false;
            this.gridVariables.Name = "gridVariables";
            this.gridVariables.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridVariables.RowHeadersWidth = 4;
            this.gridVariables.RowTemplate.MinimumHeight = 22;
            this.gridVariables.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridVariables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridVariables.ShowEditingIcon = false;
            this.gridVariables.Size = new System.Drawing.Size(437, 214);
            this.gridVariables.TabIndex = 13;
            this.gridVariables.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridVariables_CellValueChanged);
            this.gridVariables.Click += new System.EventHandler(this.gridVariables_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Variable";
            this.dataGridViewTextBoxColumn1.Name = "Variable";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 250;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Type";
            this.dataGridViewTextBoxColumn3.Name = "Type";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 70;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            dataGridViewCellStyle2.FormatProvider = new System.Globalization.CultureInfo("en-GB");
            this.dataGridViewCheckBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewCheckBoxColumn1.FalseValue = "false";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Target";
            this.dataGridViewCheckBoxColumn1.IndeterminateValue = "false";
            this.dataGridViewCheckBoxColumn1.Name = "Target";
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCheckBoxColumn1.TrueValue = "true";
            this.dataGridViewCheckBoxColumn1.Width = 40;
            // 
            // dataGridViewCheckBoxColumn4
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.FormatProvider = new System.Globalization.CultureInfo("en-GB");
            this.dataGridViewCheckBoxColumn4.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewCheckBoxColumn4.FalseValue = "false";
            this.dataGridViewCheckBoxColumn4.HeaderText = "Predictor";
            this.dataGridViewCheckBoxColumn4.IndeterminateValue = "true";
            this.dataGridViewCheckBoxColumn4.Name = "Predictor";
            this.dataGridViewCheckBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCheckBoxColumn4.TrueValue = "true";
            this.dataGridViewCheckBoxColumn4.Width = 50;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(110, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Set up variable\'s properties";
            // 
            // tbLearningSample
            // 
            this.tbLearningSample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbLearningSample.Location = new System.Drawing.Point(8, 377);
            this.tbLearningSample.Maximum = 100;
            this.tbLearningSample.Minimum = 5;
            this.tbLearningSample.Name = "tbLearningSample";
            this.tbLearningSample.Size = new System.Drawing.Size(214, 45);
            this.tbLearningSample.SmallChange = 5;
            this.tbLearningSample.TabIndex = 15;
            this.tbLearningSample.Tag = "";
            this.tbLearningSample.TickFrequency = 5;
            this.tbLearningSample.Value = 80;
            this.tbLearningSample.Scroll += new System.EventHandler(this.tBLearningSample_Scroll);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 347);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Size of the learning sample (5-100%)";
            // 
            // lbLearningSampleValue
            // 
            this.lbLearningSampleValue.AutoSize = true;
            this.lbLearningSampleValue.Location = new System.Drawing.Point(96, 365);
            this.lbLearningSampleValue.Name = "lbLearningSampleValue";
            this.lbLearningSampleValue.Size = new System.Drawing.Size(23, 13);
            this.lbLearningSampleValue.TabIndex = 17;
            this.lbLearningSampleValue.Text = "80%";
            // 
            // FrmWizardDefineVariables
            // 
            this.ClientSize = new System.Drawing.Size(437, 414);
            this.ControlBox = false;
            this.Controls.Add(this.lbLearningSampleValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbLearningSample);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridVariables);
            this.Controls.Add(this.btNext);
            this.Controls.Add(this.pbBanner);
            this.Controls.Add(this.btBack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmWizardDefineVariables";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wizard Define Variables";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmWizardDefineVariables_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVariables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLearningSample)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.PictureBox pbBanner;
        private System.Windows.Forms.Button btBack;
        private System.Windows.Forms.DataGridView gridVariables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbLearningSample;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbLearningSampleValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn4;
    }
}