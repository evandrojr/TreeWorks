#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#endregion

namespace Spartacus
{
    partial class FrmWizardNewProject : Form
    {

        public FrmWizardNewProject()
        {
            InitializeComponent();
        }

        private void btNext_Click(object sender, EventArgs e)
        {

            if (rbNewProject.Checked == true) {
                FrmWizardDataset fwdt = new FrmWizardDataset();
                fwdt.Show();
            } else {
                MessageBox.Show("Open project not implemented...");
                return;
            }
            Close();
            Def.ToolBar.Items["btNew"].Enabled = false;
        }

        private void FrmWizardNewProject_FormClosing(object sender, FormClosingEventArgs e) {
            Def.ToolBar.Items["btNew"].Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {

        }
 
    }
}