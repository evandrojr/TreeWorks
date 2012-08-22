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
    public class NodeTargetContinuous : Node{

        public NodeTargetContinuousUI UI;
        public double Mean, Variance, StdDev;
        
        //Used only for the root because it does not have ancestor
        public NodeTargetContinuous() : base() {
            ImpCalc();
            UI = new NodeTargetContinuousUI(this);
        }

        public NodeTargetContinuous(Node ancestor) : base(ancestor) {
            ImpCalc(); // Outdated use the PreCalculated
            //Imp = Ancestor.DescendentImpPreCalculated[ancestor.DescendentLst.Count - 1];
            UpdateLevelLst(this);
            UI = new NodeTargetContinuousUI(this);
        }

        public override void AllGainsCalc() {

            int valueCount=0;
            for (int x = 0; x < PredictorLst.Count; ++x) {

                valueCount = PredictorLst[x].DistinctValuesCount;
                if (valueCount <= 1) {
                    PredictorLst[x].SplitStatus = Predictor.SplitStatusEnum.OnlyOneValueAvailable;
                    PredictorLst[x].Gain = 0;
                    PredictorLst[x].SplitValue = 0;
                    continue;
                }
                if (PredictorLst[x].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                    Riv.MinInfoCont(this, PredictorLst[x]);
                    continue;
                }
                if (PredictorLst[x].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical) {
                    Riv.MinInfoCat(this, PredictorLst[x]);
                }
            }
        }

        //
        //public override void AllGainsCalcMv() {
        //    MessageBox.Show("AllGainsCalcMv() has not been implemented yet");
        //}

        public override void LocalMinimumSearchMv() {
            MessageBox.Show("LocalMinimumSearchMv() has not been implemented yet");
        }

        public void StdDevCalc() {
            //Outdated can be done with one database pass (implement the function!)
            string sql = 
            @"Select 
                COALESCE(avg(" + Def.Schema.Target.Name + "),-1) " +
            "FROM " + 
                Def.DbTrTb + this.Id + "," + Def.DbBsTb + " " +
            "WHERE " +
                Def.DbTrTb + this.Id + "." + Def.DbTableIdName + "=" + Def.DbBsTb + "." + Def.DbTableIdName;
            Mean = Def.Db.GetNumber(sql);
            sql = 
            @"Select 
                COALESCE(variance(" + Def.Schema.Target.Name + "),0) " +
            "FROM " +
                Def.DbTrTb + this.Id + "," + Def.DbBsTb + " " +
            "WHERE " +
                Def.DbTrTb + this.Id + "." + Def.DbTableIdName + "=" + Def.DbBsTb + "." + Def.DbTableIdName;
            Variance = Def.Db.GetNumber(sql);
            StdDev = Math.Sqrt(Variance);
        }

        public override Point CenterCoord {
            set {
                Point pt = (Point)value;
                centerCoord.X = pt.X;
                centerCoord.Y = pt.Y;
                this.UI.Left = centerCoord.X - (this.UI.Width / 2);
                this.UI.Top = centerCoord.Y - (this.UI.Height / 2);
                this.UI.LabelTop.Left = centerCoord.X - (this.UI.LabelTop.Width / 2);
                this.UI.LabelTop.Top = this.UI.Top - (int)(Tree.LabelTopDistanceToNode + UI.LabelTop.Height);
                this.UI.LabelBottom.Left = centerCoord.X - (int)(this.UI.LabelBottom.Width / 2);
                this.UI.LabelBottom.Top = this.UI.Top + this.UI.Height + (Tree.LabelBottomDistanceToNode);
            }
            get {
                return centerCoord;
            }
        }

        public override void Dispose() {
            UI.Dispose();
            Def.Tree.NodeLst.Remove((Node)this);
            Table = null;
            Ancestor = null;
            DescendentLst = null;
            PredictorLst = null;
        }

        public override int Width {
            get { return UI.Width; }
        }

        public override int Height {
            get { return UI.Height; }
        }

        public override string LabelTopText {
            get { return UI.LabelTop.Text; }
            set { UI.LabelTop.Text = value; }
        }

        public override void LabelBottomShow() {
            UI.LabelBottom.Show();
        }

        public override void LabelBottomHide() {
            UI.LabelBottom.Hide();
        }


        public override string LabelBottomText {
            get { return UI.LabelBottom.Text; }
            set { UI.LabelBottom.Text=value; }
        }

        public override bool SplitDone {
            get { return splitDone; }
            set {
                splitDone = value;
                if (value == true) {
                    UI.removeSubNodesToolStripMenuItem.Enabled = true;
                    UI.manuallysplitToolStripMenuItem.Enabled = false;
                    UI.autosplitToolStripMenuItem.Enabled = false;
                    UI.fullautogrowToolStripMenuItem.Enabled = false;
                    UI.LabelBottom.Show();
                } else {
                    UI.removeSubNodesToolStripMenuItem.Enabled = false;
                    UI.manuallysplitToolStripMenuItem.Enabled = true;
                    UI.autosplitToolStripMenuItem.Enabled = true;
                    UI.fullautogrowToolStripMenuItem.Enabled = true;
                }
            }
        }

        public override void ImpCalc() {
            StdDevCalc(); //Calculates and sets the Mean and Variance too
            Imp = Variance;
        }

        public override void DisplayUpdate() {
            StdDevCalc();
            ImpCalc();
            UI.DisplayUpdate();
        }

    }
}
