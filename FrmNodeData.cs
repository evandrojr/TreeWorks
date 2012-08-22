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
    partial class FrmNodeData : Form {

        Node n = null;
        int page = 1;
        int pageMax;

        SortedList<string, bool> sortDesc = new SortedList<string, bool>();
        string sortVar = Def.DbTableIdName;
        string orderBy = Def.DbTableIdName;
        
        public FrmNodeData(Node _n) {
            InitializeComponent();
            n = _n;
        }

        private void FrmNodeData_Load(object sender, EventArgs e) {
            if (n.Table.RowCount == 0) {
                return;
            }

            pageMax = (int)(n.Table.RowCount / Def.NodeDataViewRowsMax) + 1;
            Text = "Data of node " + n.Id;
            Grid.ColumnCount = n.Table.VariableCount;
            for (int x = 0; x < n.Table.VariableCount; ++x) {
                sortDesc.Add(Def.Schema.VariableLst[x].Name, false);
                Grid.Columns[x].HeaderText = Def.Schema.VariableLst[x].Name;
                Grid.Columns[x].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            update();
            lbRecordCount.Text = n.Table.RowCount + " records in this node";
        }

        private void btPrevious_Click(object sender, EventArgs e) {
            --page;
            update();
        }

        private void btNext_Click(object sender, EventArgs e) {
            ++page;
            update();
        }

        private void update() {

            Grid.Rows.Clear();
            if ((page * n.Table.RowCount) > Def.NodeDataViewRowsMax)
                Grid.Rows.Add(Def.NodeDataViewRowsMax);
            else
                Grid.Rows.Add(n.Table.RowCount % Def.NodeDataViewRowsMax);

            if (page > 1) {
                btPrevious.Enabled = true;
                btFirst.Enabled = true;
            } else {
                btPrevious.Enabled = false;
                btFirst.Enabled = false;
            }
            if (page < pageMax) {
                btNext.Enabled = true;
                btLast.Enabled = true;
            } else {
                btNext.Enabled = false;
                btLast.Enabled = false;
            }
            Def.Db.NodeGridFill(n, Grid, page, orderBy);
            lbPage.Text = "Page " + page + " of " + pageMax;             
        }

        private void Grid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            sortVar = Grid.Columns[e.ColumnIndex].HeaderText;
            if (sortDesc[sortVar]) {
                orderBy = sortVar + " DESC ";
                sortDesc[sortVar] = false;
            } else {
                orderBy = sortVar;
                sortDesc[sortVar] = true;
            }
            update();
        }

        private void btFirst_Click(object sender, EventArgs e) {
            page=1;
            update();
        }

        private void btLast_Click(object sender, EventArgs e) {
            page = pageMax;
            update();
        }

        private void Grid_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }

    }
}