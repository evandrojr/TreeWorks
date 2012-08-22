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
    partial class FrmOption : Form {
        public FrmOption() {
            InitializeComponent();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmOption_Load(object sender, EventArgs e)
        {
            tbLearningSample.Value = Def.DefaultPercOfDataUsedForLearningSample;
            lbLearningSampleValue.Text = Def.DefaultPercOfDataUsedForLearningSample.ToString();
            tbTreeLevelsMax.Text = Def.TreeLevelsMax.ToString();
            tbTreeMinNumberOfCasesPerNode.Text = Def.TreeMinNumberOfCasesPerNode.ToString();
            tbTreeMinNumberOfCasesPerNode.Text = Def.TreeMinNumberOfCasesPerNode.ToString();
            tbClfMaxNumberOfValues.Text = Def.ClfMaxNumberOfValuesForFullSearch.ToString();
            tbUser.Text = Def.DbUser;
            tbPassword.Text =  Def.DbPassword;
            tbDatabase.Text = Def.DbDatabase;
            tbServer.Text = Def.DbServer;
            tbPort.Text = Def.DbPort;
            tbSampleSizeCI.Text = Def.SampleSizeCI.ToString();
            tbSampleSizeError.Text = Def.SampleSizeError.ToString();
            tbPresentValueCoefficient.Text = Def.PresentCoefficientValue.ToString();
            sldOptimisationLevelForCategoricalSearch.Value = Def.ClfOptimisationLevelForCatSearch;
            tbOptimisationLevelForCategoricalSearch.Text = Def.ClfOptimisationLevelForCatSearch.ToString();
            if (Def.SampleUsingTheSameSeed)
                cbSampleUsingTheSameSeed.Checked=true;
        }

        //private void tbMinNumOfCasesPerContinuosSplit_Validated(object sender, EventArgs e)
        //{

        //    if (Validation.IsInteger(tbTreeMinNumberOfCasesPerNode.Text))
        //        Def.TreeMinNumberOfCasesPerNode = Convert.ToInt32(tbTreeMinNumberOfCasesPerNode.Text);
        //    else
        //    {
        //        MessageBox.Show(tbTreeMinNumberOfCasesPerNode.Text + " is not a valid value", "Warning");
        //        tbTreeMinNumberOfCasesPerNode.Text = Def.TreeMinNumberOfCasesPerNode.ToString();
        //    }
        //}

        private void tBLearningSample_Scroll(object sender, EventArgs e)
        {
            lbLearningSampleValue.Text = tbLearningSample.Value + "%";
            Def.DefaultPercOfDataUsedForLearningSample = tbLearningSample.Value;
        }

        //private void tbMinNumOfCasesPerCategoricalSplit_Validated(object sender, EventArgs e)
        //{
        //    if(Validation.IsInteger(tbTreeMinNumberOfCasesPerNode.Text))
        //        Def.TreeMinNumberOfCasesPerNode = Convert.ToInt32(tbTreeMinNumberOfCasesPerNode.Text);
        //    else
        //    {
        //        MessageBox.Show(tbTreeMinNumberOfCasesPerNode.Text + " is not a valid value", "Warning");
        //        tbTreeMinNumberOfCasesPerNode.Text = Def.TreeMinNumberOfCasesPerNode.ToString();
        //    }
        //}

        private void tbClfMaxNumberOfValues_Validated(object sender, EventArgs e)
        {
            if (Validation.IsInteger(tbClfMaxNumberOfValues.Text)) {
                   Def.ClfMaxNumberOfValuesForFullSearch = Convert.ToInt32(tbClfMaxNumberOfValues.Text);
            } else {
                MessageBox.Show(tbClfMaxNumberOfValues.Text + " is not a valid value", "Warning");
                tbClfMaxNumberOfValues.Text = Def.ClfMaxNumberOfValuesForFullSearch.ToString();
            }
        }

        private void tbTreeLevelsMax_Validated(object sender, EventArgs e)
        {
            if (Validation.IsInteger(tbTreeLevelsMax.Text))
                Def.TreeLevelsMax = Convert.ToInt32(tbTreeLevelsMax.Text);
            else {
                MessageBox.Show(tbTreeLevelsMax.Text + " is not a valid value", "Warning");
                tbTreeLevelsMax.Text = Def.TreeLevelsMax.ToString();
            }
        }

        private void tbUser_Validated(object sender, EventArgs e) {
            Def.DbUser = tbUser.Text;
        }

        private void tbPassword_Validated(object sender, EventArgs e) {
            Def.DbPassword = tbPassword.Text;
        }

        private void tbDatabase_Validated(object sender, EventArgs e) {
            Def.DbDatabase = tbDatabase.Text;
        }

        private void tbServer_Validated(object sender, EventArgs e) {
            Def.DbServer = tbServer.Text;
        }

        private void tbPort_Validated(object sender, EventArgs e) {
            Def.DbPort = tbPort.Text;
        }

        private void btConnect_Click(object sender, EventArgs e) {

            Def.Db.ConStr = "DRIVER={" + Def.DbDriver + "};UID=" + Def.DbUser + ";SERVER=" + Def.DbServer + ";Port=" + Def.DbPort + ";Database=" + Def.DbDatabase + ";Password=" + Def.DbPassword;
            if (Def.Db.Connect()) {
                MessageBox.Show("Connected to database");
            }
        }

        private void tbSampleSizeCI_Validated(object sender, EventArgs e) {
            double v;
            bool error=false;
           
            if (Double.TryParse(tbSampleSizeCI.Text,out v))
                if(v<0 || v>100){
                    error=true;
                    MessageBox.Show("The confidence interval should be between 0 and 100", "Warning");
                }
                else
                    Def.SampleSizeCI = v;
            else {
                MessageBox.Show("The confidence interval should be between 0 and 100", "Warning");
                error = true;
            }
            if(error)
                tbSampleSizeCI.Text = Def.SampleSizeCI.ToString();
        }

        private void tbSampleSizeError_Validated(object sender, EventArgs e) {
            double v;
            bool error = false;

            if (Double.TryParse(tbSampleSizeError.Text, out v))
                if (v < 0 || v > 100) {
                    error = true;
                    MessageBox.Show("The error should be between 0 and 100", "Warning");
                } else
                    Def.SampleSizeError = v;
            else {
                MessageBox.Show("The error should be between 0 and 100", "Warning");
                error = true;
            }
            if (error)
                tbSampleSizeError.Text = Def.SampleSizeError.ToString();
        }

        private void tbTreeMinNumberOfCasesPerNode_Validated(object sender, EventArgs e) {

            int v;
            bool error=false;

            if (Validation.IsInteger(tbTreeMinNumberOfCasesPerNode.Text)) {
                v = Convert.ToInt32(tbTreeMinNumberOfCasesPerNode.Text);
                if (v >= 1)
                    Def.TreeMinNumberOfCasesPerNode = v;
                else {
                    MessageBox.Show(tbTreeMinNumberOfCasesPerNode.Text + " is not a valid value", "Warning");
                    error = true;
                }
            } else {
                MessageBox.Show(tbTreeMinNumberOfCasesPerNode.Text + " is not a valid value", "Warning");
                error = true;
            }
            if(error)
                tbTreeMinNumberOfCasesPerNode.Text = Def.TreeMinNumberOfCasesPerNode.ToString();
        }

        private void FrmOption_FormClosing(object sender, FormClosingEventArgs e) {
            Def.Db.ConStr = "DRIVER={" + Def.DbDriver + "};UID=" + Def.DbUser + ";SERVER=" + Def.DbServer + ";Port=" + Def.DbPort + ";Database=" + Def.DbDatabase + ";Password=" + Def.DbPassword;
            Def.Db.Connect();
        }

        private void tbPresentValueCoefficient_Validated(object sender, EventArgs e) {
            Def.PresentCoefficientValue = Convert.ToDouble(tbPresentValueCoefficient.Text);
        }

        private void cbSampleUsingTheSameSeed_CheckedChanged(object sender, EventArgs e) {
            Def.SampleUsingTheSameSeed = cbSampleUsingTheSameSeed.Checked;
        }

        private void sldOptimisationLevelForCategoricalSearch_Scroll(object sender, EventArgs e) {
            tbOptimisationLevelForCategoricalSearch.Text = sldOptimisationLevelForCategoricalSearch.Value.ToString();
            Def.ClfOptimisationLevelForCatSearch = sldOptimisationLevelForCategoricalSearch.Value;
        }

    }
}