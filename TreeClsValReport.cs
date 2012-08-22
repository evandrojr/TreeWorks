using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Spartacus {
    class TreeClsValReport {

        string reportFilename;

        public TreeClsValReport(string reportFilename) {
            this.reportFilename = reportFilename;
        }

        public void Dump() {

            int rowCount = Def.Tree.NodeLst.Count + 1;
            string row ="Node,tN,vN,tn%,vn%,tClass,vClass,Correct,Misclassified,Correct %,Misclassified %,Relative Correct %,Relative Misclassified %";

            NodeTargetCategorical n;

            TextWriter tw = new StreamWriter(reportFilename + ".csv");
            tw.WriteLine(row);

            string sqlVlCountMinority, sqlVlMostCommonClass, sqlvN, vMostCommonClass, sqlVnCorrect;
            double tN, vN, vMis, vCrt, vrootN = -1;
            double vSumCrtPerc = 0, vSumMisPerc = 0, vSumRelCrtPerc = 0, vSumRelMisPerc = 0;
            double vSumCrt = 0, vSumMis = 0, vSumCases = 0;
            double vSumTermCrt = 0, vSumTermCases = 0;
            int y = 0;

            for (y = 0; y < Def.Tree.NodeLst.Count; ++y) {
                n = (NodeTargetCategorical)Def.Tree.NodeLst[y];
                tN = (double)n.Table.RowCount;
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

                row = n.Id + "," + n.Table.RowCount + "," + vN + "," + Math.Round((double)(n.Table.RowCount * 100) / (Def.Tree.Root.Table.RowCount), 2) + "," + Math.Round(vN * 100 / vrootN, 2) + "," + mode.Key + "," + vMostCommonClass + "," + vCrt + "," + vMis + "," + Math.Round(vCrt * 100 / vN, 2) + "," + Math.Round(vMis * 100 / vN, 2) + "," + Math.Round(vCrt * 100 / vrootN, 2) + "," + Math.Round(vMis * 100 / vrootN, 2);
                tw.WriteLine(row);

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
            row=",Sum(Correct)/Sum(vN)," + Math.Round(vSumCrt / vSumCases, 4) + ",Sum(Mis)/Sum(vN)," + Math.Round(vSumMis / vSumCases, 4) + ",,,,," + Math.Round(vSumCrtPerc, 2) + "," + Math.Round(vSumMisPerc, 2) + "," + Math.Round(vSumRelCrtPerc, 2) + "," + Math.Round(vSumRelMisPerc, 2);
            tw.WriteLine(row);

            row = ",Rdc imp," + Math.Round(Def.Tree.ReductionInImp, 4) + ",TermCrt/Term," + Math.Round((vSumTermCrt / vSumTermCases),4);
            tw.WriteLine(row);

            // close the stream
            tw.Close();


        }


    }
}
