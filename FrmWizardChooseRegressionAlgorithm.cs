using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Spartacus {
    public partial class FrmWizardChooseRegressionAlgorithm : Form {
        public FrmWizardChooseRegressionAlgorithm() {
            InitializeComponent();
        }

        private void btNext_Click(object sender, EventArgs e) {
            if (rbNetReductionInVariance.Checked == true) {
                Def.Tree.Algorithm = Tree.AlgorithmEnum.NetRiV;
                Def.FrmMain.Text = Def.APPLICATION_NAME + " using Net Reduction in Variance" + " on the " + Def.DbTableInUse + " data set"; ;
            } else
            if (rbGrossReductionInVariance.Checked == true) {
                Def.Tree.Algorithm = Tree.AlgorithmEnum.GrossRiV;
                Def.FrmMain.Text = Def.APPLICATION_NAME + " using Gross Reduction in Variance" + " on the " + Def.DbTableInUse + " data set"; ;
            }
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

        private void btBack_Click(object sender, EventArgs e) {
            FrmWizardDefineVariables fBack = new FrmWizardDefineVariables();
            fBack.Show();
            Close();
        }
    }
}