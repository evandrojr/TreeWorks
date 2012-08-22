namespace Spartacus {
    partial class FrmDataTransport {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDataTransport));
            this.label3 = new System.Windows.Forms.Label();
            this.btTransfer = new System.Windows.Forms.Button();
            this.btPreview = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cBDestinationNode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cBSourceNode = new System.Windows.Forms.ComboBox();
            this.cbVariable = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbOperator = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbWhere = new System.Windows.Forms.TextBox();
            this.clbValue = new System.Windows.Forms.CheckedListBox();
            this.groupBoxWith = new System.Windows.Forms.GroupBox();
            this.rbNone = new System.Windows.Forms.RadioButton();
            this.rbOr = new System.Windows.Forms.RadioButton();
            this.rbAnd = new System.Windows.Forms.RadioButton();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btInsert = new System.Windows.Forms.Button();
            this.groupBoxWith.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label3.Location = new System.Drawing.Point(26, 285);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 40);
            this.label3.TabIndex = 13;
            this.label3.Text = "Hint: The \'is null\' keyword matches blank values.";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // btTransfer
            // 
            this.btTransfer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btTransfer.Location = new System.Drawing.Point(527, 303);
            this.btTransfer.Name = "btTransfer";
            this.btTransfer.Size = new System.Drawing.Size(106, 39);
            this.btTransfer.TabIndex = 12;
            this.btTransfer.Text = "Move data";
            this.btTransfer.Click += new System.EventHandler(this.btTransfer_Click);
            // 
            // btPreview
            // 
            this.btPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPreview.Location = new System.Drawing.Point(409, 303);
            this.btPreview.Name = "btPreview";
            this.btPreview.Size = new System.Drawing.Size(111, 39);
            this.btPreview.TabIndex = 11;
            this.btPreview.Text = "Preview data transfer";
            this.btPreview.Click += new System.EventHandler(this.btPreview_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(311, 301);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Destination node";
            // 
            // cBDestinationNode
            // 
            this.cBDestinationNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cBDestinationNode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBDestinationNode.FormattingEnabled = true;
            this.cBDestinationNode.Location = new System.Drawing.Point(311, 319);
            this.cBDestinationNode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.cBDestinationNode.Name = "cBDestinationNode";
            this.cBDestinationNode.Size = new System.Drawing.Size(82, 21);
            this.cBDestinationNode.TabIndex = 9;
            this.cBDestinationNode.SelectedIndexChanged += new System.EventHandler(this.cBDestinationNode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(220, 301);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Source node";
            // 
            // cBSourceNode
            // 
            this.cBSourceNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cBSourceNode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBSourceNode.FormattingEnabled = true;
            this.cBSourceNode.Location = new System.Drawing.Point(213, 319);
            this.cBSourceNode.Name = "cBSourceNode";
            this.cBSourceNode.Size = new System.Drawing.Size(82, 21);
            this.cBSourceNode.TabIndex = 7;
            this.cBSourceNode.SelectedIndexChanged += new System.EventHandler(this.cBSourceNode_SelectedIndexChanged);
            // 
            // cbVariable
            // 
            this.cbVariable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVariable.FormattingEnabled = true;
            this.cbVariable.Location = new System.Drawing.Point(21, 44);
            this.cbVariable.Name = "cbVariable";
            this.cbVariable.Size = new System.Drawing.Size(121, 21);
            this.cbVariable.TabIndex = 14;
            this.cbVariable.SelectedIndexChanged += new System.EventHandler(this.cbVariable_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Variable";
            // 
            // cbOperator
            // 
            this.cbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperator.FormattingEnabled = true;
            this.cbOperator.Location = new System.Drawing.Point(164, 44);
            this.cbOperator.Name = "cbOperator";
            this.cbOperator.Size = new System.Drawing.Size(72, 21);
            this.cbOperator.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(178, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Operator";
            // 
            // tbWhere
            // 
            this.tbWhere.Location = new System.Drawing.Point(21, 133);
            this.tbWhere.Multiline = true;
            this.tbWhere.Name = "tbWhere";
            this.tbWhere.Size = new System.Drawing.Size(610, 135);
            this.tbWhere.TabIndex = 18;
            // 
            // clbValue
            // 
            this.clbValue.CheckOnClick = true;
            this.clbValue.FormattingEnabled = true;
            this.clbValue.Location = new System.Drawing.Point(268, 45);
            this.clbValue.Name = "clbValue";
            this.clbValue.Size = new System.Drawing.Size(168, 64);
            this.clbValue.TabIndex = 19;
            this.clbValue.Visible = false;
            // 
            // groupBoxWith
            // 
            this.groupBoxWith.Controls.Add(this.rbNone);
            this.groupBoxWith.Controls.Add(this.rbOr);
            this.groupBoxWith.Controls.Add(this.rbAnd);
            this.groupBoxWith.Location = new System.Drawing.Point(454, 19);
            this.groupBoxWith.Name = "groupBoxWith";
            this.groupBoxWith.Size = new System.Drawing.Size(87, 98);
            this.groupBoxWith.TabIndex = 20;
            this.groupBoxWith.TabStop = false;
            this.groupBoxWith.Text = "With";
            // 
            // rbNone
            // 
            this.rbNone.AutoSize = true;
            this.rbNone.Checked = true;
            this.rbNone.Location = new System.Drawing.Point(15, 68);
            this.rbNone.Name = "rbNone";
            this.rbNone.Size = new System.Drawing.Size(62, 17);
            this.rbNone.TabIndex = 2;
            this.rbNone.TabStop = true;
            this.rbNone.Text = "(NONE)";
            // 
            // rbOr
            // 
            this.rbOr.AutoSize = true;
            this.rbOr.Location = new System.Drawing.Point(15, 45);
            this.rbOr.Name = "rbOr";
            this.rbOr.Size = new System.Drawing.Size(41, 17);
            this.rbOr.TabIndex = 1;
            this.rbOr.Text = "OR";
            // 
            // rbAnd
            // 
            this.rbAnd.AutoSize = true;
            this.rbAnd.Location = new System.Drawing.Point(15, 22);
            this.rbAnd.Name = "rbAnd";
            this.rbAnd.Size = new System.Drawing.Size(48, 17);
            this.rbAnd.TabIndex = 0;
            this.rbAnd.Text = "AND";
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(295, 45);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(100, 20);
            this.tbValue.TabIndex = 21;
            this.tbValue.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(322, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Value(s)";
            // 
            // btInsert
            // 
            this.btInsert.Location = new System.Drawing.Point(556, 58);
            this.btInsert.Name = "btInsert";
            this.btInsert.Size = new System.Drawing.Size(75, 23);
            this.btInsert.TabIndex = 23;
            this.btInsert.Text = "Insert";
            this.btInsert.Click += new System.EventHandler(this.btInsert_Click);
            // 
            // FrmDataTransport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 352);
            this.Controls.Add(this.btInsert);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.groupBoxWith);
            this.Controls.Add(this.clbValue);
            this.Controls.Add(this.tbWhere);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbOperator);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbVariable);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btTransfer);
            this.Controls.Add(this.btPreview);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cBDestinationNode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cBSourceNode);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDataTransport";
            this.Text = "Move data";
            this.Load += new System.EventHandler(this.FrmDataTransport_Load);
            this.groupBoxWith.ResumeLayout(false);
            this.groupBoxWith.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btTransfer;
        private System.Windows.Forms.Button btPreview;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cBDestinationNode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cBSourceNode;
        private System.Windows.Forms.ComboBox cbVariable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbOperator;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbWhere;
        private System.Windows.Forms.CheckedListBox clbValue;
        private System.Windows.Forms.GroupBox groupBoxWith;
        private System.Windows.Forms.RadioButton rbAnd;
        private System.Windows.Forms.RadioButton rbNone;
        private System.Windows.Forms.RadioButton rbOr;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btInsert;

    }
}