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
    partial class FrmCategoricalSplitProperties : Form {

        Node node;
        Predictor pred;
//        bool gainOutdated = false;
        bool gainOutdated = true; //Use false, true just for checking

        public FrmCategoricalSplitProperties(Node _node, Predictor _pred) {
            node = _node;
            pred = _pred;
            List<string> caseLst;

            InitializeComponent();

            //If a split could not be defined, leave the 1st on the left and the rest on the right
            if (pred.ChildrenGroups == null) {
                //Set the possible children
                MessageBox.Show("This split has not been computed", "Warning!"); 
                ValueGroup valueGroup = new ValueGroup(_pred, 2);
                pred.ChildrenGroups = valueGroup;

                lbLeft.Items.Add(pred.ValueSd.Keys[0]);
                for (int i = 1; i < pred.DistinctValuesCount; ++i) {
                       lbRight.Items.Add(pred.ValueSd.Keys[i]);
                }
                checks();
                return;
            }

            caseLst = pred.ChildrenGroups.ValueGroupLst[0];
            for (int i = 0; i < caseLst.Count; ++i) {
                  lbLeft.Items.Add(caseLst[i]);
            }

            caseLst = pred.ChildrenGroups.ValueGroupLst[1];
            for (int i = 0; i < caseLst.Count; ++i) {
                  lbRight.Items.Add(caseLst[i]);
            }
            checks();
        }

        private void btClose_Click(object sender, EventArgs e) {
            Close();
        }

        private void checks() {
            
            if (lbLeft.Items.Count == 0)
                btToRight.Enabled = false;
            else
                btToRight.Enabled = true;

            if (lbRight.Items.Count == 0)
                btToLeft.Enabled = false;
            else
                btToLeft.Enabled = true;
        }

        private void btToRight_Click(object sender, EventArgs e)
        {
            List<string> remove = new List<string>();

            if (lbLeft.SelectedItems.Count == lbLeft.Items.Count) {
                MessageBox.Show("You can not move all the cases. At least one item should remain in this node", "Warning");
                return;
            }

            foreach (string s in lbLeft.SelectedItems) {
                remove.Add(s);
                lbRight.Items.Add(s);
            }
            foreach(string s in remove){
                lbLeft.Items.Remove(s);
            }
            pred.CustomisedSplit = true;
            gainOutdated = true;
            checks();
        }

        private void btToLeft_Click(object sender, EventArgs e)
        {
            List<string> remove = new List<string>();

            if (lbRight.SelectedItems.Count == lbRight.Items.Count) {
                MessageBox.Show("You can not move all the cases. At least one item should remain in this node", "Warning");
                return;
            }

            foreach (string s in lbRight.SelectedItems) {
                remove.Add(s);
                lbLeft.Items.Add(s);
            }
            foreach (string s in remove) {
                lbRight.Items.Remove(s);
            }
            pred.CustomisedSplit = true;
            gainOutdated = true;
            checks();
        }

        private void FrmCartCategoricalSplitProperties_Load(object sender, EventArgs e)
        {
            lbGain.Text = Convert.ToString(Math.Round(pred.Gain, 2));
            checks();
        }

        private void FrmCartCategoricalSplitProperties_FormClosing(object sender, FormClosingEventArgs e)
        {
            List<string> caseLLst = new List<string>(lbLeft.Items.Count);
            List<string> caseRLst = new List<string>(lbRight.Items.Count);
        
            pred.ChildrenGroups.ValueGroupLst[0].Clear();
            pred.ChildrenGroups.ValueGroupLst[1].Clear();

            foreach (string s in lbLeft.Items) {
                 caseLLst.Add(s);
            }
            pred.ChildrenGroups.ValueGroupLst[0] = caseLLst;
            foreach (string r in lbRight.Items) {
                 caseRLst.Add(r);
            }
            pred.ChildrenGroups.ValueGroupLst[1] = caseRLst;
            GainCalculate();            
        }

        private void GainCalculate() {
            if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous)
                GainRegCalculate();
            else
                GainClfCalculate();
            gainOutdated = false;
        }

        private void GainClfCalculate() {

            string left="", right="";
            string sql = "";
            double splitImp = 0;
            double leftCount = 0, rightCount = 0;
            double leftImp = 0, rightImp = 0;
            List<double> leftNLst = null;
            List<double> rightNLst = null;


            if (gainOutdated == false)
                return;

            for (int i = 0; i < lbLeft.Items.Count; ++i) {
                left += pred.Variable.Name + "='" + lbLeft.Items[i] + "' ";
                if (i < (lbLeft.Items.Count - 1)) {
                    left += " or ";
                }else
                    left += ")";
            }

            for (int i = 0; i < lbRight.Items.Count; ++i) {
                right += pred.Variable.Name + "='" + lbRight.Items[i] + "' ";
                if (i < (lbRight.Items.Count - 1)) {
                    right += " or ";
                } else
                    right += ")";
            }

            sql =
            @"SELECT " +
                "COUNT(" + node.Tree.Schema.Target.Name + ") " + 
            "FROM "
               + Def.DbBsTb + " , " + Def.DbTrTb + node.Id + " " +
            "WHERE ("
                + Def.DbBsTb + "." + Def.DbTableIdName + "=" +
                Def.DbTrTb + node.Id + "." + Def.DbTableIdName + ") and (" +
                left + " "+
            "GROUP BY " + node.Tree.Schema.Target.Name;
            leftNLst = Def.Db.GetNumberLst(sql);
            sql = 
            @"SELECT " + 
                "COUNT(" + node.Tree.Schema.Target.Name + ") " +
            "FROM "
               + Def.DbBsTb + " , " + Def.DbTrTb + node.Id + " " +
            "WHERE ("
                + Def.DbBsTb + "." + Def.DbTableIdName + "=" +
                Def.DbTrTb + node.Id + "." + Def.DbTableIdName + ") and (" +
                right + " " +
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
            lbGain.Text = Convert.ToString(Math.Round(pred.Gain, 2));
        }

        private void GainRegCalculate() {

            string left = "", right = "";
            string sql = "";
            double varianceSplit;
            List<double> leftNLst = null;
            List<double> rightNLst = null;

            if (gainOutdated == false)
                return;

            for (int i = 0; i < lbLeft.Items.Count; ++i) {
                left += pred.Variable.Name + "='" + lbLeft.Items[i] + "' ";
                if (i < (lbLeft.Items.Count - 1)) {
                    left += " or ";
                } else
                    left += ")";
            }

            for (int i = 0; i < lbRight.Items.Count; ++i) {
                right += pred.Variable.Name + "='" + lbRight.Items[i] + "' ";
                if (i < (lbRight.Items.Count - 1)) {
                    right += " or ";
                } else
                    right += ")";
            }
            sql =
            @"SELECT " +
                "COALESCE(variance(" + node.Tree.Schema.Target.Name + "), 0), count(" + pred.Variable.Name + ") " +
            "FROM "
               + Def.DbBsTb + " , " + Def.DbTrTb + node.Id + " " +
            "WHERE ("
                + Def.DbBsTb + "." + Def.DbTableIdName + "=" +
                Def.DbTrTb + node.Id + "." + Def.DbTableIdName + ") and (" +
                left;
            leftNLst = Def.Db.GetNumberRowLst(sql);
            sql =
            @"SELECT " +
                "COALESCE(variance(" + node.Tree.Schema.Target.Name + "), 0), count(" + pred.Variable.Name + ") " +
            "FROM "
               + Def.DbBsTb + " , " + Def.DbTrTb + node.Id + " " +
            "WHERE ("
                + Def.DbBsTb + "." + Def.DbTableIdName + "=" +
                Def.DbTrTb + node.Id + "." + Def.DbTableIdName + ") and (" +
                right;
            rightNLst = Def.Db.GetNumberRowLst(sql);
            varianceSplit = (leftNLst[0] * leftNLst[1] + rightNLst[0] * rightNLst[1]) / (leftNLst[1] + rightNLst[1]);
            pred.Gain = (node.Imp - varianceSplit) * 100 / node.Imp; ;
            pred.Gain *= (double)(node.Table.RowCount - pred.NullCount) / node.Table.RowCount;
            lbGain.Text = Convert.ToString(Math.Round(pred.Gain, 2));
        }


        private void btRecalculateGain_Click(object sender, EventArgs e) {
             GainCalculate();
        }

        private void btLeftShowAsText_Click(object sender, EventArgs e) {
            FrmTextBox ftb = new FrmTextBox();
            ftb.Text = "Left node values for " + pred.Variable.Name;
            for (int i = 0; i < lbLeft.Items.Count; ++i) {
                ftb.TextBox.Text+=lbLeft.Items[i];
                if (i < (lbLeft.Items.Count - 1)) {
                    ftb.TextBox.Text+="\r\n";
                }
            }
            ftb.Show();
        }

        private void btRightShowAsText_Click(object sender, EventArgs e) {
            FrmTextBox ftb = new FrmTextBox();
            ftb.Text = "Right node values for " + pred.Variable.Name;
            for (int i = 0; i < lbRight.Items.Count; ++i) {
                ftb.TextBox.Text += lbRight.Items[i];
                if (i < (lbRight.Items.Count - 1)) {
                    ftb.TextBox.Text += "\r\n";
                }
            }
            ftb.Show();
        }
    }
}