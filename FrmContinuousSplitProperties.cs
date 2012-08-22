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
    partial class FrmContinuousSplitProperties : Form {

        Node node;
        Predictor pred;
        List<double> NLst;

        public FrmContinuousSplitProperties(Node _node, Predictor _pred) {
            InitializeComponent();
            node = _node;
            pred = _pred;
        }

        private void FrmCartContinuousSplitProperties_Load(object sender, EventArgs e)
        {
            double l,h;
            double splitValue = pred.SplitValue;
            int splitIndex=-1;
            string sql;
            List<double> OriginalValLst;

            tbGain.Text = pred.Gain.ToString();

            if(pred.DistinctValuesCount == 1)
                splitIndex = 0;

            sql=@"
            SELECT 
                DISTINCT " + pred.Variable.Name + " " + 
            "FROM "
                + Def.DbBsTb + " , " + Def.DbTrTb + node.Id + " " + 
            "WHERE " 
                + Def.DbBsTb + "." + Def.DbTableIdName + "=" +
                Def.DbTrTb + node.Id + "." + Def.DbTableIdName + " AND " +
                pred.Variable.Name + " IS NOT NULL " +
            "ORDER BY " 
                + pred.Variable.Name + " ASC ";

            OriginalValLst = Def.Db.GetNumberLst(sql);
            if (OriginalValLst.Count < 2) {
                MessageBox.Show("Less than two values available for the variable this form wasn't supposed to be shown", "Error");
                return;
            }
            trackBar.SetRange(0, OriginalValLst.Count - 2);
            NLst = new List<double>(OriginalValLst.Count);
            NLst.Add(OriginalValLst[0]);
            for (int i = 1; i < OriginalValLst.Count - 2; ++i) {
                NLst.Add((OriginalValLst[i - 1] + OriginalValLst[i + 1])/2);
            }
            NLst.Add(OriginalValLst[OriginalValLst.Count-2]);
            h = OriginalValLst[OriginalValLst.Count - 2];
            for (int i = 0; i < NLst.Count; ++i) {
                if (NLst[i] == splitValue) {
                    splitIndex = i;
                    break;
                }
            }
            if (splitIndex == -1) {
                MessageBox.Show("FrmCartContinuousSplitProperties: Could not find index of the variable", "Error");
                return;
            } else
                trackBar.Value = splitIndex;

            tbSplitValue.Text = splitValue.ToString();
            l = pred.LowerNumber;
            
            lbLower.Text = l.ToString();
            lbHigher.Text = h.ToString();
        }

        private void trackBar_Scroll(object sender, EventArgs e) {
            tbSplitValue.Text = NLst[trackBar.Value].ToString();
            pred.CustomisedSplit = true;
            tbGain.Text = "?";
            pred.SplitValue = NLst[trackBar.Value];
        }

        private void btClose_Click(object sender, EventArgs e){
            Close();
        }

        private void GainCalculate() {
            if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous)
                GainRegCalculate();
            else
                GainClfCalculate();
        }

        private void GainRegCalculate() {

            double varianceSplit;
            List<double> leftNLst = null;
            List<double> rightNLst = null;
            string sql;

            sql =
            @"SELECT 
                COALESCE(variance(" + node.Tree.Schema.Target.Name + "), 0), count(" + pred.Variable.Name + ") " +
            "FROM "
               + Def.DbBsTb + " , " + Def.DbTrTb + node.Id + " " +
            "WHERE "
                + Def.DbBsTb + "." + Def.DbTableIdName + "=" +
                Def.DbTrTb + node.Id + "." + Def.DbTableIdName + " and " +
                Def.DbBsTb + "." + pred.Variable.Name + "<=" +
               pred.SplitValue;
            leftNLst = Def.Db.GetNumberRowLst(sql);
            sql = @"
            SELECT 
                COALESCE(variance(" + node.Tree.Schema.Target.Name + "), 0), count(" + pred.Variable.Name + ") " +
            "FROM "
               + Def.DbBsTb + " , " + Def.DbTrTb + node.Id + " " +
            "WHERE "
                + Def.DbBsTb + "." + Def.DbTableIdName + "=" +
                Def.DbTrTb + node.Id + "." + Def.DbTableIdName + " and " +
                Def.DbBsTb + "." + pred.Variable.Name + ">" +
               pred.SplitValue;
            rightNLst = Def.Db.GetNumberRowLst(sql);
            if (leftNLst[0] == -1 || rightNLst[0] == -1)
                pred.Gain = Double.NaN;
            else {
                varianceSplit = (leftNLst[0] * leftNLst[1] + rightNLst[0] * rightNLst[1]) / (leftNLst[1] + rightNLst[1]);
                pred.Gain = (node.Imp - varianceSplit) * 100 / node.Imp; ;
                pred.Gain *= (double)(node.Table.RowCount - pred.NullCount) / node.Table.RowCount;
            }
            tbGain.Text = pred.Gain.ToString();
        }


        private void GainClfCalculate() {

            //Gain values are sometimes different for the one obtained from the purity method. Which is wrong?
            double splitImp = 0 ;
            double leftCount=0, rightCount=0;
            double leftImp = 0, rightImp = 0;
            List<double> leftNLst = null;
            List<double> rightNLst = null;
            string sql;

            sql =
            @"SELECT " +
                 "count(" + node.Tree.Schema.Target.Name + ") " +
            "FROM "
               + Def.DbBsTb + " , " + Def.DbTrTb + node.Id + " " +
            "WHERE "
                + Def.DbBsTb + "." + Def.DbTableIdName + "=" +
                Def.DbTrTb + node.Id + "." + Def.DbTableIdName + " and " +
                Def.DbBsTb + "." + pred.Variable.Name + "<=" +
               pred.SplitValue + " " +
             "GROUP BY " + node.Tree.Schema.Target.Name;
            
           leftNLst = Def.Db.GetNumberLst(sql);

           sql =
           @"SELECT " +
                "count(" + node.Tree.Schema.Target.Name + ") " +
           "FROM "
              + Def.DbBsTb + " , " + Def.DbTrTb + node.Id + " " +
           "WHERE "
               + Def.DbBsTb + "." + Def.DbTableIdName + "=" +
               Def.DbTrTb + node.Id + "." + Def.DbTableIdName + " and " +
               Def.DbBsTb + "." + pred.Variable.Name + ">" +
              pred.SplitValue + " " +
           "GROUP BY " + node.Tree.Schema.Target.Name;
           rightNLst = Def.Db.GetNumberLst(sql);

           for (int i = 0; i < leftNLst.Count; ++i) {
               leftCount += leftNLst[i];
           }
           for (int i = 0; i < rightNLst.Count; ++i) {
               rightCount += rightNLst[i];
           }
           if (Def.Tree.Algorithm == Tree.AlgorithmEnum.Entropy) {
               for (int i = 0; i < leftNLst.Count; ++i) {
                   leftImp += (leftNLst[i] / leftCount) * Math.Log(leftNLst[i] / leftCount);
               }
               leftImp *= -1;
               for (int i = 0; i < rightNLst.Count; ++i) {
                   rightImp += (rightNLst[i] / rightCount) * Math.Log(rightNLst[i] / rightCount);
               }
               rightImp *= -1;
           } else
           if (Def.Tree.Algorithm == Tree.AlgorithmEnum.Gini) {
               for (int i = 0; i < leftNLst.Count; ++i) {
                   leftImp += Math.Pow((double)leftNLst[i] / leftCount, 2);
               }
               leftImp = 1 - leftImp;
               for (int i = 0; i < rightNLst.Count; ++i) {
                   rightImp += Math.Pow((double)rightNLst[i] / rightCount, 2);
               }
               rightImp = 1 - rightImp;
           }

           splitImp = (leftImp * leftCount + rightImp * rightCount) / (leftCount + rightCount);
           pred.Gain = (node.Imp - splitImp) * 100 / node.Imp; ;
           pred.Gain *= (double)(node.Table.RowCount - pred.NullCount) / node.Table.RowCount;
           tbGain.Text = pred.Gain.ToString();
        }


        private void FrmCartContinuousSplitProperties_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tbGain.Text == "?") {
                GainCalculate();
            }
        }

        private void btGainCalc_Click(object sender, EventArgs e) {
            GainCalculate();
        }

    }
}