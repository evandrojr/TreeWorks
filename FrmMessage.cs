#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#endregion

namespace Spartacus {
    partial class FrmMessage : Form {
        public FrmMessage() {
            InitializeComponent();
        }

        public FrmMessage(string message, string caption) : this() {
            lblMessage.Text = message;
            this.Text = caption;
            lblMessage.Top = (this.Height / 2) - (lblMessage.Height / 2);
            lblMessage.Left = (this.Width / 2) - (lblMessage.Width / 2);
         }

    }
}