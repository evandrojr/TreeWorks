using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Spartacus {
    public partial class FrmClsValidation : Form {
        public FrmClsValidation() {
            InitializeComponent();
        }

        private void FrmClsValidation_Load(object sender, EventArgs e) {

            NodeTargetCategorical n;
            grid.Rows.Add(Def.Tree.NodeLst.Count+2);
            string sqlVlCountMinority, sqlVlMostCommonClass, sqlvN, vMostCommonClass, sqlVnCorrect;
            double tN, vN, vMis, vCrt, vrootN=-1;
            double vSumCrtPerc = 0, vSumMisPerc = 0, vSumRelCrtPerc = 0, vSumRelMisPerc = 0;
            double vSumCrt = 0, vSumMis = 0, vSumCases = 0;
            double vSumTermCrt = 0, vSumTermCases = 0;
            int y = 0;

            for(y=0; y < Def.Tree.NodeLst.Count; ++y) {
                n = (NodeTargetCategorical) Def.Tree.NodeLst[y];
                tN = (double) n.Table.RowCount;                
                sqlvN = "Select count(*) from " + Def.DbTsTb + n.Id;
                vN = Def.Db.GetNumber(sqlvN);
                if (y == 0)
                    vrootN = vN;
//                sqlCountMinority=@"Select 
                KeyValuePair<string, int> mode = new KeyValuePair<string, int>();
                mode = n.TargetClasses.Mode();

                if (vN > 0) {
                    sqlVlMostCommonClass =
                    @"Select "
                        + Def.DbBsTb + "." + Def.Schema.Target.Name + " " +
                    "FROM "
                        + Def.DbBsTb + "," + Def.DbTsTb + n.Id + " " +
                    "Where "
                        + Def.DbBsTb + "." + Def.DbTableIdName + " = " +
                        Def.DbTsTb + n.Id + "." + Def.DbTableIdName + " " +
                    "Group by " +
                        Def.DbBsTb + "." + Def.Schema.Target.Name + " " +
                    "Order by " +
                        "count(" + Def.Schema.Target.Name + ") desc limit 1";
                    vMostCommonClass = Def.Db.GetText(sqlVlMostCommonClass);
                } else {
                    vMostCommonClass = "(empty)";
                }

                sqlVnCorrect =
                    @"Select count(*) " +
                    "FROM "
                        + Def.DbBsTb + "," + Def.DbTsTb + n.Id + " " +
                    "Where "
                        + Def.DbBsTb + "." + Def.DbTableIdName + " = " +
                        Def.DbTsTb + n.Id + "." + Def.DbTableIdName + " and " +
                        Def.DbBsTb + "." + Def.Schema.Target.Name + " = '" + mode.Key + "'";
                vCrt = Def.Db.GetNumber(sqlVnCorrect);

                sqlVlCountMinority =
                    @"Select count(*) " +
                    "FROM "
                        + Def.DbBsTb + "," + Def.DbTsTb + n.Id + " " +
                    "Where "
                        + Def.DbBsTb + "." + Def.DbTableIdName + " = " +
                        Def.DbTsTb + n.Id + "." + Def.DbTableIdName + " and " +
                        Def.DbBsTb + "." + Def.Schema.Target.Name + " <> '" + mode.Key + "'"; ;
                vMis = Def.Db.GetNumber(sqlVlCountMinority);

                grid.Rows[y].HeaderCell.Value = n.Id.ToString();
                grid.Rows[y].Cells[0].Value = n.Table.RowCount;
                grid.Rows[y].Cells[1].Value = vN;
                grid.Rows[y].Cells[2].Value = Math.Round((double) (n.Table.RowCount * 100) / (Def.Tree.Root.Table.RowCount) , 2);
                grid.Rows[y].Cells[3].Value = Math.Round(vN * 100 / vrootN, 2);
                grid.Rows[y].Cells[4].Value = mode.Key;
                grid.Rows[y].Cells[5].Value = vMostCommonClass;
                grid.Rows[y].Cells[6].Value = vCrt.ToString();
                grid.Rows[y].Cells[7].Value = vMis.ToString();
                grid.Rows[y].Cells[8].Value = Math.Round(vCrt * 100 / vN, 2);
                grid.Rows[y].Cells[9].Value = Math.Round(vMis * 100 / vN, 2);
                grid.Rows[y].Cells[10].Value = Math.Round(vCrt * 100 / vrootN , 2);
                grid.Rows[y].Cells[11].Value = Math.Round(vMis * 100 / vrootN, 2);

                if (n.DescendentLst.Count == 0) {
                    vSumTermCrt += vCrt;
                    vSumTermCases += vN;
                }

                if (y > 0) {
                    vSumCrtPerc += vCrt * 100 / vN;
                    vSumMisPerc += vMis * 100 / vN;
                    vSumRelCrtPerc += vCrt * 100 / vrootN;
                    vSumRelMisPerc += vMis * 100 / vrootN;
                    vSumCrt += vCrt;
                    vSumMis += vMis;
                    vSumCases += vN;
                }


            }
            grid.Rows[y].Cells[0].Value = "Sum(Crt)/Sum(vN)";
            grid.Rows[y].Cells[1].Value = Math.Round(vSumCrt / vSumCases, 4);
            grid.Rows[y].Cells[2].Value = "Sum(Mis)/Sum(vN)";
            grid.Rows[y].Cells[3].Value = Math.Round(vSumMis / vSumCases, 4);
            grid.Rows[y].Cells[8].Value = Math.Round(vSumCrtPerc, 2);
            grid.Rows[y].Cells[9].Value = Math.Round(vSumMisPerc, 2);
            grid.Rows[y].Cells[10].Value = Math.Round(vSumRelCrtPerc, 2);
            grid.Rows[y].Cells[11].Value = Math.Round(vSumRelMisPerc, 2);
            
            grid.Rows[y+1].Cells[0].Value = "Rdc imp";
            grid.Rows[y + 1].Cells[1].Value = Math.Round(Def.Tree.ReductionInImp,4);

            grid.Rows[y + 1].Cells[2].Value = "TermCrt/Term";
            grid.Rows[y + 1].Cells[3].Value = Math.Round((vSumTermCrt / vSumTermCases),4);
        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }


    }
}