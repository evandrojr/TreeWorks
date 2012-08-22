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
    partial class FrmNodeDesign : Form {
        public FrmNodeDesign() {
            InitializeComponent();
        }

        private void FrmNodeDesign_Load(object sender, EventArgs e)
        {
            int x,y,w,h;


            x = y = 0;
            //NodeContinuous.Columns
//            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 7.5f);
//            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 7.5f);
//            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 7.5f);
            
            /*
            nodeContinuous.Rows.Add(5);
            nodeContinuous.Rows[y].Cells[x].Value = "Node 0"; ++y;
            nodeContinuous.Rows[y].Cells[x].Value = "Mean"; ++y;
            nodeContinuous.Rows[y].Cells[x].Value = "Std Dev"; ++y;
            nodeContinuous.Rows[y].Cells[x].Value = "n"; ++y;
            nodeContinuous.Rows[y].Cells[x].Value = "%"; ++y;
            ++x; y = 1;
            nodeContinuous.Rows[y].Cells[x].Value = "235"; ++y;
            nodeContinuous.Rows[y].Cells[x].Value = "8.5"; ++y;
            nodeContinuous.Rows[y].Cells[x].Value = "465546554"; ++y;
            nodeContinuous.Rows[y].Cells[x].Value = "100.00"; ++y;
            ++x; y = 1;

            nodeContinuous.AutoSize = true;
            nodeContinuous.Height = 1;
            nodeContinuous.Width = 1;
            h = nodeContinuous.Height - 15;
            w = nodeContinuous.Width - 15;
            nodeContinuous.AutoSize = false;
            nodeContinuous.Height = h;
            nodeContinuous.Width = w;

            */

            nodeCategorical.Rows.Add(5);
            x = y = 0;
            //NodeContinuous.Columns
//            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 7.5f);
//            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 7.5f);
//            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 7.5f);
            nodeCategorical.Rows[y].Cells[x].Value = "Node 0"; ++y;
            nodeCategorical.Rows[y].Cells[x].Value = "Class"; ++y;
            nodeCategorical.Rows[y].Cells[x].Value = "Don't Play"; ++y;
            nodeCategorical.Rows[y].Cells[x].Value = "Play"; ++y;
            nodeCategorical.Rows[y].Cells[x].Value = "Total"; ++y;

            ++x; y=1;
            nodeCategorical.Rows[y].Cells[x].Value = "%r"; ++y;
            nodeCategorical.Rows[y].Cells[x].Value = "30"; ++y;
            nodeCategorical.Rows[y].Cells[x].Value = "70"; ++y;
            nodeCategorical.Rows[y].Cells[x].Value = "100"; ++y;

            ++x; y=1;
            nodeCategorical.Rows[y].Cells[x].Value = "%a"; ++y;
            nodeCategorical.Rows[y].Cells[x].Value = "50"; ++y;
            nodeCategorical.Rows[y].Cells[x].Value = "20"; ++y;
            nodeCategorical.Rows[y].Cells[x].Value = "70"; ++y;

            ++x; y=1;
            nodeCategorical.Rows[y].Cells[x].Value = "n"; ++y;
            nodeCategorical.Rows[y].Cells[x].Value = "5000000"; ++y;
            nodeCategorical.Rows[y].Cells[x].Value = "2000000"; ++y;
            nodeCategorical.Rows[y].Cells[x].Value = "7000000"; ++y;

            //DataGridViewAutoSizeColumnCriteria cc = new DataGridViewAutoSizeColumnCriteria();
           // cc

           // nodeCategorical.AutoRelocate = true;

//            nodeCategorical.ce

            
//            nodeCategorical.AutoSizeColumn(DataGridViewAutoSizeColumnCriteria.HeaderAndRows, 0);
//            nodeCategorical.AutoSizeColumn(DataGridViewAutoSizeColumnCriteria.HeaderAndRows, 1);    
//            nodeCategorical.AutoSizeColumn(DataGridViewAutoSizeColumnCriteria.HeaderAndRows, 2);
//            nodeCategorical.AutoSizeColumn(DataGridViewAutoSizeColumnCriteria.HeaderAndRows, 3);
            

            nodeCategorical.Height = 1;
            nodeCategorical.Width = 1;
            h = nodeCategorical.Height - 15;
            w = nodeCategorical.Width - 15;
            nodeCategorical.AutoSize = false;
            nodeCategorical.Height = h;
            nodeCategorical.Width = w;


        }

        private void panel1_Paint(object sender, PaintEventArgs e) {
        
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("click");
        }

        private void nodeContinuous_Click(object sender, EventArgs e)
        {
            MessageBox.Show("click");
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            
//            MessageBox.Show(nodeCategorical.Height + "X" + nodeCategorical.Width);

//            nodeCategorical.AutoSize = false;
//            nodeCategorical.si
//            nodeCategorical.Height -= 10;
//            nodeCategorical.Width -= 10;
        }

        private void viewdataToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
        }

        private void manuallysplitToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
        }

        private void autosplitToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
        }

        private void fullautogrowToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
        }
    }
}