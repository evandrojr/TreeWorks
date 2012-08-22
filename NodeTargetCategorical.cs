#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Drawing;
using System.Drawing.Imaging;

#endregion

namespace Spartacus {
    public class NodeTargetCategorical : Node {

        public NodeTargetCategoricalUI UI;
        public TargetCategoricalClass TargetClasses;

        //Used only for the root because it does not have ancestor
        public NodeTargetCategorical() : base() {
            //Fill the TargetCategoricalCases
            TargetClasses = new TargetCategoricalClass(Def.Schema.Target, this);
            //Fills the TargetCases
            ImpCalc();
            if (Def.ExperimentRunning)
                return;
            UI = new NodeTargetCategoricalUI(this);
        }

        public NodeTargetCategorical(Node ancestor) : base(ancestor) {
            //Fill the TargetCategoricalCases
            TargetClasses = new TargetCategoricalClass(Def.Schema.Target, this);
            //Imp = ancestor.DescendentImpPreCalculated[ancestor.DescendentLst.Count - 1];
            ImpCalc(); // Outdated use the PreCalculated
            UpdateLevelLst(this);
            if (Def.ExperimentRunning)
                return;
            UI = new NodeTargetCategoricalUI(this);
        }

        public override void AllGainsCalc() {

            int valueCount = 0;
            for (int x = 0; x < PredictorLst.Count; ++x) {
                if (TargetClasses.ClassCount == 1) {
                    PredictorLst[x].SplitStatus = Predictor.SplitStatusEnum.OneClassNode;
                    PredictorLst[x].Gain = 0;
                    PredictorLst[x].SplitValue = 0;
                    continue;
                }
                valueCount = PredictorLst[x].DistinctValuesCount;
                if (valueCount <= 1) {
                    PredictorLst[x].SplitStatus = Predictor.SplitStatusEnum.OnlyOneValueAvailable;
                    PredictorLst[x].Gain = 0;
                    PredictorLst[x].SplitValue = 0;
                    continue;
                }
                if (PredictorLst[x].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                    if (this.Tree.Algorithm == Tree.AlgorithmEnum.Entropy) {
                        Entropy.MinImpCont(this, PredictorLst[x]);
                    }else
                    if (this.Tree.Algorithm == Tree.AlgorithmEnum.Gini) {
                        Gini.MinImpCont(this, PredictorLst[x]);
                    }
                    else
                    if (this.Tree.Algorithm == Tree.AlgorithmEnum.MaxDif) {
                        MaxDif.MinImpCont(this, PredictorLst[x]);
                    }
                }
                if (PredictorLst[x].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical) {
                    if (this.Tree.Algorithm == Tree.AlgorithmEnum.Entropy) {
                        if (PredictorLst[x].DistinctValuesCount <= Def.ClfMaxNumberOfValuesForFullSearch)
                            if (PredictorLst[x].DistinctValuesCount <= Def.ClfMaxNumberOfValuesForFullSearch)
                                Entropy.MinImpCatFullSearch(this, PredictorLst[x]);
                            else
                                Entropy.MinImpCatProgressive(this, PredictorLst[x]);
                    } else
                    if (this.Tree.Algorithm == Tree.AlgorithmEnum.Gini) {
                            if (PredictorLst[x].DistinctValuesCount <= Def.ClfMaxNumberOfValuesForFullSearch)
                                Gini.MinImpCatFullSearch(this, PredictorLst[x]);
                            else
                                Gini.MinImpCatProgressive(this, PredictorLst[x]);
                    } else
                    if (this.Tree.Algorithm == Tree.AlgorithmEnum.MaxDif) {
                            if (PredictorLst[x].DistinctValuesCount <= Def.ClfMaxNumberOfValuesForFullSearch)
                                MaxDif.MinFullSearchImp(this, PredictorLst[x]);
                            else
                                MaxDif.MinImpCatProgressive(this, PredictorLst[x]);
                    }//else
                    //if (this.Tree.Algorithm == Tree.AlgorithmEnum.Hybrid) {
                    //        if (PredictorLst[x].DistinctValuesCount <= Def.ClfMaxNumberOfValuesForFullSearch)
                    //            Hybrid.MinImpCatFullSearch(this, PredictorLst[x]);
                    //        else
                    //            Hybrid.MinImpCatProgressive(this, PredictorLst[x]);
                    //}

                }    
             }
        }

        public override void LocalMinimumSearchMv(){
            Def.LogMessage = "";
            //Gini.MinImpMvStartingFromBestNumericalNoOtherOrder(this, BestSplit);
            //Gini.MinImpMvStartingFromBestNumericalOrderedByBestUnivariateNumericalSplits(this, BestSplit);
            //Gini.MinImpMvStartingFromBestNominalNoOtherOrder(this, BestSplit);
            //Gini.MinImpMvStartingFromBestNominalOrderedByBestUnivariateNumericalSplit(this, BestSplit);



            Gini.MinImpMvGreed(this, BestSplit);

            if (!Def.ExperimentRunning) {
                FrmTextBox ft = new FrmTextBox();
                ft.TextBox.Text = Def.LogMessage;
                ft.TextBox.Font = new Font("Monotype", 10);
                ft.Text = "Log messages";
                ft.Show();
            }
            //this.MvTb.DataFill();
            //string f="";
            //for (int y = 0; y < Table.RowCount; ++y) {
            //    for (int x = 0; x < MvTb.PredMvLst.Count; ++x) {
            //        f += MvTb.PredMvLst[x].X(y) + " ";
            //    }
            //    f += Environment.NewLine;
            //}
            //FE.Show(f, "data");
        }

        public override Point CenterCoord {
            set {
                if (Def.ExperimentRunning) {
                    return;
                }
                Point pt = (Point)value;
                centerCoord.X = pt.X;
                centerCoord.Y = pt.Y;
                this.UI.Left = centerCoord.X - (this.UI.Width / 2);
                this.UI.Top = centerCoord.Y - (this.UI.Height / 2);
                this.UI.LabelTop.Left = centerCoord.X - (this.UI.LabelTop.Width / 2);
                this.UI.LabelTop.Top = this.UI.Top - (int)(Tree.LabelTopDistanceToNode + UI.LabelTop.Height);
                this.UI.LabelBottom.Left = centerCoord.X - (int)(this.UI.LabelBottom.Width / 2);
                this.UI.LabelBottom.Top = this.UI.Top + this.UI.Height + (Tree.LabelBottomDistanceToNode);
                this.UI.Grid.Top = this.UI.Top + this.UI.Height;
                this.UI.Grid.Left = this.UI.Left + 20;
            }
            get {
                if (Def.ExperimentRunning)
                    return new Point(0, 0);
                return centerCoord;
            }
        }

        public override int Width {
            get { return UI.Width; }
        }

        public override int Height {
            get { return UI.Height; }
        }

        public override string LabelTopText {
            get {
                if (Def.ExperimentRunning)
                    return "Experiment";
                return UI.LabelTop.Text; }
            set {
                if (Def.ExperimentRunning)
                    return;
                UI.LabelTop.Text = value; }
        }

        public override void LabelBottomShow() {
            UI.LabelBottom.Show();
        }

        public override void LabelBottomHide() {
            UI.LabelBottom.Hide();
        }

        public override string LabelBottomText {
            get {
                if (Def.ExperimentRunning)
                    return "Experiment"; 
                return UI.LabelBottom.Text; }
            set {
                if (Def.ExperimentRunning)
                    return;
                UI.LabelBottom.Text = value; }
        }

        public override void Dispose() {
            Def.Tree.NodeLst.Remove((Node)this);
            Table = null;
            Ancestor = null;
            DescendentLst = null;
            PredictorLst = null;
            UI.Dispose();
        }

        public override bool SplitDone {
            get{return splitDone;}
            set { splitDone = value;
                if (value == true) {
                    if (!Def.ExperimentRunning) {
                        UI.removeSubNodesToolStripMenuItem.Enabled = true;
                        UI.manuallysplitToolStripMenuItem.Enabled = false;
                        UI.autosplitToolStripMenuItem.Enabled = false;
                        UI.fullautogrowToolStripMenuItem.Enabled = false;
                        UI.LabelBottom.Show();
                    }
                } else {
                    if (!Def.ExperimentRunning) {
                        UI.removeSubNodesToolStripMenuItem.Enabled = false;
                        UI.manuallysplitToolStripMenuItem.Enabled = true;
                        UI.autosplitToolStripMenuItem.Enabled = true;
                        UI.fullautogrowToolStripMenuItem.Enabled = true;
                    }
                }
            }
        }

        public override void DisplayUpdate() {
            if (Def.ExperimentRunning)
                return;
            Point pos = UI.Location;
            UI.Dispose();
            UI = new NodeTargetCategoricalUI(this);
            UI.Location = pos;
        }

        public override void ImpCalc() {
            if (Def.Tree.Algorithm == Tree.AlgorithmEnum.Entropy)
                Imp = Entropy.NodeImp(this);
            else
            if (Def.Tree.Algorithm == Tree.AlgorithmEnum.Gini)
                Imp = Gini.NodeImp(this);
            else
            if (Def.Tree.Algorithm == Tree.AlgorithmEnum.MaxDif)
                Imp = MaxDif.NodeImp(this);
            else{
                MessageBox.Show("No algorithm defined for the tree (NodeTagetCategorical.cs)");
            }
        }


    }
}
