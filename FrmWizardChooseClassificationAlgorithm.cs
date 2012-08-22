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
    partial class FrmWizardChooseClassificationAlgorithm : Form
    {
        public FrmWizardChooseClassificationAlgorithm()
        {
            InitializeComponent();
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            if (rbGini.Checked == true) {
                Def.Tree.Algorithm = Tree.AlgorithmEnum.Gini;
                Def.FrmMain.Text = Def.APPLICATION_NAME + " using Gini index " + "on the " + Def.DbTableInUse + " data set"; 
            } else
            if (rbEntropy.Checked == true) {
                Def.Tree.Algorithm = Tree.AlgorithmEnum.Entropy;
                Def.FrmMain.Text = Def.APPLICATION_NAME + " using Information Entropy" + " on the " + Def.DbTableInUse + " data set"; ;
            }
            else
            if (rbMaxDif.Checked == true) {
                Def.Tree.Algorithm = Tree.AlgorithmEnum.MaxDif;
                Def.FrmMain.Text = Def.APPLICATION_NAME + " using MaxDif" + " on the " + Def.DbTableInUse + " data set"; ; 
            }//else
            //if (rbHybrid.Checked == true) {
            //    Def.Tree.Algorithm = Tree.AlgorithmEnum.Hybrid;
            //    Def.FrmMain.Text = Def.APPLICATION_NAME + " using mixed algorithms" + " on the " + Def.DbTableInUse + " data set"; ;
            //} 
            if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical) {
                NodeTargetCategorical root = new NodeTargetCategorical();
                Def.Tree.Root = root;
            } else {
                NodeTargetContinuous root = new NodeTargetContinuous();
                Def.Tree.Root = root;
            }
            Def.Tree.GrowthState = Tree.GrowthStateEnum.Root;
            Def.TreeCanBeDisplayed = true;
            Def.FrmMain.TreeBuild();
            Def.ToolBar.Items["btNew"].Enabled = true;
            Close();
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            FrmWizardDefineVariables fBack = new FrmWizardDefineVariables();
            fBack.Show();
            Close();
        }

    }
}