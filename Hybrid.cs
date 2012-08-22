using System;
using System.Collections.Generic;
using System.Text;

namespace Spartacus {
    class Hybrid {






        //public static double MinImpCatProgressive(NodeTargetCategorical n, Predictor p) {

        //    //For some partitions gets the min Impiance
        //    int valueCount = p.DistinctValuesCount;
        //    double impBest= double.NaN;
        //    int i;
        //    int instanceCount = n.Table.RowCount;
        //    double imp = Double.NaN;
        //    double impIfValueGoesLeft = Double.NaN, impIfValueGoesRight = Double.NaN;
        //    List<NTT> nttLst;
        //    double impBeforePhase2;
        //    int improvementCode = 0;

        //    p.SplitStatus = Predictor.SplitStatusEnum.CanBeUsed;

        //    if (valueCount < 2) {
        //        p.SplitStatus = Predictor.SplitStatusEnum.OnlyOneValueAvailable;
        //        p.Gain = 0;
        //        return 0;
        //    }

        //    List<string> lComb = new List<string>(valueCount);
        //    List<string> rComb = new List<string>(valueCount);
        //    SortedList<string, int> lPredVal = new SortedList<string, int>();
        //    SortedList<string, int> rPredVal = new SortedList<string, int>();

        //    string sql =
        //    @"SELECT ALL " +
        //        " count(*), " +
        //        Def.DbBsTb + "." + p.Variable.Name + ",  " +
        //        Def.DbBsTb + "." + Def.Schema.Target.Name + " " +
        //    "FROM "
        //        + Def.DbBsTb + "," + Def.DbTrTb + n.Id + " " +
        //    "WHERE "
        //        + Def.DbBsTb + "." + Def.DbTableIdName + " = " +
        //        Def.DbTrTb + n.Id + "." + Def.DbTableIdName + " AND " +
        //        Def.DbBsTb + "." + p.Variable.Name + " IS NOT NULL " +
        //    "GROUP BY " +
        //        Def.DbBsTb + "." + Def.Schema.Target.Name + ", " +
        //        Def.DbBsTb + "." + p.Variable.Name + " ";
        //    nttLst = Def.Db.GetNTTLst(sql);

        //    lComb.Clear(); rComb.Clear();
        //    lPredVal.Clear(); rPredVal.Clear();
        //    lComb.Add(p.ValueSd.Keys[0]);  // Adds the 1st value to the combination of the left node
        //    rComb.Add(p.ValueSd.Keys[1]);  // Adds the 2nd value to the combination of the right node
        //    //Done only if the number of values is 2
        //    if (valueCount == 2) {
        //        imp = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
        //        p.ChildrenGroups.ValueGroupLst[0].Clear();
        //        p.ChildrenGroups.ValueGroupLst[1].Clear();
        //        if (Double.IsNaN(imp)) {
        //            p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
        //            p.ImpUniMin = Double.NaN;
        //            p.Gain = Double.NaN;
        //            p.ChildrenGroups.ValueGroupLst[0].Clear();
        //            p.ChildrenGroups.ValueGroupLst[1].Clear();
        //            return 0;
        //        }
        //        foreach(string s in lComb){
        //            p.ChildrenGroups.ValueGroupLst[0].Add(s);
        //        }
        //        foreach(string s in rComb){
        //            p.ChildrenGroups.ValueGroupLst[1].Add(s);
        //        }
        //        n.ImpBestUniSplit = imp;
        //        p.Gain = (n.Imp - imp) * 100 / n.Imp;
        //        p.Gain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;
        //        return p.Gain;
        //    }
        //    //IF valueCount > 2
        //    for (i = 2; i < valueCount; ++i) {
        //        //try to adding to the left
        //        lComb.Add(p.ValueSd.Keys[i]);
        //        impIfValueGoesLeft = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
        //        //Changes the side
        //        rComb.Add(p.ValueSd.Keys[i]);
        //        lComb.RemoveAt(lComb.Count - 1);
        //        impIfValueGoesRight = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
        //        if (!Double.IsNaN(impIfValueGoesLeft) && !Double.IsNaN(impIfValueGoesRight)) {
        //            if (impIfValueGoesLeft < impIfValueGoesRight) {
        //                imp = impIfValueGoesLeft;
        //                lComb.Add(p.ValueSd.Keys[i]);
        //                rComb.RemoveAt(rComb.Count - 1);
        //            } else {
        //                imp = impIfValueGoesRight;
        //            }
        //        } else {
        //            if (Double.IsNaN(impIfValueGoesLeft)) {
        //                imp = impIfValueGoesRight;
        //            }
        //            if (Double.IsNaN(impIfValueGoesRight)) {
        //                imp = impIfValueGoesLeft;
        //                lComb.Add(p.ValueSd.Keys[i]);
        //                rComb.RemoveAt(rComb.Count - 1);
        //            }
        //        }
        //    }  

        //        impBest = imp;
        //        if (Def.ClfOptimisationLevelForCatSearch >= 1) {

        //            #region enhanced progressive phase 1


        //            // 0 = no improvement
        //            // 1 = 1
        //            // 2 = 2
        //            // 3 = 3

        //            //if (valueCount > 2) {

        //            // Final combinations for the two 1st values 
        //            // 1- Removes 1st val from left and put in the right
        //            // 2- Removes 2nd val from right and send to left
        //            // 3- Puts back the 1st val to left

        //            // 0) Initial status 
        //            // left  0xxxxxx
        //            // right 1xxxxxx

        //            // 1) 
        //            // left  xxxxxx
        //            // right 1xxxxxx0

        //            // 2)  
        //            // left  xxxxxx1
        //            // right xxxxxx0

        //            // 3)  
        //            // left  xxxxxx10
        //            // right xxxxxx

        //            // 1  
        //            lComb.RemoveAt(0);
        //            rComb.Add(p.ValueSd.Keys[0]);
        //            imp = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
        //            if (imp < impBest) {
        //                improvementCode = 1;
        //                impBest = imp;
        //            }

        //            // 2  
        //            rComb.RemoveAt(0);
        //            lComb.Add(p.ValueSd.Keys[1]);
        //            imp = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
        //            if (imp < impBest) {
        //                improvementCode = 2;
        //                impBest = imp;
        //            }

        //            // 3  
        //            rComb.RemoveAt(rComb.Count - 1);
        //            lComb.Add(p.ValueSd.Keys[0]);
        //            imp = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
        //            if (imp < impBest) {
        //                improvementCode = 3;
        //                impBest = imp;
        //            }

        //            if (improvementCode == 0) {
        //                rComb.Add(lComb[lComb.Count - 2]);
        //                lComb.RemoveAt(lComb.Count - 2);
        //            }
        //            if (improvementCode == 1) {
        //                lComb.RemoveAt(lComb.Count - 1);
        //                lComb.RemoveAt(lComb.Count - 1);
        //                rComb.Add(p.ValueSd.Keys[0]);
        //                rComb.Add(p.ValueSd.Keys[1]);
        //            }
        //            if (improvementCode == 2) {
        //                lComb.RemoveAt(lComb.Count - 1);
        //                rComb.RemoveAt(rComb.Count - 1);
        //                lComb.Add(p.ValueSd.Keys[1]);
        //                rComb.Add(p.ValueSd.Keys[0]);                        
        //            }

        //            #endregion  enhanced progressive phase 1

        //            #region enhanced progressive phase 2

        //            if (Def.ClfOptimisationLevelForCatSearch >= 2) {
        //                impBeforePhase2 = imp;
        //                int lCombCount = lComb.Count;
        //                for (int lidx = 0; lidx < lCombCount; ++lidx) {
        //                    rComb.Add(lComb[0]);
        //                    lComb.RemoveAt(0);

        //                    imp = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
        //                    if (imp < impBest) {
        //                        impBest = imp;
        //                    } else {
        //                        lComb.Add(rComb[rComb.Count - 1]);
        //                        rComb.RemoveAt(rComb.Count - 1);
        //                    }
        //                }

        //                int rCombCount = rComb.Count;
        //                for (int ridx = 0; ridx < rCombCount; ++ridx) {
        //                    lComb.Add(rComb[0]);
        //                    rComb.RemoveAt(0);

        //                    imp = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
        //                    if (imp < impBest) {
        //                        impBest = imp;
        //                    } else {
        //                        rComb.Add(lComb[lComb.Count - 1]);
        //                        lComb.RemoveAt(lComb.Count - 1);
        //                    }
        //                }
        //            }//If optimisation >=2
        //            #endregion phase 2
        //        }//If optimisation >=1

        //    if (Double.IsNaN(impBest)) {
        //        p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
        //        p.ImpUniMin = Double.NaN;
        //        p.Gain = Double.NaN;
        //        p.ChildrenGroups.ValueGroupLst[0].Clear();
        //        p.ChildrenGroups.ValueGroupLst[1].Clear();
        //        return 0;
        //    }

        //    p.ChildrenGroups.ValueGroupLst[0].Clear();
        //    p.ChildrenGroups.ValueGroupLst[1].Clear();
        //    foreach(string s in lComb){
        //        p.ChildrenGroups.ValueGroupLst[0].Add(s);
        //    }
        //    foreach(string s in rComb){
        //        p.ChildrenGroups.ValueGroupLst[1].Add(s);
        //    }
        //    imp = impBest;
        //    p.ImpUniMin = imp;
        //    p.Gain = (n.Imp - imp) * 100 / n.Imp;
        //    p.Gain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;
        //    return p.Gain;
        //}

        //private static double fillPredVals(SortedList<string, int> lPredVal, SortedList<string, int> rPredVal, List<string> lComb, List<string> rComb, List<NTT> nttLst, NodeTargetCategorical n) {

        //    int nodeLfItemCount, nodeRtItemCount;
        //    int instanceI;
        //    double imp = Double.NaN, lImp = Double.NaN, rImp = Double.NaN;

        //    nodeLfItemCount = nodeRtItemCount = 0;
        //    lPredVal.Clear(); rPredVal.Clear();
        //    for (instanceI = 0; instanceI < nttLst.Count; ++instanceI) {
        //        foreach (string ls in lComb) {
        //            if (nttLst[instanceI].T0 == ls) {
        //                if (!lPredVal.ContainsKey(nttLst[instanceI].T1))
        //                    lPredVal.Add(nttLst[instanceI].T1, (int)nttLst[instanceI].N);
        //                else
        //                    lPredVal[nttLst[instanceI].T1] += (int)nttLst[instanceI].N;
        //                nodeLfItemCount += (int)nttLst[instanceI].N;
        //                break;
        //            }
        //        }
        //        foreach (string rs in rComb) {
        //            if (nttLst[instanceI].T0 == rs) {
        //                if (!rPredVal.ContainsKey(nttLst[instanceI].T1))
        //                    rPredVal.Add(nttLst[instanceI].T1, (int)nttLst[instanceI].N);
        //                else
        //                    rPredVal[nttLst[instanceI].T1] += (int)nttLst[instanceI].N;
        //                nodeRtItemCount += (int)nttLst[instanceI].N;
        //                break;
        //            }
        //        }
        //    }
        //    if (nodeLfItemCount >= Def.TreeMinNumberOfCasesPerNode && nodeRtItemCount >= Def.TreeMinNumberOfCasesPerNode) {
        //        lImp = Gini.ImpCat(lPredVal, nodeLfItemCount);
        //        rImp = Gini.ImpCat(rPredVal, nodeRtItemCount);
        //        imp = (double)(nodeLfItemCount) / n.Table.RowCount * lImp + (double)nodeRtItemCount / n.Table.RowCount * rImp;
        //    }
        //    return imp;
        //}
    }
}
