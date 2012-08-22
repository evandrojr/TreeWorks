using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Spartacus {
    public partial class FrmPredict : Form {
        public FrmPredict() {
            InitializeComponent();
            Fill();

        }
        /// <summary>
        /// Fills the Grid 
        /// 
        /// </summary>
        private void Fill(){

            for (int i = 0; i < Def.Tree.Root.PredictorLst.Count; ++i) {
                if (Def.Tree.Root.PredictorLst[i].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                    DataGridViewTextBoxColumn Ctb = new DataGridViewTextBoxColumn();
                    Grid.Columns.Add(Ctb);
                } else {
                    DataGridViewComboBoxColumn Ccb = new DataGridViewComboBoxColumn();
                    Grid.Columns.Add(Ccb);
                    for (int z = 0; z < Def.Tree.Root.PredictorLst[i].ValueSd.Count; ++z)
                        Ccb.Items.Add(Def.Tree.Root.PredictorLst[i].ValueSd.Keys[z]);
                }
                Grid.Columns[i].HeaderText = Def.Tree.Root.PredictorLst[i].Variable.Name;
           }
           Grid.Rows.Add(1);
        }

        private void btNaiveBayes_Click(object sender, EventArgs e) {
            
            //Do for each class
            NodeTargetCategorical n = (NodeTargetCategorical) Def.Tree.Root;

            int max = 0;
            double sik;
            string xs, predName, classVal;
            double xn, prob=1, probMax=0;
            double[] meanAndStdev = new double[2];
            string selMeanAndStdev;

            for (int clIdx = 0; clIdx < n.TargetClasses.ClassCount; ++clIdx) {
                classVal = n.TargetClasses.ClassSd.Keys[clIdx];
                prob = 1;
                for (int i = 0; i < Def.Tree.Root.PredictorLst.Count; ++i) {
                    predName = Def.Tree.Root.PredictorLst[i].Variable.Name;
                    if (Def.Tree.Root.PredictorLst[i].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                        xn = Convert.ToDouble(Grid[i, 0].Value);
                        
                        selMeanAndStdev = @" 
                            SELECT 
                                avg(" + predName + "), stddev( " + predName + ") " +
                            " FROM " + 
                                Def.DbBsTb + 
                            " WHERE " + 
                                Def.Schema.Target.Name + " = '" + classVal + "'";
                        meanAndStdev = Def.Db.GetNN(selMeanAndStdev);
                        prob *= Math.Exp(-1 * Math.Pow(xn - meanAndStdev[0], 2) / (2 * meanAndStdev[1] * meanAndStdev[1])) / Math.Sqrt(2 * Math.PI) * meanAndStdev[1];
                    } else {
                        xs = Convert.ToString(Grid[i, 0].Value);
                        sik = Def.Db.GetNumber(@"
                            SELECT 
                                count(*) " +
                            " FROM " + 
                                Def.DbBsTb + 
                            " WHERE " + 
                                Def.Schema.Target.Name + " = '" + classVal + "' and " + predName + " = '" + xs + "'");
                         prob *= sik / n.TargetClasses.ClassSd[classVal]; 
                    }
                }
                
                prob *= (double)n.TargetClasses.ClassSd[classVal] / Def.Tree.Root.Table.RowCount;

                if (clIdx == 0) {
                    probMax = prob;
                } else {
                    if (prob > probMax)
                        max = clIdx;
                }


            }
            if(probMax == 0)
                MessageBox.Show("Sorry, could not decide");
            else
                MessageBox.Show("Classified as " + n.TargetClasses.ClassSd.Keys[max]);

        }



    }
}