using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Spartacus {
    public partial class FrmVltValidation : Form {
        public FrmVltValidation() {
            InitializeComponent();
        }

        private void FrmRValidation_Load(object sender, EventArgs e) {
            
            NodeTargetContinuous n;
            grid.Rows.Add(Def.Tree.NodeLst.Count);
            string sqlVN, sqlVMean, sqlVStdev;
            double vn, vmean, vstdev, vrootN=0, vrootMean=0, trootMean=0, tn=0, tmean=0, tstdev=0;
            double tfitnessNum = 0, tfitnessDen = 0;
            double vfitnessNum = 0, vfitnessDen = 0;
            double pStdev = 0; 
            double T = 0, Talfa2DF;
            double[] Tpdf5percTwoSidedArr = new double[] {12.7062, 4.30265, 3.18245, 2.77645, 2.57058, 2.44691, 2.36462, 2.306, 2.26216, 2.22814, 2.20099, 2.17881, 2.16037, 2.14479, 2.13145, 2.11991, 2.10982, 2.10092, 2.09302, 2.08596, 2.07961, 2.07387, 2.06866, 2.0639, 2.05954, 2.05553, 2.05183, 2.04841, 2.04523, 2.04227, 1.95996};

            List<double> classVal;

            for(int y=0; y < Def.Tree.NodeLst.Count; ++y) {
                n = (NodeTargetContinuous) Def.Tree.NodeLst[y];
                tn = n.Table.RowCount;
                tmean = n.Mean;
                tstdev = n.StdDev;
                sqlVN = "Select count(*) from " + Def.DbTsTb + n.Id;
                sqlVMean =
                @"Select 
                COALESCE(avg(" + Def.Schema.Target.Name + "),0) " +
                "FROM " +
                    Def.DbTsTb + n.Id + "," + Def.DbBsTb + " " +
                "WHERE " +
                    Def.DbTsTb + n.Id + "." + Def.DbTableIdName + "=" + Def.DbBsTb + "." + Def.DbTableIdName;
                sqlVStdev =
                @"Select 
                COALESCE(STDDEV(" + Def.Schema.Target.Name + "),0) " +
                "FROM " +
                    Def.DbTsTb + n.Id + "," + Def.DbBsTb + " " +
                "WHERE " +
                    Def.DbTsTb + n.Id + "." + Def.DbTableIdName + "=" + Def.DbBsTb + "." + Def.DbTableIdName;
                vn = Def.Db.GetNumber(sqlVN);
                vmean = Def.Db.GetNumber(sqlVMean);
                if(y==0){
                    trootMean = n.Mean;
                    vrootMean=vmean;
                    vrootN = vn;
                }
                vstdev = Def.Db.GetNumber(sqlVStdev);
                grid.Rows[y].HeaderCell.Value = n.Id.ToString();
                grid.Rows[y].Cells[0].Value = n.Table.RowCount;
                grid.Rows[y].Cells[1].Value = vn;
                grid.Rows[y].Cells[2].Value = Math.Round((double) (n.Table.RowCount * 100) / (Def.Tree.Root.Table.RowCount) , 2);
                grid.Rows[y].Cells[3].Value = Math.Round(vn * 100 / vrootN, 2);
                grid.Rows[y].Cells[4].Value = Math.Round(n.Mean, 2);
                grid.Rows[y].Cells[5].Value = Math.Round(vmean, 2);
                grid.Rows[y].Cells[6].Value = Math.Round(n.StdDev, 2);
                grid.Rows[y].Cells[7].Value = Math.Round(vstdev, 2);

                if (vn >= 5 && n.Table.RowCount >= 5) {
                    if ((vn + n.Table.RowCount - 2) < 31) {
                        Talfa2DF = Tpdf5percTwoSidedArr[(int) vn + n.Table.RowCount - 3];
                    } else
                        Talfa2DF = 1.96;
                    pStdev = Math.Sqrt(((tn - 1) * tstdev * tstdev + (vn-1) * vstdev * vstdev) / (tn+vn-2));
                    T = (tmean - vmean) / (pStdev * Math.Sqrt(1 / tn + 1 / vn));
                    grid.Rows[y].Cells[8].Value = Math.Round(T, 2);
                    grid.Rows[y].Cells[9].Value = Math.Round(Talfa2DF, 2);
                    if (T < (Talfa2DF * -1) || T > Talfa2DF)
                        grid.Rows[y].Cells[10].Value = "Fail";
                    else
                        grid.Rows[y].Cells[10].Value = "Pass";
                } else {
                    if (vn >= 1 && n.Table.RowCount >= 1) {
                        Talfa2DF = Tpdf5percTwoSidedArr[(int) vn + n.Table.RowCount - 3];
                    } else {
                        Talfa2DF = double.NaN;
                    }
                    grid.Rows[y].Cells[8].Value = "?";
                    grid.Rows[y].Cells[9].Value = Math.Round(Talfa2DF, 2);
                    grid.Rows[y].Cells[10].Value = "?";
                }



    //Error Calculation
                if (n.DescendentLst.Count == 0) {
                    tfitnessNum += vn * Math.Pow((n.Mean - trootMean), 2);
                    vfitnessNum += vn * Math.Pow((vmean - vrootMean), 2);
                    classVal = Def.Db.GetNumberLst(
                    "SELECT ALL " + Def.Schema.Target.Name + 
                    " FROM " +
                    Def.DbTsTb + n.Id + "," + Def.DbBsTb + " " +
                    "WHERE " +
                    Def.DbTsTb + n.Id + "." + Def.DbTableIdName + "=" + Def.DbBsTb + "." + Def.DbTableIdName);
                    foreach(double cv in classVal){
                        vfitnessDen += Math.Pow(cv - vrootMean, 2);
                    }
                    classVal = Def.Db.GetNumberLst(
                    "SELECT ALL " + Def.Schema.Target.Name +
                    " FROM " +
                    Def.DbTrTb + n.Id + "," + Def.DbBsTb + " " +
                    "WHERE " +
                    Def.DbTrTb + n.Id + "." + Def.DbTableIdName + "=" + Def.DbBsTb + "." + Def.DbTableIdName);
                    foreach (double cv in classVal) {
                        tfitnessDen += Math.Pow(cv - trootMean, 2);
                    }                
                }

                lbTrTreeFitness.Text = "Training tree error: "  + Math.Round(tfitnessNum / tfitnessDen, 4);
                lbVlTreeFitness.Text = "Validation tree error: " + Math.Round(vfitnessNum / vfitnessDen, 4);
        //End error Calculation
            }
        }
    }
}