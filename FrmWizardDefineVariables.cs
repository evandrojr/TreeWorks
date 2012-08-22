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
    partial class FrmWizardDefineVariables : Form
    {
     
        public FrmWizardDefineVariables()
        {
            InitializeComponent();


        }

        private void btNext_Click(object sender, EventArgs e)
        {
            int nullRowsDeleted=-1;
            string sqlDeleteNulls;  
            if (saveTest() == true) {
                save();
                Def.DatasetWithNullRowCount = (int) Def.Db.GetNumber("select count(*) from " + Def.DbBsTb);
                Def.DatasetNotNullRowCount = (int)Def.Db.GetNumber("select count(*) from " + Def.DbBsTb + " WHERE " + Def.Schema.Target.Name + " IS NOT NULL");                
                Def.TrainingSetPercent = tbLearningSample.Value;
                Def.TestingSetPercent = 100 - Def.TrainingSetPercent;
                Def.TrainingSetRowCount = Def.TrainingSetPercent * Def.DatasetNotNullRowCount / 100;
                Def.TestingSetRowCount = Def.DatasetNotNullRowCount - Def.TrainingSetRowCount;
                Def.Db.RefereceTableCreate(0);
                Def.Db.ExecuteNonQuery("DROP table " + Def.DbTsTb + "0 ", true);
                Def.Db.ExecuteNonQuery("Create table " + Def.DbTsTb +  "0 (" + Def.DbTableIdName + " integer PRIMARY KEY)");
                Def.Db.Sample();
                sqlDeleteNulls =
                @"DELETE FROM " +
                    Def.DbTrTb + "0 " +
                 "WHERE " +
                    Def.DbTableIdName + " in " +
                    "(select " + Def.DbTableIdName + " " +
                        "FROM " + Def.DbBsTb + " WHERE " + Def.Schema.Target.Name + " IS NULL)";
                Def.Db.ExecuteNonQuery(sqlDeleteNulls);
                //if (Def.Multivariate) {
                //    Def.Db.RefereceTableMvCreate(0);
                //    Def.Db.ExecuteNonQuery("DROP table " + Def.DbTsMvTb + "0 ", true);
                //    Def.Db.ExecuteNonQuery("Create table " + Def.DbTsMvTb + "0 (" + Def.DbTableIdName + " integer PRIMARY KEY)");
                //    Def.Db.Sample();
                //    sqlDeleteNulls =
                //    @"DELETE FROM " +
                //        Def.DbTrMvTb + "0 " +
                //     "WHERE " +
                //        Def.DbTableIdName + " in " +
                //        "(select " + Def.DbTableIdName + " " +
                //            "FROM " + Def.DbBsMvTb + " WHERE " + Def.Schema.Target.Name + " IS NULL)";
                //    Def.Db.ExecuteNonQuery(sqlDeleteNulls);
                //}


                nullRowsDeleted = Def.DatasetWithNullRowCount - Def.DatasetNotNullRowCount;
                if (nullRowsDeleted > 0)
                    MessageBox.Show(nullRowsDeleted + " rows containing null values in the target variable have been deleted", "Warning");
                if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical) {
                    Def.Tree.ModelType = Tree.ModelTypeEnum.Classification;
                    FrmWizardChooseClassificationAlgorithm fNext = new FrmWizardChooseClassificationAlgorithm();
                    fNext.Show();
                } else {
                    Def.Tree.ModelType = Tree.ModelTypeEnum.Regression;
                    FrmWizardChooseRegressionAlgorithm fNext = new FrmWizardChooseRegressionAlgorithm();
                    fNext.Show();
                }
                Close();
            } else
                return;
        }

        private void btBack_Click(object sender, EventArgs e){
            FrmWizardDataset fBack = new FrmWizardDataset();
            fBack.Show();
            Close();
        }

        private void FrmWizardDefineVariables_Load(object sender, EventArgs e)
        {
            Def.Schema.Load();
            gridVariables.Rows.Add(Def.Schema.VariableLst.Count);
            for (int y = 0; y < Def.Schema.VariableLst.Count; ++y) {
                gridVariables.Rows[y].Cells[0].Value = Def.Schema.VariableLst[y].Name;
                gridVariables.Rows[y].Cells[1].Value = Def.Schema.VariableLst[y].VariableTypeUserSet;
                gridVariables.Rows[y].Cells[2].Value = false;
                gridVariables.Rows[y].Cells[3].Value = true;
            }
            tbLearningSample.Value = Def.DefaultPercOfDataUsedForLearningSample;
            lbLearningSampleValue.Text = tbLearningSample.Value + "%";
            this.gridVariables.Enabled = true;
            this.btBack.Enabled = true;
            this.btNext.Enabled = true;
        }




        private void gridVariables_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
//            if(timer.Enabled==false)
//                MessageBox.Show("Mudou gridVariables_CellValueChanged");
        }

        
        private bool saveTest() {

            bool found1Target = false;
            bool found1Predictor = false;

            for (int y = 0; y < Def.Schema.VariableLst.Count; ++y) {
                if (Fcn.Test(gridVariables.Rows[y].Cells[2].Value)) //= is TRUE
                    found1Target = true;
                if (Fcn.Test(gridVariables.Rows[y].Cells[3].Value)) //= is TRUE
                    found1Predictor = true;
            }
            if (!found1Target || !found1Predictor){
                MessageBox.Show("You should select one target variable and one predictor", "Warning!");
                return false;
            }
            else
                return true;
        }

        private void save() {

            Def.Schema.PredictorLst.Clear();
            for (int y = 0; y < Def.Schema.VariableLst.Count; ++y) {
                Def.Schema.VariableLst[y].VariableTypeUserSet = (SchemaVariable.VariableTypeEnum) gridVariables.Rows[y].Cells[1].Value;
/*
                if (VariableCct.VariableTypeEnum.Categorical.ToString() == gridVariables.Rows[y].Cells[1].Value.ToString())
                    Def.Schema.VariableLst[y].VariableTypeUserSet = VariableCct.VariableTypeEnum.Categorical;
                else
                    Def.Schema.VariableLst[y].VariableTypeUserSet = VariableCct.VariableTypeEnum.Continuous;
*/
                //Set the target
                if (Fcn.Test(gridVariables.Rows[y].Cells[2].Value))//= TRUE
                    Def.Schema.Target = Def.Schema.VariableLst[y];

                //Set the role and predictors  
                if (Fcn.Test(gridVariables.Rows[y].Cells[3].Value) && ! Fcn.Test(gridVariables.Rows[y].Cells[2].Value)) {//= TRUE && FALSE
                    Def.Schema.PredictorLst.Add(Def.Schema.VariableLst[y]);
                    Def.Schema.VariableLst[y].VariableRole = SchemaVariable.VariableRoleEnum.Predictor;
                }else
                if (!Fcn.Test(gridVariables.Rows[y].Cells[3].Value) && Fcn.Test(gridVariables.Rows[y].Cells[2].Value))//= FALSE && TRUE
                    Def.Schema.VariableLst[y].VariableRole = SchemaVariable.VariableRoleEnum.Target;
                else
                if (!Fcn.Test(gridVariables.Rows[y].Cells[3].Value) && !Fcn.Test(gridVariables.Rows[y].Cells[2].Value))//= FALSE && FALSE
                    Def.Schema.VariableLst[y].VariableRole = SchemaVariable.VariableRoleEnum.NotUsed;
            }
        }


        private void gridVariables_Click(object sender, EventArgs e)
        {
            DataGridView grid = (DataGridView)sender;
            
            //Disabled because support for continuos variables as categorical is not implemented yet

            //if (grid.CurrentCell.ColumnIndex == 1) {
            //    if (Def.Schema.VariableLst[grid.CurrentCell.RowIndex].VariableTypeDetected == SchemaVariable.VariableTypeEnum.Continuous)
            //        if (grid.CurrentCell.Value.ToString() == SchemaVariable.VariableTypeEnum.Continuous.ToString()) {
            //            grid.CurrentCell.Value = SchemaVariable.VariableTypeEnum.Categorical;
            //            Def.Schema.VariableLst[grid.CurrentCell.RowIndex].VariableTypeUserSet = SchemaVariable.VariableTypeEnum.Categorical;
            //        } else {
            //            grid.CurrentCell.Value = SchemaVariable.VariableTypeEnum.Continuous;
            //            Def.Schema.VariableLst[grid.CurrentCell.RowIndex].VariableTypeUserSet = SchemaVariable.VariableTypeEnum.Continuous;
            //        }
            //}
            //else
            if(grid.CurrentCell.ColumnIndex == 2) {
                for (int y = 0; y < Def.Schema.VariableLst.Count; ++y) {
                    gridVariables.Rows[y].Cells[2].Value = 0;
                }
                gridVariables.Rows[grid.CurrentCell.RowIndex].Cells[2].Value = 1;
                gridVariables.Rows[grid.CurrentCell.RowIndex].Cells[3].Value = 0;
            } else
            if (grid.CurrentCell.ColumnIndex == 3) {
                gridVariables.Rows[grid.CurrentCell.RowIndex].Cells[2].Value = 0;
                if ( !Fcn.Test(gridVariables.Rows[grid.CurrentCell.RowIndex].Cells[3].Value) )//= FALSE
                    gridVariables.Rows[grid.CurrentCell.RowIndex].Cells[3].Value = 1;
                else
                    gridVariables.Rows[grid.CurrentCell.RowIndex].Cells[3].Value = 0;
            }
        }

        private void tBLearningSample_Scroll(object sender, EventArgs e)
        {
            lbLearningSampleValue.Text = tbLearningSample.Value + "%";
        }

    }
} 