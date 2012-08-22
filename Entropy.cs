#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

#endregion


namespace Spartacus {
    class Entropy {

        public static double NodeImp(NodeTargetCategorical node) {

            List<double> nLst = new List<double>();
            double info=0;

            string sql =
            @"SELECT " +
                      "count(" + Def.Schema.Target.Name + ") " + 
             "FROM " + 
                Def.DbBsTb + ", " + Def.DbTrTb + node.Id + " " +
             "WHERE " +
                Def.DbBsTb + "." + Def.DbTableIdName + "=" + 
                Def.DbTrTb + node.Id + "." + Def.DbTableIdName + " " +
             "GROUP BY " +
                    Def.Schema.Target.Name;
             nLst = Def.Db.GetNumberLst(sql);
             for (int i = 0; i < nLst.Count; ++i) {
                 info += (nLst[i] / node.Table.RowCount) * Math.Log(nLst[i] / node.Table.RowCount); 
             }
             return info * -1;
        }

        public static double ImpCat(SortedList<string, int> classText, int itemCount) {
            double sumP2 = 0;

            foreach (int J in classText.Values) {
                sumP2 += (double)J / itemCount * Math.Log((double)J / itemCount, 2);
            }
            return sumP2 * -1;

        }

        public static double ImpCont(int i1, int i2, List<N3T> AvcLst, out int rowCount) {

            double s1, s2, s3;
            int i;
            rowCount = 0;
            SortedDictionary<string, double> classFreq = new SortedDictionary<string, double>();
            //AvcLst[i].N0 = Value of the dependent varible
            //AvcLst[i].N1 = Frequency of y
            //AvcLst[i].N2 = Total of distinct registries until that row
            //AvcLst[i].T = y

            s1 = s2 = s3 = 0;
            if (i2 - i1 + 1 == 1) {
                rowCount = (int)AvcLst[0].N2;
                return 0;
            }
            for (i = i1; i <= i2; ++i) {
                if(classFreq.ContainsKey(AvcLst[i].T)){
                    classFreq[AvcLst[i].T]+=AvcLst[i].N1;
                }else{
                    classFreq.Add(AvcLst[i].T, AvcLst[i].N1);
                }
//               s1 += AvsLst[i].N0 * AvsLst[i].N2;
//               s2 += AvsLst[i].N0 * AvsLst[i].N0 * AvsLst[i].N2;
            }
            if (i1 == 0)
                s3 = AvcLst[i2].N2;
            else
                s3 = AvcLst[i2].N2 - AvcLst[i1 - 1].N2;
            rowCount = (int) s3;

            foreach (KeyValuePair<string, double> pair in classFreq) {
                s1 += pair.Value / rowCount * Math.Log(pair.Value / rowCount);
            }
            if (rowCount <= 1) {
                MessageBox.Show("Row Count should never be <=1");
                return 0;
            }
            return s1 * -1;
        }

        //Sets the best p.SplitValue p.Gain
        public static double MinImpCont(NodeTargetCategorical n, Predictor p) {

            int i, leftRowCount = 0, rightRowCount = 0;
            //            int dfd; //delete
            double minImp, imp, lImp, rImp, instanceCount = n.Table.RowCount;
            lImp = rImp = minImp = imp = double.NaN;
            List<N3T> AvcLst;
            List<int> thresholdIndexLst;
            //Tries each partition:

            //AvcLst[i].N0 = Value of the dependent varible
            //AvcLst[i].N1 = Frequency of y
            //AvcLst[i].N2 = Total of distinct registries until that row
            //AvcLst[i].T  = y

            string sql =
            @"SELECT ALL " +
                Def.DbBsTb + "." + p.Variable.Name + ", " +
                " count(" + Def.DbBsTb + "." + p.Variable.Name + "),0 , " +
                Def.DbBsTb + "." + Def.Schema.Target.Name + " " +
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
            AvcLst = Def.Db.GetN3TLst(sql);

            //N2 is the number of registries until a given row
            if (AvcLst.Count > 0) {
                AvcLst[0].N2 = AvcLst[0].N1;
                for (i = 1; i < AvcLst.Count; ++i)
                    AvcLst[i].N2 = AvcLst[i - 1].N2 + AvcLst[i].N1;
            }

            thresholdIndexLst = Fcn.SetPossibleThresholdIndexLst(AvcLst);
            if (thresholdIndexLst.Count == 0) {
                p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
                p.Gain = 0;
                return 0;
            }

            n.DescendentImpPreCalculated = new List<double>(2);
            n.DescendentImpPreCalculated.Add(0);
            n.DescendentImpPreCalculated.Add(0);

            p.SplitStatus = Predictor.SplitStatusEnum.CanBeUsed;
            if (AvcLst.Count == 0) {
                p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
                p.Gain = 0;
                return 0;
            }
            for (i = 0; i < thresholdIndexLst.Count; ++i) {
                lImp = ImpCont(0, thresholdIndexLst[i], AvcLst, out leftRowCount);
                rImp = ImpCont(thresholdIndexLst[i] + 1, AvcLst.Count - 1, AvcLst, out rightRowCount);
                if (Double.IsNaN(minImp) && leftRowCount >= Def.TreeMinNumberOfCasesPerNode && rightRowCount >= Def.TreeMinNumberOfCasesPerNode) {
                    imp = (leftRowCount * lImp + rightRowCount * rImp) / (leftRowCount + rightRowCount);
                    if (Double.IsNaN(imp) == false) {
                        minImp = imp;
                        p.SplitValue = AvcLst[thresholdIndexLst[i]].N0;
                        n.DescendentImpPreCalculated[0] = lImp;
                        n.DescendentImpPreCalculated[1] = rImp;
                    }
                } else {
                    imp = (leftRowCount * lImp + rightRowCount * rImp) / (leftRowCount + rightRowCount);
                    if (imp < minImp && !Double.IsNaN(imp) && leftRowCount >= Def.TreeMinNumberOfCasesPerNode && rightRowCount >= Def.TreeMinNumberOfCasesPerNode) {
                        minImp = imp;
                        p.SplitValue = AvcLst[thresholdIndexLst[i]].N0;
                        n.DescendentImpPreCalculated[0] = lImp;
                        n.DescendentImpPreCalculated[1] = rImp;
                    }
                }
            }
            if (Double.IsNaN(minImp)) {
                p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
                p.Gain = 0;
                return 0;
            }

            List<double> nLst;
            sql = @"
            SELECT 
                DISTINCT " + p.Variable.Name + " " +
            "FROM "
                + Def.DbBsTb + " , " + Def.DbTrTb + n.Id + " " +
            "WHERE "
                + Def.DbBsTb + "." + Def.DbTableIdName + "=" +
                Def.DbTrTb + n.Id + "." + Def.DbTableIdName + " AND " +
                p.Variable.Name + " IS NOT NULL " +
            "ORDER BY "
                + p.Variable.Name + " ASC ";
            nLst = Def.Db.GetNumberLst(sql);

            //Finds the angle bisector of the slit
            if (p.SplitValue != nLst[0] && p.SplitValue != nLst[nLst.Count - 2]) {
                for (i = 1; i < nLst.Count - 2; ++i) {
                    if (p.SplitValue == nLst[i]) {
                        p.SplitValue = (nLst[i - 1] + nLst[i + 1]) / 2;
                        break;
                    }
                }
            }

            p.ImpUniMin = minImp;
            p.Gain = (n.Imp - minImp) * 100 / n.Imp;
            p.Gain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;
            return p.Gain;
        }


        //Sets the best p.SplitValue p.Gain
//        public static double MinImpCont(NodeTargetCategorical n, Predictor p) {

//            int i, leftRowCount = 0, rightRowCount = 0;
//            //            int dfd; //delete
//            double minImp, info, lImp, rImp, instanceCount = n.Table.RowCount;
//            lImp = rImp = minImp = info = double.NaN;
//            List<N3T> AvcLst;
//            List<int> thresholdIndexLst;
//            //Tries each partition:

//            //AvcLst[i].N0 = Value of the dependent varible
//            //AvcLst[i].N1 = Frequency of y
//            //AvcLst[i].N2 = Total of distinct registries until that row
//            //AvcLst[i].T = y

//            string sql =
//            @"SELECT ALL " +
//                Def.DbBsTb + "." + p.Variable.Name + ", " +
//                " count(" + Def.DbBsTb + "." + p.Variable.Name + "),0 , " +
//                Def.DbBsTb + "." + Def.Schema.Target.Name + " " +
//            "FROM "
//                + Def.DbBsTb + "," + Def.DbTrTb + n.Id + " " +
//            "WHERE "
//                + Def.DbBsTb + "." + Def.DbTableIdName + " = " +
//                Def.DbTrTb + n.Id + "." + Def.DbTableIdName + " " +
//                " AND " + Def.DbBsTb + "." + p.Variable.Name + " IS NOT NULL " +
//            "GROUP BY " +
//                Def.DbBsTb + "." + Def.Schema.Target.Name + ", " +
//                Def.DbBsTb + "." + p.Variable.Name + " " +
//            "ORDER BY " +
//                Def.DbBsTb + "." + p.Variable.Name;
//            AvcLst = Def.Db.GetN3TLst(sql);

//            //N2 is the number of registries until a given row
//            if (AvcLst.Count > 0) {
//                AvcLst[0].N2 = AvcLst[0].N1;
//                for (i = 1; i < AvcLst.Count; ++i)
//                    AvcLst[i].N2 = AvcLst[i - 1].N2 + AvcLst[i].N1;
//            }

//            thresholdIndexLst = Fcn.SetPossibleThresholdIndexLst(AvcLst);
//            if (thresholdIndexLst.Count == 0) {
//                p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
//                p.Gain = 0;
//                return 0;
//            }

//            n.DescendentImpPreCalculated = new List<double>(2);
//            n.DescendentImpPreCalculated.Add(0);
//            n.DescendentImpPreCalculated.Add(0);

//            p.SplitStatus = Predictor.SplitStatusEnum.CanBeUsed;

//            if (AvcLst.Count == 0) {
//                p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
//                p.Gain = 0;
//                return 0;
//            }
//            for (i = 0; i < thresholdIndexLst.Count; ++i) {
//                lImp = ImpCont(0, thresholdIndexLst[i], AvcLst, out leftRowCount);
//                rImp = ImpCont(thresholdIndexLst[i] + 1, AvcLst.Count - 1, AvcLst, out rightRowCount);
//                if (Double.IsNaN(minImp) && leftRowCount >= Def.TreeMinNumberOfCasesPerNode && rightRowCount >= Def.TreeMinNumberOfCasesPerNode) {
//                    info = (leftRowCount * lImp + rightRowCount * rImp) / (leftRowCount + rightRowCount);
//                    if (Double.IsNaN(info) == false) {
//                        minImp = info;
//                        p.SplitValue = AvcLst[thresholdIndexLst[i]].N0;
//                        n.DescendentImpPreCalculated[0] = lImp;
//                        n.DescendentImpPreCalculated[1] = rImp;
//                    }
//                } else {
//                    info = (leftRowCount * lImp + rightRowCount * rImp) / (leftRowCount + rightRowCount);
//                    if (info < minImp && !Double.IsNaN(info) && leftRowCount >= Def.TreeMinNumberOfCasesPerNode && rightRowCount >= Def.TreeMinNumberOfCasesPerNode) {
//                        minImp = info;
//                        p.SplitValue = AvcLst[thresholdIndexLst[i]].N0;
//                        n.DescendentImpPreCalculated[0] = lImp;
//                        n.DescendentImpPreCalculated[1] = rImp;
//                    }
//                }
//            }
//            if (Double.IsNaN(minImp)) {
//                p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
//                p.Gain = 0;
//                return 0;
//            }
//            List<double> nLst;
//            sql = @"
//            SELECT 
//                DISTINCT " + p.Variable.Name + " " +
//            "FROM "
//                + Def.DbBsTb + " , " + Def.DbTrTb + n.Id + " " +
//            "WHERE "
//                + Def.DbBsTb + "." + Def.DbTableIdName + "=" +
//                Def.DbTrTb + n.Id + "." + Def.DbTableIdName + " AND " +
//                p.Variable.Name + " IS NOT NULL " +
//            "ORDER BY "
//                + p.Variable.Name + " ASC ";
//            nLst = Def.Db.GetNumberLst(sql);

//            //Finds the angle bisector of the slit
//            if (p.SplitValue != nLst[0] && p.SplitValue != nLst[nLst.Count - 2]) {
//                for (i = 1; i < nLst.Count - 2; ++i) {
//                    if (p.SplitValue == nLst[i]) {
//                        p.SplitValue = (nLst[i - 1] + nLst[i + 1]) / 2;
//                        break;
//                    }
//                }
//            }



//            p.Gain = (n.Imp - minImp) * 100 / n.Imp;
//            p.Gain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;
//            return p.Gain;
//        }

        public static double Killed_MinRandomImp(NodeTargetCategorical n, Predictor p) {

            //For some partitions gets the min Impiance

            int valueCount = p.DistinctValuesCount;

            int pos, instanceI;
            double partitionCount;
            int c, i;
            double bestPartition = 0, minImp = Double.NaN, imp = Double.NaN, lImp = Double.NaN, rImp = Double.NaN;
            int nodeLfItemCount, nodeRtItemCount;
            List<NTT> nttLst;
            string binStr = "";

            p.SplitStatus = Predictor.SplitStatusEnum.CanBeUsed;
            n.DescendentImpPreCalculated = new List<double>(2);
            n.DescendentImpPreCalculated.Add(0);
            n.DescendentImpPreCalculated.Add(0);

            List<string> lComb = new List<string>(valueCount);
            List<string> rComb = new List<string>(valueCount);
            SortedList<string, int> lPredVal = new SortedList<string, int>();
            SortedList<string, int> rPredVal = new SortedList<string, int>();

            string sql =
            @"SELECT ALL " +
                " count(*), " +
                Def.DbBsTb + "." + p.Variable.Name + ",  " +
                Def.DbBsTb + "." + Def.Schema.Target.Name + " " +
            "FROM "
                + Def.DbBsTb + "," + Def.DbTrTb + n.Id + " " +
            "WHERE "
                + Def.DbBsTb + "." + Def.DbTableIdName + " = " +
                Def.DbTrTb + n.Id + "." + Def.DbTableIdName + " AND " +
                Def.DbBsTb + "." + p.Variable.Name + " IS NOT NULL " +
            "GROUP BY " +
                Def.DbBsTb + "." + Def.Schema.Target.Name + ", " +
                Def.DbBsTb + "." + p.Variable.Name + " ";
            nttLst = Def.Db.GetNTTLst(sql);

            int instanceCount = n.Table.RowCount;

            partitionCount = Math.Pow(2, valueCount - 1) - 1;

            double partitionCountMax = 0;
            if (partitionCount > 4095) {
                partitionCountMax = 4095;
            } else
                partitionCountMax = partitionCount;
            List<double> partLst = new List<double>((int)partitionCountMax);
            i = 1;

            //CHECK
            for (int t = 0; t < partitionCountMax; ++t) {
                partLst.Add((int)RNG.GetUniform(i, partitionCount));
                ++i;
            }

            for (i = 0; i < partitionCountMax; ++i) {
                pos = 0;
                binStr = Fcn.Decimal2BinaryStr(partLst[(int)i]);
                lComb.Clear(); rComb.Clear();
                lPredVal.Clear(); rPredVal.Clear();
                nodeLfItemCount = nodeRtItemCount = 0;
                for (c = 0; c < p.ValueSd.Count; ++c) {
                    if (binStr[binStr.Length - 1 - c] == '1')
                        lComb.Add(p.ValueSd.Keys[pos]); //if the 'case' is in, put it on the left
                    else
                        rComb.Add(p.ValueSd.Keys[pos]);//else, in the right side
                    ++pos;
                }
                for (instanceI = 0; instanceI < nttLst.Count; ++instanceI) {
                    foreach (string ls in lComb) {
                        if (nttLst[instanceI].T0 == ls) {
                            if (!lPredVal.ContainsKey(nttLst[instanceI].T1))
                                lPredVal.Add(nttLst[instanceI].T1, (int)nttLst[instanceI].N);
                            else
                                lPredVal[nttLst[instanceI].T1] += (int)nttLst[instanceI].N;
                            nodeLfItemCount += (int)nttLst[instanceI].N;
                            break;
                        }
                    }
                    foreach (string rs in rComb) {
                        if (nttLst[instanceI].T0 == rs) {
                            if (!rPredVal.ContainsKey(nttLst[instanceI].T1))
                                rPredVal.Add(nttLst[instanceI].T1, (int)nttLst[instanceI].N);
                            else
                                rPredVal[nttLst[instanceI].T1] += (int)nttLst[instanceI].N;
                            nodeRtItemCount += (int)nttLst[instanceI].N;
                            break;
                        }
                    }
                }
                if (nodeLfItemCount >= Def.TreeMinNumberOfCasesPerNode && nodeRtItemCount >= Def.TreeMinNumberOfCasesPerNode) {
                    lImp = ImpCat(lPredVal, nodeLfItemCount);
                    rImp = ImpCat(rPredVal, nodeRtItemCount);
                    if (Double.IsNaN(minImp)) {
                        bestPartition = partLst[(int)i];
                        minImp = (double)(nodeLfItemCount) / instanceCount * lImp + (double)nodeRtItemCount / instanceCount * rImp;
                        n.DescendentImpPreCalculated[0] = lImp;
                        n.DescendentImpPreCalculated[1] = rImp;
                    } else {
                        imp = (double)(nodeLfItemCount) / instanceCount * lImp + (double)nodeRtItemCount / instanceCount * rImp;
                        if (imp < minImp) {
                            minImp = imp;
                            bestPartition = partLst[(int)i];
                            n.DescendentImpPreCalculated[0] = lImp;
                            n.DescendentImpPreCalculated[1] = rImp;
                        }
                    }
                }
            }

            if (bestPartition == 0) {
                p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
                p.Gain = 0;
                return 0;
            }

            //Set the possible children
            ValueGroup valueGroup = new ValueGroup(p, 2);
            p.ChildrenGroups = valueGroup;

            pos = 0;
            binStr = Fcn.Decimal2BinaryStr((double)bestPartition);
            for (c = 0; c < p.ValueSd.Count; ++c) {
                //                if ((bestPartition & c) == c)
                if (binStr[binStr.Length - 1 - c] == '1')
                    valueGroup.AddValueFromIndex(pos, 0); //if the 'case' is in put it on the left
                else
                    valueGroup.AddValueFromIndex(pos, 1);//else, in the right side
                ++pos;
            }

            p.Gain = (n.Imp - minImp) * 100 / n.Imp;
            p.Gain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;
            return p.Gain;
        }

        ////Sets the best p.Gain, valueGroup.ValueGroupLst[0] (left child) and valueGroup.ValueGroupLst[1] (right child)
        
        //DELETE Later commmented on 04/07/2006!
        //public static double MinFullSearchImp(NodeTargetCategorical n, Predictor p) {

        //    //For each possible partition gets the min Impiance

        //    int valueCount = p.DistinctValuesCount;
        //    if (valueCount > Def.ClfMaxNumberOfValuesForFullSearch) {
        //        p.SplitStatus = Predictor.SplitStatusEnum.TooManyValuesToSearch;
        //        p.Gain = 0;
        //        return 0;
        //    }

        //    int partitionCount, pos, instanceI;
        //    uint c, i, bestPartition = 0;
        //    double minImp = Double.NaN, imp = Double.NaN, lImp = Double.NaN, rImp = Double.NaN;
        //    int nodeLfItemCount, nodeRtItemCount;
        //    List<NTT> nttLst;

        //    p.SplitStatus = Predictor.SplitStatusEnum.CanBeUsed;
        //    n.DescendentImpPreCalculated = new List<double>(2);
        //    n.DescendentImpPreCalculated.Add(0);
        //    n.DescendentImpPreCalculated.Add(0);

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

        //    int instanceCount = n.Table.RowCount;

        //    partitionCount = (int)(Math.Pow(2, valueCount - 1) - 1);

        //    for (i = 1; i <= partitionCount; ++i) { //Enumerates all the possible partition but the empty
        //        pos = 0;
        //        lComb.Clear(); rComb.Clear();
        //        lPredVal.Clear(); rPredVal.Clear();
        //        nodeLfItemCount = nodeRtItemCount = 0;
        //        for (c = 1; c <= partitionCount + 1; c *= 2) {
        //            if ((i & c) == c) // i & c == c
        //                lComb.Add(p.ValueSd.Keys[pos]); //if the 'case' is in, put it on the left
        //            else
        //                rComb.Add(p.ValueSd.Keys[pos]);//else, in the right side
        //            ++pos;
        //        }
        //        for (instanceI = 0; instanceI < nttLst.Count; ++instanceI) {
        //            foreach (string ls in lComb) {
        //                if (nttLst[instanceI].T0 == ls) {
        //                    if (!lPredVal.ContainsKey(nttLst[instanceI].T1))
        //                        lPredVal.Add(nttLst[instanceI].T1, (int)nttLst[instanceI].N);
        //                    else
        //                        lPredVal[nttLst[instanceI].T1] += (int)nttLst[instanceI].N;
        //                    nodeLfItemCount += (int)nttLst[instanceI].N;
        //                    break;
        //                }
        //            }
        //            foreach (string rs in rComb) {
        //                if (nttLst[instanceI].T0 == rs) {
        //                    if (!rPredVal.ContainsKey(nttLst[instanceI].T1))
        //                        rPredVal.Add(nttLst[instanceI].T1, (int)nttLst[instanceI].N);
        //                    else
        //                        rPredVal[nttLst[instanceI].T1] += (int)nttLst[instanceI].N;
        //                    nodeRtItemCount += (int)nttLst[instanceI].N;
        //                    break;
        //                }
        //            }
        //        }
        //        if (nodeLfItemCount >= Def.TreeMinNumberOfCasesPerNode && nodeRtItemCount >= Def.TreeMinNumberOfCasesPerNode) {
        //            lImp = ImpCat(lPredVal, nodeLfItemCount);
        //            rImp = ImpCat(rPredVal, nodeRtItemCount);
        //            if (Double.IsNaN(minImp)) {
        //                bestPartition = i;
        //                minImp = (double)(nodeLfItemCount) / instanceCount * lImp + (double)nodeRtItemCount / instanceCount * rImp;
        //                n.DescendentImpPreCalculated[0] = lImp;
        //                n.DescendentImpPreCalculated[1] = rImp;
        //            } else {
        //                imp = (double)(nodeLfItemCount) / instanceCount * lImp + (double)nodeRtItemCount / instanceCount * rImp;
        //                if (imp < minImp) {
        //                    minImp = imp;
        //                    bestPartition = i;
        //                    n.DescendentImpPreCalculated[0] = lImp;
        //                    n.DescendentImpPreCalculated[1] = rImp;
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
        //    ValueGroup valueGroup = new ValueGroup(p, 2);
        //    p.ChildrenGroups = valueGroup;

        //    pos = 0;
        //    for (c = 1; c <= partitionCount + 1; c *= 2) {
        //        if ((bestPartition & c) == c)
        //            valueGroup.AddValueFromIndex(pos, 0); //if the 'case' is in put it on the left
        //        else
        //            valueGroup.AddValueFromIndex(pos, 1);//else, in the right side
        //        ++pos;
        //    }

        //    p.Gain = (n.Imp - minImp) * 100 / n.Imp;
        //    p.Gain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;
        //    return p.Gain;
        //}

        //Sets the best p.Gain, valueGroup.ValueGroupLst[0] (left child) and valueGroup.ValueGroupLst[1] (right child)
        public static double MinImpCatFullSearch(NodeTargetCategorical n, Predictor p) {

            //For each possible partition gets the min Imp

            int valueCount = p.DistinctValuesCount;
            if (valueCount > Def.ClfMaxNumberOfValuesForFullSearch) {
                p.SplitStatus = Predictor.SplitStatusEnum.TooManyValuesToSearch;
                p.Gain = 0;
                return 0;
            }

            int partitionCount, pos, instanceI;
            uint c, i, bestPartition = 0;
            double minImp = Double.NaN, imp = Double.NaN, lImp = Double.NaN, rImp = Double.NaN;
            int nodeLfItemCount, nodeRtItemCount;
            List<NTT> nttLst;

            p.SplitStatus = Predictor.SplitStatusEnum.CanBeUsed;
            n.DescendentImpPreCalculated = new List<double>(2);
            n.DescendentImpPreCalculated.Add(0);
            n.DescendentImpPreCalculated.Add(0);

            List<string> lComb = new List<string>(valueCount);
            List<string> rComb = new List<string>(valueCount);
            SortedList<string, int> lPredVal = new SortedList<string, int>();
            SortedList<string, int> rPredVal = new SortedList<string, int>();

            string sql =
            @"SELECT ALL " +
                " count(*), " +
                Def.DbBsTb + "." + p.Variable.Name + ",  " +
                Def.DbBsTb + "." + Def.Schema.Target.Name + " " +
            "FROM "
                + Def.DbBsTb + "," + Def.DbTrTb + n.Id + " " +
            "WHERE "
                + Def.DbBsTb + "." + Def.DbTableIdName + " = " +
                Def.DbTrTb + n.Id + "." + Def.DbTableIdName + " AND " +
                Def.DbBsTb + "." + p.Variable.Name + " IS NOT NULL " +
            "GROUP BY " +
                Def.DbBsTb + "." + Def.Schema.Target.Name + ", " +
                Def.DbBsTb + "." + p.Variable.Name + " ";
            nttLst = Def.Db.GetNTTLst(sql);

            int instanceCount = n.Table.RowCount;

            partitionCount = (int)(Math.Pow(2, valueCount - 1) - 1);

            for (i = 1; i <= partitionCount; ++i) { //Enumerates all the possible partition but the empty
                pos = 0;
                lComb.Clear(); rComb.Clear();
                lPredVal.Clear(); rPredVal.Clear();
                nodeLfItemCount = nodeRtItemCount = 0;
                for (c = 1; c <= partitionCount + 1; c *= 2) {
                    if ((i & c) == c) // i & c == c
                        lComb.Add(p.ValueSd.Keys[pos]); //if the 'case' is in, put it on the left
                    else
                        rComb.Add(p.ValueSd.Keys[pos]);//else, in the right side
                    ++pos;
                }
                for (instanceI = 0; instanceI < nttLst.Count; ++instanceI) {
                    foreach (string ls in lComb) {
                        if (nttLst[instanceI].T0 == ls) {
                            if (!lPredVal.ContainsKey(nttLst[instanceI].T1))
                                lPredVal.Add(nttLst[instanceI].T1, (int)nttLst[instanceI].N);
                            else
                                lPredVal[nttLst[instanceI].T1] += (int)nttLst[instanceI].N;
                            nodeLfItemCount += (int)nttLst[instanceI].N;
                            break;
                        }
                    }
                    foreach (string rs in rComb) {
                        if (nttLst[instanceI].T0 == rs) {
                            if (!rPredVal.ContainsKey(nttLst[instanceI].T1))
                                rPredVal.Add(nttLst[instanceI].T1, (int)nttLst[instanceI].N);
                            else
                                rPredVal[nttLst[instanceI].T1] += (int)nttLst[instanceI].N;
                            nodeRtItemCount += (int)nttLst[instanceI].N;
                            break;
                        }
                    }
                }
                if (nodeLfItemCount >= Def.TreeMinNumberOfCasesPerNode && nodeRtItemCount >= Def.TreeMinNumberOfCasesPerNode) {
                    lImp = ImpCat(lPredVal, nodeLfItemCount);
                    rImp = ImpCat(rPredVal, nodeRtItemCount);
                    if (Double.IsNaN(minImp)) {
                        bestPartition = i;
                        minImp = (double)(nodeLfItemCount) / instanceCount * lImp + (double)nodeRtItemCount / instanceCount * rImp;
                        n.DescendentImpPreCalculated[0] = lImp;
                        n.DescendentImpPreCalculated[1] = rImp;
                    } else {
                        imp = (double)(nodeLfItemCount) / instanceCount * lImp + (double)nodeRtItemCount / instanceCount * rImp;
                        if (imp < minImp) {
                            minImp = imp;
                            bestPartition = i;
                            n.DescendentImpPreCalculated[0] = lImp;
                            n.DescendentImpPreCalculated[1] = rImp;
                        }
                    }
                }
            }
            if (bestPartition == 0) {
                p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
                p.Gain = 0;
                return 0;
            }

            //Set the possible children
            ValueGroup valueGroup = new ValueGroup(p, 2);
            p.ChildrenGroups = valueGroup;

            pos = 0;
            for (c = 1; c <= partitionCount + 1; c *= 2) {
                if ((bestPartition & c) == c)
                    valueGroup.AddValueFromIndex(pos, 0); //if the 'case' is in put it on the left
                else
                    valueGroup.AddValueFromIndex(pos, 1);//else, in the right side
                ++pos;
            }
            p.ImpUniMin = minImp;
            p.Gain = (n.Imp - minImp) * 100 / n.Imp;
            p.Gain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;
            return p.Gain;
        }

        public static double MinImpCatProgressive(NodeTargetCategorical n, Predictor p) {

            //For some partitions gets the min Impiance
            int valueCount = p.DistinctValuesCount;
            double impBest = double.NaN;
            int i;
            int instanceCount = n.Table.RowCount;
            double imp = Double.NaN;
            double impIfValueGoesLeft = Double.NaN, impIfValueGoesRight = Double.NaN;
            List<NTT> nttLst;
            double impBeforePhase2;
            int improvementCode = 0;

            p.SplitStatus = Predictor.SplitStatusEnum.CanBeUsed;

            if (valueCount < 2) {
                p.SplitStatus = Predictor.SplitStatusEnum.OnlyOneValueAvailable;
                p.Gain = 0;
                return 0;
            }

            List<string> lComb = new List<string>(valueCount);
            List<string> rComb = new List<string>(valueCount);
            SortedList<string, int> lPredVal = new SortedList<string, int>();
            SortedList<string, int> rPredVal = new SortedList<string, int>();

            string sql =
            @"SELECT ALL " +
                " count(*), " +
                Def.DbBsTb + "." + p.Variable.Name + ",  " +
                Def.DbBsTb + "." + Def.Schema.Target.Name + " " +
            "FROM "
                + Def.DbBsTb + "," + Def.DbTrTb + n.Id + " " +
            "WHERE "
                + Def.DbBsTb + "." + Def.DbTableIdName + " = " +
                Def.DbTrTb + n.Id + "." + Def.DbTableIdName + " AND " +
                Def.DbBsTb + "." + p.Variable.Name + " IS NOT NULL " +
            "GROUP BY " +
                Def.DbBsTb + "." + Def.Schema.Target.Name + ", " +
                Def.DbBsTb + "." + p.Variable.Name + " ";
            nttLst = Def.Db.GetNTTLst(sql);

            lComb.Clear(); rComb.Clear();
            lPredVal.Clear(); rPredVal.Clear();
            lComb.Add(p.ValueSd.Keys[0]);  // Adds the 1st value to the combination of the left node
            rComb.Add(p.ValueSd.Keys[1]);  // Adds the 2nd value to the combination of the right node
            //Done only if the number of values is 2
            if (valueCount == 2) {
                imp = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
                p.ChildrenGroups.ValueGroupLst[0].Clear();
                p.ChildrenGroups.ValueGroupLst[1].Clear();
                if (Double.IsNaN(imp)) {
                    p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
                    p.ImpUniMin = Double.NaN;
                    p.Gain = Double.NaN;
                    p.ChildrenGroups.ValueGroupLst[0].Clear();
                    p.ChildrenGroups.ValueGroupLst[1].Clear();
                    return 0;
                }
                foreach (string s in lComb) {
                    p.ChildrenGroups.ValueGroupLst[0].Add(s);
                }
                foreach (string s in rComb) {
                    p.ChildrenGroups.ValueGroupLst[1].Add(s);
                }
                n.ImpBestUniSplit = imp;
                p.Gain = (n.Imp - imp) * 100 / n.Imp;
                p.Gain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;
                return p.Gain;
            }
            //IF valueCount > 2
            for (i = 2; i < valueCount; ++i) {
                //try to adding to the left
                lComb.Add(p.ValueSd.Keys[i]);
                impIfValueGoesLeft = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
                //Changes the side
                rComb.Add(p.ValueSd.Keys[i]);
                lComb.RemoveAt(lComb.Count - 1);
                impIfValueGoesRight = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
                if (!Double.IsNaN(impIfValueGoesLeft) && !Double.IsNaN(impIfValueGoesRight)) {
                    if (impIfValueGoesLeft < impIfValueGoesRight) {
                        imp = impIfValueGoesLeft;
                        lComb.Add(p.ValueSd.Keys[i]);
                        rComb.RemoveAt(rComb.Count - 1);
                    } else {
                        imp = impIfValueGoesRight;
                    }
                } else {
                    if (Double.IsNaN(impIfValueGoesLeft)) {
                        imp = impIfValueGoesRight;
                    }
                    if (Double.IsNaN(impIfValueGoesRight)) {
                        imp = impIfValueGoesLeft;
                        lComb.Add(p.ValueSd.Keys[i]);
                        rComb.RemoveAt(rComb.Count - 1);
                    }
                }
            }

            impBest = imp;
            if (Def.ClfOptimisationLevelForCatSearch >= 1) {

                #region enhanced progressive phase 1


                // 0 = no improvement
                // 1 = 1
                // 2 = 2
                // 3 = 3

                //if (valueCount > 2) {

                // Final combinations for the two 1st values 
                // 1- Removes 1st val from left and put in the right
                // 2- Removes 2nd val from right and send to left
                // 3- Puts back the 1st val to left

                // 0) Initial status 
                // left  0xxxxxx
                // right 1xxxxxx

                // 1) 
                // left  xxxxxx
                // right 1xxxxxx0

                // 2)  
                // left  xxxxxx1
                // right xxxxxx0

                // 3)  
                // left  xxxxxx10
                // right xxxxxx

                // 1  
                lComb.RemoveAt(0);
                rComb.Add(p.ValueSd.Keys[0]);
                imp = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
                if (imp < impBest) {
                    improvementCode = 1;
                    impBest = imp;
                }

                // 2  
                rComb.RemoveAt(0);
                lComb.Add(p.ValueSd.Keys[1]);
                imp = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
                if (imp < impBest) {
                    improvementCode = 2;
                    impBest = imp;
                }

                // 3  
                rComb.RemoveAt(rComb.Count - 1);
                lComb.Add(p.ValueSd.Keys[0]);
                imp = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
                if (imp < impBest) {
                    improvementCode = 3;
                    impBest = imp;
                }

                if (improvementCode == 0) {
                    rComb.Add(lComb[lComb.Count - 2]);
                    lComb.RemoveAt(lComb.Count - 2);
                }
                if (improvementCode == 1) {
                    lComb.RemoveAt(lComb.Count - 1);
                    lComb.RemoveAt(lComb.Count - 1);
                    rComb.Add(p.ValueSd.Keys[0]);
                    rComb.Add(p.ValueSd.Keys[1]);
                }
                if (improvementCode == 2) {
                    lComb.RemoveAt(lComb.Count - 1);
                    rComb.RemoveAt(rComb.Count - 1);
                    lComb.Add(p.ValueSd.Keys[1]);
                    rComb.Add(p.ValueSd.Keys[0]);
                }

                #endregion  enhanced progressive phase 1

                #region enhanced progressive phase 2

                if (Def.ClfOptimisationLevelForCatSearch >= 2) {
                    impBeforePhase2 = imp;
                    int lCombCount = lComb.Count;
                    for (int lidx = 0; lidx < lCombCount; ++lidx) {
                        rComb.Add(lComb[0]);
                        lComb.RemoveAt(0);

                        imp = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
                        if (imp < impBest) {
                            impBest = imp;
                        } else {
                            lComb.Add(rComb[rComb.Count - 1]);
                            rComb.RemoveAt(rComb.Count - 1);
                        }
                    }

                    int rCombCount = rComb.Count;
                    for (int ridx = 0; ridx < rCombCount; ++ridx) {
                        lComb.Add(rComb[0]);
                        rComb.RemoveAt(0);

                        imp = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
                        if (imp < impBest) {
                            impBest = imp;
                        } else {
                            rComb.Add(lComb[lComb.Count - 1]);
                            lComb.RemoveAt(lComb.Count - 1);
                        }
                    }
                }//If optimisation >=2
                #endregion phase 2
            }//If optimisation >=1

            if (Double.IsNaN(impBest)) {
                p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
                p.ImpUniMin = Double.NaN;
                p.Gain = Double.NaN;
                p.ChildrenGroups.ValueGroupLst[0].Clear();
                p.ChildrenGroups.ValueGroupLst[1].Clear();
                return 0;
            }

            p.ChildrenGroups.ValueGroupLst[0].Clear();
            p.ChildrenGroups.ValueGroupLst[1].Clear();
            foreach (string s in lComb) {
                p.ChildrenGroups.ValueGroupLst[0].Add(s);
            }
            foreach (string s in rComb) {
                p.ChildrenGroups.ValueGroupLst[1].Add(s);
            }
            imp = impBest;
            p.ImpUniMin = imp;
            p.Gain = (n.Imp - imp) * 100 / n.Imp;
            p.Gain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;
            return p.Gain;
        }

        private static double fillPredVals(SortedList<string, int> lPredVal, SortedList<string, int> rPredVal, List<string> lComb, List<string> rComb, List<NTT> nttLst, NodeTargetCategorical n) {

            int nodeLfItemCount, nodeRtItemCount;
            int instanceI;
            double imp = Double.NaN, lImp = Double.NaN, rImp = Double.NaN;

            nodeLfItemCount = nodeRtItemCount = 0;
            lPredVal.Clear(); rPredVal.Clear();
            for (instanceI = 0; instanceI < nttLst.Count; ++instanceI) {
                foreach (string ls in lComb) {
                    if (nttLst[instanceI].T0 == ls) {
                        if (!lPredVal.ContainsKey(nttLst[instanceI].T1))
                            lPredVal.Add(nttLst[instanceI].T1, (int)nttLst[instanceI].N);
                        else
                            lPredVal[nttLst[instanceI].T1] += (int)nttLst[instanceI].N;
                        nodeLfItemCount += (int)nttLst[instanceI].N;
                        break;
                    }
                }
                foreach (string rs in rComb) {
                    if (nttLst[instanceI].T0 == rs) {
                        if (!rPredVal.ContainsKey(nttLst[instanceI].T1))
                            rPredVal.Add(nttLst[instanceI].T1, (int)nttLst[instanceI].N);
                        else
                            rPredVal[nttLst[instanceI].T1] += (int)nttLst[instanceI].N;
                        nodeRtItemCount += (int)nttLst[instanceI].N;
                        break;
                    }
                }
            }
            if (nodeLfItemCount >= Def.TreeMinNumberOfCasesPerNode && nodeRtItemCount >= Def.TreeMinNumberOfCasesPerNode) {
                lImp = ImpCat(lPredVal, nodeLfItemCount);
                rImp = ImpCat(rPredVal, nodeRtItemCount);
                imp = (double)(nodeLfItemCount) / n.Table.RowCount * lImp + (double)nodeRtItemCount / n.Table.RowCount * rImp;
            }
            return imp;
        }

    }
}
