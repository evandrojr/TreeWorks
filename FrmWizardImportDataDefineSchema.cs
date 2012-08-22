using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;

namespace Spartacus {

    public delegate void DelegateAddString(String s);
    public delegate void DelegateThreadFinished();

    public partial class FrmWizardImportDataDefineSchema : Form {

        public FrmWizardImportDataDefineSchema() {
            InitializeComponent();
        }

        private void FrmWizardImportDataDefineSchema_Load(object sender, EventArgs e) {
            mtbTableName.Text = Def.DataImportDefs.TableName;
            grid.RowCount = Def.DataImportDefs.VariableLst.Count;
            for (int y = 0; y < Def.DataImportDefs.VariableLst.Count; ++y) {
                grid.Rows[y].Cells[0].Value = true;
                grid.Rows[y].Cells[1].Value = Def.DataImportDefs.VariableLst[y].Name;
                grid.Rows[y].Cells[2].Value = Def.DataImportDefs.VariableLst[y].DataType;
                grid.Rows[y].Cells[3].Value = Def.DataImportDefs.VariableLst[y].Blanks;
                grid.Rows[y].Cells[4].Value = Def.DataImportDefs.VariableLst[y].Mean;
                grid.Rows[y].Cells[5].Value = Def.DataImportDefs.VariableLst[y].Minimum;
                grid.Rows[y].Cells[6].Value = Def.DataImportDefs.VariableLst[y].Maximum;
                if (Def.DataImportDefs.RegistryCount == Def.DataImportDefs.VariableLst[y].Blanks) {
                    grid.Rows[y].Cells[0].Value = false;
                    grid.Rows[y].Cells[3].Value = "(All)";
                    grid.Rows[y].Cells[4].Value = "NaN";
                }
            }
            lblRegistryCount.Text = Def.DataImportDefs.RegistryCount + " records";
            grid.AutoResizeColumns();
        }

        private void btNext_Click(object sender, EventArgs e) {
            
            bool foundOneVariableToImport = false;
            if (cbRemoveRowsWithBlanks.Checked)
                Def.ImportedDataRemoveRowsWithBlanks = true;
            else
                Def.ImportedDataRemoveRowsWithBlanks = false;


            for (int y = 0; y < Def.DataImportDefs.VariableLst.Count; ++y) {
                if (!Fcn.Test(grid.Rows[y].Cells[0].Value)) {
                    Def.DataImportDefs.VariableLst[y].Import = false;
                } else
                    foundOneVariableToImport = true;
                Def.DataImportDefs.VariableLst[y].Name = grid.Rows[y].Cells[1].Value.ToString();
            }
            if (!foundOneVariableToImport) {
                MessageBox.Show("You must import at least one variable", "Warning:");
                return;
            }
            int lastIndexForMv = 0;
            Def.DataImportDefs.VariableMvLst = new List<DataImportVariable>();
            for (int i = 0; i < Def.DataImportDefs.VariableLst.Count; ++i) {
                if (Def.DataImportDefs.VariableLst[i].Import == false)
                    continue;
                if (Def.DataImportDefs.VariableLst[i].DataType == Database.DataTypeEnum.Number) {
                    Def.DataImportDefs.VariableMvLst.Add(Def.DataImportDefs.VariableLst[i]);
                    ++lastIndexForMv;
                } else {
                    if (Def.DataImportDefs.DependentVariableIdx == i) {
                        Def.DataImportDefs.VariableMvLst.Add(Def.DataImportDefs.VariableLst[i]);
                        ++lastIndexForMv;
                        continue;
                    }
                    for (int vci = 0; vci < Def.DataImportDefs.VariableLst[i].ValueGroupLst.Count; ++vci) {
                        Def.DataImportDefs.VariableMvLst.Add(new DataImportVariable());
                        Def.DataImportDefs.VariableMvLst[lastIndexForMv].DataType = Database.DataTypeEnum.Number;
                        Def.DataImportDefs.VariableMvLst[lastIndexForMv].Name = Def.DataImportDefs.VariableLst[i].Name + "_" + vci;
                        ++lastIndexForMv;
                    }
                }
            }
            FrmWizardImportDataLoadDatabase fwidld = new FrmWizardImportDataLoadDatabase();
            fwidld.Show();
            Close();
        }

        private void btBack_Click(object sender, EventArgs e) {
            FrmWizardImportDataSelectFile fwidsl = new FrmWizardImportDataSelectFile();
            fwidsl.Show();
            Close();
        }

        private void label2_Click(object sender, EventArgs e) {

        }

        private void cbDependentVariable_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void mtbTableName_Validated(object sender, EventArgs e)
        {
            Def.DataImportDefs.TableName = mtbTableName.Text.Replace(" ","");        
        }
    }
}