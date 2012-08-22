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
    partial class FrmPredictor : Form {

        Node node;

        public FrmPredictor(Node _node) {
            
            node = _node;
            InitializeComponent();
            Grid.Rows.Add(node.PredictorLst.Count);
            for (int x = 0; x < node.PredictorLst.Count; ++x) {
                Grid.Rows[x].Cells[0].Value = node.PredictorLst[x].Variable.Name;
                Grid.Rows[x].Cells[1].Value = node.PredictorLst[x].Variable.VariableTypeUserSet.ToString();
                Grid.Rows[x].Cells[2].Value = "Default";
                if (node.PredictorLst[x].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical)
                    Grid.Rows[x].Cells[5].Value = " - ";
                else
                    Grid.Rows[x].Cells[5].Value = node.PredictorLst[x].SplitValue;
                if (node.PredictorLst[x].SplitStatus == Predictor.SplitStatusEnum.TooManyValuesToSearch) {
                    Grid.Rows[x].Cells[3].Value = "Too many values to test (" + node.PredictorLst[x].DistinctValuesCount + ")";
                    Grid.Rows[x].Cells[4].Value = "N/A";
                    for (int c = 0; c < 6; ++c)
                        Grid.Rows[x].Cells[c].Style.BackColor = Color.Yellow;
                } else
                if (node.PredictorLst[x].SplitStatus == Predictor.SplitStatusEnum.NotEnoughCases) {
                    Grid.Rows[x].Cells[3].Value = "Too few elements for one of the nodes";
                    Grid.Rows[x].Cells[4].Value = "N/A";
                    for (int c = 0; c < 6; ++c)
                        Grid.Rows[x].Cells[c].Style.BackColor = Color.Red;
                } else
                if (node.PredictorLst[x].SplitStatus == Predictor.SplitStatusEnum.OneClassNode) {
                    Grid.Rows[x].Cells[3].Value = "This node is pure";
                    Grid.Rows[x].Cells[4].Value = "N/A";
                    for (int c = 0; c < 6; ++c)
                        Grid.Rows[x].Cells[c].Style.BackColor = Color.Red;
                } else
                if (node.PredictorLst[x].SplitStatus == Predictor.SplitStatusEnum.OnlyOneValueAvailable) {
                    Grid.Rows[x].Cells[3].Value = "Only one value available";
                    Grid.Rows[x].Cells[4].Value = "N/A";
                    for (int c = 0; c < 6; ++c)
                        Grid.Rows[x].Cells[c].Style.BackColor = Color.Red;
                } else {
                    if (Def.Tree.Algorithm != Tree.AlgorithmEnum.MaxDif) {
                        Grid.Rows[x].Cells[3].Value = Math.Round(node.PredictorLst[x].Gain, 8);
                        if (node.PredictorLst[x].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical)
                            Grid.Rows[x].Cells[3].Value += " (" + node.PredictorLst[x].DistinctValuesCount + " vls)";
                        Grid.Rows[x].Cells[4].Value = Math.Round((node.PredictorLst[x].Gain * node.Table.RowCount) / (node.Table.RowCount - node.PredictorLst[x].NullCount), 8);
                    } else {
                        Grid.Rows[x].Cells[3].Value = Math.Round(node.PredictorLst[x].Gain, 8);
                        Grid.Rows[x].Cells[4].Value = Math.Round((node.PredictorLst[x].Gain * node.Table.RowCount) / (node.Table.RowCount - node.PredictorLst[x].NullCount), 6);
                    }
                }
            }
            Grid.Rows[node.BestSplitIdx].Selected = true;
            if(node.BestSplit != null)
                if(node.BestSplit.SplitStatus == Predictor.SplitStatusEnum.CanBeUsed)
                    for (int x = 0; x < 6; ++x) {
                        Grid.Rows[node.BestSplitIdx].Cells[x].Style.BackColor = Color.Green;
                    }
         }

        private void btProperties_Click(object sender, EventArgs e)
        {
            Predictor predictorSelected = node.PredictorLst[Grid.SelectedCells[0].RowIndex];
            //if it has TooManyCasesToSearch it can be manually split
            if (predictorSelected.SplitStatus != Predictor.SplitStatusEnum.CanBeUsed && predictorSelected.SplitStatus != Predictor.SplitStatusEnum.TooManyValuesToSearch) {
                MessageBox.Show("Properties not available for this predictor", "Warning");
                return;
            }
            if (predictorSelected.Variable.VariableTypeUserSet.ToString() == "Continuous") {
                FrmContinuousSplitProperties fccsp = new FrmContinuousSplitProperties(node, predictorSelected);
                fccsp.ShowDialog(this);
            } else {
                FrmCategoricalSplitProperties fccsp = new FrmCategoricalSplitProperties(node, predictorSelected);
                fccsp.ShowDialog();
            }
            if (predictorSelected.CustomisedSplit == true) {
                Grid.Rows[Grid.SelectedCells[0].RowIndex].Cells[2].Value = "Custom";
                if (Def.Tree.Algorithm != Tree.AlgorithmEnum.MaxDif) {
                    Grid.Rows[Grid.SelectedCells[0].RowIndex].Cells[3].Value = Math.Round(predictorSelected.Gain, 2);
                    Grid.Rows[Grid.SelectedCells[0].RowIndex].Cells[4].Value = Math.Round((predictorSelected.Gain * node.Table.RowCount) / (node.Table.RowCount - predictorSelected.NullCount), 2);
                } else {
                    Grid.Rows[Grid.SelectedCells[0].RowIndex].Cells[3].Value = Math.Round(predictorSelected.Gain, 6);
                    Grid.Rows[Grid.SelectedCells[0].RowIndex].Cells[4].Value = Math.Round((predictorSelected.Gain * node.Table.RowCount) / (node.Table.RowCount - predictorSelected.NullCount), 6);
                }
                if (predictorSelected.Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous)
                    Grid.Rows[Grid.SelectedCells[0].RowIndex].Cells[5].Value = predictorSelected.SplitValue.ToString();
            }
        }

        private void btSplit_Click(object sender, EventArgs e)
        {
            node.SplitVariableIdx = Grid.SelectedCells[0].RowIndex;
            //if it is a user defined split, then it can be used
            if (node.SplitVariable.SplitStatus != Predictor.SplitStatusEnum.CanBeUsed && node.SplitVariable.CustomisedSplit != true) {
                MessageBox.Show("Predictor not available to split", "Warning");
                return;
            }
            node.Tree.ManuallySplit(node);
            Close();
        }

    }
}