using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Spartacus {
    public partial class FrmWizardImportDataLoadDatabase : Form {
        public FrmWizardImportDataLoadDatabase() {
            InitializeComponent();
        }

        private void FrmWizardImportDataLoadDatabase_Load(object sender, EventArgs e) {
            Def.ToolBar.Items["btNew"].Enabled = true;
            if (Def.DataImportDefs.Multivariate == false) {
                Def.Db.ImportedIndexesDrop();
                Def.Db.ImportedTableDrop();
                Def.Db.ImportedSequenceDrop();
                Def.Db.ImportedSequenceCreate();
                Def.Db.ImportedTableCreate();
                Def.Db.ImportedTableFill();
                Def.Db.ImportedIndexesCreate();
            }else{
                MessageBox.Show("Multivariate load is not necessary anymore");
                //Def.Db.ImportedSequenceDrop();
                //Def.Db.ImportedSequenceCreate();
                //Def.Db.ImportedTableDrop();
                //Def.Db.ImportedTableCreate();
                //Def.Db.ImportedIndexesDrop();
                //Def.Db.ImportedTableFill();
                //Def.Db.ImportedIndexesCreate();
                //Def.Db.ImportedSequenceMvDrop();
                //Def.Db.ImportedSequenceMvCreate();
                //Def.Db.ImportedTableMvDrop();
                //Def.Db.ImportedTableMvCreate();
                //Def.Db.ImportedIndexesMvDrop();
                //Def.Db.ImportedTableMvFill();
                //Def.Db.ImportedIndexesMvCreate();
            }
            Text = "Data loaded";
            lbMessage.Visible = true;
//            lbNullRowsDeleted.Text = nullRowsDeleted.ToString();
            this.btClose.Enabled = true;
        }

        private void btClose_Click(object sender, EventArgs e) {
            Close();
        }


    }
}