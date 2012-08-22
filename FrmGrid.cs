using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Spartacus {
    public partial class FrmGrid : Form {
        public DataGridView Grid = new DataGridView();
        public FrmGrid() {
            InitializeComponent();
            Controls.Add(Grid);
            Grid.Dock = DockStyle.Fill;
        }
    }
}