using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Spartacus {
    public partial class FrmReferenceTableData : Form {

        string table = null;
        int page = 1, rowCount=0;
        int pageMax;

 
        SortedList<string, bool> sortDesc = new SortedList<string, bool>();
        string sortVar = Def.DbTableIdName;
        string orderBy = Def.DbTableIdName;

        public FrmReferenceTableData(string _table) {
            InitializeComponent();
            table = _table;
            rowCount = (int) Def.Db.GetNumber("SELECT COUNT(*) FROM " + table);
            pageMax = (int)(rowCount / Def.NodeDataViewRowsMax) + 1;
         }

        private void btFirst_Click(object sender, EventArgs e) {
            page = 1;
            update();
        }

        private void btPrevious_Click(object sender, EventArgs e) {
            --page;
            update();
        }

        private void btNext_Click(object sender, EventArgs e) {
            ++page;
            update();
        }

        private void btLast_Click(object sender, EventArgs e) {
            page = pageMax;
            update();
        }

        private void update() {

            Grid.Rows.Clear();
            if ((page * rowCount) > Def.NodeDataViewRowsMax)
                Grid.Rows.Add(Def.NodeDataViewRowsMax);
            else
                Grid.Rows.Add(rowCount % Def.NodeDataViewRowsMax);
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
            Def.Db.ReferenceTableGridFill(table, Grid, page, orderBy);
            lbPage.Text = "Page " + page + " of " + pageMax;
        }


        private void FrmTableData_Load(object sender, EventArgs e) {

            Grid.ColumnCount = Def.Schema.VariableLst.Count;
            for (int x = 0; x < Def.Schema.VariableLst.Count; ++x) {
                sortDesc.Add(Def.Schema.VariableLst[x].Name, false);
                Grid.Columns[x].HeaderText = Def.Schema.VariableLst[x].Name;
                Grid.Columns[x].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            update();
            lbRecordCount.Text = rowCount + " records in this table";

        }
    }
}