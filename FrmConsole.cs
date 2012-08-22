using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Spartacus {
    public partial class FrmConsole : Form {
        public FrmConsole() {
            InitializeComponent();
            Experiment e = new Experiment(this);

        }
        public void ad(string t) {
            tbMsg.Text += t + Environment.NewLine;
        }
    }
}