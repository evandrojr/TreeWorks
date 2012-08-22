#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using System.Data;
using System.Windows.Forms;

#endregion

namespace Spartacus {
    
class Riv {

        //Sets the best p.SplitValue p.Gain
        //public static double MinInfoContSQL(NodeTargetContinuous n, Predictor p) {

        //    int i;
        //    double l, r, minVar, var;
        //    l = r = minVar = var = 0;

        //    //Tries each partition:
        //    //p.Gain = (n.Imp - minVar) * 100 / n.Imp;

        //    List<double> vlLst;
        //    List<double> NLst;
        //    OdbcTransaction dbTrans = null;
        //    dbTrans = Def.Db.Con.BeginTransaction();

        //    string sql =
        //    @"SELECT DISTINCT " +
        //        Def.DbBsTb + "." + p.Variable.Name + " " +
        //    "FROM " +
        //        Def.DbBsTb + "," + Def.DbTrTb + n.Id + " " +
        //    "WHERE " +
        //        Def.DbBsTb + "." + Def.DbTableIdName + " = " +
        //        Def.DbTrTb + n.Id + "." + Def.DbTableIdName + " " +
        //    "ORDER BY " +
        //        Def.DbBsTb + "." + p.Variable.Name;

        //    vlLst = Def.Db.GetNumberLst(sql, dbTrans);

        //    p.SplitStatus = Predictor.SplitStatusEnum.CanBeUsed;

        //    if (vlLst.Count == 0) {
        //        p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
        //        p.Gain = 0;
        //        return 0;
        //    }
        //    for (i = 0; i < vlLst.Count; ++i) {
        //        sql =
        //       @"SELECT 
        //            COALESCE(variance(" + n.Tree.Schema.Target.Name + "), 0), count(*) " +
        //        "FROM "
        //        + Def.DbBsTb + " , " + Def.DbTrTb + n.Id + " " +
        //        "WHERE "
        //            + Def.DbBsTb + "." + Def.DbTableIdName + "=" +
        //            Def.DbTrTb + n.Id + "." + Def.DbTableIdName + " and " +
        //            Def.DbBsTb + "." + p.Variable.Name + "<=" + vlLst[i];
        //        NLst = Def.Db.GetNumberRowLst(sql, dbTrans);
        //        l = NLst[0] * NLst[1] / n.Table.RowCount;
        //        sql =
        //       @"SELECT 
        //            COALESCE(variance(" + n.Tree.Schema.Target.Name + "), 0), count(*) " +
        //        "FROM "
        //        + Def.DbBsTb + " , " + Def.DbTrTb + n.Id + " " +
        //        "WHERE "
        //            + Def.DbBsTb + "." + Def.DbTableIdName + "=" +
        //            Def.DbTrTb + n.Id + "." + Def.DbTableIdName + " and " +
        //            Def.DbBsTb + "." + p.Variable.Name + ">" + vlLst[i];
        //        NLst = Def.Db.GetNumberRowLst(sql, dbTrans);
        //        r = NLst[0] * NLst[1] / n.Table.RowCount;
        //        var = (l + r);
        //        if (i == 0) {
        //            minVar = var;
        //            p.SplitValue = vlLst[i];
        //        } else {
        //            if (var < minVar) {
        //                minVar = var;
        //                p.SplitValue = vlLst[i];
        //            }
        //        }
        //    }
        //    dbTrans.Commit();
        //    p.Gain = (n.Imp - minVar) * 100 / n.Imp; ;
        //    return p.Gain;
        //}

        //============================================================================================================
        //============================================================================================================

        //Sets the best p.SplitValue p.Gain
        //Used to be:        public static double MinInfoCatHeuristic(NodeTargetContinuous n, Predictor p) {
        public static double MinInfoCat(NodeTargetContinuous n, Predictor p) {

            int i, bestPartitionSplitPoint = 0;
            double minVar, lVar, rVar, instanceCount = n.Table.RowCount, partitionInfo;
            lVar = rVar = minVar = double.NaN;
            List<string> valLst;
            List<NNT> DepIndepLst;
            List<string> left = new List<string>();

            string sqlAverage =
            @"SELECT " +
                Def.DbBsTb + "." + p.Variable.Name + " " +
            "FROM "
                + Def.DbBsTb + "," + Def.DbTrTb + n.Id + " " +
            "WHERE "
                + Def.DbBsTb + "." + Def.DbTableIdName + " = " +
                Def.DbTrTb + n.Id + "." + Def.DbTableIdName + " " +
                "AND " + Def.DbBsTb + "." + p.Variable.Name + " IS NOT NULL " +
            "GROUP BY " +
                Def.DbBsTb + "." + p.Variable.Name + " " +
            "ORDER BY avg("
                + Def.DbBsTb + "." + Def.Schema.Target.Name + ")";
            valLst = Def.Db.GetTextLst(sqlAverage);

            if (valLst.Count == 0) {
                p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
                p.Gain = 0;
                return 0;
            }

            string sqlDepVar =
            @"SELECT " +
                Def.DbBsTb + "." + Def.Schema.Target.Name + ", " +
                "count(*), " +
                Def.DbBsTb + "." + p.Variable.Name + " " +
            "FROM "
                + Def.DbBsTb + "," + Def.DbTrTb + n.Id + " " +
            "WHERE "
                + Def.DbBsTb + "." + Def.DbTableIdName + " = " +
                Def.DbTrTb + n.Id + "." + Def.DbTableIdName + " " +
                "AND " + Def.DbBsTb + "." + p.Variable.Name + " IS NOT NULL " +
            "GROUP BY " +
                Def.DbBsTb + "." + Def.Schema.Target.Name + ", " +
                Def.DbBsTb + "." + p.Variable.Name;
            DepIndepLst = Def.Db.GetNNTLst(sqlDepVar);

            p.SplitStatus = Predictor.SplitStatusEnum.CanBeUsed;

            left.Add(valLst[0]);
            for (i = 0; i < valLst.Count - 1; ++i) {
                if (Double.IsNaN(minVar)) {
                    partitionInfo = PartitionInfo(left, DepIndepLst, n, p);
                    if (Double.IsNaN(partitionInfo) == false) {
                        minVar = partitionInfo;
                        bestPartitionSplitPoint = i;
                    }
                } else {
                    partitionInfo = PartitionInfo(left, DepIndepLst, n, p);
                    if (Double.IsNaN(partitionInfo) == false && partitionInfo < minVar) {
                        minVar = partitionInfo;
                        bestPartitionSplitPoint = i;
                    }
                }
                left.Add(valLst[i + 1]);
            }
            if(Double.IsNaN(minVar)){
                p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
                p.Gain = 0;
                return 0;
            }

            p.ChildrenGroups.ValueGroupLst[0].Clear();
            p.ChildrenGroups.ValueGroupLst[1].Clear();

            //Add left node values

         //   int IndexOfKeyValLsti; // Just to check negative indexes
            for (i = 0; i <= bestPartitionSplitPoint; ++i) {
  //              IndexOfKeyValLsti = p.ValueSd.IndexOfKey(valLst[i]);
    //            if (IndexOfKeyValLsti < 0)
     //               MessageBox.Show("Negative index in MinInfoCatHeuristic", "Error");
                      p.ChildrenGroups.ValueGroupLst[0].Add(valLst[i]);
            }
            //Add right node values
            for (i = bestPartitionSplitPoint + 1; i < valLst.Count; ++i) {
             //   IndexOfKeyValLsti = p.ValueSd.IndexOfKey(valLst[i]);
               // if (IndexOfKeyValLsti < 0)
                 //   MessageBox.Show("Negative index in MinInfoCatHeuristic", "Error");
                p.ChildrenGroups.ValueGroupLst[1].Add(valLst[i]);
            }

            p.Gain = (n.Imp - minVar) * 100 / n.Imp;
            p.Gain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;

            return p.Gain;
        }


        //============================================================================================================
        //============================================================================================================

        private static double PartitionInfo(List<string> leftLst, List<NNT> dataLst, Node node, Predictor pred) {

            //Returns Double.NaN if the variance can not be calculated

            int di;
            string indep;
            double dep, leftSum = 0, leftSum2 = 0, rightSum = 0, rightSum2 = 0;
            double leftVar, rightVar, freq, leftRowCounter = 0, rightRowCounter = 0, partitionInfo;

            for (di = 0; di < dataLst.Count; ++di) {
                dep = dataLst[di].N0;
                freq = dataLst[di].N1;
                indep = dataLst[di].T;
                if (leftLst.Contains(indep)) {
                    leftSum += dep * freq;
                    leftSum2 += dep * dep * freq;
                    leftRowCounter += freq;
                } else {
                    rightSum += dep * freq;
                    rightSum2 += dep * dep * freq;
                    rightRowCounter += freq;
                }
            }
            if (leftRowCounter < Def.TreeMinNumberOfCasesPerNode || rightRowCounter < Def.TreeMinNumberOfCasesPerNode) {
                return double.NaN;
            }

            leftVar = ((leftSum2 * leftRowCounter) - (leftSum * leftSum)) / (leftRowCounter * (leftRowCounter - 1));
            rightVar = ((rightSum2 * rightRowCounter) - (rightSum * rightSum)) / (rightRowCounter * (rightRowCounter - 1));
            partitionInfo = ((leftVar * leftRowCounter) + (rightVar * rightRowCounter)) / (leftRowCounter + rightRowCounter);

            return partitionInfo;
        }


        public static double Info(int i1, int i2, List<N4> AvsLst, out int rowCount) {

            double s1, s2, s3;
            int i;

            rowCount = 0;

            //AvsLst[i].N0 = y
            //AvsLst[i].N1 = Value of the dependent varible
            //AvsLst[i].N2 = Frequency of y
            //AvsLst[i].N3 = Total of distinct registries until that row

            //s1 = SUM (y)
            //s2 = SUM (y^2)
            //s3 = N
            s1 = s2 = s3 = 0;
            if (i2 - i1 + 1 == 1) {
                rowCount = (int)AvsLst[0].N3;
                return 0;
            }
            for (i = i1; i <= i2; ++i) {
                s1 += AvsLst[i].N0 * AvsLst[i].N2;
                s2 += AvsLst[i].N0 * AvsLst[i].N0 * AvsLst[i].N2;
            }
            if (i1 == 0)
                s3 = AvsLst[i2].N3;
            else
                s3 = AvsLst[i2].N3 - AvsLst[i1 - 1].N3;
            rowCount = (int)s3;
            if (s3 <= 1)
                return 0;
            return ((s3 * s2) - (s1 * s1)) / (s3 * (s3 - 1));
        }


        //============================================================================================================
        //============================================================================================================

        //Sets the best p.SplitValue p.Gain
        //public static double MinInfoCatHeuristicSLOW(NodeTargetContinuous n, Predictor p) {

        //    int i, bestPartitionSplitPoint = 0;
        //    //            int dfd; //delete
        //    double minVar = 0, var, lVar, rVar, instanceCount = n.Table.RowCount;
        //    lVar = rVar = minVar = var = 0;
        //    List<NNT> nntLst;
        //    List<string> left = new List<string>();
        //    List<string> right = new List<string>();
        //    List<string> leftBest = new List<string>();
        //    List<string> rightBest = new List<string>();


        //    //Tries each partition:

        //    //AvsLst[i].N0 = y
        //    //AvsLst[i].N1 = Value of the dependent varible
        //    //AvsLst[i].N2 = Frequency of y
        //    //AvsLst[i].N3 = Total of distinct registries until that row

        //    string sql =
        //    @"SELECT count(*), " +
        //           "0, " +
        //           Def.DbBsTb + "." + p.Variable.Name + " " +
        //    "FROM "
        //        + Def.DbBsTb + "," + Def.DbTrTb + n.Id + " " +
        //    "WHERE "
        //        + Def.DbBsTb + "." + Def.DbTableIdName + " = " +
        //        Def.DbTrTb + n.Id + "." + Def.DbTableIdName + " " +
        //    "GROUP BY " +
        //        Def.DbBsTb + "." + p.Variable.Name + " " +
        //    "ORDER BY avg("
        //        + Def.DbBsTb + "." + Def.Schema.Target.Name + ")";
        //    nntLst = Def.Db.GetNNTLst(sql);

        //    //N1 is the number of registries until a given row

        //    left.Add(nntLst[0].T);
        //    leftBest.Add(nntLst[0].T);
        //    if (nntLst.Count > 0) {
        //        nntLst[0].N1 = nntLst[0].N0;
        //        for (i = 1; i < nntLst.Count; ++i) {
        //            nntLst[i].N1 = nntLst[i - 1].N1 + nntLst[i].N0;
        //            right.Add(nntLst[i].T);
        //            rightBest.Add(nntLst[i].T);
        //        }
        //    }

        //    n.DescendentImpPreCalculated = new List<double>(2);
        //    n.DescendentImpPreCalculated.Add(0);
        //    n.DescendentImpPreCalculated.Add(0);

        //    p.SplitStatus = Predictor.SplitStatusEnum.CanBeUsed;

        //    if (nntLst.Count == 0) {
        //        p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
        //        p.Gain = 0;
        //        return 0;
        //    }
        //    for (i = 0; i < nntLst.Count - 1; ++i) {

        //        //if(i==12 && p.Variable.Name=="apache_2")
        //        //    dfd=8;
        //        lVar = Fcn.InfoTimesCount(left, n, p);
        //        rVar = Fcn.InfoTimesCount(right, n, p);
        //        if (i == 0) {
        //            minVar = (lVar + rVar) / instanceCount;
        //            n.DescendentImpPreCalculated[0] = lVar;
        //            n.DescendentImpPreCalculated[1] = rVar;
        //        } else {
        //            var = (lVar + rVar) / instanceCount;
        //            if (var < minVar) {
        //                minVar = var;
        //                bestPartitionSplitPoint = i;
        //                n.DescendentImpPreCalculated[0] = lVar;
        //                n.DescendentImpPreCalculated[1] = rVar;
        //                leftBest.Clear();
        //                rightBest.Clear();
        //                foreach (string s in left)
        //                    leftBest.Add(s);
        //                foreach (string s in right)
        //                    rightBest.Add(s);
        //            }
        //        }
        //        left.Add(right[0]);
        //        right.RemoveAt(0);
        //    }

        //    p.ChildrenGroups.ValueLst[0].Clear();
        //    p.ChildrenGroups.ValueLst[1].Clear();
        //    //Add left node values
        //    for (i = 0; i <= bestPartitionSplitPoint; ++i)
        //        p.ChildrenGroups.ValueLst[0].Add(p.CaseSd.IndexOfKey(nntLst[i].T));
        //    //Add right node values
        //    for (i = bestPartitionSplitPoint + 1; i < nntLst.Count; ++i)
        //        p.ChildrenGroups.ValueLst[1].Add(p.CaseSd.IndexOfKey(nntLst[i].T));


        //    p.Gain = (n.Imp - minVar) * 100 / n.Imp;
        //    return p.Gain;
        //}


        ////============================================================================================================
        ////============================================================================================================


        //Sets the best p.SplitValue p.Gain
        public static double MinInfoCont(NodeTargetContinuous n, Predictor p) {

            int i, leftRowCount = 0, rightRowCount = 0;
            //            int dfd; //delete
            double minVar, var, lVar, rVar, instanceCount = n.Table.RowCount;
            lVar = rVar = minVar = var = double.NaN;
            List<N4> AvsLst;
            List<int> thresholdIndexLst;
            //Tries each partition:

            //AvsLst[i].N0 = y
            //AvsLst[i].N1 = Value of the dependent varible
            //AvsLst[i].N2 = Frequency of y
            //AvsLst[i].N3 = Total of distinct registries until that row

            string sql =
            @"SELECT ALL "
                + Def.DbBsTb + "." + Def.Schema.Target.Name + ", " +
                Def.DbBsTb + "." + p.Variable.Name + ", " +
                " count(" + Def.DbBsTb + "." + p.Variable.Name + "), 0 " +
            "FROM "
                + Def.DbBsTb + "," + Def.DbTrTb + n.Id + " " +
            "WHERE "
                + Def.DbBsTb + "." + Def.DbTableIdName + " = " +
                Def.DbTrTb + n.Id + "." + Def.DbTableIdName + " " +
                " AND " + Def.DbBsTb + "." + p.Variable.Name + " IS NOT NULL " +
            "GROUP BY " +
                Def.DbBsTb + "." + Def.Schema.Target.Name + ", " +
                Def.DbBsTb + "." + p.Variable.Name + " " +
            "ORDER BY " +
                Def.DbBsTb + "." + p.Variable.Name;
            AvsLst = Def.Db.GetN4Lst(sql);

            //N3 is the number of registries until a given row
            if (AvsLst.Count > 0) {
                AvsLst[0].N3 = AvsLst[0].N2;
                for (i = 1; i < AvsLst.Count; ++i)
                    AvsLst[i].N3 = AvsLst[i - 1].N3 + AvsLst[i].N2;
            }

            thresholdIndexLst = Fcn.SetPossibleThresholdIndexLst(AvsLst);
            if (thresholdIndexLst.Count == 0) {
                p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
                p.Gain = 0;
                return 0;
            }

            n.DescendentImpPreCalculated = new List<double>(2);
            n.DescendentImpPreCalculated.Add(0);
            n.DescendentImpPreCalculated.Add(0);

            p.SplitStatus = Predictor.SplitStatusEnum.CanBeUsed;

            if (AvsLst.Count == 0) {
                p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
                p.Gain = 0;
                return 0;
            }
            for (i = 0; i < thresholdIndexLst.Count; ++i) {
                lVar = Info(0, thresholdIndexLst[i], AvsLst, out leftRowCount);
                rVar = Info(thresholdIndexLst[i] + 1, AvsLst.Count - 1, AvsLst, out rightRowCount);
                if (Double.IsNaN(minVar) && leftRowCount >= Def.TreeMinNumberOfCasesPerNode && rightRowCount >= Def.TreeMinNumberOfCasesPerNode) {
                    var = (leftRowCount * lVar + rightRowCount * rVar) / (leftRowCount + rightRowCount);
                    if (Double.IsNaN(var) == false) {
                        minVar = var;
                        p.SplitValue = AvsLst[thresholdIndexLst[i]].N1;
                        n.DescendentImpPreCalculated[0] = lVar;
                        n.DescendentImpPreCalculated[1] = rVar;
                    }
                } else {
                    var = (leftRowCount * lVar + rightRowCount * rVar) / (leftRowCount + rightRowCount);
                    if (var < minVar && !Double.IsNaN(var) && leftRowCount >= Def.TreeMinNumberOfCasesPerNode && rightRowCount >= Def.TreeMinNumberOfCasesPerNode) {
                        minVar = var;
                        p.SplitValue = AvsLst[thresholdIndexLst[i]].N1;
                        n.DescendentImpPreCalculated[0] = lVar;
                        n.DescendentImpPreCalculated[1] = rVar;
                    }
                }
            }
            if (Double.IsNaN(minVar)) {
                p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
                p.Gain = 0;
                return 0;
            }

            p.Gain = (n.Imp - minVar) * 100 / n.Imp;
            p.Gain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;
            return p.Gain;
        }


        //============================================================================================================
        //============================================================================================================


        //Sets the best p.Gain, valueGroup.ValueLst[0] (left child) and valueGroup.ValueLst[1] (right child)
        //public static double MinInfoCatForVariableWithManyValues(NodeTargetContinuous n, Predictor p) {

        //    //For each possible partition gets the min Variance

        //    int valueCount = p.DistinctValuesCount;
        //    if (valueCount > Def.ClfMaxNumberOfValuesForFullSearch) {
        //        p.SplitStatus = Predictor.SplitStatusEnum.TooManyValuesToSearch;
        //        p.Gain = 0;
        //        return 0;
        //    }


        //    double partitionCount;
        //    double i, bestPartition = 0;
        //    double minVar = 0, var, lVar, rVar;
        //    bool firstTime = true;
        //    string binStr = "";
        //    int nodeLfItemCount, nodeRtItemCount, instanceI, pos, c;
        //    List<NNT> nntLst;
        //    NN nn;


        //    p.SplitStatus = Predictor.SplitStatusEnum.CanBeUsed;
        //    n.DescendentImpPreCalculated = new List<double>(2);
        //    n.DescendentImpPreCalculated.Add(0);
        //    n.DescendentImpPreCalculated.Add(0);

        //    List<string> lComb = new List<string>(valueCount);
        //    List<string> rComb = new List<string>(valueCount);
        //    List<NN> lPredVal = new List<NN>();
        //    List<NN> rPredVal = new List<NN>();

        //    string sql =
        //    @"SELECT ALL "
        //        + Def.DbBsTb + "." + Def.Schema.Target.Name + ", " +
        //        " count(*), " +
        //        Def.DbBsTb + "." + p.Variable.Name + " " +
        //    "FROM "
        //        + Def.DbBsTb + "," + Def.DbTrTb + n.Id + " " +
        //    "WHERE "
        //        + Def.DbBsTb + "." + Def.DbTableIdName + " = " +
        //        Def.DbTrTb + n.Id + "." + Def.DbTableIdName + " " +
        //    "GROUP BY " +
        //        Def.DbBsTb + "." + Def.Schema.Target.Name + ", " +
        //        Def.DbBsTb + "." + p.Variable.Name + " ";
        //    nntLst = Def.Db.GetNNTLst(sql);

        //    int instanceCount = n.Table.RowCount;

        //    partitionCount = (Math.Pow(2, valueCount - 1) - 1);
        //    //if (partitionCount > 65536)
        //    //    partitionCount = 65536; 


        //    double partitionCountMax = 0;
        //    if (partitionCount > Def.PartitionCountMax) {
        //        partitionCountMax = Def.PartitionCountMax;
        //    } else
        //        partitionCountMax = partitionCount;
        //    List<double> partLst = new List<double>((int)partitionCountMax);
        //    i = 1;

        //    //CHECK
        //    for (int t = 0; t < partitionCountMax; ++t) {
        //        partLst.Add((int)RNG.GetUniform(i, partitionCount));
        //        ++i;
        //    }

        //    //            for (i = 1; i <= partitionCount; ++i) { //Enumerates all the possible partition but the empty
        //    for (i = 0; i < partitionCountMax; ++i) {

        //        pos = 0;
        //        //                binStr = Fcn.Decimal2BinaryStr((double) i);
        //        binStr = Fcn.Decimal2BinaryStr(partLst[(int)i]);
        //        lComb.Clear(); rComb.Clear();
        //        lPredVal.Clear(); rPredVal.Clear();
        //        nodeLfItemCount = nodeRtItemCount = 0;
        //        for (c = 0; c < p.CaseSd.Count; ++c) {
        //            //                    if ((i & c) == c) // i & c == c
        //            if (binStr[binStr.Length - 1 - c] == '1')
        //                lComb.Add(p.CaseSd.Keys[pos]); //if the 'case' is in, put it on the left
        //            else
        //                rComb.Add(p.CaseSd.Keys[pos]);//else, in the right side
        //            ++pos;
        //        }
        //        for (instanceI = 0; instanceI < nntLst.Count; ++instanceI) {
        //            foreach (string ls in lComb) {
        //                if (nntLst[instanceI].T == ls) {
        //                    nn = new NN(nntLst[instanceI].N0, nntLst[instanceI].N1);
        //                    lPredVal.Add(nn);
        //                    nodeLfItemCount += (int)nntLst[instanceI].N1;
        //                    break;
        //                }
        //            }
        //            foreach (string rs in rComb) {
        //                if (nntLst[instanceI].T == rs) {
        //                    nn = new NN(nntLst[instanceI].N0, nntLst[instanceI].N1);
        //                    rPredVal.Add(nn);
        //                    nodeRtItemCount += (int)nntLst[instanceI].N1;
        //                    break;
        //                }
        //            }
        //        }
        //        if (nodeLfItemCount >= Def.TreeMinNumberOfCasesPerNode && nodeRtItemCount >= Def.TreeMinNumberOfCasesPerNode) {
        //            lVar = Fcn.Info(lPredVal);
        //            rVar = Fcn.Info(rPredVal);
        //            if (firstTime) {
        //                firstTime = false;
        //                bestPartition = partLst[(int)i];
        //                minVar = (double)(nodeLfItemCount) / instanceCount * lVar + (double)nodeRtItemCount / instanceCount * rVar;
        //                n.DescendentImpPreCalculated[0] = lVar;
        //                n.DescendentImpPreCalculated[1] = rVar;
        //                //Define the best division 
        //                //p.SplitValue = n.Table.GetNumber(p.Variable, thresholdIndexLst[i]);
        //            } else {
        //                var = (double)(nodeLfItemCount) / instanceCount * lVar + (double)nodeRtItemCount / instanceCount * rVar;
        //                if (var < minVar) {
        //                    minVar = var;
        //                    bestPartition = partLst[(int)i];
        //                    n.DescendentImpPreCalculated[0] = lVar;
        //                    n.DescendentImpPreCalculated[1] = rVar;
        //                    //p.SplitValue = n.Table.GetNumber(p.Variable, thresholdIndexLst[i]);
        //                }
        //            }
        //        }
        //    }

        //    if (bestPartition == 0) {
        //        p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
        //        p.Gain = 0;
        //        return 0;
        //    }

        //    //Set the possible children
        //    ValueGroup valueGroup = new ValueGroup(2);
        //    p.ChildrenGroups = valueGroup;

        //    pos = 0;
        //    binStr = Fcn.Decimal2BinaryStr((double)bestPartition);
        //    for (c = 0; c < p.CaseSd.Count; ++c) {
        //        //                if ((bestPartition & c) == c)
        //        if (binStr[binStr.Length - 1 - c] == '1')
        //            valueGroup.ValueLst[0].Add(pos); //if the 'case' is in put it on the left
        //        else
        //            valueGroup.ValueLst[1].Add(pos);//else, in the right side
        //        ++pos;
        //    }

        //    p.Gain = (n.Imp - minVar) * 100 / n.Imp;
        //    return p.Gain;
        //}



        //Sets the best p.Gain, valueGroup.ValueLst[0] (left child) and valueGroup.ValueLst[1] (right child)
      //public static double MinInfoCatForVariableWithFewValues(NodeTargetContinuous n, Predictor p) {

      //      //For each possible partition gets the min Variance

      //      int valueCount = p.DistinctValuesCount;
      //      if (valueCount > Def.ClfMaxNumberOfValuesForFullSearch) {
      //          p.SplitStatus = Predictor.SplitStatusEnum.TooManyValuesToSearch;
      //          p.Gain = 0;
      //          return 0;
      //      }

      //      int partitionCount, pos, instanceI;
      //      uint c, i, bestPartition = 0;
      //      double minVar = 0, var, lVar, rVar;
      //      bool firstTime = true;
      //      int nodeLfItemCount, nodeRtItemCount;
      //      List<NNT> nntLst;
      //      NN nn;

      //      p.SplitStatus = Predictor.SplitStatusEnum.CanBeUsed;
      //      n.DescendentImpPreCalculated = new List<double>(2);
      //      n.DescendentImpPreCalculated.Add(0);
      //      n.DescendentImpPreCalculated.Add(0);

      //      List<string> lComb = new List<string>(valueCount);
      //      List<string> rComb = new List<string>(valueCount);
      //      List<NN> lPredVal = new List<NN>();
      //      List<NN> rPredVal = new List<NN>();

      //      string sql =
      //      @"SELECT ALL "
      //          + Def.DbBsTb + "." + Def.Schema.Target.Name + ", " +
      //          " count(*), " +
      //          Def.DbBsTb + "." + p.Variable.Name + "  " +
      //      "FROM "
      //          + Def.DbBsTb + "," + Def.DbTrTb + n.Id + " " +
      //      "WHERE "
      //          + Def.DbBsTb + "." + Def.DbTableIdName + " = " +
      //          Def.DbTrTb + n.Id + "." + Def.DbTableIdName + " AND " +
      //          Def.DbBsTb + "." + p.Variable.Name + " IS NOT NULL " +
      //      "GROUP BY " +
      //          Def.DbBsTb + "." + Def.Schema.Target.Name + ", " +
      //          Def.DbBsTb + "." + p.Variable.Name + " ";
      //      nntLst = Def.Db.GetNNTLst(sql);

      //      int instanceCount = n.Table.RowCount;

      //      partitionCount = (int)(Math.Pow(2, valueCount - 1) - 1);

      //      for (i = 1; i <= partitionCount; ++i) { //Enumerates all the possible partition but the empty
      //          pos = 0;
      //          lComb.Clear(); rComb.Clear();
      //          lPredVal.Clear(); rPredVal.Clear();
      //          nodeLfItemCount = nodeRtItemCount = 0;
      //          for (c = 1; c <= partitionCount + 1; c *= 2) {
      //              if ((i & c) == c) // i & c == c
      //                  lComb.Add(p.CaseSd.Keys[pos]); //if the 'case' is in, put it on the left
      //              else
      //                  rComb.Add(p.CaseSd.Keys[pos]);//else, in the right side
      //              ++pos;
      //          }
      //          for (instanceI = 0; instanceI < nntLst.Count; ++instanceI) {
      //              foreach (string ls in lComb) {
      //                  if (nntLst[instanceI].T == ls) {
      //                      nn = new NN(nntLst[instanceI].N0, nntLst[instanceI].N1);
      //                      lPredVal.Add(nn);
      //                      nodeLfItemCount += (int)nntLst[instanceI].N1;
      //                      break;
      //                  }
      //              }
      //              foreach (string rs in rComb) {
      //                  if (nntLst[instanceI].T == rs) {
      //                      nn = new NN(nntLst[instanceI].N0, nntLst[instanceI].N1);
      //                      rPredVal.Add(nn);
      //                      nodeRtItemCount += (int)nntLst[instanceI].N1;
      //                      break;
      //                  }
      //              }
      //          }
      //          if (nodeLfItemCount >= Def.TreeMinNumberOfCasesPerNode && nodeRtItemCount >= Def.TreeMinNumberOfCasesPerNode) {
      //              lVar = Fcn.Info(lPredVal);
      //              rVar = Fcn.Info(rPredVal);
      //              if (firstTime) {
      //                  firstTime = false;
      //                  bestPartition = i;
      //                  minVar = (double)(nodeLfItemCount) / instanceCount * lVar + (double)nodeRtItemCount / instanceCount * rVar;
      //                  n.DescendentImpPreCalculated[0] = lVar;
      //                  n.DescendentImpPreCalculated[1] = rVar;
      //                  //Define the best division 
      //                  //p.SplitValue = n.Table.GetNumber(p.Variable, thresholdIndexLst[i]);
      //              } else {
      //                  var = (double)(nodeLfItemCount) / instanceCount * lVar + (double)nodeRtItemCount / instanceCount * rVar;
      //                  if (var < minVar) {
      //                      minVar = var;
      //                      bestPartition = i;
      //                      n.DescendentImpPreCalculated[0] = lVar;
      //                      n.DescendentImpPreCalculated[1] = rVar;
      //                      //p.SplitValue = n.Table.GetNumber(p.Variable, thresholdIndexLst[i]);
      //                  }
      //              }
      //          }
      //      }

      //      if (bestPartition == 0) {
      //          p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
      //          p.Gain = 0;
      //          return 0;
      //      }

      //      //Set the possible children
      //      ValueGroup valueGroup = new ValueGroup(2);
      //      p.ChildrenGroups = valueGroup;

      //      pos = 0;
      //      for (c = 1; c <= partitionCount + 1; c *= 2) {
      //          if ((bestPartition & c) == c)
      //              valueGroup.ValueLst[0].Add(pos); //if the 'case' is in put it on the left
      //          else
      //              valueGroup.ValueLst[1].Add(pos);//else, in the right side
      //          ++pos;
      //      }

      //      p.Gain = (n.Imp - minVar) * 100 / n.Imp;
      //      p.Gain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;
      //      return p.Gain;
      //  }

        //The gain is only for manual split
        //        double InfoCat(Predictor pred, List<double> lPredVal, List<double> rPredVal) {
        //
        //            double lVar = Fcn.Info(lPredVal);
        //            double rVar = Fcn.Info(rPredVal);
        //
        //            double var=1;
        //
        //            pred.Gain = 1.0 / var;
        //            return pred.Gain;
        //        }
    }
}
