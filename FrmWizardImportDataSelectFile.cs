using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Spartacus {
    public partial class FrmWizardImportDataSelectFile : Form {
        
        /// <summary>
        /// 
        /// </summary>
        public FrmWizardImportDataSelectFile() {
            InitializeComponent();
        }

        private void FrmWizardImportDataSelectFile_Load(object sender, EventArgs e) {
            Def.DataImportDefs = new DataImport();
        }

        private void btOpenFile_Click(object sender, EventArgs e) {
            this.openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e) {

            Def.DataImportDefs.Filename = openFileDialog.FileName;
            Def.DataImportDefs.TableName = Fcn.FileBaseName(Fcn.FileName(Def.DataImportDefs.FilenameNormalised));
            Regex r = new Regex("[0-9]");
            if (r.IsMatch(Def.DataImportDefs.TableName.Substring(0, 1)))
                Def.DataImportDefs.TableName = "T" + Def.DataImportDefs.TableName;
         


            try {
                Def.DataImportDefs.Autodetect();
            } catch (Exception ex) {
                MessageBox.Show("Check if the format is correct or if the file is not being used by another program " + ex.Message, "Error opening file");
//                FrmWizardImportDataSelectFile fwidsf = new FrmWizardImportDataSelectFile();
//                fwidsf.Show();
//                Close();
                return;
            }


            btNext.Enabled = true;
        }

        private void btNext_Click(object sender, EventArgs e) {
            FrmWizardImportDataDefineSchema fwidds = new FrmWizardImportDataDefineSchema();
            fwidds.Show();
            Close();
        }


    }
}