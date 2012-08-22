#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

#endregion

namespace Spartacus
{
    partial class FrmWizardDataset : Form
    {
        public FrmWizardDataset()
        {
            InitializeComponent();
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            Def.DbTableInUse = lbTables.SelectedItem.ToString();
            if (rbMultivariate.Checked)
                Def.Multivariate = true;
            else
                Def.Multivariate = false;
            FrmWizardDefineVariables fNext = new FrmWizardDefineVariables();
            fNext.Show();
            Close();
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            FrmWizardNewProject fBack = new FrmWizardNewProject();
            fBack.Show();
            Close();
        }

        private void FrmWizardDataset_Load(object sender, EventArgs e) {
            List<string> tablesLst = Def.Db.BaseTableList();
            this.lbTables.Items.Clear();
            foreach (string table in tablesLst) {
                this.lbTables.Items.Add(table.Substring(0,table.IndexOf(Def.DbBaseTableSufix)));
            }
            tablesLst.Clear();
        }

        private void lbTables_SelectedValueChanged(object sender, EventArgs e) {
            if (lbTables.SelectedIndex != -1) {
                btNext.Enabled = true;
            }
        }

        private void lbTables_Validated(object sender, EventArgs e) {

        }

        private void groupBox1_Enter(object sender, EventArgs e) {

        }

 


            /*
            string oldDataSourceFileName = Def.DataSourceFileName;
            bool btNextEnabled = true;
            FrmWizardChooseExcelSheet fwces = new FrmWizardChooseExcelSheet();

            switch (Def.DatabaseDriver)
            {
                case Db.DriverEnum.Excel:
                    fwces.ShowDialog();
                    if(Def.DataSourceExcelSheet=="")
                        btNextEnabled = false;
                    break;
                case Db.DriverEnum.DBase:
                    // Must not have spaces and should be in the 8.3 format
                    if (Def.DataSourceFileName.IndexOf(' ') != -1 || Def.DataSourceFileBaseName.Length > 8)
                    {
                        Def.DataSourceFileBaseName = Def.DataSourceFileBaseName.Replace(" ", "").Substring(0, 8);
                        Def.DataSourceFileName = Def.DataSourceFileBaseName + ".dbf";
                        if (MessageBox.Show("The file name for dBase files can not contain blank spaces and must be in the 8.3 format should it be copied to " + Def.DataSourceFileName + "?", "Error openning the dBase table", MessageBoxButtons.OKCancel, MessageBoxIcon.Imprmation) == DialogResult.Cancel)
                        {
                            return;
                        }
                        Def.DataSourceFileNameWithPath = Def.DataSourceFilePath + "\\" + Def.DataSourceFileName;
                        try
                        {
                            System.IO.File.Copy(Def.DataSourceFilePath + "\\" + oldDataSourceFileName, Def.DataSourceFileNameWithPath, true);
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error:");
                        }
                        Def.DataSourceFileName = Fcn.FileName(Def.DataSourceFileNameWithPath);
                        Def.DataSourceFileBaseName = Fcn.FileBaseName(Def.DataSourceFileName);
                      }
                    break;

            }
            */
         
            
            
        


    }
}